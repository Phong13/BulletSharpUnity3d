using System;
using BulletSharp.Math;
using static BulletSharp.UnsafeNativeMethods;

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

		public BvhTriangleMeshShape(StridingMeshInterface meshInterface, bool useQuantizedAabbCompression,
			bool buildBvh = true)
			: base(btBvhTriangleMeshShape_new(meshInterface.Native, useQuantizedAabbCompression,
				buildBvh))
		{
			_meshInterface = meshInterface;
		}

		public BvhTriangleMeshShape(StridingMeshInterface meshInterface, bool useQuantizedAabbCompression,
			Vector3 bvhAabbMin, Vector3 bvhAabbMax, bool buildBvh = true)
			: base(btBvhTriangleMeshShape_new2(meshInterface.Native, useQuantizedAabbCompression,
				ref bvhAabbMin, ref bvhAabbMax, buildBvh))
		{
			_meshInterface = meshInterface;
		}

		public void BuildOptimizedBvh()
		{
			btBvhTriangleMeshShape_buildOptimizedBvh(Native);
			_optimizedBvh = null;
		}

		public void PartialRefitTreeRef(ref Vector3 aabbMin, ref Vector3 aabbMax)
		{
			btBvhTriangleMeshShape_partialRefitTree(Native, ref aabbMin, ref aabbMax);
		}

		public void PartialRefitTree(Vector3 aabbMin, Vector3 aabbMax)
		{
			btBvhTriangleMeshShape_partialRefitTree(Native, ref aabbMin, ref aabbMax);
		}

		public void PerformConvexcast(TriangleCallback callback, Vector3 boxSource,
			Vector3 boxTarget, Vector3 boxMin, Vector3 boxMax)
		{
			btBvhTriangleMeshShape_performConvexcast(Native, callback.Native, ref boxSource,
				ref boxTarget, ref boxMin, ref boxMax);
		}

		public void PerformRaycast(TriangleCallback callback, Vector3 raySource,
			Vector3 rayTarget)
		{
			btBvhTriangleMeshShape_performRaycast(Native, callback.Native, ref raySource,
				ref rayTarget);
		}

		public void RefitTreeRef(ref Vector3 aabbMin, ref Vector3 aabbMax)
		{
			btBvhTriangleMeshShape_refitTree(Native, ref aabbMin, ref aabbMax);
		}

		public void RefitTree(Vector3 aabbMin, Vector3 aabbMax)
		{
			btBvhTriangleMeshShape_refitTree(Native, ref aabbMin, ref aabbMax);
		}

		public void SerializeSingleBvh(Serializer serializer)
		{
			btBvhTriangleMeshShape_serializeSingleBvh(Native, serializer._native);
		}

		public void SerializeSingleTriangleInfoMap(Serializer serializer)
		{
			btBvhTriangleMeshShape_serializeSingleTriangleInfoMap(Native, serializer._native);
		}

		public void SetOptimizedBvh(OptimizedBvh bvh, Vector3 localScaling)
		{
			System.Diagnostics.Debug.Assert(!OwnsBvh);
			btBvhTriangleMeshShape_setOptimizedBvh2(Native, (bvh != null) ? bvh._native : IntPtr.Zero, ref localScaling);
			_optimizedBvh = bvh;
		}

		public OptimizedBvh OptimizedBvh
		{
			get
			{
				if (_optimizedBvh == null && OwnsBvh)
				{
					IntPtr optimizedBvhPtr = btBvhTriangleMeshShape_getOptimizedBvh(Native);
					_optimizedBvh = new OptimizedBvh(optimizedBvhPtr, true);
				}
				return _optimizedBvh;
			}
			set
			{
				System.Diagnostics.Debug.Assert(!OwnsBvh);
				btBvhTriangleMeshShape_setOptimizedBvh(Native, (value != null) ? value._native : IntPtr.Zero);
				_optimizedBvh = value;
			}
		}

		public bool OwnsBvh => btBvhTriangleMeshShape_getOwnsBvh(Native);

		public TriangleInfoMap TriangleInfoMap
		{
			get
			{
				if (_triangleInfoMap == null)
				{
					IntPtr triangleInfoMap = btBvhTriangleMeshShape_getTriangleInfoMap(Native);
					if (triangleInfoMap != IntPtr.Zero)
					{
						_triangleInfoMap = new TriangleInfoMap(triangleInfoMap, true);
					}
				}
				return _triangleInfoMap;
			}
			set
			{
				btBvhTriangleMeshShape_setTriangleInfoMap(Native, (value != null) ? value.Native : IntPtr.Zero);
				_triangleInfoMap = value;
			}
		}

		public bool UsesQuantizedAabbCompression => btBvhTriangleMeshShape_usesQuantizedAabbCompression(Native);
	}
}
