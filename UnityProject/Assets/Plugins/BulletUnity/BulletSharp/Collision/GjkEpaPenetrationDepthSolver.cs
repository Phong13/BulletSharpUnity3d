using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class GjkEpaPenetrationDepthSolver : ConvexPenetrationDepthSolver
	{
		public GjkEpaPenetrationDepthSolver()
			: base(btGjkEpaPenetrationDepthSolver_new())
		{
		}
	}
}
