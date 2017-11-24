using System;


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
			return UnsafeNativeMethods.btSoftBodySolver_checkInitialized(_native);
		}

		public void CopyBackToSoftBodies(bool bMove = true)
		{
			UnsafeNativeMethods.btSoftBodySolver_copyBackToSoftBodies(_native, bMove);
		}
		/*
		public void Optimize(AlignedObjectArray softBodies, bool forceUpdate = false)
		{
			UnsafeNativeMethods.btSoftBodySolver_optimize(_native, softBodies._native, forceUpdate);
		}
		*/
		public void PredictMotion(float solverdt)
		{
			UnsafeNativeMethods.btSoftBodySolver_predictMotion(_native, solverdt);
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
			UnsafeNativeMethods.btSoftBodySolver_solveConstraints(_native, solverdt);
		}

		public void UpdateSoftBodies()
		{
			UnsafeNativeMethods.btSoftBodySolver_updateSoftBodies(_native);
		}

		public int NumberOfPositionIterations
		{
			get { return  UnsafeNativeMethods.btSoftBodySolver_getNumberOfPositionIterations(_native);}
			set {  UnsafeNativeMethods.btSoftBodySolver_setNumberOfPositionIterations(_native, value);}
		}

		public int NumberOfVelocityIterations
		{
			get { return  UnsafeNativeMethods.btSoftBodySolver_getNumberOfVelocityIterations(_native);}
			set {  UnsafeNativeMethods.btSoftBodySolver_setNumberOfVelocityIterations(_native, value);}
		}
		/*
		public SolverTypes SolverType
		{
			get { return btSoftBodySolver_getSolverType(_native); }
		}
		*/
		public float TimeScale{ get { return  UnsafeNativeMethods.btSoftBodySolver_getTimeScale(_native);} }

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (_native != IntPtr.Zero)
			{
				UnsafeNativeMethods.btSoftBodySolver_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~SoftBodySolver()
		{
			Dispose(false);
		}
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
