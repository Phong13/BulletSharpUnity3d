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

        CollisionWorld world;
        RigidBody rigidBody;
        ContactResultCallbackUnity contactCallback;
        HashSet<CollisionObject> objsIWasInContactWithLastFrame = new HashSet<CollisionObject>();
        HashSet<CollisionObject> objsCurrentlyInContactWith = new HashSet<CollisionObject>();

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
            objsCurrentlyInContactWith.Add(other);
            return 0; //todo what am I supposed to return?
        }

        void FixedUpdate() {
            objsCurrentlyInContactWith.Clear();
            world.ContactTest(rigidBody, contactCallback);

            //TODO see if there is a way to do this without needing to allocate two HashSets every FixeUpdate
            //TODO think about efficiency. things won't change much most of the FixedUpdate calls. Using these sets may be overkill
            //TODO can probably arrays if less than 4 total collisions in previous and current and use hashsets if more.

            //enter collisions
            HashSet<CollisionObject> enterObjects = new HashSet<CollisionObject>(objsCurrentlyInContactWith);
            enterObjects.ExceptWith(objsIWasInContactWithLastFrame);
            //exit collisions
            HashSet<CollisionObject> exitObjects = new HashSet<CollisionObject>(objsIWasInContactWithLastFrame);
            exitObjects.ExceptWith(objsCurrentlyInContactWith);
            //stay collisions
            objsCurrentlyInContactWith.ExceptWith(enterObjects);

            foreach(CollisionObject o in enterObjects) {
                OnCollisionEnter();
            }

            foreach (CollisionObject o in objsCurrentlyInContactWith) {
                OnCollisionStay();
            }

            foreach (CollisionObject o in exitObjects) {
                OnCollisionExit();
            }

            objsIWasInContactWithLastFrame.Clear();
            objsIWasInContactWithLastFrame.UnionWith(enterObjects);
            objsIWasInContactWithLastFrame.UnionWith(objsCurrentlyInContactWith);
        }

        //todo need the collision details
        public virtual void OnCollisionEnter() {
            Debug.Log("Enter with ");
        }

        //todo need the collision details
        public virtual void OnCollisionStay() {
            Debug.Log("Stay with ");
        }

        //todo need the collision details
        public virtual void OnCollisionExit() {
            Debug.Log("Exit with ");
        }
    }
}
