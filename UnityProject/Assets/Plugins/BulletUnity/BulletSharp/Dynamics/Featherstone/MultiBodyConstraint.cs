using System;


namespace BulletSharp
{
	public abstract class MultiBodyConstraint : IDisposable
	{
		internal IntPtr Native;

		internal MultiBodyConstraint(IntPtr native, MultiBody bodyA, MultiBody bodyB)
		{
			Native = native;
			MultiBodyA = bodyA;
			MultiBodyB = bodyB;
		}

		public void AllocateJacobiansMultiDof()
		{
			UnsafeNativeMethods.btMultiBodyConstraint_allocateJacobiansMultiDof(Native);
		}
		/*
		public void CreateConstraintRows(MultiBodyConstraintArray constraintRows,
			MultiBodyJacobianData data, ContactSolverInfo infoGlobal)
		{
			UnsafeNativeMethods.btMultiBodyConstraint_createConstraintRows(Native, constraintRows.Native,
				data.Native, infoGlobal.Native);
		}
		*/
		public void DebugDraw(IDebugDraw drawer)
		{
			UnsafeNativeMethods.btMultiBodyConstraint_debugDraw(Native, BulletSharp.DebugDraw.GetUnmanaged(drawer));
		}

		public void FinalizeMultiDof()
		{
			UnsafeNativeMethods.btMultiBodyConstraint_finalizeMultiDof(Native);
		}

		public float GetAppliedImpulse(int dof)
		{
			return UnsafeNativeMethods.btMultiBodyConstraint_getAppliedImpulse(Native, dof);
		}

		public float GetPosition(int row)
		{
			return UnsafeNativeMethods.btMultiBodyConstraint_getPosition(Native, row);
		}

		public void InternalSetAppliedImpulse(int dof, float appliedImpulse)
		{
			UnsafeNativeMethods.btMultiBodyConstraint_internalSetAppliedImpulse(Native, dof, appliedImpulse);
		}
		/*
		public float JacobianA(int row)
		{
			return UnsafeNativeMethods.btMultiBodyConstraint_jacobianA(Native, row);
		}

		public float JacobianB(int row)
		{
			return UnsafeNativeMethods.btMultiBodyConstraint_jacobianB(Native, row);
		}
		*/
		public void SetPosition(int row, float pos)
		{
			UnsafeNativeMethods.btMultiBodyConstraint_setPosition(Native, row, pos);
		}

		public void UpdateJacobianSizes()
		{
			UnsafeNativeMethods.btMultiBodyConstraint_updateJacobianSizes(Native);
		}

		public int IslandIdA => UnsafeNativeMethods.btMultiBodyConstraint_getIslandIdA(Native);

		public int IslandIdB => UnsafeNativeMethods.btMultiBodyConstraint_getIslandIdB(Native);

		public bool IsUnilateral => UnsafeNativeMethods.btMultiBodyConstraint_isUnilateral(Native);

		public float MaxAppliedImpulse
		{
			get => UnsafeNativeMethods.btMultiBodyConstraint_getMaxAppliedImpulse(Native);
			set => UnsafeNativeMethods.btMultiBodyConstraint_setMaxAppliedImpulse(Native, value);
		}

		public MultiBody MultiBodyA { get; }

		public MultiBody MultiBodyB { get; }

		public int NumRows => UnsafeNativeMethods.btMultiBodyConstraint_getNumRows(Native);

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (Native != IntPtr.Zero)
			{
				UnsafeNativeMethods.btMultiBodyConstraint_delete(Native);
				Native = IntPtr.Zero;
			}
		}

		~MultiBodyConstraint()
		{
			Dispose(false);
		}
	}
}
