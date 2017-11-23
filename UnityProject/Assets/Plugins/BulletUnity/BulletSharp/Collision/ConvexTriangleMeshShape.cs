using System;
using BulletSharp.Math;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class ConvexTriangleMeshShape : PolyhedralConvexAabbCachingShape
	{
		private StridingMeshInterface _meshInterface;

		internal ConvexTriangleMeshShape(IntPtr native)
			: base(native)
		{
		}

		public ConvexTriangleMeshShape(StridingMeshInterface meshInterface, bool calcAabb = true)
			: base(btConvexTriangleMeshShape_new(meshInterface.Native, calcAabb))
		{
			_meshInterface = meshInterface;
		}

		public void CalculatePrincipalAxisTransform(Matrix principal, out Vector3 inertia,
			out float volume)
		{
			btConvexTriangleMeshShape_calculatePrincipalAxisTransform(Native, ref principal,
				out inertia, out volume);
		}

		public StridingMeshInterface MeshInterface => _meshInterface;
	}
}
