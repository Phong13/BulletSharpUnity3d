

namespace BulletSharp
{
	public class ContinuousConvexCollision : ConvexCast
	{
		public ContinuousConvexCollision(ConvexShape shapeA, ConvexShape shapeB,
			VoronoiSimplexSolver simplexSolver, ConvexPenetrationDepthSolver penetrationDepthSolver)
			: base(UnsafeNativeMethods.btContinuousConvexCollision_new(shapeA.Native, shapeB.Native,
				simplexSolver.Native, penetrationDepthSolver.Native))
		{
		}

		public ContinuousConvexCollision(ConvexShape shapeA, StaticPlaneShape plane)
			: base(UnsafeNativeMethods.btContinuousConvexCollision_new2(shapeA.Native, plane.Native))
		{
		}
	}
}
