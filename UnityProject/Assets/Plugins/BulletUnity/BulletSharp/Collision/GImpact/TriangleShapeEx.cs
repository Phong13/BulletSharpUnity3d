using BulletSharp.Math;
using System;
using static BulletSharp.UnsafeNativeMethods;

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
			Native = GIM_TRIANGLE_CONTACT_new();
		}

		public GimTriangleContact(GimTriangleContact other)
		{
			Native = GIM_TRIANGLE_CONTACT_new2(other.Native);
		}

		public void CopyFrom(GimTriangleContact other)
		{
			GIM_TRIANGLE_CONTACT_copy_from(Native, other.Native);
		}
		/*
		public void MergePoints(Vector4 plane, float margin, Vector3 points, int pointCount)
		{
			GIM_TRIANGLE_CONTACT_merge_points(Native, ref plane, margin, ref points, pointCount);
		}
		*/
		public float PenetrationDepth
		{
			get => GIM_TRIANGLE_CONTACT_getPenetration_depth(Native);
			set => GIM_TRIANGLE_CONTACT_setPenetration_depth(Native, value);
		}

		public int PointCount
		{
			get => GIM_TRIANGLE_CONTACT_getPoint_count(Native);
			set => GIM_TRIANGLE_CONTACT_setPoint_count(Native, value);
		}

		public Vector3Array Points => new Vector3Array(GIM_TRIANGLE_CONTACT_getPoints(Native), 16);

		public Vector4 SeparatingNormal
		{
			get
			{
				Vector4 value;
				GIM_TRIANGLE_CONTACT_getSeparating_normal(Native, out value);
				return value;
			}
			set => GIM_TRIANGLE_CONTACT_setSeparating_normal(Native, ref value);
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
				GIM_TRIANGLE_CONTACT_delete(Native);
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
			Native = btPrimitiveTriangle_new();
		}

		public void ApplyTransform(Matrix t)
		{
			btPrimitiveTriangle_applyTransform(Native, ref t);
		}

		public void BuildTriPlane()
		{
			btPrimitiveTriangle_buildTriPlane(Native);
		}
		/*
		public int ClipTriangle(PrimitiveTriangle other, Vector3 clippedPoints)
		{
			return btPrimitiveTriangle_clip_triangle(Native, other.Native, ref clippedPoints);
		}
		*/
		public bool FindTriangleCollisionClipMethod(PrimitiveTriangle other, GimTriangleContact contacts)
		{
			return btPrimitiveTriangle_find_triangle_collision_clip_method(Native, other.Native, contacts.Native);
		}

		public void GetEdgePlane(int edgeIndex, out Vector4 plane)
		{
			btPrimitiveTriangle_get_edge_plane(Native, edgeIndex, out plane);
		}

		public bool OverlapTestConservative(PrimitiveTriangle other)
		{
			return btPrimitiveTriangle_overlap_test_conservative(Native, other.Native);
		}

		public float Dummy
		{
			get => btPrimitiveTriangle_getDummy(Native);
			set => btPrimitiveTriangle_setDummy(Native, value);
		}

		public float Margin
		{
			get => btPrimitiveTriangle_getMargin(Native);
			set => btPrimitiveTriangle_setMargin(Native, value);
		}

		public Vector4 Plane
		{
			get
			{
				Vector4 value;
				btPrimitiveTriangle_getPlane(Native, out value);
				return value;
			}
			set => btPrimitiveTriangle_setPlane(Native, ref value);
		}

		public Vector3Array Vertices => new Vector3Array(btPrimitiveTriangle_getVertices(Native), 3);

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (Native != IntPtr.Zero)
			{
				btPrimitiveTriangle_delete(Native);
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
			: base(btTriangleShapeEx_new())
		{
		}

		public TriangleShapeEx(Vector3 p0, Vector3 p1, Vector3 p2)
			: base(btTriangleShapeEx_new2(ref p0, ref p1, ref p2))
		{
		}

		public TriangleShapeEx(TriangleShapeEx other)
			: base(btTriangleShapeEx_new3(other.Native))
		{
		}

		public void ApplyTransform(Matrix transform)
		{
			btTriangleShapeEx_applyTransform(Native, ref transform);
		}

		public void BuildTriPlane(out Vector4 plane)
		{
			btTriangleShapeEx_buildTriPlane(Native, out plane);
		}

		public bool OverlapTestConservative(TriangleShapeEx other)
		{
			return btTriangleShapeEx_overlap_test_conservative(Native, other.Native);
		}
	}
}
