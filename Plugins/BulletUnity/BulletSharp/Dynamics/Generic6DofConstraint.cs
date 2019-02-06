using System;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.Math;

namespace BulletSharp
{
	[Flags]
	public enum SixDofFlags
	{
		None = 0,
		CfmNormal = 1,
		CfmStop = 2,
		ErpStop = 4
	}

	public class RotationalLimitMotor : IDisposable
	{
		internal IntPtr _native;
		bool _preventDelete;

		internal RotationalLimitMotor(IntPtr native, bool preventDelete)
		{
			_native = native;
			_preventDelete = preventDelete;
		}

		public RotationalLimitMotor()
		{
			_native = btRotationalLimitMotor_new();
		}

		public RotationalLimitMotor(RotationalLimitMotor limitMotor)
		{
			_native = btRotationalLimitMotor_new2(limitMotor._native);
		}

		public bool NeedApplyTorques()
		{
			return btRotationalLimitMotor_needApplyTorques(_native);
		}

        public float SolveAngularLimitsRef(float timeStep, ref Vector3 axis, float jacDiagABInv, RigidBody body0, RigidBody body1)
        {
            return btRotationalLimitMotor_solveAngularLimits(_native, timeStep, ref axis, jacDiagABInv, body0._native, body1._native);
        }

		public float SolveAngularLimits(float timeStep, Vector3 axis, float jacDiagABInv, RigidBody body0, RigidBody body1)
		{
			return btRotationalLimitMotor_solveAngularLimits(_native, timeStep, ref axis, jacDiagABInv, body0._native, body1._native);
		}

		public int TestLimitValue(float testValue)
		{
			return btRotationalLimitMotor_testLimitValue(_native, testValue);
		}

		public float AccumulatedImpulse
		{
			get { return btRotationalLimitMotor_getAccumulatedImpulse(_native); }
			set { btRotationalLimitMotor_setAccumulatedImpulse(_native, value); }
		}

		public float Bounce
		{
			get { return btRotationalLimitMotor_getBounce(_native); }
			set { btRotationalLimitMotor_setBounce(_native, value); }
		}

		public int CurrentLimit
		{
			get { return btRotationalLimitMotor_getCurrentLimit(_native); }
			set { btRotationalLimitMotor_setCurrentLimit(_native, value); }
		}

		public float CurrentLimitError
		{
			get { return btRotationalLimitMotor_getCurrentLimitError(_native); }
			set { btRotationalLimitMotor_setCurrentLimitError(_native, value); }
		}

		public float CurrentPosition
		{
			get { return btRotationalLimitMotor_getCurrentPosition(_native); }
			set { btRotationalLimitMotor_setCurrentPosition(_native, value); }
		}

		public float Damping
		{
			get { return btRotationalLimitMotor_getDamping(_native); }
			set { btRotationalLimitMotor_setDamping(_native, value); }
		}

		public bool EnableMotor
		{
			get { return btRotationalLimitMotor_getEnableMotor(_native); }
			set { btRotationalLimitMotor_setEnableMotor(_native, value); }
		}

		public float HiLimit
		{
			get { return btRotationalLimitMotor_getHiLimit(_native); }
			set { btRotationalLimitMotor_setHiLimit(_native, value); }
		}

		public bool IsLimited
		{
			get { return btRotationalLimitMotor_isLimited(_native); }
		}

		public float LimitSoftness
		{
			get { return btRotationalLimitMotor_getLimitSoftness(_native); }
			set { btRotationalLimitMotor_setLimitSoftness(_native, value); }
		}

		public float LoLimit
		{
			get { return btRotationalLimitMotor_getLoLimit(_native); }
			set { btRotationalLimitMotor_setLoLimit(_native, value); }
		}

		public float MaxLimitForce
		{
			get { return btRotationalLimitMotor_getMaxLimitForce(_native); }
			set { btRotationalLimitMotor_setMaxLimitForce(_native, value); }
		}

		public float MaxMotorForce
		{
			get { return btRotationalLimitMotor_getMaxMotorForce(_native); }
			set { btRotationalLimitMotor_setMaxMotorForce(_native, value); }
		}

