using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp.SoftBody
{
	public class SoftBodySolver : IDisposable
	{
		internal IntPtr _native;

		internal SoftBodySolver(IntPtr native)
		{
			_native = native;
		}

		public bool CheckInitialized()
		{
			return btSoftBodySolver_checkInitialized(_native);
		}

		public void CopyBackToSoftBodies()
		{
			btSoftBodySolver_copyBackToSoftBodies(_native);
		}

		public void CopyBackToSoftBodies(bool bMove)
		{
			btSoftBodySolver_copyBackToSoftBodies2(_native, bMove);
		}
        /*
		public void Optimize(AlignedObjectArray softBodies)
		{
			btSoftBodySolver_optimize(_native, softBodies._native);
		}

		public void Optimize(AlignedObjectArray softBodies, bool forceUpdate)
		{
			btSoftBodySolver_optimize2(_native, softBodies._native, forceUpdate);
		}
        */
		public void PredictMotion(float solverdt)
		{
			btSoftBodySolver_predictMotion(_native, solverdt);
		}
        /*
		public void ProcessCollision(SoftBody __unnamed0, CollisionObjectWrapper __unnamed1)
		{
			btSoftBodySolver_processCollision(_native, __unnamed0._native, __unnamed1._native);
		}

		public void ProcessCollision(SoftBody __unnamed0, SoftBody __unnamed1)
		{
			btSoftBodySolver_processCollision2(_native, __unnamed0._native, __unnamed1._native);
		}
        */
		public void SolveConstraints(float solverdt)
		{
			btSoftBodySolver_solveConstraints(_native, solverdt);
		}

		public void UpdateSoftBodies()
		{
			btSoftBodySolver_updateSoftBodies(_native);
		}

		public int NumberOfPositionIterations
		{
			get { return btSoftBodySolver_getNumberOfPositionIterations(_native); }
			set { btSoftBodySolver_setNumberOfPositionIterations(_native, value); }
		}

		public int NumberOfVelocityIterations
		{
			get { return btSoftBodySolver_getNumberOfVelocityIterations(_native); }
			set { btSoftBodySolver_setNumberOfVelocityIterations(_native, value); }
		}
        /*
		public SolverTypes SolverType
		{
			get { return btSoftBodySolver_getSolverType(_native); }
		}
        */
		public float TimeScale
		{
			get { return btSoftBodySolver_getTimeScale(_native); }
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (_native != IntPtr.Zero)
			{
				btSoftBodySolver_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~SoftBodySolver()
		{
			Dispose(false);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btSoftBodySolver_checkInitialized(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBodySolver_copyBackToSoftBodies(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBodySolver_copyBackToSoftBodies2(IntPtr obj, bool bMove);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btSoftBodySolver_getNumberOfPositionIterations(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btSoftBodySolver_getNumberOfVelocityIterations(IntPtr obj);
		//[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		//static extern SolverTypes btSoftBodySolver_getSolverType(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btSoftBodySolver_getTimeScale(IntPtr obj);
		//[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		//static extern void btSoftBodySolver_optimize(IntPtr obj, IntPtr softBodies);
		//[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		//static extern void btSoftBodySolver_optimize2(IntPtr obj, IntPtr softBodies, bool forceUpdate);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBodySolver_predictMotion(IntPtr obj, float solverdt);
		//[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		//static extern void btSoftBodySolver_processCollision(IntPtr obj, IntPtr __unnamed0, IntPtr __unnamed1);
		//[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		//static extern void btSoftBodySolver_processCollision2(IntPtr obj, IntPtr __unnamed0, IntPtr __unnamed1);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBodySolver_setNumberOfPositionIterations(IntPtr obj, int iterations);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBodySolver_setNumberOfVelocityIterations(IntPtr obj, int iterations);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBodySolver_solveConstraints(IntPtr obj, float solverdt);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBodySolver_updateSoftBodies(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBodySolver_delete(IntPtr obj);
	}
    /*
	public class SoftBodySolverOutput : IDisposable
	{
		internal IntPtr _native;

		internal SoftBodySolverOutput(IntPtr native)
		{
			_native = native;
		}

		public void CopySoftBodyToVertexBuffer(SoftBody softBody, VertexBufferDescriptor vertexBuffer)
		{
			btSoftBodySolverOutput_copySoftBodyToVertexBuffer(_native, softBody._native, vertexBuffer._native);
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (_native != IntPtr.Zero)
			{
				btSoftBodySolverOutput_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~SoftBodySolverOutput()
		{
			Dispose(false);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBodySolverOutput_copySoftBodyToVertexBuffer(IntPtr obj, IntPtr softBody, IntPtr vertexBuffer);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBodySolverOutput_delete(IntPtr obj);
	}*/
}
