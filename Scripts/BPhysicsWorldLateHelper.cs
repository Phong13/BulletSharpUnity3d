using UnityEngine;

namespace BulletUnity
{
    /**
    This script is last in the script execution order. Its purpose is to ensure that StepSimulation is called after other scripts LateUpdate calls
    Do not add this script manually. The BPhysicsWorld will add it.
    */
    public class BPhysicsWorldLateHelper : BasePhysicsWorldHelper
    {

        internal float m_lastInterpolationTime = 0;
        internal float m_elapsedBetweenFixedFrames = 0;

        protected virtual void Awake()
        {
            m_lastInterpolationTime = Time.time;
            m_elapsedBetweenFixedFrames = 0;
        }

        protected virtual void FixedUpdate()
        {
            if (m_ddWorld != null)
            {
                /*  stepSimulation proceeds the simulation over 'timeStep', units in preferably in seconds.
                    By default, Bullet will subdivide the timestep in constant substeps of each 'fixedTimeStep'.
                    in order to keep the simulation real-time, the maximum number of substeps can be clamped to 'maxSubSteps'.
                    You can disable subdividing the timestep/substepping by passing maxSubSteps=0 as second argument to stepSimulation, but in that case you have to keep the timeStep constant. */
                Debug.Assert(m_elapsedBetweenFixedFrames < FixedTimeStep);
                float deltaTime = FixedTimeStep - m_elapsedBetweenFixedFrames;
                int numSteps = m_ddWorld.StepSimulation(deltaTime, 1, FixedTimeStep);
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
            float deltaTime = Time.time - m_lastInterpolationTime;

            // We want to ensure that each bullet sim step corresponds to exactly one Unity FixedUpdate timestep
            if (deltaTime > 0f && (m_elapsedBetweenFixedFrames + deltaTime) < FixedTimeStep)
            {
                m_elapsedBetweenFixedFrames += deltaTime;
                int numSteps = m_ddWorld.StepSimulation(deltaTime, 1, FixedTimeStep);
                m_lastInterpolationTime = Time.time;
                numUpdates++;
            }
        }
    }
}
