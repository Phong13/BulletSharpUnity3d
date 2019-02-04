using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BulletSharp;
using BulletSharpExamples;
using DemoFramework;
using BulletUnity;

/*
WARNING

The DemoFramework that these examples run in is not implemented using good design from a Unity point of view. 
I would not recommend setting up Bullet physics in Unity based on the DemoFramework these examples run in.

This framework is designed to run the demos included in the bullet distribution with minimal modification. 
They are included here as a reference and to make sure things are working correctly.
*/
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
    public bool IsDebugDrawEnabled = false;
    public string[] demoNames = new string[] { "BasicDemo", "BenchmarkDemo", "Box2DDemo", "BspDemo", "CcdPhysicsDemo", "CharacterDemo", "CollisionInterfaceDemo", "ConcaveConvexCastDemo", "ConcaveRaycastDemo",
                                                "ConstraintDemo", "ConvexDecompositionDemo", "DistanceDemo", "FeatherStoneDemo", "GImpactTestDemo", "MotorDemo", "PendulumDemo", "RagdollDemo", "RollingFrictionDemo",
                                                "SerializeDemo", "SoftDemo", "VehicleDemo"};

    void Start()
    {
        RunDemo("BasicDemo");
    }

    void RunDemo(string nm)
    {
        if (nm.Equals("BasicDemo")) {
            demo = new BasicDemo.BasicDemo();
        }
        if (nm.Equals("BenchmarkDemo"))
        {
            demo = new BenchmarkDemo.BenchmarkDemo();
        }
        if (nm.Equals("Box2DDemo"))
        {
            demo = new Box2DDemo.Box2DDemo();
        }
        if (nm.Equals("BspDemo"))
        {
            demo = new BspDemo.BspDemo();
        }
        if (nm.Equals("CcdPhysicsDemo"))
        {
            demo = new CcdPhysicsDemo.CcdPhysicsDemo();
        }
        if (nm.Equals("CharacterDemo"))
        {
            demo = new CharacterDemo.CharacterDemo();
        }
        if (nm.Equals("CollisionInterfaceDemo"))
        {
            demo = new CollisionInterfaceDemo.CollisionInterfaceDemo();
        }
        if (nm.Equals("ConcaveConvexCastDemo"))
        {
            demo = new ConcaveConvexCastDemo.ConcaveConvexCastDemo();
        }
        if (nm.Equals("ConcaveRaycastDemo"))
        {
            demo = new ConcaveRaycastDemo.ConcaveRaycastDemo();
        }
        if (nm.Equals("ConstraintDemo"))
        {
            demo = new ConstraintDemo.ConstraintDemo();
        }
        if (nm.Equals("ConvexDecompositionDemo"))
        {
            demo = new ConvexDecompositionDemo.ConvexDecompositionDemo();
        }
        if (nm.Equals("DistanceDemo"))
        {
            demo = new DistanceDemo.DistanceDemo();
        }
        if (nm.Equals("FeatherStoneDemo"))
        {
            demo = new FeatherStoneDemo.FeatherStoneDemo();
        }
        if (nm.Equals("GImpactTestDemo"))
        {
            demo = new GImpactTestDemo.GImpactTestDemo();
        }
        if (nm.Equals("MotorDemo"))
        {
            demo = new MotorDemo.MotorDemo();
        }
        if (nm.Equals("PendulumDemo"))
        {
            demo = new PendulumDemo.PendulumDemo();
        }
        if (nm.Equals("RagdollDemo"))
        {
            demo = new RagdollDemo.RagdollDemo();
        }
        if (nm.Equals("RollingFrictionDemo"))
        {
            demo = new RollingFrictionDemo.RollingFrictionDemo();
        }
        if (nm.Equals("SerializeDemo"))
        {
            demo = new SerializeDemo.SerializeDemo();
        }
        if (nm.Equals("SoftDemo"))
        {
            demo = new SoftDemo.SoftDemo();
        }
        if (nm.Equals("VehicleDemo"))
        {
            demo = new VehicleDemo.VehicleDemo();
        }
        demo.DebugDrawMode = DebugDrawModes.DrawWireframe;
        demo.Run();
        IsDebugDrawEnabled = false;
    }

    bool showDemoNames = false;
    public GUILayoutOption w = GUILayout.MinWidth(150);
    int maxPerCol = 15;
    void OnGUI()
    {
        if (GUILayout.Button("Demo List", GUILayout.MinWidth(120), GUILayout.MinHeight(44)))
        {
            showDemoNames = !showDemoNames;
        }
        if (showDemoNames)
        {
            GUILayout.BeginHorizontal(GUILayout.Width(1000));
            GUILayout.BeginVertical("box", w);
            int counter = 0;
            for (int j = 0; j < demoNames.Length; j++)
            {
                if (GUILayout.Button(demoNames[j], GUILayout.MinWidth(100), GUILayout.MinHeight(44)))
                {
                    if (demo != null)
                    {
                        demo.ExitPhysics();
                    }
                    RunDemo(demoNames[j]);
                }
                counter++;
                if (counter > 0 && counter % maxPerCol == 0)
                {
                    GUILayout.EndVertical();
                    GUILayout.BeginVertical("box", w);
                }
            }
            GUILayout.EndVertical();
            GUILayout.BeginVertical("box", w);
            if (demo is SoftDemo.SoftDemo)
            {
                SoftDemo.SoftDemo sd = (SoftDemo.SoftDemo) demo;
                SoftDemo.SoftDemo.DemoConstructor[] sdemos = sd.demos;
                for (int j = 0; j < sdemos.Length; j++)
                {
                    string nm = sdemos[j].Method.Name;
                    if (GUILayout.Button(nm, GUILayout.MinWidth(100), GUILayout.MinHeight(44)))
                    {
                        sd.demo = j;
                        sd.ClientResetScene();
                    }
                    counter++;
                    if (counter > 0 && counter % maxPerCol == 0)
                    {
                        GUILayout.EndVertical();
                        GUILayout.BeginVertical("box", w);
                    }
                }
            }
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
        }
    }


    public void OnDrawGizmos()
    {
        if (demo != null && demo.World != null)
        {
            IsDebugDrawEnabled = false;
            demo.World.DebugDrawWorld();
        }
    }

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
                    //BulletSharp.Math.Matrix transform = co.WorldTransform;
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
                        ggo.transform.localPosition = BSExtensionMethods2.ExtractTranslationFromMatrix(ref mt);
                        ggo.transform.localRotation = BSExtensionMethods2.ExtractRotationFromMatrix(ref mt);
                        ggo.transform.localScale = BSExtensionMethods2.ExtractScaleFromMatrix(ref mt);

                        /*
                        BulletRigidBodyProxy rbp = ggo.AddComponent<BulletRigidBodyProxy>();
                        rbp.target = body;
                        return go;
                        */
                        //InitRigidBodyInstance(colObj, child.ChildShape, ref childTransform);
                    }
                } else if (cs.ShapeType == BroadphaseNativeType.CapsuleShape) {
                    CapsuleShape css = (CapsuleShape) cs;
                    GameObject ggo = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                    Destroy(ggo.GetComponent<Collider>());
                    go = new GameObject();
                    ggo.transform.parent = go.transform;
                    ggo.transform.localPosition = Vector3.zero;
                    ggo.transform.localRotation = Quaternion.identity;
                    ggo.transform.localScale = new Vector3(css.Radius * 2f,css.HalfHeight * 2f,css.Radius * 2f);
                    BulletRigidBodyProxy rbp = go.AddComponent<BulletRigidBodyProxy>();
                    rbp.target = co;
                } else { 
                    //Debug.Log("Creating " + cs.ShapeType + " for " + co.ToString());
                    go = CreateUnityCollisionObjectProxy(co as CollisionObject);
                }
            }
            createdObjs.Add(go);
            //Debug.Log("Created Unity Shape for shapeType=" + co.CollisionShape.ShapeType + " collisionShape=" + co.ToString());
        }
    }

    void Update() {
        if (demo.Input != null) {
            demo.Input.KeysReleased.Clear();
            demo.Input.KeysReleased.AddRange(demo.Input.KeysDown);
            demo.Input.KeysPressed.Clear();
            demo.Input.KeysDown.Clear();
            //demo.Input.ClearKeyCache();
            for (int i = 0; i < BulletSharpExamples.Input.UnityKeys.Length; i++) {
                KeyCode k = BulletSharpExamples.Input.UnityKeys[i];
                if (UnityEngine.Input.GetKey(k)) {
                    demo.Input.KeysDown.Add(BulletSharpExamples.Input.BSKeys[i]);
                }
                if (UnityEngine.Input.GetKeyDown(k)) {
                    demo.Input.KeysPressed.Add(BulletSharpExamples.Input.BSKeys[i]);
                }
            }
            for (int i = 0; i < demo.Input.KeysPressed.Count; i++)
            {
                demo.Input.KeysReleased.Remove(demo.Input.KeysPressed[i]);
            }
            for (int i = 0; i < demo.Input.KeysDown.Count; i++)
            {
                demo.Input.KeysReleased.Remove(demo.Input.KeysDown[i]);
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


    public GameObject CreateUnityCollisionObjectProxy(CollisionObject body) {
        if (body is GhostObject)
        {
            Debug.Log("ghost obj");
        }
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
            if (f.Nodes.Count != 3) {
                Debug.LogError("Face was not a triangle");
                continue;
            }
            for (int j = 0; j < f.Nodes.Count; j++) { 
                tris.Add( node2vertIdx[f.Nodes[j]]);
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
            cube.transform.position = BSExtensionMethods2.ExtractTranslationFromMatrix(ref m);
            cube.transform.rotation = BSExtensionMethods2.ExtractRotationFromMatrix(ref m);
            cube.transform.localScale = BSExtensionMethods2.ExtractScaleFromMatrix(ref m);
            Destroy(cube.GetComponent<BulletRigidBodyProxy>());
            BulletMultiBodyLinkColliderProxy cp = cube.AddComponent<BulletMultiBodyLinkColliderProxy>();
            cp.target = body;

        } else {
            Debug.LogError("Not implemented");
        }

    }
}
