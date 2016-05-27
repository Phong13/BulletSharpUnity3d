using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
	public class GjkEpaPenetrationDepthSolver : ConvexPenetrationDepthSolver
	{
		public GjkEpaPenetrationDepthSolver()
			: base(btGjkEpaPenetrationDepthSolver_new())
		{
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btGjkEpaPenetrationDepthSolver_new();
	}
}
