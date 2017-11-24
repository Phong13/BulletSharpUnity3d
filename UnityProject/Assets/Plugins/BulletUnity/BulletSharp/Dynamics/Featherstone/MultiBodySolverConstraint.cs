using System;
using BulletSharp.Math;


namespace BulletSharp
{
	public class MultiBodySolverConstraint : IDisposable
	{
		internal IntPtr Native;

		protected MultiBody _multiBodyA;
		protected MultiBody _multiBodyB;

		public MultiBodySolverConstraint()
		{
			Native = UnsafeNativeMethods.btMultiBodySolverConstraint_new();
		}

		public Vector3 AngularComponentA
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btMultiBodySolverConstraint_getAngularComponentA(Native, out value);
				return value;
			}
			set {  UnsafeNativeMethods.btMultiBodySolverConstraint_setAngularComponentA(Native, ref value);}
		}

		public Vector3 AngularComponentB
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btMultiBodySolverConstraint_getAngularComponentB(Native, out value);
				return value;
			}
			set {  UnsafeNativeMethods.btMultiBodySolverConstraint_setAngularComponentB(Native, ref value);}
		}

		public float AppliedImpulse
		{
			get { return  UnsafeNativeMethods.btMultiBodySolverConstraint_getAppliedImpulse(Native);}
			set {  UnsafeNativeMethods.btMultiBodySolverConstraint_setAppliedImpulse(Native, value);}
		}

		public float AppliedPushImpulse
		{
			get { return  UnsafeNativeMethods.btMultiBodySolverConstraint_getAppliedPushImpulse(Native);}
			set {  UnsafeNativeMethods.btMultiBodySolverConstraint_setAppliedPushImpulse(Native, value);}
		}

		public float Cfm
		{
			get { return  UnsafeNativeMethods.btMultiBodySolverConstraint_getCfm(Native);}
			set {  UnsafeNativeMethods.btMultiBodySolverConstraint_setCfm(Native, value);}
		}

		public Vector3 ContactNormal1
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btMultiBodySolverConstraint_getContactNormal1(Native, out value);
				return value;
			}
			set {  UnsafeNativeMethods.btMultiBodySolverConstraint_setContactNormal1(Native, ref value);}
		}

		public Vector3 ContactNormal2
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btMultiBodySolverConstraint_getContactNormal2(Native, out value);
				return value;
			}
			set {  UnsafeNativeMethods.btMultiBodySolverConstraint_setContactNormal2(Native, ref value);}
		}

		public int DeltaVelAindex
		{
			get { return  UnsafeNativeMethods.btMultiBodySolverConstraint_getDeltaVelAindex(Native);}
			set {  UnsafeNativeMethods.btMultiBodySolverConstraint_setDeltaVelAindex(Native, value);}
		}

		public int DeltaVelBindex
		{
			get { return  UnsafeNativeMethods.btMultiBodySolverConstraint_getDeltaVelBindex(Native);}
			set {  UnsafeNativeMethods.btMultiBodySolverConstraint_setDeltaVelBindex(Native, value);}
		}

		public float Friction
		{
			get { return  UnsafeNativeMethods.btMultiBodySolverConstraint_getFriction(Native);}
			set {  UnsafeNativeMethods.btMultiBodySolverConstraint_setFriction(Native, value);}
		}

		public int FrictionIndex
		{
			get { return  UnsafeNativeMethods.btMultiBodySolverConstraint_getFrictionIndex(Native);}
			set {  UnsafeNativeMethods.btMultiBodySolverConstraint_setFrictionIndex(Native, value);}
		}

		public int JacAindex
		{
			get { return  UnsafeNativeMethods.btMultiBodySolverConstraint_getJacAindex(Native);}
			set {  UnsafeNativeMethods.btMultiBodySolverConstraint_setJacAindex(Native, value);}
		}

		public int JacBindex
		{
			get { return  UnsafeNativeMethods.btMultiBodySolverConstraint_getJacBindex(Native);}
			set {  UnsafeNativeMethods.btMultiBodySolverConstraint_setJacBindex(Native, value);}
		}

		public float JacDiagABInv
		{
			get { return  UnsafeNativeMethods.btMultiBodySolverConstraint_getJacDiagABInv(Native);}
			set {  UnsafeNativeMethods.btMultiBodySolverConstraint_setJacDiagABInv(Native, value);}
		}

		public int LinkA
		{
			get { return  UnsafeNativeMethods.btMultiBodySolverConstraint_getLinkA(Native);}
			set {  UnsafeNativeMethods.btMultiBodySolverConstraint_setLinkA(Native, value);}
		}

		public int LinkB
		{
			get { return  UnsafeNativeMethods.btMultiBodySolverConstraint_getLinkB(Native);}
			set {  UnsafeNativeMethods.btMultiBodySolverConstraint_setLinkB(Native, value);}
		}

		public float LowerLimit
		{
			get { return  UnsafeNativeMethods.btMultiBodySolverConstraint_getLowerLimit(Native);}
			set {  UnsafeNativeMethods.btMultiBodySolverConstraint_setLowerLimit(Native, value);}
		}

		public MultiBody MultiBodyA
		{
			get
			{
				if (_multiBodyA == null)
				{
					_multiBodyA = new MultiBody(UnsafeNativeMethods.btMultiBodySolverConstraint_getMultiBodyA(Native));
				}
				return _multiBodyA;
			}
			set
			{
				UnsafeNativeMethods.btMultiBodySolverConstraint_setMultiBodyA(Native, value.Native);
				_multiBodyA = value;
			}
		}

		public MultiBody MultiBodyB
		{
			get
			{
				if (_multiBodyB == null)
				{
					_multiBodyB = new MultiBody(UnsafeNativeMethods.btMultiBodySolverConstraint_getMultiBodyB(Native));
				}
				return _multiBodyB;
			}
			set
			{
				UnsafeNativeMethods.btMultiBodySolverConstraint_setMultiBodyB(Native, value.Native);
				_multiBodyB = value;
			}
		}
		/*
		public MultiBodyConstraint OrgConstraint
		{
			get { return UnsafeNativeMethods.btMultiBodySolverConstraint_getOrgConstraint(_native); }
			set { UnsafeNativeMethods.btMultiBodySolverConstraint_setOrgConstraint(_native, value._native); }
		}
		*/
		public int OrgDofIndex
		{
			get { return  UnsafeNativeMethods.btMultiBodySolverConstraint_getOrgDofIndex(Native);}
			set {  UnsafeNativeMethods.btMultiBodySolverConstraint_setOrgDofIndex(Native, value);}
		}

		public IntPtr OriginalContactPoint
		{
			get { return  UnsafeNativeMethods.btMultiBodySolverConstraint_getOriginalContactPoint(Native);}
			set {  UnsafeNativeMethods.btMultiBodySolverConstraint_setOriginalContactPoint(Native, value);}
		}

		public int OverrideNumSolverIterations
		{
			get { return  UnsafeNativeMethods.btMultiBodySolverConstraint_getOverrideNumSolverIterations(Native);}
			set {  UnsafeNativeMethods.btMultiBodySolverConstraint_setOverrideNumSolverIterations(Native, value);}
		}

		public Vector3 Relpos1CrossNormal
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btMultiBodySolverConstraint_getRelpos1CrossNormal(Native, out value);
				return value;
			}
			set {  UnsafeNativeMethods.btMultiBodySolverConstraint_setRelpos1CrossNormal(Native, ref value);}
		}

		public Vector3 Relpos2CrossNormal
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btMultiBodySolverConstraint_getRelpos2CrossNormal(Native, out value);
				return value;
			}
			set {  UnsafeNativeMethods.btMultiBodySolverConstraint_setRelpos2CrossNormal(Native, ref value);}
		}

		public float Rhs
		{
			get { return  UnsafeNativeMethods.btMultiBodySolverConstraint_getRhs(Native);}
			set {  UnsafeNativeMethods.btMultiBodySolverConstraint_setRhs(Native, value);}
		}

		public float RhsPenetration
		{
			get { return  UnsafeNativeMethods.btMultiBodySolverConstraint_getRhsPenetration(Native);}
			set {  UnsafeNativeMethods.btMultiBodySolverConstraint_setRhsPenetration(Native, value);}
		}

		public int SolverBodyIdA
		{
			get { return  UnsafeNativeMethods.btMultiBodySolverConstraint_getSolverBodyIdA(Native);}
			set {  UnsafeNativeMethods.btMultiBodySolverConstraint_setSolverBodyIdA(Native, value);}
		}

		public int SolverBodyIdB
		{
			get { return  UnsafeNativeMethods.btMultiBodySolverConstraint_getSolverBodyIdB(Native);}
			set {  UnsafeNativeMethods.btMultiBodySolverConstraint_setSolverBodyIdB(Native, value);}
		}

		public float UnusedPadding4
		{
			get { return  UnsafeNativeMethods.btMultiBodySolverConstraint_getUnusedPadding4(Native);}
			set {  UnsafeNativeMethods.btMultiBodySolverConstraint_setUnusedPadding4(Native, value);}
		}

		public float UpperLimit
		{
			get { return  UnsafeNativeMethods.btMultiBodySolverConstraint_getUpperLimit(Native);}
			set {  UnsafeNativeMethods.btMultiBodySolverConstraint_setUpperLimit(Native, value);}
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
				UnsafeNativeMethods.btMultiBodySolverConstraint_delete(Native);
				Native = IntPtr.Zero;
			}
		}

		~MultiBodySolverConstraint()
		{
			Dispose(false);
		}
	}
}
