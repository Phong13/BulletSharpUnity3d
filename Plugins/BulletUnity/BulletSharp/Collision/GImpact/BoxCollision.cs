using System;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.Math;

namespace BulletSharp
{
	public enum eBT_PLANE_INTERSECTION_TYPE
	{
		BackPlane = 0,
		CollidePlane,
		FrontPlane
	}

	public class Aabb : IDisposable
	{
		internal IntPtr _native;
		bool _preventDelete;

		internal Aabb(IntPtr native, bool preventDelete)
		{
			_native = native;
			_preventDelete = preventDelete;
		}

		public Aabb()
		{
			_native = btAABB_new();
		}

		public Aabb(Vector3 v1, Vector3 v2, Vector3 v3)
		{
			_native = btAABB_new2(ref v1, ref v2, ref v3);
		}

		public Aabb(Vector3 v1, Vector3 v2, Vector3 v3, float margin)
		{
			_native = btAABB_new3(ref v1, ref v2, ref v3, margin);
		}

		public Aabb(Aabb other)
		{
			_native = btAABB_new4(other._native);
		}

		public Aabb(Aabb other, float margin)
		{
			_native = btAABB_new5(other._native, margin);
		}

		public void ApplyTransform(ref Matrix trans)
		{
			btAABB_appy_transform(_native, ref trans);
		}

		public bool CollidePlane(ref Vector4 plane)
		{
			return btAABB_collide_plane(_native, ref plane);
		}

		public bool CollideRay(ref Vector3 vorigin, ref Vector3 vdir)
		{
			return btAABB_collide_ray(_native, ref vorigin, ref vdir);
		}

		public bool CollideTriangleExact(ref Vector3 p1, ref Vector3 p2, ref Vector3 p3, ref Vector4 trianglePlane)
		{
			return btAABB_collide_triangle_exact(_native, ref p1, ref p2, ref p3, ref trianglePlane);
		}

		public void CopyWithMargin(Aabb other, float margin)
		{
			btAABB_copy_with_margin(_native, other._native, margin);
		}

		public void FindIntersection(Aabb other, Aabb intersection)
		{
			btAABB_find_intersection(_native, other._native, intersection._native);
		}

		public void GetCenterExtend(out Vector3 center, out Vector3 extend)
		{
			btAABB_get_center_extend(_native, out center, out extend);
		}

		public bool HasCollision(Aabb other)
		{
			return btAABB_has_collision(_native, other._native);
		}

		public void IncrementMargin(float margin)
		{
			btAABB_increment_margin(_native, margin);
		}

		public void Invalidate()
		{
			btAABB_invalidate(_native);
		}

		public void Merge(Aabb box)
		{
			btAABB_merge(_native, box._native);
		}

		public bool OverlappingTransConservative(Aabb box, ref Matrix trans1To0)
		{
			return btAABB_overlapping_trans_conservative(_native, box._native, ref trans1To0);
		}

		public eBT_PLANE_INTERSECTION_TYPE PlaneClassify(Vector4 plane)
		{
			return btAABB_plane_classify(_native, ref plane);
		}

        public void ProjectionInterval(ref Vector3 direction, out float vmin, out float vmax)
		{
            btAABB_projection_interval(_native, ref direction, out vmin, out vmax);
		}

		public Vector3 Max
		{
			get
			{
				Vector3 value;
				btAABB_getMax(_native, out value);
				return value;
			}
			set { btAABB_setMax(_native, ref value); }
		}

		public Vector3 Min
		{
			get
			{
				Vector3 value;
				btAABB_getMin(_native, out value);
				return value;
			}
			set { btAABB_setMin(_native, ref value); }
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
				if (!_preventDelete)
				{
					btAABB_delete(_native);
				}
				_native = IntPtr.Zero;
			}
		}

		~Aabb()
		{
			Dispose(false);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btAABB_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btAABB_new2([In] ref Vector3 V1, [In] ref Vector3 V2, [In] ref Vector3 V3);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btAABB_new3([In] ref Vector3 V1, [In] ref Vector3 V2, [In] ref Vector3 V3, float margin);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btAABB_new4(IntPtr other);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btAABB_new5(IntPtr other, float margin);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btAABB_appy_transform(IntPtr obj, [In] ref Matrix trans);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btAABB_appy_transform_trans_cache(IntPtr obj, IntPtr trans);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btAABB_collide_plane(IntPtr obj, [In] ref Vector4 plane);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btAABB_collide_ray(IntPtr obj, [In] ref Vector3 vorigin, [In] ref Vector3 vdir);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btAABB_collide_triangle_exact(IntPtr obj, [In] ref Vector3 p1, [In] ref Vector3 p2, [In] ref Vector3 p3, [In] ref Vector4 triangle_plane);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btAABB_copy_with_margin(IntPtr obj, IntPtr other, float margin);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btAABB_find_intersection(IntPtr obj, IntPtr other, IntPtr intersection);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btAABB_get_center_extend(IntPtr obj, [Out] out Vector3 center, [Out] out Vector3 extend);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btAABB_getMax(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btAABB_getMin(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btAABB_has_collision(IntPtr obj, IntPtr other);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btAABB_increment_margin(IntPtr obj, float margin);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btAABB_invalidate(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btAABB_merge(IntPtr obj, IntPtr box);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btAABB_overlapping_trans_cache(IntPtr obj, IntPtr box, IntPtr transcache, bool fulltest);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btAABB_overlapping_trans_conservative(IntPtr obj, IntPtr box, [In] ref Matrix trans1_to_0);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btAABB_overlapping_trans_conservative2(IntPtr obj, IntPtr box, IntPtr trans1_to_0);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern eBT_PLANE_INTERSECTION_TYPE btAABB_plane_classify(IntPtr obj, [In] ref Vector4 plane);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btAABB_projection_interval(IntPtr obj, [In] ref Vector3 direction, [Out] out float vmin, [Out] out float vmax);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btAABB_setMax(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btAABB_setMin(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btAABB_delete(IntPtr obj);
	}
}
