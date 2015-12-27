using UnityEngine;
using System.Collections;
using BulletSharp;
using System;
using System.Collections.Generic;

namespace BulletUnity {

    /*
    This is implemented as a separate component from the BRigidBody for performance reasons. It must be assigned to a
    game object with a RigidBody component attached.

    It is expensive for every BRigidBody to do a CollisionCheck every simulation frame. Bullet supports
    multiple approaches to collision detection.

    If you want to detect all collisions between all objects then the most efficient way is to iterate over
    the manifolds in the World Collision dispatcher.

    If you want only a few objects in your scene to have collision callbacks then attach the BCollisionCallback to the
    rigidbodies you want the callbacks for.

    With bullet you can also query for collisions between objects on demand
    */
    public class BCollisionCallback : MonoBehaviour {

        public class ContactResultCallbackUnity : ContactResultCallback {
            BCollisionCallback notifyMe;

            public ContactResultCallbackUnity(BCollisionCallback callMe) {
                notifyMe = callMe;
            }

            public override float AddSingleResult(ManifoldPoint cp, CollisionObjectWrapper colObj0Wrap, int partId0, int index0, CollisionObjectWrapper colObj1Wrap, int partId1, int index1) {
                return notifyMe.AddSingleResult(cp, colObj0Wrap, partId0, index0, colObj1Wrap, partId1, index1);
            }
        }

        public class BulletUnityContactPoint {
            public CollisionObject objA;
            public CollisionShape shapeA;
            public CollisionObject objB;
            public CollisionShape shapeB;
            public float AppliedImpulse;
            public float AppliedImpulseLateral1;
            public float AppliedImpulseLateral2;
            public float CombinedFriction;
            public float CombinedRestitution;
            public float CombinedRollingFriction;
            public float ContactCfm1;
            public float ContactCfm2;
            public float ContactMotion1;
            public float ContactMotion2;
            public float Distance;
            public float Distance1;
            //index is the index in the triangle array for triangle meshes
            public float Index0;
            public float Index1;
            //part is the part id for large meshes
            public int PartID0;
            public int PartID1;
            public Vector3 LateralFrictionDir1;
            public Vector3 LateralFrictionDir2;
            public int Lifetime;
            public Vector3 NormalWorldOnB;
            public Vector3 PositionWorldOnB;
            public Vector3 PositionWorldOnA;

            public BulletUnityContactPoint(ManifoldPoint mp, CollisionObjectWrapper a, CollisionObjectWrapper b) {
                objA = a.CollisionObject;
                objB = b.CollisionObject;
                shapeA = a.CollisionShape;
                shapeB = b.CollisionShape;
                AppliedImpulse = mp.AppliedImpulse;
                AppliedImpulseLateral1 = mp.AppliedImpulseLateral1;
                AppliedImpulseLateral2 = mp.AppliedImpulseLateral2;
                CombinedFriction = mp.CombinedFriction;
                CombinedRestitution = mp.CombinedRestitution;
                CombinedRollingFriction = mp.CombinedRollingFriction;
                ContactCfm1 = mp.ContactCfm1;
                ContactCfm2 = mp.ContactCfm2;
                ContactMotion1 = mp.ContactMotion1;
                ContactMotion2 = mp.ContactMotion2;
                Distance = mp.Distance;
                Distance1 = mp.Distance1;
                Index0 = mp.Index0;
                Index1 = mp.Index1;
                PartID0 = mp.PartId0;
                PartID1 = mp.PartId1;
                LateralFrictionDir1 = mp.LateralFrictionDir1.ToUnity();
                LateralFrictionDir2 = mp.LateralFrictionDir2.ToUnity();
                Lifetime = mp.LifeTime;
                NormalWorldOnB = mp.NormalWorldOnB.ToUnity();
                PositionWorldOnA = mp.PositionWorldOnA.ToUnity();
                PositionWorldOnB = mp.PositionWorldOnB.ToUnity();
            }
        }

        public struct SingleCollision {
            public CollisionObject obj;
            public BulletUnityContactPoint collisionDetails;

            public SingleCollision(CollisionObject o, BulletUnityContactPoint cp) {
                obj = o;
                collisionDetails = cp;
            }
            /*
            public override bool Equals(object obj) {
                if (!(obj is SingleCollision)) return false;
                SingleCollision other = (SingleCollision)obj;
                return obj.Equals(other.obj);
            }

            public override int GetHashCode() {
                return obj.GetHashCode();
            }
            */
        }

