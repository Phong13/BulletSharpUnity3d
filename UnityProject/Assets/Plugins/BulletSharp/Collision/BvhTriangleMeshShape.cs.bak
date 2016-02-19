using System;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.Math;

namespace BulletSharp
{
	public class BvhTriangleMeshShape : TriangleMeshShape
	{
        private OptimizedBvh _optimizedBvh;
		private TriangleInfoMap _triangleInfoMap;

		internal BvhTriangleMeshShape(IntPtr native)
			: base(native)
		{
		}

		public BvhTriangleMeshShape(StridingMeshInterface meshInterface, bool useQuantizedAabbCompression)
			: base(btBvhTriangleMeshShape_new(meshInterface._native, useQuantizedAabbCompression))
		{
			_meshInterface = meshInterface;
		}

		public BvhTriangleMeshShape(StridingMeshInterface meshInterface, bool useQuantizedAabbCompression, bool buildBvh)
			: base(btBvhTriangleMeshShape_new2(meshInterface._native, useQuantizedAabbCompression, buildBvh))
		{
			_meshInterface = meshInterface;
		}

		public BvhTriangleMeshShape(StridingMeshInterface meshInterface, bool useQuantizedAabbCompression, Vector3 bvhAabbMin, Vector3 bvhAabbMax)
			: base(btBvhTriangleMeshShape_new3(meshInterface._native, useQuantizedAabbCompression, ref bvhAabbMin, ref bvhAabbMax))
		{
			_meshInterface = meshInterface;
		}

		public BvhTriangleMeshShape(StridingMeshInterface meshInterface, bool useQuantizedAabbCompression, Vector3 bvhAabbMin, Vector3 bvhAabbMax, bool buildBvh)
			: base(btBvhTriangleMeshShape_new4(meshInterface._native, useQuantizedAabbCompression, ref bvhAabbMin, ref bvhAabbMax, buildBvh))
		{
			_meshInterface = meshInterface;
		}

		public void BuildOptimizedBvh()
		{
			btBvhTriangleMeshShape_buildOptimizedBvh(_native);
		}

        public void PartialRefitTree(ref Vector3 aabbMin, ref Vector3 aabbMax)
		{
			btBvhTriangleMeshShape_partialRefitTree(_native, ref aabbMin, ref aabbMax);
		}

		public void PerformConvexcast(TriangleCallback callback, Vector3 boxSource, Vector3 boxTarget, Vector3 boxMin, Vector3 boxMax)
		{
			btBvhTriangleMeshShape_performConvexcast(_native, callback._native, ref boxSource, ref boxTarget, ref boxMin, ref boxMax);
		}

		public void PerformRaycast(TriangleCallback callback, Vector3 raySource, Vector3 rayTarget)
		{
			btBvhTriangleMeshShape_performRaycast(_native, callback._native, ref raySource, ref rayTarget);
		}

        public void RefitTree(ref Vector3 aabbMin, ref Vector3 aabbMax)
		{
			btBvhTriangleMeshShape_refitTree(_native, ref aabbMin, ref aabbMax);
		}

		public void SerializeSingleBvh(Serializer serializer)
		{
			btBvhTriangleMeshShape_serializeSingleBvh(_native, serializer._native);
		}

		public void SerializeSingleTriangleInfoMap(Serializer serializer)
		{
			btBvhTriangleMeshShape_serializeSingleTriangleInfoMap(_native, serializer._native);
		}

		public void SetOptimizedBvh(OptimizedBvh bvh, Vector3 localScaling)
		{
            btBvhTriangleMeshShape_setOptimizedBvh2(_native, (bvh != null) ? bvh._native : IntPtr.Zero, ref localScaling);
            _optimizedBvh = bvh;
		}

		public OptimizedBvh OptimizedBvh
		{
            get
            {
                if (_optimizedBvh == null)
                {
                    IntPtr optimizedBvhPtr = btBvhTriangleMeshShape_getOptimizedBvh(_native);
                    if (optimizedBvhPtr != IntPtr.Zero)
                    {
                        _optimizedBvh = new OptimizedBvh(optimizedBvhPtr);
                    }
                }
                return _optimizedBvh;
            }
            set
            {
                btBvhTriangleMeshShape_setOptimizedBvh(_native, (value != null) ? value._native : IntPtr.Zero);
            }
		}

		public bool OwnsBvh
		{
			get { return btBvhTriangleMeshShape_getOwnsBvh(_native); }
		}

		public TriangleInfoMap TriangleInfoMap
		{
            get
            {
                if (_triangleInfoMap == null)
                {
                    IntPtr triangleInfoMap = btBvhTriangleMeshShape_getTriangleInfoMap(_native);
                    if (triangleInfoMap != IntPtr.Zero)
                    {
                        _triangleInfoMap = new TriangleInfoMap(triangleInfoMap, true);
                    }
                }
                return _triangleInfoMap;
            }
            set
            {
                _triangleInfoMap = value;
                btBvhTriangleMeshShape_setTriangleInfoMap(_native, (value != null) ? value._native : IntPtr.Zero);
            }
		}

        public bool UsesQuantizedAabbCompression
        {
            get { return btBvhTriangleMeshShape_usesQuantizedAabbCompression(_native); }
        }

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btBvhTriangleMeshShape_new(IntPtr meshInterface, bool useQuantizedAabbCompression);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btBvhTriangleMeshShape_new2(IntPtr meshInterface, bool useQuantizedAabbCompression, bool buildBvh);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btBvhTriangleMeshShape_new3(IntPtr meshInterface, bool useQuantizedAabbCompression, [In] ref Vector3 bvhAabbMin, [In] ref Vector3 bvhAabbMax);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btBvhTriangleMeshShape_new4(IntPtr meshInterface, bool useQuantizedAabbCompression, [In] ref Vector3 bvhAabbMin, [In] ref Vector3 bvhAabbMax, bool buildBvh);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btBvhTriangleMeshShape_buildOptimizedBvh(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btBvhTriangleMeshShape_getOptimizedBvh(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btBvhTriangleMeshShape_getOwnsBvh(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btBvhTriangleMeshShape_getTriangleInfoMap(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btBvhTriangleMeshShape_partialRefitTree(IntPtr obj, [In] ref Vector3 aabbMin, [In] ref Vector3 aabbMax);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btBvhTriangleMeshShape_performConvexcast(IntPtr obj, IntPtr callback, [In] ref Vector3 boxSource, [In] ref Vector3 boxTarget, [In] ref Vector3 boxMin, [In] ref Vector3 boxMax);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btBvhTriangleMeshShape_performRaycast(IntPtr obj, IntPtr callback, [In] ref Vector3 raySource, [In] ref Vector3 rayTarget);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btBvhTriangleMeshShape_refitTree(IntPtr obj, [In] ref Vector3 aabbMin, [In] ref Vector3 aabbMax);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btBvhTriangleMeshShape_serializeSingleBvh(IntPtr obj, IntPtr serializer);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btBvhTriangleMeshShape_serializeSingleTriangleInfoMap(IntPtr obj, IntPtr serializer);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btBvhTriangleMeshShape_setOptimizedBvh(IntPtr obj, IntPtr bvh);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btBvhTriangleMeshShape_setOptimizedBvh2(IntPtr obj, IntPtr bvh, [In] ref Vector3 localScaling);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btBvhTriangleMeshShape_setTriangleInfoMap(IntPtr obj, IntPtr triangleInfoMap);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btBvhTriangleMeshShape_usesQuantizedAabbCompression(IntPtr obj);
	}
}
