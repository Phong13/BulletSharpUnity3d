using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
	public class NncgConstraintSolver : SequentialImpulseConstraintSolver
	{
		public NncgConstraintSolver()
			: base(btNNCGConstraintSolver_new(), false)
		{
		}

		public bool OnlyForNoneContact
		{
			get { return btNNCGConstraintSolver_getOnlyForNoneContact(_native); }
			set { btNNCGConstraintSolver_setOnlyForNoneContact(_native, value); }
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btNNCGConstraintSolver_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btNNCGConstraintSolver_getOnlyForNoneContact(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btNNCGConstraintSolver_setOnlyForNoneContact(IntPtr obj, bool value);
	}
}
