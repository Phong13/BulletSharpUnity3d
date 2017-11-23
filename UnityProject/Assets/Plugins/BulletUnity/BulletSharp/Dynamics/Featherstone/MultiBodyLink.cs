using System;
using BulletSharp.Math;
using static BulletSharp.UnsafeNativeMethods;

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
			btMultibodyLink_getAxisBottom(Native, dof, out value);
			return value;
		}

		public Vector3 GetAxisTop(int dof)
		{
			Vector3 value;
			btMultibodyLink_getAxisTop(Native, dof, out value);
			return value;
		}

		public void SetAxisBottom(int dof, float x, float y, float z)
		{
			btMultibodyLink_setAxisBottom(Native, dof, x, y, z);
		}

		public void SetAxisBottom(int dof, Vector3 axis)
		{
			btMultibodyLink_setAxisBottom2(Native, dof, ref axis);
		}

		public void SetAxisTop(int dof, float x, float y, float z)
		{
			btMultibodyLink_setAxisTop(Native, dof, x, y, z);
		}

		public void SetAxisTop(int dof, Vector3 axis)
		{
			btMultibodyLink_setAxisTop2(Native, dof, ref axis);
		}

		public void UpdateCacheMultiDof(float[] pq = null)
		{
			btMultibodyLink_updateCacheMultiDof(Native, pq);
		}
/*
		public SpatialMotionVector AbsFrameLocVelocity
		{
			get { return btMultibodyLink_getAbsFrameLocVelocity(_native); }
			set { btMultibodyLink_setAbsFrameLocVelocity(_native, value._native); }
		}

		public SpatialMotionVector AbsFrameTotVelocity
		{
			get { return btMultibodyLink_getAbsFrameTotVelocity(_native); }
			set { btMultibodyLink_setAbsFrameTotVelocity(_native, value._native); }
		}
*/
		public Vector3 AppliedConstraintForce
		{
			get
			{
				Vector3 value;
				btMultibodyLink_getAppliedConstraintForce(Native, out value);
				return value;
			}
			set => btMultibodyLink_setAppliedConstraintForce(Native, ref value);
		}

		public Vector3 AppliedConstraintTorque
		{
			get
			{
				Vector3 value;
				btMultibodyLink_getAppliedConstraintTorque(Native, out value);
				return value;
			}
			set => btMultibodyLink_setAppliedConstraintTorque(Native, ref value);
		}

		public Vector3 AppliedForce
		{
			get
			{
				Vector3 value;
				btMultibodyLink_getAppliedForce(Native, out value);
				return value;
			}
			set => btMultibodyLink_setAppliedForce(Native, ref value);
		}

		public Vector3 AppliedTorque
		{
			get
			{
				Vector3 value;
				btMultibodyLink_getAppliedTorque(Native, out value);
				return value;
			}
			set => btMultibodyLink_setAppliedTorque(Native, ref value);
		}
/*
		public SpatialMotionVector[] Axes
		{
			get { return btMultibodyLink_getAxes(_native); }
		}
*/
		public Quaternion CachedRotParentToThis
		{
			get
			{
				Quaternion value;
				btMultibodyLink_getCachedRotParentToThis(Native, out value);
				return value;
			}
			set => btMultibodyLink_setCachedRotParentToThis(Native, ref value);
		}

		public Vector3 CachedRVector
		{
			get
			{
				Vector3 value;
				btMultibodyLink_getCachedRVector(Native, out value);
				return value;
			}
			set => btMultibodyLink_setCachedRVector(Native, ref value);
		}

		public Matrix CachedWorldTransform
		{
			get
			{
				Matrix value;
				btMultibodyLink_getCachedWorldTransform(Native, out value);
				return value;
			}
			set => btMultibodyLink_setCachedWorldTransform(Native, ref value);
		}

		public int CfgOffset
		{
			get => btMultibodyLink_getCfgOffset(Native);
			set => btMultibodyLink_setCfgOffset(Native, value);
		}

		public MultiBodyLinkCollider Collider
		{
			get => CollisionObject.GetManaged(btMultibodyLink_getCollider(Native)) as MultiBodyLinkCollider;
			set => btMultibodyLink_setCollider(Native, value.Native);
		}

		public int DofCount
		{
			get => btMultibodyLink_getDofCount(Native);
			set => btMultibodyLink_setDofCount(Native, value);
		}

		public int DofOffset
		{
			get => btMultibodyLink_getDofOffset(Native);
			set => btMultibodyLink_setDofOffset(Native, value);
		}

		public Vector3 DVector
		{
			get
			{
				Vector3 value;
				btMultibodyLink_getDVector(Native, out value);
				return value;
			}
			set => btMultibodyLink_setDVector(Native, ref value);
		}

		public Vector3 EVector
		{
			get
			{
				Vector3 value;
				btMultibodyLink_getEVector(Native, out value);
				return value;
			}
			set => btMultibodyLink_setEVector(Native, ref value);
		}

		public int Flags
		{
			get => btMultibodyLink_getFlags(Native);
			set => btMultibodyLink_setFlags(Native, value);
		}

		public Vector3 InertiaLocal
		{
			get
			{
				Vector3 value;
				btMultibodyLink_getInertiaLocal(Native, out value);
				return value;
			}
			set => btMultibodyLink_setInertiaLocal(Native, ref value);
		}

		public float JointDamping
		{
			get => btMultibodyLink_getJointDamping(Native);
			set => btMultibodyLink_setJointDamping(Native, value);
		}
		/*
		public MultiBodyJointFeedback JointFeedback
		{
			get { return _jointFeedback; }
			set
			{
				btMultibodyLink_setJointFeedback(_native, value._native);
				_jointFeedback = value;
			}
		}
		*/
		public float JointFriction
		{
			get => btMultibodyLink_getJointFriction(Native);
			set => btMultibodyLink_setJointFriction(Native, value);
		}
		/*
		public char JointName
		{
			get { return btMultibodyLink_getJointName(_native); }
			set { btMultibodyLink_setJointName(_native, value._native); }
		}

		public FloatArray JointPos
		{
			get { return btMultibodyLink_getJointPos(_native); }
		}

		public FloatArray JointTorque
		{
			get { return btMultibodyLink_getJointTorque(_native); }
		}
		*/
		public FeatherstoneJointType JointType
		{
			get => btMultibodyLink_getJointType(Native);
			set => btMultibodyLink_setJointType(Native, value);
		}
	   /*
		public char LinkName
		{
			get { return btMultibodyLink_getLinkName(_native); }
			set { btMultibodyLink_setLinkName(_native, value._native); }
		}
		*/
		public float Mass
		{
			get => btMultibodyLink_getMass(Native);
			set => btMultibodyLink_setMass(Native, value);
		}

		public int Parent
		{
			get => btMultibodyLink_getParent(Native);
			set => btMultibodyLink_setParent(Native, value);
		}

		public int PosVarCount
		{
			get => btMultibodyLink_getPosVarCount(Native);
			set => btMultibodyLink_setPosVarCount(Native, value);
		}

		public IntPtr UserPtr
		{
			get => btMultibodyLink_getUserPtr(Native);
			set => btMultibodyLink_setUserPtr(Native, value);
		}

		public Quaternion ZeroRotParentToThis
		{
			get
			{
				Quaternion value;
				btMultibodyLink_getZeroRotParentToThis(Native, out value);
				return value;
			}
			set => btMultibodyLink_setZeroRotParentToThis(Native, ref value);
		}
	}
}
