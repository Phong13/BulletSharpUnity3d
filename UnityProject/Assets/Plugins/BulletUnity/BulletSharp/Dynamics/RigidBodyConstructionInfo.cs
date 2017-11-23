using System;
using BulletSharp.Math;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class RigidBodyConstructionInfo : IDisposable
	{
		internal IntPtr Native;

		private CollisionShape _collisionShape;
		private MotionState _motionState;

		public RigidBodyConstructionInfo(float mass, MotionState motionState,
			CollisionShape collisionShape)
		{
			Native = btRigidBody_btRigidBodyConstructionInfo_new(mass, motionState != null ? motionState._native : IntPtr.Zero,
				collisionShape != null ? collisionShape.Native : IntPtr.Zero);
			_collisionShape = collisionShape;
			_motionState = motionState;
		}

		public RigidBodyConstructionInfo(float mass, MotionState motionState,
			CollisionShape collisionShape, Vector3 localInertia)
		{
			Native = btRigidBody_btRigidBodyConstructionInfo_new2(mass, motionState != null ? motionState._native : IntPtr.Zero,
				collisionShape != null ? collisionShape.Native : IntPtr.Zero, ref localInertia);
			_collisionShape = collisionShape;
			_motionState = motionState;
		}

		public float AdditionalAngularDampingFactor
		{
			get => btRigidBody_btRigidBodyConstructionInfo_getAdditionalAngularDampingFactor(Native);
			set => btRigidBody_btRigidBodyConstructionInfo_setAdditionalAngularDampingFactor(Native, value);
		}

		public float AdditionalAngularDampingThresholdSqr
		{
			get => btRigidBody_btRigidBodyConstructionInfo_getAdditionalAngularDampingThresholdSqr(Native);
			set => btRigidBody_btRigidBodyConstructionInfo_setAdditionalAngularDampingThresholdSqr(Native, value);
		}

		public bool AdditionalDamping
		{
			get => btRigidBody_btRigidBodyConstructionInfo_getAdditionalDamping(Native);
			set => btRigidBody_btRigidBodyConstructionInfo_setAdditionalDamping(Native, value);
		}

		public float AdditionalDampingFactor
		{
			get => btRigidBody_btRigidBodyConstructionInfo_getAdditionalDampingFactor(Native);
			set => btRigidBody_btRigidBodyConstructionInfo_setAdditionalDampingFactor(Native, value);
		}

		public float AdditionalLinearDampingThresholdSqr
		{
			get => btRigidBody_btRigidBodyConstructionInfo_getAdditionalLinearDampingThresholdSqr(Native);
			set => btRigidBody_btRigidBodyConstructionInfo_setAdditionalLinearDampingThresholdSqr(Native, value);
		}

		public float AngularDamping
		{
			get => btRigidBody_btRigidBodyConstructionInfo_getAngularDamping(Native);
			set => btRigidBody_btRigidBodyConstructionInfo_setAngularDamping(Native, value);
		}

		public float AngularSleepingThreshold
		{
			get => btRigidBody_btRigidBodyConstructionInfo_getAngularSleepingThreshold(Native);
			set => btRigidBody_btRigidBodyConstructionInfo_setAngularSleepingThreshold(Native, value);
		}

		public CollisionShape CollisionShape
		{
			get => _collisionShape;
			set
			{
				btRigidBody_btRigidBodyConstructionInfo_setCollisionShape(Native, value != null ? value.Native : IntPtr.Zero);
				_collisionShape = value;
			}
		}

		public float Friction
		{
			get => btRigidBody_btRigidBodyConstructionInfo_getFriction(Native);
			set => btRigidBody_btRigidBodyConstructionInfo_setFriction(Native, value);
		}

		public float LinearDamping
		{
			get => btRigidBody_btRigidBodyConstructionInfo_getLinearDamping(Native);
			set => btRigidBody_btRigidBodyConstructionInfo_setLinearDamping(Native, value);
		}

		public float LinearSleepingThreshold
		{
			get => btRigidBody_btRigidBodyConstructionInfo_getLinearSleepingThreshold(Native);
			set => btRigidBody_btRigidBodyConstructionInfo_setLinearSleepingThreshold(Native, value);
		}

		public Vector3 LocalInertia
		{
			get
			{
				Vector3 value;
				btRigidBody_btRigidBodyConstructionInfo_getLocalInertia(Native, out value);
				return value;
			}
			set => btRigidBody_btRigidBodyConstructionInfo_setLocalInertia(Native, ref value);
		}

		public float Mass
		{
			get => btRigidBody_btRigidBodyConstructionInfo_getMass(Native);
			set => btRigidBody_btRigidBodyConstructionInfo_setMass(Native, value);
		}

		public MotionState MotionState
		{
			get => _motionState;
			set
			{
				btRigidBody_btRigidBodyConstructionInfo_setMotionState(Native, value != null ? value._native : IntPtr.Zero);
				_motionState = value;
			}
		}

		public float Restitution
		{
			get => btRigidBody_btRigidBodyConstructionInfo_getRestitution(Native);
			set => btRigidBody_btRigidBodyConstructionInfo_setRestitution(Native, value);
		}

		public float RollingFriction
		{
			get => btRigidBody_btRigidBodyConstructionInfo_getRollingFriction(Native);
			set => btRigidBody_btRigidBodyConstructionInfo_setRollingFriction(Native, value);
		}

		public Matrix StartWorldTransform
		{
			get
			{
				Matrix value;
				btRigidBody_btRigidBodyConstructionInfo_getStartWorldTransform(Native, out value);
				return value;
			}
			set => btRigidBody_btRigidBodyConstructionInfo_setStartWorldTransform(Native, ref value);
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
				btRigidBody_btRigidBodyConstructionInfo_delete(Native);
				Native = IntPtr.Zero;
			}
		}

		~RigidBodyConstructionInfo()
		{
			Dispose(false);
		}
	}
}
