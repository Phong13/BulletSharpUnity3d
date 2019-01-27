using System;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.Math;

namespace BulletSharp
{
	public class PointCollector : DiscreteCollisionDetectorInterface.Result
	{
		public PointCollector()
			: base(btPointCollector_new())
		{
		}

		public float Distance
		{
			get { return btPointCollector_getDistance(_native); }
			set { btPointCollector_setDistance(_native, value); }
		}

		public bool HasResult
		{
			get { return btPointCollector_getHasResult(_native); }
			set { btPointCollector_setHasResult(_native, value); }
		}

		public Vector3 NormalOnBInWorld
		{
			get
			{
				Vector3 value;
				btPointCollector_getNormalOnBInWorld(_native, out value);
				return value;
			}
			set { btPointCollector_setNormalOnBInWorld(_native, ref value); }
		}

		public Vector3 PointInWorld
		{
			get
			{
				Vector3 value;
				btPointCollector_getPointInWorld(_native, out value);
				return value;
			}
			set { btPointCollector_setPointInWorld(_native, ref value); }
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btPointCollector_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btPointCollector_getDistance(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btPointCollector_getHasResult(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btPointCollector_getNormalOnBInWorld(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btPointCollector_getPointInWorld(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btPointCollector_setDistance(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btPointCollector_setHasResult(IntPtr obj, bool value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btPointCollector_setNormalOnBInWorld(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btPointCollector_setPointInWorld(IntPtr obj, [In] ref Vector3 value);
	}
}
