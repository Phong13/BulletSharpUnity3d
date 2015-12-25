using System;
using System.Collections;
using BulletSharp;
using BulletSharp.Math;

namespace BulletUnity
{
    public class DebugDrawUnity : DebugDraw
    {
        public override void DrawLine(ref Vector3 from, ref Vector3 to, ref Vector3 fromColor) {
            UnityEngine.Gizmos.color = new UnityEngine.Color(fromColor.X, fromColor.Y, fromColor.Z);
            UnityEngine.Gizmos.DrawLine(from.ToUnity(), to.ToUnity());
        }
        public override void DrawLine(ref Vector3 from, ref Vector3 to, ref Vector3 fromColor, ref Vector3 toColor) {
            UnityEngine.Gizmos.color = new UnityEngine.Color(fromColor.X, fromColor.Y, fromColor.Z);
            UnityEngine.Gizmos.DrawLine(from.ToUnity(), to.ToUnity());
        }
        public override void DrawBox(ref Vector3 bbMin, ref Vector3 bbMax, ref Vector3 color) {
            UnityEngine.Bounds b = new UnityEngine.Bounds(bbMin.ToUnity(), UnityEngine.Vector3.zero);
            b.Encapsulate(bbMax.ToUnity());
            UnityEngine.Gizmos.color = new UnityEngine.Color(color.X, color.Y, color.Z);
            UnityEngine.Gizmos.DrawWireCube(b.center,b.size);
        }

        public override void DrawBox(ref Vector3 bbMin, ref Vector3 bbMax, ref Matrix trans, ref Vector3 color) {
            UnityEngine.Vector3 pos = BSExtensionMethods2.ExtractTranslationFromMatrix(ref trans);
            UnityEngine.Quaternion rot = BSExtensionMethods2.ExtractRotationFromMatrix(ref trans);
            UnityEngine.Vector3 scale = BSExtensionMethods2.ExtractScaleFromMatrix(ref trans);
            UnityEngine.Vector3 size = (bbMax - bbMin).ToUnity();
            UnityEngine.Color c = new UnityEngine.Color(color.X, color.Y, color.Z);
            BUtility.DebugDrawBox(pos, rot, scale, size,c);
        }
        public override void DrawSphere(ref Vector3 p, float radius, ref Vector3 color) {
            UnityEngine.Color c = new UnityEngine.Color(color.X, color.Y, color.Z);
            BUtility.DebugDrawSphere(p.ToUnity(),UnityEngine.Quaternion.identity,UnityEngine.Vector3.one, UnityEngine.Vector3.one * radius, c);
        }

        public override void DrawSphere(float radius, ref Matrix trans, ref Vector3 color) {
            UnityEngine.Vector3 pos = BSExtensionMethods2.ExtractTranslationFromMatrix(ref trans);
            UnityEngine.Quaternion rot = BSExtensionMethods2.ExtractRotationFromMatrix(ref trans);
            UnityEngine.Vector3 scale = BSExtensionMethods2.ExtractScaleFromMatrix(ref trans);
            UnityEngine.Color c = new UnityEngine.Color(color.X, color.Y, color.Z);
            BUtility.DebugDrawSphere(pos, rot, scale, UnityEngine.Vector3.one * radius, c);
        }

        public override void DrawTriangle(ref Vector3 v0, ref Vector3 v1, ref Vector3 v2, ref Vector3 n0, ref Vector3 n1, ref Vector3 n2, ref Vector3 color, float alpha) {
            //todo normals and alpha
            UnityEngine.Gizmos.color = new UnityEngine.Color(color.X, color.Y, color.Z);
            UnityEngine.Gizmos.DrawLine(v0.ToUnity(), v1.ToUnity());
            UnityEngine.Gizmos.DrawLine(v1.ToUnity(), v2.ToUnity());
            UnityEngine.Gizmos.DrawLine(v2.ToUnity(), v0.ToUnity());
        }
        public override void DrawTriangle(ref Vector3 v0, ref Vector3 v1, ref Vector3 v2, ref Vector3 color, float alpha) {
            UnityEngine.Gizmos.color = new UnityEngine.Color(color.X, color.Y, color.Z);
            UnityEngine.Gizmos.DrawLine(v0.ToUnity(), v1.ToUnity());
            UnityEngine.Gizmos.DrawLine(v1.ToUnity(), v2.ToUnity());
            UnityEngine.Gizmos.DrawLine(v2.ToUnity(), v0.ToUnity());
        }
        public override void DrawContactPoint(ref Vector3 pointOnB, ref Vector3 normalOnB, float distance, int lifeTime, ref Vector3 color) {
            UnityEngine.Debug.LogError("Not implemented");
        }
        public override void ReportErrorWarning(String warningString) {
            UnityEngine.Debug.LogError(warningString);
        }
        public override void Draw3dText(ref Vector3 location, String textString) {
            UnityEngine.Debug.LogError("Not implemented");
        }
        
