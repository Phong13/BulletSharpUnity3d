using BulletSharp;
using System;

namespace DemoFramework
{
    public interface ISimulationCustomUpdate : IDisposable
    {
        void OnUpdate();
    }
}
