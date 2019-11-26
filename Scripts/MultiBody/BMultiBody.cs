using System;
using System.Collections.Generic;
using BulletSharp;
using UnityEngine;
namespace BulletUnity
{
    public class BMultiBody : BCollisionObject
    {

        public List<BMultiBodyLink> Links;

        public object UserObject
        {
            get;
            set;
        }


        MultiBody m_multiBody;

        public MultiBody MultiBody
        {
            get
            {
                return m_multiBody;
            }
        }

        BulletSharp.Math.Vector3 _localInertia = BulletSharp.Math.Vector3.Zero;
        public BulletSharp.Math.Vector3 localInertia
        {
            get
            {
                return _localInertia;
            }
        }


        [SerializeField]
        float _mass = 1f;
        public float mass
        {
            set
            {
                _mass = value;
            }
            get
            {
                return _mass;
            }
        }

        [SerializeField]
        bool selfCollision;
        public bool SelfCollision
        {
            set
            {
                selfCollision = value;
            }
            get
            {
                return selfCollision;
            }
        }

        /*   public override CollisionObject GetCollisionObject()
           {
               return m_collisionObject;
           }*/

        protected override void Dispose(bool isdisposing)
        {
            if (m_multiBody != null)
            {
                if (isInWorld && isdisposing)
                {
                    BPhysicsWorld pw = BPhysicsWorld.Get();
                    if (pw != null && pw.world != null)
                    {
                        ((MultiBodyDynamicsWorld)pw.world).RemoveMultiBody(m_multiBody);
                    }
                }
                m_multiBody.Dispose();
                m_multiBody = null;

            }
        }


        protected override void AddObjectToBulletWorld()
        {
            BPhysicsWorld.Get().AddMultiBody(this);
        }

        protected override void RemoveObjectFromBulletWorld()
        {
            BPhysicsWorld pw = BPhysicsWorld.Get();
            if (pw != null && m_multiBody != null && isInWorld)
            {
                pw.RemoveMultiBody(m_multiBody);
                isInWorld = false;
            }
        }

        /**
Creates or configures a RigidBody based on the current settings. Does not alter the internal state of this component in any way. 
Can be used to create copies of this BRigidBody for use in other physics simulations.
*/
        public virtual bool CreateMultiBody(ref MultiBody mb, ref BulletSharp.Math.Vector3 localInertia, CollisionShape cs)
        {
            localInertia = BulletSharp.Math.Vector3.Zero;

            if (_mass > 0)
            {
                cs.CalculateLocalInertia(_mass, out localInertia);
            }

            if (mb == null)
            {

                int nbLinks = Links.Count;
                foreach (BMultiBodyLink link in Links)
                    nbLinks += link.NbLinks;

                Debug.Log("Adding multibody with " + nbLinks + " links");
                mb = new MultiBody(nbLinks, _mass, localInertia, false, false);
                mb.BaseWorldTransform = transform.localToWorldMatrix.ToBullet();
                mb.HasSelfCollision = SelfCollision;
                var collider = new MultiBodyLinkCollider(mb, -1);
                collider.CollisionShape = cs;
                collider.WorldTransform = transform.localToWorldMatrix.ToBullet();
                collider.CollisionFlags = collisionFlags;
                collider.UserObject = UserObject ?? this;
                BPhysicsWorld.Get().world.AddCollisionObject(collider, groupsIBelongTo, collisionMask);
                mb.BaseCollider = collider;
                m_collisionObject = collider;

                BulletMultiBodyLinkColliderProxy baseProxy = gameObject.GetComponent<BulletMultiBodyLinkColliderProxy>();
                if (baseProxy == null)
                    baseProxy = gameObject.AddComponent<BulletMultiBodyLinkColliderProxy>();
                baseProxy.target = collider;

                try
                {

                    int currentLinkIndex = 0;
                    for (int i = 0; i < Links.Count; ++i)
                    {
                        currentLinkIndex += Links[i].AddLinkToMultiBody(this, currentLinkIndex, -1, transform);
                    }

                    mb.FinalizeMultiDof();
                }
                catch (Exception e) // if an error occurs, don't add the object, otherwise unity will crash
                {
                    Debug.LogErrorFormat("Error occured while setting MultiBody : {0}\n{1}", e.Message, e.StackTrace);
                    BPhysicsWorld.Get().world.RemoveCollisionObject(collider);
                    return false;
                }
                mb.CanSleep = false;
                mb.UserObject = UserObject ?? this;
            }
            return true;
        }

        //called by Physics World just before multi body is added to world.
        //the current multi body properties are used to rebuild the multi body.
        internal override bool _BuildCollisionObject()
        {
            BPhysicsWorld world = BPhysicsWorld.Get();
            if (m_multiBody != null && isInWorld && world != null)
            {
                isInWorld = false;
                world.RemoveMultiBody(m_multiBody);
            }

            if (transform.localScale != UnityEngine.Vector3.one)
            {
                Debug.LogErrorFormat("The local scale on {0} rigid body is not one. Bullet physics does not support scaling on a rigid body world transform. Instead alter the dimensions of the CollisionShape.", name);
                return false;
            }

            return CreateMultiBody(ref m_multiBody, ref _localInertia, GetComponent<BCollisionShape>().GetCollisionShape());
        }
    }
}