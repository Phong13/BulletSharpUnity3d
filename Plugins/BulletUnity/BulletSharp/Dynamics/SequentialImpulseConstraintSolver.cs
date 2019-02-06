using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
	public class SequentialImpulseConstraintSolver : ConstraintSolver
	{
		internal SequentialImpulseConstraintSolver(IntPtr native, bool preventDelete)
			: base(native, preventDelete)
		{
		}

		public SequentialImpulseConstraintSolver()
			: base(btSequentialImpulseConstraintSolver_new(), false)
		{
		}

		public ulong BtRand2()
		{
			return btSequentialImpulseConstraintSolver_btRand2(_native);
		}

		public int BtRandInt2(int n)
		{
			return btSequentialImpulseConstraintSolver_btRandInt2(_native, n);
		}
/*
		public void SetConstraintRowSolverGeneric(SingleConstraintRowSolver rowSolver)
		{
			btSequentialImpulseConstraintSolver_setConstraintRowSolverGeneric(_native, rowSolver._native);
		}

		public void SetConstraintRowSolverLowerLimit(SingleConstraintRowSolver rowSolver)
		{
			btSequentialImpulseConstraintSolver_setConstraintRowSolverLowerLimit(_native, rowSolver._native);
		}

		public SingleConstraintRowSolver ActiveConstraintRowSolverGeneric
		{
			get { return btSequentialImpulseConstraintSolver_getActiveConstraintRowSolverGeneric(_native); }
		}

		public SingleConstraintRowSolver ActiveConstraintRowSolverLowerLimit
		{
			get { return btSequentialImpulseConstraintSolver_getActiveConstraintRowSolverLowerLimit(_native); }
		}
*/
		public ulong RandSeed
		{
			get { return btSequentialImpulseConstraintSolver_getRandSeed(_native); }
			set { btSequentialImpulseConstraintSolver_setRandSeed(_native, value); }
		}
/*
		public SingleConstraintRowSolver ScalarConstraintRowSolverGeneric
		{
			get { return btSequentialImpulseConstraintSolver_getScalarConstraintRowSolverGeneric(_native); }
		}

		public SingleConstraintRowSolver ScalarConstraintRowSolverLowerLimit
		{
			get { return btSequentialImpulseConstraintSolver_getScalarConstraintRowSolverLowerLimit(_native); }
		}

		public SingleConstraintRowSolver SSE2ConstraintRowSolverGeneric
		{
			get { return btSequentialImpulseConstraintSolver_getSSE2ConstraintRowSolverGeneric(_native); }
		}

		public SingleConstraintRowSolver SSE2ConstraintRowSolverLowerLimit
		{
			get { return btSequentialImpulseConstraintSolver_getSSE2ConstraintRowSolverLowerLimit(_native); }
		}

		public SingleConstraintRowSolver SSE41ConstraintRowSolverGeneric
		{
			get { return btSequentialImpulseConstraintSolver_getSSE4_1ConstraintRowSolverGeneric(_native); }
		}

		public SingleConstraintRowSolver SSE41ConstraintRowSolverLowerLimit
		{
			get { return btSequentialImpulseConstraintSolver_getSSE4_1ConstraintRowSolverLowerLimit(_native); }
		}
*/
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSequentialImpulseConstraintSolver_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern ulong btSequentialImpulseConstraintSolver_btRand2(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btSequentialImpulseConstraintSolver_btRandInt2(IntPtr obj, int n);
		//[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		//static extern IntPtr btSequentialImpulseConstraintSolver_getActiveConstraintRowSolverGeneric(IntPtr obj);
		//[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		//static extern IntPtr btSequentialImpulseConstraintSolver_getActiveConstraintRowSolverLowerLimit(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern ulong btSequentialImpulseConstraintSolver_getRandSeed(IntPtr obj);
		//[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		//static extern IntPtr btSequentialImpulseConstraintSolver_getScalarConstraintRowSolverGeneric(IntPtr obj);
		//[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		//static extern IntPtr btSequentialImpulseConstraintSolver_getScalarConstraintRowSolverLowerLimit(IntPtr obj);
		//[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		//static extern IntPtr btSequentialImpulseConstraintSolver_getSSE2ConstraintRowSolverGeneric(IntPtr obj);
		//[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		//static extern IntPtr btSequentialImpulseConstraintSolver_getSSE2ConstraintRowSolverLowerLimit(IntPtr obj);
		//[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		//static extern IntPtr btSequentialImpulseConstraintSolver_getSSE4_1ConstraintRowSolverGeneric(IntPtr obj);
		//[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		//static extern IntPtr btSequentialImpulseConstraintSolver_getSSE4_1ConstraintRowSolverLowerLimit(IntPtr obj);
		//[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		//static extern void btSequentialImpulseConstraintSolver_setConstraintRowSolverGeneric(IntPtr obj, IntPtr rowSolver);
		//[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		//static extern void btSequentialImpulseConstraintSolver_setConstraintRowSolverLowerLimit(IntPtr obj, IntPtr rowSolver);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSequentialImpulseConstraintSolver_setRandSeed(IntPtr obj, ulong seed);
	}
}
