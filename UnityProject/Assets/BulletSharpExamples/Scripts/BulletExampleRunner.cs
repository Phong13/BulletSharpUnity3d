using UnityEngine;
using System.Collections;
using BulletSharp;
using BulletSharpExamples;

public class BulletExampleRunner : MonoBehaviour {
    protected static BulletExampleRunner singleton;

    public BulletSharpExamples.Graphics graphics;
    public SoftDemo.SoftDemo demo;

    public GameObject cubePrefab;
    public GameObject ropePrefab;
    
    //singleton not sure if it needs to be
    public static BulletExampleRunner Get() {
        if (singleton == null) {
            BulletExampleRunner[] ws = FindObjectsOfType<BulletExampleRunner>();
            if (ws.Length == 1) {
                singleton = ws[0];
            } else if (ws.Length == 0) {
                Debug.LogError("Need to add a BulletExampleRunner to the scene");
            } else {
                Debug.LogError("Found more than one dynamics world.");
                singleton = ws[0];
                for (int i = 1; i < ws.Length; i++) {
                    GameObject.Destroy(ws[i].gameObject);
                }
            }
        }
        return singleton;
    }

    void Start() {
        demo = new SoftDemo.SoftDemo();
        demo.Run();

        //create the Unity versions of the bullet rigid bodies.
        //a better way to do this would be to have Component wrappers that create the
        //bullet components and add them to the bullet physics world. I did it this
        //way because we are working with non-unity example code files that I want to
        //work with minimal changes.
        for (int i = 0; i < demo.World.NumCollisionObjects; i++) {
            BulletSharp.CollisionObject obj = demo.World.CollisionObjectArray[i];
            if (obj is BulletSharp.SoftBody.SoftBody) {
                CreateUnitySoftBodyProxy(obj as BulletSharp.SoftBody.SoftBody);
            } else if (obj is BulletSharp.RigidBody) {
                CreateUnityRigidBodyProxy(obj as BulletSharp.RigidBody);
            } else {
                Debug.LogError("Not implemented");
            }
        }
    }

    void FixedUpdate() {
        demo.OnUpdate();
    }

    void OnDestroy() {
        demo.Dispose();
    }

    public void CreateUnityRigidBodyProxy(BulletSharp.RigidBody body) {
        CollisionShape cs = body.CollisionShape;
        if (cs is BoxShape) {
            BoxShape bxcs = cs as BoxShape;
            GameObject cube = Instantiate<GameObject>(cubePrefab);
            BulletSharp.Math.Vector3 s = bxcs.HalfExtentsWithMargin;
            MeshRenderer mr = cube.GetComponentInChildren<MeshRenderer>();
            mr.transform.localScale = s.ToUnity() * 2f;
            Matrix4x4 m = body.WorldTransform.ToUnity();
            cube.transform.position = BSExtensionMethods.ExtractTranslationFromMatrix(ref m);
            cube.transform.rotation = BSExtensionMethods.ExtractRotationFromMatrix(ref m);
            cube.transform.localScale = BSExtensionMethods.ExtractScaleFromMatrix(ref m);
            BulletRigidBodyProxy p = cube.GetComponent<BulletRigidBodyProxy>();
            p.target = body;
        } else {
            Debug.LogError("Unknown collision shape.");
        }
    }

    public void CreateUnitySoftBodyProxy(BulletSharp.SoftBody.SoftBody body) {
        //determine what kind of soft body it is
        //rope
        GameObject rope = Instantiate<GameObject>(ropePrefab);
        LineRenderer lr = rope.GetComponent<LineRenderer>();
        lr.SetVertexCount(body.Nodes.Count);
        BulletRopeProxy ropeProxy = rope.GetComponent<BulletRopeProxy>();
        ropeProxy.target = body;
    }
}
