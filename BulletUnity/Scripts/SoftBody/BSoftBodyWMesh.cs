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
    //[RequireComponent(typeof(MeshFilter))]
    //[RequireComponent(typeof(MeshRenderer))]
    public class BSoftBodyWMesh : BSoftBody
    {
        public BUserMeshSettings meshSettings = new BUserMeshSettings();

        private MeshFilter _meshFilter;
        protected MeshFilter meshFilter
        {
            get { return _meshFilter = _meshFilter ?? GetComponent<MeshFilter>(); }
        }

        internal override bool _BuildCollisionObject()
        {

            Mesh mesh = meshSettings.Build();
            if (mesh == null)
            {
                Debug.LogError("Could not build mesh from meshSettings for " + this);
                return false;
            }

            GetComponent<MeshFilter>().sharedMesh = mesh;

            if (World == null)
            {
                return false;
            }
            //convert the mesh data to Bullet data and create SoftBody
            BulletSharp.Math.Vector3[] bVerts = new BulletSharp.Math.Vector3[mesh.vertexCount];
            Vector3[] verts = mesh.vertices;
            for (int i = 0; i < mesh.vertexCount; i++)
            {
                bVerts[i] = verts[i].ToBullet();
            }

            SoftBody m_BSoftBody = SoftBodyHelpers.CreateFromTriMesh(World.WorldInfo, bVerts, mesh.triangles);
            m_collisionObject = m_BSoftBody;
            SoftBodySettings.ConfigureSoftBody(m_BSoftBody);         //Set SB settings

            //Set SB position to GO position
            m_BSoftBody.Rotate(transform.rotation.ToBullet());
            m_BSoftBody.Translate(transform.position.ToBullet());
            m_BSoftBody.Scale(transform.localScale.ToBullet());

            return true;
        }

        /// <summary>
        /// Create new SoftBody object using a Mesh
        /// </summary>
        /// <param name="position">World position</param>
        /// <param name="rotation">rotation</param>
        /// <param name="mesh">Need to provide a mesh</param>
        /// <param name="buildNow">Build now or configure properties and call BuildSoftBody() after</param>
        /// <param name="sBpresetSelect">Use a particular softBody configuration pre select values</param>
        /// <returns></returns>
        public static GameObject CreateNew(Vector3 position, Quaternion rotation, Mesh mesh, bool buildNow, SBSettingsPresets sBpresetSelect)
        {
            GameObject go = new GameObject("SoftBodyWMesh");
            go.transform.position = position;
            go.transform.rotation = rotation;
            BSoftBodyWMesh BSoft = go.AddComponent<BSoftBodyWMesh>();
            MeshFilter meshFilter = go.AddComponent<MeshFilter>();
            MeshRenderer meshRenderer = go.AddComponent<MeshRenderer>();
            BSoft.meshSettings.UserMesh = mesh;
            UnityEngine.Material material = new UnityEngine.Material(Shader.Find("Standard"));
            meshRenderer.material = material;

            BSoft.SoftBodySettings.ResetToSoftBodyPresets(sBpresetSelect); //Apply SoftBody settings presets

            if (buildNow)
            {
                BSoft._BuildCollisionObject();  //Build the SoftBody
            }
            go.name = "BSoftBodyWMesh";
            return go;
        }

        /// <summary>
        /// Update Mesh (or line renderer) at runtime, call from Update 
        /// </summary>
        public override void UpdateMesh()
        {
            Mesh mesh = meshFilter.sharedMesh;
            if (verts != null && verts.Length > 0)
            {
                mesh.vertices = verts;
                mesh.normals = norms;
                mesh.RecalculateBounds();
                transform.SetTransformationFromBulletMatrix(m_collisionObject.WorldTransform);  //Set SoftBody position, No motionstate    
            }
        }
    }
}