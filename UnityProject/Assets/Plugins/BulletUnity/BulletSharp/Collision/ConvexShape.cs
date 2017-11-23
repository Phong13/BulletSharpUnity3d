using System;
using BulletSharp.Math;


namespace BulletSharp
{
	public class ConvexShape : CollisionShape
	{
		internal ConvexShape(IntPtr native, bool preventDelete = false)
			: base(native, preventDelete)
		{
		}
		/*
		public void BatchedUnitVectorGetSupportingVertexWithoutMargin(Vector3 vectors,
			Vector3 supportVerticesOut, int numVectors)
		{
			btConvexShape_batchedUnitVectorGetSupportingVertexWithoutMargin(Native,
				vectors.Native, supportVerticesOut.Native, numVectors);
		}
		*/

		public void GetAabbNonVirtual(Matrix t, out Vector3 aabbMin, out Vector3 aabbMax)
		{
			UnsafeNativeMethods.btConvexShape_getAabbNonVirtual(Native, ref t, out aabbMin, out aabbMax);
		}

		public void GetAabbSlow(Matrix t, out Vector3 aabbMin, out Vector3 aabbMax)
		{
			UnsafeNativeMethods.btConvexShape_getAabbSlow(Native, ref t, out aabbMin, out aabbMax);
		}

		public void GetPreferredPenetrationDirection(int index, out Vector3 penetrationVector)
		{
			UnsafeNativeMethods.btConvexShape_getPreferredPenetrationDirection(Native, index, out penetrationVector);
		}

		public Vector3 LocalGetSupportingVertex(Vector3 vec)
		{
			Vector3 value;
			UnsafeNativeMethods.btConvexShape_localGetSupportingVertex(Native, ref vec, out value);
			return value;
		}

		public Vector3 LocalGetSupportingVertexWithoutMargin(Vector3 vec)
		{
			Vector3 value;
			UnsafeNativeMethods.btConvexShape_localGetSupportingVertexWithoutMargin(Native, ref vec,
				out value);
			return value;
		}

		public Vector3 LocalGetSupportVertexNonVirtual(Vector3 vec)
		{
			Vector3 value;
			UnsafeNativeMethods.btConvexShape_localGetSupportVertexNonVirtual(Native, ref vec, out value);
			return value;
		}

		public Vector3 LocalGetSupportVertexWithoutMarginNonVirtual(Vector3 vec)
		{
			Vector3 value;
			UnsafeNativeMethods.btConvexShape_localGetSupportVertexWithoutMarginNonVirtual(Native, ref vec,
				out value);
			return value;
		}

		public void ProjectRef(ref Matrix trans, ref Vector3 dir, out float minProj, out float maxProj,
			out Vector3 witnesPtMin, out Vector3 witnesPtMax)
		{
			UnsafeNativeMethods.btConvexShape_project(Native, ref trans, ref dir, out minProj, out maxProj,
				out witnesPtMin, out witnesPtMax);
		}

		public void Project(Matrix trans, Vector3 dir, out float minProj, out float maxProj,
			out Vector3 witnesPtMin, out Vector3 witnesPtMax)
		{
			UnsafeNativeMethods.btConvexShape_project(Native, ref trans, ref dir, out minProj, out maxProj,
				out witnesPtMin, out witnesPtMax);
		}

		public float MarginNonVirtual => UnsafeNativeMethods.btConvexShape_getMarginNonVirtual(Native);

		public int NumPreferredPenetrationDirections => UnsafeNativeMethods.btConvexShape_getNumPreferredPenetrationDirections(Native);
	}
}
