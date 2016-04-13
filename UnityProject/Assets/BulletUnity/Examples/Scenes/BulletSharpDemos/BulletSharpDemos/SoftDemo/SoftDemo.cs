using BulletSharp;
using BulletSharp.Math;
using BulletSharp.SoftBody;
using DemoFramework;
using System;
using System.Linq;
using CollisionFlags = BulletSharp.SoftBody.CollisionFlags;
using BulletSharpExamples;

namespace SoftDemo
{
    class ImplicitSphere : ImplicitFn
    {
        Vector3 _center;
        float _sqRadius;

        public ImplicitSphere(ref Vector3 center, float radius)
        {
            _center = center;
            _sqRadius = radius * radius;
        }

        public override float Eval(ref Vector3 x)
        {
            return (x - _center).LengthSquared - _sqRadius;
        }
    };

    class MotorControl : AngularJoint.IControl
    {
        float goal = 0;
        float maxTorque = 0;

        public float Goal
        {
            get { return goal; }
            set { goal = value; }
        }

        public float MaxTorque
        {
            get { return maxTorque; }
            set { maxTorque = value; }
        }

        public override float Speed(AngularJoint joint, float current)
        {
            return current + Math.Min(maxTorque, Math.Max(-maxTorque, goal - current));
        }
    }

    class SteerControl : AngularJoint.IControl
    {
        float sign;
        MotorControl _motorControl;

        public float Angle { get; set; }

        public SteerControl(float sign, MotorControl motorControl)
        {
            this.sign = sign;
            _motorControl = motorControl;
        }

        public override void Prepare(AngularJoint joint)
        {
            joint.Refs[0] = new Vector3((float)Math.Cos(Angle * sign), 0, (float)Math.Sin(Angle * sign));
        }

        public override float Speed(AngularJoint joint, float current)
        {
            return _motorControl.Speed(joint, current);
        }
    }

    class SoftDemo : Demo
    {
        Vector3 eye = new Vector3(20, 20, 80);
        Vector3 target = new Vector3(0, 0, 10);

        Point lastMousePos;
        Vector3 impact;
        SRayCast results = new SRayCast();
        Node node;
        Vector3 goal;
        bool drag;

        public int demo = 27;

        SoftBodyWorldInfo softBodyWorldInfo;

        public bool cutting;
        const int maxProxies = 32766;

        static MotorControl motorControl = new MotorControl();
        static SteerControl steerControlF = new SteerControl(1, motorControl);
        static SteerControl steerControlR = new SteerControl(-1, motorControl);

        SoftRigidDynamicsWorld SoftWorld
        {
            get { return World as SoftRigidDynamicsWorld; }
        }

        public delegate void DemoConstructor();

        public DemoConstructor[] demos;

        public SoftDemo()
        {
            demos = new DemoConstructor[] { Init_Cloth, Init_Pressure, Init_Volume, Init_Ropes, Init_RopeAttach,
                Init_ClothAttach, Init_Sticks, Init_CapsuleCollision, Init_Collide, Init_Collide2, Init_Collide3, Init_Impact, Init_Aero,
                Init_Aero2, Init_Friction, Init_Torus, Init_TorusMatch, Init_Bunny, Init_BunnyMatch, Init_Cutting1,
                Init_ClusterDeform, Init_ClusterCollide1, Init_ClusterCollide2, Init_ClusterSocket, Init_ClusterHinge,
                Init_ClusterCombine, Init_ClusterCar, Init_ClusterRobot, Init_ClusterStackSoft, Init_ClusterStackMixed,
                Init_TetraCube, Init_TetraBunny, Init_Bending
            };
        }

        protected override void OnInitialize()
        {
            Freelook.SetEyeTarget(eye, target);

            Graphics.SetFormText("BulletSharp - SoftBody Demo");
        }

        void NextDemo()
        {
            demo++;
            if (demo >= demos.Length)
                demo = 0;

            ClientResetScene();
        }

        void PreviousDemo()
        {
            demo--;
            if (demo < 0)
                demo = demos.Length - 1;

            ClientResetScene();
        }

        void InitializeDemo()
        {
            motorControl.Goal = 0;
            motorControl.MaxTorque = 0;

            CollisionShape groundShape = new BoxShape(50, 50, 50);
            CollisionShapes.Add(groundShape);
            RigidBody body = LocalCreateRigidBody(0, Matrix.Translation(0, -62, 0), groundShape);
            body.UserObject = "Ground";

            softBodyWorldInfo.SparseSdf.Reset();

            softBodyWorldInfo.AirDensity = 1.2f;
            softBodyWorldInfo.WaterDensity = 0;
            softBodyWorldInfo.WaterOffset = 0;
            softBodyWorldInfo.WaterNormal = Vector3.Zero;
            softBodyWorldInfo.Gravity = new Vector3(0, -10, 0);

            Graphics.CullingEnabled = true;
            demos[demo]();
        }

        static Vector3 GetRandomVector(Random random)
        {
            return new Vector3((float)random.NextDouble(),
                    (float)random.NextDouble(), (float)random.NextDouble());
        }

        SoftBody Create_SoftBox(Vector3 p, Vector3 s)
        {
            Vector3 h = s * 0.5f;
            Vector3[] c = new Vector3[]{
                h * new Vector3(-1,-1,-1),
                h * new Vector3(+1,-1,-1),
                h * new Vector3(-1,+1,-1),
                h * new Vector3(+1,+1,-1),
                h * new Vector3(-1,-1,+1),
                h * new Vector3(+1,-1,+1),
                h * new Vector3(-1,+1,+1),
                h * new Vector3(+1,+1,+1)};
            SoftBody psb = SoftBodyHelpers.CreateFromConvexHull(softBodyWorldInfo, c);
            psb.GenerateBendingConstraints(2);
            psb.Translate(p);
            SoftWorld.AddSoftBody(psb);

            return psb;
        }

        SoftBody Create_SoftBoulder(Vector3 p, Vector3 s, int np)
        {
            Random random = new Random();
            Vector3[] pts = new Vector3[np];
            for (int i = 0; i < np; ++i)
                pts[i] = GetRandomVector(random) * s;

            SoftBody psb = SoftBodyHelpers.CreateFromConvexHull(softBodyWorldInfo, pts);
            psb.GenerateBendingConstraints(2);
            psb.Translate(p);
            SoftWorld.AddSoftBody(psb);

            return psb;
        }

