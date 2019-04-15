using System.Collections;
using UnityEngine;

namespace BulletUnity
{
    /// <summary>
    /// Threading bullet allows to run bullet simulation steps at a higher frequency than Unity.
    /// When not threaded, bullet will interpolate automatically the transforms but the action callbacks won't be called regularly
    /// </summary>
    public class BTThreadedWorldHelper : BasePhysicsWorldHelper
    {

        private void Start()
        {
            StartCoroutine(DelayThreadStart());

        }

        private IEnumerator DelayThreadStart()
        {
            // wait 5 sec before starting
            yield return new WaitForSeconds(5);
            HighResolutionTimer timer = new HighResolutionTimer(FixedTimeStep * 1000);
            timer.UseHighPriorityThread = false;
            timer.Elapsed += (s, e) =>
            {
                PhysicsUpdate(e.Delay / 1000);
            };
            timer.Start();
            yield return null;
        }

        void PhysicsUpdate(double deltaTime)
        {
            if (m_ddWorld != null)
            {
                lock (m_ddWorld)
                {
                    float timeStep = (float)deltaTime * TimeStepRatio;
                    int maxStep = Mathf.CeilToInt(timeStep / FixedTimeStep);

                    m__frameCount += m_ddWorld.StepSimulation(timeStep, maxStep < 1 ? 1 : maxStep, FixedTimeStep);

                    if (m_collisionEventHandler != null)
                    {
                        m_collisionEventHandler.OnPhysicsStep(m_world);
                    }
                }
            }
        }

    }
}