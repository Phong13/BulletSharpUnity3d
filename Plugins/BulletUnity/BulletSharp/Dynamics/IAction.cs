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
        IAction _actionInterface;
        DynamicsWorld _world;

        [UnmanagedFunctionPointerAttribute(Native.Conv), SuppressUnmanagedCodeSecurity]
        delegate void DebugDrawUnmanagedDelegate(IntPtr iaPtrThis, IntPtr debugDrawer);
        [UnmanagedFunctionPointerAttribute(Native.Conv), SuppressUnmanagedCodeSecurity]
        delegate void UpdateActionUnmanagedDelegate(IntPtr iaPtrThis, IntPtr collisionWorld, float deltaTimeStep);

        DebugDrawUnmanagedDelegate _debugDraw;
        UpdateActionUnmanagedDelegate _updateAction; 

        public ActionInterfaceWrapper(IAction actionInterface, DynamicsWorld world)
        {
            _actionInterface = actionInterface;
            _world = world;

            _debugDraw = new DebugDrawUnmanagedDelegate(DebugDrawUnmanaged);
            _updateAction = new UpdateActionUnmanagedDelegate(UpdateActionUnmanaged);
			GCHandle handle = GCHandle.Alloc(this, GCHandleType.Normal);
			//UnityEngine.Debug.Log("Created " + GCHandle.ToIntPtr(handle).ToInt64());
            _native = btActionInterfaceWrapper_new(
                Marshal.GetFunctionPointerForDelegate(_debugDraw),
                Marshal.GetFunctionPointerForDelegate(_updateAction),
				GCHandle.ToIntPtr(handle));
			//UnityEngine.Debug.Log("Intptr" + bgActionInterface_getManagedWrapperPntr(_native));
        }

		//changed these so they are static fuctions and have MonoPInvokeCallback decorator so they work from iOS (uses AOT)
		[MonoPInvokeCallback(typeof(DebugDrawUnmanagedDelegate))]
        private static void DebugDrawUnmanaged(IntPtr iaPtrThis, IntPtr debugDrawer)
        {
			//UnityEngine.Debug.Log("callback dd yes!");
			ActionInterfaceWrapper ai = GCHandle.FromIntPtr(iaPtrThis).Target as ActionInterfaceWrapper;
            ai._actionInterface.DebugDraw(DebugDraw.GetManaged(debugDrawer));
        }

		//changed these so they are static fuctions and have MonoPInvokeCallback decorator so they work from iOS (uses AOT)
		[MonoPInvokeCallback(typeof(UpdateActionUnmanagedDelegate))]
        private static void UpdateActionUnmanaged(IntPtr iaPtrThis, IntPtr collisionWorld, float deltaTimeStep)
        {
			//UnityEngine.Debug.Log("Callback yes!! " + iaPtrThis.ToInt64());
			ActionInterfaceWrapper ai = GCHandle.FromIntPtr(iaPtrThis).Target as ActionInterfaceWrapper;
			ai._actionInterface.UpdateAction(ai._world, deltaTimeStep);
        }

		public void Dispose()
		{
			//UnityEngine.Debug.Log("Dispose 1");
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			//UnityEngine.Debug.Log("Dispose 2");
			if (_native != IntPtr.Zero)
			{
				//UnityEngine.Debug.Log("Dispose 3");
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