        void Create_RbUpStack(int count)
        {
            const float mass = 10.0f;

            CompoundShape cylinderCompound = new CompoundShape();
            CollisionShape cylinderShape = new CylinderShapeX(4, 1, 1);
            CollisionShape boxShape = new BoxShape(4, 1, 1);
            cylinderCompound.AddChildShape(Matrix.Identity, boxShape);
            Quaternion orn = Quaternion.RotationYawPitchRoll((float)Math.PI / 2.0f, 0.0f, 0.0f);
            Matrix localTransform = Matrix.RotationQuaternion(orn);
            //localTransform *= Matrix.Translation(new Vector3(1,1,1));
            cylinderCompound.AddChildShape(localTransform, cylinderShape);

            CollisionShape[] shape = new CollisionShape[]{cylinderCompound,
                new BoxShape(new Vector3(1,1,1)),
                new SphereShape(1.5f)};

            for (int i = 0; i < count; ++i)
                LocalCreateRigidBody(mass, Matrix.Translation(0, 2 + 6 * i, 0), shape[i % shape.Length]);
        }

        void Create_BigBall(Vector3 position)
        {
            LocalCreateRigidBody(10.0f, Matrix.Translation(position), new SphereShape(1.5f));
        }

        RigidBody Create_BigPlate(float mass, float height)
        {
            RigidBody body = LocalCreateRigidBody(mass, Matrix.Translation(0, height, 0.5f), new BoxShape(5, 1, 5));
            body.Friction = 1;
            return body;
        }

        RigidBody Create_BigPlate()
        {
            return Create_BigPlate(15, 4);
        }

        void Init_Pressure()
        {
            SoftBody psb = SoftBodyHelpers.CreateEllipsoid(softBodyWorldInfo, new Vector3(35, 25, 0),
                new Vector3(3, 3, 3), 512);
            psb.Materials[0].LinearStiffness = 0.1f;
            psb.Cfg.DynamicFriction = 1;
            psb.Cfg.Damping = 0.001f; // fun factor...
            psb.Cfg.Pressure = 2500;
            psb.SetTotalMass(30, true);
            SoftWorld.AddSoftBody(psb);

            Create_BigPlate();
            Create_LinearStair(10, Vector3.Zero, new Vector3(2, 1, 5));
        }

        void Init_Ropes()
        {
			UnityEngine.Debug.Log("");
            const int n = 15;
            for (int i = 0; i < n; i++)
            {
                SoftBody psb = SoftBodyHelpers.CreateRope(softBodyWorldInfo,
                    new Vector3(-10, 0, i * 0.25f),
                    new Vector3(10, 0, i * 0.25f), 16, 1 + 2);
                psb.Cfg.PositionIterations = 4;
                psb.Materials[0].LinearStiffness = 0.1f + (i / (float)(n - 1)) * 0.9f;
                psb.TotalMass = 20;
                SoftWorld.AddSoftBody(psb);
            }
        }

        SoftBody Create_Rope(Vector3 p)
        {
            SoftBody psb = SoftBodyHelpers.CreateRope(softBodyWorldInfo, p, p + new Vector3(10, 0, 0), 8, 1);
            psb.TotalMass = 50;
            SoftWorld.AddSoftBody(psb);
            return psb;
        }

        void Init_RopeAttach()
        {
            softBodyWorldInfo.SparseSdf.RemoveReferences(null);
            RigidBody body = LocalCreateRigidBody(50, Matrix.Translation(12, 8, 0), new BoxShape(2, 6, 2));
            SoftBody psb0 = Create_Rope(new Vector3(0, 8, -1));
            SoftBody psb1 = Create_Rope(new Vector3(0, 8, +1));
            psb0.AppendAnchor(psb0.Nodes.Count - 1, body);
            psb1.AppendAnchor(psb1.Nodes.Count - 1, body);
        }

        void Init_ClothAttach()
        {
            const float s = 4;
            const float h = 6;
            int r = 9;
            SoftBody psb = SoftBodyHelpers.CreatePatch(softBodyWorldInfo, new Vector3(-s, h, -s),
                new Vector3(+s, h, -s),
                new Vector3(-s, h, +s),
                new Vector3(+s, h, +s), r, r, 4 + 8, true);
            SoftWorld.AddSoftBody(psb);

            RigidBody body = LocalCreateRigidBody(20, Matrix.Translation(0, h, -(s + 3.5f)), new BoxShape(s, 1, 3));
            psb.AppendAnchor(0, body);
            psb.AppendAnchor(r - 1, body);
            body.UserObject = "LargeBox";
            cutting = true;

            Graphics.CullingEnabled = false;
        }

        void Create_LinearStair(int count, Vector3 origin, Vector3 sizes)
        {
            BoxShape shape = new BoxShape(sizes);
            for (int i = 0; i < count; i++)
            {
                RigidBody body = LocalCreateRigidBody(0,
                    Matrix.Translation(origin + new Vector3(sizes.X * 2 * i, sizes.Y * 2 * i, 0)), shape);
                body.Friction = 1;
            }
        }

        void Init_Impact()
        {
            SoftBody psb = SoftBodyHelpers.CreateRope(softBodyWorldInfo,
                Vector3.Zero, new Vector3(0, -1, 0), 0, 1);
            SoftWorld.AddSoftBody(psb);
            psb.Cfg.RigidContactHardness = 0.5f;
            LocalCreateRigidBody(10, Matrix.Translation(0, 20, 0), new BoxShape(2));
        }

        void Init_CapsuleCollision()
        {
            float s = 4;
            float h = 6;
            int r = 20;

            Matrix startTransform = Matrix.Translation(0, h - 2, 0);

            CollisionShape capsuleShape = new CapsuleShapeX(1, 5);
            capsuleShape.Margin = 0.5f;

            //capsuleShape.LocalScaling = new Vector3(5, 1, 1);
            //RigidBody body = LocalCreateRigidBody(20, startTransform, capsuleShape);
            RigidBody body = LocalCreateRigidBody(0, startTransform, capsuleShape);
            body.Friction = 0.8f;

            const int fixeds = 0; //4+8;
            SoftBody psb = SoftBodyHelpers.CreatePatch(softBodyWorldInfo, new Vector3(-s, h, -s),
                new Vector3(+s, h, -s),
                new Vector3(-s, h, +s),
                new Vector3(+s, h, +s), r, r, fixeds, true);
            SoftWorld.AddSoftBody(psb);
            psb.TotalMass = 0.1f;

            psb.Cfg.PositionIterations = 10;
            psb.Cfg.ClusterIterations = 10;
            psb.Cfg.DriftIterations = 10;
            //psb.Cfg.VelocityIterations = 10;


            //psb.AppendAnchor(0, body);
            //psb.AppendAnchor(r-1, body);
            // cutting=true;

            Graphics.CullingEnabled = false;
        }

