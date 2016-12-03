using System;
using UnityEngine;
using System.Collections;
using BulletSharp;
using BulletSharp.SoftBody;
using System.Collections.Generic;
using BulletUnity.Debugging;


namespace BulletUnity
{
    public class BPhysicsWorld : MonoBehaviour, IDisposable
    {

        public enum WorldType
        {
            CollisionOnly,
            RigidBodyDynamics,
            MultiBodyWorld, //for FeatherStone forward dynamics I think
            SoftBodyAndRigidBody,
        }

        public enum CollisionConfType
        {
            DefaultDynamicsWorldCollisionConf,
            SoftBodyRigidBodyCollisionConf,
        }

        public enum BroadphaseType
        {
            DynamicAABBBroadphase,
            Axis3SweepBroadphase,
            Axis3SweepBroadphase_32bit,
            SimpleBroadphase,
        }

        protected static BPhysicsWorld singleton;
        protected static bool _isDisposed = false;
        const int axis3SweepMaxProxies = 32766;

        public static BPhysicsWorld Get()
        {
            if (singleton == null && !_isDisposed)
            {
                if (!Application.isPlaying)
                {
                    return null;
                }
                BPhysicsWorld[] ws = FindObjectsOfType<BPhysicsWorld>();
                if (ws.Length == 1)
                {
                    singleton = ws[0];
                }
                else if (ws.Length == 0)
                {
                    Debug.LogError("Need to add a dynamics world to the scene");
                }
                else {
                    Debug.LogError("Found more than one dynamics world.");
                    singleton = ws[0];
                    for (int i = 1; i < ws.Length; i++)
                    {
                        GameObject.Destroy(ws[i].gameObject);
                    }
                }
                singleton._InitializePhysicsWorld();
            }
            //if (singleton.m_world == null && !singleton.isDisposed) singleton._InitializePhysicsWorld();
            return singleton;
        }

        [SerializeField]
        protected DebugDrawModes _debugDrawMode = DebugDrawModes.DrawWireframe;
        public DebugDrawModes DebugDrawMode
        {
            get { return _debugDrawMode; }
            set
            {
                _debugDrawMode = value;
                if (_doDebugDraw && m_world != null && m_world.DebugDrawer != null)
                {
                    m_world.DebugDrawer.DebugMode = value;
                }
            }
        }

        [SerializeField]
        protected bool _doDebugDraw = false;
        public bool DoDebugDraw
        {
            get { return _doDebugDraw; }
            set
            {
                if (_doDebugDraw != value && m_world != null)
                {
                    if (value == true)
                    {
                        DebugDrawUnity db = new DebugDrawUnity();
                        db.DebugMode = _debugDrawMode;
                        m_world.DebugDrawer = db;
                    }
                    else {
                        IDebugDraw db = m_world.DebugDrawer;
                        if (db != null && db is IDisposable)
                        {
                            ((IDisposable)db).Dispose();
                        }
                        m_world.DebugDrawer = null;
                    }
                }
                _doDebugDraw = value;
            }
        }

        [SerializeField]
        WorldType m_worldType = WorldType.RigidBodyDynamics;
        public WorldType worldType
        {
            get { return m_worldType; }
            set
            {
                if (value != m_worldType && m_world != null)
                {
                    Debug.LogError("Can't modify a Physics World after simulation has started");
                    return;
                }
                m_worldType = value;
            }
        }

        [SerializeField]
        CollisionConfType m_collisionType = CollisionConfType.DefaultDynamicsWorldCollisionConf;
        public CollisionConfType collisionType
        {
            get { return m_collisionType; }
            set
            {
                if (value != m_collisionType && m_world != null)
                {
                    Debug.LogError("Can't modify a Physics World after simulation has started");
                    return;
                }
                m_collisionType = value;
            }
        }

        [SerializeField]
        BroadphaseType m_broadphaseType = BroadphaseType.DynamicAABBBroadphase;
        public BroadphaseType broadphaseType
        {
            get { return m_broadphaseType; }
            set
            {
                if (value != m_broadphaseType && m_world != null)
                {
                    Debug.LogError("Can't modify a Physics World after simulation has started");
                    return;
                }
                m_broadphaseType = value;
            }
        }

