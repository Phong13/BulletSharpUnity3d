using System;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class Convex2DConvex2DAlgorithm : ActivatingCollisionAlgorithm
	{
		public class CreateFunc : CollisionAlgorithmCreateFunc
		{
			private ConvexPenetrationDepthSolver _pdSolver;
			private VoronoiSimplexSolver _simplexSolver;

			internal CreateFunc(IntPtr native)
				: base(native, true)
			{
			}

			public CreateFunc(VoronoiSimplexSolver simplexSolver, ConvexPenetrationDepthSolver pdSolver)
				: base(btConvex2dConvex2dAlgorithm_CreateFunc_new(simplexSolver.Native,
					pdSolver.Native), false)
			{
				_pdSolver = pdSolver;
				_simplexSolver = simplexSolver;
			}

			public override CollisionAlgorithm CreateCollisionAlgorithm(CollisionAlgorithmConstructionInfo __unnamed0,
				CollisionObjectWrapper body0Wrap, CollisionObjectWrapper body1Wrap)
			{
				return new Convex2DConvex2DAlgorithm(btCollisionAlgorithmCreateFunc_CreateCollisionAlgorithm(
					Native, __unnamed0.Native, body0Wrap.Native, body1Wrap.Native));
			}

			public int MinimumPointsPerturbationThreshold
			{
				get => btConvex2dConvex2dAlgorithm_CreateFunc_getMinimumPointsPerturbationThreshold(Native);
				set => btConvex2dConvex2dAlgorithm_CreateFunc_setMinimumPointsPerturbationThreshold(Native, value);
			}

			public int NumPerturbationIterations
			{
				get => btConvex2dConvex2dAlgorithm_CreateFunc_getNumPerturbationIterations(Native);
				set => btConvex2dConvex2dAlgorithm_CreateFunc_setNumPerturbationIterations(Native, value);
			}

			public ConvexPenetrationDepthSolver PdSolver
			{
				get => _pdSolver;
				set
				{
					btConvex2dConvex2dAlgorithm_CreateFunc_setPdSolver(Native, value.Native);
					_pdSolver = value;
				}
			}

			public VoronoiSimplexSolver SimplexSolver
			{
				get => _simplexSolver;
				set
				{
					btConvex2dConvex2dAlgorithm_CreateFunc_setSimplexSolver(Native, value.Native);
					_simplexSolver = value;
				}
			}
		}

		internal Convex2DConvex2DAlgorithm(IntPtr native)
			: base(native)
		{
		}

		public Convex2DConvex2DAlgorithm(PersistentManifold mf, CollisionAlgorithmConstructionInfo ci,
			CollisionObjectWrapper body0Wrap, CollisionObjectWrapper body1Wrap, VoronoiSimplexSolver simplexSolver,
			ConvexPenetrationDepthSolver pdSolver, int numPerturbationIterations, int minimumPointsPerturbationThreshold)
			: base(btConvex2dConvex2dAlgorithm_new(mf.Native, ci.Native, body0Wrap.Native,
				body1Wrap.Native, simplexSolver.Native, pdSolver.Native, numPerturbationIterations,
				minimumPointsPerturbationThreshold))
		{
		}

		public void SetLowLevelOfDetail(bool useLowLevel)
		{
			btConvex2dConvex2dAlgorithm_setLowLevelOfDetail(Native, useLowLevel);
		}

		public PersistentManifold Manifold => new PersistentManifold(btConvex2dConvex2dAlgorithm_getManifold(Native), true);
	}
}
