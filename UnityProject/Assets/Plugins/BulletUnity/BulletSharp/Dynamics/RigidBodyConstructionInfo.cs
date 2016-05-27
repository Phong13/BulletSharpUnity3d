using System;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.Math;

namespace BulletSharp
{

	public class RigidBodyConstructionInfo : IDisposable
	{
		internal IntPtr _native;

        private CollisionShape _collisionShape;
        internal MotionState _motionState;

		public RigidBodyConstructionInfo(float mass, MotionState motionState, CollisionShape collisionShape)
		{
            _native = btRigidBody_btRigidBodyConstructionInfo_new(mass, (motionState != null) ? motionState._native : IntPtr.Zero,
                (collisionShape != null) ? collisionShape._native : IntPtr.Zero);
            _collisionShape = collisionShape;
            _motionState = motionState;
		}

		public RigidBodyConstructionInfo(float mass, MotionState motionState, CollisionShape collisionShape, Vector3 localInertia)
		{
            _native = btRigidBody_btRigidBodyConstructionInfo_new2(mass, (motionState != null) ? motionState._native : IntPtr.Zero,
                (collisionShape != null) ? collisionShape._native : IntPtr.Zero, ref localInertia);
            _collisionShape = collisionShape;
            _motionState = motionState;
		}

		public float AdditionalAngularDampingFactor
		{
			get { return btRigidBody_btRigidBodyConstructionInfo_getAdditionalAngularDampingFactor(_native); }
			set { btRigidBody_btRigidBodyConstructionInfo_setAdditionalAngularDampingFactor(_native, value); }
		}

		public float AdditionalAngularDampingThresholdSqr
		{
			get { return btRigidBody_btRigidBodyConstructionInfo_getAdditionalAngularDampingThresholdSqr(_native); }
			set { btRigidBody_btRigidBodyConstructionInfo_setAdditionalAngularDampingThresholdSqr(_native, value); }
		}

		public bool AdditionalDamping
		{
			get { return btRigidBody_btRigidBodyConstructionInfo_getAdditionalDamping(_native); }
			set { btRigidBody_btRigidBodyConstructionInfo_setAdditionalDamping(_native, value); }
		}

		public float AdditionalDampingFactor
		{
			get { return btRigidBody_btRigidBodyConstructionInfo_getAdditionalDampingFactor(_native); }
			set { btRigidBody_btRigidBodyConstructionInfo_setAdditionalDampingFactor(_native, value); }
		}

		public float AdditionalLinearDampingThresholdSqr
		{
			get { return btRigidBody_btRigidBodyConstructionInfo_getAdditionalLinearDampingThresholdSqr(_native); }
			set { btRigidBody_btRigidBodyConstructionInfo_setAdditionalLinearDampingThresholdSqr(_native, value); }
		}

		public float AngularDamping
		{
			get { return btRigidBody_btRigidBodyConstructionInfo_getAngularDamping(_native); }
			set { btRigidBody_btRigidBodyConstructionInfo_setAngularDamping(_native, value); }
		}

		public float AngularSleepingThreshold
		{
			get { return btRigidBody_btRigidBodyConstructionInfo_getAngularSleepingThreshold(_native); }
			set { btRigidBody_btRigidBodyConstructionInfo_setAngularSleepingThreshold(_native, value); }
		}

		public CollisionShape CollisionShape
		{
            get { return _collisionShape; }
            set
            {
                _collisionShape = value;
                btRigidBody_btRigidBodyConstructionInfo_setCollisionShape(_native, (value != null) ? value._native : IntPtr.Zero);
            }
		}

		public float Friction
		{
			get { return btRigidBody_btRigidBodyConstructionInfo_getFriction(_native); }
			set { btRigidBody_btRigidBodyConstructionInfo_setFriction(_native, value); }
		}

		public float LinearDamping
		{
			get { return btRigidBody_btRigidBodyConstructionInfo_getLinearDamping(_native); }
			set { btRigidBody_btRigidBodyConstructionInfo_setLinearDamping(_native, value); }
		}

		public float LinearSleepingThreshold
		{
			get { return btRigidBody_btRigidBodyConstructionInfo_getLinearSleepingThreshold(_native); }
			set { btRigidBody_btRigidBodyConstructionInfo_setLinearSleepingThreshold(_native, value); }
		}

		public Vector3 LocalInertia
		{
			get
			{
				Vector3 value;
				btRigidBody_btRigidBodyConstructionInfo_getLocalInertia(_native, out value);
				return value;
			}
			set { btRigidBody_btRigidBodyConstructionInfo_setLocalInertia(_native, ref value); }
		}

