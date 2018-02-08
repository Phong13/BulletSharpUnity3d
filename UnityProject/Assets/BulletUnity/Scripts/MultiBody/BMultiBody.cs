using UnityEngine;
using System.Collections;
using System;
using BulletSharp;
using BulletSharp.Math;
using System.Collections.Generic;
using BulletUnity.Debugging;

namespace BulletUnity
{
    [AddComponentMenu("Physics Bullet/MultiBody")]
    public class BMultiBody : BCollisionObject, IDisposable
    {
        public bool fixedBase;
        public bool canSleep;
        public float baseMass;

        [NonSerialized]
        protected MultiBody m_multibody;
        protected BCollisionShape m_baseCollisionShape;
        protected MultiBodyLinkCollider m_baseCollider;
        protected List<BMultiBodyLink> m_links;
        
        public MultiBody GetMultiBody()
        {
            return m_multibody;
        }

        public List<BMultiBodyLink> GetLinks()
        {
            return m_links;
        }

        public MultiBodyLinkCollider GetBaseCollider()
        {
            return m_baseCollider;
        }

        protected virtual void AddMultiBodyToBulletWorld()
        {
            BPhysicsWorld.Get().AddMultiBody(this);
        }

        protected virtual void RemoveMultiBodyFromBulletWorld()
        {
            if (isInWorld)
            {
                BPhysicsWorld w = BPhysicsWorld.Get(); //todo adding UserObject to multibody should fix this
                if (w != null) BPhysicsWorld.Get().RemoveMultiBody(this);
            }
        }


        // Add this object to the world on Start. We are doing this so that scripts which add this componnet to 
        // game objects have a chance to configure them before the object is added to the bullet world.
        // Be aware that Start is not affected by script execution order so objects such as constraints should
        // make sure that objects they depend on have been added to the world before they add themselves.
        // This can be called more than once
        internal override void Start()
        {
            if (m_startHasBeenCalled == false)
            {
                m_startHasBeenCalled = true;
                AddMultiBodyToBulletWorld();
            }
        }

        private void Update()
        {
            if (m_baseCollider != null && isInWorld)
            {
                Matrix4x4 m = m_baseCollider.WorldTransform.ToUnity();
                transform.position = BSExtensionMethods2.ExtractTranslationFromMatrix(ref m);
                transform.rotation = BSExtensionMethods2.ExtractRotationFromMatrix(ref m);
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
                AddMultiBodyToBulletWorld();
            }
        }

