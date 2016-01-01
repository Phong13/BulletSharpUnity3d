using UnityEngine;
using System.Collections;
using BulletSharp.SoftBody;
using System;
using BulletSharp;
using System.Collections.Generic;
//using BulletSharp.SoftBody;

namespace BulletUnity
{

    /// <summary>
    /// Used base for any(most) softbodies needing a mesh and meshrenderer.
    /// </summary>
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    public class BSoftBodyWMesh : BSoftBody
    {
      
        private MeshFilter _meshFilter;
        protected MeshFilter meshFilter
        {
            get { return _meshFilter = _meshFilter ?? GetComponent<MeshFilter>(); }
        }
   
        public override bool BuildSoftBody()
        {
            Mesh mesh = meshFilter.sharedMesh;

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

            //Set SB settings
            SoftBodySettings.ConfigureSoftBody(m_BSoftBody);         

            //Set SB position to GO position
            m_BSoftBody.Translate(transform.position.ToBullet());
            m_BSoftBody.Scale(transform.localScale.ToBullet());

            //UpdateMesh();

            return true;
        }

       
        /// <summary>
        /// Update Mesh (or line renderer) at runtime, call from Update 
        /// </summary>
        public override void UpdateMesh()
        {
            Mesh mesh = meshFilter.sharedMesh;
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
            meshFilter.sharedMesh = mesh;
            transform.SetTransformationFromBulletMatrix(m_BSoftBody.WorldTransform);  //Set SoftBody position, No motionstate

        }



    }
}