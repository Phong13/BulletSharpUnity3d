using System;
using System.Runtime.InteropServices;
using System.Security;
using AOT;


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
		private IAction _actionInterface;
		private DynamicsWorld _world;

		[UnmanagedFunctionPointer(Native.Conv), SuppressUnmanagedCodeSecurity]
		private delegate void DebugDrawUnmanagedDelegate(IntPtr iaPtrThis, IntPtr debugDrawer);
		[UnmanagedFunctionPointer(Native.Conv), SuppressUnmanagedCodeSecurity]
		private delegate void UpdateActionUnmanagedDelegate(IntPtr iaPtrThis, IntPtr collisionWorld, float deltaTimeStep);

		private DebugDrawUnmanagedDelegate _debugDraw;
		private UpdateActionUnmanagedDelegate _updateAction;

		public ActionInterfaceWrapper(IAction actionInterface, DynamicsWorld world)
		{
			_actionInterface = actionInterface;
			_world = world;

			_debugDraw = new DebugDrawUnmanagedDelegate(DebugDrawUnmanaged);
			_updateAction = new UpdateActionUnmanagedDelegate(UpdateActionUnmanaged);
            GCHandle handle = GCHandle.Alloc(this, GCHandleType.Normal);

            _native = btActionInterfaceWrapper_new(
				Marshal.GetFunctionPointerForDelegate(_debugDraw),
				Marshal.GetFunctionPointerForDelegate(_updateAction),
                GCHandle.ToIntPtr(handle));
            //UnityEngine.Debug.Log("Intptr" + bgActionInterface_getManagedWrapperPntr(_native));
        }

        [MonoPInvokeCallback(typeof(DebugDrawUnmanagedDelegate))]
        private static void DebugDrawUnmanaged(IntPtr iaPtrThis, IntPtr debugDrawer)
		{
            ActionInterfaceWrapper ai = GCHandle.FromIntPtr(iaPtrThis).Target as ActionInterfaceWrapper;
            ai._actionInterface.DebugDraw(DebugDraw.GetManaged(debugDrawer));
		}

        [MonoPInvokeCallback(typeof(UpdateActionUnmanagedDelegate))]
        private static void UpdateActionUnmanaged(IntPtr iaPtrThis, IntPtr collisionWorld, float deltaTimeStep)
		{
            ActionInterfaceWrapper ai = GCHandle.FromIntPtr(iaPtrThis).Target as ActionInterfaceWrapper;
            ai._actionInterface.UpdateAction(ai._world, deltaTimeStep);
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
        static extern IntPtr btActionInterfaceWrapper_new(IntPtr debugDrawCallback, IntPtr updateActionCallback, IntPtr thisObj);

        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr bgActionInterface_getManagedWrapperPntr(IntPtr obj);

        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btActionInterface_delete(IntPtr obj);
    }
}
