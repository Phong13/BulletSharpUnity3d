using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Reflection;


public static class BSExtensionMethods {
    public static IntPtr Add(this IntPtr ptr, int amt) {
        return new IntPtr(ptr.ToInt64() + amt);
    }

    public static void Dispose(this BinaryReader reader) {
        MethodInfo dynMethod = reader.GetType().GetMethod("Dispose",
                        BindingFlags.NonPublic | BindingFlags.Instance,
                        null,
                        new Type[] {typeof(bool)},
                        null);
        dynMethod.Invoke(reader, new System.Object[] { true });
    } 

    public static BulletSharp.Math.Vector3 ToBullet(this UnityEngine.Vector3 v) {
        return new BulletSharp.Math.Vector3(v.x,v.y,v.z);
    }

    public static UnityEngine.Vector3 ToUnity(this BulletSharp.Math.Vector3 v) {
        return new UnityEngine.Vector3(v.X, v.Y, v.Z);
    }

    public static UnityEngine.Matrix4x4 ToUnity(this BulletSharp.Math.Matrix bm) {
        Matrix4x4 um = new Matrix4x4();
        um[0, 0] = bm[0, 0];
        um[0, 1] = bm[1, 0];
        um[0, 2] = bm[2, 0];
        um[0, 3] = bm[3, 0];

        um[1, 0] = bm[0,1];
        um[1, 1] = bm[1,1];
        um[1, 2] = bm[2,1];
        um[1, 3] = bm[3,1];

        um[2, 0] = bm[0,2];
        um[2, 1] = bm[1,2];
        um[2, 2] = bm[2,2];
        um[2, 3] = bm[3,2];

        um[3, 0] = bm[ 0,3];
        um[3, 1] = bm[ 1,3];
        um[3, 2] = bm[ 2,3];
        um[3, 3] = bm[ 3,3];
        return um;
    }

    public static BulletSharp.Math.Matrix ToBullet(this UnityEngine.Matrix4x4 um) {
        BulletSharp.Math.Matrix bm = new BulletSharp.Math.Matrix();
        um.ToBullet(ref bm);
        return bm;
    }

    public static void ToBullet(this UnityEngine.Matrix4x4 um, ref BulletSharp.Math.Matrix bm) {
        bm[0, 0] = um[ 0,0];
        bm[0, 1] = um[ 1,0];
        bm[0, 2] = um[ 2,0];
        bm[0, 3] = um[ 3,0];

        bm[1, 0] = um[ 0,1];
        bm[1, 1] = um[ 1,1];
        bm[1, 2] = um[ 2,1];
        bm[1, 3] = um[ 3,1];

        bm[2, 0] = um[ 0,2];
        bm[2, 1] = um[ 1,2];
        bm[2, 2] = um[ 2,2];
        bm[2, 3] = um[ 3,2];

        bm[3, 0] = um[ 0,3];
        bm[3, 1] = um[ 1,3];
        bm[3, 2] = um[ 2,3];
        bm[3, 3] = um[ 3,3];
    }

    /// <summary>
    /// Extract translation from transform matrix.
    /// </summary>
    /// <param name="matrix">Transform matrix. This parameter is passed by reference
    /// to improve performance; no changes will be made to it.</param>
    /// <returns>
    /// Translation offset.
    /// </returns>
    public static Vector3 ExtractTranslationFromMatrix(ref Matrix4x4 matrix) {
        Vector3 translate;
        translate.x = matrix.m03;
        translate.y = matrix.m13;
        translate.z = matrix.m23;
        return translate;
    }

    /// <summary>
    /// Extract rotation quaternion from transform matrix.
    /// </summary>
    /// <param name="matrix">Transform matrix. This parameter is passed by reference
    /// to improve performance; no changes will be made to it.</param>
    /// <returns>
    /// Quaternion representation of rotation transform.
    /// </returns>
    public static Quaternion ExtractRotationFromMatrix(ref Matrix4x4 matrix) {
        Vector3 forward;
        forward.x = matrix.m02;
        forward.y = matrix.m12;
        forward.z = matrix.m22;

        Vector3 upwards;
        upwards.x = matrix.m01;
        upwards.y = matrix.m11;
        upwards.z = matrix.m21;

        return Quaternion.LookRotation(forward, upwards);
    }

    /// <summary>
    /// Extract scale from transform matrix.
    /// </summary>
    /// <param name="matrix">Transform matrix. This parameter is passed by reference
    /// to improve performance; no changes will be made to it.</param>
    /// <returns>
    /// Scale vector.
    /// </returns>
    public static Vector3 ExtractScaleFromMatrix(ref Matrix4x4 matrix) {
        Vector3 scale;
        scale.x = new Vector4(matrix.m00, matrix.m10, matrix.m20, matrix.m30).magnitude;
        scale.y = new Vector4(matrix.m01, matrix.m11, matrix.m21, matrix.m31).magnitude;
        scale.z = new Vector4(matrix.m02, matrix.m12, matrix.m22, matrix.m32).magnitude;
        return scale;
    }

    /// <summary>
    /// Extract position, rotation and scale from TRS matrix.
    /// </summary>
    /// <param name="matrix">Transform matrix. This parameter is passed by reference
    /// to improve performance; no changes will be made to it.</param>
    /// <param name="localPosition">Output position.</param>
    /// <param name="localRotation">Output rotation.</param>
    /// <param name="localScale">Output scale.</param>
    public static void DecomposeMatrix(ref Matrix4x4 matrix, out Vector3 localPosition, out Quaternion localRotation, out Vector3 localScale) {
        localPosition = ExtractTranslationFromMatrix(ref matrix);
        localRotation = ExtractRotationFromMatrix(ref matrix);
        localScale = ExtractScaleFromMatrix(ref matrix);
    }

    /// <summary>
    /// Set transform component from TRS matrix.
    /// </summary>
    /// <param name="transform">Transform component.</param>
    /// <param name="matrix">Transform matrix. This parameter is passed by reference
    /// to improve performance; no changes will be made to it.</param>
    public static void SetTransformFromMatrix(Transform transform, ref Matrix4x4 matrix) {
        transform.localPosition = ExtractTranslationFromMatrix(ref matrix);
        transform.localRotation = ExtractRotationFromMatrix(ref matrix);
        transform.localScale = ExtractScaleFromMatrix(ref matrix);
    }


    // EXTRAS!

    /// <summary>
    /// Identity quaternion.
    /// </summary>
    /// <remarks>
    /// <para>It is faster to access this variation than <c>Quaternion.identity</c>.</para>
    /// </remarks>
    public static readonly Quaternion IdentityQuaternion = Quaternion.identity;
    /// <summary>
    /// Identity matrix.
    /// </summary>
    /// <remarks>
    /// <para>It is faster to access this variation than <c>Matrix4x4.identity</c>.</para>
    /// </remarks>
    public static readonly Matrix4x4 IdentityMatrix = Matrix4x4.identity;

    /// <summary>
    /// Get translation matrix.
    /// </summary>
    /// <param name="offset">Translation offset.</param>
    /// <returns>
    /// The translation transform matrix.
    /// </returns>
    public static Matrix4x4 TranslationMatrix(Vector3 offset) {
        Matrix4x4 matrix = IdentityMatrix;
        matrix.m03 = offset.x;
        matrix.m13 = offset.y;
        matrix.m23 = offset.z;
        return matrix;
    }
}