        DebugDrawModes _debugMode;
        public override DebugDrawModes DebugMode {
            get { return _debugMode; }
            set { _debugMode = value; }
        }

        public override void DrawAabb(ref Vector3 from, ref Vector3 to, ref Vector3 color) {
            DrawBox(ref from, ref to, ref color);
        }
        public override void DrawTransform(ref Matrix transform, float orthoLen) {
            //todo
            UnityEngine.Debug.LogError("Not implemented");
        }
        public override void DrawArc(ref Vector3 center, ref Vector3 normal, ref Vector3 axis, float radiusA, float radiusB, float minAngle, float maxAngle,
            ref Vector3 color, bool drawSect)
        {
            UnityEngine.Debug.LogError("Not implemented");
        }
        public override void DrawArc(ref Vector3 center, ref Vector3 normal, ref Vector3 axis, float radiusA, float radiusB, float minAngle, float maxAngle,
            ref Vector3 color, bool drawSect, float stepDegrees)
        {
            UnityEngine.Color col = new UnityEngine.Color(color.X, color.Y, color.Z);
            BUtility.DebugDrawArc(center.ToUnity(), normal.ToUnity(), axis.ToUnity(), radiusA, radiusB, minAngle, maxAngle, col, drawSect, stepDegrees);
        }
        public override void DrawSpherePatch(ref Vector3 center, ref Vector3 up, ref Vector3 axis, float radius,
            float minTh, float maxTh, float minPs, float maxPs, ref Vector3 color)
        {
            UnityEngine.Debug.LogError("Not implemented");
            
        }
        public override void DrawSpherePatch(ref Vector3 center, ref Vector3 up, ref Vector3 axis, float radius,
            float minTh, float maxTh, float minPs, float maxPs, ref Vector3 color, float stepDegrees)
        {
            UnityEngine.Debug.LogError("Not implemented");
        }
        public override void DrawCapsule(float radius, float halfHeight, int upAxis, ref Matrix trans, ref Vector3 color) {
            UnityEngine.Vector3 pos = BSExtensionMethods2.ExtractTranslationFromMatrix(ref trans);
            UnityEngine.Quaternion rot = BSExtensionMethods2.ExtractRotationFromMatrix(ref trans);
            UnityEngine.Vector3 scale = BSExtensionMethods2.ExtractScaleFromMatrix(ref trans);
            UnityEngine.Color c = new UnityEngine.Color(color.X, color.Y, color.Z);
            BUtility.DebugDrawCapsule(pos, rot, scale, radius, halfHeight, upAxis, c);
        }
        public override void DrawCylinder(float radius, float halfHeight, int upAxis, ref Matrix trans, ref Vector3 color) {
            UnityEngine.Vector3 pos = BSExtensionMethods2.ExtractTranslationFromMatrix(ref trans);
            UnityEngine.Quaternion rot = BSExtensionMethods2.ExtractRotationFromMatrix(ref trans);
            UnityEngine.Vector3 scale = BSExtensionMethods2.ExtractScaleFromMatrix(ref trans);
            UnityEngine.Color c = new UnityEngine.Color(color.X, color.Y, color.Z);
            BUtility.DebugDrawCylinder(pos,rot,scale,radius,halfHeight,upAxis, c);
        }
        public override void DrawCone(float radius, float height, int upAxis, ref Matrix trans, ref Vector3 color) {
            UnityEngine.Vector3 pos = BSExtensionMethods2.ExtractTranslationFromMatrix(ref trans);
            UnityEngine.Quaternion rot = BSExtensionMethods2.ExtractRotationFromMatrix(ref trans);
            UnityEngine.Vector3 scale = BSExtensionMethods2.ExtractScaleFromMatrix(ref trans);
            UnityEngine.Color c = new UnityEngine.Color(color.X, color.Y, color.Z);
            BUtility.DebugDrawCone(pos, rot, scale, radius, height, upAxis, c);
        }
        public override void DrawPlane(ref Vector3 planeNormal, float planeConst, ref Matrix trans, ref Vector3 color) {
            UnityEngine.Vector3 pos = BSExtensionMethods2.ExtractTranslationFromMatrix(ref trans);
            UnityEngine.Quaternion rot = BSExtensionMethods2.ExtractRotationFromMatrix(ref trans);
            UnityEngine.Vector3 scale = BSExtensionMethods2.ExtractScaleFromMatrix(ref trans);
            UnityEngine.Color c = new UnityEngine.Color(color.X, color.Y, color.Z);
            BUtility.DebugDrawPlane(pos, rot, scale, planeNormal.ToUnity(), planeConst, c);
        }
    }
}
