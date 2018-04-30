using System;
using System.Runtime.InteropServices;
using BulletSharp.Math;
using BulletSharp;

namespace BulletSharp.InverseDynamics
{
    public enum JointType
    {
        /// no degree of freedom, moves with parent
        FIXED = 0,
        /// one rotational degree of freedom relative to parent
        REVOLUTE,
        /// one translational degree of freedom relative to parent
        PRISMATIC,
        /// six degrees of freedom relative to parent
        FLOATING
    };

    public class MultiBodyTree : IDisposable
    {
        internal IntPtr Native;
        private bool _preventDelete;
        private bool _isDisposed;

        public int NumBodies
        {
            get
            {
                return UnsafeNativeMethodsInverseDynamics.MultiBodyTree_numBodies(Native);
            }
        }

        public int NumDoFs
        {
            get
            {
                return UnsafeNativeMethodsInverseDynamics.MultiBodyTree_numDoFs(Native);
            }
        }

        internal MultiBodyTree(IntPtr tree, bool preventDelete = false)
        {
            Native = tree;
            _init(preventDelete);
        }

        public MultiBodyTree(bool preventDelete = false)
        {
            Native = UnsafeNativeMethodsInverseDynamics.MultiBodyTree_new();
            _init(preventDelete);
        }

        void _init(bool preventDelete) {
            if (preventDelete)
            {
                _preventDelete = true;
            }
            else
            {
                UnityEngine.Debug.LogError("TODO handle userPtr");
                GCHandle handle = GCHandle.Alloc(this, GCHandleType.Normal);
                // UnsafeNativeMethodsInverseDynamics.MultiBodyTree_setUserPtr(Native, GCHandle.ToIntPtr(handle));
            }
        }

		public int AddBody(int body_index, int parent_index, JointType joint_type, Vector3 parent_r_parent_body_ref, Matrix3x3FloatData body_T_parent_ref, Vector3 body_axis_of_motion, int mass, Vector3 body_r_body_com, Matrix3x3FloatData body_I_body, int user_int, IntPtr user_ptr)
		{
            return UnsafeNativeMethodsInverseDynamics.MultiBodyTree_addBody(Native, body_index, parent_index, joint_type, ref parent_r_parent_body_ref, ref body_T_parent_ref, ref body_axis_of_motion, mass, ref body_r_body_com, ref body_I_body, user_int, user_ptr);
		}

		public void AddUserForce(int body_index, Vector3 body_force)
		{
            UnsafeNativeMethodsInverseDynamics.MultiBodyTree_addUserForce(Native, body_index, ref body_force);
		}

		public void AddUserMoment(int body_index, Vector3 body_moment)
		{
            UnsafeNativeMethodsInverseDynamics.MultiBodyTree_addUserMoment(Native, body_index, ref body_moment);
		}

        /// <summary>
        /// Call this after adding bodies using AddBody.
        /// Don't add any bodies after calling this.
        /// It is not necessary to call this if using MultiBodyTreeCreator.CreateTree
        /// </summary>
        public void FinalizeInitialization()
        {
            UnsafeNativeMethodsInverseDynamics.MultiBodyTree_finalize(Native);
        }

        /// <summary>
        /// Calculate Inverse Dynamics. The returned values are only for the joints. Be careful when using this for floating base models. 
        /// MultiBodyTree includes the 6 DOF for the base (Unlike for a Multbody), so the input and output arrays need to be prepadded with 6 extra elements.
        /// </summary>
        /// <param name="floatingBase"> Does the multibody have a floating base. Pad with six extra elements if floating base.</param>
        /// <param name="q"> Positions of joints. Value of DOFs should be length of dofs excluding fixed base if is fixed base. Pad with six extra elements if floating base.</param>
        /// <param name="u"> Velocities of joints. Value of DOFs velocity. Pad with six extra elements if floating base.</param>
        /// <param name="dot_u"> Desired accelerations of joints. Pad with six extra elements if floating base. </param>
        /// <param name="joint_forces"> Output jorces necessary to achieve desired accelerations. Pad with six extra elements if floating base.</param>
        /// <returns></returns>
        public int CalculateInverseDynamics(bool floatingBase, float[] q, float[] u, float[] dot_u, float[] joint_forces)
        {
            int baseDofs;
            if (floatingBase)
            {
                baseDofs = 6;
            }
            else
            {
                baseDofs = 0;
            }

            int numDof = q.Length - baseDofs;
            return UnsafeNativeMethodsInverseDynamics.MultiBodyTree_calculateInverseDynamics(Native,numDof,baseDofs,q,u,dot_u,joint_forces);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                _isDisposed = true;

                if (!_preventDelete)
                {
                    //IntPtr userPtr = UnsafeNativeMethods.btCollisionShape_getUserPointer(Native);
                    //GCHandle.FromIntPtr(userPtr).Free();
                    UnsafeNativeMethodsInverseDynamics.MultiBodyTree_delete(Native);
                }
            }
        }

