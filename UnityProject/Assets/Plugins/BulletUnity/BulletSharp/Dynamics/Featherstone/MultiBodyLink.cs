using System;
using BulletSharp.Math;


namespace BulletSharp
{
	public enum FeatherstoneJointType
	{
		Revolute = 0,
		Prismatic = 1,
		Spherical = 2,
		Planar = 3,
		Fixed = 4,
		Invalid
	}

	[Flags]
	public enum MultiBodyLinkFlags
	{
		None = 0,
		DisableParentCollision = 1
	}

	public class MultiBodyLink
	{
		internal IntPtr Native;

		internal MultiBodyLink(IntPtr native)
		{
			Native = native;
		}

		public Vector3 GetAxisBottom(int dof)
		{
			Vector3 value;
			UnsafeNativeMethods.btMultibodyLink_getAxisBottom(Native, dof, out value);
			return value;
		}

		public Vector3 GetAxisTop(int dof)
		{
			Vector3 value;
			UnsafeNativeMethods.btMultibodyLink_getAxisTop(Native, dof, out value);
			return value;
		}

		public void SetAxisBottom(int dof, float x, float y, float z)
		{
			UnsafeNativeMethods.btMultibodyLink_setAxisBottom(Native, dof, x, y, z);
		}

		public void SetAxisBottom(int dof, Vector3 axis)
		{
			UnsafeNativeMethods.btMultibodyLink_setAxisBottom2(Native, dof, ref axis);
		}

		public void SetAxisTop(int dof, float x, float y, float z)
		{
			UnsafeNativeMethods.btMultibodyLink_setAxisTop(Native, dof, x, y, z);
		}

		public void SetAxisTop(int dof, Vector3 axis)
		{
			UnsafeNativeMethods.btMultibodyLink_setAxisTop2(Native, dof, ref axis);
		}

		public void UpdateCacheMultiDof(float[] pq = null)
		{
			UnsafeNativeMethods.btMultibodyLink_updateCacheMultiDof(Native, pq);
		}
/*
		public SpatialMotionVector AbsFrameLocVelocity
		{
			get { return UnsafeNativeMethods.btMultibodyLink_getAbsFrameLocVelocity(_native); }
			set { btMultibodyLink_setAbsFrameLocVelocity(_native, value._native); }
		}

		public SpatialMotionVector AbsFrameTotVelocity
		{
			get { return UnsafeNativeMethods.btMultibodyLink_getAbsFrameTotVelocity(_native); }
			set { btMultibodyLink_setAbsFrameTotVelocity(_native, value._native); }
		}
*/
		public Vector3 AppliedConstraintForce
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btMultibodyLink_getAppliedConstraintForce(Native, out value);
				return value;
			}
			set => UnsafeNativeMethods.btMultibodyLink_setAppliedConstraintForce(Native, ref value);
		}

		public Vector3 AppliedConstraintTorque
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btMultibodyLink_getAppliedConstraintTorque(Native, out value);
				return value;
			}
			set => UnsafeNativeMethods.btMultibodyLink_setAppliedConstraintTorque(Native, ref value);
		}

		public Vector3 AppliedForce
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btMultibodyLink_getAppliedForce(Native, out value);
				return value;
			}
			set => UnsafeNativeMethods.btMultibodyLink_setAppliedForce(Native, ref value);
		}

		public Vector3 AppliedTorque
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btMultibodyLink_getAppliedTorque(Native, out value);
				return value;
			}
			set => UnsafeNativeMethods.btMultibodyLink_setAppliedTorque(Native, ref value);
		}