        [SerializeField]
        Vector3 m_axis3SweepBroadphaseMin = new Vector3(-1000f, -1000f, -1000f);
        public Vector3 axis3SweepBroadphaseMin
        {
            get { return m_axis3SweepBroadphaseMin; }
            set
            {
                if (value != m_axis3SweepBroadphaseMin && m_world != null)
                {
                    Debug.LogError( "Can't modify a Physics World after simulation has started");
                    return;
                }
                m_axis3SweepBroadphaseMin = value;
            }
        }

        [SerializeField]
        Vector3 m_axis3SweepBroadphaseMax = new Vector3(1000f, 1000f, 1000f);
        public Vector3 axis3SweepBroadphaseMax
        {
            get { return m_axis3SweepBroadphaseMax; }
            set
            {
                if (value != m_axis3SweepBroadphaseMax && m_world != null)
                {
                    Debug.LogError( "Can't modify a Physics World after simulation has started");
                    return;
                }
                m_axis3SweepBroadphaseMax = value;
            }
        }

        [SerializeField]
        Vector3 m_gravity = new Vector3(0f, -9.8f, 0f);
        public Vector3 gravity
        {
            get { return m_gravity; }
            set
            {
                if (_ddWorld != null)
                {
                    BulletSharp.Math.Vector3 grav = value.ToBullet();
                    _ddWorld.SetGravity(ref grav);
                }
                m_gravity = value;
            }
        }

        [SerializeField]
        float m_fixedTimeStep = 1f / 60f;
        public float fixedTimeStep
        {
            get
            {
                return m_fixedTimeStep;
            }
            set
            {
                if (lateUpdateHelper != null)
                {
                    lateUpdateHelper.m_fixedTimeStep = value;
                }
                m_fixedTimeStep = value;
            }
        }

        [SerializeField]
        int m_maxSubsteps = 3;
        public int maxSubsteps
        {
            get
            {
                return m_maxSubsteps;
            }
            set
            {
                if (lateUpdateHelper != null)
                {
                    lateUpdateHelper.m_maxSubsteps = value;
                }
                m_maxSubsteps = value;
            }
        }

        public BDebug.DebugType debugType;

        /*
        [SerializeField]
        bool m_doCollisionCallbacks = true;
        public bool doCollisionCallbacks
        {
            get { return m_doCollisionCallbacks; }
            set { m_doCollisionCallbacks = value; }
        }
        */

        BPhysicsWorldLateHelper lateUpdateHelper;

        CollisionConfiguration CollisionConf;
        CollisionDispatcher Dispatcher;
        BroadphaseInterface Broadphase;
        SoftBodyWorldInfo softBodyWorldInfo;
        SequentialImpulseConstraintSolver Solver;
        GhostPairCallback ghostPairCallback = null;
        ulong sequentialImpulseConstraintSolverRandomSeed = 12345;



        CollisionWorld m_world;
        public CollisionWorld world
        {
            get { return m_world; }
            set { m_world = value; }
        }

        private DiscreteDynamicsWorld _ddWorld; // convenience variable so we arn't typecasting all the time.

        public int frameCount
        {
            get
            {
                if (lateUpdateHelper != null)
                {
                    return lateUpdateHelper.m__frameCount;
                } else
                {
                    return -1;
                }
            }
        }

        public float timeStr;

        public void RegisterCollisionCallbackListener(BCollisionObject.BICollisionCallbackEventHandler toBeAdded)
        {
            if (lateUpdateHelper != null) lateUpdateHelper.RegisterCollisionCallbackListener(toBeAdded);
        }

        public void DeregisterCollisionCallbackListener(BCollisionObject.BICollisionCallbackEventHandler toBeRemoved)
        {
            if (lateUpdateHelper != null) lateUpdateHelper.DeregisterCollisionCallbackListener(toBeRemoved);
        }

        public void OnDrawGizmos()
        {
            if (_doDebugDraw && m_world != null)
            {
                m_world.DebugDrawWorld();
            }
        }

        //It is critical that Awake be called before any other scripts call BPhysicsWorld.Get()
        //Set this script and any derived classes very early in script execution order.
        protected virtual void Awake()
        {
            _isDisposed = false;
            singleton = BPhysicsWorld.Get();
        }

