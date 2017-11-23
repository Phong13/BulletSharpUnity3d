using System;


namespace BulletSharp
{
	public class GImpactCollisionAlgorithm : ActivatingCollisionAlgorithm
	{
		public class CreateFunc : CollisionAlgorithmCreateFunc
		{
			internal CreateFunc(IntPtr native)
				: base(native, true)
			{
			}

			public CreateFunc()
				: base(UnsafeNativeMethods.btGImpactCollisionAlgorithm_CreateFunc_new(), false)
			{
			}

			public override CollisionAlgorithm CreateCollisionAlgorithm(CollisionAlgorithmConstructionInfo __unnamed0,
				CollisionObjectWrapper body0Wrap, CollisionObjectWrapper body1Wrap)
			{
				return new GImpactCollisionAlgorithm(UnsafeNativeMethods.btCollisionAlgorithmCreateFunc_CreateCollisionAlgorithm(
					Native, __unnamed0.Native, body0Wrap.Native, body1Wrap.Native));
			}
		}

		internal GImpactCollisionAlgorithm(IntPtr native)
			: base(native)
		{
		}

		public GImpactCollisionAlgorithm(CollisionAlgorithmConstructionInfo constructionInfo, CollisionObjectWrapper body0Wrap,
			CollisionObjectWrapper body1Wrap)
			: base(UnsafeNativeMethods.btGImpactCollisionAlgorithm_new(constructionInfo.Native, body0Wrap.Native,
				body1Wrap.Native))
		{
		}

		public void GImpactVsCompoundShape(CollisionObjectWrapper body0Wrap, CollisionObjectWrapper body1Wrap,
			GImpactShapeInterface shape0, CompoundShape shape1, bool swapped)
		{
			UnsafeNativeMethods.btGImpactCollisionAlgorithm_gimpact_vs_compoundshape(Native, body0Wrap.Native,
				body1Wrap.Native, shape0.Native, shape1.Native, swapped);
		}

		public void GImpactVsConcave(CollisionObjectWrapper body0Wrap, CollisionObjectWrapper body1Wrap,
			GImpactShapeInterface shape0, ConcaveShape shape1, bool swapped)
		{
			UnsafeNativeMethods.btGImpactCollisionAlgorithm_gimpact_vs_concave(Native, body0Wrap.Native,
				body1Wrap.Native, shape0.Native, shape1.Native, swapped);
		}

		public void GImpactVsGImpact(CollisionObjectWrapper body0Wrap, CollisionObjectWrapper body1Wrap,
			GImpactShapeInterface shape0, GImpactShapeInterface shape1)
		{
			UnsafeNativeMethods.btGImpactCollisionAlgorithm_gimpact_vs_gimpact(Native, body0Wrap.Native,
				body1Wrap.Native, shape0.Native, shape1.Native);
		}

		public void GImpactVsShape(CollisionObjectWrapper body0Wrap, CollisionObjectWrapper body1Wrap,
			GImpactShapeInterface shape0, CollisionShape shape1, bool swapped)
		{
			UnsafeNativeMethods.btGImpactCollisionAlgorithm_gimpact_vs_shape(Native, body0Wrap.Native,
				body1Wrap.Native, shape0.Native, shape1.Native, swapped);
		}

		public ManifoldResult InternalGetResultOut()
		{
			return new ManifoldResult(UnsafeNativeMethods.btGImpactCollisionAlgorithm_internalGetResultOut(Native));
		}

		public static void RegisterAlgorithm(CollisionDispatcher dispatcher)
		{
			UnsafeNativeMethods.btGImpactCollisionAlgorithm_registerAlgorithm(dispatcher.Native);
		}

		public int Face0
		{
			get => UnsafeNativeMethods.btGImpactCollisionAlgorithm_getFace0(Native);
			set => UnsafeNativeMethods.btGImpactCollisionAlgorithm_setFace0(Native, value);
		}

		public int Face1
		{
			get => UnsafeNativeMethods.btGImpactCollisionAlgorithm_getFace1(Native);
			set => UnsafeNativeMethods.btGImpactCollisionAlgorithm_setFace1(Native, value);
		}

		public int Part0
		{
			get => UnsafeNativeMethods.btGImpactCollisionAlgorithm_getPart0(Native);
			set => UnsafeNativeMethods.btGImpactCollisionAlgorithm_setPart0(Native, value);
		}

		public int Part1
		{
			get => UnsafeNativeMethods.btGImpactCollisionAlgorithm_getPart1(Native);
			set => UnsafeNativeMethods.btGImpactCollisionAlgorithm_setPart1(Native, value);
		}
	}
}