        ~MultiBodyTree()
        {
            Dispose(false);
        }

        /*
		public void B3_DECLARE_ALIGNED_ALLOCATOR()
		{
		}
        */

		public int CalculateJacobians(bool floatingBase, float[] q)
		{
            int baseDofs;
            if (floatingBase)
            {
                baseDofs = 6;
            }
            else
            {
                baseDofs = 0;
            }
            int numDof = q.Length;
            return UnsafeNativeMethodsInverseDynamics.MultiBodyTree_calculateJacobians(Native, numDof, baseDofs, q);
        }

		public int CalculateJacobians(bool floatingBase, float[] q, float[] u)
		{
            int baseDofs;
            if (floatingBase)
            {
                baseDofs = 6;
            }
            else
            {
                baseDofs = 0;
            }
            int numDof = q.Length;
            return UnsafeNativeMethodsInverseDynamics.MultiBodyTree_calculateJacobians(Native, numDof, baseDofs, q, u);
        }

		public int CalculateKinematics(bool floatingBase, float[] q, float[] u, float[] dot_u)
		{
            int baseDofs;
            if (floatingBase)
            {
                baseDofs = 6;
            }
            else
            {
                baseDofs = 0;
            }
            int numDof = q.Length;
            return UnsafeNativeMethodsInverseDynamics.MultiBodyTree_calculateKinematics(Native, numDof, baseDofs, q, u, dot_u);
        }

        /*
		public void CalculateMassMatrix(vecx^ q, bool update_kinematics, bool initialize_matrix, bool set_lower_triangular_matrix, matxx^ mass_matrix)
		{
		}
        
		public void CalculateMassMatrix(vecx^ q, matxx^ mass_matrix)
		{
		}
        */

		public int CalculatePositionAndVelocityKinematics(bool floatingBase, float[] q, float[] u)
		{
            int baseDofs;
            if (floatingBase)
            {
                baseDofs = 6;
            }
            else
            {
                baseDofs = 0;
            }
            int numDof = q.Length;
            return UnsafeNativeMethodsInverseDynamics.MultiBodyTree_calculatePositionAndVelocityKinematics(Native, numDof, baseDofs, q, u);
        }

		public int CalculatePositionKinematics(bool floatingBase, float[] q)
		{
            int baseDofs;
            if (floatingBase)
            {
                baseDofs = 6;
            }
            else
            {
                baseDofs = 0;
            }
            int numDof = q.Length;
            return UnsafeNativeMethodsInverseDynamics.MultiBodyTree_calculatePositionKinematics(Native, numDof, baseDofs, q);
        }

		public void ClearAllUserForcesAndMoments()
		{
            UnsafeNativeMethodsInverseDynamics.MultiBodyTree_clearAllUserForcesAndMoments(Native);
        }

		public bool GetAcceptInvalidMassProperties()
		{
            return UnsafeNativeMethodsInverseDynamics.MultiBodyTree_getAcceptInvalidMassProperties(Native);
        }

		public int GetBodyAngularAcceleration(int body_index, out Vector3 world_dot_omega)
		{
            return UnsafeNativeMethodsInverseDynamics.MultiBodyTree_getBodyAngularAcceleration(Native, body_index, out world_dot_omega);

        }

		public int GetBodyAngularVelocity(int body_index, out Vector3 world_omega)
		{
            return UnsafeNativeMethodsInverseDynamics.MultiBodyTree_getBodyAngularVelocity(Native, body_index, out world_omega);
        }

		public int GetBodyAxisOfMotion(int body_index, out Vector3 axis)
		{
            return UnsafeNativeMethodsInverseDynamics.MultiBodyTree_getBodyAxisOfMotion(Native, body_index, out axis);
        }

		public int GetBodyCoM(int body_index, out Vector3 world_com)
		{
            return UnsafeNativeMethodsInverseDynamics.MultiBodyTree_getBodyCoM(Native, body_index, out world_com);
        }

		public int GetBodyDotJacobianRotU(int body_index, out Vector3 world_dot_jac_rot_u)
		{
            return UnsafeNativeMethodsInverseDynamics.MultiBodyTree_getBodyDotJacobianRotU(Native, body_index, out world_dot_jac_rot_u);
        }

		public int GetBodyDotJacobianTransU(int body_index, out Vector3 world_dot_jac_trans_u)
		{
            return UnsafeNativeMethodsInverseDynamics.MultiBodyTree_getBodyDotJacobianTransU(Native, body_index, out world_dot_jac_trans_u);
        }

		public int GetBodyFirstMassMoment(int body_index, out Vector3 first_mass_moment)
		{
            return UnsafeNativeMethodsInverseDynamics.MultiBodyTree_getBodyFirstMassMoment(Native, body_index, out first_mass_moment);
        }

        /*
		public int GetBodyJacobianRot(int body_index, mat3x^ world_jac_rot)
		{
		}
		public int GetBodyJacobianTrans(int body_index, mat3x^ world_jac_trans)
		{
		}
        */

