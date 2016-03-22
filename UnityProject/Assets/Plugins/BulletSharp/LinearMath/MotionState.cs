using System;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.Math;
using AOT;

namespace BulletSharp
{
	public abstract class MotionState : IDisposable
	{
		internal IntPtr _native;

        [UnmanagedFunctionPointer(Native.Conv), SuppressUnmanagedCodeSecurity]
        delegate void GetWorldTransformUnmanagedDelegate(IntPtr managedMotionStatePtr, out Matrix worldTrans);
        [UnmanagedFunctionPointer(Native.Conv), SuppressUnmanagedCodeSecurity]
        delegate void SetWorldTransformUnmanagedDelegate(IntPtr managedMotionStatePtr, ref Matrix worldTrans);

        GetWorldTransformUnmanagedDelegate _getWorldTransform;
        SetWorldTransformUnmanagedDelegate _setWorldTransform;

		internal MotionState(IntPtr native)
		{
			_native = native;
		}

        protected MotionState()
        {
            _getWorldTransform = new GetWorldTransformUnmanagedDelegate(GetWorldTransformUnmanaged);
            _setWorldTransform = new SetWorldTransformUnmanagedDelegate(SetWorldTransformUnmanaged);

			GCHandle handle = GCHandle.Alloc(this, GCHandleType.Normal);
			//UnityEngine.Debug.Log("Created MoState" + GCHandle.ToIntPtr(handle).ToInt64());
            _native = btMotionStateWrapper_new(
                Marshal.GetFunctionPointerForDelegate(_getWorldTransform),
                Marshal.GetFunctionPointerForDelegate(_setWorldTransform),
				GCHandle.ToIntPtr(handle));
        }

		[MonoPInvokeCallback(typeof(GetWorldTransformUnmanagedDelegate))]
        static void GetWorldTransformUnmanaged(IntPtr msPtr, out Matrix worldTrans)
        {
			//UnityEngine.Debug.Log("Get" + msPtr.ToInt64());
			MotionState ms = GCHandle.FromIntPtr(msPtr).Target as MotionState;
            ms.GetWorldTransform(out worldTrans);
        }

		[MonoPInvokeCallback(typeof(SetWorldTransformUnmanagedDelegate))]
        static void SetWorldTransformUnmanaged(IntPtr msPtr, ref Matrix worldTrans)
        {
			//UnityEngine.Debug.Log("Set" + msPtr.ToInt64());
			MotionState ms = GCHandle.FromIntPtr(msPtr).Target as MotionState;
            ms.SetWorldTransform(ref worldTrans);
        }

        public abstract void GetWorldTransform(out Matrix worldTrans);
        public abstract void SetWorldTransform(ref Matrix worldTrans);

        public Matrix WorldTransform
        {
            get
            {
                Matrix transform;
                GetWorldTransform(out transform);
                return transform;
            }
            set { SetWorldTransform(ref value); }
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
				btMotionState_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~MotionState()
		{
			Dispose(false);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMotionState_getWorldTransform(IntPtr obj, [Out] out Matrix worldTrans);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMotionState_setWorldTransform(IntPtr obj, [In] ref Matrix worldTrans);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMotionState_delete(IntPtr obj);

        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btMotionStateWrapper_new(IntPtr getWorldTransformCallback, IntPtr setWorldTransformCallback, IntPtr motionStatePtr);
    }
}