        public class SingleCollisionComparer:IEqualityComparer<SingleCollision>{
            public bool Equals(SingleCollision a, SingleCollision b) {
                return a.Equals(b);
            }
            public int GetHashCode(SingleCollision a) {
                return a.GetHashCode();
            } 
        }

        public bool returnFullCollisionDetails;
        CollisionWorld world;
        RigidBody rigidBody;
        ContactResultCallbackUnity contactCallback;
        HashSet<SingleCollision> objsIWasInContactWithLastFrame = new HashSet<SingleCollision>(new SingleCollisionComparer());
        HashSet<SingleCollision> objsCurrentlyInContactWith = new HashSet<SingleCollision>(new SingleCollisionComparer());

        void OnEnable() {
            objsIWasInContactWithLastFrame.Clear();
            BRigidBody brb = GetComponent<BRigidBody>();
            if (brb == null) {
                Debug.LogError("BCollisionCallback must be attached to a game object with a BRigidBody component.");
                return;
            }
            rigidBody = brb.GetRigidBody();
            world = BPhysicsWorld.Get().World;
            if (contactCallback == null) contactCallback = new ContactResultCallbackUnity(this);
        }

        void OnDisable() {
            objsIWasInContactWithLastFrame.Clear();
            rigidBody = null;
            world = null;
        }

        void OnDestroy() {
            contactCallback.Dispose();
        }

        // index is the index in the triangle array if the collision object was a triangle array
        // partID is the part of the mesh involved
        public float AddSingleResult(ManifoldPoint cp, CollisionObjectWrapper colObj0Wrap, int partId0, int index0, CollisionObjectWrapper colObj1Wrap, int partId1, int index1) {
            CollisionObject other = colObj0Wrap.CollisionObject;
            if (other == rigidBody) {
                other = colObj1Wrap.CollisionObject;
            }
            BulletUnityContactPoint cpp = null;
            if (returnFullCollisionDetails) {
                cpp = new BulletUnityContactPoint(cp,colObj0Wrap,colObj1Wrap);
            }
            objsCurrentlyInContactWith.Add(new SingleCollision(other, cpp));
            return 0; //todo what am I supposed to return?
        }

        void FixedUpdate() {
            objsCurrentlyInContactWith.Clear();
            world.ContactTest(rigidBody, contactCallback);

            //TODO see if there is a way to do this without needing to allocate two HashSets every FixeUpdate
            //TODO think about efficiency. things won't change much most of the FixedUpdate calls. Using these sets may be overkill
            //TODO can probably arrays if less than 4 total collisions in previous and current and use hashsets if more.

            //enter collisions
            HashSet<SingleCollision> enterObjects = new HashSet<SingleCollision>(objsCurrentlyInContactWith, new SingleCollisionComparer());
            enterObjects.ExceptWith(objsIWasInContactWithLastFrame);
            //exit collisions
            HashSet<SingleCollision> exitObjects = new HashSet<SingleCollision>(objsIWasInContactWithLastFrame, new SingleCollisionComparer());
            exitObjects.ExceptWith(objsCurrentlyInContactWith);
            //stay collisions
            objsCurrentlyInContactWith.ExceptWith(enterObjects);

            foreach(SingleCollision o in enterObjects) {
                BOnCollisionEnter(o);
            }

            foreach (SingleCollision o in objsCurrentlyInContactWith) {
                BOnCollisionStay(o);
            }

            foreach (SingleCollision o in exitObjects) {
                BOnCollisionExit(o);
            }

            objsIWasInContactWithLastFrame.Clear();
            objsIWasInContactWithLastFrame.UnionWith(enterObjects);
            objsIWasInContactWithLastFrame.UnionWith(objsCurrentlyInContactWith);
        }

        public virtual void BOnCollisionEnter(SingleCollision details) {
            Debug.Log("Enter with " + details.obj + " details " + details.collisionDetails + " frame" + BPhysicsWorld.Get().frameCount);
        }

        public virtual void BOnCollisionStay(SingleCollision details) {
            Debug.Log("Stay with " + details.obj + " details " + details.collisionDetails + " frame" + BPhysicsWorld.Get().frameCount);
        }

        public virtual void BOnCollisionExit(SingleCollision details) {
            Debug.Log("Exit with " + details.obj + " details " + details.collisionDetails + " frame" + BPhysicsWorld.Get().frameCount);
        }
    }
}
