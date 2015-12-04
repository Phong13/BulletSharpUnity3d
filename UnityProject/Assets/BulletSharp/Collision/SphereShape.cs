using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
	public class SphereShape : ConvexInternalShape
	{
		public SphereShape(float radius)
			: base(btSphereShape_new(radius))
		{
		}

		public void SetUnscaledRadius(float radius)
		{
			btSphereShape_setUnscaledRadius(_native, radius);
		}

		public float Radius
		{
			get { return btSphereShape_getRadius(_native); }
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSphereShape_new(float radius);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btSphereShape_getRadius(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSphereShape_setUnscaledRadius(IntPtr obj, float radius);
	}
}