		public float Mass
		{
			get { return btRigidBody_btRigidBodyConstructionInfo_getMass(_native); }
			set { btRigidBody_btRigidBodyConstructionInfo_setMass(_native, value); }
		}

		public MotionState MotionState
		{
            get { return _motionState; }
            set
            {
                btRigidBody_btRigidBodyConstructionInfo_setMotionState(_native, (value != null) ? value._native : IntPtr.Zero);
                _motionState = value;
            }
		}

		public float Restitution
		{
			get { return btRigidBody_btRigidBodyConstructionInfo_getRestitution(_native); }
			set { btRigidBody_btRigidBodyConstructionInfo_setRestitution(_native, value); }
		}

		public float RollingFriction
		{
			get { return btRigidBody_btRigidBodyConstructionInfo_getRollingFriction(_native); }
			set { btRigidBody_btRigidBodyConstructionInfo_setRollingFriction(_native, value); }
		}

		public Matrix StartWorldTransform
		{
			get
			{
				Matrix value;
				btRigidBody_btRigidBodyConstructionInfo_getStartWorldTransform(_native, out value);
				return value;
			}
			set { btRigidBody_btRigidBodyConstructionInfo_setStartWorldTransform(_native, ref value); }
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
				btRigidBody_btRigidBodyConstructionInfo_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~RigidBodyConstructionInfo()
		{
			Dispose(false);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btRigidBody_btRigidBodyConstructionInfo_new(float mass, IntPtr motionState, IntPtr collisionShape);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btRigidBody_btRigidBodyConstructionInfo_new2(float mass, IntPtr motionState, IntPtr collisionShape, [In] ref Vector3 localInertia);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btRigidBody_btRigidBodyConstructionInfo_getAdditionalAngularDampingFactor(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btRigidBody_btRigidBodyConstructionInfo_getAdditionalAngularDampingThresholdSqr(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btRigidBody_btRigidBodyConstructionInfo_getAdditionalDamping(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btRigidBody_btRigidBodyConstructionInfo_getAdditionalDampingFactor(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btRigidBody_btRigidBodyConstructionInfo_getAdditionalLinearDampingThresholdSqr(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btRigidBody_btRigidBodyConstructionInfo_getAngularDamping(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btRigidBody_btRigidBodyConstructionInfo_getAngularSleepingThreshold(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btRigidBody_btRigidBodyConstructionInfo_getCollisionShape(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btRigidBody_btRigidBodyConstructionInfo_getFriction(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btRigidBody_btRigidBodyConstructionInfo_getLinearDamping(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btRigidBody_btRigidBodyConstructionInfo_getLinearSleepingThreshold(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btRigidBody_btRigidBodyConstructionInfo_getLocalInertia(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btRigidBody_btRigidBodyConstructionInfo_getMass(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btRigidBody_btRigidBodyConstructionInfo_getMotionState(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btRigidBody_btRigidBodyConstructionInfo_getRestitution(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btRigidBody_btRigidBodyConstructionInfo_getRollingFriction(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btRigidBody_btRigidBodyConstructionInfo_getStartWorldTransform(IntPtr obj, [Out] out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btRigidBody_btRigidBodyConstructionInfo_setAdditionalAngularDampingFactor(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btRigidBody_btRigidBodyConstructionInfo_setAdditionalAngularDampingThresholdSqr(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btRigidBody_btRigidBodyConstructionInfo_setAdditionalDamping(IntPtr obj, bool value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btRigidBody_btRigidBodyConstructionInfo_setAdditionalDampingFactor(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btRigidBody_btRigidBodyConstructionInfo_setAdditionalLinearDampingThresholdSqr(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btRigidBody_btRigidBodyConstructionInfo_setAngularDamping(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btRigidBody_btRigidBodyConstructionInfo_setAngularSleepingThreshold(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btRigidBody_btRigidBodyConstructionInfo_setCollisionShape(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btRigidBody_btRigidBodyConstructionInfo_setFriction(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btRigidBody_btRigidBodyConstructionInfo_setLinearDamping(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btRigidBody_btRigidBodyConstructionInfo_setLinearSleepingThreshold(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btRigidBody_btRigidBodyConstructionInfo_setLocalInertia(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btRigidBody_btRigidBodyConstructionInfo_setMass(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btRigidBody_btRigidBodyConstructionInfo_setMotionState(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btRigidBody_btRigidBodyConstructionInfo_setRestitution(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btRigidBody_btRigidBodyConstructionInfo_setRollingFriction(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btRigidBody_btRigidBodyConstructionInfo_setStartWorldTransform(IntPtr obj, [In] ref Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btRigidBody_btRigidBodyConstructionInfo_delete(IntPtr obj);
    }
}
