

namespace BulletSharp
{
	public class MinkowskiPenetrationDepthSolver : ConvexPenetrationDepthSolver
	{
		public MinkowskiPenetrationDepthSolver()
			: base(UnsafeNativeMethods.btMinkowskiPenetrationDepthSolver_new())
		{
		}
	}
}
