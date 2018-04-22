using BulletSharp.Math;
using System;
using System.Runtime.InteropServices;
using System.Security;
using InverseDynamicsBullet3;

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
        public static extern int MultiBodyTree_calculateInverseDynamics(IntPtr obj, int qLength, int uLenght, float[] q, float[] u, float[] dot_u, [In, Out] float[] joint_forces);
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern int MultiBodyTree_numBodies(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern int MultiBodyTree_numDoFs(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern void MultiBodyTree_finalize(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern void MultiBodyTree_delete(IntPtr obj);
        /*
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern void MultiBodyTree_calculateJacobians(IntPtr obj, vecx^ q, vecx^ u);
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern void MultiBodyTree_calculateJacobians(IntPtr obj, vecx^ q);
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern void MultiBodyTree_calculateKinematics(IntPtr obj, vecx^ q, vecx^ u, vecx^ dot_u);
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern void MultiBodyTree_calculateMassMatrix(IntPtr obj, vecx^ q, bool update_kinematics, bool initialize_matrix, bool set_lower_triangular_matrix, matxx^ mass_matrix);
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern void MultiBodyTree_calculateMassMatrix(IntPtr obj, vecx^ q, matxx^ mass_matrix);
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern void MultiBodyTree_calculatePositionAndVelocityKinematics(IntPtr obj, vecx^ q, vecx^ u);
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern void MultiBodyTree_calculatePositionKinematics(IntPtr obj, vecx^ q);
        */

        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern void MultiBodyTree_clearAllUserForcesAndMoments(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern void MultiBodyTree_getAcceptInvalidMassProperties(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern void MultiBodyTree_getBodyAngularAcceleration(IntPtr obj, int body_index, ref Vector3 world_dot_omega);
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern void MultiBodyTree_getBodyAngularVelocity(IntPtr obj, int body_index, ref Vector3 world_omega);
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern void MultiBodyTree_getBodyAxisOfMotion(IntPtr obj, int body_index, ref Vector3 axis);
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern void MultiBodyTree_getBodyCoM(IntPtr obj, int body_index, ref Vector3 world_com);
        /*
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern void MultiBodyTree_getBodyDotJacobianRotU(IntPtr obj, int body_index, vec3^ world_dot_jac_rot_u);
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern void MultiBodyTree_getBodyDotJacobianTransU(IntPtr obj, int body_index, vec3^ world_dot_jac_trans_u);
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern void MultiBodyTree_getBodyFirstMassMoment(IntPtr obj, int body_index, vec3^ first_mass_moment);
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern void MultiBodyTree_getBodyJacobianRot(IntPtr obj, int body_index, mat3x^ world_jac_rot);
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern void MultiBodyTree_getBodyJacobianTrans(IntPtr obj, int body_index, mat3x^ world_jac_trans);
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern void MultiBodyTree_getBodyLinearAcceleration(IntPtr obj, int body_index, vec3^ world_acceleration);
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern void MultiBodyTree_getBodyLinearVelocity(IntPtr obj, int body_index, vec3^ world_velocity);
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern void MultiBodyTree_getBodyLinearVelocityCoM(IntPtr obj, int body_index, vec3^ world_velocity);
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern void MultiBodyTree_getBodyMass(IntPtr obj, int body_index, int^ mass);
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern void MultiBodyTree_getBodyOrigin(IntPtr obj, int body_index, vec3^ world_origin);
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern void MultiBodyTree_getBodySecondMassMoment(IntPtr obj, int body_index, mat33^ second_mass_moment);
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern void MultiBodyTree_getBodyTParentRef(IntPtr obj, int body_index, mat33^ T);
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern void MultiBodyTree_getBodyTransform(IntPtr obj, int body_index, mat33^ world_T_body);
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern void MultiBodyTree_getDoFOffset(IntPtr obj, int body_index, int^ q_offset);
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern void MultiBodyTree_getJointType(IntPtr obj, int body_index, JointType^ joint_type);
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern void MultiBodyTree_getJointTypeStr(IntPtr obj, int body_index, char^ joint_type);
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern void MultiBodyTree_getParentIndex(IntPtr obj, int body_index, int^ parent_index);
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern void MultiBodyTree_getParentRParentBodyRef(IntPtr obj, int body_index, vec3^ r);
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern void MultiBodyTree_getUserInt(IntPtr obj, int body_index, int^ user_int);
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern void MultiBodyTree_getUserPtr(IntPtr obj, int body_index, void^ user_ptr);
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern void MultiBodyTree_printTree(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern void MultiBodyTree_printTreeData(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern void MultiBodyTree_setAcceptInvalidMassParameters(IntPtr obj, bool flag);
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern void MultiBodyTree_setBodyFirstMassMoment(IntPtr obj, int body_index, vec3^ first_mass_moment);
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern void MultiBodyTree_setBodyMass(IntPtr obj, int body_index, int mass);
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern void MultiBodyTree_setBodySecondMassMoment(IntPtr obj, int body_index, mat33^ second_mass_moment);
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern void MultiBodyTree_setGravityInWorldFrame(IntPtr obj, vec3^ gravity);
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern void MultiBodyTree_setUserInt(IntPtr obj, int body_index, int user_int);
        [DllImport(Native.Dll, CallingConvention = Native.Conv)]
        public static extern void MultiBodyTree_setUserPtr(IntPtr obj, int body_index, void^ user_ptr);
        */
    }
}
