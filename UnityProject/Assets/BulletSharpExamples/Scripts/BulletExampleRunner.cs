using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BulletSharp;
using BulletSharpExamples;
using DemoFramework;

public class BulletExampleRunner : MonoBehaviour {
    protected static BulletExampleRunner singleton;
    public BulletSharpExamples.Graphics graphics;
    public DemoFramework.Demo demo;

    public Material mat;
    public Material groundMat;
    public GameObject cubePrefab;
    public GameObject ropePrefab;
    public GameObject softBodyPrefab;

    public List<GameObject> createdObjs = new List<GameObject>();

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
    }

    public void PostOnInitializePhysics() {
        for (int i = 0; i < demo.World.CollisionObjectArray.Count; i++) {
            CollisionObject co = demo.World.CollisionObjectArray[i];
            CollisionShape cs = co.CollisionShape;
            GameObject go;
            if (cs.ShapeType == BroadphaseNativeType.SoftBodyShape) {
                BulletSharp.SoftBody.SoftBody sb = (BulletSharp.SoftBody.SoftBody)co;
                if (sb.Faces.Count == 0) {
                    //rope
                    go = CreateUnitySoftBodyRope(sb);
                } else {
                    go = CreateUnitySoftBodyCloth(sb);
                }
            } else {
                //rigid body
                if (cs.ShapeType == BroadphaseNativeType.CompoundShape) {
                    BulletSharp.Math.Matrix transform = co.WorldTransform;
                    go = new GameObject("Compund Shape");
                    BulletRigidBodyProxy rbp = go.AddComponent<BulletRigidBodyProxy>();
                    rbp.target = co as RigidBody;
                    foreach (BulletSharp.CompoundShapeChild child in (cs as CompoundShape).ChildList) {
                        BulletSharp.Math.Matrix childTransform = child.Transform;
                        GameObject ggo = new GameObject(child.ToString());
                        MeshFilter mf = ggo.AddComponent<MeshFilter>();
                        Mesh m = mf.mesh;
                        MeshFactory2.CreateShape(child.ChildShape, m);
                        MeshRenderer mr = ggo.AddComponent<MeshRenderer>();
                        mr.sharedMaterial = mat;
                        ggo.transform.SetParent(go.transform);
                        Matrix4x4 mt = childTransform.ToUnity();
                        ggo.transform.localPosition = BSExtensionMethods.ExtractTranslationFromMatrix(ref mt);
                        ggo.transform.localRotation = BSExtensionMethods.ExtractRotationFromMatrix(ref mt);
                        ggo.transform.localScale = BSExtensionMethods.ExtractScaleFromMatrix(ref mt);
                        
                        /*
                        BulletRigidBodyProxy rbp = ggo.AddComponent<BulletRigidBodyProxy>();
                        rbp.target = body;
                        return go;
                        */
                        //InitRigidBodyInstance(colObj, child.ChildShape, ref childTransform);
                    }
                } else {
                    go = CreateUnityRigidBody(co as RigidBody);
                }
            }
            createdObjs.Add(go);
            Debug.Log("Created Unity Shape for " + co);
        }
    }

    void Update() {
        if (demo.Input != null) {
            demo.Input.ClearKeyCache();
            for (int i = 0; i < BulletSharpExamples.Input.UnityKeys.Length; i++) {
                KeyCode k = BulletSharpExamples.Input.UnityKeys[i];
                if (UnityEngine.Input.GetKey(k)) {
                    demo.Input.KeysDown.Add(BulletSharpExamples.Input.BSKeys[i]);
                }
                if (UnityEngine.Input.GetKeyDown(k)) {
                    demo.Input.KeysPressed.Add(BulletSharpExamples.Input.BSKeys[i]);
                }
            }
            demo.OnHandleInput();
        }
    }

    void FixedUpdate() {
        demo.OnUpdate();
    }

    void OnDestroy() {
        demo.Dispose();
    }

    public void ExitPhysics() {
        for (int i = 0; i < createdObjs.Count; i++) {
            Destroy(createdObjs[i]);
        }
        createdObjs.Clear();
    }



    /*
    public GameObject CreateUnityRigidBodyProxy(BulletSharp.RigidBody body) {
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
            return p.gameObject;
        } else {
            Debug.LogError("Unknown collision shape. " + cs);
        }
        return null;
    }
    */

    public GameObject CreateUnityRigidBody(RigidBody body) {
        GameObject go = new GameObject(body.ToString());
        MeshFilter mf = go.AddComponent<MeshFilter>();
        Mesh m = mf.mesh;
        MeshFactory2.CreateShape(body.CollisionShape, m);
        MeshRenderer mr = go.AddComponent<MeshRenderer>();
        mr.sharedMaterial = mat;
        if (body.UserObject != null && body.UserObject.Equals("Ground")) {
            mr.sharedMaterial = groundMat;
        }
        BulletRigidBodyProxy rbp = go.AddComponent<BulletRigidBodyProxy>();
        rbp.target = body;
        return go;
    }

    public GameObject CreateUnitySoftBodyRope(BulletSharp.SoftBody.SoftBody body) {
        //determine what kind of soft body it is
        //rope
        GameObject rope = Instantiate<GameObject>(ropePrefab);
        LineRenderer lr = rope.GetComponent<LineRenderer>();
        lr.SetVertexCount(body.Nodes.Count);
        BulletRopeProxy ropeProxy = rope.GetComponent<BulletRopeProxy>();
        ropeProxy.target = body;
        return rope;
    }

    public GameObject CreateUnitySoftBodyCloth(BulletSharp.SoftBody.SoftBody body) {
        //build nodes 2 verts map
        Dictionary<BulletSharp.SoftBody.Node, int> node2vertIdx = new Dictionary<BulletSharp.SoftBody.Node, int>();
        for (int i = 0; i < body.Nodes.Count; i++) {
            node2vertIdx.Add(body.Nodes[i], i);
        }
        List<int> tris = new List<int>();
        for (int i = 0; i < body.Faces.Count; i++) {
            BulletSharp.SoftBody.Face f = body.Faces[i];
            if (f.N.Count != 3) {
                Debug.LogError("Face was not a triangle");
                continue;
            }
            for (int j = 0; j < f.N.Count; j++) { 
                tris.Add( node2vertIdx[f.N[j]]);
            }
        }
        GameObject go = Instantiate<GameObject>(softBodyPrefab);
        BulletSoftBodyProxy sbp = go.GetComponent<BulletSoftBodyProxy>();
        List<int> trisRev = new List<int>();
        for (int i = 0; i < tris.Count; i+=3) {
            trisRev.Add(tris[i]);
            trisRev.Add(tris[i + 2]);
            trisRev.Add(tris[i + 1]);
        }
        tris.AddRange(trisRev);
        sbp.target = body;
        sbp.verts = new Vector3[body.Nodes.Count];
        sbp.tris = tris.ToArray();
        return go;
    }

    public void CreateUnityMultiBodyLinkColliderProxy(MultiBodyLinkCollider body) {
        GameObject cube = Instantiate<GameObject>(cubePrefab);
        CollisionShape cs = body.CollisionShape;
        if (cs is BoxShape) {
            BoxShape bxcs = cs as BoxShape;
            BulletSharp.Math.Vector3 s = bxcs.HalfExtentsWithMargin;
            MeshRenderer mr = cube.GetComponentInChildren<MeshRenderer>();
            mr.transform.localScale = s.ToUnity() * 2f;
            Matrix4x4 m = body.WorldTransform.ToUnity();
            cube.transform.position = BSExtensionMethods.ExtractTranslationFromMatrix(ref m);
            cube.transform.rotation = BSExtensionMethods.ExtractRotationFromMatrix(ref m);
            cube.transform.localScale = BSExtensionMethods.ExtractScaleFromMatrix(ref m);
            Destroy(cube.GetComponent<BulletRigidBodyProxy>());
            BulletMultiBodyLinkColliderProxy cp = cube.AddComponent<BulletMultiBodyLinkColliderProxy>();
            cp.target = body;

        } else {
            Debug.LogError("Not implemented");
        }

    }
}
