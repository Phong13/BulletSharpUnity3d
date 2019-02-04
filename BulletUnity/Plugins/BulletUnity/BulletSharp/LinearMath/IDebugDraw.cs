/*
 * C# / XNA  port of Bullet (c) 2011 Mark Neale <xexuxjy@hotmail.com>
 *
 * Bullet Continuous Collision Detection and Physics Library
 * Copyright (c) 2003-2008 Erwin Coumans  http://www.bulletphysics.com/
 *
 * This software is provided 'as-is', without any express or implied warranty.
 * In no event will the authors be held liable for any damages arising from
 * the use of this software.
 * 
 * Permission is granted to anyone to use this software for any purpose, 
 * including commercial applications, and to alter it and redistribute it
 * freely, subject to the following restrictions:
 * 
 * 1. The origin of this software must not be misrepresented; you must not
 *    claim that you wrote the original software. If you use this software
 *    in a product, an acknowledgment in the product documentation would be
 *    appreciated but is not required.
 * 2. Altered source versions must be plainly marked as such, and must not be
 *    misrepresented as being the original software.
 * 3. This notice may not be removed or altered from any source distribution.
 */

using BulletSharp.Math;
using System;

namespace BulletSharp
{
    public interface IDebugDraw
    {
        void DrawLine(ref Vector3 from, ref Vector3 to, ref Vector3 fromColor);
        void DrawLine(ref Vector3 from, ref Vector3 to, ref Vector3 fromColor, ref Vector3 toColor);
        void DrawBox(ref Vector3 bbMin, ref Vector3 bbMax, ref Vector3 color);
        void DrawBox(ref Vector3 bbMin, ref Vector3 bbMax, ref Matrix trans, ref Vector3 color);
        void DrawSphere(ref Vector3 p, float radius, ref Vector3 color);
        void DrawSphere(float radius, ref Matrix transform, ref Vector3 color);
        void DrawTriangle(ref Vector3 v0, ref Vector3 v1, ref Vector3 v2, ref Vector3 n0, ref Vector3 n1, ref Vector3 n2, ref Vector3 color, float alpha);
        void DrawTriangle(ref Vector3 v0, ref Vector3 v1, ref Vector3 v2, ref Vector3 color, float alpha);
        void DrawContactPoint(ref Vector3 pointOnB, ref Vector3 normalOnB, float distance, int lifeTime, ref Vector3 color);
        void ReportErrorWarning(String warningString);
        void Draw3dText(ref Vector3 location, String textString);

        DebugDrawModes DebugMode { get; set; }

        void DrawAabb(ref Vector3 from, ref Vector3 to, ref Vector3 color);
        void DrawTransform(ref Matrix transform, float orthoLen);
        void DrawArc(ref Vector3 center, ref Vector3 normal, ref Vector3 axis, float radiusA, float radiusB, float minAngle, float maxAngle,
            ref Vector3 color, bool drawSect);
        void DrawArc(ref Vector3 center, ref Vector3 normal, ref Vector3 axis, float radiusA, float radiusB, float minAngle, float maxAngle,
            ref Vector3 color, bool drawSect, float stepDegrees);
        void DrawSpherePatch(ref Vector3 center, ref Vector3 up, ref Vector3 axis, float radius,
            float minTh, float maxTh, float minPs, float maxPs, ref Vector3 color);
        void DrawSpherePatch(ref Vector3 center, ref Vector3 up, ref Vector3 axis, float radius,
            float minTh, float maxTh, float minPs, float maxPs, ref Vector3 color, float stepDegrees);
        void DrawCapsule(float radius, float halfHeight, int upAxis, ref Matrix transform, ref Vector3 color);
        void DrawCylinder(float radius, float halfHeight, int upAxis, ref Matrix transform, ref Vector3 color);
        void DrawCone(float radius, float height, int upAxis, ref Matrix transform, ref Vector3 color);
        void DrawPlane(ref Vector3 planeNormal, float planeConst, ref Matrix transform, ref Vector3 color);
    }



    /*-------------------------------------------------------------------------------------*/

    [Flags]
    public enum DebugDrawModes
    {
        None = 0,
        DrawWireframe = 1,
        DrawAabb = 2,
        DrawFeaturesText = 4,
        DrawContactPoints = 8,
        NoDeactivation = 16,
        NoHelpText = 32,
        DrawText = 64,
        ProfileTimings = 128,
        EnableSatComparison = 256,
        DisableBulletLCP = 512,
        EnableCCD = 1024,
        DrawConstraints = (1 << 11),
        DrawConstraintLimits = (1 << 12),
        DrawFastWireframe = (1 << 13),
        DrawNormals = (1 << 14),
        All = DrawWireframe | DrawAabb | DrawFeaturesText | DrawContactPoints | DrawText | DrawConstraints | DrawConstraintLimits,
        MaxDebugDrawMode
    }

    /*-------------------------------------------------------------------------------------*/

}