        protected virtual void OnDestroy()
        {
            BDebug.Log(debugType, "Destroying Physics World");
            Dispose(false);
        }

        public bool isDisposed
        {
            get { return _isDisposed; }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public bool AddAction(IAction action)
        {
            if (!_isDisposed)
            {
                if (m_worldType < WorldType.RigidBodyDynamics)
                {
                    Debug.LogError("World type must not be collision only");
                }
                else
                {
                    ((DynamicsWorld)world).AddAction(action);
                }
                return true;
            }
            return false;
        }

        public void RemoveAction(IAction action)
        {
            if (!_isDisposed)
            {
                if (m_worldType < WorldType.RigidBodyDynamics)
                {
                    Debug.LogError( "World type must not be collision only");
                }
                ((DiscreteDynamicsWorld)m_world).RemoveAction(action);
            }
        }

        public bool AddCollisionObject(BCollisionObject co)
        {
            if (co is BRigidBody)
            {
                return AddRigidBody((BRigidBody) co);
            }
            if (co is BSoftBody)
            {
                return AddSoftBody((BSoftBody)co);
            }

            if (!_isDisposed)
            {
                if (debugType >= BDebug.DebugType.Debug) Debug.LogFormat("Adding collision object {0} to world", co);
                if (co._BuildCollisionObject())
                {
                    m_world.AddCollisionObject(co.GetCollisionObject(), co.groupsIBelongTo, co.collisionMask);
                    co.isInWorld = true;
                    if (ghostPairCallback == null && co is BGhostObject && world is DynamicsWorld)
                    {
                        ghostPairCallback = new GhostPairCallback();
                        ((DynamicsWorld)world).PairCache.SetInternalGhostPairCallback(ghostPairCallback);
                    }
                    if (co is BCharacterController && world is DynamicsWorld)
                    {
                        AddAction(((BCharacterController)co).GetKinematicCharacterController());
                    }
                    
                }
                return true;
            }
            return false;
        }

        public void RemoveCollisionObject(BCollisionObject co)
        {
            if (co is BRigidBody)
            {
                RemoveRigidBody((RigidBody)co.GetCollisionObject());
                return;
            }
            if (co is BSoftBody)
            {
                RemoveSoftBody((SoftBody)co.GetCollisionObject());
                return;
            }
            if (!_isDisposed)
            {
                if (co is BCharacterController && world is DynamicsWorld)
                {
                    RemoveAction(((BCharacterController)co).GetKinematicCharacterController());
                }
                if (debugType >= BDebug.DebugType.Debug) Debug.LogFormat("Removing collisionObject {0} from world", co);
                m_world.RemoveCollisionObject(co.GetCollisionObject());
                co.isInWorld = false;
            }
        }

        public bool AddRigidBody(BRigidBody rb)
        {
            if (!_isDisposed)
            {
                if (m_worldType < WorldType.RigidBodyDynamics)
                {
                    Debug.LogError( "World type must not be collision only");
                }
                if (debugType >= BDebug.DebugType.Debug) Debug.LogFormat("Adding rigidbody {0} to world", rb);
                if (rb._BuildCollisionObject())
                {
                    ((DiscreteDynamicsWorld)m_world).AddRigidBody((RigidBody)rb.GetCollisionObject(), rb.groupsIBelongTo, rb.collisionMask);
                    rb.isInWorld = true;
                }
                return true;
            }
            return false;
        }

        public void RemoveRigidBody(BulletSharp.RigidBody rb)
        {
            if (!_isDisposed)
            {
                if (m_worldType < WorldType.RigidBodyDynamics)
                {
                    Debug.LogError( "World type must not be collision only");
                }
                if (debugType >= BDebug.DebugType.Debug) Debug.LogFormat("Removing rigidbody {0} from world", rb.UserObject);
                ((DiscreteDynamicsWorld)m_world).RemoveRigidBody(rb);
                if (rb.UserObject is BCollisionObject) ((BCollisionObject)rb.UserObject).isInWorld = false;
            }
        }

        public bool AddConstraint(BTypedConstraint c)
        {
            if (!_isDisposed)
            {
                if (m_worldType < WorldType.RigidBodyDynamics)
                {
                    Debug.LogError( "World type must not be collision only");
                    return false;
                }
                if (debugType >= BDebug.DebugType.Debug) Debug.LogFormat("Adding constraint {0} to world", c);
                if (c._BuildConstraint())
                {
                    ((DiscreteDynamicsWorld)m_world).AddConstraint(c.GetConstraint(), c.disableCollisionsBetweenConstrainedBodies);
                    c.m_isInWorld = true;
                }
                return true;
            }
            return false;
        }

        public void RemoveConstraint(BulletSharp.TypedConstraint c)
        {
            if (!_isDisposed)
            {
                if (m_worldType < WorldType.RigidBodyDynamics)
                {
                    Debug.LogError( "World type must not be collision only");
                }
                if (debugType >= BDebug.DebugType.Debug) Debug.LogFormat("Removing constraint {0} from world", c.Userobject);
                ((DiscreteDynamicsWorld)m_world).RemoveConstraint(c);
                if (c.Userobject is BTypedConstraint) ((BTypedConstraint)c.Userobject).m_isInWorld = false;
            }
        }

        public bool AddSoftBody(BSoftBody softBody)
        {
            if (!(m_world is BulletSharp.SoftBody.SoftRigidDynamicsWorld))
            {
                if (debugType <= BDebug.DebugType.Trace) Debug.LogErrorFormat("The Physics World must be a BSoftBodyWorld for adding soft bodies");
                return false;
            }
            if (!_isDisposed)
            {
                if (debugType >= BDebug.DebugType.Debug) Debug.LogFormat("Adding softbody {0} to world", softBody);
                if (softBody._BuildCollisionObject())
                {
                    ((BulletSharp.SoftBody.SoftRigidDynamicsWorld)m_world).AddSoftBody((SoftBody)softBody.GetCollisionObject());
                    softBody.isInWorld = true;
                }
                return true;
            }
            return false;
        }

        public void RemoveSoftBody(BulletSharp.SoftBody.SoftBody softBody)
        {
            if (!_isDisposed && m_world is BulletSharp.SoftBody.SoftRigidDynamicsWorld)
            {
                if (debugType >= BDebug.DebugType.Debug) Debug.LogFormat("Removing softbody {0} from world", softBody.UserObject);
                ((BulletSharp.SoftBody.SoftRigidDynamicsWorld)m_world).RemoveSoftBody(softBody);
                if (softBody.UserObject is BCollisionObject) ((BCollisionObject)softBody.UserObject).isInWorld = false;
            }
        }

        protected virtual void _InitializePhysicsWorld()
        {
            _isDisposed = false;
            CreatePhysicsWorld(out m_world, out CollisionConf, out Dispatcher, out Broadphase, out Solver, out softBodyWorldInfo);
            if (m_world is DiscreteDynamicsWorld)
            {
                _ddWorld = (DiscreteDynamicsWorld)m_world;
            }
            //Add a BPhysicsWorldLateHelper component to call FixedUpdate
            lateUpdateHelper = GetComponent<BPhysicsWorldLateHelper>();
            if (lateUpdateHelper == null)
            {
                lateUpdateHelper = gameObject.AddComponent<BPhysicsWorldLateHelper>();
            }
            lateUpdateHelper.m_world = world;
            lateUpdateHelper.m_ddWorld = _ddWorld;
            lateUpdateHelper.m_physicsWorld = this;
            lateUpdateHelper.m__frameCount = 0;
            lateUpdateHelper.m_lastSimulationStepTime = 0;
        }

        /*
        Does not set any local variables. Is safe to use to create duplicate physics worlds for independant simulation.
        */
        public bool CreatePhysicsWorld( out CollisionWorld world, 
                                        out CollisionConfiguration collisionConfig, 
                                        out CollisionDispatcher dispatcher, 
                                        out BroadphaseInterface broadphase, 
                                        out SequentialImpulseConstraintSolver solver,
                                        out SoftBodyWorldInfo softBodyWorldInfo)
        {
            bool success = true;
            if (m_worldType == WorldType.SoftBodyAndRigidBody && m_collisionType == CollisionConfType.DefaultDynamicsWorldCollisionConf)
            {
                Debug.LogError( "For World Type = SoftBodyAndRigidBody collisionType must be collisionType=SoftBodyRigidBodyCollisionConf. Switching");
                m_collisionType = CollisionConfType.SoftBodyRigidBodyCollisionConf;
                success = false;
            }

            collisionConfig = null;
            if (m_collisionType == CollisionConfType.DefaultDynamicsWorldCollisionConf)
            {
                collisionConfig = new DefaultCollisionConfiguration();
            }
            else if (m_collisionType == CollisionConfType.SoftBodyRigidBodyCollisionConf)
            {
                collisionConfig = new SoftBodyRigidBodyCollisionConfiguration();
            }

            dispatcher = new CollisionDispatcher(collisionConfig);

            if (m_broadphaseType == BroadphaseType.DynamicAABBBroadphase)
            {
                broadphase = new DbvtBroadphase();
            }
            else if (m_broadphaseType == BroadphaseType.Axis3SweepBroadphase)
            {
                broadphase = new AxisSweep3(m_axis3SweepBroadphaseMin.ToBullet(), m_axis3SweepBroadphaseMax.ToBullet(), axis3SweepMaxProxies);
            }
            else if (m_broadphaseType == BroadphaseType.Axis3SweepBroadphase_32bit)
            {
                broadphase = new AxisSweep3_32Bit(m_axis3SweepBroadphaseMin.ToBullet(), m_axis3SweepBroadphaseMax.ToBullet(), axis3SweepMaxProxies);
            }
            else
            {
                broadphase = null;
            }
            world = null;
            softBodyWorldInfo = null;
            solver = null;
            if (m_worldType == WorldType.CollisionOnly)
            {
                world = new CollisionWorld(dispatcher, broadphase, collisionConfig);
            }
            else if (m_worldType == WorldType.RigidBodyDynamics)
            {
                world = new DiscreteDynamicsWorld(dispatcher, broadphase, null, collisionConfig);
            }
            else if (m_worldType == WorldType.MultiBodyWorld)
            {
                world = new MultiBodyDynamicsWorld(dispatcher, broadphase, null, collisionConfig);
            }
            else if (m_worldType == WorldType.SoftBodyAndRigidBody)
            {
                Solver = new SequentialImpulseConstraintSolver();
                Solver.RandSeed = sequentialImpulseConstraintSolverRandomSeed;
                m_world = new SoftRigidDynamicsWorld(Dispatcher, Broadphase, Solver, CollisionConf);
                _ddWorld = (DiscreteDynamicsWorld)m_world;
                SoftRigidDynamicsWorld _sworld = (SoftRigidDynamicsWorld)m_world;

                m_world.DispatchInfo.EnableSpu = true;
                _sworld.WorldInfo.SparseSdf.Initialize();
                _sworld.WorldInfo.SparseSdf.Reset();
                _sworld.WorldInfo.AirDensity = 1.2f;
                _sworld.WorldInfo.WaterDensity = 0;
                _sworld.WorldInfo.WaterOffset = 0;
                _sworld.WorldInfo.WaterNormal = BulletSharp.Math.Vector3.Zero;
                _sworld.WorldInfo.Gravity = m_gravity.ToBullet();
            }
            if (world is DiscreteDynamicsWorld)
            {
                ((DiscreteDynamicsWorld)world).Gravity = m_gravity.ToBullet();
            }
            if (_doDebugDraw)
            {
                DebugDrawUnity db = new DebugDrawUnity();
                db.DebugMode = _debugDrawMode;
                world.DebugDrawer = db;
            }
            return success;
        }

        protected void Dispose(bool disposing)
        {
            if (debugType >= BDebug.DebugType.Debug) Debug.Log("BDynamicsWorld Disposing physics.");

            if (lateUpdateHelper != null)
            {
                lateUpdateHelper.m_ddWorld = null;
                lateUpdateHelper.m_world = null;
            }
            if (m_world != null)
            {
                //remove/dispose constraints
                int i;
                if (_ddWorld != null)
                {
                    if (debugType >= BDebug.DebugType.Debug) Debug.LogFormat("Removing Constraints {0}", _ddWorld.NumConstraints);
                    for (i = _ddWorld.NumConstraints - 1; i >= 0; i--)
                    {
                        TypedConstraint constraint = _ddWorld.GetConstraint(i);
                        _ddWorld.RemoveConstraint(constraint);
                        if (constraint.Userobject is BTypedConstraint) ((BTypedConstraint)constraint.Userobject).m_isInWorld = false;
                        if (debugType >= BDebug.DebugType.Debug) Debug.LogFormat("Removed Constaint {0}", constraint.Userobject);
                        constraint.Dispose();
                    }
                }

                if (debugType >= BDebug.DebugType.Debug) Debug.LogFormat("Removing Collision Objects {0}", _ddWorld.NumCollisionObjects);
                //remove the rigidbodies from the dynamics world and delete them
                for (i = m_world.NumCollisionObjects - 1; i >= 0; i--)
                {
                    CollisionObject obj = m_world.CollisionObjectArray[i];
                    RigidBody body = obj as RigidBody;
                    if (body != null && body.MotionState != null)
                    {
                        Debug.Assert(body.NumConstraintRefs == 0, "Rigid body still had constraints");
                        body.MotionState.Dispose();
                    }
                    m_world.RemoveCollisionObject(obj);
                    if (obj.UserObject is BCollisionObject) ((BCollisionObject)obj.UserObject).isInWorld = false;
                    if (debugType >= BDebug.DebugType.Debug) Debug.LogFormat("Removed CollisionObject {0}", obj.UserObject);
                    obj.Dispose();
                }

                if (m_world.DebugDrawer != null)
                {
                    if (m_world.DebugDrawer is IDisposable)
                    {
                        IDisposable dis = (IDisposable)m_world.DebugDrawer;
                        dis.Dispose();
                    }
                }

                m_world.Dispose();
                Broadphase.Dispose();
                Dispatcher.Dispose();
                CollisionConf.Dispose();
                _ddWorld = null;
                m_world = null;
            }

            if (Broadphase != null)
            {
                Broadphase.Dispose();
                Broadphase = null;
            }
            if (Dispatcher != null)
            {
                Dispatcher.Dispose();
                Dispatcher = null;
            }
            if (CollisionConf != null)
            {
                CollisionConf.Dispose();
                CollisionConf = null;
            }
            if (Solver != null)
            {
                Solver.Dispose();
                Solver = null;
            }
            if (softBodyWorldInfo != null)
            {
                softBodyWorldInfo.Dispose();
                softBodyWorldInfo = null;
            }
            _isDisposed = true;
            singleton = null;
        }
    }