        void Init_Collide()
        {
            for (int i = 0; i < 3; ++i)
            {
                SoftBody psb = SoftBodyHelpers.CreateFromTriMesh(softBodyWorldInfo, TorusMesh.Vertices, TorusMesh.Indices);
                psb.GenerateBendingConstraints(2);
                psb.Cfg.PositionIterations = 2;
                psb.Cfg.Collisions |= CollisionFlags.VertexFaceSoftSoft;
                psb.RandomizeConstraints();
                Matrix m = Matrix.RotationYawPitchRoll((float)Math.PI / 2 * (i & 1), (float)Math.PI / 2 * (1 - (i & 1)), 0) *
                    Matrix.Translation(3 * i, 2, 0);
                psb.Transform(m);
                psb.Scale(new Vector3(2, 2, 2));
                psb.SetTotalMass(50, true);
                SoftWorld.AddSoftBody(psb);
            }
            cutting = true;
        }

        void Init_Collide2()
        {
            for (int i = 0; i < 3; ++i)
            {
                SoftBody psb = SoftBodyHelpers.CreateFromTriMesh(softBodyWorldInfo, BunnyMesh.Vertices, BunnyMesh.Indices);
                Material pm = psb.AppendMaterial();
                pm.LinearStiffness = 0.5f;
                pm.Flags -= MaterialFlags.DebugDraw;
                psb.GenerateBendingConstraints(2, pm);
                psb.Cfg.PositionIterations = 2;
                psb.Cfg.DynamicFriction = 0.5f;
                psb.Cfg.Collisions |= CollisionFlags.VertexFaceSoftSoft;
                psb.RandomizeConstraints();
                Matrix m = Matrix.RotationYawPitchRoll((float)Math.PI / 2 * (i & 1), 0, 0) *
                    Matrix.Translation(0, -1 + 5 * i, 0);
                psb.Transform(m);
                psb.Scale(new Vector3(6, 6, 6));
                psb.SetTotalMass(100, true);
                SoftWorld.AddSoftBody(psb);
            }
            cutting = true;
        }

        void Init_Collide3()
        {
            float s = 8;
            SoftBody psb = SoftBodyHelpers.CreatePatch(softBodyWorldInfo, new Vector3(-s, 0, -s),
                new Vector3(+s, 0, -s),
                new Vector3(-s, 0, +s),
                new Vector3(+s, 0, +s),
                15, 15, 1 + 2 + 4 + 8, true);
            psb.Materials[0].LinearStiffness = 0.4f;
            psb.Cfg.Collisions |= CollisionFlags.VertexFaceSoftSoft;
            psb.TotalMass = 150;
            SoftWorld.AddSoftBody(psb);

            s = 4;
            Vector3 o = new Vector3(5, 10, 0);
            psb = SoftBodyHelpers.CreatePatch(softBodyWorldInfo,
                new Vector3(-s, 0, -s) + o,
                new Vector3(+s, 0, -s) + o,
                new Vector3(-s, 0, +s) + o,
                new Vector3(+s, 0, +s) + o,
                7, 7, 0, true);
            Material pm = psb.AppendMaterial();
            pm.LinearStiffness = 0.1f;
            pm.Flags -= MaterialFlags.DebugDraw;
            psb.GenerateBendingConstraints(2, pm);
            psb.Materials[0].LinearStiffness = 0.5f;
            psb.Cfg.Collisions |= CollisionFlags.VertexFaceSoftSoft;
            psb.TotalMass = 150;
            SoftWorld.AddSoftBody(psb);
            cutting = true;

            Graphics.CullingEnabled = false;
        }

        // Aerodynamic forces, 50x1g flyers
        void Init_Aero()
        {
            const float s = 2;
            const int segments = 6;
            const int count = 50;
            Random random = new Random();
            for (int i = 0; i < count; ++i)
            {
                SoftBody psb = SoftBodyHelpers.CreatePatch(softBodyWorldInfo,
                    new Vector3(-s, 0, -s), new Vector3(+s, 0, -s),
                    new Vector3(-s, 0, +s), new Vector3(+s, 0, +s),
                    segments, segments, 0, true);
                Material pm = psb.AppendMaterial();
                pm.Flags -= MaterialFlags.DebugDraw;
                psb.GenerateBendingConstraints(2, pm);
                psb.Cfg.Lift = 0.004f;
                psb.Cfg.Drag = 0.0003f;
                psb.Cfg.AeroModel = AeroModel.VertexTwoSided;
                Matrix trans = Matrix.Identity;
                Vector3 ra = 0.1f * GetRandomVector(random);
                Vector3 rp = 75 * GetRandomVector(random) + new Vector3(-50, 15, 0);
                Quaternion rot = Quaternion.RotationYawPitchRoll(
                    (float)Math.PI / 8 + ra.X, (float)-Math.PI / 7 + ra.Y, ra.Z);
                trans *= Matrix.RotationQuaternion(rot);
                trans *= Matrix.Translation(rp);
                psb.Transform(trans);
                psb.TotalMass = 0.1f;
                psb.AddForce(new Vector3(0, (float)random.NextDouble(), 0), 0);
                SoftWorld.AddSoftBody(psb);
            }

            Graphics.CullingEnabled = false;
        }

        void Init_Aero2()
        {
            const float s = 5;
            const int segments = 10;
            const int count = 5;
            Vector3 pos = new Vector3(-s * segments, 0, 0);
            float gap = 0.5f;

            for (int i = 0; i < count; ++i)
            {
                SoftBody psb = SoftBodyHelpers.CreatePatch(softBodyWorldInfo, new Vector3(-s, 0, -s * 3),
                    new Vector3(+s, 0, -s * 3),
                    new Vector3(-s, 0, +s),
                    new Vector3(+s, 0, +s),
                    segments, segments * 3,
                    1 + 2, true);

                psb.CollisionShape.Margin = 0.5f;
                Material pm = psb.AppendMaterial();
                pm.LinearStiffness = 0.0004f;
                pm.Flags -= MaterialFlags.DebugDraw;
                psb.GenerateBendingConstraints(2, pm);

                psb.Cfg.Lift = 0.05f;
                psb.Cfg.Drag = 0.01f;

                //psb.Cfg.LF = 0.004f;
                //psb.Cfg.DG = 0.0003f;

                psb.Cfg.PositionIterations = 2;
                psb.Cfg.AeroModel = AeroModel.VertexTwoSidedLiftDrag;


                psb.WindVelocity = new Vector3(4, -12.0f, -25.0f);

                pos += new Vector3(s * 2 + gap, 0, 0);
                Matrix trs = Matrix.RotationX((float)Math.PI / 2) * Matrix.Translation(pos);
                psb.Transform(trs);
                psb.TotalMass = 2.0f;

                SoftBodyHelpers.ReoptimizeLinkOrder(psb);

                SoftWorld.AddSoftBody(psb);
            }

            Graphics.CullingEnabled = false;
        }

