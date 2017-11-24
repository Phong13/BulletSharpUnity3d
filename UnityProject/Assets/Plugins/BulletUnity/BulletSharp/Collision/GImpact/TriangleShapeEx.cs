using BulletSharp.Math;
using System;


namespace BulletSharp
{
	public class GimTriangleContact : IDisposable
	{
		internal IntPtr Native;

		internal GimTriangleContact(IntPtr native)
		{
			Native = native;
		}

		public GimTriangleContact()
		{
			Native = UnsafeNativeMethods.GIM_TRIANGLE_CONTACT_new();
		}

		public GimTriangleContact(GimTriangleContact other)
		{
			Native = UnsafeNativeMethods.GIM_TRIANGLE_CONTACT_new2(other.Native);
		}

		public void CopyFrom(GimTriangleContact other)
		{
			UnsafeNativeMethods.GIM_TRIANGLE_CONTACT_copy_from(Native, other.Native);
		}
		/*
		public void MergePoints(Vector4 plane, float margin, Vector3 points, int pointCount)
		{
			UnsafeNativeMethods.GIM_TRIANGLE_CONTACT_merge_points(Native, ref plane, margin, ref points, pointCount);
		}
		*/
		public float PenetrationDepth
		{
			get { return  UnsafeNativeMethods.GIM_TRIANGLE_CONTACT_getPenetration_depth(Native);}
			set {  UnsafeNativeMethods.GIM_TRIANGLE_CONTACT_setPenetration_depth(Native, value);}
		}

		public int PointCount
		{
			get { return  UnsafeNativeMethods.GIM_TRIANGLE_CONTACT_getPoint_count(Native);}
			set {  UnsafeNativeMethods.GIM_TRIANGLE_CONTACT_setPoint_count(Native, value);}
		}

		public Vector3Array Points{ get { return  new Vector3Array(UnsafeNativeMethods.GIM_TRIANGLE_CONTACT_getPoints(Native), 16);} }

		public Vector4 SeparatingNormal
		{
			get
			{
				Vector4 value;
				UnsafeNativeMethods.GIM_TRIANGLE_CONTACT_getSeparating_normal(Native, out value);
				return value;
			}
			set {  UnsafeNativeMethods.GIM_TRIANGLE_CONTACT_setSeparating_normal(Native, ref value);}
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (Native != IntPtr.Zero)
			{
				UnsafeNativeMethods.GIM_TRIANGLE_CONTACT_delete(Native);
				Native = IntPtr.Zero;
			}
		}

		~GimTriangleContact()
		{
			Dispose(false);
		}
	}

	public class PrimitiveTriangle : IDisposable
	{
		internal IntPtr Native;

		internal PrimitiveTriangle(IntPtr native)
		{
			Native = native;
		}

		public PrimitiveTriangle()
		{
			Native = UnsafeNativeMethods.btPrimitiveTriangle_new();
		}

		public void ApplyTransform(Matrix t)
		{
			UnsafeNativeMethods.btPrimitiveTriangle_applyTransform(Native, ref t);
		}

		public void BuildTriPlane()
		{
			UnsafeNativeMethods.btPrimitiveTriangle_buildTriPlane(Native);
		}
		/*
		public int ClipTriangle(PrimitiveTriangle other, Vector3 clippedPoints)
		{
			return UnsafeNativeMethods.btPrimitiveTriangle_clip_triangle(Native, other.Native, ref clippedPoints);
		}
		*/
		public bool FindTriangleCollisionClipMethod(PrimitiveTriangle other, GimTriangleContact contacts)
		{
			return UnsafeNativeMethods.btPrimitiveTriangle_find_triangle_collision_clip_method(Native, other.Native, contacts.Native);
		}

		public void GetEdgePlane(int edgeIndex, out Vector4 plane)
		{
			UnsafeNativeMethods.btPrimitiveTriangle_get_edge_plane(Native, edgeIndex, out plane);
		}

		public bool OverlapTestConservative(PrimitiveTriangle other)
		{
			return UnsafeNativeMethods.btPrimitiveTriangle_overlap_test_conservative(Native, other.Native);
		}

		public float Dummy
		{
			get { return  UnsafeNativeMethods.btPrimitiveTriangle_getDummy(Native);}
			set {  UnsafeNativeMethods.btPrimitiveTriangle_setDummy(Native, value);}
		}

		public float Margin
		{
			get { return  UnsafeNativeMethods.btPrimitiveTriangle_getMargin(Native);}
			set {  UnsafeNativeMethods.btPrimitiveTriangle_setMargin(Native, value);}
		}

		public Vector4 Plane
		{
			get
			{
				Vector4 value;
				UnsafeNativeMethods.btPrimitiveTriangle_getPlane(Native, out value);
				return value;
			}
			set {  UnsafeNativeMethods.btPrimitiveTriangle_setPlane(Native, ref value);}
		}

		public Vector3Array Vertices{ get { return  new Vector3Array(UnsafeNativeMethods.btPrimitiveTriangle_getVertices(Native), 3);} }

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (Native != IntPtr.Zero)
			{
				UnsafeNativeMethods.btPrimitiveTriangle_delete(Native);
				Native = IntPtr.Zero;
			}
		}

		~PrimitiveTriangle()
		{
			Dispose(false);
		}
	}

	public class TriangleShapeEx : TriangleShape
	{
		public TriangleShapeEx()
			: base(UnsafeNativeMethods.btTriangleShapeEx_new())
		{
		}

		public TriangleShapeEx(Vector3 p0, Vector3 p1, Vector3 p2)
			: base(UnsafeNativeMethods.btTriangleShapeEx_new2(ref p0, ref p1, ref p2))
		{
		}

		public TriangleShapeEx(TriangleShapeEx other)
			: base(UnsafeNativeMethods.btTriangleShapeEx_new3(other.Native))
		{
		}

		public void ApplyTransform(Matrix transform)
		{
			UnsafeNativeMethods.btTriangleShapeEx_applyTransform(Native, ref transform);
		}

		public void BuildTriPlane(out Vector4 plane)
		{
			UnsafeNativeMethods.btTriangleShapeEx_buildTriPlane(Native, out plane);
		}

		public bool OverlapTestConservative(TriangleShapeEx other)
		{
			return UnsafeNativeMethods.btTriangleShapeEx_overlap_test_conservative(Native, other.Native);
		}
	}
}
