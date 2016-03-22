using System;
using System.IO;
using System.Runtime.InteropServices;
using BulletSharp;
using BulletSharp.Math;
using DemoFramework;
using BulletSharpExamples;

namespace ConcaveRaycastDemo
{
    // Scrolls back and forth over terrain
    class RaycastBar
    {
        const int NUMRAYS_IN_BAR = 100;

        Vector3[] source = new Vector3[NUMRAYS_IN_BAR];
        Vector3[] dest = new Vector3[NUMRAYS_IN_BAR];
        Vector3[] direction = new Vector3[NUMRAYS_IN_BAR];
        Vector3[] hit = new Vector3[NUMRAYS_IN_BAR];
        Vector3[] normal = new Vector3[NUMRAYS_IN_BAR];

        int frame_counter;
        int ms;
        int sum_ms;
        int sum_ms_samples;
        int min_ms;
        int max_ms;

        float dx;
        float min_x;
        float max_x;
        float min_y;
        float max_y;
        float sign;

        public RaycastBar()
        {
            ms = 0;
            max_ms = 0;
            min_ms = 9999;
            sum_ms_samples = 0;
            sum_ms = 0;
        }

        public RaycastBar(bool unused, float ray_length, float min_z, float max_z, float min_y = -10, float max_y = 10)
        {
            frame_counter = 0;
            ms = 0;
            max_ms = 0;
            min_ms = 9999;
            sum_ms_samples = 0;
            sum_ms = 0;
            dx = 10.0f;
            min_x = -40;
            max_x = 20;
            this.min_y = min_y;
            this.max_y = max_y;
            sign = 1.0f;
            for (int i = 0; i < NUMRAYS_IN_BAR; i++)
            {
                float z = (max_z - min_z) / NUMRAYS_IN_BAR * (float)i + min_z;
                source[i] = new Vector3(min_x, this.max_y, z);
                dest[i] = new Vector3(min_x + ray_length, this.min_y, z);
                normal[i] = new Vector3(1.0f, 0.0f, 0.0f);
            }
        }

        public RaycastBar(float ray_length, float z, float min_y = -1000, float max_y = 10)
        {
            frame_counter = 0;
            ms = 0;
            max_ms = 0;
            min_ms = 9999;
            sum_ms_samples = 0;
            sum_ms = 0;
            dx = 10.0f;
            min_x = -40;
            max_x = 20;
            this.min_y = min_y;
            this.max_y = max_y;
            sign = 1.0f;
            float dalpha = 4 * (float)Math.PI / NUMRAYS_IN_BAR;
            for (int i = 0; i < NUMRAYS_IN_BAR; i++)
            {
                float alpha = dalpha * i;
                // rotate around by alpha degrees y
                Matrix tr = Matrix.RotationQuaternion(Quaternion.RotationAxis(new Vector3(0.0f, 1.0f, 0.0f), alpha));
                direction[i] = new Vector3(1.0f, 0.0f, 0.0f);
                direction[i] = Vector3.TransformCoordinate(direction[i], tr);
                direction[i] = direction[i] * ray_length;
                source[i] = new Vector3(min_x, max_y, z);
                dest[i] = source[i] + direction[i];
                dest[i][1] = min_y;
                normal[i] = new Vector3(1.0f, 0.0f, 0.0f);
            }
        }

        public void Move(float dt)
        {
            if (dt > (1.0f / 60.0f))
                dt = 1.0f / 60.0f;
            for (int i = 0; i < NUMRAYS_IN_BAR; i++)
            {
                source[i][0] += dx * dt * sign;
                dest[i][0] += dx * dt * sign;
            }
            if (source[0][0] < min_x)
                sign = 1.0f;
            else if (source[0][0] > max_x)
                sign = -1.0f;
        }

