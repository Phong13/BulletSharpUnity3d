using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class GjkConvexCast : ConvexCast
	{
		public GjkConvexCast(ConvexShape convexA, ConvexShape convexB, VoronoiSimplexSolver simplexSolver)
			: base(btGjkConvexCast_new(convexA.Native, convexB.Native, simplexSolver.Native))
		{
		}
	}
}
