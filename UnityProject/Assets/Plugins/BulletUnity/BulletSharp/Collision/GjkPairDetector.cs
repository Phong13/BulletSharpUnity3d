using System;
using BulletSharp.Math;


namespace BulletSharp
{
	public class GjkPairDetector : DiscreteCollisionDetectorInterface
	{
		internal GjkPairDetector(IntPtr native)
			: base(native)
		{
		}

		public GjkPairDetector(ConvexShape objectA, ConvexShape objectB, VoronoiSimplexSolver simplexSolver,
			ConvexPenetrationDepthSolver penetrationDepthSolver)
			: base(UnsafeNativeMethods.btGjkPairDetector_new(objectA.Native, objectB.Native, simplexSolver.Native,
				(penetrationDepthSolver != null) ? penetrationDepthSolver.Native : IntPtr.Zero))
		{
		}

		public GjkPairDetector(ConvexShape objectA, ConvexShape objectB, int shapeTypeA,
			int shapeTypeB, float marginA, float marginB, VoronoiSimplexSolver simplexSolver,
			ConvexPenetrationDepthSolver penetrationDepthSolver)
			: base(UnsafeNativeMethods.btGjkPairDetector_new2(objectA.Native, objectB.Native, shapeTypeA,
				shapeTypeB, marginA, marginB, simplexSolver.Native, (penetrationDepthSolver != null) ? penetrationDepthSolver.Native : IntPtr.Zero))
		{
		}

		public void GetClosestPointsNonVirtual(ClosestPointInput input, Result output,
			IDebugDraw debugDraw)
		{
			UnsafeNativeMethods.btGjkPairDetector_getClosestPointsNonVirtual(Native, input.Native,
				output.Native, DebugDraw.GetUnmanaged(debugDraw));
		}

		public void SetIgnoreMargin(bool ignoreMargin)
		{
			UnsafeNativeMethods.btGjkPairDetector_setIgnoreMargin(Native, ignoreMargin);
		}

		public void SetMinkowskiA(ConvexShape minkA)
		{
			UnsafeNativeMethods.btGjkPairDetector_setMinkowskiA(Native, minkA.Native);
		}

		public void SetMinkowskiB(ConvexShape minkB)
		{
			UnsafeNativeMethods.btGjkPairDetector_setMinkowskiB(Native, minkB.Native);
		}

		public void SetPenetrationDepthSolver(ConvexPenetrationDepthSolver penetrationDepthSolver)
		{
			UnsafeNativeMethods.btGjkPairDetector_setPenetrationDepthSolver(Native, penetrationDepthSolver.Native);
		}

		public Vector3 CachedSeparatingAxis
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btGjkPairDetector_getCachedSeparatingAxis(Native, out value);
				return value;
			}
			set => UnsafeNativeMethods.btGjkPairDetector_setCachedSeparatingAxis(Native, ref value);
		}

		public float CachedSeparatingDistance => UnsafeNativeMethods.btGjkPairDetector_getCachedSeparatingDistance(Native);

		public int CatchDegeneracies
		{
			get => UnsafeNativeMethods.btGjkPairDetector_getCatchDegeneracies(Native);
			set => UnsafeNativeMethods.btGjkPairDetector_setCatchDegeneracies(Native, value);
		}

		public int CurIter
		{
			get => UnsafeNativeMethods.btGjkPairDetector_getCurIter(Native);
			set => UnsafeNativeMethods.btGjkPairDetector_setCurIter(Native, value);
		}

		public int DegenerateSimplex
		{
			get => UnsafeNativeMethods.btGjkPairDetector_getDegenerateSimplex(Native);
			set => UnsafeNativeMethods.btGjkPairDetector_setDegenerateSimplex(Native, value);
		}

		public int FixContactNormalDirection
		{
			get => UnsafeNativeMethods.btGjkPairDetector_getFixContactNormalDirection(Native);
			set => UnsafeNativeMethods.btGjkPairDetector_setFixContactNormalDirection(Native, value);
		}

		public int LastUsedMethod
		{
			get => UnsafeNativeMethods.btGjkPairDetector_getLastUsedMethod(Native);
			set => UnsafeNativeMethods.btGjkPairDetector_setLastUsedMethod(Native, value);
		}
	}
}
