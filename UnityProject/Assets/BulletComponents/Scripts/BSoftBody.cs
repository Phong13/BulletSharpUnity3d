using UnityEngine;
using System.Collections;
using BulletSharp.SoftBody;
using System;
using BulletSharp;
using System.Collections.Generic;
//using BulletSharp.Math;

namespace BulletUnity
{

    public class BSoftBody : MonoBehaviour  //, IDisposable
    {
        public SoftBody m_BSoftBody;
        public UnityEngine.Vector3[] verts = new UnityEngine.Vector3[1];
        public UnityEngine.Vector3[] norms = new UnityEngine.Vector3[1];
        public int[] tris = new int[1];
        private Mesh mesh;


        //BGameObjectMotionState m_motionState;

        protected bool isInWorld = false;
        SoftRigidDynamicsWorld _world;
        SoftRigidDynamicsWorld World
        {
            get
            {
                if (_world == null)
                    _world = (SoftRigidDynamicsWorld)BSoftBodyWorld.Get().World;
                return _world;
            }
        }

        void Start()
        {
            //mesh = new Mesh();

        }



        void Update()
        {
            //if (verts.Length != m_BSoftBody.Nodes.Count)
            //{
            verts = new Vector3[m_BSoftBody.Nodes.Count]; 
            //}
            //if (norms.Length != m_BSoftBody.Nodes.Count)
            //{
            norms = new Vector3[m_BSoftBody.Nodes.Count];
            //}
            for (int i = 0; i < m_BSoftBody.Nodes.Count; i++)
            {
                verts[i] = m_BSoftBody.Nodes[i].Position.ToUnity();
                norms[i] = m_BSoftBody.Nodes[i].Normal.ToUnity();
            }
       
            mesh.vertices = verts;
            mesh.normals = norms;
            //mesh.triangles = tris;
            mesh.RecalculateBounds();
            GetComponent<MeshFilter>().mesh = mesh;
            transform.SetTransformationFromBulletMatrix(m_BSoftBody.WorldTransform);
        }


        void Awake()
        {
            mesh = GetComponent<MeshFilter>().mesh;

            //m_BSoftBody = new SoftBody(World.WorldInfo);

            _CreateSoftBody();

            if (m_BSoftBody != null)
            {
                World.AddSoftBody(m_BSoftBody);
                isInWorld = true;
            }

        }

