using System;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.Math;

namespace BulletSharp
{
	public class MultiBodySolverConstraint : IDisposable
	{
		internal IntPtr _native;

        protected MultiBody _multiBodyA;
        protected MultiBody _multiBodyB;

		internal MultiBodySolverConstraint(IntPtr native)
		{
			_native = native;
		}

		public MultiBodySolverConstraint()
		{
			_native = btMultiBodySolverConstraint_new();
		}

		public Vector3 AngularComponentA
		{
			get
			{
				Vector3 value;
				btMultiBodySolverConstraint_getAngularComponentA(_native, out value);
				return value;
			}
			set { btMultiBodySolverConstraint_setAngularComponentA(_native, ref value); }
		}

		public Vector3 AngularComponentB
		{
			get
			{
				Vector3 value;
				btMultiBodySolverConstraint_getAngularComponentB(_native, out value);
				return value;
			}
			set { btMultiBodySolverConstraint_setAngularComponentB(_native, ref value); }
		}

		public float AppliedImpulse
		{
			get { return btMultiBodySolverConstraint_getAppliedImpulse(_native); }
			set { btMultiBodySolverConstraint_setAppliedImpulse(_native, value); }
		}

		public float AppliedPushImpulse
		{
			get { return btMultiBodySolverConstraint_getAppliedPushImpulse(_native); }
			set { btMultiBodySolverConstraint_setAppliedPushImpulse(_native, value); }
		}

		public float Cfm
		{
			get { return btMultiBodySolverConstraint_getCfm(_native); }
			set { btMultiBodySolverConstraint_setCfm(_native, value); }
		}

		public Vector3 ContactNormal1
		{
			get
			{
				Vector3 value;
				btMultiBodySolverConstraint_getContactNormal1(_native, out value);
				return value;
			}
			set { btMultiBodySolverConstraint_setContactNormal1(_native, ref value); }
		}

		public Vector3 ContactNormal2
		{
			get
			{
				Vector3 value;
				btMultiBodySolverConstraint_getContactNormal2(_native, out value);
				return value;
			}
			set { btMultiBodySolverConstraint_setContactNormal2(_native, ref value); }
		}

		public int DeltaVelAindex
		{
			get { return btMultiBodySolverConstraint_getDeltaVelAindex(_native); }
			set { btMultiBodySolverConstraint_setDeltaVelAindex(_native, value); }
		}

		public int DeltaVelBindex
		{
			get { return btMultiBodySolverConstraint_getDeltaVelBindex(_native); }
			set { btMultiBodySolverConstraint_setDeltaVelBindex(_native, value); }
		}

		public float Friction
		{
			get { return btMultiBodySolverConstraint_getFriction(_native); }
			set { btMultiBodySolverConstraint_setFriction(_native, value); }
		}

		public int FrictionIndex
		{
			get { return btMultiBodySolverConstraint_getFrictionIndex(_native); }
			set { btMultiBodySolverConstraint_setFrictionIndex(_native, value); }
		}

		public int JacAindex
		{
			get { return btMultiBodySolverConstraint_getJacAindex(_native); }
			set { btMultiBodySolverConstraint_setJacAindex(_native, value); }
		}

		public int JacBindex
		{
			get { return btMultiBodySolverConstraint_getJacBindex(_native); }
			set { btMultiBodySolverConstraint_setJacBindex(_native, value); }
		}

		public float JacDiagABInv
		{
			get { return btMultiBodySolverConstraint_getJacDiagABInv(_native); }
			set { btMultiBodySolverConstraint_setJacDiagABInv(_native, value); }
		}

		public int LinkA
		{
			get { return btMultiBodySolverConstraint_getLinkA(_native); }
			set { btMultiBodySolverConstraint_setLinkA(_native, value); }
		}

		public int LinkB
		{
			get { return btMultiBodySolverConstraint_getLinkB(_native); }
			set { btMultiBodySolverConstraint_setLinkB(_native, value); }
		}

		public float LowerLimit
		{
			get { return btMultiBodySolverConstraint_getLowerLimit(_native); }
			set { btMultiBodySolverConstraint_setLowerLimit(_native, value); }
		}

		public MultiBody MultiBodyA
		{
            get
            {
                if (_multiBodyA == null)
                {
                    _multiBodyA = new MultiBody(btMultiBodySolverConstraint_getMultiBodyA(_native));
                }
                return _multiBodyA;
            }
			set
			{
				btMultiBodySolverConstraint_setMultiBodyA(_native, value._native);
				_multiBodyA = value;
			}
		}

