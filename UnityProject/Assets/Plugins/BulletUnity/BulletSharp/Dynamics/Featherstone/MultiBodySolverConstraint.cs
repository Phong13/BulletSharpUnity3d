using System;
using BulletSharp.Math;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class MultiBodySolverConstraint : IDisposable
	{
		internal IntPtr Native;

		protected MultiBody _multiBodyA;
		protected MultiBody _multiBodyB;

		public MultiBodySolverConstraint()
		{
			Native = btMultiBodySolverConstraint_new();
		}

		public Vector3 AngularComponentA
		{
			get
			{
				Vector3 value;
				btMultiBodySolverConstraint_getAngularComponentA(Native, out value);
				return value;
			}
			set => btMultiBodySolverConstraint_setAngularComponentA(Native, ref value);
		}

		public Vector3 AngularComponentB
		{
			get
			{
				Vector3 value;
				btMultiBodySolverConstraint_getAngularComponentB(Native, out value);
				return value;
			}
			set => btMultiBodySolverConstraint_setAngularComponentB(Native, ref value);
		}

		public float AppliedImpulse
		{
			get => btMultiBodySolverConstraint_getAppliedImpulse(Native);
			set => btMultiBodySolverConstraint_setAppliedImpulse(Native, value);
		}

		public float AppliedPushImpulse
		{
			get => btMultiBodySolverConstraint_getAppliedPushImpulse(Native);
			set => btMultiBodySolverConstraint_setAppliedPushImpulse(Native, value);
		}

		public float Cfm
		{
			get => btMultiBodySolverConstraint_getCfm(Native);
			set => btMultiBodySolverConstraint_setCfm(Native, value);
		}

		public Vector3 ContactNormal1
		{
			get
			{
				Vector3 value;
				btMultiBodySolverConstraint_getContactNormal1(Native, out value);
				return value;
			}
			set => btMultiBodySolverConstraint_setContactNormal1(Native, ref value);
		}

		public Vector3 ContactNormal2
		{
			get
			{
				Vector3 value;
				btMultiBodySolverConstraint_getContactNormal2(Native, out value);
				return value;
			}
			set => btMultiBodySolverConstraint_setContactNormal2(Native, ref value);
		}

		public int DeltaVelAindex
		{
			get => btMultiBodySolverConstraint_getDeltaVelAindex(Native);
			set => btMultiBodySolverConstraint_setDeltaVelAindex(Native, value);
		}

		public int DeltaVelBindex
		{
			get => btMultiBodySolverConstraint_getDeltaVelBindex(Native);
			set => btMultiBodySolverConstraint_setDeltaVelBindex(Native, value);
		}

		public float Friction
		{
			get => btMultiBodySolverConstraint_getFriction(Native);
			set => btMultiBodySolverConstraint_setFriction(Native, value);
		}

		public int FrictionIndex
		{
			get => btMultiBodySolverConstraint_getFrictionIndex(Native);
			set => btMultiBodySolverConstraint_setFrictionIndex(Native, value);
		}

		public int JacAindex
		{
			get => btMultiBodySolverConstraint_getJacAindex(Native);
			set => btMultiBodySolverConstraint_setJacAindex(Native, value);
		}

		public int JacBindex
		{
			get => btMultiBodySolverConstraint_getJacBindex(Native);
			set => btMultiBodySolverConstraint_setJacBindex(Native, value);
		}

		public float JacDiagABInv
		{
			get => btMultiBodySolverConstraint_getJacDiagABInv(Native);
			set => btMultiBodySolverConstraint_setJacDiagABInv(Native, value);
		}

		public int LinkA
		{
			get => btMultiBodySolverConstraint_getLinkA(Native);
			set => btMultiBodySolverConstraint_setLinkA(Native, value);
		}

		public int LinkB
		{
			get => btMultiBodySolverConstraint_getLinkB(Native);
			set => btMultiBodySolverConstraint_setLinkB(Native, value);
		}

		public float LowerLimit
		{
			get => btMultiBodySolverConstraint_getLowerLimit(Native);
			set => btMultiBodySolverConstraint_setLowerLimit(Native, value);
		}

		public MultiBody MultiBodyA
		{
			get
			{
				if (_multiBodyA == null)
				{
					_multiBodyA = new MultiBody(btMultiBodySolverConstraint_getMultiBodyA(Native));
				}
				return _multiBodyA;
			}
			set
			{
				btMultiBodySolverConstraint_setMultiBodyA(Native, value.Native);
				_multiBodyA = value;
			}
		}

		public MultiBody MultiBodyB
		{
			get
			{
				if (_multiBodyB == null)
				{
					_multiBodyB = new MultiBody(btMultiBodySolverConstraint_getMultiBodyB(Native));
				}
				return _multiBodyB;
			}
			set
			{
				btMultiBodySolverConstraint_setMultiBodyB(Native, value.Native);
				_multiBodyB = value;
			}
		}
		/*
		public MultiBodyConstraint OrgConstraint
		{
			get { return btMultiBodySolverConstraint_getOrgConstraint(_native); }
			set { btMultiBodySolverConstraint_setOrgConstraint(_native, value._native); }
		}
		*/
		public int OrgDofIndex
		{
			get => btMultiBodySolverConstraint_getOrgDofIndex(Native);
			set => btMultiBodySolverConstraint_setOrgDofIndex(Native, value);
		}

		public IntPtr OriginalContactPoint
		{
			get => btMultiBodySolverConstraint_getOriginalContactPoint(Native);
			set => btMultiBodySolverConstraint_setOriginalContactPoint(Native, value);
		}

		public int OverrideNumSolverIterations
		{
			get => btMultiBodySolverConstraint_getOverrideNumSolverIterations(Native);
			set => btMultiBodySolverConstraint_setOverrideNumSolverIterations(Native, value);
		}

		public Vector3 Relpos1CrossNormal
		{
			get
			{
				Vector3 value;
				btMultiBodySolverConstraint_getRelpos1CrossNormal(Native, out value);
				return value;
			}
			set => btMultiBodySolverConstraint_setRelpos1CrossNormal(Native, ref value);
		}

		public Vector3 Relpos2CrossNormal
		{
			get
			{
				Vector3 value;
				btMultiBodySolverConstraint_getRelpos2CrossNormal(Native, out value);
				return value;
			}
			set => btMultiBodySolverConstraint_setRelpos2CrossNormal(Native, ref value);
		}

		public float Rhs
		{
			get => btMultiBodySolverConstraint_getRhs(Native);
			set => btMultiBodySolverConstraint_setRhs(Native, value);
		}

		public float RhsPenetration
		{
			get => btMultiBodySolverConstraint_getRhsPenetration(Native);
			set => btMultiBodySolverConstraint_setRhsPenetration(Native, value);
		}

		public int SolverBodyIdA
		{
			get => btMultiBodySolverConstraint_getSolverBodyIdA(Native);
			set => btMultiBodySolverConstraint_setSolverBodyIdA(Native, value);
		}

		public int SolverBodyIdB
		{
			get => btMultiBodySolverConstraint_getSolverBodyIdB(Native);
			set => btMultiBodySolverConstraint_setSolverBodyIdB(Native, value);
		}

		public float UnusedPadding4
		{
			get => btMultiBodySolverConstraint_getUnusedPadding4(Native);
			set => btMultiBodySolverConstraint_setUnusedPadding4(Native, value);
		}

		public float UpperLimit
		{
			get => btMultiBodySolverConstraint_getUpperLimit(Native);
			set => btMultiBodySolverConstraint_setUpperLimit(Native, value);
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (Native != IntPtr.Zero)
			{
				btMultiBodySolverConstraint_delete(Native);
				Native = IntPtr.Zero;
			}
		}

		~MultiBodySolverConstraint()
		{
			Dispose(false);
		}
	}
}
