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
            [Tooltip("Rope start position in world position")]
            public Vector3 startPoint;
            [Tooltip("Rope end position in world position")]
            public Vector3 endPoint;

            public float width = .25f;
            public Color startColor = Color.white;
            public Color endColor = Color.white;

        }

        public RopeSettings meshSettings = new RopeSettings();

        [Tooltip("Rope anchors, if any")]
        public RopeAnchor[] ropeAnchors;

        int lrVertexCount = 0;

        LineRenderer _lr;
        LineRenderer lr
        {
            get { return _lr = _lr ?? GetComponent<LineRenderer>(); }
        }

        internal override bool _BuildCollisionObject()
        {
            if (World == null)
            {
                return false;
            }
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

            SoftBody m_BSoftBody = SoftBodyHelpers.CreateRope(World.WorldInfo,
                meshSettings.startPoint.ToBullet(), meshSettings.endPoint.ToBullet(), meshSettings.numPointsInRope, 0);
            m_collisionObject = m_BSoftBody;

            verts = new Vector3[m_BSoftBody.Nodes.Count];
            norms = new Vector3[m_BSoftBody.Nodes.Count];

            for (int i = 0; i < m_BSoftBody.Nodes.Count; i++)
            {
                verts[i] = m_BSoftBody.Nodes[i].Position.ToUnity();
                norms[i] = m_BSoftBody.Nodes[i].Normal.ToUnity();
            }

            //Set SB settings
            SoftBodySettings.ConfigureSoftBody(m_BSoftBody);

            foreach (RopeAnchor anchor in ropeAnchors)
            {
                //anchorNode point 0 to 1, rounds to node # 
                int node = (int)Mathf.Floor(Mathf.Lerp(0, m_BSoftBody.Nodes.Count - 1, anchor.anchorNodePoint));

                if (anchor.body != null)
                    m_BSoftBody.AppendAnchor(node, (BulletSharp.RigidBody) anchor.body.GetCollisionObject());
                else
                {
                    m_BSoftBody.SetMass(node, 0);  //setting node mass to 0 fixes it in space apparently
                }

            }

            //TODO: lr, Doesnt always work in editor
            LineRenderer lr = GetComponent<LineRenderer>();

            lr.useWorldSpace = false;

            lr.SetVertexCount(verts.Length);
            lr.SetWidth(meshSettings.width, meshSettings.width);
            lr.SetColors(meshSettings.startColor, meshSettings.endColor);

            //Set SB position to GO position
            //m_BSoftBody.Rotate(transform.rotation.ToBullet());
            //m_BSoftBody.Translate(transform.position.ToBullet());
            //m_BSoftBody.Scale(transform.localScale.ToBullet());
            
            UpdateMesh();
            return true;
        }

        /// <summary>
        /// Create new SoftBody object
        /// </summary>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        /// <param name="buildNow">Build now or configure properties and call BuildSoftBody() after</param>
        /// <returns></returns>
        public static GameObject CreateNew(Vector3 position, Quaternion rotation, bool buildNow = true)
        {
            GameObject go = new GameObject("SoftBodyRope");
            go.transform.position = position;
            go.transform.rotation = rotation;
            BSoftBodyRope bRope = go.AddComponent<BSoftBodyRope>();

            UnityEngine.Material material = new UnityEngine.Material(Shader.Find("LineRenderFix"));

          
            bRope.lr.sharedMaterial = material;

            bRope.SoftBodySettings.ResetToSoftBodyPresets(SBSettingsPresets.Rope);
            if (buildNow)
                bRope._BuildCollisionObject();
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
                lr.SetColors(meshSettings.startColor, meshSettings.endColor);
            }
            for (int i = 0; i < verts.Length; i++)
            {
                lr.SetPosition(i, verts[i]);
            }

            //transform.SetTransformationFromBulletMatrix(m_BSoftBody.WorldTransform);  //Set SoftBody position, No motionstate
        }



    }

    [Serializable]
    public class RopeAnchor
    {
        [Tooltip("Anchor to body.  null = anchor to current rope node world position")]
        public BRigidBody body;

        //public bool anchorSameAsNode = true;
        [Range(0, 1)]
        [Tooltip("Anchor point location calulated from total rope lenghth.  Anchor point inserted at ((startPoint - endPoint) * anchorNodePoint; (0 to 1) (0 to 100%)")]
        public float anchorNodePoint;

    }

}