using System;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.Math;

namespace BulletSharp
{
	/*
	public class GimTriangleContact : IDisposable
	{
		internal IntPtr _native;

		internal GimTriangleContact(IntPtr native)
		{
			_native = native;
		}

		public GimTriangleContact()
		{
			_native = GIM_TRIANGLE_CONTACT_new();
		}

		public GimTriangleContact(GimTriangleContact other)
		{
			_native = GIM_TRIANGLE_CONTACT_new2(other._native);
		}

		public void CopyFrom(GimTriangleContact other)
		{
			GIM_TRIANGLE_CONTACT_copy_from(_native, other._native);
		}
        
		public float PenetrationDepth
		{
			get { return GIM_TRIANGLE_CONTACT_getPenetration_depth(_native); }
			set { GIM_TRIANGLE_CONTACT_setPenetration_depth(_native, value); }
		}

		public int PointCount
		{
			get { return GIM_TRIANGLE_CONTACT_getPoint_count(_native); }
			set { GIM_TRIANGLE_CONTACT_setPoint_count(_native, value); }
		}

		public Vector3Array Points
		{
            get { return new Vector3Array(GIM_TRIANGLE_CONTACT_getPoints(_native), 16); }
		}

		public Vector4 SeparatingNormal
		{
			get
			{
				Vector4 value;
				GIM_TRIANGLE_CONTACT_getSeparating_normal(_native, out value);
				return value;
			}
			set { GIM_TRIANGLE_CONTACT_setSeparating_normal(_native, ref value); }
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
				GIM_TRIANGLE_CONTACT_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~GimTriangleContact()
		{
			Dispose(false);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr GIM_TRIANGLE_CONTACT_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr GIM_TRIANGLE_CONTACT_new2(IntPtr other);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void GIM_TRIANGLE_CONTACT_copy_from(IntPtr obj, IntPtr other);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float GIM_TRIANGLE_CONTACT_getPenetration_depth(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int GIM_TRIANGLE_CONTACT_getPoint_count(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr GIM_TRIANGLE_CONTACT_getPoints(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void GIM_TRIANGLE_CONTACT_getSeparating_normal(IntPtr obj, [Out] out Vector4 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void GIM_TRIANGLE_CONTACT_merge_points(IntPtr obj, [In] ref Vector4 plane, float margin, [In] ref Vector3 points, int point_count);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void GIM_TRIANGLE_CONTACT_setPenetration_depth(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void GIM_TRIANGLE_CONTACT_setPoint_count(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void GIM_TRIANGLE_CONTACT_setSeparating_normal(IntPtr obj, [In] ref Vector4 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void GIM_TRIANGLE_CONTACT_delete(IntPtr obj);
	}
	*/
	/*
	public class PrimitiveTriangle : IDisposable
	{
		internal IntPtr _native;

		internal PrimitiveTriangle(IntPtr native)
		{
			_native = native;
		}

		public PrimitiveTriangle()
		{
			_native = btPrimitiveTriangle_new();
		}

		public void ApplyTransform(Matrix t)
		{
			btPrimitiveTriangle_applyTransform(_native, ref t);
		}

		public void BuildTriPlane()
		{
			btPrimitiveTriangle_buildTriPlane(_native);
		}
        
		public bool FindTriangleCollisionClipMethod(PrimitiveTriangle other, GimTriangleContact contacts)
		{
			return btPrimitiveTriangle_find_triangle_collision_clip_method(_native, other._native, contacts._native);
		}

		public void GetEdgePlane(int edgeIndex, out Vector4 plane)
		{
			btPrimitiveTriangle_get_edge_plane(_native, edgeIndex, out plane);
		}

		public bool OverlapTestConservative(PrimitiveTriangle other)
		{
			return btPrimitiveTriangle_overlap_test_conservative(_native, other._native);
		}

		public float Dummy
		{
			get { return btPrimitiveTriangle_getDummy(_native); }
			set { btPrimitiveTriangle_setDummy(_native, value); }
		}

		public float Margin
		{
			get { return btPrimitiveTriangle_getMargin(_native); }
			set { btPrimitiveTriangle_setMargin(_native, value); }
		}

		public Vector4 Plane
		{
			get
			{
				Vector4 value;
				btPrimitiveTriangle_getPlane(_native, out value);
				return value;
			}
			set { btPrimitiveTriangle_setPlane(_native, ref value); }
		}

		public Vector3Array Vertices
		{
            get { return new Vector3Array(btPrimitiveTriangle_getVertices(_native), 3); }
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
				btPrimitiveTriangle_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~PrimitiveTriangle()
		{
			Dispose(false);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btPrimitiveTriangle_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btPrimitiveTriangle_applyTransform(IntPtr obj, [In] ref Matrix t);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btPrimitiveTriangle_buildTriPlane(IntPtr obj);
		//[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		//static extern int btPrimitiveTriangle_clip_triangle(IntPtr obj, IntPtr other, [Out] out Vector3 clipped_points);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btPrimitiveTriangle_find_triangle_collision_clip_method(IntPtr obj, IntPtr other, IntPtr contacts);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btPrimitiveTriangle_get_edge_plane(IntPtr obj, int edge_index, [Out] out Vector4 plane);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btPrimitiveTriangle_getDummy(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btPrimitiveTriangle_getMargin(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btPrimitiveTriangle_getPlane(IntPtr obj, [Out] out Vector4 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btPrimitiveTriangle_getVertices(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btPrimitiveTriangle_overlap_test_conservative(IntPtr obj, IntPtr other);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btPrimitiveTriangle_setDummy(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btPrimitiveTriangle_setMargin(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btPrimitiveTriangle_setPlane(IntPtr obj, [In] ref Vector4 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btPrimitiveTriangle_delete(IntPtr obj);

	}

	public class TriangleShapeEx : TriangleShape
	{
		public TriangleShapeEx()
			: base(btTriangleShapeEx_new())
		{
		}

		public TriangleShapeEx(Vector3 p0, Vector3 p1, Vector3 p2)
			: base(btTriangleShapeEx_new2(ref p0, ref p1, ref p2))
		{
		}

		public TriangleShapeEx(TriangleShapeEx other)
			: base(btTriangleShapeEx_new3(other._native))
		{
		}

		public void ApplyTransform(Matrix t)
		{
			btTriangleShapeEx_applyTransform(_native, ref t);
		}

		public void BuildTriPlane(out Vector4 plane)
		{
			btTriangleShapeEx_buildTriPlane(_native, out plane);
		}

		public bool OverlapTestConservative(TriangleShapeEx other)
		{
			return btTriangleShapeEx_overlap_test_conservative(_native, other._native);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btTriangleShapeEx_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btTriangleShapeEx_new2([In] ref Vector3 p0, [In] ref Vector3 p1, [In] ref Vector3 p2);
		//[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		//static extern IntPtr btTriangleShapeEx_new3(IntPtr other);
		//[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		//static extern void btTriangleShapeEx_applyTransform(IntPtr obj, [In] ref Matrix t);
		//[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		//static extern void btTriangleShapeEx_buildTriPlane(IntPtr obj, [Out] out Vector4 plane);
		//[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		//[return: MarshalAs(UnmanagedType.I1)]
		//static extern bool btTriangleShapeEx_overlap_test_conservative(IntPtr obj, IntPtr other);
	}
*/
}