/*
		public SpatialMotionVector[] Axes
		{
			get { return UnsafeNativeMethods.btMultibodyLink_getAxes(_native); }
		}
*/
		public Quaternion CachedRotParentToThis
		{
			get
			{
				Quaternion value;
				UnsafeNativeMethods.btMultibodyLink_getCachedRotParentToThis(Native, out value);
				return value;
			}
			set => UnsafeNativeMethods.btMultibodyLink_setCachedRotParentToThis(Native, ref value);
		}

		public Vector3 CachedRVector
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btMultibodyLink_getCachedRVector(Native, out value);
				return value;
			}
			set => UnsafeNativeMethods.btMultibodyLink_setCachedRVector(Native, ref value);
		}

		public Matrix CachedWorldTransform
		{
			get
			{
				Matrix value;
				UnsafeNativeMethods.btMultibodyLink_getCachedWorldTransform(Native, out value);
				return value;
			}
			set => UnsafeNativeMethods.btMultibodyLink_setCachedWorldTransform(Native, ref value);
		}

		public int CfgOffset
		{
			get => UnsafeNativeMethods.btMultibodyLink_getCfgOffset(Native);
			set => UnsafeNativeMethods.btMultibodyLink_setCfgOffset(Native, value);
		}

		public MultiBodyLinkCollider Collider
		{
			get => CollisionObject.GetManaged(UnsafeNativeMethods.btMultibodyLink_getCollider(Native)) as MultiBodyLinkCollider;
			set => UnsafeNativeMethods.btMultibodyLink_setCollider(Native, value.Native);
		}

		public int DofCount
		{
			get => UnsafeNativeMethods.btMultibodyLink_getDofCount(Native);
			set => UnsafeNativeMethods.btMultibodyLink_setDofCount(Native, value);
		}

		public int DofOffset
		{
			get => UnsafeNativeMethods.btMultibodyLink_getDofOffset(Native);
			set => UnsafeNativeMethods.btMultibodyLink_setDofOffset(Native, value);
		}

		public Vector3 DVector
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btMultibodyLink_getDVector(Native, out value);
				return value;
			}
			set => UnsafeNativeMethods.btMultibodyLink_setDVector(Native, ref value);
		}

		public Vector3 EVector
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btMultibodyLink_getEVector(Native, out value);
				return value;
			}
			set => UnsafeNativeMethods.btMultibodyLink_setEVector(Native, ref value);
		}

		public int Flags
		{
			get => UnsafeNativeMethods.btMultibodyLink_getFlags(Native);
			set => UnsafeNativeMethods.btMultibodyLink_setFlags(Native, value);
		}

		public Vector3 InertiaLocal
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btMultibodyLink_getInertiaLocal(Native, out value);
				return value;
			}
			set => UnsafeNativeMethods.btMultibodyLink_setInertiaLocal(Native, ref value);
		}

		public float JointDamping
		{
			get => UnsafeNativeMethods.btMultibodyLink_getJointDamping(Native);
			set => UnsafeNativeMethods.btMultibodyLink_setJointDamping(Native, value);
		}
		/*
		public MultiBodyJointFeedback JointFeedback
		{
			get { return _jointFeedback; }
			set
			{
				UnsafeNativeMethods.btMultibodyLink_setJointFeedback(_native, value._native);
				_jointFeedback = value;
			}
		}
		*/
		public float JointFriction
		{
			get => UnsafeNativeMethods.btMultibodyLink_getJointFriction(Native);
			set => UnsafeNativeMethods.btMultibodyLink_setJointFriction(Native, value);
		}
		/*
		public char JointName
		{
			get { return UnsafeNativeMethods.btMultibodyLink_getJointName(_native); }
			set { UnsafeNativeMethods.btMultibodyLink_setJointName(_native, value._native); }
		}

		public FloatArray JointPos
		{
			get { return UnsafeNativeMethods.btMultibodyLink_getJointPos(_native); }
		}

		public FloatArray JointTorque
		{
			get { return UnsafeNativeMethods.btMultibodyLink_getJointTorque(_native); }
		}
		*/
		public FeatherstoneJointType JointType
		{
			get => UnsafeNativeMethods.btMultibodyLink_getJointType(Native);
			set => UnsafeNativeMethods.btMultibodyLink_setJointType(Native, value);
		}
	   /*
		public char LinkName
		{
			get { return UnsafeNativeMethods.btMultibodyLink_getLinkName(_native); }
			set { UnsafeNativeMethods.btMultibodyLink_setLinkName(_native, value._native); }
		}
		*/
		public float Mass
		{
			get => UnsafeNativeMethods.btMultibodyLink_getMass(Native);
			set => UnsafeNativeMethods.btMultibodyLink_setMass(Native, value);
		}

		public int Parent
		{
			get => UnsafeNativeMethods.btMultibodyLink_getParent(Native);
			set => UnsafeNativeMethods.btMultibodyLink_setParent(Native, value);
		}

		public int PosVarCount
		{
			get => UnsafeNativeMethods.btMultibodyLink_getPosVarCount(Native);
			set => UnsafeNativeMethods.btMultibodyLink_setPosVarCount(Native, value);
		}

		public IntPtr UserPtr
		{
			get => UnsafeNativeMethods.btMultibodyLink_getUserPtr(Native);
			set => UnsafeNativeMethods.btMultibodyLink_setUserPtr(Native, value);
		}

		public Quaternion ZeroRotParentToThis
		{
			get
			{
				Quaternion value;
				UnsafeNativeMethods.btMultibodyLink_getZeroRotParentToThis(Native, out value);
				return value;
			}
			set => UnsafeNativeMethods.btMultibodyLink_setZeroRotParentToThis(Native, ref value);
		}
	}
}
