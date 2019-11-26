using BulletSharp;
using UnityEngine;

namespace BulletUnity
{
    public abstract class BCollisionCallbacks : MonoBehaviour, BCollisionObject.BICollisionCallbackEventHandler
    {
        public abstract void Start();

        public void OnEnable()
        {
            BCollisionObject co = GetComponent<BCollisionObject>();
            if (co != null)
            {
                co.AddOnCollisionCallbackEventHandler(this);
            }
        }

        public void OnDisable()
        {
            BCollisionObject co = GetComponent<BCollisionObject>();
            if (co != null)
            {
                co.RemoveOnCollisionCallbackEventHandler(this);
            }
        }

        public abstract void OnFinishedVisitingManifolds();

        public abstract void OnVisitPersistentManifold(PersistentManifold pm);
    }
}
