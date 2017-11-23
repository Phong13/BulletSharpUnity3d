

namespace BulletSharp
{
	public class GjkEpaPenetrationDepthSolver : ConvexPenetrationDepthSolver
	{
		public GjkEpaPenetrationDepthSolver()
			: base(UnsafeNativeMethods.btGjkEpaPenetrationDepthSolver_new())
		{
		}
	}
}
