using BulletSharp.Math;
using System;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.InverseDynamics;

namespace BulletSharp
{
    [SuppressUnmanagedCodeSecurity]
    public static class UnsafeNativeMethodsInverseDynamics
    {
        // MultiBodyTreeCreator
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern IntPtr MultiBodyTreeCreator_new();
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern int MultiBodyTreeCreator_createFromBtMultiBody(IntPtr multiBodyTreecreator, IntPtr multibody);
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern IntPtr MultiBodyTreeCreator_CreateMultiBodyTree(IntPtr treeCreator); 
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern void MultiBodyTreeCreator_delete(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern int MultiBodyTreeCreator_getNumBodies(IntPtr obj);

        // MultiBodyTree
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern IntPtr MultiBodyTree_new();
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern int MultiBodyTree_addBody(IntPtr obj, int body_index, int parent_index, JointType joint_type, ref Vector3 parent_r_parent_body_ref, [In] ref Matrix3x3FloatData body_T_parent_ref, [In] ref Vector3 body_axis_of_motion, int mass, [In] ref Vector3 body_r_body_com, [In] ref Matrix3x3FloatData body_I_body, int user_int, IntPtr user_ptr);
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern void MultiBodyTree_addUserForce(IntPtr obj, int body_index, [In] ref Vector3 body_force);
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern void MultiBodyTree_addUserMoment(IntPtr obj, int body_index, [In] ref Vector3 body_moment);
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern int MultiBodyTree_calculateInverseDynamics(IntPtr obj, int num_dof, int baseDofs, float[] q, float[] u, float[] dot_u, [In, Out] float[] joint_forces);
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern int MultiBodyTree_numBodies(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern int MultiBodyTree_numDoFs(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern void MultiBodyTree_finalize(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern void MultiBodyTree_delete(IntPtr obj);
        
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern int MultiBodyTree_calculateJacobians(IntPtr obj, int num_dof, int baseDofs, float[] q, float[] u);
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern int MultiBodyTree_calculateJacobians(IntPtr obj, int num_dof, int baseDofs, float[] q);
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern int MultiBodyTree_calculateKinematics(IntPtr obj, int num_dof, int baseDofs, float[] q, float[] u, float[] dot_u);
        /*
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern void MultiBodyTree_calculateMassMatrix(IntPtr obj, vecx^ q, bool update_kinematics, bool initialize_matrix, bool set_lower_triangular_matrix, matxx^ mass_matrix);
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern void MultiBodyTree_calculateMassMatrix(IntPtr obj, vecx^ q, matxx^ mass_matrix);
        */
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern int MultiBodyTree_calculatePositionAndVelocityKinematics(IntPtr obj, int num_dof, int baseDofs, float[] q, float[] u);
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern int MultiBodyTree_calculatePositionKinematics(IntPtr obj, int num_dof, int baseDofs, float[] q);
        

        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern void MultiBodyTree_clearAllUserForcesAndMoments(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern bool MultiBodyTree_getAcceptInvalidMassProperties(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern int MultiBodyTree_getBodyAngularAcceleration(IntPtr obj, int body_index, out Vector3 world_dot_omega);
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern int MultiBodyTree_getBodyAngularVelocity(IntPtr obj, int body_index, out Vector3 world_omega);
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern int MultiBodyTree_getBodyAxisOfMotion(IntPtr obj, int body_index, out Vector3 axis);
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern int MultiBodyTree_getBodyCoM(IntPtr obj, int body_index, out Vector3 world_com);
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern int MultiBodyTree_getBodyDotJacobianRotU(IntPtr obj, int body_index, out Vector3 world_dot_jac_rot_u);
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern int MultiBodyTree_getBodyDotJacobianTransU(IntPtr obj, int body_index, out Vector3 world_dot_jac_trans_u);
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern int MultiBodyTree_getBodyFirstMassMoment(IntPtr obj, int body_index, out Vector3 first_mass_moment);
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        //public static extern int MultiBodyTree_getBodyJacobianRot(IntPtr obj, int body_index, mat3x^ world_jac_rot);
        //[DllImport(Native.Dll, CallingConvention = Native.Conv)]
        //public static extern int MultiBodyTree_getBodyJacobianTrans(IntPtr obj, int body_index, mat3x^ world_jac_trans);
        //[DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern int MultiBodyTree_getBodyLinearAcceleration(IntPtr obj, int body_index, out Vector3 world_acceleration);
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern int MultiBodyTree_getBodyLinearVelocity(IntPtr obj, int body_index, out Vector3 world_velocity);
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern int MultiBodyTree_getBodyLinearVelocityCoM(IntPtr obj, int body_index, out Vector3 world_velocity);
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern int MultiBodyTree_getBodyMass(IntPtr obj, int body_index, out float mass);
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern int MultiBodyTree_getBodyOrigin(IntPtr obj, int body_index, out Vector3 world_origin);

        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern int MultiBodyTree_getBodySecondMassMoment(IntPtr obj, int body_index, out Matrix3x3FloatData second_mass_moment);
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern int MultiBodyTree_getBodyTParentRef(IntPtr obj, int body_index, out Matrix3x3FloatData T);
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern int MultiBodyTree_getBodyTransform(IntPtr obj, int body_index, out Matrix3x3FloatData world_T_body);
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern int MultiBodyTree_getDoFOffset(IntPtr obj, int body_index, out int q_offset);
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern int MultiBodyTree_getJointType(IntPtr obj, int body_index, out int joint_type);
        //[DllImport(Native.Dll, CallingConvention = Native.Conv)]
        //public static extern void MultiBodyTree_getJointTypeStr(IntPtr obj, int body_index, char^ joint_type);
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern int MultiBodyTree_getParentIndex(IntPtr obj, int body_index, out int parent_index);
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern int MultiBodyTree_getParentRParentBodyRef(IntPtr obj, int body_index, out Vector3 r);
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern int MultiBodyTree_getUserInt(IntPtr obj, int body_index, out int userInt);

        // [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        // public static extern IntPtr MultiBodyTree_getUserPtr(IntPtr obj, int body_index);

        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern void MultiBodyTree_setAcceptInvalidMassParameters(IntPtr obj, bool flag);
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern int MultiBodyTree_setBodyFirstMassMoment(IntPtr obj, int body_index, ref Vector3 first_mass_moment);
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern int MultiBodyTree_setBodyMass(IntPtr obj, int body_index, float mass);
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern int MultiBodyTree_setBodySecondMassMoment(IntPtr obj, int body_index, ref Matrix3x3FloatData second_mass_moment);
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern void MultiBodyTree_setGravityInWorldFrame(IntPtr obj, ref Vector3 gravity);
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern int MultiBodyTree_setUserInt(IntPtr obj, int body_index, int user_int);

        //[DllImport(Native.Dll, CallingConvention = Native.Conv)]
        //public static extern int MultiBodyTree_setUserPtr(IntPtr obj, int body_index, IntPtr user_ptr);
        
    }
}
