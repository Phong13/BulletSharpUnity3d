using System;
using System.Runtime.InteropServices;
using BulletSharp.Math;
using BulletSharp;

namespace InverseDynamicsBullet3
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
                //UnsafeNativeMethodsInverseDynamics.btCollisionShape_setUserPointer(native, GCHandle.ToIntPtr(handle));
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
        /// Calculate Inverse Dynamics
        /// </summary>
        /// <param name="floatingBase"> Does the multibody have a floating base. </param>
        /// <param name="q"> Value of DOFs should be length of dofs excluding fixed base if is fixed base</param>
        /// <param name="u"> Value of DOFs velocity </param>
        /// <param name="dot_u"> Value of DOFs Acceleration </param>
        /// <param name="joint_forces"></param>
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
            int numDof = q.Length;
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
                    UnityEngine.Debug.LogError("TODO userPtr");
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
		public void CalculateInverseDynamics(vecx^ q, vecx^ u, vecx^ dot_u, vecx^ joint_forces)
		{
		}
		public void CalculateJacobians(vecx^ q, vecx^ u)
		{
		}
		public void CalculateJacobians(vecx^ q)
		{
		}
		public void CalculateKinematics(vecx^ q, vecx^ u, vecx^ dot_u)
		{
		}
		public void CalculateMassMatrix(vecx^ q, bool update_kinematics, bool initialize_matrix, bool set_lower_triangular_matrix, matxx^ mass_matrix)
		{
		}
		public void CalculateMassMatrix(vecx^ q, matxx^ mass_matrix)
		{
		}
		public void CalculatePositionAndVelocityKinematics(vecx^ q, vecx^ u)
		{
		}
		public void CalculatePositionKinematics(vecx^ q)
		{
		}
		public void ClearAllUserForcesAndMoments()
		{
		}
		public void Finalize()
		{
		}
		public void GetAcceptInvalidMassProperties()
		{
		}
		public void GetBodyAngularAcceleration(int body_index, Vector3 world_dot_omega)
		{
		}
		public void GetBodyAngularVelocity(int body_index, Vector3 world_omega)
		{
		}
		public void GetBodyAxisOfMotion(int body_index, Vector3 axis)
		{
		}
		public void GetBodyCoM(int body_index, Vector3 world_com)
		{
		}
		public void GetBodyDotJacobianRotU(int body_index, Vector3 world_dot_jac_rot_u)
		{
		}
		public void GetBodyDotJacobianTransU(int body_index, Vector3 world_dot_jac_trans_u)
		{
		}
		public void GetBodyFirstMassMoment(int body_index, Vector3 first_mass_moment)
		{
		}
		public void GetBodyJacobianRot(int body_index, mat3x^ world_jac_rot)
		{
		}
		public void GetBodyJacobianTrans(int body_index, mat3x^ world_jac_trans)
		{
		}
		public void GetBodyLinearAcceleration(int body_index, Vector3 world_acceleration)
		{
		}
		public void GetBodyLinearVelocity(int body_index, Vector3 world_velocity)
		{
		}
		public void GetBodyLinearVelocityCoM(int body_index, Vector3 world_velocity)
		{
		}
		public void GetBodyMass(int body_index, int^ mass)
		{
		}
		public void GetBodyOrigin(int body_index, Vector3 world_origin)
		{
		}
		public void GetBodySecondMassMoment(int body_index, Matrix3x3 second_mass_moment)
		{
		}
		public void GetBodyTParentRef(int body_index, Matrix3x3 T)
		{
		}
		public void GetBodyTransform(int body_index, Matrix3x3 world_T_body)
		{
		}
		public void GetDoFOffset(int body_index, int^ q_offset)
		{
		}
		public void GetJointType(int body_index, JointType^ joint_type)
		{
		}
		public void GetJointTypeStr(int body_index, char^ joint_type)
		{
		}
		public void GetParentIndex(int body_index, int^ parent_index)
		{
		}
		public void GetParentRParentBodyRef(int body_index, Vector3 r)
		{
		}
		public void GetUserInt(int body_index, int^ user_int)
		{
		}
		public void GetUserPtr(int body_index, void^ user_ptr)
		{
		}
        */
        public int NumBodies()
		{

            return UnsafeNativeMethodsInverseDynamics.MultiBodyTree_numBodies(Native);
		}
		public int NumDoFs()
		{
            return UnsafeNativeMethodsInverseDynamics.MultiBodyTree_numDoFs(Native);
		}
        /*
		public void PrintTree()
		{
		}
		public void PrintTreeData()
		{
		}
		public void SetAcceptInvalidMassParameters(bool flag)
		{
		}
		public void SetBodyFirstMassMoment(int body_index, Vector3 first_mass_moment)
		{
		}
		public void SetBodyMass(int body_index, int mass)
		{
		}
		public void SetBodySecondMassMoment(int body_index, Matrix3x3 second_mass_moment)
		{
		}
		public void SetGravityInWorldFrame(Vector3 gravity)
		{
		}
		public void SetUserInt(int body_index, int user_int)
		{
		}
		public void SetUserPtr(int body_index, void^ user_ptr)
		{
		}
        */
    }
}
