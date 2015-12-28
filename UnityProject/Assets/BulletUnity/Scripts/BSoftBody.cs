using UnityEngine;
using System.Collections;
using BulletSharp.SoftBody;
using System;
using BulletSharp;
using System.Collections.Generic;
//using BulletSharp.Math;

namespace BulletUnity {

    public abstract class BSoftBody : MonoBehaviour, IDisposable {
        protected SoftBody m_BSoftBody;
        protected bool isInWorld = false;

        void OnEnable() {
            if (BPhysicsWorld.Get().AddSoftBody(this)) {
                isInWorld = true;
            }
        }

        void OnDisable() {
            if (isInWorld) {
                BPhysicsWorld.Get().RemoveSoftBody(m_BSoftBody);
            }
            isInWorld = false;
        }

        public BulletSharp.SoftBody.SoftBody GetSoftBody() {
            if (m_BSoftBody == null) {
                _BuildSoftBody();
            }
            return m_BSoftBody;
        }

        internal abstract bool _BuildSoftBody();

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
                    BPhysicsWorld.Get().RemoveSoftBody(m_BSoftBody);
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
    }
}