    public class BDefaultCollisionHandler
    {
        HashSet<BCollisionObject.BICollisionCallbackEventHandler> collisionCallbackListeners = new HashSet<BCollisionObject.BICollisionCallbackEventHandler>();

        public void RegisterCollisionCallbackListener(BCollisionObject.BICollisionCallbackEventHandler toBeAdded)
        {
            collisionCallbackListeners.Add(toBeAdded);
        }

        public void DeregisterCollisionCallbackListener(BCollisionObject.BICollisionCallbackEventHandler toBeRemoved)
        {
            collisionCallbackListeners.Remove(toBeRemoved);
        }

        public void OnPhysicsStep(CollisionWorld world)
        {
            Dispatcher dispatcher = world.Dispatcher;
            int numManifolds = dispatcher.NumManifolds;
            for (int i = 0; i < numManifolds; i++)
            {
                PersistentManifold contactManifold = dispatcher.GetManifoldByIndexInternal(i);
                CollisionObject a = contactManifold.Body0;
                CollisionObject b = contactManifold.Body1;
                if (a is CollisionObject && a.UserObject is BCollisionObject && ((BCollisionObject)a.UserObject).collisionCallbackEventHandler != null)
                {
                    ((BCollisionObject)a.UserObject).collisionCallbackEventHandler.OnVisitPersistentManifold(contactManifold);
                }
                if (b is CollisionObject && b.UserObject is BCollisionObject && ((BCollisionObject)b.UserObject).collisionCallbackEventHandler != null)
                {
                    ((BCollisionObject)b.UserObject).collisionCallbackEventHandler.OnVisitPersistentManifold(contactManifold);
                }
            }
            foreach (BCollisionObject.BICollisionCallbackEventHandler coeh in collisionCallbackListeners)
            {
                if (coeh != null) coeh.OnFinishedVisitingManifolds();
            }
        }
    }
}
