using UnityEngine;
using System.Collections;
using BulletSharp.SoftBody;
using System;
using BulletSharp;
using System.Collections.Generic;
//using BulletSharp.Math;

namespace BulletUnity
{

    public class BSoftBody : MonoBehaviour, IDisposable
    {
        //common Soft body settings class used for all softbodies, parameters set based on type of soft body
        [SerializeField]
        private SBSettings _softBodySettings = new SBSettings();      //SoftBodyEditor will display this when needed
        public SBSettings SoftBodySettings
        {
            get { return _softBodySettings; }
            set { _softBodySettings = value; }
        }

        protected SoftBody m_BSoftBody;

        protected bool isInWorld = false;

        SoftRigidDynamicsWorld _world;
        protected SoftRigidDynamicsWorld World
        {
            get { return _world = _world ?? (SoftRigidDynamicsWorld)BPhysicsWorld.Get().world; }
        }

        //for converting to/from unity mesh
        protected UnityEngine.Vector3[] verts = new UnityEngine.Vector3[0];
        protected UnityEngine.Vector3[] norms = new UnityEngine.Vector3[0];
        protected int[] tris = new int[1];

        //void OnEnable()    
        void Start()  //Need to add in start or SB is not initialized and world may not exist yet
        {
            if (BPhysicsWorld.Get().AddSoftBody(this))
            {
                isInWorld = true;
            }
        }
        
        void OnDisable()
        {
            if (isInWorld)
            {
                World.RemoveSoftBody(m_BSoftBody);
            }
            isInWorld = false;
        }

        public BulletSharp.SoftBody.SoftBody GetSoftBody()
        {
            if (m_BSoftBody == null)
            {
                BuildSoftBody();
            }
            return m_BSoftBody;
        }

        public virtual bool BuildSoftBody()
        {
            return false;
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
                if (m_BSoftBody != null)
                {
                    World.RemoveSoftBody(m_BSoftBody);
                }
            }
            if (m_BSoftBody != null)
            {
                //if (m_BSoftBody.MotionState != null) m_BSoftBody.MotionState.Dispose();
                m_BSoftBody.Dispose();
                m_BSoftBody = null;
            }
            Debug.Log("Destroying SoftBody " + name);
        }

        public void DumpDataFromBullet()
        {
            if (isInWorld)
            {
                if (verts.Length != m_BSoftBody.Nodes.Count)
                {
                    verts = new Vector3[m_BSoftBody.Nodes.Count];
                }
                if (norms.Length != verts.Length)
                {
                    norms = new Vector3[m_BSoftBody.Nodes.Count];
                }
                for (int i = 0; i < m_BSoftBody.Nodes.Count; i++)
                {
                    verts[i] = m_BSoftBody.Nodes[i].Position.ToUnity();
                    norms[i] = m_BSoftBody.Nodes[i].Normal.ToUnity();
                }
            }
        }


        void Update()
        {
            DumpDataFromBullet();  //Get Bullet data
            UpdateMesh(); //Update mesh based on bullet data
            //Make coffee
        }

        /// <summary>
        /// Update Mesh (or line renderer) at runtime, call from Update 
        /// </summary>
        public virtual void UpdateMesh()
        {

        }





    }
}