        // when scene is closed objects, including the physics world, are destroyed in random order. 
        // There is no way to distinquish between scene close destruction and normal gameplay destruction.
        // Objects cannot depend on world existing when they Dispose of themselves. World may have been destroyed first.
        protected virtual void OnDisable()
        {
            if (isInWorld)
            {
                RemoveMultiBodyFromBulletWorld();
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
            if (isInWorld && isdisposing && m_multibody != null)
            {
                BPhysicsWorld pw = BPhysicsWorld.Get();
                if (pw != null && pw.world != null)
                {
                    if (m_baseCollider != null)
                    {
                        pw.world.RemoveCollisionObject(m_baseCollider);
                    }
                    if (m_multibody != null)
                    {
                        ((MultiBodyDynamicsWorld)pw.world).RemoveMultiBody(m_multibody);
                    }
                }
            }
            if (m_multibody != null)
            {

                m_multibody.Dispose();
                m_multibody = null;
            }
            if (m_baseCollider != null)
            {
                m_baseCollider.Dispose();
                m_baseCollider = null;
            }
            if (m_links != null)
            {
                m_links.Clear();
                m_links = null;
            }
        }

        internal bool _BuildMultiBody()
        {
            BPhysicsWorld world = BPhysicsWorld.Get();
            if (m_multibody != null && isInWorld && world != null)
            {
                isInWorld = false;
                world.RemoveMultiBody(this);
            }

            if (transform.localScale != UnityEngine.Vector3.one)
            {
                Debug.LogErrorFormat("The local scale on {0} rigid body is not one. Bullet physics does not support scaling on a rigid body world transform. Instead alter the dimensions of the CollisionShape.", name);
            }

            m_baseCollisionShape = GetComponent<BCollisionShape>();
            if (m_baseCollisionShape == null)
            {
                Debug.LogErrorFormat("There was no collision shape component attached to this BMultiBody. {0}", name);
                return false;
            }
            if (GetComponent<BMultiBodyLink>() != null)
            {
                Debug.LogErrorFormat("There must not be a BMultiBodyLink component attached to the gameObject with a BMultiBody component. {0}", name);
                return false;
            }

            m_links = new List<BMultiBodyLink>();
            if (!GetLinksInChildrenAndNumber(transform, m_links, -1))
            {
                Debug.LogError("Error building multibody");
                return false;
            }
            if (m_links.Count == 0)
            {
                Debug.LogError("Could not find any links");
                return false;
            }
            BCollisionShape[] shapes = new BCollisionShape[m_links.Count];
            BMultiBodyConstraint[] constraints = new BMultiBodyConstraint[m_links.Count];
            for (int i = 0; i < m_links.Count; i++)
            {
                shapes[i] = m_links[i].GetComponent<BCollisionShape>();
                if (shapes[i] == null && shapes[i].GetComponent<RigidBody>() != null)
                {
                    Debug.LogErrorFormat("BMultiBodyLink must not have a RigidBody component. {0}", m_links[i]);
                    return false;
                }
                constraints[i] = m_links[i].GetComponent<BMultiBodyConstraint>();
            }

            BulletSharp.MultiBody mb = m_multibody;
            CreateOrConfigureMultiBody(ref mb, baseMass, shapes, constraints);
            m_multibody = mb;

            //TODO is this allowed
            //m_multibody.UserObject = this;

            return true;
        }

        void CreateOrConfigureMultiBody(ref MultiBody mb, float baseMass, BCollisionShape[] shapes, BMultiBodyConstraint[] constraints)
        {
            BulletSharp.Math.Vector3 inertia = BulletSharp.Math.Vector3.Zero;
            if (baseMass != 0)
            {
                CollisionShape cs = m_baseCollisionShape.GetCollisionShape();
                cs.CalculateLocalInertia(baseMass, out inertia);
            }

            mb = new MultiBody(m_links.Count, baseMass, inertia, fixedBase, canSleep);
            mb.BasePosition = transform.position.ToBullet();
            UnityEngine.Quaternion r = UnityEngine.Quaternion.Inverse(transform.rotation);
            mb.WorldToBaseRot = r.ToBullet();
            for (int i = 0; i < m_links.Count; i++)
            {
                Debug.Log("Found link " + i + " parent " + m_links[i].parentIndex + " index " + m_links[i].index);
                CollisionShape cs = shapes[i].GetCollisionShape();
                if (cs != null)
                {
                    cs.CalculateLocalInertia(m_links[i].mass, out inertia);
                } else
                {
                    inertia = BulletSharp.Math.Vector3.Zero;
                }
                FeatherstoneJointType jt = m_links[i].jointType;
                int parentIdx = m_links[i].parentIndex;
                UnityEngine.Vector3 parentCOM2ThisPivotOffset;
                if (m_links[i].parentIndex >= 0) {
                    parentCOM2ThisPivotOffset = m_links[parentIdx].transform.InverseTransformPoint(m_links[i].transform.TransformPoint(m_links[i].localPivotPosition));
                } else
                {
                    parentCOM2ThisPivotOffset = transform.InverseTransformPoint(m_links[i].transform.TransformPoint(m_links[i].localPivotPosition));
                }
                UnityEngine.Vector3 thisPivotToThisCOMOffset = -m_links[i].localPivotPosition;
                UnityEngine.Quaternion parentToThisRotation = UnityEngine.Quaternion.Inverse(m_links[i].transform.localRotation);
                switch (jt)
                {
                    case FeatherstoneJointType.Fixed:
                        mb.SetupFixed(i,m_links[i].mass,inertia,m_links[i].parentIndex, parentToThisRotation.ToBullet(), parentCOM2ThisPivotOffset.ToBullet(), thisPivotToThisCOMOffset.ToBullet(), false);
                        break;
                    case FeatherstoneJointType.Planar:
                        mb.SetupPlanar(i, m_links[i].mass, inertia, m_links[i].parentIndex, parentToThisRotation.ToBullet(), m_links[i].rotationAxis.ToBullet(), thisPivotToThisCOMOffset.ToBullet(), false);
                        break;
                    case FeatherstoneJointType.Prismatic:
                        mb.SetupPrismatic(i, m_links[i].mass, inertia, m_links[i].parentIndex, parentToThisRotation.ToBullet(), m_links[i].rotationAxis.ToBullet(), parentCOM2ThisPivotOffset.ToBullet(), thisPivotToThisCOMOffset.ToBullet(), false);
                        break;
                    case FeatherstoneJointType.Revolute:
                        mb.SetupRevolute(i, m_links[i].mass, inertia, m_links[i].parentIndex, parentToThisRotation.ToBullet(), m_links[i].rotationAxis.ToBullet(), parentCOM2ThisPivotOffset.ToBullet(), thisPivotToThisCOMOffset.ToBullet(), false);
                        break;
                    case FeatherstoneJointType.Spherical:
                        mb.SetupSpherical(i, m_links[i].mass, inertia, m_links[i].parentIndex, parentToThisRotation.ToBullet(), parentCOM2ThisPivotOffset.ToBullet(), thisPivotToThisCOMOffset.ToBullet(), false);
                        break;
                    default:
                        Debug.LogError("Invalid joint type for link " + m_links[i].name);
                        break;
                }
            }
            mb.CanSleep = true;
            mb.HasSelfCollision = false;
            mb.UseGyroTerm = true;

            bool damping = true;
            if (damping)
            {
                mb.LinearDamping = 0.1f;
                mb.AngularDamping = 0.9f;
            }
            else
            {
                mb.LinearDamping = 0;
                mb.AngularDamping = 0;
            }

            mb.FinalizeMultiDof();
            m_multibody = mb;
        }

        public void CreateColliders()
        {
            if (m_multibody == null)
            {
                Debug.LogError("Multibody must exist before creating colliders");
                return;
            }
            m_baseCollider = new MultiBodyLinkCollider(m_multibody, -1);
            m_baseCollider.UserObject = this;
            //todo validate that shape exists on base and all
            BCollisionShape shape = GetComponent<BCollisionShape>();
            m_baseCollider.CollisionShape = shape.GetCollisionShape();
            Matrix worldTrans = Matrix.RotationQuaternion(transform.rotation.ToBullet());
            worldTrans.Origin = transform.position.ToBullet();
            m_baseCollider.WorldTransform = worldTrans;
            bool isDynamic = (baseMass > 0 && !fixedBase);
            m_groupsIBelongTo = isDynamic ? (m_groupsIBelongTo) : (m_groupsIBelongTo | BulletSharp.CollisionFilterGroups.StaticFilter);
            m_collisionMask = isDynamic ? (m_collisionMask) : (m_collisionMask | BulletSharp.CollisionFilterGroups.StaticFilter);
            m_multibody.BaseCollider = m_baseCollider;
            
            for (int i = 0; i < m_links.Count; i++)
            {
                //create colliders
                MultiBodyLinkCollider col = m_links[i].m_linkCollider = new MultiBodyLinkCollider(m_multibody, m_links[i].index);
                col.UserObject = m_links[i];
                shape = m_links[i].GetComponent<BCollisionShape>();
                col.CollisionShape = shape.GetCollisionShape();
                worldTrans = Matrix.RotationQuaternion(m_links[i].transform.rotation.ToBullet());
                worldTrans.Origin = m_links[i].transform.position.ToBullet();
                col.WorldTransform = worldTrans;
                m_multibody.GetLink(i).Collider = col;
            }
        }

    //TODO handle random BMultiBodyLinks that skip in hierarchy
    //not using GetComponentsInChildren because the order in which links are discovered is important.
    bool GetLinksInChildrenAndNumber(Transform t, List<BMultiBodyLink> links, int parentIndex)
        {
            BMultiBodyLink mbl = t.GetComponent<BMultiBodyLink>();
            if (mbl != null)
            {
                links.Add(mbl);
                mbl.index = links.Count - 1;
                mbl.parentIndex = parentIndex;
            }
            for (int i = 0; i < t.childCount; i++)
            {
                int newParent;
                if (mbl == null)
                {
                    newParent = parentIndex;
                }
                else
                {
                    newParent = mbl.index;
                }
                if (!GetLinksInChildrenAndNumber(t.GetChild(i), links, newParent))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
