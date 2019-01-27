using System;
using System.Runtime.InteropServices;
using System.Security;
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
		public void BatchedUnitVectorGetSupportingVertexWithoutMargin(Vector3 vectors, Vector3 supportVerticesOut, int numVectors)
		{
			btConvexShape_batchedUnitVectorGetSupportingVertexWithoutMargin(_native, vectors._native, supportVerticesOut._native, numVectors);
		}
        */

        public void GetAabbNonVirtualRef(ref Matrix t, out Vector3 aabbMin, out Vector3 aabbMax)
        {
            btConvexShape_getAabbNonVirtual(_native, ref t, out aabbMin, out aabbMax);
        }

		public void GetAabbNonVirtual(Matrix t, out Vector3 aabbMin, out Vector3 aabbMax)
		{
			btConvexShape_getAabbNonVirtual(_native, ref t, out aabbMin, out aabbMax);
		}

        public void GetAabbSlowRef(ref Matrix t, out Vector3 aabbMin, out Vector3 aabbMax)
        {
            btConvexShape_getAabbSlow(_native, ref t, out aabbMin, out aabbMax);
        }

		public void GetAabbSlow(Matrix t, out Vector3 aabbMin, out Vector3 aabbMax)
		{
			btConvexShape_getAabbSlow(_native, ref t, out aabbMin, out aabbMax);
		}

		public void GetPreferredPenetrationDirection(int index, out Vector3 penetrationVector)
		{
			btConvexShape_getPreferredPenetrationDirection(_native, index, out penetrationVector);
		}

        public void LocalGetSupportingVertex(ref Vector3 vec, out Vector3 value)
        {
            btConvexShape_localGetSupportingVertex(_native, ref vec, out value);
        }

		public Vector3 LocalGetSupportingVertex(Vector3 vec)
		{
			Vector3 value;
			btConvexShape_localGetSupportingVertex(_native, ref vec, out value);
			return value;
		}

        public void LocalGetSupportingVertexWithoutMargin(ref Vector3 vec, out Vector3 value)
        {
            btConvexShape_localGetSupportingVertexWithoutMargin(_native, ref vec, out value);
        }

		public Vector3 LocalGetSupportingVertexWithoutMargin(Vector3 vec)
		{
			Vector3 value;
			btConvexShape_localGetSupportingVertexWithoutMargin(_native, ref vec, out value);
			return value;
		}

        public void LocalGetSupportVertexNonVirtual(ref Vector3 vec, out Vector3 value)
        {
            btConvexShape_localGetSupportVertexNonVirtual(_native, ref vec, out value);
        }

		public Vector3 LocalGetSupportVertexNonVirtual(Vector3 vec)
		{
			Vector3 value;
			btConvexShape_localGetSupportVertexNonVirtual(_native, ref vec, out value);
			return value;
		}

        public void LocalGetSupportVertexWithoutMarginNonVirtual(ref Vector3 vec, out Vector3 value)
        {
            btConvexShape_localGetSupportVertexWithoutMarginNonVirtual(_native, ref vec, out value);
        }

		public Vector3 LocalGetSupportVertexWithoutMarginNonVirtual(Vector3 vec)
		{
			Vector3 value;
			btConvexShape_localGetSupportVertexWithoutMarginNonVirtual(_native, ref vec, out value);
			return value;
		}

		public void Project(ref Matrix trans, Vector3 dir, out float minProj, out float maxProj, out Vector3 witnesPtMin, out Vector3 witnesPtMax)
		{
			btConvexShape_project(_native, ref trans, ref dir, out minProj, out maxProj, out witnesPtMin, out witnesPtMax);
		}

		public float MarginNonVirtual
		{
			get { return btConvexShape_getMarginNonVirtual(_native); }
		}

		public int NumPreferredPenetrationDirections
		{
			get { return btConvexShape_getNumPreferredPenetrationDirections(_native); }
		}

		//[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		//static extern void btConvexShape_batchedUnitVectorGetSupportingVertexWithoutMargin(IntPtr obj, IntPtr vectors, IntPtr supportVerticesOut, int numVectors);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConvexShape_getAabbNonVirtual(IntPtr obj, [In] ref Matrix t, [Out] out Vector3 aabbMin, [Out] out Vector3 aabbMax);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConvexShape_getAabbSlow(IntPtr obj, [In] ref Matrix t, [Out] out Vector3 aabbMin, [Out] out Vector3 aabbMax);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btConvexShape_getMarginNonVirtual(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btConvexShape_getNumPreferredPenetrationDirections(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConvexShape_getPreferredPenetrationDirection(IntPtr obj, int index, [Out] out Vector3 penetrationVector);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConvexShape_localGetSupportingVertex(IntPtr obj, [In] ref Vector3 vec, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConvexShape_localGetSupportingVertexWithoutMargin(IntPtr obj, [In] ref Vector3 vec, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConvexShape_localGetSupportVertexNonVirtual(IntPtr obj, [In] ref Vector3 vec, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConvexShape_localGetSupportVertexWithoutMarginNonVirtual(IntPtr obj, [In] ref Vector3 vec, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConvexShape_project(IntPtr obj, [In] ref Matrix trans, [In] ref Vector3 dir, [Out] out float minProj, [Out] out float maxProj, [Out] out Vector3 witnesPtMin, [Out] out Vector3 witnesPtMax);
	}
}
