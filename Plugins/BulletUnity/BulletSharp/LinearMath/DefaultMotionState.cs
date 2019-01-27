using System;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.Math;

namespace BulletSharp
{
	public class DefaultMotionState : MotionState
	{
		internal DefaultMotionState(IntPtr native)
			: base(native)
		{
		}

		public DefaultMotionState()
			: base(btDefaultMotionState_new())
		{
		}

		public DefaultMotionState(Matrix startTrans)
			: base(btDefaultMotionState_new2(ref startTrans))
		{
		}

		public DefaultMotionState(Matrix startTrans, Matrix centerOfMassOffset)
			: base(btDefaultMotionState_new3(ref startTrans, ref centerOfMassOffset))
		{
		}

        public override void GetWorldTransform(out Matrix worldTrans)
        {
            btMotionState_getWorldTransform(_native, out worldTrans);
        }

        public override void SetWorldTransform(ref Matrix worldTrans)
        {
            btMotionState_setWorldTransform(_native, ref worldTrans);
        }

		public Matrix CenterOfMassOffset
		{
			get
			{
				Matrix value;
				btDefaultMotionState_getCenterOfMassOffset(_native, out value);
				return value;
			}
			set { btDefaultMotionState_setCenterOfMassOffset(_native, ref value); }
		}

		public Matrix GraphicsWorldTrans
		{
			get
			{
				Matrix value;
				btDefaultMotionState_getGraphicsWorldTrans(_native, out value);
				return value;
			}
			set { btDefaultMotionState_setGraphicsWorldTrans(_native, ref value); }
		}

		public Matrix StartWorldTrans
		{
			get
			{
				Matrix value;
				btDefaultMotionState_getStartWorldTrans(_native, out value);
				return value;
			}
			set { btDefaultMotionState_setStartWorldTrans(_native, ref value); }
		}

		public IntPtr UserPointer
		{
			get { return btDefaultMotionState_getUserPointer(_native); }
			set { btDefaultMotionState_setUserPointer(_native, value); }
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btDefaultMotionState_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btDefaultMotionState_new2([In] ref Matrix startTrans);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btDefaultMotionState_new3([In] ref Matrix startTrans, [In] ref Matrix centerOfMassOffset);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDefaultMotionState_getCenterOfMassOffset(IntPtr obj, [Out] out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDefaultMotionState_getGraphicsWorldTrans(IntPtr obj, [Out] out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDefaultMotionState_getStartWorldTrans(IntPtr obj, [Out] out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btDefaultMotionState_getUserPointer(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDefaultMotionState_setCenterOfMassOffset(IntPtr obj, [In] ref Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDefaultMotionState_setGraphicsWorldTrans(IntPtr obj, [In] ref Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDefaultMotionState_setStartWorldTrans(IntPtr obj, [In] ref Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDefaultMotionState_setUserPointer(IntPtr obj, IntPtr value);

        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btMotionState_getWorldTransform(IntPtr obj, [Out] out Matrix worldTrans);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btMotionState_setWorldTransform(IntPtr obj, [In] ref Matrix worldTrans);
	}
}