        void Init_Friction()
        {
            const float bs = 2;
            const float ts = bs + bs / 4;
            for (int i = 0, ni = 20; i < ni; ++i)
            {
                Vector3 p = new Vector3(-ni * ts / 2 + i * ts, bs, 40);
                SoftBody psb = Create_SoftBox(p, new Vector3(bs, bs, bs));
                psb.Cfg.DynamicFriction = 0.1f * ((i + 1) / (float)ni);
                psb.AddVelocity(new Vector3(0, 0, -10));
            }
        }

        void Init_TetraBunny()
        {
            SoftBody psb = SoftBodyHelpers.CreateFromTetGenData(softBodyWorldInfo,
                Bunny.GetElements(), null, Bunny.GetNodes(), false, true, true);
            SoftWorld.AddSoftBody(psb);
            psb.Rotate(Quaternion.RotationYawPitchRoll((float)Math.PI / 2, 0, 0));
            psb.SetVolumeMass(150);
            psb.Cfg.PositionIterations = 2;
            //psb.Cfg.PIterations = 1;
            cutting = false;
            //psb.CollisionShape.Margin = 0.01f;
            psb.Cfg.Collisions = CollisionFlags.ClusterClusterSoftSoft | CollisionFlags.ClusterConvexRigidSoft; //| CollisionFlags.ClusterSelf;

            ///pass zero in generateClusters to create  cluster for each tetrahedron or triangle
            psb.GenerateClusters(0);
            //psb.Materials[0].Lst = 0.2f;
            psb.Cfg.DynamicFriction = 10;

            Graphics.CullingEnabled = false;
        }

        void Init_TetraCube()
        {
            UnityEngine.TextAsset ele = (UnityEngine.TextAsset) UnityEngine.Resources.Load("Cube.ele");  //(elementFilename != null) ? File.ReadAllText(elementFilename) : null;
            UnityEngine.TextAsset face = (UnityEngine.TextAsset) UnityEngine.Resources.Load("Cube.ply"); // (faceFilename != null) ? File.ReadAllText(faceFilename) : null;
            UnityEngine.TextAsset node = (UnityEngine.TextAsset) UnityEngine.Resources.Load("Cube.node");  // File.ReadAllText(nodeFilename)
            SoftBody psb = SoftBodyHelpers.CreateFromTetGenData(softBodyWorldInfo, ele.text, face.text, node.text, false, true, true);

            SoftWorld.AddSoftBody(psb);
            psb.Scale(new Vector3(4, 4, 4));
            psb.Translate(0, 5, 0);
            psb.SetVolumeMass(300);

            // fix one vertex
            //psb.SetMass(0,0);
            //psb.SetMass(10,0);
            //psb.SetMass(20,0);
            psb.Cfg.PositionIterations = 1;
            //psb.GenerateClusters(128);
            psb.GenerateClusters(16);
            //psb.CollisionShape.Margin = 0.5f;

            psb.CollisionShape.Margin = 0.01f;
            psb.Cfg.Collisions = CollisionFlags.ClusterClusterSoftSoft | CollisionFlags.ClusterConvexRigidSoft;
            // | Collision.ClusterSelf;
            psb.Materials[0].LinearStiffness = 0.8f;
            cutting = false;

            Graphics.CullingEnabled = false;
        }

        void Init_Volume()
        {
            SoftBody psb = SoftBodyHelpers.CreateEllipsoid(softBodyWorldInfo, new Vector3(35, 25, 0),
                new Vector3(1, 1, 1) * 3, 512);
            psb.Materials[0].LinearStiffness = 0.45f;
            psb.Cfg.VolumeConversation = 20;
            psb.SetTotalMass(50, true);
            psb.SetPose(true, false);
            SoftWorld.AddSoftBody(psb);

            Create_BigPlate();
            Create_LinearStair(10, Vector3.Zero, new Vector3(2, 1, 5));
        }

        void Init_Sticks()
        {
            const int n = 16;
            const int sg = 4;
            const float sz = 5;
            const float hg = 4;
            const float inf = 1 / (float)(n - 1);
            for (int y = 0; y < n; ++y)
            {
                for (int x = 0; x < n; ++x)
                {
                    Vector3 org = new Vector3(-sz + sz * 2 * x * inf,
                        -10, -sz + sz * 2 * y * inf);

                    SoftBody psb = SoftBodyHelpers.CreateRope(softBodyWorldInfo, org,
                        org + new Vector3(hg * 0.001f, hg, 0), sg, 1);

                    psb.Cfg.Damping = 0.005f;
                    psb.Cfg.RigidContactHardness = 0.1f;
                    for (int i = 0; i < 3; ++i)
                    {
                        psb.GenerateBendingConstraints(2 + i);
                    }
                    psb.SetMass(1, 0);
                    psb.TotalMass = 0.01f;
                    SoftWorld.AddSoftBody(psb);
                }
            }
            Create_BigBall(new Vector3(0, 13, 0));
        }

        void Init_Bending()
        {
            const float s = 4;
            Vector3[] x = new Vector3[]{new Vector3(-s,0,-s),
                new Vector3(+s,0,-s),
                new Vector3(+s,0,+s),
                new Vector3(-s,0,+s)};
            float[] m = new float[] { 0, 0, 0, 1 };
            SoftBody psb = new SoftBody(softBodyWorldInfo, 4, x, m);
            psb.AppendLink(0, 1);
            psb.AppendLink(1, 2);
            psb.AppendLink(2, 3);
            psb.AppendLink(3, 0);
            psb.AppendLink(0, 2);

            SoftWorld.AddSoftBody(psb);
        }

