using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class MinkowskiPenetrationDepthSolver : ConvexPenetrationDepthSolver
	{
		public MinkowskiPenetrationDepthSolver()
			: base(btMinkowskiPenetrationDepthSolver_new())
		{
		}
	}
}
