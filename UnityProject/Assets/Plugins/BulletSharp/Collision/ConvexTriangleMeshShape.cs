using System;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.Math;

namespace BulletSharp
{
	public class ConvexTriangleMeshShape : PolyhedralConvexAabbCachingShape
	{
		private StridingMeshInterface _meshInterface;

		internal ConvexTriangleMeshShape(IntPtr native)
			: base(native)
		{
		}

		public ConvexTriangleMeshShape(StridingMeshInterface meshInterface)
			: base(btConvexTriangleMeshShape_new(meshInterface._native))
		{
			_meshInterface = meshInterface;
		}

		public ConvexTriangleMeshShape(StridingMeshInterface meshInterface, bool calcAabb)
			: base(btConvexTriangleMeshShape_new2(meshInterface._native, calcAabb))
		{
			_meshInterface = meshInterface;
		}

		public void CalculatePrincipalAxisTransform(ref Matrix principal, out Vector3 inertia, out float volume)
		{
			btConvexTriangleMeshShape_calculatePrincipalAxisTransform(_native, ref principal, out inertia, out volume);
		}

		public StridingMeshInterface MeshInterface
		{
			get { return _meshInterface; }
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btConvexTriangleMeshShape_new(IntPtr meshInterface);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btConvexTriangleMeshShape_new2(IntPtr meshInterface, bool calcAabb);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConvexTriangleMeshShape_calculatePrincipalAxisTransform(IntPtr obj, [In, Out] ref Matrix principal, [Out] out Vector3 inertia, [Out] out float volume);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btConvexTriangleMeshShape_getMeshInterface(IntPtr obj);
	}
}