        void Init_Cloth()
        {
            float s = 8;
            SoftBody psb = SoftBodyHelpers.CreatePatch(softBodyWorldInfo, new Vector3(-s, 0, -s),
                new Vector3(+s, 0, -s),
                new Vector3(-s, 0, +s),
                new Vector3(+s, 0, +s),
                31, 31,
                1 + 2 + 4 + 8, true);

            psb.CollisionShape.Margin = 0.5f;
            Material pm = psb.AppendMaterial();
            pm.LinearStiffness = 0.4f;
            pm.Flags -= MaterialFlags.DebugDraw;
            psb.GenerateBendingConstraints(2, pm);
            psb.TotalMass = 150;
            SoftWorld.AddSoftBody(psb);

            Create_RbUpStack(10);
            cutting = true;

            Graphics.CullingEnabled = false;
        }

        void Init_Bunny()
        {
            SoftBody psb = SoftBodyHelpers.CreateFromTriMesh(softBodyWorldInfo, BunnyMesh.Vertices, BunnyMesh.Indices);
            Material pm = psb.AppendMaterial();
            pm.LinearStiffness = 0.5f;
            pm.Flags -= MaterialFlags.DebugDraw;
            psb.GenerateBendingConstraints(2, pm);
            psb.Cfg.PositionIterations = 2;
            psb.Cfg.DynamicFriction = 0.5f;
            psb.RandomizeConstraints();
            Matrix m = Matrix.RotationYawPitchRoll(0, (float)Math.PI / 2, 0) *
                Matrix.Translation(0, 4, 0);
            psb.Transform(m);
            psb.Scale(new Vector3(6, 6, 6));
            psb.SetTotalMass(100, true);
            SoftWorld.AddSoftBody(psb);
            cutting = true;
        }

        void Init_BunnyMatch()
        {
            SoftBody psb = SoftBodyHelpers.CreateFromTriMesh(softBodyWorldInfo, BunnyMesh.Vertices, BunnyMesh.Indices);
            psb.Cfg.DynamicFriction = 0.5f;
            psb.Cfg.PoseMatching = 0.05f;
            psb.Cfg.PositionIterations = 5;
            psb.RandomizeConstraints();
            psb.Scale(new Vector3(6, 6, 6));
            psb.SetTotalMass(100, true);
            psb.SetPose(false, true);
            SoftWorld.AddSoftBody(psb);
        }

        void Init_Torus()
        {
            SoftBody psb = SoftBodyHelpers.CreateFromTriMesh(softBodyWorldInfo, TorusMesh.Vertices, TorusMesh.Indices);
            psb.GenerateBendingConstraints(2);
            psb.Cfg.PositionIterations = 2;
            psb.RandomizeConstraints();
            Matrix m = Matrix.RotationYawPitchRoll(0, (float)Math.PI / 2, 0) *
                Matrix.Translation(0, 4, 0);
            psb.Transform(m);
            psb.Scale(new Vector3(2, 2, 2));
            psb.SetTotalMass(50, true);
            SoftWorld.AddSoftBody(psb);
            cutting = true;
        }

        void Init_TorusMatch()
        {
            SoftBody psb = SoftBodyHelpers.CreateFromTriMesh(softBodyWorldInfo, TorusMesh.Vertices, TorusMesh.Indices);
            psb.Materials[0].LinearStiffness = 0.1f;
            psb.Cfg.PoseMatching = 0.05f;
            psb.RandomizeConstraints();
            Matrix m = Matrix.RotationYawPitchRoll(0, (float)Math.PI / 2, 0) *
                Matrix.Translation(0, 4, 0);
            psb.Transform(m);
            psb.Scale(new Vector3(2, 2, 2));
            psb.SetTotalMass(50, true);
            psb.SetPose(false, true);
            SoftWorld.AddSoftBody(psb);
        }

        void Init_Cutting1()
        {
            const float s = 6;
            const float h = 2;
            const int r = 16;
            Vector3[] p = new Vector3[]{new Vector3(+s,h,-s),
                new Vector3(-s,h,-s),
                new Vector3(+s,h,+s),
                new Vector3(-s,h,+s)};
            SoftBody psb = SoftBodyHelpers.CreatePatch(softBodyWorldInfo, p[0], p[1], p[2], p[3], r, r, 1 + 2 + 4 + 8, true);
            SoftWorld.AddSoftBody(psb);
            psb.Cfg.PositionIterations = 1;
            cutting = true;

            Graphics.CullingEnabled = false;
        }

        void CreateGear(Vector3 pos, float speed)
        {
            Matrix startTransform = Matrix.Translation(pos);
            CompoundShape shape = new CompoundShape();
#if true
            shape.AddChildShape(Matrix.Identity, new BoxShape(5, 1, 6));
            shape.AddChildShape(Matrix.RotationZ((float)Math.PI), new BoxShape(5, 1, 6));
#else
            shape.AddChildShape(Matrix.Identity, new CylinderShapeZ(5,1,7));
            shape.AddChildShape(Matrix.RotationZ((float)Math.PI), new BoxShape(4,1,8));
#endif
            RigidBody body = LocalCreateRigidBody(10, startTransform, shape);
            body.Friction = 1;
            HingeConstraint hinge = new HingeConstraint(body, Matrix.Identity);
            if (speed != 0) hinge.EnableAngularMotor(true, speed, 3);
            World.AddConstraint(hinge);
        }

        SoftBody Create_ClusterBunny(Vector3 x, Vector3 a)
        {
            SoftBody psb = SoftBodyHelpers.CreateFromTriMesh(softBodyWorldInfo, BunnyMesh.Vertices, BunnyMesh.Indices);
            Material pm = psb.AppendMaterial();
            pm.LinearStiffness = 1;
            pm.Flags -= MaterialFlags.DebugDraw;
            psb.GenerateBendingConstraints(2, pm);
            psb.Cfg.PositionIterations = 2;
            psb.Cfg.DynamicFriction = 1;
            psb.Cfg.Collisions = CollisionFlags.ClusterClusterSoftSoft | CollisionFlags.ClusterConvexRigidSoft;
            psb.RandomizeConstraints();
            Matrix m = Matrix.RotationYawPitchRoll(a.X, a.Y, a.Z) * Matrix.Translation(x);
            psb.Transform(m);
            psb.Scale(new Vector3(8, 8, 8));
            psb.SetTotalMass(150, true);
            psb.GenerateClusters(1);
            SoftWorld.AddSoftBody(psb);
            return psb;
        }

