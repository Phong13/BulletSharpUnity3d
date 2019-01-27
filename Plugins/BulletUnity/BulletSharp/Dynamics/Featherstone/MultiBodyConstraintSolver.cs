using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
	public class MultiBodyConstraintSolver : SequentialImpulseConstraintSolver
	{
		public MultiBodyConstraintSolver()
			: base(btMultiBodyConstraintSolver_new(), false)
		{
		}
        /*
		public float SolveGroupCacheFriendlyFinish(CollisionObject bodies, int numBodies, ContactSolverInfo infoGlobal)
		{
			return btMultiBodyConstraintSolver_solveGroupCacheFriendlyFinish(_native, bodies._native, numBodies, infoGlobal._native);
		}

		public void SolveMultiBodyGroup(CollisionObject bodies, int numBodies, PersistentManifold manifold, int numManifolds, TypedConstraint constraints, int numConstraints, MultiBodyConstraint multiBodyConstraints, int numMultiBodyConstraints, ContactSolverInfo info, IDebugDraw debugDrawer, Dispatcher dispatcher)
		{
			btMultiBodyConstraintSolver_solveMultiBodyGroup(_native, bodies._native, numBodies, manifold._native, numManifolds, constraints._native, numConstraints, multiBodyConstraints._native, numMultiBodyConstraints, info._native, DebugDraw.GetUnmanaged(debugDrawer), dispatcher._native);
		}
        */
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btMultiBodyConstraintSolver_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btMultiBodyConstraintSolver_solveGroupCacheFriendlyFinish(IntPtr obj, IntPtr bodies, int numBodies, IntPtr infoGlobal);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBodyConstraintSolver_solveMultiBodyGroup(IntPtr obj, IntPtr bodies, int numBodies, IntPtr manifold, int numManifolds, IntPtr constraints, int numConstraints, IntPtr multiBodyConstraints, int numMultiBodyConstraints, IntPtr info, IntPtr debugDrawer, IntPtr dispatcher);
	}
}
