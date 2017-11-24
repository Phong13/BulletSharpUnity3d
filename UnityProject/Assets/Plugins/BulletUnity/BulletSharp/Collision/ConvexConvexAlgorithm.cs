using System;


namespace BulletSharp
{
	public class ConvexConvexAlgorithm : ActivatingCollisionAlgorithm
	{
		public class CreateFunc : CollisionAlgorithmCreateFunc
		{
			private ConvexPenetrationDepthSolver _pdSolver;

			internal CreateFunc(IntPtr native)
				: base(native, true)
			{
			}

			public CreateFunc(ConvexPenetrationDepthSolver pdSolver)
				: base(UnsafeNativeMethods.btConvexConvexAlgorithm_CreateFunc_new(pdSolver.Native), false)
			{
				_pdSolver = pdSolver;
			}

			public override CollisionAlgorithm CreateCollisionAlgorithm(CollisionAlgorithmConstructionInfo __unnamed0, 
				CollisionObjectWrapper body0Wrap, CollisionObjectWrapper body1Wrap)
			{
				return new ConvexConvexAlgorithm(UnsafeNativeMethods.btCollisionAlgorithmCreateFunc_CreateCollisionAlgorithm(
					Native, __unnamed0.Native, body0Wrap.Native, body1Wrap.Native));
			}

			public int MinimumPointsPerturbationThreshold
			{
				get { return  UnsafeNativeMethods.btConvexConvexAlgorithm_CreateFunc_getMinimumPointsPerturbationThreshold(Native);}
				set {  UnsafeNativeMethods.btConvexConvexAlgorithm_CreateFunc_setMinimumPointsPerturbationThreshold(Native, value);}
			}

			public int NumPerturbationIterations
			{
				get { return  UnsafeNativeMethods.btConvexConvexAlgorithm_CreateFunc_getNumPerturbationIterations(Native);}
				set {  UnsafeNativeMethods.btConvexConvexAlgorithm_CreateFunc_setNumPerturbationIterations(Native, value);}
			}

			public ConvexPenetrationDepthSolver PdSolver
			{
				get { return  _pdSolver;}
				set
				{
					UnsafeNativeMethods.btConvexConvexAlgorithm_CreateFunc_setPdSolver(Native, value.Native);
					_pdSolver = value;
				}
			}
		}

		internal ConvexConvexAlgorithm(IntPtr native)
			: base(native)
		{
		}

		public ConvexConvexAlgorithm(PersistentManifold mf, CollisionAlgorithmConstructionInfo ci,
			CollisionObjectWrapper body0Wrap, CollisionObjectWrapper body1Wrap, VoronoiSimplexSolver simplexSolver,
			ConvexPenetrationDepthSolver pdSolver, int numPerturbationIterations, int minimumPointsPerturbationThreshold)
			: base(UnsafeNativeMethods.btConvexConvexAlgorithm_new(mf.Native, ci.Native, body0Wrap.Native,
				body1Wrap.Native, simplexSolver.Native, pdSolver.Native, numPerturbationIterations,
				minimumPointsPerturbationThreshold))
		{
		}

		public void SetLowLevelOfDetail(bool useLowLevel)
		{
			UnsafeNativeMethods.btConvexConvexAlgorithm_setLowLevelOfDetail(Native, useLowLevel);
		}

		public PersistentManifold Manifold{ get { return  new PersistentManifold(UnsafeNativeMethods.btConvexConvexAlgorithm_getManifold(Native), true);} }
	}
}
