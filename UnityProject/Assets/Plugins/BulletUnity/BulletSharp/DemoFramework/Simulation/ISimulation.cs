using BulletSharp;
using System;

namespace DemoFramework
{
    public interface ISimulation : IDisposable
    {
        CollisionConfiguration CollisionConfiguration { get; }
        CollisionDispatcher Dispatcher { get; }
        BroadphaseInterface Broadphase { get; }
        DiscreteDynamicsWorld World { get; }
    }
}
