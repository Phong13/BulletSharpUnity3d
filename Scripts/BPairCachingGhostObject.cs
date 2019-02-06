using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BulletSharp;

namespace BulletUnity
{

    public class BPairCachingGhostObject : BGhostObject
    {
        AlignedManifoldArray manifoldArray = new AlignedManifoldArray();

        private PairCachingGhostObject m_ghostObject
        {
            get { return (PairCachingGhostObject) m_collisionObject; }
        }

        internal override bool _BuildCollisionObject()
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
                m_collisionObject = new BulletSharp.PairCachingGhostObject();
                m_collisionObject.CollisionShape = cs;
                BulletSharp.Math.Matrix worldTrans;
                BulletSharp.Math.Quaternion q = transform.rotation.ToBullet();
                BulletSharp.Math.Matrix.RotationQuaternion(ref q, out worldTrans);
                worldTrans.Origin = transform.position.ToBullet();
                m_collisionObject.WorldTransform = worldTrans;
                m_collisionObject.UserObject = this;
                m_collisionObject.CollisionFlags = m_collisionObject.CollisionFlags | BulletSharp.CollisionFlags.KinematicObject;
                m_collisionObject.CollisionFlags &= ~BulletSharp.CollisionFlags.StaticObject;
            }
            else {
                BulletSharp.Math.Matrix worldTrans;
                BulletSharp.Math.Quaternion q = transform.rotation.ToBullet();
                BulletSharp.Math.Matrix.RotationQuaternion(ref q, out worldTrans);
                worldTrans.Origin = transform.position.ToBullet();
                m_collisionObject.WorldTransform = worldTrans;
                m_collisionObject.CollisionShape = cs;
                m_collisionObject.CollisionFlags = m_collisionObject.CollisionFlags | BulletSharp.CollisionFlags.KinematicObject;
                m_collisionObject.CollisionFlags &= ~BulletSharp.CollisionFlags.StaticObject;
            }
            return true;
        }

        void PrintCollisionFlags(BulletSharp.CollisionFlags flags)
        {
            string s = "";
            if ((flags & BulletSharp.CollisionFlags.CharacterObject) == BulletSharp.CollisionFlags.CharacterObject) s += " charObj,";
            if ((flags & BulletSharp.CollisionFlags.CustomMaterialCallback) == BulletSharp.CollisionFlags.CustomMaterialCallback) s += " custMat,";
            if ((flags & BulletSharp.CollisionFlags.DisableSpuCollisionProcessing) == BulletSharp.CollisionFlags.DisableSpuCollisionProcessing) s += " disableSpu,";
            if ((flags & BulletSharp.CollisionFlags.DisableVisualizeObject) == BulletSharp.CollisionFlags.DisableVisualizeObject) s += " disableVis,";
            if ((flags & BulletSharp.CollisionFlags.KinematicObject) == BulletSharp.CollisionFlags.KinematicObject) s += " kinematic,";
            if ((flags & BulletSharp.CollisionFlags.NoContactResponse) == BulletSharp.CollisionFlags.NoContactResponse) s += " NoContactResponse,";
            if ((flags & BulletSharp.CollisionFlags.StaticObject) == BulletSharp.CollisionFlags.StaticObject) s += " staticObj,";
            Debug.Log(s);

        }

        HashSet<CollisionObject> objsIWasInContactWithLastFrame = new HashSet<CollisionObject>();
        HashSet<CollisionObject> objsCurrentlyInContactWith = new HashSet<CollisionObject>();
        void FixedUpdate()
        {
            CollisionWorld collisionWorld = BPhysicsWorld.Get().world;
            collisionWorld.Dispatcher.DispatchAllCollisionPairs(m_ghostObject.OverlappingPairCache, collisionWorld.DispatchInfo, collisionWorld.Dispatcher);

            //m_currentPosition = m_ghostObject.WorldTransform.Origin;

            //float maxPen = 0f;
            objsCurrentlyInContactWith.Clear();
            for (int i = 0; i < m_ghostObject.OverlappingPairCache.NumOverlappingPairs; i++)
            {
                manifoldArray.Clear();

                BroadphasePair collisionPair = m_ghostObject.OverlappingPairCache.OverlappingPairArray[i];

                CollisionObject obj0 = collisionPair.Proxy0.ClientObject as CollisionObject;
                CollisionObject obj1 = collisionPair.Proxy1.ClientObject as CollisionObject;

                if ((obj0 != null && !obj0.HasContactResponse) || (obj1 != null && !obj1.HasContactResponse))
                    continue;

                if (collisionPair.Algorithm != null)
                {
                    collisionPair.Algorithm.GetAllContactManifolds(manifoldArray);
                }

                CollisionObject otherObj = null;
                if (manifoldArray.Count > 0)
                {
                    PersistentManifold pm = manifoldArray[0];
                    if (pm.Body0 == m_collisionObject)
                    {
                        otherObj = pm.Body1;
                    }
                    else
                    {
                        otherObj = pm.Body0;
                    }
                }
                else
                {
                    continue;
                }

                objsCurrentlyInContactWith.Add(otherObj);
                if (!objsIWasInContactWithLastFrame.Contains(otherObj))
                {
                    BOnTriggerEnter(otherObj, manifoldArray);
                }
                else
                {
                    BOnTriggerStay(otherObj, manifoldArray);
                }
            }
            objsIWasInContactWithLastFrame.ExceptWith(objsCurrentlyInContactWith);

            foreach (CollisionObject co in objsIWasInContactWithLastFrame)
            {
                BOnTriggerExit(co);
            }

            //swap the hashsets so objsIWasInContactWithLastFrame now contains the list of objs.
            HashSet<CollisionObject> temp = objsIWasInContactWithLastFrame;
            objsIWasInContactWithLastFrame = objsCurrentlyInContactWith;
            objsCurrentlyInContactWith = temp;
        }
         
        public override void BOnTriggerEnter(CollisionObject other, AlignedManifoldArray manifoldArray)
        {
            Debug.Log("Enter with " + other.UserObject + " fixedFrame " + BPhysicsWorld.Get().frameCount);
        }

        public override void BOnTriggerStay(CollisionObject other, AlignedManifoldArray manifoldArray)
        {
            Debug.Log("Stay with " + other.UserObject + " fixedFrame " + BPhysicsWorld.Get().frameCount);
        }

        public override void BOnTriggerExit(CollisionObject other)
        {
            Debug.Log("Exit with " + other.UserObject + " fixedFrame " + BPhysicsWorld.Get().frameCount);
        }
        //============
    }
}
