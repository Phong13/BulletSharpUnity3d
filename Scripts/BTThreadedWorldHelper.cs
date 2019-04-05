using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace BulletUnity
{
    public class BTThreadedWorldHelper : BasePhysicsWorldHelper
    {

        readonly Stopwatch stopwatch = new Stopwatch();

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
                PhysicsUpdate(stopwatch.Elapsed);
                stopwatch.Restart();
            };
            stopwatch.Start();
            timer.Start();
            yield return null;
        }

        void PhysicsUpdate(TimeSpan deltaTime)
        {
            if (m_ddWorld != null)
            {
                lock (m_ddWorld)
                {
                    float fixedDeltaTime = (float)deltaTime.TotalSeconds;
                    m__frameCount += m_ddWorld.StepSimulation(fixedDeltaTime, 1, FixedTimeStep);

                    if (m_collisionEventHandler != null)
                    {
                        m_collisionEventHandler.OnPhysicsStep(m_world);
                    }
                }
            }
        }

    }
}