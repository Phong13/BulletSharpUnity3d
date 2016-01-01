using UnityEngine;
using System.Collections;
using BulletSharp.SoftBody;
using System;
//using BulletSharp;
using System.Collections.Generic;

namespace BulletUnity
{
    [RequireComponent(typeof(LineRenderer))]
    public class BSoftBodyRope : BSoftBody
    {
        [Serializable]
        public class RopeSettings
        {
            public int numPointsInRope = 10;
            public Vector3 startPoint;
            public Vector3 endPoint;

            public float width = .25f;
            public Color color = Color.white;

        }

        int lrVertexCount = 0;

        public RopeSettings meshSettings = new RopeSettings();

        LineRenderer _lr;
        LineRenderer lr
        {
            get { return _lr = _lr ?? GetComponent<LineRenderer>(); }
        }

        public override bool BuildSoftBody()
        {
            if (meshSettings.numPointsInRope < 2)
            {
                Debug.LogError("There must be at least two points in the rope");
                return false;
            }
            if (SoftBodySettings.totalMass <= 0f)
            {
                Debug.LogError("The rope must have a positive mass");
                return false;
            }

            m_BSoftBody = SoftBodyHelpers.CreateRope(World.WorldInfo,
                meshSettings.startPoint.ToBullet(), meshSettings.endPoint.ToBullet(), meshSettings.numPointsInRope, 0);

            //TODO: lr, Doesnt always work in editor
            GetComponent<LineRenderer>().useWorldSpace = false;

            verts = new Vector3[m_BSoftBody.Nodes.Count];
            norms = new Vector3[m_BSoftBody.Nodes.Count];

            for (int i = 0; i < m_BSoftBody.Nodes.Count; i++)
            {
                verts[i] = m_BSoftBody.Nodes[i].Position.ToUnity();
                norms[i] = m_BSoftBody.Nodes[i].Normal.ToUnity();
            }

            //Set SB settings
            SoftBodySettings.ConfigureSoftBody(m_BSoftBody);

            //Set SB position to GO position
            m_BSoftBody.Translate(transform.position.ToBullet());
            m_BSoftBody.Scale(transform.localScale.ToBullet());

            UpdateMesh();
            return true;
        }

        /// <summary>
        /// Create new SoftBody object
        /// </summary>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        /// <returns></returns>
        public static GameObject CreateNew(Vector3 position, Quaternion rotation)
        {
            GameObject go = new GameObject();
            go.transform.position = position;
            go.transform.rotation = rotation;
            BSoftBodyRope bRope = go.AddComponent<BSoftBodyRope>();

            UnityEngine.Material material = new UnityEngine.Material(Shader.Find("Particles/Additive"));
            bRope.lr.sharedMaterial = material;

            bRope.SoftBodySettings.ResetToSoftBodyPresets(SBSettingsPresets.Rope);
            bRope.BuildSoftBody();
            go.name = "BSoftBodyRope";
            return go;
        }

        /// <summary>
        /// Update Rope line renderer at runtime, called from Update 
        /// </summary>
        public override void UpdateMesh()
        {
            if (lr == null)
            {
                return;
            }
            if (lr.enabled == false)
            {
                lr.enabled = true;
            }

            if (lrVertexCount != verts.Length)
            {
                lrVertexCount = verts.Length;
                lr.SetVertexCount(lrVertexCount);
                lr.SetWidth(meshSettings.width, meshSettings.width);
                lr.SetColors(meshSettings.color, meshSettings.color);
            }
            for (int i = 0; i < verts.Length; i++)
            {
                lr.SetPosition(i, verts[i]);
            }

            transform.SetTransformationFromBulletMatrix(m_BSoftBody.WorldTransform);  //Set SoftBody position, No motionstate

        }


        public void BuildMesh()
        {



        }

    }
}