        SoftBody Create_ClusterTorus(Vector3 x, Vector3 a, Vector3 s)
        {
            SoftBody psb = SoftBodyHelpers.CreateFromTriMesh(softBodyWorldInfo, TorusMesh.Vertices, TorusMesh.Indices);
            Material pm = psb.AppendMaterial();
            pm.LinearStiffness = 1;
            pm.Flags -= MaterialFlags.DebugDraw;
            psb.GenerateBendingConstraints(2, pm);
            psb.Cfg.PositionIterations = 2;
            psb.Cfg.Collisions = CollisionFlags.ClusterClusterSoftSoft | CollisionFlags.ClusterConvexRigidSoft;
            psb.RandomizeConstraints();
            psb.Scale(s);
            Matrix m = Matrix.RotationYawPitchRoll(a.X, a.Y, a.Z) * Matrix.Translation(x);
            psb.Transform(m);
            psb.SetTotalMass(50, true);
            psb.GenerateClusters(64);
            SoftWorld.AddSoftBody(psb);
            return psb;
        }

        SoftBody Create_ClusterTorus(Vector3 x, Vector3 a)
        {
            return Create_ClusterTorus(x, a, new Vector3(2));
        }

        void Init_ClusterDeform()
        {
            SoftBody psb = Create_ClusterTorus(Vector3.Zero, new Vector3((float)Math.PI / 2, 0, (float)Math.PI / 2));
            psb.GenerateClusters(8);
            psb.Cfg.DynamicFriction = 1;
        }

        void Init_ClusterCollide1()
        {
            const float s = 8;
            SoftBody psb = SoftBodyHelpers.CreatePatch(softBodyWorldInfo, new Vector3(-s, 0, -s),
                new Vector3(+s, 0, -s),
                new Vector3(-s, 0, +s),
                new Vector3(+s, 0, +s),
                17, 17,//9,9,//31,31,
                1 + 2 + 4 + 8,
                true);
            Material pm = psb.AppendMaterial();
            pm.LinearStiffness = 0.4f;
            pm.Flags -= MaterialFlags.DebugDraw;
            psb.Cfg.DynamicFriction = 1;
            psb.Cfg.SoftRigidHardness = 1;
            psb.Cfg.SoftRigidImpulseSplit = 0;
            psb.Cfg.Collisions = CollisionFlags.ClusterClusterSoftSoft | CollisionFlags.ClusterConvexRigidSoft;
            psb.GenerateBendingConstraints(2, pm);

            psb.CollisionShape.Margin = 0.05f;
            psb.TotalMass = 50;

            // pass zero in generateClusters to create  cluster for each tetrahedron or triangle
            psb.GenerateClusters(0);
            //psb.GenerateClusters(64);

            SoftWorld.AddSoftBody(psb);

            Create_RbUpStack(10);

            Graphics.CullingEnabled = false;
        }

        void Init_ClusterCollide2()
        {
            for (int i = 0; i < 3; ++i)
            {
                SoftBody psb = SoftBodyHelpers.CreateFromTriMesh(softBodyWorldInfo, TorusMesh.Vertices, TorusMesh.Indices);
                Material pm = psb.AppendMaterial();
                pm.Flags -= MaterialFlags.DebugDraw;
                psb.GenerateBendingConstraints(2, pm);
                psb.Cfg.PositionIterations = 2;
                psb.Cfg.DynamicFriction = 1;
                psb.Cfg.SoftSoftHardness = 1;
                psb.Cfg.SoftSoftImpulseSplit = 0;
                psb.Cfg.SoftKineticHardness = 0.1f;
                psb.Cfg.SoftKineticImpulseSplit = 1;
                psb.Cfg.Collisions = CollisionFlags.ClusterClusterSoftSoft | CollisionFlags.ClusterConvexRigidSoft;
                psb.RandomizeConstraints();
                Matrix m = Matrix.RotationYawPitchRoll((float)Math.PI / 2 * (i & 1), (float)Math.PI / 2 * (1 - (i & 1)), 0)
                    * Matrix.Translation(3 * i, 2, 0);
                psb.Transform(m);
                psb.Scale(new Vector3(2, 2, 2));
                psb.SetTotalMass(50, true);
                psb.GenerateClusters(16);
                SoftWorld.AddSoftBody(psb);
            }
        }

        void Init_ClusterSocket()
        {
            SoftBody psb = Create_ClusterTorus(Vector3.Zero, new Vector3((float)Math.PI / 2, 0, (float)Math.PI / 2));
            RigidBody prb = Create_BigPlate(50, 8);
            psb.Cfg.DynamicFriction = 1;
            LinearJoint.Specs lj = new LinearJoint.Specs();
            lj.Position = new Vector3(0, 5, 0);
            psb.AppendLinearJoint(lj, new Body(prb));
        }

        void Init_ClusterHinge()
        {
            SoftBody psb = Create_ClusterTorus(Vector3.Zero, new Vector3((float)Math.PI / 2, 0, (float)Math.PI / 2));
            RigidBody prb = Create_BigPlate(50, 8);
            psb.Cfg.DynamicFriction = 1;
            AngularJoint.Specs aj = new AngularJoint.Specs();
            aj.Axis = new Vector3(0, 0, 1);
            psb.AppendAngularJoint(aj, new Body(prb));
        }

        void Init_ClusterCombine()
        {
            Vector3 sz = new Vector3(2, 4, 2);
            SoftBody psb0 = Create_ClusterTorus(new Vector3(0, 8, 0), new Vector3((float)Math.PI / 2, 0, (float)Math.PI / 2), sz);
            SoftBody psb1 = Create_ClusterTorus(new Vector3(0, 8, 10), new Vector3((float)Math.PI / 2, 0, (float)Math.PI / 2), sz);
            SoftBody[] psbs = new SoftBody[] { psb0, psb1 };
            for (int j = 0; j < 2; ++j)
            {
                psbs[j].Cfg.DynamicFriction = 1;
                psbs[j].Cfg.DynamicFriction = 0;
                psbs[j].Cfg.PositionIterations = 1;
                psbs[j].Clusters[0].Matching = 0.05f;
                psbs[j].Clusters[0].NodeDamping = 0.05f;
            }
            AngularJoint.Specs aj = new AngularJoint.Specs();
            aj.Axis = new Vector3(0, 0, 1);
            aj.Control = motorControl;
            psb0.AppendAngularJoint(aj, psb1);

            LinearJoint.Specs lj = new LinearJoint.Specs();
            lj.Position = new Vector3(0, 8, 5);
            psb0.AppendLinearJoint(lj, psb1);
        }