		public float NormalCfm
		{
			get { return btRotationalLimitMotor_getNormalCFM(_native); }
			set { btRotationalLimitMotor_setNormalCFM(_native, value); }
		}

		public float StopCfm
		{
			get { return btRotationalLimitMotor_getStopCFM(_native); }
			set { btRotationalLimitMotor_setStopCFM(_native, value); }
		}

		public float StopErp
		{
			get { return btRotationalLimitMotor_getStopERP(_native); }
			set { btRotationalLimitMotor_setStopERP(_native, value); }
		}

		public float TargetVelocity
		{
			get { return btRotationalLimitMotor_getTargetVelocity(_native); }
			set { btRotationalLimitMotor_setTargetVelocity(_native, value); }
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
				if (!_preventDelete)
				{
					btRotationalLimitMotor_delete(_native);
				}
				_native = IntPtr.Zero;
			}
		}

		~RotationalLimitMotor()
		{
			Dispose(false);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btRotationalLimitMotor_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btRotationalLimitMotor_new2(IntPtr limot);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btRotationalLimitMotor_getAccumulatedImpulse(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btRotationalLimitMotor_getBounce(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btRotationalLimitMotor_getCurrentLimit(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btRotationalLimitMotor_getCurrentLimitError(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btRotationalLimitMotor_getCurrentPosition(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btRotationalLimitMotor_getDamping(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btRotationalLimitMotor_getEnableMotor(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btRotationalLimitMotor_getHiLimit(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btRotationalLimitMotor_getLimitSoftness(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btRotationalLimitMotor_getLoLimit(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btRotationalLimitMotor_getMaxLimitForce(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btRotationalLimitMotor_getMaxMotorForce(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btRotationalLimitMotor_getNormalCFM(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btRotationalLimitMotor_getStopCFM(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btRotationalLimitMotor_getStopERP(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btRotationalLimitMotor_getTargetVelocity(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btRotationalLimitMotor_isLimited(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btRotationalLimitMotor_needApplyTorques(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btRotationalLimitMotor_setAccumulatedImpulse(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btRotationalLimitMotor_setBounce(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btRotationalLimitMotor_setCurrentLimit(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btRotationalLimitMotor_setCurrentLimitError(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btRotationalLimitMotor_setCurrentPosition(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btRotationalLimitMotor_setDamping(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btRotationalLimitMotor_setEnableMotor(IntPtr obj, bool value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btRotationalLimitMotor_setHiLimit(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btRotationalLimitMotor_setLimitSoftness(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btRotationalLimitMotor_setLoLimit(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btRotationalLimitMotor_setMaxLimitForce(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btRotationalLimitMotor_setMaxMotorForce(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btRotationalLimitMotor_setNormalCFM(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btRotationalLimitMotor_setStopCFM(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btRotationalLimitMotor_setStopERP(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btRotationalLimitMotor_setTargetVelocity(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btRotationalLimitMotor_solveAngularLimits(IntPtr obj, float timeStep, [In] ref Vector3 axis, float jacDiagABInv, IntPtr body0, IntPtr body1);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btRotationalLimitMotor_testLimitValue(IntPtr obj, float test_value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btRotationalLimitMotor_delete(IntPtr obj);
	}

	public class TranslationalLimitMotor : IDisposable
	{
		internal IntPtr _native;
		bool _preventDelete;

		internal TranslationalLimitMotor(IntPtr native, bool preventDelete)
		{
			_native = native;
			_preventDelete = preventDelete;
		}

		public TranslationalLimitMotor()
		{
			_native = btTranslationalLimitMotor_new();
		}

		public TranslationalLimitMotor(TranslationalLimitMotor other)
		{
			_native = btTranslationalLimitMotor_new2(other._native);
		}

		public bool IsLimited(int limitIndex)
		{
			return btTranslationalLimitMotor_isLimited(_native, limitIndex);
		}

		public bool NeedApplyForce(int limitIndex)
		{
			return btTranslationalLimitMotor_needApplyForce(_native, limitIndex);
		}

        public float SolveLinearAxisRef(float timeStep, float jacDiagABInv, RigidBody body1, ref Vector3 pointInA, RigidBody body2, ref Vector3 pointInB, int limitIndex, ref Vector3 axisNormalOnA, ref Vector3 anchorPos)
        {
            return btTranslationalLimitMotor_solveLinearAxis(_native, timeStep, jacDiagABInv, body1._native, ref pointInA, body2._native, ref pointInB, limitIndex, ref axisNormalOnA, ref anchorPos);
        }

		public float SolveLinearAxis(float timeStep, float jacDiagABInv, RigidBody body1, Vector3 pointInA, RigidBody body2, Vector3 pointInB, int limitIndex, Vector3 axisNormalOnA, Vector3 anchorPos)
		{
			return btTranslationalLimitMotor_solveLinearAxis(_native, timeStep, jacDiagABInv, body1._native, ref pointInA, body2._native, ref pointInB, limitIndex, ref axisNormalOnA, ref anchorPos);
		}

		public int TestLimitValue(int limitIndex, float testValue)
		{
			return btTranslationalLimitMotor_testLimitValue(_native, limitIndex, testValue);
		}

		public Vector3 AccumulatedImpulse
		{
			get
			{
				Vector3 value;
				btTranslationalLimitMotor_getAccumulatedImpulse(_native, out value);
				return value;
			}
			set { btTranslationalLimitMotor_setAccumulatedImpulse(_native, ref value); }
		}
        /*
		public IntArray CurrentLimit
		{
			get { return new IntArray(btTranslationalLimitMotor_getCurrentLimit(_native), 3); }
		}
        */
		public Vector3 CurrentLimitError
		{
			get
			{
				Vector3 value;
				btTranslationalLimitMotor_getCurrentLimitError(_native, out value);
				return value;
			}
			set { btTranslationalLimitMotor_setCurrentLimitError(_native, ref value); }
		}

		public Vector3 CurrentLinearDiff
		{
			get
			{
				Vector3 value;
				btTranslationalLimitMotor_getCurrentLinearDiff(_native, out value);
				return value;
			}
			set { btTranslationalLimitMotor_setCurrentLinearDiff(_native, ref value); }
		}

		public float Damping
		{
			get { return btTranslationalLimitMotor_getDamping(_native); }
			set { btTranslationalLimitMotor_setDamping(_native, value); }
		}
        /*
		public bool EnableMotor
		{
			get { return btTranslationalLimitMotor_getEnableMotor(_native); }
		}
        */
		public float LimitSoftness
		{
			get { return btTranslationalLimitMotor_getLimitSoftness(_native); }
			set { btTranslationalLimitMotor_setLimitSoftness(_native, value); }
		}

		public Vector3 LowerLimit
		{
			get
			{
				Vector3 value;
				btTranslationalLimitMotor_getLowerLimit(_native, out value);
				return value;
			}
			set { btTranslationalLimitMotor_setLowerLimit(_native, ref value); }
		}

		public Vector3 MaxMotorForce
		{
			get
			{
				Vector3 value;
				btTranslationalLimitMotor_getMaxMotorForce(_native, out value);
				return value;
			}
			set { btTranslationalLimitMotor_setMaxMotorForce(_native, ref value); }
		}

        public Vector3 NormalCfm
		{
			get
			{
				Vector3 value;
				btTranslationalLimitMotor_getNormalCFM(_native, out value);
				return value;
			}
			set { btTranslationalLimitMotor_setNormalCFM(_native, ref value); }
		}

		public float Restitution
		{
			get { return btTranslationalLimitMotor_getRestitution(_native); }
			set { btTranslationalLimitMotor_setRestitution(_native, value); }
		}

        public Vector3 StopCfm
		{
			get
			{
				Vector3 value;
				btTranslationalLimitMotor_getStopCFM(_native, out value);
				return value;
			}
			set { btTranslationalLimitMotor_setStopCFM(_native, ref value); }
		}

        public Vector3 StopErp
		{
			get
			{
				Vector3 value;
				btTranslationalLimitMotor_getStopERP(_native, out value);
				return value;
			}
			set { btTranslationalLimitMotor_setStopERP(_native, ref value); }
		}

		public Vector3 TargetVelocity
		{
			get
			{
				Vector3 value;
				btTranslationalLimitMotor_getTargetVelocity(_native, out value);
				return value;
			}
			set { btTranslationalLimitMotor_setTargetVelocity(_native, ref value); }
		}

		public Vector3 UpperLimit
		{
			get
			{
				Vector3 value;
				btTranslationalLimitMotor_getUpperLimit(_native, out value);
				return value;
			}
			set { btTranslationalLimitMotor_setUpperLimit(_native, ref value); }
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
				if (!_preventDelete)
				{
					btTranslationalLimitMotor_delete(_native);
				}
				_native = IntPtr.Zero;
			}
		}

		~TranslationalLimitMotor()
		{
			Dispose(false);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btTranslationalLimitMotor_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btTranslationalLimitMotor_new2(IntPtr other);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTranslationalLimitMotor_getAccumulatedImpulse(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btTranslationalLimitMotor_getCurrentLimit(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTranslationalLimitMotor_getCurrentLimitError(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTranslationalLimitMotor_getCurrentLinearDiff(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btTranslationalLimitMotor_getDamping(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern IntPtr btTranslationalLimitMotor_getEnableMotor(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btTranslationalLimitMotor_getLimitSoftness(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTranslationalLimitMotor_getLowerLimit(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTranslationalLimitMotor_getMaxMotorForce(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTranslationalLimitMotor_getNormalCFM(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btTranslationalLimitMotor_getRestitution(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTranslationalLimitMotor_getStopCFM(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTranslationalLimitMotor_getStopERP(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTranslationalLimitMotor_getTargetVelocity(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTranslationalLimitMotor_getUpperLimit(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btTranslationalLimitMotor_isLimited(IntPtr obj, int limitIndex);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btTranslationalLimitMotor_needApplyForce(IntPtr obj, int limitIndex);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTranslationalLimitMotor_setAccumulatedImpulse(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTranslationalLimitMotor_setCurrentLimitError(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTranslationalLimitMotor_setCurrentLinearDiff(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTranslationalLimitMotor_setDamping(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTranslationalLimitMotor_setLimitSoftness(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTranslationalLimitMotor_setLowerLimit(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTranslationalLimitMotor_setMaxMotorForce(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTranslationalLimitMotor_setNormalCFM(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTranslationalLimitMotor_setRestitution(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTranslationalLimitMotor_setStopCFM(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTranslationalLimitMotor_setStopERP(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTranslationalLimitMotor_setTargetVelocity(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTranslationalLimitMotor_setUpperLimit(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btTranslationalLimitMotor_solveLinearAxis(IntPtr obj, float timeStep, float jacDiagABInv, IntPtr body1, [In] ref Vector3 pointInA, IntPtr body2, [In] ref Vector3 pointInB, int limit_index, [In] ref Vector3 axis_normal_on_a, [In] ref Vector3 anchorPos);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btTranslationalLimitMotor_testLimitValue(IntPtr obj, int limitIndex, float test_value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTranslationalLimitMotor_delete(IntPtr obj);
	}

	public class Generic6DofConstraint : TypedConstraint
	{
        private RotationalLimitMotor[] _angularLimits = new RotationalLimitMotor[3];
        private TranslationalLimitMotor _linearLimits;

		internal Generic6DofConstraint(IntPtr native)
			: base(native)
		{
		}

		public Generic6DofConstraint(RigidBody rigidBodyA, RigidBody rigidBodyB, Matrix frameInA, Matrix frameInB, bool useLinearReferenceFrameA)
			: base(btGeneric6DofConstraint_new(rigidBodyA._native, rigidBodyB._native, ref frameInA, ref frameInB, useLinearReferenceFrameA))
		{
			_rigidBodyA = rigidBodyA;
			_rigidBodyB = rigidBodyB;
		}

		public Generic6DofConstraint(RigidBody rigidBodyB, Matrix frameInB, bool useLinearReferenceFrameB)
			: base(btGeneric6DofConstraint_new2(rigidBodyB._native, ref frameInB, useLinearReferenceFrameB))
		{
            _rigidBodyA = GetFixedBody();
			_rigidBodyB = rigidBodyB;
		}

		public void CalcAnchorPos()
		{
			btGeneric6DofConstraint_calcAnchorPos(_native);
		}

        public void CalculateTransforms(ref Matrix transA, ref Matrix transB)
		{
			btGeneric6DofConstraint_calculateTransforms(_native, ref transA, ref transB);
		}

		public void CalculateTransforms()
		{
			btGeneric6DofConstraint_calculateTransforms2(_native);
		}

		public int GetLimitMotorInfo2(RotationalLimitMotor limitMotor, Matrix transA, Matrix transB, Vector3 linVelA, Vector3 linVelB, Vector3 angVelA, Vector3 angVelB, ConstraintInfo2 info, int row, out Vector3 ax1, int rotational)
		{
			return btGeneric6DofConstraint_get_limit_motor_info2(_native, limitMotor._native, ref transA, ref transB, ref linVelA, ref linVelB, ref angVelA, ref angVelB, info._native, row, out ax1, rotational);
		}

		public int GetLimitMotorInfo2(RotationalLimitMotor limitMotor, Matrix transA, Matrix transB, Vector3 linVelA, Vector3 linVelB, Vector3 angVelA, Vector3 angVelB, ConstraintInfo2 info, int row, out Vector3 ax1, int rotational, int rotAllowed)
		{
			return btGeneric6DofConstraint_get_limit_motor_info22(_native, limitMotor._native, ref transA, ref transB, ref linVelA, ref linVelB, ref angVelA, ref angVelB, info._native, row, out ax1, rotational, rotAllowed);
		}

		public float GetAngle(int axisIndex)
		{
			return btGeneric6DofConstraint_getAngle(_native, axisIndex);
		}

		public Vector3 GetAxis(int axisIndex)
		{
			Vector3 value;
			btGeneric6DofConstraint_getAxis(_native, axisIndex, out value);
			return value;
		}

		public void GetInfo1NonVirtual(ConstraintInfo1 info)
		{
			btGeneric6DofConstraint_getInfo1NonVirtual(_native, info._native);
		}

		public void GetInfo2NonVirtual(ConstraintInfo2 info, Matrix transA, Matrix transB, Vector3 linVelA, Vector3 linVelB, Vector3 angVelA, Vector3 angVelB)
		{
			btGeneric6DofConstraint_getInfo2NonVirtual(_native, info._native, ref transA, ref transB, ref linVelA, ref linVelB, ref angVelA, ref angVelB);
		}

		public float GetRelativePivotPosition(int axisIndex)
		{
			return btGeneric6DofConstraint_getRelativePivotPosition(_native, axisIndex);
		}

		public RotationalLimitMotor GetRotationalLimitMotor(int index)
		{
            if (_angularLimits[index] == null)
            {
                _angularLimits[index] = new RotationalLimitMotor(btGeneric6DofConstraint_getRotationalLimitMotor(_native, index), true);
            }
            return _angularLimits[index];
		}

		public bool IsLimited(int limitIndex)
		{
			return btGeneric6DofConstraint_isLimited(_native, limitIndex);
		}

        public void SetAxisRef(ref Vector3 axis1, ref Vector3 axis2)
        {
            btGeneric6DofConstraint_setAxis(_native, ref axis1, ref axis2);
        }

		public void SetAxis(Vector3 axis1, Vector3 axis2)
		{
			btGeneric6DofConstraint_setAxis(_native, ref axis1, ref axis2);
		}

        public void SetFramesRef(ref Matrix frameA, ref Matrix frameB)
        {
            btGeneric6DofConstraint_setFrames(_native, ref frameA, ref frameB);
        }

		public void SetFrames(Matrix frameA, Matrix frameB)
		{
			btGeneric6DofConstraint_setFrames(_native, ref frameA, ref frameB);
		}

		public void SetLimit(int axis, float lo, float hi)
		{
			btGeneric6DofConstraint_setLimit(_native, axis, lo, hi);
		}

		public bool TestAngularLimitMotor(int axisIndex)
		{
			return btGeneric6DofConstraint_testAngularLimitMotor(_native, axisIndex);
		}

		public void UpdateRhs(float timeStep)
		{
			btGeneric6DofConstraint_updateRHS(_native, timeStep);
		}

		public Vector3 AngularLowerLimit
		{
			get
			{
				Vector3 value;
				btGeneric6DofConstraint_getAngularLowerLimit(_native, out value);
				return value;
			}
			set { btGeneric6DofConstraint_setAngularLowerLimit(_native, ref value); }
		}

		public Vector3 AngularUpperLimit
		{
			get
			{
				Vector3 value;
				btGeneric6DofConstraint_getAngularUpperLimit(_native, out value);
				return value;
			}
			set { btGeneric6DofConstraint_setAngularUpperLimit(_native, ref value); }
		}

		public Matrix CalculatedTransformA
		{
			get
			{
				Matrix value;
				btGeneric6DofConstraint_getCalculatedTransformA(_native, out value);
				return value;
			}
		}

		public Matrix CalculatedTransformB
		{
			get
			{
				Matrix value;
				btGeneric6DofConstraint_getCalculatedTransformB(_native, out value);
				return value;
			}
		}

		public SixDofFlags Flags
		{
			get { return btGeneric6DofConstraint_getFlags(_native); }
		}

		public Matrix FrameOffsetA
		{
			get
			{
				Matrix value;
				btGeneric6DofConstraint_getFrameOffsetA(_native, out value);
				return value;
			}
		}

		public Matrix FrameOffsetB
		{
			get
			{
				Matrix value;
				btGeneric6DofConstraint_getFrameOffsetB(_native, out value);
				return value;
			}
		}

		public Vector3 LinearLowerLimit
		{
			get
			{
				Vector3 value;
				btGeneric6DofConstraint_getLinearLowerLimit(_native, out value);
				return value;
			}
			set { btGeneric6DofConstraint_setLinearLowerLimit(_native, ref value); }
		}

		public Vector3 LinearUpperLimit
		{
			get
			{
				Vector3 value;
				btGeneric6DofConstraint_getLinearUpperLimit(_native, out value);
				return value;
			}
			set { btGeneric6DofConstraint_setLinearUpperLimit(_native, ref value); }
		}

		public TranslationalLimitMotor TranslationalLimitMotor
		{
            get
            {
                if (_linearLimits == null)
                {
                    _linearLimits = new TranslationalLimitMotor(btGeneric6DofConstraint_getTranslationalLimitMotor(_native), true);
                }
                return _linearLimits;
            }
		}

		public bool UseFrameOffset
		{
			get { return btGeneric6DofConstraint_getUseFrameOffset(_native); }
			set { btGeneric6DofConstraint_setUseFrameOffset(_native, value); }
		}

		public bool UseLinearReferenceFrameA
		{
			get { return btGeneric6DofConstraint_getUseLinearReferenceFrameA(_native); }
			set { btGeneric6DofConstraint_setUseLinearReferenceFrameA(_native, value); }
		}

		public bool UseSolveConstraintObsolete
		{
			get { return btGeneric6DofConstraint_getUseSolveConstraintObsolete(_native); }
			set { btGeneric6DofConstraint_setUseSolveConstraintObsolete(_native, value); }
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btGeneric6DofConstraint_new(IntPtr rbA, IntPtr rbB, [In] ref Matrix frameInA, [In] ref Matrix frameInB, bool useLinearReferenceFrameA);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btGeneric6DofConstraint_new2(IntPtr rbB, [In] ref Matrix frameInB, bool useLinearReferenceFrameB);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGeneric6DofConstraint_calcAnchorPos(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGeneric6DofConstraint_calculateTransforms(IntPtr obj, [In] ref Matrix transA, [In] ref Matrix transB);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGeneric6DofConstraint_calculateTransforms2(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btGeneric6DofConstraint_get_limit_motor_info2(IntPtr obj, IntPtr limot, [In] ref Matrix transA, [In] ref Matrix transB, [In] ref Vector3 linVelA, [In] ref Vector3 linVelB, [In] ref Vector3 angVelA, [In] ref Vector3 angVelB, IntPtr info, int row, [Out] out Vector3 ax1, int rotational);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btGeneric6DofConstraint_get_limit_motor_info22(IntPtr obj, IntPtr limot, [In] ref Matrix transA, [In] ref Matrix transB, [In] ref Vector3 linVelA, [In] ref Vector3 linVelB, [In] ref Vector3 angVelA, [In] ref Vector3 angVelB, IntPtr info, int row, [Out] out Vector3 ax1, int rotational, int rotAllowed);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btGeneric6DofConstraint_getAngle(IntPtr obj, int axis_index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGeneric6DofConstraint_getAngularLowerLimit(IntPtr obj, [Out] out Vector3 angularLower);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGeneric6DofConstraint_getAngularUpperLimit(IntPtr obj, [Out] out Vector3 angularUpper);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGeneric6DofConstraint_getAxis(IntPtr obj, int axis_index, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGeneric6DofConstraint_getCalculatedTransformA(IntPtr obj, [Out] out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGeneric6DofConstraint_getCalculatedTransformB(IntPtr obj, [Out] out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern SixDofFlags btGeneric6DofConstraint_getFlags(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGeneric6DofConstraint_getFrameOffsetA(IntPtr obj, [Out] out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGeneric6DofConstraint_getFrameOffsetB(IntPtr obj, [Out] out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGeneric6DofConstraint_getInfo1NonVirtual(IntPtr obj, IntPtr info);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGeneric6DofConstraint_getInfo2NonVirtual(IntPtr obj, IntPtr info, [In] ref Matrix transA, [In] ref Matrix transB, [In] ref Vector3 linVelA, [In] ref Vector3 linVelB, [In] ref Vector3 angVelA, [In] ref Vector3 angVelB);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGeneric6DofConstraint_getLinearLowerLimit(IntPtr obj, [Out] out Vector3 linearLower);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGeneric6DofConstraint_getLinearUpperLimit(IntPtr obj, [Out] out Vector3 linearUpper);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btGeneric6DofConstraint_getRelativePivotPosition(IntPtr obj, int axis_index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btGeneric6DofConstraint_getRotationalLimitMotor(IntPtr obj, int index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btGeneric6DofConstraint_getTranslationalLimitMotor(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btGeneric6DofConstraint_getUseFrameOffset(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btGeneric6DofConstraint_getUseLinearReferenceFrameA(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btGeneric6DofConstraint_getUseSolveConstraintObsolete(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btGeneric6DofConstraint_isLimited(IntPtr obj, int limitIndex);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGeneric6DofConstraint_setAngularLowerLimit(IntPtr obj, [In] ref Vector3 angularLower);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGeneric6DofConstraint_setAngularUpperLimit(IntPtr obj, [In] ref Vector3 angularUpper);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGeneric6DofConstraint_setAxis(IntPtr obj, [In] ref Vector3 axis1, [In] ref Vector3 axis2);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGeneric6DofConstraint_setFrames(IntPtr obj, [In] ref Matrix frameA, [In] ref Matrix frameB);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGeneric6DofConstraint_setLimit(IntPtr obj, int axis, float lo, float hi);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGeneric6DofConstraint_setLinearLowerLimit(IntPtr obj, [In] ref Vector3 linearLower);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGeneric6DofConstraint_setLinearUpperLimit(IntPtr obj, [In] ref Vector3 linearUpper);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGeneric6DofConstraint_setUseFrameOffset(IntPtr obj, bool frameOffsetOnOff);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGeneric6DofConstraint_setUseLinearReferenceFrameA(IntPtr obj, bool linearReferenceFrameA);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGeneric6DofConstraint_setUseSolveConstraintObsolete(IntPtr obj, bool value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btGeneric6DofConstraint_testAngularLimitMotor(IntPtr obj, int axis_index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGeneric6DofConstraint_updateRHS(IntPtr obj, float timeStep);
	}

    [StructLayout(LayoutKind.Sequential)]
    internal struct Generic6DofConstraintFloatData
    {
        public TypedConstraintFloatData TypedConstraintData;
        public TransformFloatData RigidBodyAFrame;
        public TransformFloatData RigidBodyBFrame;
        public Vector3FloatData LinearUpperLimit;
        public Vector3FloatData LinearLowerLimit;
        public Vector3FloatData AngularUpperLimit;
        public Vector3FloatData AngularLowerLimit;
        public int UseLinearReferenceFrameA;
        public int UseOffsetForConstraintFrame;

        public static int Offset(string fieldName) { return Marshal.OffsetOf(typeof(Generic6DofConstraintFloatData), fieldName).ToInt32(); }
    }
}
