using System;
using BulletSharp.Math;
using static BulletSharp.UnsafeNativeMethods;

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
			btUsageBitfield_reset(Native);
		}

		public bool Unused1
		{
			get => btUsageBitfield_getUnused1(Native);
			set => btUsageBitfield_setUnused1(Native, value);
		}

		public bool Unused2
		{
			get => btUsageBitfield_getUnused2(Native);
			set => btUsageBitfield_setUnused2(Native, value);
		}

		public bool Unused3
		{
			get => btUsageBitfield_getUnused3(Native);
			set => btUsageBitfield_setUnused3(Native, value);
		}

		public bool Unused4
		{
			get => btUsageBitfield_getUnused4(Native);
			set => btUsageBitfield_setUnused4(Native, value);
		}

		public bool UsedVertexA
		{
			get => btUsageBitfield_getUsedVertexA(Native);
			set => btUsageBitfield_setUsedVertexA(Native, value);
		}

		public bool UsedVertexB
		{
			get => btUsageBitfield_getUsedVertexB(Native);
			set => btUsageBitfield_setUsedVertexB(Native, value);
		}

		public bool UsedVertexC
		{
			get => btUsageBitfield_getUsedVertexC(Native);
			set => btUsageBitfield_setUsedVertexC(Native, value);
		}

		public bool UsedVertexD
		{
			get => btUsageBitfield_getUsedVertexD(Native);
			set => btUsageBitfield_setUsedVertexD(Native, value);
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
			Native = btSubSimplexClosestResult_new();
		}

		public void Reset()
		{
			btSubSimplexClosestResult_reset(Native);
		}

		public void SetBarycentricCoordinates()
		{
			btSubSimplexClosestResult_setBarycentricCoordinates(Native);
		}

		public void SetBarycentricCoordinates(float a)
		{
			btSubSimplexClosestResult_setBarycentricCoordinates2(Native, a);
		}

		public void SetBarycentricCoordinates(float a, float b)
		{
			btSubSimplexClosestResult_setBarycentricCoordinates3(Native, a, b);
		}

		public void SetBarycentricCoordinates(float a, float b, float c)
		{
			btSubSimplexClosestResult_setBarycentricCoordinates4(Native, a, b, c);
		}

		public void SetBarycentricCoordinates(float a, float b, float c, float d)
		{
			btSubSimplexClosestResult_setBarycentricCoordinates5(Native, a, b, c,
				d);
		}
		/*
		public FloatArray BarycentricCoords
		{
			get { return btSubSimplexClosestResult_getBarycentricCoords(Native); }
		}
		*/
		public Vector3 ClosestPointOnSimplex
		{
			get
			{
				Vector3 value;
				btSubSimplexClosestResult_getClosestPointOnSimplex(Native, out value);
				return value;
			}
			set => btSubSimplexClosestResult_setClosestPointOnSimplex(Native, ref value);
		}

		public bool Degenerate
		{
			get => btSubSimplexClosestResult_getDegenerate(Native);
			set => btSubSimplexClosestResult_setDegenerate(Native, value);
		}

		public bool IsValid => btSubSimplexClosestResult_isValid(Native);

		public UsageBitfield UsedVertices
		{
			get => new UsageBitfield(btSubSimplexClosestResult_getUsedVertices(Native));
			set => btSubSimplexClosestResult_setUsedVertices(Native, value.Native);
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
				btSubSimplexClosestResult_delete(Native);
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
			Native = btVoronoiSimplexSolver_new();
		}

		public void AddVertexRef(ref Vector3 w, ref Vector3 p, ref Vector3 q)
		{
			btVoronoiSimplexSolver_addVertex(Native, ref w, ref p, ref q);
		}

		public void AddVertex(Vector3 w, Vector3 p, Vector3 q)
		{
			btVoronoiSimplexSolver_addVertex(Native, ref w, ref p, ref q);
		}

		public void BackupClosest(out Vector3 v)
		{
			btVoronoiSimplexSolver_backup_closest(Native, out v);
		}

		public bool Closest(out Vector3 v)
		{
			return btVoronoiSimplexSolver_closest(Native, out v);
		}

		public bool ClosestPtPointTetrahedronRef(ref Vector3 p, ref Vector3 a, ref Vector3 b, ref Vector3 c,
			ref Vector3 d, SubSimplexClosestResult finalResult)
		{
			return btVoronoiSimplexSolver_closestPtPointTetrahedron(Native, ref p,
				ref a, ref b, ref c, ref d, finalResult.Native);
		}

		public bool ClosestPtPointTetrahedron(Vector3 p, Vector3 a, Vector3 b, Vector3 c,
			Vector3 d, SubSimplexClosestResult finalResult)
		{
			return btVoronoiSimplexSolver_closestPtPointTetrahedron(Native, ref p,
				ref a, ref b, ref c, ref d, finalResult.Native);
		}

		public bool ClosestPtPointTriangleRef(ref Vector3 p, ref Vector3 a, ref Vector3 b, ref Vector3 c,
			SubSimplexClosestResult result)
		{
			return btVoronoiSimplexSolver_closestPtPointTriangle(Native, ref p,
				ref a, ref b, ref c, result.Native);
		}

		public bool ClosestPtPointTriangle(Vector3 p, Vector3 a, Vector3 b, Vector3 c,
			SubSimplexClosestResult result)
		{
			return btVoronoiSimplexSolver_closestPtPointTriangle(Native, ref p,
				ref a, ref b, ref c, result.Native);
		}

		public void ComputePoints(out Vector3 p1, out Vector3 p2)
		{
			btVoronoiSimplexSolver_compute_points(Native, out p1, out p2);
		}

		public bool EmptySimplex()
		{
			return btVoronoiSimplexSolver_emptySimplex(Native);
		}

		public bool FullSimplex()
		{
			return btVoronoiSimplexSolver_fullSimplex(Native);
		}
		/*
		public int GetSimplex(Vector3[] pBuf, Vector3[] qBuf, Vector3[] yBuf)
		{
			return btVoronoiSimplexSolver_getSimplex(Native, pBuf, qBuf,
				yBuf);
		}
		*/
		public bool InSimplex(Vector3 w)
		{
			return btVoronoiSimplexSolver_inSimplex(Native, ref w);
		}

		public float MaxVertex()
		{
			return btVoronoiSimplexSolver_maxVertex(Native);
		}

		public int PointOutsideOfPlane(Vector3 p, Vector3 a, Vector3 b, Vector3 c,
			Vector3 d)
		{
			return btVoronoiSimplexSolver_pointOutsideOfPlane(Native, ref p, ref a,
				ref b, ref c, ref d);
		}

		public void ReduceVertices(UsageBitfield usedVerts)
		{
			btVoronoiSimplexSolver_reduceVertices(Native, usedVerts.Native);
		}

		public void RemoveVertex(int index)
		{
			btVoronoiSimplexSolver_removeVertex(Native, index);
		}

		public void Reset()
		{
			btVoronoiSimplexSolver_reset(Native);
		}

		public bool UpdateClosestVectorAndPoints()
		{
			return btVoronoiSimplexSolver_updateClosestVectorAndPoints(Native);
		}

		public SubSimplexClosestResult CachedBC
		{
			get => new SubSimplexClosestResult(btVoronoiSimplexSolver_getCachedBC(Native));
			set => btVoronoiSimplexSolver_setCachedBC(Native, value.Native);
		}

		public Vector3 CachedP1
		{
			get
			{
				Vector3 value;
				btVoronoiSimplexSolver_getCachedP1(Native, out value);
				return value;
			}
			set => btVoronoiSimplexSolver_setCachedP1(Native, ref value);
		}

		public Vector3 CachedP2
		{
			get
			{
				Vector3 value;
				btVoronoiSimplexSolver_getCachedP2(Native, out value);
				return value;
			}
			set => btVoronoiSimplexSolver_setCachedP2(Native, ref value);
		}

		public Vector3 CachedV
		{
			get
			{
				Vector3 value;
				btVoronoiSimplexSolver_getCachedV(Native, out value);
				return value;
			}
			set => btVoronoiSimplexSolver_setCachedV(Native, ref value);
		}

		public bool CachedValidClosest
		{
			get => btVoronoiSimplexSolver_getCachedValidClosest(Native);
			set => btVoronoiSimplexSolver_setCachedValidClosest(Native, value);
		}

		public float EqualVertexThreshold
		{
			get => btVoronoiSimplexSolver_getEqualVertexThreshold(Native);
			set => btVoronoiSimplexSolver_setEqualVertexThreshold(Native, value);
		}

		public Vector3 LastW
		{
			get
			{
				Vector3 value;
				btVoronoiSimplexSolver_getLastW(Native, out value);
				return value;
			}
			set => btVoronoiSimplexSolver_setLastW(Native, ref value);
		}

		public bool NeedsUpdate
		{
			get => btVoronoiSimplexSolver_getNeedsUpdate(Native);
			set => btVoronoiSimplexSolver_setNeedsUpdate(Native, value);
		}

		public int NumVertices
		{
			get => btVoronoiSimplexSolver_getNumVertices(Native);
			set => btVoronoiSimplexSolver_setNumVertices(Native, value);
		}
		/*
		public Vector3[] SimplexPointsP
		{
			get { return btVoronoiSimplexSolver_getSimplexPointsP(Native); }
		}

		public Vector3[] SimplexPointsQ
		{
			get { return btVoronoiSimplexSolver_getSimplexPointsQ(Native); }
		}

		public Vector3[] SimplexVectorW
		{
			get { return btVoronoiSimplexSolver_getSimplexVectorW(Native); }
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
					btVoronoiSimplexSolver_delete(Native);
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