        void Init_ClusterCar()
        {
            //SetAzi(180);
            Vector3 origin = new Vector3(100, 80, 0);
            Quaternion orientation = Quaternion.RotationYawPitchRoll(-(float)Math.PI / 2, 0, 0);
            const float widthf = 8;
            const float widthr = 9;
            const float length = 8;
            const float height = 4;
            Vector3[] wheels = {
                new Vector3(+widthf,-height,+length), // Front left
                new Vector3(-widthf,-height,+length), // Front right
                new Vector3(+widthr,-height,-length), // Rear left
                new Vector3(-widthr,-height,-length), // Rear right
            };
            SoftBody pa = Create_ClusterBunny(Vector3.Zero, Vector3.Zero);
            SoftBody pfl = Create_ClusterTorus(wheels[0], new Vector3(0, 0, (float)Math.PI / 2), new Vector3(2, 4, 2));
            SoftBody pfr = Create_ClusterTorus(wheels[1], new Vector3(0, 0, (float)Math.PI / 2), new Vector3(2, 4, 2));
            SoftBody prl = Create_ClusterTorus(wheels[2], new Vector3(0, 0, (float)Math.PI / 2), new Vector3(2, 5, 2));
            SoftBody prr = Create_ClusterTorus(wheels[3], new Vector3(0, 0, (float)Math.PI / 2), new Vector3(2, 5, 2));

            pfl.Cfg.DynamicFriction =
                pfr.Cfg.DynamicFriction =
                prl.Cfg.DynamicFriction =
                prr.Cfg.DynamicFriction = 1;

            LinearJoint.Specs lspecs = new LinearJoint.Specs();
            lspecs.ConstraintForceMixing = 1;
            lspecs.ErrorReductionParameter = 1;
            lspecs.Position = Vector3.Zero;

            lspecs.Position = wheels[0]; pa.AppendLinearJoint(lspecs, pfl);
            lspecs.Position = wheels[1]; pa.AppendLinearJoint(lspecs, pfr);
            lspecs.Position = wheels[2]; pa.AppendLinearJoint(lspecs, prl);
            lspecs.Position = wheels[3]; pa.AppendLinearJoint(lspecs, prr);

            AngularJoint.Specs aspecs = new AngularJoint.Specs();
            aspecs.ConstraintForceMixing = 1;
            aspecs.ErrorReductionParameter = 1;
            aspecs.Axis = new Vector3(1, 0, 0);

            aspecs.Control = steerControlF;
            pa.AppendAngularJoint(aspecs, pfl);
            pa.AppendAngularJoint(aspecs, pfr);

            aspecs.Control = motorControl;
            pa.AppendAngularJoint(aspecs, prl);
            pa.AppendAngularJoint(aspecs, prr);

            pa.Rotate(orientation);
            pfl.Rotate(orientation);
            pfr.Rotate(orientation);
            prl.Rotate(orientation);
            prr.Rotate(orientation);
            pa.Translate(origin);
            pfl.Translate(origin);
            pfr.Translate(origin);
            prl.Translate(origin);
            prr.Translate(origin);
            pfl.Cfg.PositionIterations =
                pfr.Cfg.PositionIterations =
                prl.Cfg.PositionIterations =
                prr.Cfg.PositionIterations = 1;
            pfl.Clusters[0].Matching =
                pfr.Clusters[0].Matching =
                prl.Clusters[0].Matching =
                prr.Clusters[0].Matching = 0.05f;
            pfl.Clusters[0].NodeDamping =
                pfr.Clusters[0].NodeDamping =
                prl.Clusters[0].NodeDamping =
                prr.Clusters[0].NodeDamping = 0.05f;

            Create_LinearStair(20, new Vector3(0, -8, 0), new Vector3(3, 2, 40));
            Create_RbUpStack(50);
            //autocam=true;
        }

        SoftBody Init_ClusterRobot_CreateBall(Vector3 pos)
        {
            SoftBody psb = SoftBodyHelpers.CreateEllipsoid(softBodyWorldInfo, pos, new Vector3(1, 1, 1) * 3, 512);
            psb.Materials[0].LinearStiffness = 0.45f;
            psb.Cfg.VolumeConversation = 20;
            psb.SetTotalMass(50, true);
            psb.SetPose(true, false);
            psb.GenerateClusters(1);
            SoftWorld.AddSoftBody(psb);
            return psb;
        }

        void Init_ClusterRobot()
        {
            Vector3 basePos = new Vector3(0, 25, 8);
            SoftBody psb0 = Init_ClusterRobot_CreateBall(basePos + new Vector3(-8, 0, 0));
            SoftBody psb1 = Init_ClusterRobot_CreateBall(basePos + new Vector3(+8, 0, 0));
            SoftBody psb2 = Init_ClusterRobot_CreateBall(basePos + new Vector3(0, 0, +8 * (float)Math.Sqrt(2)));
            Vector3 ctr = (psb0.ClusterCom(0) + psb1.ClusterCom(0) + psb2.ClusterCom(0)) / 3;
            CylinderShape pshp = new CylinderShape(new Vector3(8, 1, 8));
            RigidBody prb = LocalCreateRigidBody(50, Matrix.Translation(ctr + new Vector3(0, 5, 0)), pshp);
            LinearJoint.Specs ls = new LinearJoint.Specs();
            ls.ErrorReductionParameter = 0.5f;
            Body prbBody = new Body(prb);
            ls.Position = psb0.ClusterCom(0); psb0.AppendLinearJoint(ls, prbBody);
            ls.Position = psb1.ClusterCom(0); psb1.AppendLinearJoint(ls, prbBody);
            ls.Position = psb2.ClusterCom(0); psb2.AppendLinearJoint(ls, prbBody);

            BoxShape pbox = new BoxShape(20, 1, 40);
            RigidBody pgrn = LocalCreateRigidBody(0, Matrix.RotationZ(-(float)Math.PI / 4), pbox);
        }

        void Init_ClusterStackSoft()
        {
            for (int i = 0; i < 10; ++i)
            {
                SoftBody psb = Create_ClusterTorus(new Vector3(0, -9 + 8.25f * i, 0), Vector3.Zero);
                psb.Cfg.DynamicFriction = 1;
            }
        }

