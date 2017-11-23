using BulletSharp.Math;
using System;


namespace BulletSharp
{
	public class Face : IDisposable
	{
		internal IntPtr Native;

		internal Face(IntPtr native)
		{
			Native = native;
		}

		public Face()
		{
			Native = UnsafeNativeMethods.btFace_new();
		}
		/*
		public AlignedIntArray Indices
		{
			get { return new AlignedIntArray(btFace_getIndices(Native)); }
		}

		public ScalarArray Plane
		{
			get { return new ScalarArray(btFace_getPlane(Native)); }
		}
		*/
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (Native != IntPtr.Zero)
			{
				UnsafeNativeMethods.btFace_delete(Native);
				Native = IntPtr.Zero;
			}
		}

		~Face()
		{
			Dispose(false);
		}
	}

	public class ConvexPolyhedron : IDisposable
	{
		internal IntPtr Native;

		//AlignedFaceArray _faces;
		AlignedVector3Array _uniqueEdges;
		AlignedVector3Array _vertices;

		internal ConvexPolyhedron(IntPtr native)
		{
			Native = native;
		}

		public ConvexPolyhedron()
		{
			Native = UnsafeNativeMethods.btConvexPolyhedron_new();
		}

		public void Initialize()
		{
			UnsafeNativeMethods.btConvexPolyhedron_initialize(Native);
		}

		public void ProjectRef(ref Matrix trans, ref Vector3 dir, out float minProj, out float maxProj,
			out Vector3 witnesPtMin, out Vector3 witnesPtMax)
		{
			UnsafeNativeMethods.btConvexPolyhedron_project(Native, ref trans, ref dir, out minProj,
				out maxProj, out witnesPtMin, out witnesPtMax);
		}

		public void Project(Matrix trans, Vector3 dir, out float minProj, out float maxProj,
			out Vector3 witnesPtMin, out Vector3 witnesPtMax)
		{
			UnsafeNativeMethods.btConvexPolyhedron_project(Native, ref trans, ref dir, out minProj,
				out maxProj, out witnesPtMin, out witnesPtMax);
		}

		public bool TestContainment()
		{
			return UnsafeNativeMethods.btConvexPolyhedron_testContainment(Native);
		}

		public Vector3 Extents
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btConvexPolyhedron_getExtents(Native, out value);
				return value;
			}
			set => UnsafeNativeMethods.btConvexPolyhedron_setExtents(Native, ref value);
		}
		/*
		public AlignedFaceArray Faces
		{
			get { return UnsafeNativeMethods.btConvexPolyhedron_getFaces(Native); }
		}
		*/
		public Vector3 LocalCenter
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btConvexPolyhedron_getLocalCenter(Native, out value);
				return value;
			}
			set => UnsafeNativeMethods.btConvexPolyhedron_setLocalCenter(Native, ref value);
		}

		public Vector3 C
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btConvexPolyhedron_getMC(Native, out value);
				return value;
			}
			set => UnsafeNativeMethods.btConvexPolyhedron_setMC(Native, ref value);
		}

		public Vector3 E
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btConvexPolyhedron_getME(Native, out value);
				return value;
			}
			set => UnsafeNativeMethods.btConvexPolyhedron_setME(Native, ref value);
		}

		public float Radius
		{
			get => UnsafeNativeMethods.btConvexPolyhedron_getRadius(Native);
			set => UnsafeNativeMethods.btConvexPolyhedron_setRadius(Native, value);
		}

		public AlignedVector3Array UniqueEdges
		{
			get
			{
				if (_uniqueEdges == null)
				{
					_uniqueEdges = new AlignedVector3Array(UnsafeNativeMethods.btConvexPolyhedron_getUniqueEdges(Native));
				}
				return _uniqueEdges;
			}
		}

		public AlignedVector3Array Vertices
		{
			get
			{
				if (_vertices == null)
				{
					_vertices = new AlignedVector3Array(UnsafeNativeMethods.btConvexPolyhedron_getVertices(Native));
				}
				return _vertices;
			}
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
				UnsafeNativeMethods.btConvexPolyhedron_delete(Native);
				Native = IntPtr.Zero;
			}
		}

		~ConvexPolyhedron()
		{
			Dispose(false);
		}
	}
}
