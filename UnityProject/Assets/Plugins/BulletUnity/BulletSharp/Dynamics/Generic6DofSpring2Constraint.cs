using System;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.Math;

namespace BulletSharp
{
	public enum RotateOrder
	{
		XYZ,
		XZY,
		YXZ,
		YZX,
		ZXY,
		ZYX
	}

	public class RotationalLimitMotor2 : IDisposable
	{
		internal IntPtr _native;
		bool _preventDelete;

		internal RotationalLimitMotor2(IntPtr native, bool preventDelete)
		{
			_native = native;
			_preventDelete = preventDelete;
		}

		public RotationalLimitMotor2()
		{
			_native = btRotationalLimitMotor2_new();
		}

		public RotationalLimitMotor2(RotationalLimitMotor2 limitMotor)
		{
			_native = btRotationalLimitMotor2_new2(limitMotor._native);
		}

		public void TestLimitValue(float testValue)
		{
			btRotationalLimitMotor2_testLimitValue(_native, testValue);
		}

		public float Bounce
		{
			get { return btRotationalLimitMotor2_getBounce(_native); }
			set { btRotationalLimitMotor2_setBounce(_native, value); }
		}

		public int CurrentLimit
		{
			get { return btRotationalLimitMotor2_getCurrentLimit(_native); }
			set { btRotationalLimitMotor2_setCurrentLimit(_native, value); }
		}

		public float CurrentLimitError
		{
			get { return btRotationalLimitMotor2_getCurrentLimitError(_native); }
			set { btRotationalLimitMotor2_setCurrentLimitError(_native, value); }
		}

		public float CurrentLimitErrorHi
		{
			get { return btRotationalLimitMotor2_getCurrentLimitErrorHi(_native); }
			set { btRotationalLimitMotor2_setCurrentLimitErrorHi(_native, value); }
		}

		public float CurrentPosition
		{
			get { return btRotationalLimitMotor2_getCurrentPosition(_native); }
			set { btRotationalLimitMotor2_setCurrentPosition(_native, value); }
		}

		public bool EnableMotor
		{
			get { return btRotationalLimitMotor2_getEnableMotor(_native); }
			set { btRotationalLimitMotor2_setEnableMotor(_native, value); }
		}

		public bool EnableSpring
		{
			get { return btRotationalLimitMotor2_getEnableSpring(_native); }
			set { btRotationalLimitMotor2_setEnableSpring(_native, value); }
		}

		public float EquilibriumPoint
		{
			get { return btRotationalLimitMotor2_getEquilibriumPoint(_native); }
			set { btRotationalLimitMotor2_setEquilibriumPoint(_native, value); }
		}

		public float HiLimit
		{
			get { return btRotationalLimitMotor2_getHiLimit(_native); }
			set { btRotationalLimitMotor2_setHiLimit(_native, value); }
		}

		public bool IsLimited
		{
			get { return btRotationalLimitMotor2_isLimited(_native); }
		}

		public float LoLimit
		{
			get { return btRotationalLimitMotor2_getLoLimit(_native); }
			set { btRotationalLimitMotor2_setLoLimit(_native, value); }
		}

		public float MaxMotorForce
		{
			get { return btRotationalLimitMotor2_getMaxMotorForce(_native); }
			set { btRotationalLimitMotor2_setMaxMotorForce(_native, value); }
		}

		public float MotorCFM
		{
			get { return btRotationalLimitMotor2_getMotorCFM(_native); }
			set { btRotationalLimitMotor2_setMotorCFM(_native, value); }
		}

		public float MotorERP
		{
			get { return btRotationalLimitMotor2_getMotorERP(_native); }
			set { btRotationalLimitMotor2_setMotorERP(_native, value); }
		}

		public bool ServoMotor
		{
			get { return btRotationalLimitMotor2_getServoMotor(_native); }
			set { btRotationalLimitMotor2_setServoMotor(_native, value); }
		}

		public float ServoTarget
		{
			get { return btRotationalLimitMotor2_getServoTarget(_native); }
			set { btRotationalLimitMotor2_setServoTarget(_native, value); }
		}

		public float SpringDamping
		{
			get { return btRotationalLimitMotor2_getSpringDamping(_native); }
			set { btRotationalLimitMotor2_setSpringDamping(_native, value); }
		}

