using UnityEngine;
using System;
using System.Collections;
using BulletSharp;

namespace BulletUnity
{
    public class BCollisionObject : MonoBehaviour, IDisposable
    {

        public interface BICollisionCallbackEventHandler
        {
            void OnVisitPersistentManifold(PersistentManifold pm);
            void OnFinishedVisitingManifolds();
        }

        //public delegate void OnCollisionCallbackEventHandler(PersistentManifold pm);

        protected CollisionObject m_collisionObject;
        protected BCollisionShape m_collisionShape;
        protected bool isInWorld = false;
        public BulletSharp.CollisionFlags m_collisionFlags = BulletSharp.CollisionFlags.StaticObject;
        public BulletSharp.CollisionFilterGroups m_groupsIBelongTo = BulletSharp.CollisionFilterGroups.DefaultFilter; // A bitmask
        public BulletSharp.CollisionFilterGroups m_collisionMask = BulletSharp.CollisionFilterGroups.AllFilter; // A colliding object must match this mask in order to collide with me.


        BICollisionCallbackEventHandler m_onCollisionCallback;
        public virtual BICollisionCallbackEventHandler collisionCallbackEventHandler
        {
            get { return m_onCollisionCallback; }
        }

        public virtual void AddOnCollisionCallbackEventHandler(BICollisionCallbackEventHandler myCallback)
        {
            BPhysicsWorld bhw = BPhysicsWorld.Get();
            if (m_onCollisionCallback != null)
            {
                Debug.LogErrorFormat("BCollisionObject {0} already has a collision callback. You must remove it before adding another. ", name);
                
            }
            m_onCollisionCallback = myCallback;
            bhw.RegisterCollisionCallbackListener(m_onCollisionCallback);
        }

        public virtual void RemoveOnCollisionCallbackEventHandler()
        {
            BPhysicsWorld bhw = BPhysicsWorld.Get();
            if (m_onCollisionCallback != null)
            {
                bhw.DeregisterCollisionCallbackListener(m_onCollisionCallback);
            }
            m_onCollisionCallback = null;
        }

        //called by Physics World just before rigid body is added to world.
        //the current rigid body properties are used to rebuild the rigid body.
        internal virtual bool _BuildCollisionObject()
        {
            BPhysicsWorld world = BPhysicsWorld.Get();
            if (m_collisionObject != null)
            {
                if (isInWorld && world != null)
                {
                    isInWorld = false;
                    world.RemoveCollisionObject(m_collisionObject);
                }
            }

            if (transform.localScale != UnityEngine.Vector3.one)
            {
                Debug.LogError("The local scale on this collision shape is not one. Bullet physics does not support scaling on a rigid body world transform. Instead alter the dimensions of the CollisionShape.");
            }

            m_collisionShape = GetComponent<BCollisionShape>();
            if (m_collisionShape == null)
            {
                Debug.LogError("There was no collision shape component attached to this BRigidBody. " + name);
                return false;
            }

            CollisionShape cs = m_collisionShape.GetCollisionShape();
            //rigidbody is dynamic if and only if mass is non zero, otherwise static


            if (m_collisionObject == null)
            {
                m_collisionObject = new CollisionObject();
                m_collisionObject.CollisionShape = cs;
                m_collisionObject.UserObject = this;

                BulletSharp.Math.Matrix worldTrans;
                BulletSharp.Math.Quaternion q = transform.rotation.ToBullet();
                BulletSharp.Math.Matrix.RotationQuaternion(ref q, out worldTrans);
                worldTrans.Origin = transform.position.ToBullet();
                m_collisionObject.WorldTransform = worldTrans;
                m_collisionObject.CollisionFlags = m_collisionFlags;
            }
            else {
                m_collisionObject.CollisionShape = cs;
                BulletSharp.Math.Matrix worldTrans;
                BulletSharp.Math.Quaternion q = transform.rotation.ToBullet();
                BulletSharp.Math.Matrix.RotationQuaternion(ref q, out worldTrans);
                worldTrans.Origin = transform.position.ToBullet();
                m_collisionObject.WorldTransform = worldTrans;
                m_collisionObject.CollisionFlags = m_collisionFlags;
            }
            return true;
        }

        public virtual CollisionObject GetCollisionObject()
        {
            if (m_collisionObject == null)
            {
                _BuildCollisionObject();
            }
            return m_collisionObject;
        }

        protected virtual void Awake()
        {
            /*
            BRigidBody[] rbs = GetComponentsInParent<BRigidBody>();
            if (rbs.Length != 1)
            {
                Debug.LogError("Can't nest rigid bodies. The transforms are updated by Bullet in undefined order which can cause spasing. Object " + name);
            }
            */
            m_collisionShape = GetComponent<BCollisionShape>();
            if (m_collisionShape == null)
            {
                Debug.LogError("A BRigidBody component must be on an object with a BCollisionShape component.");
            }
        }

        protected virtual void Start()
        {
            if (BPhysicsWorld.Get().AddCollisionObject(this))
            {
                isInWorld = true;
            }
        }

        protected virtual void OnEnable()
        {
            if (!isInWorld)
            {
                BPhysicsWorld.Get().AddCollisionObject(this);
            }
            isInWorld = true;
        }

        protected virtual void OnDisable()
        {
            if (isInWorld)
            {
                BPhysicsWorld.Get().RemoveCollisionObject(m_collisionObject);
            }
            isInWorld = false;
        }

        protected virtual void OnDestroy()
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
            if (isInWorld && isdisposing && m_collisionObject != null)
            {
                BPhysicsWorld pw = BPhysicsWorld.Get();
                if (pw != null && pw.world != null)
                {
                    ((DiscreteDynamicsWorld)pw.world).RemoveCollisionObject(m_collisionObject);
                }
            }
            if (m_collisionObject != null)
            {
               
                m_collisionObject.Dispose();
                m_collisionObject = null;
            }
            Debug.Log("Destroying CollisionObject " + name);
        }

        public virtual void SetPosition(Vector3 position)
        {
            if (isInWorld)
            {
                BulletSharp.Math.Matrix newTrans = m_collisionObject.WorldTransform;
                newTrans.Origin = position.ToBullet();
                m_collisionObject.WorldTransform = newTrans;
                transform.position = position;
            } else
            {
                transform.position = position;
            }

        }

        public virtual void SetPositionAndRotation(Vector3 position, Quaternion rotation)
        {
            if (isInWorld)
            {
                BulletSharp.Math.Matrix newTrans = m_collisionObject.WorldTransform;
                BulletSharp.Math.Quaternion q = rotation.ToBullet();
                BulletSharp.Math.Matrix.RotationQuaternion(ref q, out newTrans);
                newTrans.Origin = transform.position.ToBullet();
                m_collisionObject.WorldTransform = newTrans;
                transform.position = position;
                transform.rotation = rotation;
            } else
            {
                transform.position = position;
                transform.rotation = rotation;
            }
        }

    }
}
