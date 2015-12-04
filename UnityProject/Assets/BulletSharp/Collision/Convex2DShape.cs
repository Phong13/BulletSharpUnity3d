using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
	public class Convex2DShape : ConvexShape
	{
        private ConvexShape _childShape;

		public Convex2DShape(ConvexShape convexChildShape)
			: base(btConvex2dShape_new(convexChildShape._native))
		{
            _childShape = convexChildShape;
		}

		public ConvexShape ChildShape
		{
            get { return _childShape; }
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btConvex2dShape_new(IntPtr convexChildShape);
		//[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		//static extern IntPtr btConvex2dShape_getChildShape(IntPtr obj);
	}
}