		public bool SpringDampingLimited
		{
			get { return btRotationalLimitMotor2_getSpringDampingLimited(_native); }
			set { btRotationalLimitMotor2_setSpringDampingLimited(_native, value); }
		}

		public float SpringStiffness
		{
			get { return btRotationalLimitMotor2_getSpringStiffness(_native); }
			set { btRotationalLimitMotor2_setSpringStiffness(_native, value); }
		}

		public bool SpringStiffnessLimited
		{
			get { return btRotationalLimitMotor2_getSpringStiffnessLimited(_native); }
			set { btRotationalLimitMotor2_setSpringStiffnessLimited(_native, value); }
		}

		public float StopCFM
		{
			get { return btRotationalLimitMotor2_getStopCFM(_native); }
			set { btRotationalLimitMotor2_setStopCFM(_native, value); }
		}

		public float StopERP
		{
			get { return btRotationalLimitMotor2_getStopERP(_native); }
			set { btRotationalLimitMotor2_setStopERP(_native, value); }
		}

		public float TargetVelocity
		{
			get { return btRotationalLimitMotor2_getTargetVelocity(_native); }
			set { btRotationalLimitMotor2_setTargetVelocity(_native, value); }
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
					btRotationalLimitMotor2_delete(_native);
				}
				_native = IntPtr.Zero;
			}
		}

		~RotationalLimitMotor2()
		{
			Dispose(false);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btRotationalLimitMotor2_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btRotationalLimitMotor2_new2(IntPtr limot);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btRotationalLimitMotor2_getBounce(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btRotationalLimitMotor2_getCurrentLimit(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btRotationalLimitMotor2_getCurrentLimitError(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btRotationalLimitMotor2_getCurrentLimitErrorHi(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btRotationalLimitMotor2_getCurrentPosition(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btRotationalLimitMotor2_getEnableMotor(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btRotationalLimitMotor2_getEnableSpring(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btRotationalLimitMotor2_getEquilibriumPoint(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btRotationalLimitMotor2_getHiLimit(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btRotationalLimitMotor2_getLoLimit(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btRotationalLimitMotor2_getMaxMotorForce(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btRotationalLimitMotor2_getMotorCFM(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btRotationalLimitMotor2_getMotorERP(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btRotationalLimitMotor2_getServoMotor(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btRotationalLimitMotor2_getServoTarget(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btRotationalLimitMotor2_getSpringDamping(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btRotationalLimitMotor2_getSpringDampingLimited(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btRotationalLimitMotor2_getSpringStiffness(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btRotationalLimitMotor2_getSpringStiffnessLimited(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btRotationalLimitMotor2_getStopCFM(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btRotationalLimitMotor2_getStopERP(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btRotationalLimitMotor2_getTargetVelocity(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btRotationalLimitMotor2_isLimited(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btRotationalLimitMotor2_setBounce(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btRotationalLimitMotor2_setCurrentLimit(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btRotationalLimitMotor2_setCurrentLimitError(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btRotationalLimitMotor2_setCurrentLimitErrorHi(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btRotationalLimitMotor2_setCurrentPosition(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btRotationalLimitMotor2_setEnableMotor(IntPtr obj, bool value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btRotationalLimitMotor2_setEnableSpring(IntPtr obj, bool value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btRotationalLimitMotor2_setEquilibriumPoint(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btRotationalLimitMotor2_setHiLimit(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btRotationalLimitMotor2_setLoLimit(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btRotationalLimitMotor2_setMaxMotorForce(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btRotationalLimitMotor2_setMotorCFM(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btRotationalLimitMotor2_setMotorERP(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btRotationalLimitMotor2_setServoMotor(IntPtr obj, bool value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btRotationalLimitMotor2_setServoTarget(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btRotationalLimitMotor2_setSpringDamping(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btRotationalLimitMotor2_setSpringDampingLimited(IntPtr obj, bool value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btRotationalLimitMotor2_setSpringStiffness(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btRotationalLimitMotor2_setSpringStiffnessLimited(IntPtr obj, bool value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btRotationalLimitMotor2_setStopCFM(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btRotationalLimitMotor2_setStopERP(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btRotationalLimitMotor2_setTargetVelocity(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btRotationalLimitMotor2_testLimitValue(IntPtr obj, float test_value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btRotationalLimitMotor2_delete(IntPtr obj);
	}

	public class TranslationalLimitMotor2 : IDisposable
	{
		internal IntPtr _native;
		bool _preventDelete;

		internal TranslationalLimitMotor2(IntPtr native, bool preventDelete)
		{
			_native = native;
			_preventDelete = preventDelete;
		}

		public TranslationalLimitMotor2()
		{
			_native = btTranslationalLimitMotor2_new();
		}

		public TranslationalLimitMotor2(TranslationalLimitMotor2 other)
		{
			_native = btTranslationalLimitMotor2_new2(other._native);
		}

		public bool IsLimited(int limitIndex)
		{
			return btTranslationalLimitMotor2_isLimited(_native, limitIndex);
		}

		public void TestLimitValue(int limitIndex, float testValue)
		{
			btTranslationalLimitMotor2_testLimitValue(_native, limitIndex, testValue);
		}

		public Vector3 Bounce
		{
			get
			{
				Vector3 value;
				btTranslationalLimitMotor2_getBounce(_native, out value);
				return value;
			}
			set { btTranslationalLimitMotor2_setBounce(_native, ref value); }
		}
        /*
		public IntArray CurrentLimit
		{
            get { return new IntArray(btTranslationalLimitMotor2_getCurrentLimit(_native), 3); }
		}
        */
		public Vector3 CurrentLimitError
		{
			get
			{
				Vector3 value;
				btTranslationalLimitMotor2_getCurrentLimitError(_native, out value);
				return value;
			}
			set { btTranslationalLimitMotor2_setCurrentLimitError(_native, ref value); }
		}

		public Vector3 CurrentLimitErrorHi
		{
			get
			{
				Vector3 value;
				btTranslationalLimitMotor2_getCurrentLimitErrorHi(_native, out value);
				return value;
			}
			set { btTranslationalLimitMotor2_setCurrentLimitErrorHi(_native, ref value); }
		}

		public Vector3 CurrentLinearDiff
		{
			get
			{
				Vector3 value;
				btTranslationalLimitMotor2_getCurrentLinearDiff(_native, out value);
				return value;
			}
			set { btTranslationalLimitMotor2_setCurrentLinearDiff(_native, ref value); }
		}
        /*
		public BoolArray EnableMotor
		{
            get { return new BoolArray(btTranslationalLimitMotor2_getEnableMotor(_native), 3); }
		}

		public BoolArray EnableSpring
		{
            get { return new BoolArray(btTranslationalLimitMotor2_getEnableSpring(_native), 3); }
		}
        */
		public Vector3 EquilibriumPoint
		{
			get
			{
				Vector3 value;
				btTranslationalLimitMotor2_getEquilibriumPoint(_native, out value);
				return value;
			}
			set { btTranslationalLimitMotor2_setEquilibriumPoint(_native, ref value); }
		}

		public Vector3 LowerLimit
		{
			get
			{
				Vector3 value;
				btTranslationalLimitMotor2_getLowerLimit(_native, out value);
				return value;
			}
			set { btTranslationalLimitMotor2_setLowerLimit(_native, ref value); }
		}

		public Vector3 MaxMotorForce
		{
			get
			{
				Vector3 value;
				btTranslationalLimitMotor2_getMaxMotorForce(_native, out value);
				return value;
			}
			set { btTranslationalLimitMotor2_setMaxMotorForce(_native, ref value); }
		}

		public Vector3 MotorCFM
		{
			get
			{
				Vector3 value;
				btTranslationalLimitMotor2_getMotorCFM(_native, out value);
				return value;
			}
			set { btTranslationalLimitMotor2_setMotorCFM(_native, ref value); }
		}

		public Vector3 MotorERP
		{
			get
			{
				Vector3 value;
				btTranslationalLimitMotor2_getMotorERP(_native, out value);
				return value;
			}
			set { btTranslationalLimitMotor2_setMotorERP(_native, ref value); }
		}
        /*
		public BoolArray ServoMotor
		{
            get { return new BoolArray(btTranslationalLimitMotor2_getServoMotor(_native)); }
		}
        */
		public Vector3 ServoTarget
		{
			get
			{
				Vector3 value;
				btTranslationalLimitMotor2_getServoTarget(_native, out value);
				return value;
			}
			set { btTranslationalLimitMotor2_setServoTarget(_native, ref value); }
		}

		public Vector3 SpringDamping
		{
			get
			{
				Vector3 value;
				btTranslationalLimitMotor2_getSpringDamping(_native, out value);
				return value;
			}
			set { btTranslationalLimitMotor2_setSpringDamping(_native, ref value); }
		}
        /*
		public BoolArray SpringDampingLimited
		{
			get { return btTranslationalLimitMotor2_getSpringDampingLimited(_native); }
		}
        */
		public Vector3 SpringStiffness
		{
			get
			{
				Vector3 value;
				btTranslationalLimitMotor2_getSpringStiffness(_native, out value);
				return value;
			}
			set { btTranslationalLimitMotor2_setSpringStiffness(_native, ref value); }
		}
        /*
		public BoolArray SpringStiffnessLimited
		{
			get { return btTranslationalLimitMotor2_getSpringStiffnessLimited(_native); }
		}
        */
		public Vector3 StopCFM
		{
			get
			{
				Vector3 value;
				btTranslationalLimitMotor2_getStopCFM(_native, out value);
				return value;
			}
			set { btTranslationalLimitMotor2_setStopCFM(_native, ref value); }
		}

		public Vector3 StopERP
		{
			get
			{
				Vector3 value;
				btTranslationalLimitMotor2_getStopERP(_native, out value);
				return value;
			}
			set { btTranslationalLimitMotor2_setStopERP(_native, ref value); }
		}

		public Vector3 TargetVelocity
		{
			get
			{
				Vector3 value;
				btTranslationalLimitMotor2_getTargetVelocity(_native, out value);
				return value;
			}
			set { btTranslationalLimitMotor2_setTargetVelocity(_native, ref value); }
		}

		public Vector3 UpperLimit
		{
			get
			{
				Vector3 value;
				btTranslationalLimitMotor2_getUpperLimit(_native, out value);
				return value;
			}
			set { btTranslationalLimitMotor2_setUpperLimit(_native, ref value); }
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
					btTranslationalLimitMotor2_delete(_native);
				}
				_native = IntPtr.Zero;
			}
		}

		~TranslationalLimitMotor2()
		{
			Dispose(false);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btTranslationalLimitMotor2_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btTranslationalLimitMotor2_new2(IntPtr other);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTranslationalLimitMotor2_getBounce(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btTranslationalLimitMotor2_getCurrentLimit(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTranslationalLimitMotor2_getCurrentLimitError(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTranslationalLimitMotor2_getCurrentLimitErrorHi(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTranslationalLimitMotor2_getCurrentLinearDiff(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern IntPtr btTranslationalLimitMotor2_getEnableMotor(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern IntPtr btTranslationalLimitMotor2_getEnableSpring(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTranslationalLimitMotor2_getEquilibriumPoint(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTranslationalLimitMotor2_getLowerLimit(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTranslationalLimitMotor2_getMaxMotorForce(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTranslationalLimitMotor2_getMotorCFM(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTranslationalLimitMotor2_getMotorERP(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern IntPtr btTranslationalLimitMotor2_getServoMotor(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTranslationalLimitMotor2_getServoTarget(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTranslationalLimitMotor2_getSpringDamping(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern IntPtr btTranslationalLimitMotor2_getSpringDampingLimited(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTranslationalLimitMotor2_getSpringStiffness(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern IntPtr btTranslationalLimitMotor2_getSpringStiffnessLimited(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTranslationalLimitMotor2_getStopCFM(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTranslationalLimitMotor2_getStopERP(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTranslationalLimitMotor2_getTargetVelocity(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTranslationalLimitMotor2_getUpperLimit(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btTranslationalLimitMotor2_isLimited(IntPtr obj, int limitIndex);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTranslationalLimitMotor2_setBounce(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTranslationalLimitMotor2_setCurrentLimitError(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTranslationalLimitMotor2_setCurrentLimitErrorHi(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTranslationalLimitMotor2_setCurrentLinearDiff(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTranslationalLimitMotor2_setEquilibriumPoint(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTranslationalLimitMotor2_setLowerLimit(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTranslationalLimitMotor2_setMaxMotorForce(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTranslationalLimitMotor2_setMotorCFM(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTranslationalLimitMotor2_setMotorERP(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTranslationalLimitMotor2_setServoTarget(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTranslationalLimitMotor2_setSpringDamping(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTranslationalLimitMotor2_setSpringStiffness(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTranslationalLimitMotor2_setStopCFM(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTranslationalLimitMotor2_setStopERP(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTranslationalLimitMotor2_setTargetVelocity(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTranslationalLimitMotor2_setUpperLimit(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTranslationalLimitMotor2_testLimitValue(IntPtr obj, int limitIndex, float test_value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTranslationalLimitMotor2_delete(IntPtr obj);
	}

	public class Generic6DofSpring2Constraint : TypedConstraint
	{
        RotationalLimitMotor2[] _angularLimits = new RotationalLimitMotor2[3];
        TranslationalLimitMotor2 _linearLimits;

		internal Generic6DofSpring2Constraint(IntPtr native)
			: base(native)
		{
		}

		public Generic6DofSpring2Constraint(RigidBody rigidBodyA, RigidBody rigidBodyB, Matrix frameInA, Matrix frameInB)
			: base(btGeneric6DofSpring2Constraint_new(rigidBodyA._native, rigidBodyB._native, ref frameInA, ref frameInB))
		{
			_rigidBodyA = rigidBodyA;
			_rigidBodyB = rigidBodyB;
		}

		public Generic6DofSpring2Constraint(RigidBody rigidBodyA, RigidBody rigidBodyB, Matrix frameInA, Matrix frameInB, RotateOrder rotOrder)
			: base(btGeneric6DofSpring2Constraint_new2(rigidBodyA._native, rigidBodyB._native, ref frameInA, ref frameInB, rotOrder))
		{
			_rigidBodyA = rigidBodyA;
			_rigidBodyB = rigidBodyB;
		}

		public Generic6DofSpring2Constraint(RigidBody rigidBodyB, Matrix frameInB)
			: base(btGeneric6DofSpring2Constraint_new3(rigidBodyB._native, ref frameInB))
		{
            _rigidBodyA = GetFixedBody();
			_rigidBodyB = rigidBodyB;
		}

		public Generic6DofSpring2Constraint(RigidBody rigidBodyB, Matrix frameInB, RotateOrder rotOrder)
			: base(btGeneric6DofSpring2Constraint_new4(rigidBodyB._native, ref frameInB, rotOrder))
		{
            _rigidBodyA = GetFixedBody();
			_rigidBodyB = rigidBodyB;
		}

		public void CalculateTransforms(Matrix transA, Matrix transB)
		{
			btGeneric6DofSpring2Constraint_calculateTransforms(_native, ref transA, ref transB);
		}

		public void CalculateTransforms()
		{
			btGeneric6DofSpring2Constraint_calculateTransforms2(_native);
		}

		public void EnableMotor(int index, bool onOff)
		{
			btGeneric6DofSpring2Constraint_enableMotor(_native, index, onOff);
		}

		public void EnableSpring(int index, bool onOff)
		{
			btGeneric6DofSpring2Constraint_enableSpring(_native, index, onOff);
		}

		public float GetAngle(int axisIndex)
		{
			return btGeneric6DofSpring2Constraint_getAngle(_native, axisIndex);
		}

		public Vector3 GetAxis(int axisIndex)
		{
			Vector3 value;
			btGeneric6DofSpring2Constraint_getAxis(_native, axisIndex, out value);
			return value;
		}

		public float GetRelativePivotPosition(int axisIndex)
		{
			return btGeneric6DofSpring2Constraint_getRelativePivotPosition(_native, axisIndex);
		}

		public RotationalLimitMotor2 GetRotationalLimitMotor(int index)
		{
            if (_angularLimits[index] == null)
            {
                _angularLimits[index] = new RotationalLimitMotor2(btGeneric6DofSpring2Constraint_getRotationalLimitMotor(_native, index), true);
            }
            return _angularLimits[index];
		}

		public bool IsLimited(int limitIndex)
		{
			return btGeneric6DofSpring2Constraint_isLimited(_native, limitIndex);
		}

		public void SetAxis(Vector3 axis1, Vector3 axis2)
		{
			btGeneric6DofSpring2Constraint_setAxis(_native, ref axis1, ref axis2);
		}

		public void SetBounce(int index, float bounce)
		{
			btGeneric6DofSpring2Constraint_setBounce(_native, index, bounce);
		}

		public void SetDamping(int index, float damping)
		{
			btGeneric6DofSpring2Constraint_setDamping(_native, index, damping);
		}

		public void SetDamping(int index, float damping, bool limitIfNeeded)
		{
			btGeneric6DofSpring2Constraint_setDamping2(_native, index, damping, limitIfNeeded);
		}

		public void SetEquilibriumPoint()
		{
			btGeneric6DofSpring2Constraint_setEquilibriumPoint(_native);
		}

		public void SetEquilibriumPoint(int index, float val)
		{
			btGeneric6DofSpring2Constraint_setEquilibriumPoint2(_native, index, val);
		}

		public void SetEquilibriumPoint(int index)
		{
			btGeneric6DofSpring2Constraint_setEquilibriumPoint3(_native, index);
		}

		public void SetFrames(Matrix frameA, Matrix frameB)
		{
			btGeneric6DofSpring2Constraint_setFrames(_native, ref frameA, ref frameB);
		}

		public void SetLimit(int axis, float lo, float hi)
		{
			btGeneric6DofSpring2Constraint_setLimit(_native, axis, lo, hi);
		}

		public void SetLimitReversed(int axis, float lo, float hi)
		{
			btGeneric6DofSpring2Constraint_setLimitReversed(_native, axis, lo, hi);
		}

		public void SetMaxMotorForce(int index, float force)
		{
			btGeneric6DofSpring2Constraint_setMaxMotorForce(_native, index, force);
		}

		public void SetServo(int index, bool onOff)
		{
			btGeneric6DofSpring2Constraint_setServo(_native, index, onOff);
		}

		public void SetServoTarget(int index, float target)
		{
			btGeneric6DofSpring2Constraint_setServoTarget(_native, index, target);
		}

		public void SetStiffness(int index, float stiffness)
		{
			btGeneric6DofSpring2Constraint_setStiffness(_native, index, stiffness);
		}

		public void SetStiffness(int index, float stiffness, bool limitIfNeeded)
		{
			btGeneric6DofSpring2Constraint_setStiffness2(_native, index, stiffness, limitIfNeeded);
		}

		public void SetTargetVelocity(int index, float velocity)
		{
			btGeneric6DofSpring2Constraint_setTargetVelocity(_native, index, velocity);
		}

		public Vector3 AngularLowerLimit
		{
			get
			{
				Vector3 value;
				btGeneric6DofSpring2Constraint_getAngularLowerLimit(_native, out value);
				return value;
			}
			set { btGeneric6DofSpring2Constraint_setAngularLowerLimit(_native, ref value); }
		}

		public Vector3 AngularLowerLimitReversed
		{
			get
			{
				Vector3 value;
				btGeneric6DofSpring2Constraint_getAngularLowerLimitReversed(_native, out value);
				return value;
			}
			set { btGeneric6DofSpring2Constraint_setAngularLowerLimitReversed(_native, ref value); }
		}

		public Vector3 AngularUpperLimit
		{
			get
			{
				Vector3 value;
				btGeneric6DofSpring2Constraint_getAngularUpperLimit(_native, out value);
				return value;
			}
			set { btGeneric6DofSpring2Constraint_setAngularUpperLimit(_native, ref value); }
		}

		public Vector3 AngularUpperLimitReversed
		{
			get
			{
				Vector3 value;
				btGeneric6DofSpring2Constraint_getAngularUpperLimitReversed(_native, out value);
				return value;
			}
			set { btGeneric6DofSpring2Constraint_setAngularUpperLimitReversed(_native, ref value); }
		}

		public Matrix CalculatedTransformA
		{
			get
			{
				Matrix value;
				btGeneric6DofSpring2Constraint_getCalculatedTransformA(_native, out value);
				return value;
			}
		}

		public Matrix CalculatedTransformB
		{
			get
			{
				Matrix value;
				btGeneric6DofSpring2Constraint_getCalculatedTransformB(_native, out value);
				return value;
			}
		}

		public Matrix FrameOffsetA
		{
			get
			{
				Matrix value;
				btGeneric6DofSpring2Constraint_getFrameOffsetA(_native, out value);
				return value;
			}
		}

		public Matrix FrameOffsetB
		{
			get
			{
				Matrix value;
				btGeneric6DofSpring2Constraint_getFrameOffsetB(_native, out value);
				return value;
			}
		}

		public Vector3 LinearLowerLimit
		{
			get
			{
				Vector3 value;
				btGeneric6DofSpring2Constraint_getLinearLowerLimit(_native, out value);
				return value;
			}
			set { btGeneric6DofSpring2Constraint_setLinearLowerLimit(_native, ref value); }
		}

		public Vector3 LinearUpperLimit
		{
			get
			{
				Vector3 value;
				btGeneric6DofSpring2Constraint_getLinearUpperLimit(_native, out value);
				return value;
			}
			set { btGeneric6DofSpring2Constraint_setLinearUpperLimit(_native, ref value); }
		}

		public RotateOrder RotationOrder
		{
			get { return btGeneric6DofSpring2Constraint_getRotationOrder(_native); }
			set { btGeneric6DofSpring2Constraint_setRotationOrder(_native, value); }
		}

		public TranslationalLimitMotor2 TranslationalLimitMotor
		{
            get
            {
                if (_linearLimits == null)
                {
                    _linearLimits = new TranslationalLimitMotor2(btGeneric6DofSpring2Constraint_getTranslationalLimitMotor(_native), true);
                }
                return _linearLimits;
            }
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btGeneric6DofSpring2Constraint_new(IntPtr rbA, IntPtr rbB, [In] ref Matrix frameInA, [In] ref Matrix frameInB);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btGeneric6DofSpring2Constraint_new2(IntPtr rbA, IntPtr rbB, [In] ref Matrix frameInA, [In] ref Matrix frameInB, RotateOrder rotOrder);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btGeneric6DofSpring2Constraint_new3(IntPtr rbB, [In] ref Matrix frameInB);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btGeneric6DofSpring2Constraint_new4(IntPtr rbB, [In] ref Matrix frameInB, RotateOrder rotOrder);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGeneric6DofSpring2Constraint_calculateTransforms(IntPtr obj, [In] ref Matrix transA, [In] ref Matrix transB);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGeneric6DofSpring2Constraint_calculateTransforms2(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGeneric6DofSpring2Constraint_enableMotor(IntPtr obj, int index, bool onOff);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGeneric6DofSpring2Constraint_enableSpring(IntPtr obj, int index, bool onOff);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btGeneric6DofSpring2Constraint_getAngle(IntPtr obj, int axis_index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGeneric6DofSpring2Constraint_getAngularLowerLimit(IntPtr obj, [Out] out Vector3 angularLower);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGeneric6DofSpring2Constraint_getAngularLowerLimitReversed(IntPtr obj, [Out] out Vector3 angularLower);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGeneric6DofSpring2Constraint_getAngularUpperLimit(IntPtr obj, [Out] out Vector3 angularUpper);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGeneric6DofSpring2Constraint_getAngularUpperLimitReversed(IntPtr obj, [Out] out Vector3 angularUpper);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGeneric6DofSpring2Constraint_getAxis(IntPtr obj, int axis_index, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGeneric6DofSpring2Constraint_getCalculatedTransformA(IntPtr obj, [Out] out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGeneric6DofSpring2Constraint_getCalculatedTransformB(IntPtr obj, [Out] out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGeneric6DofSpring2Constraint_getFrameOffsetA(IntPtr obj, [Out] out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGeneric6DofSpring2Constraint_getFrameOffsetB(IntPtr obj, [Out] out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGeneric6DofSpring2Constraint_getLinearLowerLimit(IntPtr obj, [Out] out Vector3 linearLower);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGeneric6DofSpring2Constraint_getLinearUpperLimit(IntPtr obj, [Out] out Vector3 linearUpper);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btGeneric6DofSpring2Constraint_getRelativePivotPosition(IntPtr obj, int axis_index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btGeneric6DofSpring2Constraint_getRotationalLimitMotor(IntPtr obj, int index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern RotateOrder btGeneric6DofSpring2Constraint_getRotationOrder(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btGeneric6DofSpring2Constraint_getTranslationalLimitMotor(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btGeneric6DofSpring2Constraint_isLimited(IntPtr obj, int limitIndex);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGeneric6DofSpring2Constraint_setAngularLowerLimit(IntPtr obj, [In] ref Vector3 angularLower);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGeneric6DofSpring2Constraint_setAngularLowerLimitReversed(IntPtr obj, [In] ref Vector3 angularLower);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGeneric6DofSpring2Constraint_setAngularUpperLimit(IntPtr obj, [In] ref Vector3 angularUpper);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGeneric6DofSpring2Constraint_setAngularUpperLimitReversed(IntPtr obj, [In] ref Vector3 angularUpper);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGeneric6DofSpring2Constraint_setAxis(IntPtr obj, [In] ref Vector3 axis1, [In] ref Vector3 axis2);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGeneric6DofSpring2Constraint_setBounce(IntPtr obj, int index, float bounce);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGeneric6DofSpring2Constraint_setDamping(IntPtr obj, int index, float damping);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGeneric6DofSpring2Constraint_setDamping2(IntPtr obj, int index, float damping, bool limitIfNeeded);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGeneric6DofSpring2Constraint_setEquilibriumPoint(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGeneric6DofSpring2Constraint_setEquilibriumPoint2(IntPtr obj, int index, float val);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGeneric6DofSpring2Constraint_setEquilibriumPoint3(IntPtr obj, int index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGeneric6DofSpring2Constraint_setFrames(IntPtr obj, [In] ref Matrix frameA, [In] ref Matrix frameB);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGeneric6DofSpring2Constraint_setLimit(IntPtr obj, int axis, float lo, float hi);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGeneric6DofSpring2Constraint_setLimitReversed(IntPtr obj, int axis, float lo, float hi);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGeneric6DofSpring2Constraint_setLinearLowerLimit(IntPtr obj, [In] ref Vector3 linearLower);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGeneric6DofSpring2Constraint_setLinearUpperLimit(IntPtr obj, [In] ref Vector3 linearUpper);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGeneric6DofSpring2Constraint_setMaxMotorForce(IntPtr obj, int index, float force);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGeneric6DofSpring2Constraint_setRotationOrder(IntPtr obj, RotateOrder order);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGeneric6DofSpring2Constraint_setServo(IntPtr obj, int index, bool onOff);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGeneric6DofSpring2Constraint_setServoTarget(IntPtr obj, int index, float target);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGeneric6DofSpring2Constraint_setStiffness(IntPtr obj, int index, float stiffness);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGeneric6DofSpring2Constraint_setStiffness2(IntPtr obj, int index, float stiffness, bool limitIfNeeded);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGeneric6DofSpring2Constraint_setTargetVelocity(IntPtr obj, int index, float velocity);
	}

    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct Generic6DofSpring2ConstraintFloatData
    {
        public TypedConstraintFloatData TypeConstraintData;
        public TransformFloatData RigidBodyAFrame;
        public TransformFloatData RigidBodyBFrame;
        public Vector3FloatData LinearUpperLimit;
        public Vector3FloatData LinearLowerLimit;
        public Vector3FloatData LinearBounce;
        public Vector3FloatData LinearStopErp;
        public Vector3FloatData LinearStopCfm;
        public Vector3FloatData LinearMotorErp;
        public Vector3FloatData LinearMotorCfm;
        public Vector3FloatData LinearTargetVelocity;
        public Vector3FloatData LinearMaxMotorForce;
        public Vector3FloatData LinearServoTarget;
        public Vector3FloatData LinearSpringStiffness;
        public Vector3FloatData LinearSpringDamping;
        public Vector3FloatData LinearEquilibriumPoint;
        public fixed byte LinearEnableMotor[4];
        public fixed byte LinearServoMotor[4];
        public fixed byte LinearEnableSpring[4];
        public fixed byte LinearSpringStiffnessLimited[4];
        public fixed byte LinearSpringDampingLimited[4];
        public int Padding;
        public Vector3FloatData AngularUpperLimit;
        public Vector3FloatData AngularLowerLimit;
        public Vector3FloatData AngularBounce;
        public Vector3FloatData AngularStopErp;
        public Vector3FloatData AngularStopCfm;
        public Vector3FloatData AngularMotorErp;
        public Vector3FloatData AngularMotorCfm;
        public Vector3FloatData AngularTargetVelocity;
        public Vector3FloatData AngularMaxMotorForce;
        public Vector3FloatData AngularServoTarget;
        public Vector3FloatData AngularSpringStiffness;
        public Vector3FloatData AngularSpringDamping;
        public Vector3FloatData AngularEquilibriumPoint;
        public fixed byte AngularEnableMotor[4];
        public fixed byte AngularServoMotor[4];
        public fixed byte AngularEnableSpring[4];
        public fixed byte AngularSpringStiffnessLimited[4];
        public fixed byte AngularSpringDampingLimited[4];
        public int RotateOrder;

        public static int Offset(string fieldName) { return Marshal.OffsetOf(typeof(Generic6DofSpring2ConstraintFloatData), fieldName).ToInt32(); }
    }
}
