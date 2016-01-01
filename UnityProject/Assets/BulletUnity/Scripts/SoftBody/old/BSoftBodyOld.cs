using UnityEngine;
using System.Collections;
using BulletSharp.SoftBody;
using System;
using BulletSharp;
using System.Collections.Generic;
//using BulletSharp.Math;

namespace BulletUnity
{
    //TODO: Delete Me, For reference only. old version
    public class BSoftBodyOld : MonoBehaviour, IDisposable  //, IDisposable
    {

        public SBSettings softBodySettings = new SBSettings();

        public SoftBody m_BSoftBody;

        private UnityEngine.Vector3[] verts = new UnityEngine.Vector3[1];
        private UnityEngine.Vector3[] norms = new UnityEngine.Vector3[1];
        private int[] tris = new int[1];

        private MeshFilter _meshFilter;
        private MeshFilter meshFilter
        {
            get { return _meshFilter = _meshFilter ?? GetComponent<MeshFilter>(); }
        }

        protected bool isInWorld = false;
        SoftRigidDynamicsWorld _world;
        SoftRigidDynamicsWorld World
        {
            get { return _world = _world ?? (SoftRigidDynamicsWorld)BSoftBodyWorld.Get().World; }
        }

      
        void Update()
        {
            Mesh mesh = meshFilter.mesh;
            //mesh.Clear();

            if (verts.Length != m_BSoftBody.Nodes.Count)
            {
                verts = new Vector3[m_BSoftBody.Nodes.Count];
            }
            if (norms.Length != m_BSoftBody.Nodes.Count)
            {
                norms = new Vector3[m_BSoftBody.Nodes.Count];
            }
            for (int i = 0; i < m_BSoftBody.Nodes.Count; i++)
            {
                verts[i] = m_BSoftBody.Nodes[i].Position.ToUnity();
                norms[i] = m_BSoftBody.Nodes[i].Normal.ToUnity();
            }

            mesh.vertices = verts;
            mesh.normals = norms;
            //mesh.triangles = tris;

            mesh.RecalculateBounds();
            meshFilter.mesh = mesh;
            transform.SetTransformationFromBulletMatrix(m_BSoftBody.WorldTransform);  //Set SoftBody position, No motionstate

        }


        void Awake()
        {
            _CreateSoftBody();

            if (m_BSoftBody != null)
            {
                World.AddSoftBody(m_BSoftBody);
                isInWorld = true;
            }

        }

        void _CreateSoftBody()
        {
            BuildNewSoftBodyFromUnityMesh();

            softBodySettings.ConfigureSoftBody(m_BSoftBody);

            m_BSoftBody.Translate(transform.position.ToBullet());
            m_BSoftBody.Scale(transform.localScale.ToBullet());
        }


        void BuildNewSoftBodyFromUnityMesh()
        {
            Mesh mesh = meshFilter.mesh;

            BulletSharp.Math.Vector3[] bVerts = new BulletSharp.Math.Vector3[mesh.vertexCount];

            for (int i = 0; i < mesh.vertexCount; i++)
            {
                bVerts[i] = mesh.vertices[i].ToBullet();
                //triangles[i] = mesh.triangles[i];          
            }

            m_BSoftBody = SoftBodyHelpers.CreateFromTriMesh(World.WorldInfo, bVerts, mesh.triangles);

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

            List<int> trisRev = new List<int>();
            for (int i = 0; i < bTris.Count; i += 3)
            {
                trisRev.Add(bTris[i]);
                trisRev.Add(bTris[i + 2]);
                trisRev.Add(bTris[i + 1]);
            }
            bTris.AddRange(trisRev);
            verts = new Vector3[m_BSoftBody.Nodes.Count];
            tris = bTris.ToArray();


        }


        void OnEnable()
        {


        }

        void OnDisable()
        {
            if (isInWorld)
            {
                World.RemoveSoftBody(m_BSoftBody);
            }
            isInWorld = false;
        }

        void OnDestroy()
        {
            Dispose(false);

        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isdisposing)
        {
            if (isInWorld && isdisposing && m_BSoftBody != null)
            {
                if (World != null)
                {
                    World.RemoveSoftBody(m_BSoftBody);
                }
            }
            if (m_BSoftBody != null)
            {
                m_BSoftBody.Dispose();
                m_BSoftBody = null;
            }
            Debug.Log("Destroying SoftBody " + name);
        }

    }

}


