using System;
using BulletSharp.Math;


namespace BulletSharp
{
	public class UsageBitfield
	{
		internal IntPtr Native;

		internal UsageBitfield(IntPtr native)
		{
			Native = native;
		}

		public void Reset()
		{
			UnsafeNativeMethods.btUsageBitfield_reset(Native);
		}

		public bool Unused1
		{
			get => UnsafeNativeMethods.btUsageBitfield_getUnused1(Native);
			set => UnsafeNativeMethods.btUsageBitfield_setUnused1(Native, value);
		}

		public bool Unused2
		{
			get => UnsafeNativeMethods.btUsageBitfield_getUnused2(Native);
			set => UnsafeNativeMethods.btUsageBitfield_setUnused2(Native, value);
		}

		public bool Unused3
		{
			get => UnsafeNativeMethods.btUsageBitfield_getUnused3(Native);
			set => UnsafeNativeMethods.btUsageBitfield_setUnused3(Native, value);
		}

		public bool Unused4
		{
			get => UnsafeNativeMethods.btUsageBitfield_getUnused4(Native);
			set => UnsafeNativeMethods.btUsageBitfield_setUnused4(Native, value);
		}

		public bool UsedVertexA
		{
			get => UnsafeNativeMethods.btUsageBitfield_getUsedVertexA(Native);
			set => UnsafeNativeMethods.btUsageBitfield_setUsedVertexA(Native, value);
		}

		public bool UsedVertexB
		{
			get => UnsafeNativeMethods.btUsageBitfield_getUsedVertexB(Native);
			set => UnsafeNativeMethods.btUsageBitfield_setUsedVertexB(Native, value);
		}

		public bool UsedVertexC
		{
			get => UnsafeNativeMethods.btUsageBitfield_getUsedVertexC(Native);
			set => UnsafeNativeMethods.btUsageBitfield_setUsedVertexC(Native, value);
		}

		public bool UsedVertexD
		{
			get => UnsafeNativeMethods.btUsageBitfield_getUsedVertexD(Native);
			set => UnsafeNativeMethods.btUsageBitfield_setUsedVertexD(Native, value);
		}
	}

	public class SubSimplexClosestResult : IDisposable
	{
		internal IntPtr Native;

		internal SubSimplexClosestResult(IntPtr native)
		{
			Native = native;
		}

		public SubSimplexClosestResult()
		{
			Native = UnsafeNativeMethods.btSubSimplexClosestResult_new();
		}

		public void Reset()
		{
			UnsafeNativeMethods.btSubSimplexClosestResult_reset(Native);
		}

		public void SetBarycentricCoordinates()
		{
			UnsafeNativeMethods.btSubSimplexClosestResult_setBarycentricCoordinates(Native);
		}

		public void SetBarycentricCoordinates(float a)
		{
			UnsafeNativeMethods.btSubSimplexClosestResult_setBarycentricCoordinates2(Native, a);
		}

		public void SetBarycentricCoordinates(float a, float b)
		{
			UnsafeNativeMethods.btSubSimplexClosestResult_setBarycentricCoordinates3(Native, a, b);
		}

		public void SetBarycentricCoordinates(float a, float b, float c)
		{
			UnsafeNativeMethods.btSubSimplexClosestResult_setBarycentricCoordinates4(Native, a, b, c);
		}

