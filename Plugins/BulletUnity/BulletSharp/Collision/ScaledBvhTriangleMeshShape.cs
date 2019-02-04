using System;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.Math;

namespace BulletSharp
{
	public class ScaledBvhTriangleMeshShape : ConcaveShape
	{
		private BvhTriangleMeshShape _childShape;

		public ScaledBvhTriangleMeshShape(BvhTriangleMeshShape childShape, Vector3 localScaling)
			: base(btScaledBvhTriangleMeshShape_new(childShape._native, ref localScaling))
		{
			_childShape = childShape;
		}

		public BvhTriangleMeshShape ChildShape
		{
			get { return _childShape; }
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btScaledBvhTriangleMeshShape_new(IntPtr childShape, [In] ref Vector3 localScaling);
	}
}
