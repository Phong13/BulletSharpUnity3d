using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
	public abstract class MultiBodyConstraint : IDisposable
	{
		internal IntPtr _native;

        protected MultiBody _multiBodyA;
        protected MultiBody _multiBodyB;

		internal MultiBodyConstraint(IntPtr native)
		{
			_native = native;
		}

		public void AllocateJacobiansMultiDof()
		{
			btMultiBodyConstraint_allocateJacobiansMultiDof(_native);
		}
        /*
		public void CreateConstraintRows(MultiBodyConstraintArray constraintRows, MultiBodyJacobianData data, ContactSolverInfo infoGlobal)
		{
			btMultiBodyConstraint_createConstraintRows(_native, constraintRows._native, data._native, infoGlobal._native);
		}
        */
		public void DebugDraw(IDebugDraw drawer)
		{
			btMultiBodyConstraint_debugDraw(_native, BulletSharp.DebugDraw.GetUnmanaged(drawer));
		}

		public void FinalizeMultiDof()
		{
			btMultiBodyConstraint_finalizeMultiDof(_native);
		}

		public float GetAppliedImpulse(int dof)
		{
			return btMultiBodyConstraint_getAppliedImpulse(_native, dof);
		}

		public float GetPosition(int row)
		{
			return btMultiBodyConstraint_getPosition(_native, row);
		}

		public void InternalSetAppliedImpulse(int dof, float appliedImpulse)
		{
			btMultiBodyConstraint_internalSetAppliedImpulse(_native, dof, appliedImpulse);
		}
        /*
		public float JacobianA(int row)
		{
			return btMultiBodyConstraint_jacobianA(_native, row);
		}

		public float JacobianB(int row)
		{
			return btMultiBodyConstraint_jacobianB(_native, row);
		}
        */
		public void SetPosition(int row, float pos)
		{
			btMultiBodyConstraint_setPosition(_native, row, pos);
		}

		public void UpdateJacobianSizes()
		{
			btMultiBodyConstraint_updateJacobianSizes(_native);
		}

		public int IslandIdA
		{
			get { return btMultiBodyConstraint_getIslandIdA(_native); }
		}

		public int IslandIdB
		{
			get { return btMultiBodyConstraint_getIslandIdB(_native); }
		}

		public bool IsUnilateral
		{
			get { return btMultiBodyConstraint_isUnilateral(_native); }
		}

		public float MaxAppliedImpulse
		{
			get { return btMultiBodyConstraint_getMaxAppliedImpulse(_native); }
			set { btMultiBodyConstraint_setMaxAppliedImpulse(_native, value); }
		}

		public MultiBody MultiBodyA
		{
            get { return _multiBodyA; }
		}

		public MultiBody MultiBodyB
		{
            get { return _multiBodyB; }
		}

		public int NumRows
		{
			get { return btMultiBodyConstraint_getNumRows(_native); }
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
				btMultiBodyConstraint_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~MultiBodyConstraint()
		{
			Dispose(false);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBodyConstraint_allocateJacobiansMultiDof(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBodyConstraint_createConstraintRows(IntPtr obj, IntPtr constraintRows, IntPtr data, IntPtr infoGlobal);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBodyConstraint_debugDraw(IntPtr obj, IntPtr drawer);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBodyConstraint_finalizeMultiDof(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btMultiBodyConstraint_getAppliedImpulse(IntPtr obj, int dof);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btMultiBodyConstraint_getIslandIdA(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btMultiBodyConstraint_getIslandIdB(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btMultiBodyConstraint_getMaxAppliedImpulse(IntPtr obj);
		//[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		//static extern IntPtr btMultiBodyConstraint_getMultiBodyA(IntPtr obj);
		//[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		//static extern IntPtr btMultiBodyConstraint_getMultiBodyB(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btMultiBodyConstraint_getNumRows(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btMultiBodyConstraint_getPosition(IntPtr obj, int row);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBodyConstraint_internalSetAppliedImpulse(IntPtr obj, int dof, float appliedImpulse);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btMultiBodyConstraint_isUnilateral(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btMultiBodyConstraint_jacobianA(IntPtr obj, int row);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btMultiBodyConstraint_jacobianB(IntPtr obj, int row);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBodyConstraint_setMaxAppliedImpulse(IntPtr obj, float maxImp);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBodyConstraint_setPosition(IntPtr obj, int row, float pos);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBodyConstraint_updateJacobianSizes(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBodyConstraint_delete(IntPtr obj);
	}
}
