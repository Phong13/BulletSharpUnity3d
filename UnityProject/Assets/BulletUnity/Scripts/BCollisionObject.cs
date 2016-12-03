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


        //This is used to handle a design problem. 
        //We want OnEnable to add physics object to world and OnDisable to remove.
        //We also want user to be able to in script: AddComponent<CollisionObject>, configure it, add it to world, potentialy disable to delay it being added to world
        //Problem is OnEnable gets called before Start so that developer has no chance to configure object before it is added to world or prevent
        //It from being added.
        //Solution is not to add object to the world until after Start has been called. Start will do the first add to world. 
        protected bool m_startHasBeenCalled = false;

        protected CollisionObject m_collisionObject;
        protected BCollisionShape m_collisionShape;
        internal bool isInWorld = false;
        [SerializeField]
        protected BulletSharp.CollisionFlags m_collisionFlags = BulletSharp.CollisionFlags.None;
        [SerializeField]
        protected BulletSharp.CollisionFilterGroups m_groupsIBelongTo = BulletSharp.CollisionFilterGroups.DefaultFilter; // A bitmask
        [SerializeField]
        protected BulletSharp.CollisionFilterGroups m_collisionMask = BulletSharp.CollisionFilterGroups.AllFilter; // A colliding object must match this mask in order to collide with me.

        public virtual BulletSharp.CollisionFlags collisionFlags
        {
            get { return m_collisionFlags; }
            set {
                if (m_collisionObject != null && value != m_collisionFlags)
                {
                    m_collisionObject.CollisionFlags = value;
                    m_collisionFlags = value;
                } else
                {
                    m_collisionFlags = value;
                }
            }
        }

        public BulletSharp.CollisionFilterGroups groupsIBelongTo
        {
            get { return m_groupsIBelongTo; }
            set
            {
                if (m_collisionObject != null && value != m_groupsIBelongTo)
                {
                    Debug.LogError("Cannot change the collision group once a collision object has been created");
                } else 
                {
                    m_groupsIBelongTo = value;
                }
            }
        }

        public BulletSharp.CollisionFilterGroups collisionMask
        {
            get { return m_collisionMask; }
            set
            {
                if (m_collisionObject != null && value != m_collisionMask)
                {
                    Debug.LogError("Cannot change the collision mask once a collision object has been created");
                } else
                {
                    m_collisionMask = value;
                }
            }
        }

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
            if (bhw != null && m_onCollisionCallback != null)
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
                    world.RemoveCollisionObject(this);
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

        //Don't try to call functions on other objects such as the Physics world since they may not exit.
        protected virtual void Awake()
        {
            m_collisionShape = GetComponent<BCollisionShape>();
            if (m_collisionShape == null)
            {
                Debug.LogError("A BCollisionObject component must be on an object with a BCollisionShape component.");
            }
        }

        protected virtual void AddObjectToBulletWorld()
        {
            BPhysicsWorld.Get().AddCollisionObject(this);
        }

        protected virtual void RemoveObjectFromBulletWorld()
        {
            BPhysicsWorld.Get().RemoveCollisionObject(this);
        }

        
        // Add this object to the world on Start. We are doing this so that scripts which add this componnet to 
        // game objects have a chance to configure them before the object is added to the bullet world.
        // Be aware that Start is not affected by script execution order so objects such as constraints should
        // make sure that objects they depend on have been added to the world before they add themselves.
        // This can be called more than once
        internal virtual void Start()
        {
            if (m_startHasBeenCalled == false)
            {
                m_startHasBeenCalled = true;
                AddObjectToBulletWorld();
            }
        }

        //OnEnable and OnDisable are called when a game object is Activated and Deactivated. 
        //Unfortunately the first call comes before Awake and Start. We suppress this call so that the component
        //has a chance to initialize itself. Objects that depend on other objects such as constraints should make
        //sure those objects have been added to the world first.
        //don't try to call functions on world before Start is called. It may not exist.
        protected virtual void OnEnable()
        {
            if (!isInWorld && m_startHasBeenCalled)
            {
                AddObjectToBulletWorld();
            }
        }

        // when scene is closed objects, including the physics world, are destroyed in random order. 
        // There is no way to distinquish between scene close destruction and normal gameplay destruction.
        // Objects cannot depend on world existing when they Dispose of themselves. World may have been destroyed first.
        protected virtual void OnDisable()
        {
            if (isInWorld)
            {
                RemoveObjectFromBulletWorld();
            }
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

        public virtual void SetRotation(Quaternion rotation)
        {
            if (isInWorld)
            {
                BulletSharp.Math.Matrix newTrans = m_collisionObject.WorldTransform;
                BulletSharp.Math.Quaternion q = rotation.ToBullet();
                BulletSharp.Math.Matrix.RotationQuaternion(ref q, out newTrans);
                newTrans.Origin = transform.position.ToBullet();
                m_collisionObject.WorldTransform = newTrans;
                transform.rotation = rotation;
            }
            else
            {
                transform.rotation = rotation;
            }
        }

    }
}