        public int GetBodyLinearAcceleration(int body_index, out Vector3 world_acceleration)
		{
            return UnsafeNativeMethodsInverseDynamics.MultiBodyTree_getBodyLinearAcceleration(Native, body_index, out world_acceleration);
        }

		public int GetBodyLinearVelocity(int body_index, out Vector3 world_velocity)
		{
            return UnsafeNativeMethodsInverseDynamics.MultiBodyTree_getBodyLinearVelocity(Native, body_index, out world_velocity);
        }

		public int GetBodyLinearVelocityCoM(int body_index, out Vector3 world_velocity)
		{
            return UnsafeNativeMethodsInverseDynamics.MultiBodyTree_getBodyLinearVelocityCoM(Native, body_index, out world_velocity);
        }

		public int GetBodyMass(int body_index, out float mass)
		{
            return UnsafeNativeMethodsInverseDynamics.MultiBodyTree_getBodyMass(Native, body_index, out mass);
        }

		public int GetBodyOrigin(int body_index, out Vector3 world_origin)
		{
            return UnsafeNativeMethodsInverseDynamics.MultiBodyTree_getBodyOrigin(Native, body_index, out world_origin);
        }

		public int GetBodySecondMassMoment(int body_index, out Matrix3x3FloatData second_mass_moment)
		{
            return UnsafeNativeMethodsInverseDynamics.MultiBodyTree_getBodySecondMassMoment(Native, body_index, out second_mass_moment);

        }

		public int GetBodyTParentRef(int body_index, out Matrix3x3FloatData T)
		{
            return UnsafeNativeMethodsInverseDynamics.MultiBodyTree_getBodyTParentRef(Native, body_index, out T);

        }

		public int GetBodyTransform(int body_index, out Matrix3x3FloatData world_T_body)
		{
            return UnsafeNativeMethodsInverseDynamics.MultiBodyTree_getBodyTransform(Native, body_index, out world_T_body);

        }

		public int GetDoFOffset(int body_index, out int q_offset)
		{
            return UnsafeNativeMethodsInverseDynamics.MultiBodyTree_getDoFOffset(Native, body_index, out q_offset);
        }

		public int GetJointType(int body_index, out JointType joint_type)
		{
            int jointTypeInt;
            int ret = UnsafeNativeMethodsInverseDynamics.MultiBodyTree_getJointType(Native, body_index, out jointTypeInt);
            if (ret != -1)
            {
                joint_type = (JointType)jointTypeInt;
            } else
            {
                joint_type = JointType.FIXED;
            }
            return ret;
        }

		public int GetParentIndex(int body_index, out int parent_index)
		{
            return UnsafeNativeMethodsInverseDynamics.MultiBodyTree_getParentIndex(Native, body_index, out parent_index);
        }

		public int GetParentRParentBodyRef(int body_index, out Vector3 r)
		{
            return UnsafeNativeMethodsInverseDynamics.MultiBodyTree_getParentRParentBodyRef(Native, body_index, out r);
        }

		public int GetUserInt(int body_index, out int user_int)
		{
            return UnsafeNativeMethodsInverseDynamics.MultiBodyTree_getUserInt(Native, body_index, out user_int);
        }

		//public IntPtr GetUserPtr(int body_index)
		//{
        //    return UnsafeNativeMethodsInverseDynamics.MultiBodyTree_getUserPtr(Native, body_index);
        //}
        
		public void SetAcceptInvalidMassParameters(bool flag)
		{
            UnsafeNativeMethodsInverseDynamics.MultiBodyTree_setAcceptInvalidMassParameters(Native, flag);
        }

		public int SetBodyFirstMassMoment(int body_index, ref Vector3 first_mass_moment)
		{
            return UnsafeNativeMethodsInverseDynamics.MultiBodyTree_setBodyFirstMassMoment(Native, body_index, ref first_mass_moment);
		}

		public int SetBodyMass(int body_index, float mass)
		{
            return UnsafeNativeMethodsInverseDynamics.MultiBodyTree_setBodyMass(Native, body_index, mass);
		}

		public int SetBodySecondMassMoment(int body_index, ref Matrix3x3FloatData second_mass_moment)
		{
            return UnsafeNativeMethodsInverseDynamics.MultiBodyTree_setBodySecondMassMoment(Native, body_index, ref second_mass_moment);
		}

		public void SetGravityInWorldFrame(Vector3 gravity)
		{
            UnsafeNativeMethodsInverseDynamics.MultiBodyTree_setGravityInWorldFrame(Native, ref gravity);
		}

		public int SetUserInt(int body_index, int user_int)
		{
            return UnsafeNativeMethodsInverseDynamics.MultiBodyTree_setUserInt(Native, body_index, user_int);
		}

		//public int SetUserPtr(int body_index, IntPtr user_ptr)
		//{
        //    return UnsafeNativeMethodsInverseDynamics.MultiBodyTree_setUserPtr(Native, body_index, user_ptr);
		//}
    }
}
