using System;
using UnityEngine;
using System.Collections;
using BulletSharp;

namespace BulletUnity {
    [System.Serializable]
    public abstract class BTypedConstraint : MonoBehaviour, IDisposable {

        internal TypedConstraint constraintPtr = null;
        internal bool isInWorld;

        void OnDestroy() {
            Dispose(false);
        }

        //do not override this
        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isdisposing) {
            if (constraintPtr != null) {
                constraintPtr.Dispose();
                constraintPtr = null;
            }
        }

        //called by Physics World just before constraint is added to world.
        //the current constraint properties are used to rebuild the constraint.
        internal abstract bool _BuildConstraint();

        public abstract TypedConstraint GetConstraint();
    }
}