        public void Cast(CollisionWorld cw)
        {
#if BATCH_RAYCASTER
		    if (!gBatchRaycaster)
			    return;

		    gBatchRaycaster->clearRays ();
		    for (int i = 0; i < NUMRAYS_IN_BAR; i++)
		    {
			    gBatchRaycaster->addRay (source[i], dest[i]);
		    }
		    gBatchRaycaster->performBatchRaycast ();
		    for (int i = 0; i < gBatchRaycaster->getNumRays (); i++)
		    {
				    const SpuRaycastTaskWorkUnitOut& out = (*gBatchRaycaster)[i];
				    hit[i].setInterpolate3(source[i],dest[i],out.HitFraction);
				    normal[i] = out.hitNormal;
				    normal[i].Normalize();
		    }
#else

            for (int i = 0; i < NUMRAYS_IN_BAR; i++)
            {
                using (var cb = new ClosestRayResultCallback(ref source[i], ref dest[i]))
                {
                    cw.RayTestRef(ref source[i], ref dest[i], cb);
                    if (cb.HasHit)
                    {
                        hit[i] = cb.HitPointWorld;
                        normal[i] = cb.HitNormalWorld;
                        normal[i].Normalize();
                    }
                    else
                    {
                        hit[i] = dest[i];
                        normal[i] = new Vector3(1.0f, 0.0f, 0.0f);
                    }
                }
            }

            frame_counter++;
            if (frame_counter > 50)
            {
                min_ms = ms < min_ms ? ms : min_ms;
                max_ms = ms > max_ms ? ms : max_ms;
                sum_ms += ms;
                sum_ms_samples++;
                float mean_ms = (float)sum_ms / (float)sum_ms_samples;
                Console.WriteLine("{0} rays in {1} ms {2} {3} {4}", NUMRAYS_IN_BAR * frame_counter, ms, min_ms, max_ms, mean_ms);
                ms = 0;
                frame_counter = 0;
            }
#endif
        }

        static Vector3 green = new Vector3(0.0f, 1.0f, 0.0f);
        static Vector3 white = new Vector3(1.0f, 1.0f, 1.0f);
        //static Vector3 cyan = new Vector3(0.0f, 1.0f, 1.0f);

        public void Draw(IDebugDraw drawer)
        {
            int i;
            for (i = 0; i < NUMRAYS_IN_BAR; i++)
            {
                drawer.DrawLine(ref source[i], ref hit[i], ref green);
            }
            for (i = 0; i < NUMRAYS_IN_BAR; i++)
            {
                Vector3 to = hit[i] + normal[i];
                drawer.DrawLine(ref hit[i], ref to, ref white);
            }
        }
    }

    class ConcaveRaycastDemo : Demo
    {
        Vector3 eye = new Vector3(0, 15, 60);
        Vector3 target = new Vector3(-5, 5, 0);

        const DebugDrawModes debugMode = DebugDrawModes.None;

        const float TriangleSize = 8.0f;
        const int NumVertsX = 30;
        const int NumVertsY = 30;
        const float waveHeight = 5.0f;
        static float offset = 0.0f;
        bool animatedMesh = false;

        TriangleIndexVertexArray indexVertexArrays;
        BvhTriangleMeshShape groundShape;
        static RaycastBar raycastBar;
        RigidBody staticBody;

        protected override void OnInitialize()
        {
            Freelook.SetEyeTarget(eye, target);

            Graphics.SetFormText("BulletSharp - Concave Raycast Demo");
            Graphics.SetInfoText("Move using mouse and WASD+shift\n" +
                "F3 - Toggle debug\n" +
                //"F11 - Toggle fullscreen\n" +
                "Space - Shoot box");

            IsDebugDrawEnabled = false;
            DebugDrawMode = debugMode;
        }

        void SetVertexPositions(float waveheight, float offset)
        {
            var vertexStream = indexVertexArrays.GetVertexStream();
            using (var vertexWriter = new BinaryWriter(vertexStream))
            {
                for (int i = 0; i < NumVertsX; i++)
                {
                    for (int j = 0; j < NumVertsY; j++)
                    {
                        vertexWriter.Write((i - NumVertsX * 0.5f) * TriangleSize);
                        vertexWriter.Write(waveheight * (float)Math.Sin((float)i + offset) * (float)Math.Cos((float)j + offset));
                        vertexWriter.Write((j - NumVertsY * 0.5f) * TriangleSize);
                    }
                }
            }
        }

