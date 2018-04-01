using UnityEngine;
using System.Collections;
using BulletSharp;
using System;

namespace BulletUnity
{
    [RequireComponent(typeof(BMultiBodyLink))]
    public abstract class BMultiBodyConstraint : MonoBehaviour, IDisposable
    {
        protected MultiBodyConstraint m_multiBodyConstraintPtr;

        [NonSerialized]
        internal bool isInWorld = false;

        public MultiBodyConstraint GetMultiBodyConstraint(MultiBody mb)
        {
            if (m_multiBodyConstraintPtr == null)
            {
                BMultiBodyLink mbl = GetComponent<BMultiBodyLink>();
                if (mbl == null)
                {
                    Debug.LogError("BMultiBodyConstraint must have a BMultiBodyLink");
                    return null;
                }
                if (mbl.index == -1)
                {
                    Debug.LogError("Bad BMultiBodyLink index");
                    return null;
                }
                m_multiBodyConstraintPtr = _CreateConstraint(mb, mbl.index);
            }
            return m_multiBodyConstraintPtr;
        }

        void OnDestroy()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            System.GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isdisposing)
        {
            if (m_multiBodyConstraintPtr != null)
            {
                m_multiBodyConstraintPtr.Dispose();
                m_multiBodyConstraintPtr = null;
            }
        }

        protected abstract MultiBodyConstraint _CreateConstraint(MultiBody mb, int linkIndex);
    }
}
