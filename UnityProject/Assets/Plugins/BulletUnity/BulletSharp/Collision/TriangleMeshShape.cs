using System;
using System.Runtime.InteropServices;
using BulletSharp.Math;


namespace BulletSharp
{
	public class TriangleMeshShape : ConcaveShape
	{
		protected StridingMeshInterface _meshInterface;

		internal TriangleMeshShape(IntPtr native)
			: base(native)
		{
		}

		public void LocalGetSupportingVertex(ref Vector3 vec, out Vector3 value)
		{
			UnsafeNativeMethods.btTriangleMeshShape_localGetSupportingVertex(Native, ref vec, out value);
		}

		public Vector3 LocalGetSupportingVertex(Vector3 vec)
		{
			Vector3 value;
			UnsafeNativeMethods.btTriangleMeshShape_localGetSupportingVertex(Native, ref vec, out value);
			return value;
		}

		public void LocalGetSupportingVertexWithoutMargin(ref Vector3 vec, out Vector3 value)
		{
			UnsafeNativeMethods.btTriangleMeshShape_localGetSupportingVertexWithoutMargin(Native, ref vec,
				out value);
		}

		public Vector3 LocalGetSupportingVertexWithoutMargin(Vector3 vec)
		{
			Vector3 value;
			UnsafeNativeMethods.btTriangleMeshShape_localGetSupportingVertexWithoutMargin(Native, ref vec,
				out value);
			return value;
		}

		public void RecalcLocalAabb()
		{
			UnsafeNativeMethods.btTriangleMeshShape_recalcLocalAabb(Native);
		}

		public Vector3 LocalAabbMax
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btTriangleMeshShape_getLocalAabbMax(Native, out value);
				return value;
			}
		}

		public Vector3 LocalAabbMin
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btTriangleMeshShape_getLocalAabbMin(Native, out value);
				return value;
			}
		}

		public StridingMeshInterface MeshInterface
		{
			get
			{
				if (_meshInterface == null)
				{
					_meshInterface = new StridingMeshInterface(UnsafeNativeMethods.btTriangleMeshShape_getMeshInterface(Native));
				}
				return _meshInterface;
			}
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	internal struct TriangleMeshShapeData
	{
		public CollisionShapeFloatData CollisionShapeData;
		public StridingMeshInterfaceData MeshInterface;
		public IntPtr QuantizedFloatBvh;
		public IntPtr QuantizedDoubleBvh;
		public IntPtr TriangleInfoMap;
		public float CollisionMargin;
		public int Pad;

		public static int Offset(string fieldName) { return Marshal.OffsetOf(typeof(TriangleMeshShapeData), fieldName).ToInt32(); }
	}
}