		public void SetBarycentricCoordinates(float a, float b, float c, float d)
		{
			UnsafeNativeMethods.btSubSimplexClosestResult_setBarycentricCoordinates5(Native, a, b, c,
				d);
		}
		/*
		public FloatArray BarycentricCoords
		{
			get { return UnsafeNativeMethods.btSubSimplexClosestResult_getBarycentricCoords(Native); }
		}
		*/
		public Vector3 ClosestPointOnSimplex
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btSubSimplexClosestResult_getClosestPointOnSimplex(Native, out value);
				return value;
			}
			set => UnsafeNativeMethods.btSubSimplexClosestResult_setClosestPointOnSimplex(Native, ref value);
		}

		public bool Degenerate
		{
			get => UnsafeNativeMethods.btSubSimplexClosestResult_getDegenerate(Native);
			set => UnsafeNativeMethods.btSubSimplexClosestResult_setDegenerate(Native, value);
		}

		public bool IsValid => UnsafeNativeMethods.btSubSimplexClosestResult_isValid(Native);

		public UsageBitfield UsedVertices
		{
			get => new UsageBitfield(UnsafeNativeMethods.btSubSimplexClosestResult_getUsedVertices(Native));
			set => UnsafeNativeMethods.btSubSimplexClosestResult_setUsedVertices(Native, value.Native);
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
				UnsafeNativeMethods.btSubSimplexClosestResult_delete(Native);
				Native = IntPtr.Zero;
			}
		}

		~SubSimplexClosestResult()
		{
			Dispose(false);
		}
	}

	public class VoronoiSimplexSolver : IDisposable
	{
		internal IntPtr Native;
		private bool _preventDelete;

		internal VoronoiSimplexSolver(IntPtr native, bool preventDelete)
		{
			Native = native;
			_preventDelete = preventDelete;
		}

		public VoronoiSimplexSolver()
		{
			Native = UnsafeNativeMethods.btVoronoiSimplexSolver_new();
		}

		public void AddVertexRef(ref Vector3 w, ref Vector3 p, ref Vector3 q)
		{
			UnsafeNativeMethods.btVoronoiSimplexSolver_addVertex(Native, ref w, ref p, ref q);
		}

		public void AddVertex(Vector3 w, Vector3 p, Vector3 q)
		{
			UnsafeNativeMethods.btVoronoiSimplexSolver_addVertex(Native, ref w, ref p, ref q);
		}

		public void BackupClosest(out Vector3 v)
		{
			UnsafeNativeMethods.btVoronoiSimplexSolver_backup_closest(Native, out v);
		}

		public bool Closest(out Vector3 v)
		{
			return UnsafeNativeMethods.btVoronoiSimplexSolver_closest(Native, out v);
		}

		public bool ClosestPtPointTetrahedronRef(ref Vector3 p, ref Vector3 a, ref Vector3 b, ref Vector3 c,
			ref Vector3 d, SubSimplexClosestResult finalResult)
		{
			return UnsafeNativeMethods.btVoronoiSimplexSolver_closestPtPointTetrahedron(Native, ref p,
				ref a, ref b, ref c, ref d, finalResult.Native);
		}

		public bool ClosestPtPointTetrahedron(Vector3 p, Vector3 a, Vector3 b, Vector3 c,
			Vector3 d, SubSimplexClosestResult finalResult)
		{
			return UnsafeNativeMethods.btVoronoiSimplexSolver_closestPtPointTetrahedron(Native, ref p,
				ref a, ref b, ref c, ref d, finalResult.Native);
		}

		public bool ClosestPtPointTriangleRef(ref Vector3 p, ref Vector3 a, ref Vector3 b, ref Vector3 c,
			SubSimplexClosestResult result)
		{
			return UnsafeNativeMethods.btVoronoiSimplexSolver_closestPtPointTriangle(Native, ref p,
				ref a, ref b, ref c, result.Native);
		}

		public bool ClosestPtPointTriangle(Vector3 p, Vector3 a, Vector3 b, Vector3 c,
			SubSimplexClosestResult result)
		{
			return UnsafeNativeMethods.btVoronoiSimplexSolver_closestPtPointTriangle(Native, ref p,
				ref a, ref b, ref c, result.Native);
		}

		public void ComputePoints(out Vector3 p1, out Vector3 p2)
		{
			UnsafeNativeMethods.btVoronoiSimplexSolver_compute_points(Native, out p1, out p2);
		}

		public bool EmptySimplex()
		{
			return UnsafeNativeMethods.btVoronoiSimplexSolver_emptySimplex(Native);
		}

		public bool FullSimplex()
		{
			return UnsafeNativeMethods.btVoronoiSimplexSolver_fullSimplex(Native);
		}
		/*
		public int GetSimplex(Vector3[] pBuf, Vector3[] qBuf, Vector3[] yBuf)
		{
			return UnsafeNativeMethods.btVoronoiSimplexSolver_getSimplex(Native, pBuf, qBuf,
				yBuf);
		}
		*/
		public bool InSimplex(Vector3 w)
		{
			return UnsafeNativeMethods.btVoronoiSimplexSolver_inSimplex(Native, ref w);
		}

		public float MaxVertex()
		{
			return UnsafeNativeMethods.btVoronoiSimplexSolver_maxVertex(Native);
		}

		public int PointOutsideOfPlane(Vector3 p, Vector3 a, Vector3 b, Vector3 c,
			Vector3 d)
		{
			return UnsafeNativeMethods.btVoronoiSimplexSolver_pointOutsideOfPlane(Native, ref p, ref a,
				ref b, ref c, ref d);
		}

		public void ReduceVertices(UsageBitfield usedVerts)
		{
			UnsafeNativeMethods.btVoronoiSimplexSolver_reduceVertices(Native, usedVerts.Native);
		}

		public void RemoveVertex(int index)
		{
			UnsafeNativeMethods.btVoronoiSimplexSolver_removeVertex(Native, index);
		}

		public void Reset()
		{
			UnsafeNativeMethods.btVoronoiSimplexSolver_reset(Native);
		}

		public bool UpdateClosestVectorAndPoints()
		{
			return UnsafeNativeMethods.btVoronoiSimplexSolver_updateClosestVectorAndPoints(Native);
		}

		public SubSimplexClosestResult CachedBC
		{
			get => new SubSimplexClosestResult(UnsafeNativeMethods.btVoronoiSimplexSolver_getCachedBC(Native));
			set => UnsafeNativeMethods.btVoronoiSimplexSolver_setCachedBC(Native, value.Native);
		}

		public Vector3 CachedP1
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btVoronoiSimplexSolver_getCachedP1(Native, out value);
				return value;
			}
			set => UnsafeNativeMethods.btVoronoiSimplexSolver_setCachedP1(Native, ref value);
		}

		public Vector3 CachedP2
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btVoronoiSimplexSolver_getCachedP2(Native, out value);
				return value;
			}
			set => UnsafeNativeMethods.btVoronoiSimplexSolver_setCachedP2(Native, ref value);
		}

		public Vector3 CachedV
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btVoronoiSimplexSolver_getCachedV(Native, out value);
				return value;
			}
			set => UnsafeNativeMethods.btVoronoiSimplexSolver_setCachedV(Native, ref value);
		}

		public bool CachedValidClosest
		{
			get => UnsafeNativeMethods.btVoronoiSimplexSolver_getCachedValidClosest(Native);
			set => UnsafeNativeMethods.btVoronoiSimplexSolver_setCachedValidClosest(Native, value);
		}

		public float EqualVertexThreshold
		{
			get => UnsafeNativeMethods.btVoronoiSimplexSolver_getEqualVertexThreshold(Native);
			set => UnsafeNativeMethods.btVoronoiSimplexSolver_setEqualVertexThreshold(Native, value);
		}

		public Vector3 LastW
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btVoronoiSimplexSolver_getLastW(Native, out value);
				return value;
			}
			set => UnsafeNativeMethods.btVoronoiSimplexSolver_setLastW(Native, ref value);
		}

		public bool NeedsUpdate
		{
			get => UnsafeNativeMethods.btVoronoiSimplexSolver_getNeedsUpdate(Native);
			set => UnsafeNativeMethods.btVoronoiSimplexSolver_setNeedsUpdate(Native, value);
		}

		public int NumVertices
		{
			get => UnsafeNativeMethods.btVoronoiSimplexSolver_getNumVertices(Native);
			set => UnsafeNativeMethods.btVoronoiSimplexSolver_setNumVertices(Native, value);
		}
		/*
		public Vector3[] SimplexPointsP
		{
			get { return UnsafeNativeMethods.btVoronoiSimplexSolver_getSimplexPointsP(Native); }
		}

		public Vector3[] SimplexPointsQ
		{
			get { return UnsafeNativeMethods.btVoronoiSimplexSolver_getSimplexPointsQ(Native); }
		}

		public Vector3[] SimplexVectorW
		{
			get { return UnsafeNativeMethods.btVoronoiSimplexSolver_getSimplexVectorW(Native); }
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
				if (!_preventDelete)
				{
					UnsafeNativeMethods.btVoronoiSimplexSolver_delete(Native);
				}
				Native = IntPtr.Zero;
			}
		}

		~VoronoiSimplexSolver()
		{
			Dispose(false);
		}
	}
}
