using System;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.Math;

namespace BulletSharp
{
	public class MinkowskiSumShape : ConvexInternalShape
	{
		private ConvexShape _shapeA;
		private ConvexShape _shapeB;

		public MinkowskiSumShape(ConvexShape shapeA, ConvexShape shapeB)
			: base(btMinkowskiSumShape_new(shapeA._native, shapeB._native))
		{
			_shapeA = shapeA;
			_shapeB = shapeB;
		}

		public ConvexShape ShapeA
		{
			get { return _shapeA; }
		}

		public ConvexShape ShapeB
		{
			get { return _shapeB; }
		}

		public Matrix TransformA
		{
			get
			{
				Matrix value;
				btMinkowskiSumShape_getTransformA(_native, out value);
				return value;
			}
			set { btMinkowskiSumShape_setTransformA(_native, ref value); }
		}

		public Matrix TransformB
		{
			get
			{
				Matrix value;
				btMinkowskiSumShape_GetTransformB(_native, out value);
				return value;
			}
			set { btMinkowskiSumShape_setTransformB(_native, ref value); }
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btMinkowskiSumShape_new(IntPtr shapeA, IntPtr shapeB);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btMinkowskiSumShape_getShapeA(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btMinkowskiSumShape_getShapeB(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMinkowskiSumShape_getTransformA(IntPtr obj, [Out] out Matrix transA);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMinkowskiSumShape_GetTransformB(IntPtr obj, [Out] out Matrix transB);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMinkowskiSumShape_setTransformA(IntPtr obj, [In] ref Matrix transA);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMinkowskiSumShape_setTransformB(IntPtr obj, [In] ref Matrix transB);
	}
}