        protected override void OnInitializePhysics()
        {
            // collision configuration contains default setup for memory, collision setup
            CollisionConf = new DefaultCollisionConfiguration();
            Dispatcher = new CollisionDispatcher(CollisionConf);

            Vector3 worldMin = new Vector3(-1000, -1000, -1000);
            Vector3 worldMax = new Vector3(1000, 1000, 1000);
            Broadphase = new AxisSweep3(worldMin, worldMax);
            Solver = new SequentialImpulseConstraintSolver();

            World = new DiscreteDynamicsWorld(Dispatcher, Broadphase, Solver, CollisionConf);
            World.SolverInfo.SplitImpulse = 1;
            World.Gravity = new Vector3(0, -10, 0);


            const int totalVerts = NumVertsX * NumVertsY;
            const int totalTriangles = 2 * (NumVertsX - 1) * (NumVertsY - 1);
            indexVertexArrays = new TriangleIndexVertexArray();

            IndexedMesh mesh = new IndexedMesh();
            mesh.NumTriangles = totalTriangles;
            mesh.NumVertices = totalVerts;
            mesh.TriangleIndexStride = 3 * sizeof(int);
            mesh.VertexStride = Vector3.SizeInBytes;
            mesh.TriangleIndexBase = Marshal.AllocHGlobal(mesh.TriangleIndexStride * totalTriangles);
            mesh.VertexBase = Marshal.AllocHGlobal(mesh.VertexStride * totalVerts);
            var indicesStream = mesh.GetTriangleStream();
            var indices = new BinaryWriter(indicesStream);
            for (int i = 0; i < NumVertsX - 1; i++)
            {
                for (int j = 0; j < NumVertsY - 1; j++)
                {
                    indices.Write(j * NumVertsX + i);
                    indices.Write(j * NumVertsX + i + 1);
                    indices.Write((j + 1) * NumVertsX + i + 1);

                    indices.Write(j * NumVertsX + i);
                    indices.Write((j + 1) * NumVertsX + i + 1);
                    indices.Write((j + 1) * NumVertsX + i);
                }
            }
            indices.Dispose();

            indexVertexArrays.AddIndexedMesh(mesh);

            raycastBar = new RaycastBar(4000.0f, 0.0f);
            //raycastBar = new RaycastBar(true, 40.0f, -50.0f, 50.0f);


            CollisionShape colShape = new BoxShape(1);
            CollisionShapes.Add(colShape);

            for (int i = 0; i < 10; i++)
            {
                //CollisionShape colShape = new CapsuleShape(0.5f,2.0f);//boxShape = new SphereShape(1.0f);
                Matrix startTransform = Matrix.Translation(2 * i, 10, 1);
                LocalCreateRigidBody(1.0f, startTransform, colShape);
            }


            SetVertexPositions(waveHeight, 0.0f);

            const bool useQuantizedAabbCompression = true;
            groundShape = new BvhTriangleMeshShape(indexVertexArrays, useQuantizedAabbCompression);
            CollisionShapes.Add(groundShape);

            staticBody = LocalCreateRigidBody(0.0f, Matrix.Identity, groundShape);
            staticBody.CollisionFlags |= CollisionFlags.StaticObject;
            staticBody.UserObject = "Ground";
        }

        public override void OnUpdate()
        {
            if (animatedMesh)
            {
                offset += FrameDelta;
                SetVertexPositions(waveHeight, offset);
                //Graphics.MeshFactory.RemoveShape(groundShape);

                Vector3 worldMin = new Vector3(-1000, -1000, -1000);
                Vector3 worldMax = new Vector3(1000, 1000, 1000);

                groundShape.RefitTree(ref worldMin, ref worldMax);

                //clear all contact points involving mesh proxy. Note: this is a slow/unoptimized operation.
                Broadphase.OverlappingPairCache.CleanProxyFromPairs(staticBody.BroadphaseHandle, Dispatcher);
            }

            raycastBar.Move(FrameDelta);
            raycastBar.Cast(World);
            if (IsDebugDrawEnabled)
            {
                //raycastBar.Draw(World.DebugDrawer);
            }

            base.OnUpdate();
        }

        public override void OnHandleInput()
        {
            if (Input.KeysPressed.Contains(Keys.G))
            {
                animatedMesh = !animatedMesh;
                if (animatedMesh)
                {
                    staticBody.CollisionFlags |= CollisionFlags.KinematicObject;
                    staticBody.ActivationState = ActivationState.DisableDeactivation;
                }
                else
                {
                    staticBody.CollisionFlags &= ~CollisionFlags.KinematicObject;
                    staticBody.ActivationState = ActivationState.ActiveTag;
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
            using (Demo demo = new ConcaveRaycastDemo())
            {
                GraphicsLibraryManager.Run(demo);
            }
        }
    }
}
