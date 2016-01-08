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
        //[Tooltip("Mesh for this softbody")]
        //[SerializeField]
        //private Mesh _userMesh;
        //public Mesh UserMesh
        //{
        //    get { return _userMesh; }
        //    set { _userMesh = value; }
        //}

        //[Header("Mesh post processing")]
        //public bool autoWeldVertices = false;
        //public float autoWeldThreshold = 0.001f; //TODO
        //[Tooltip("Should use this if autoWeldVertices is selected.")]
        //public bool recalculateNormals = false;
        //public bool addBackFaceTriangles = false;
        //public bool recalculateBounds = true;
        //public bool optimize = true;

        public BConvexHullMeshSettings meshSettings = new BConvexHullMeshSettings();

        private MeshFilter _meshFilter;
        protected MeshFilter meshFilter
        {
            get { return _meshFilter = _meshFilter ?? GetComponent<MeshFilter>(); }
        }

        public override bool BuildSoftBody()
        {
            Mesh mesh = meshSettings.Build();

            GetComponent<MeshFilter>().sharedMesh = mesh;

            //convert the mesh data to Bullet data and create DoftBody
            BulletSharp.Math.Vector3[] bVerts = new BulletSharp.Math.Vector3[mesh.vertexCount];

            for (int i = 0; i < mesh.vertexCount; i++)
            {
                bVerts[i] = mesh.vertices[i].ToBullet();
            }

            m_BSoftBody = SoftBodyHelpers.CreateFromTriMesh(World.WorldInfo, bVerts, mesh.triangles);

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
        public static GameObject CreateNew(Vector3 position, Quaternion rotation, Mesh mesh, bool buildNow, SBSettingsPresets sBpresetSelect = SBSettingsPresets.ShapeMatching)
        {
            GameObject go = new GameObject();
            go.transform.position = position;
            go.transform.rotation = rotation;
            BSoftBodyWMesh BSoft = go.AddComponent<BSoftBodyWMesh>();

            BSoft.meshSettings.UserMesh = mesh;
            MeshRenderer meshRenderer = go.GetComponent<MeshRenderer>();
            UnityEngine.Material material = new UnityEngine.Material(Shader.Find("Standard"));
            meshRenderer.material = material;

            BSoft.SoftBodySettings.ResetToSoftBodyPresets(sBpresetSelect); //Apply SoftBody settings presets

            if (buildNow)
                BSoft.BuildSoftBody();  //Build the SoftBody
            go.name = "BSoftBodyWMesh";
            return go;
        }

        /// <summary>
        /// Update Mesh (or line renderer) at runtime, call from Update 
        /// </summary>
        public override void UpdateMesh()
        {
            Mesh mesh = meshFilter.sharedMesh;
            mesh.vertices = verts;
            mesh.normals = norms;
            mesh.RecalculateBounds();
            transform.SetTransformationFromBulletMatrix(m_BSoftBody.WorldTransform);  //Set SoftBody position, No motionstate    
        }



    }
}