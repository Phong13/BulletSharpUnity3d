using UnityEngine;
using System.Collections;
using BulletSharp;

namespace BulletUnity
{

    /**
    This script is last in the script execution order. Its purpose is to ensure that StepSimulation is called after other scripts LateUpdate calls
    Do not add this script manually. The BPhysicsWorld will add it.
    */
    public class BPhysicsWorldLateHelper : MonoBehaviour
    {
        internal BPhysicsWorld m_physicsWorld;
        internal BDefaultCollisionHandler m_collisionEventHandler = new BDefaultCollisionHandler();
        public void RegisterCollisionCallbackListener(BCollisionObject.BICollisionCallbackEventHandler toBeAdded)
        {
            if (m_collisionEventHandler != null) m_collisionEventHandler.RegisterCollisionCallbackListener(toBeAdded);
        }

        public void DeregisterCollisionCallbackListener(BCollisionObject.BICollisionCallbackEventHandler toBeRemoved)
        {
            if (m_collisionEventHandler != null) m_collisionEventHandler.DeregisterCollisionCallbackListener(toBeRemoved);
        }

        internal DiscreteDynamicsWorld m_ddWorld;
        internal CollisionWorld m_world;
        internal int m__frameCount = 0;
        internal float m_lastInterpolationTime = 0;
        internal float m_elapsedBetweenFixedFrames = 0;
        internal float m_fixedTimeStep = 1f / 60f;

        void Awake()
        {
            Debug.Assert(m_fixedTimeStep == Time.fixedDeltaTime);
            m_lastInterpolationTime = UnityEngine.Time.time;
            m_elapsedBetweenFixedFrames = 0;
        }

        protected virtual void FixedUpdate()
        {
            if (m_ddWorld != null)
            {
                ///stepSimulation proceeds the simulation over 'timeStep', units in preferably in seconds.
                ///By default, Bullet will subdivide the timestep in constant substeps of each 'fixedTimeStep'.
                ///in order to keep the simulation real-time, the maximum number of substeps can be clamped to 'maxSubSteps'.
                ///You can disable subdividing the timestep/substepping by passing maxSubSteps=0 as second argument to stepSimulation, but in that case you have to keep the timeStep constant.
                Debug.Assert(m_elapsedBetweenFixedFrames < m_fixedTimeStep);
                float deltaTime = m_fixedTimeStep - m_elapsedBetweenFixedFrames;
                int numSteps = m_ddWorld.StepSimulation(deltaTime, 1, m_fixedTimeStep);
                Debug.Assert(numSteps == 1);
                m__frameCount += numSteps;
                m_lastInterpolationTime = UnityEngine.Time.time;
                m_elapsedBetweenFixedFrames = 0f;
                numUpdates = 0;
            }

            //collisions
            if (m_collisionEventHandler != null)
            {
                m_collisionEventHandler.OnPhysicsStep(m_world);
            }
        }

        int numUpdates = 0;

        //This is needed for rigidBody interpolation. The motion states will update the positions of the rigidbodies
        protected virtual void Update()
        {
            float deltaTime = UnityEngine.Time.time - m_lastInterpolationTime;
            
            // We want to ensure that each bullet sim step corresponds to exactly one Unity FixedUpdate timestep
            if (deltaTime > 0f && (m_elapsedBetweenFixedFrames + deltaTime) < m_fixedTimeStep)
            {
                m_elapsedBetweenFixedFrames += deltaTime;
                int numSteps = m_ddWorld.StepSimulation(deltaTime, 1, m_fixedTimeStep);
                m_lastInterpolationTime = UnityEngine.Time.time;
                numUpdates++;
            }
        }
    }
}