        //TODO this should be modified so it is safe to call just before a rigidbody is added to the physics world
        //It should be possible to call multiple times.
        void _CreateSoftBody()
        {
            BulletSharp.Math.Vector3[] bVerts = new BulletSharp.Math.Vector3[mesh.vertexCount];
            ////BulletSharp.Math.Vector3[] triangles = new BulletSharp.Math.Vector3[mesh.triangles.Length];
            ////BulletSharp.Math.Vector3[] norms;

            for (int i = 0; i < mesh.vertexCount; i++)
            {
                bVerts[i] = mesh.vertices[i].ToBullet();
                //triangles[i] = mesh.triangles[i];          
            }

            //BSoftBody bSoftBody = GetComponent<BSoftBody>();

            //m_BSoftBody = SoftBodyHelpers.CreateFromConvexHull(World.WorldInfo, bVerts);
            m_BSoftBody = SoftBodyHelpers.CreateFromTriMesh(World.WorldInfo, bVerts, mesh.triangles);

            SoftBody psb = SoftBodyHelpers.CreateFromTriMesh(World.WorldInfo, SoftDemo.BunnyMesh.Vertices, SoftDemo.BunnyMesh.Indices);

            //m_BSoftBody.Materials[0].Lst = 0.1f;
            //m_BSoftBody.Cfg.DF = 1;
            //m_BSoftBody.Cfg.DP = 0.001f; // fun factor...
            //m_BSoftBody.Cfg.PR = 2500;
            //m_BSoftBody.SetTotalMass(30, true);


            //bunny
            BulletSharp.SoftBody.Material pm = psb.AppendMaterial();
            pm.Lst = 0.5f;
            pm.Flags -= FMaterial.DebugDraw;
            psb.GenerateBendingConstraints(2, pm);
            psb.Cfg.PIterations = 2;
            psb.Cfg.DF = 0.5f;
            psb.RandomizeConstraints();
            BulletSharp.Math.Matrix m = BulletSharp.Math.Matrix.RotationYawPitchRoll(0, (float)Math.PI / 2, 0) *
                BulletSharp.Math.Matrix.Translation(0, 4, 0);
            psb.Transform(m);
            psb.Scale(new BulletSharp.Math.Vector3(6, 6, 6));
            psb.SetTotalMass(100, true);

            //m_BSoftBody.Materials[0].Lst = 0.45f;
            //m_BSoftBody.Cfg.VC = 20;
            //m_BSoftBody.SetTotalMass(1f, true);
            //m_BSoftBody.SetPose(true, false);

            //m_BSoftBody.Cfg.DF = 0.5f;
            //m_BSoftBody.Cfg.MT = 0.05f;
            //m_BSoftBody.Cfg.PIterations = 5;
            //m_BSoftBody.RandomizeConstraints();
            //m_BSoftBody.Scale(new Vector3(1, 1, 1).ToBullet());

            //m_BSoftBody.GenerateBendingConstraints(2);
            m_BSoftBody.Translate(transform.position.ToBullet());

            //build nodes 2 verts map
            Dictionary<BulletSharp.SoftBody.Node, int> node2vertIdx = new Dictionary<BulletSharp.SoftBody.Node, int>();
            for (int i = 0; i < m_BSoftBody.Nodes.Count; i++)
            {
                node2vertIdx.Add(m_BSoftBody.Nodes[i], i);
            }
            List<int> bTris = new List<int>();
            for (int i = 0; i < m_BSoftBody.Faces.Count; i++)
            {
                BulletSharp.SoftBody.Face f = m_BSoftBody.Faces[i];
                if (f.N.Count != 3)
                {
                    Debug.LogError("Face was not a triangle");
                    continue;
                }
                for (int j = 0; j < f.N.Count; j++)
                {
                    bTris.Add(node2vertIdx[f.N[j]]);
                }
            }

            //GameObject go = Instantiate<GameObject>(softBodyPrefab);
            //BulletSoftBodyProxy sbp = go.GetComponent<BulletSoftBodyProxy>();

            List<int> trisRev = new List<int>();
            for (int i = 0; i < bTris.Count; i += 3)
            {
                trisRev.Add(bTris[i]);
                trisRev.Add(bTris[i + 2]);
                trisRev.Add(bTris[i + 1]);
            }
            bTris.AddRange(trisRev);
            //sbp.target = body;
            verts = new Vector3[m_BSoftBody.Nodes.Count];
            tris = bTris.ToArray();

            /*
            int numTriangleIndices = triangles.Length;
            int numTriangles = numTriangleIndices / 3;

            int maxIndex = 0; // triangles.Max() + 1;
            for (int i = 0; i < numTriangleIndices; i++)
            {
                if (triangles[i] > maxIndex)
                {
                    maxIndex = triangles[i];
                }
            }
            maxIndex++;

            //BSoftBody bSoftBody = GetComponent<BSoftBody>();

            //SoftRigidDynamicsWorld world = (SoftRigidDynamicsWorld)BSoftBodyWorld.Get().World;

            //SoftBody softBody = new SoftBody(world.WorldInfo);  //Crash!

            bSoftBody.m_BSoftBody = softBody;

            BitArray chks = new BitArray(maxIndex * maxIndex);
            for (int i = 0; i < numTriangleIndices; i += 3)
            {
                int[] idx = new int[] { triangles[i], triangles[i + 1], triangles[i + 2] };
                for (int j = 2, k = 0; k < 3; j = k++)
                {
                    int chkIndex = maxIndex * idx[k] + idx[j];
                    if (!chks[chkIndex])
                    {
                        chks[chkIndex] = true;
                        chks[maxIndex * idx[j] + idx[k]] = true;
                        softBody.AppendLink(idx[j], idx[k]);
                    }
                }
                softBody.AppendFace(idx[0], idx[1], idx[2]);
            }
            */

            //if (transform.localScale != UnityEngine.Vector3.one)
            //{
            //    Debug.LogError("The local scale on this soft body is not one. Bullet physics does not support scaling on a rigid body world transform. Instead alter the dimensions of the CollisionShape.");
            //}

            ////rigidbody is dynamic if and only if mass is non zero, otherwise static
            //BulletSharp.Math.Vector3 localInertia = BulletSharp.Math.Vector3.Zero;

            //CollisionShape cs = m_collisionShape.GetCollisionShape();

            //if (_type == RBType.dynamic)
            //{
            //    cs.CalculateLocalInertia(_mass, out localInertia);
            //}

            ////using motionstate is recommended, it provides interpolation capabilities, and only synchronizes 'active' objects
            //m_motionState = new BGameObjectMotionState(transform);

            //UnityEngine.Vector3 uv = transform.localScale;

            //RigidBodyConstructionInfo rbInfo;
            //if (_type == RBType.dynamic)
            //{
            //    rbInfo = new RigidBodyConstructionInfo(_mass, m_motionState, cs, localInertia);
            //}
            //else
            //{
            //    rbInfo = new RigidBodyConstructionInfo(0, m_motionState, cs, localInertia);
            //}

            //m_BSoftBody = new SoftBody(World.WorldInfo);
            //if (_type == RBType.kinematic)
            //{
            //    m_BSoftBody.CollisionFlags = m_Brigidbody.CollisionFlags | BulletSharp.CollisionFlags.KinematicObject;
            //    m_BSoftBody.ActivationState = ActivationState.DisableDeactivation;
            //}

            //rbInfo.Dispose();
        }

        //Convert mesh for Bullet
        void ConvertMesh()
        {


        }



        //void OnEnable()
        //{


        //}

        //void OnDisable()
        //{
        //    if (isInWorld)
        //    {
        //        World.RemoveSoftBody(m_BSoftBody);
        //    }
        //    isInWorld = false;
        //}

        //void OnDestroy()
        //{
        //    //Destroy(mesh);
        //    Dispose(false);

        //}


        //public void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}

        //protected virtual void Dispose(bool isdisposing)
        //{
        //    if (isInWorld && isdisposing && m_BSoftBody != null)
        //    {
        //        if (World != null)
        //        {
        //            World.RemoveSoftBody(m_BSoftBody);
        //        }
        //    }
        //    if (m_BSoftBody != null)
        //    {
        //        //if (m_BSoftBody.MotionState != null) m_BSoftBody.MotionState.Dispose();
        //        m_BSoftBody.Dispose();
        //        m_BSoftBody = null;
        //    }
        //    Debug.Log("Destroying SoftBody " + name);
        //}


    }
}