		public MultiBody MultiBodyB
		{
            get
            {
                if (_multiBodyB == null)
                {
                    _multiBodyB = new MultiBody(btMultiBodySolverConstraint_getMultiBodyB(_native));
                }
                return _multiBodyB;
            }
			set
			{
				btMultiBodySolverConstraint_setMultiBodyB(_native, value._native);
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
			get { return btMultiBodySolverConstraint_getOrgDofIndex(_native); }
			set { btMultiBodySolverConstraint_setOrgDofIndex(_native, value); }
		}

		public IntPtr OriginalContactPoint
		{
			get { return btMultiBodySolverConstraint_getOriginalContactPoint(_native); }
			set { btMultiBodySolverConstraint_setOriginalContactPoint(_native, value); }
		}

		public int OverrideNumSolverIterations
		{
			get { return btMultiBodySolverConstraint_getOverrideNumSolverIterations(_native); }
			set { btMultiBodySolverConstraint_setOverrideNumSolverIterations(_native, value); }
		}

		public Vector3 Relpos1CrossNormal
		{
			get
			{
				Vector3 value;
				btMultiBodySolverConstraint_getRelpos1CrossNormal(_native, out value);
				return value;
			}
			set { btMultiBodySolverConstraint_setRelpos1CrossNormal(_native, ref value); }
		}

		public Vector3 Relpos2CrossNormal
		{
			get
			{
				Vector3 value;
				btMultiBodySolverConstraint_getRelpos2CrossNormal(_native, out value);
				return value;
			}
			set { btMultiBodySolverConstraint_setRelpos2CrossNormal(_native, ref value); }
		}

		public float Rhs
		{
			get { return btMultiBodySolverConstraint_getRhs(_native); }
			set { btMultiBodySolverConstraint_setRhs(_native, value); }
		}

		public float RhsPenetration
		{
			get { return btMultiBodySolverConstraint_getRhsPenetration(_native); }
			set { btMultiBodySolverConstraint_setRhsPenetration(_native, value); }
		}

		public int SolverBodyIdA
		{
			get { return btMultiBodySolverConstraint_getSolverBodyIdA(_native); }
			set { btMultiBodySolverConstraint_setSolverBodyIdA(_native, value); }
		}

		public int SolverBodyIdB
		{
			get { return btMultiBodySolverConstraint_getSolverBodyIdB(_native); }
			set { btMultiBodySolverConstraint_setSolverBodyIdB(_native, value); }
		}

		public float UnusedPadding4
		{
			get { return btMultiBodySolverConstraint_getUnusedPadding4(_native); }
			set { btMultiBodySolverConstraint_setUnusedPadding4(_native, value); }
		}

		public float UpperLimit
		{
			get { return btMultiBodySolverConstraint_getUpperLimit(_native); }
			set { btMultiBodySolverConstraint_setUpperLimit(_native, value); }
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
				btMultiBodySolverConstraint_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~MultiBodySolverConstraint()
		{
			Dispose(false);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btMultiBodySolverConstraint_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBodySolverConstraint_getAngularComponentA(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBodySolverConstraint_getAngularComponentB(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btMultiBodySolverConstraint_getAppliedImpulse(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btMultiBodySolverConstraint_getAppliedPushImpulse(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btMultiBodySolverConstraint_getCfm(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBodySolverConstraint_getContactNormal1(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBodySolverConstraint_getContactNormal2(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btMultiBodySolverConstraint_getDeltaVelAindex(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btMultiBodySolverConstraint_getDeltaVelBindex(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btMultiBodySolverConstraint_getFriction(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btMultiBodySolverConstraint_getFrictionIndex(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btMultiBodySolverConstraint_getJacAindex(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btMultiBodySolverConstraint_getJacBindex(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btMultiBodySolverConstraint_getJacDiagABInv(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btMultiBodySolverConstraint_getLinkA(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btMultiBodySolverConstraint_getLinkB(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btMultiBodySolverConstraint_getLowerLimit(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btMultiBodySolverConstraint_getMultiBodyA(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btMultiBodySolverConstraint_getMultiBodyB(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btMultiBodySolverConstraint_getOrgConstraint(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btMultiBodySolverConstraint_getOrgDofIndex(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btMultiBodySolverConstraint_getOriginalContactPoint(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btMultiBodySolverConstraint_getOverrideNumSolverIterations(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBodySolverConstraint_getRelpos1CrossNormal(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBodySolverConstraint_getRelpos2CrossNormal(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btMultiBodySolverConstraint_getRhs(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btMultiBodySolverConstraint_getRhsPenetration(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btMultiBodySolverConstraint_getSolverBodyIdA(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btMultiBodySolverConstraint_getSolverBodyIdB(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btMultiBodySolverConstraint_getUnusedPadding4(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btMultiBodySolverConstraint_getUpperLimit(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBodySolverConstraint_setAngularComponentA(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBodySolverConstraint_setAngularComponentB(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBodySolverConstraint_setAppliedImpulse(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBodySolverConstraint_setAppliedPushImpulse(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBodySolverConstraint_setCfm(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBodySolverConstraint_setContactNormal1(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBodySolverConstraint_setContactNormal2(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBodySolverConstraint_setDeltaVelAindex(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBodySolverConstraint_setDeltaVelBindex(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBodySolverConstraint_setFriction(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBodySolverConstraint_setFrictionIndex(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBodySolverConstraint_setJacAindex(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBodySolverConstraint_setJacBindex(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBodySolverConstraint_setJacDiagABInv(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBodySolverConstraint_setLinkA(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBodySolverConstraint_setLinkB(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBodySolverConstraint_setLowerLimit(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBodySolverConstraint_setMultiBodyA(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBodySolverConstraint_setMultiBodyB(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBodySolverConstraint_setOrgConstraint(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBodySolverConstraint_setOrgDofIndex(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBodySolverConstraint_setOriginalContactPoint(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBodySolverConstraint_setOverrideNumSolverIterations(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBodySolverConstraint_setRelpos1CrossNormal(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBodySolverConstraint_setRelpos2CrossNormal(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBodySolverConstraint_setRhs(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBodySolverConstraint_setRhsPenetration(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBodySolverConstraint_setSolverBodyIdA(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBodySolverConstraint_setSolverBodyIdB(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBodySolverConstraint_setUnusedPadding4(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBodySolverConstraint_setUpperLimit(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBodySolverConstraint_delete(IntPtr obj);
	}
}