        //
        void Init_ClusterStackMixed()
        {
            for (int i = 0; i < 10; ++i)
            {
                if (((i + 1) & 1) == 1)
                {
                    Create_BigPlate(50, -9 + 4.25f * i);
                }
                else
                {
                    SoftBody psb = Create_ClusterTorus(new Vector3(0, -9 + 4.25f * i, 0), Vector3.Zero);
                    psb.Cfg.DynamicFriction = 1;
                }
            }
        }

        protected override void OnInitializePhysics()
        {
            // collision configuration contains default setup for memory, collision setup
            CollisionConf = new SoftBodyRigidBodyCollisionConfiguration();
            Dispatcher = new CollisionDispatcher(CollisionConf);

            Broadphase = new AxisSweep3(new Vector3(-1000, -1000, -1000),
                new Vector3(1000, 1000, 1000), maxProxies);

            // the default constraint solver.
            Solver = new SequentialImpulseConstraintSolver();

            softBodyWorldInfo = new SoftBodyWorldInfo
            {
                AirDensity = 1.2f,
                WaterDensity = 0,
                WaterOffset = 0,
                WaterNormal = Vector3.Zero,
                Gravity = new Vector3(0, -10, 0),
                Dispatcher = Dispatcher,
                Broadphase = Broadphase
            };
            softBodyWorldInfo.SparseSdf.Initialize();

            World = new SoftRigidDynamicsWorld(Dispatcher, Broadphase, Solver, CollisionConf);
            World.Gravity = new Vector3(0, -10, 0);
            World.DispatchInfo.EnableSpu = true;

            World.SetInternalTickCallback(PickingPreTickCallback, this, true);

            InitializeDemo();
        }

        public override void OnUpdate()
        {
            softBodyWorldInfo.SparseSdf.GarbageCollect();
            base.OnUpdate();
        }

        void PickingPreTickCallback(DynamicsWorld world, float timeStep)
        {
            if (!drag) return;

            Vector3 rayFrom = Freelook.Eye;
            Vector3 rayTo = GetRayTo(lastMousePos, Freelook.Eye, Freelook.Target, Graphics.FieldOfView);
            Vector3 rayDir = rayTo - rayFrom;
            rayDir.Normalize();
            Vector3 N = Freelook.Target - rayFrom;
            N.Normalize();
            float O = Vector3.Dot(impact, N);
            float den = Vector3.Dot(N, rayDir);
            if ((den * den) > 0)
            {
                float num = O - Vector3.Dot(N, rayFrom);
                float hit = num / den;
                if (hit > 0 && hit < 1500)
                {
                    goal = rayFrom + rayDir * hit;
                }
            }
            Vector3 delta = goal - node.Position;
            float maxDrag = 10;
            if (delta.LengthSquared > (maxDrag * maxDrag))
            {
                delta.Normalize();
                delta *= maxDrag;
            }
            node.Velocity += delta / timeStep;
        }

        public override void OnHandleInput()
        {
            if (Input.KeysPressed.Contains(Keys.B))
            {
                PreviousDemo();
            }
            else if (Input.KeysPressed.Contains(Keys.N))
            {
                NextDemo();
            }

            if (Input.KeysDown.Count != 0)
            {
                if (demos[demo] == Init_ClusterCombine || demos[demo] == Init_ClusterCar)
                {
                    if (Input.KeysDown.Contains(Keys.Up))
                    {
                        motorControl.MaxTorque = 1;
                        motorControl.Goal += FrameDelta * 2;
                    }
                    else if (Input.KeysDown.Contains(Keys.Down))
                    {
                        motorControl.MaxTorque = 1;
                        motorControl.Goal -= FrameDelta * 2;
                    }
                    else if (Input.KeysDown.Contains(Keys.Left))
                    {
                        steerControlF.Angle += FrameDelta;
                        steerControlR.Angle += FrameDelta;
                    }
                    else if (Input.KeysDown.Contains(Keys.Right))
                    {
                        steerControlF.Angle -= FrameDelta;
                        steerControlR.Angle -= FrameDelta;
                    }
                }
            }

            if (Input.MousePressed == MouseButtons.Right)
            {
                results.Fraction = 1;
                if (pickConstraint == null)
                {
                    Vector3 rayFrom = Freelook.Eye;
                    Vector3 rayTo = GetRayTo(Input.MousePoint, Freelook.Eye, Freelook.Target, Graphics.FieldOfView);
                    Vector3 rayDir = rayTo - rayFrom;
                    rayDir.Normalize();

                    SRayCast res = new SRayCast();
                    if (SoftWorld.SoftBodyArray.Any(b => b.RayTest(ref rayFrom, ref rayTo, res)))
                    {
                        results = res;
                        impact = rayFrom + (rayTo - rayFrom) * results.Fraction;
                        drag = !cutting;
                        lastMousePos = Input.MousePoint;

                        NodePtrArray nodes;
                        switch (results.Feature)
                        {
                            case FeatureType.Face:
                                nodes = results.Body.Faces[results.Index].Nodes;
                                break;
                            case FeatureType.Tetra:
                                nodes = results.Body.Tetras[results.Index].Nodes;
                                break;
                            default:
                                nodes = null;
                                break;
                        }
                        if (nodes != null)
                        {
                            node = nodes.Aggregate((min, n) =>
                                (n.Position - impact).LengthSquared <
                                (min.Position - impact).LengthSquared ? n : min
                            );
                            goal = node.Position;
                        }
                        else
                        {
                            node = null;
                        }
                    }
                }
            }
            else if (Input.MouseReleased == MouseButtons.Right)
            {
                if (!drag && cutting && results.Fraction < 1)
                {
                    using (var isphere = new ImplicitSphere(ref impact, 1))
                    {
                        results.Body.Refine(isphere, 0.0001f, true);
                    }
                }
                results.Fraction = 1;
                drag = false;
            }

            // Mouse movement
            if (Input.MouseDown == MouseButtons.Right)
            {
                if (node != null && results.Fraction < 1)
                {
                    if (!drag)
                    {
                        int x = Input.MousePoint.X - lastMousePos.X;
                        int y = Input.MousePoint.Y - lastMousePos.Y;
                        if ((x * x) + (y * y) > 6)
                        {
                            drag = true;
                        }
                    }
                    if (drag)
                    {
                        lastMousePos = Input.MousePoint;
                    }
                }
            }

            base.OnHandleInput();
        }
    }

    static class Program
    {
        [STAThread]
        static void Main()
        {
            using (Demo demo = new SoftDemo())
            {
                GraphicsLibraryManager.Run(demo);
            }
        }
    }
}
