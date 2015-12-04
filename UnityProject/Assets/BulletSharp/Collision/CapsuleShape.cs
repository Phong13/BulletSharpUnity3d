using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
	public class CapsuleShape : ConvexInternalShape
	{
		internal CapsuleShape(IntPtr native)
			: base(native)
		{
		}

		public CapsuleShape(float radius, float height)
			: base(btCapsuleShape_new(radius, height))
		{
		}

		public float HalfHeight
		{
			get { return btCapsuleShape_getHalfHeight(_native); }
		}

		public float Radius
		{
			get { return btCapsuleShape_getRadius(_native); }
		}

		public int UpAxis
		{
			get { return btCapsuleShape_getUpAxis(_native); }
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btCapsuleShape_new(float radius, float height);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btCapsuleShape_getHalfHeight(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btCapsuleShape_getRadius(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btCapsuleShape_getUpAxis(IntPtr obj);
	}

	public class CapsuleShapeX : CapsuleShape
	{
		public CapsuleShapeX(float radius, float height)
			: base(btCapsuleShapeX_new(radius, height))
		{
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btCapsuleShapeX_new(float radius, float height);
	}

	public class CapsuleShapeZ : CapsuleShape
	{
		public CapsuleShapeZ(float radius, float height)
			: base(btCapsuleShapeZ_new(radius, height))
		{
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btCapsuleShapeZ_new(float radius, float height);
	}
}
