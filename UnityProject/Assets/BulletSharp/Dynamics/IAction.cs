using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
    public interface IAction
    {
        void DebugDraw(IDebugDraw debugDrawer);
        void UpdateAction(CollisionWorld collisionWorld, float deltaTimeStep);
    }

    internal class ActionInterfaceWrapper : IDisposable
	{
		internal IntPtr _native;
        IAction _actionInterface;
        DynamicsWorld _world;

        [UnmanagedFunctionPointerAttribute(Native.Conv), SuppressUnmanagedCodeSecurity]
        delegate void DebugDrawUnmanagedDelegate(IntPtr debugDrawer);
        [UnmanagedFunctionPointerAttribute(Native.Conv), SuppressUnmanagedCodeSecurity]
        delegate void UpdateActionUnmanagedDelegate(IntPtr collisionWorld, float deltaTimeStep);

        DebugDrawUnmanagedDelegate _debugDraw;
        UpdateActionUnmanagedDelegate _updateAction;

        public ActionInterfaceWrapper(IAction actionInterface, DynamicsWorld world)
        {
            _actionInterface = actionInterface;
            _world = world;

            _debugDraw = new DebugDrawUnmanagedDelegate(DebugDrawUnmanaged);
            _updateAction = new UpdateActionUnmanagedDelegate(UpdateActionUnmanaged);

            _native = btActionInterfaceWrapper_new(
                Marshal.GetFunctionPointerForDelegate(_debugDraw),
                Marshal.GetFunctionPointerForDelegate(_updateAction));
        }

        private void DebugDrawUnmanaged(IntPtr debugDrawer)
        {
            _actionInterface.DebugDraw(DebugDraw.GetManaged(debugDrawer));
        }

        private void UpdateActionUnmanaged(IntPtr collisionWorld, float deltaTimeStep)
        {
            _actionInterface.UpdateAction(_world, deltaTimeStep);
        }

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (_native != IntPtr.Zero)
			{
				btActionInterface_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~ActionInterfaceWrapper()
		{
			Dispose(false);
		}

        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btActionInterfaceWrapper_new(IntPtr debugDrawCallback, IntPtr updateActionCallback);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btActionInterface_delete(IntPtr obj);
    }
}
