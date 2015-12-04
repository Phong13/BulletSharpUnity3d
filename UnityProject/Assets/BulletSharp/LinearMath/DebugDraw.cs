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

using System;
using System.Security;
using System.Runtime.InteropServices;
using BulletSharp.Math;

namespace BulletSharp
{
    public abstract class DebugDraw : IDebugDraw//, IDisposable
    {
        internal IntPtr _native;

        [UnmanagedFunctionPointer(Native.Conv), SuppressUnmanagedCodeSecurity]
        delegate void DrawAabbUnmanagedDelegate([In] ref Vector3 from, [In] ref Vector3 to, [In] ref Vector3 color);
        [UnmanagedFunctionPointer(Native.Conv), SuppressUnmanagedCodeSecurity]
        delegate void DrawArcUnmanagedDelegate([In] ref Vector3 center, [In] ref Vector3 normal, [In] ref Vector3 axis, float radiusA, float radiusB,
            float minAngle, float maxAngle, ref Vector3 color, bool drawSect, float stepDegrees);
        [UnmanagedFunctionPointer(Native.Conv), SuppressUnmanagedCodeSecurity]
        delegate void DrawBoxUnmanagedDelegate([In] ref Vector3 bbMin, [In] ref Vector3 bbMax, [In] ref Matrix trans, [In] ref Vector3 color);
        [UnmanagedFunctionPointer(Native.Conv), SuppressUnmanagedCodeSecurity]
        delegate void DrawCapsuleUnmanagedDelegate(float radius, float halfHeight, int upAxis, [In] ref Matrix transform, [In] ref Vector3 color);
        [UnmanagedFunctionPointer(Native.Conv), SuppressUnmanagedCodeSecurity]
        delegate void DrawConeUnmanagedDelegate(float radius, float height, int upAxis, [In] ref Matrix transform, [In] ref Vector3 color);
        [UnmanagedFunctionPointer(Native.Conv), SuppressUnmanagedCodeSecurity]
        delegate void DrawContactPointUnmanagedDelegate([In] ref Vector3 pointOnB, [In] ref Vector3 normalOnB, float distance, int lifeTime, [In] ref Vector3 color);
        [UnmanagedFunctionPointer(Native.Conv), SuppressUnmanagedCodeSecurity]
        delegate void DrawCylinderUnmanagedDelegate(float radius, float halfHeight, int upAxis, [In] ref Matrix transform, [In] ref Vector3 color);
        [UnmanagedFunctionPointer(Native.Conv), SuppressUnmanagedCodeSecurity]
        delegate void DrawLineUnmanagedDelegate([In] ref Vector3 from, [In] ref Vector3 to, [In] ref Vector3 color);
        [UnmanagedFunctionPointer(Native.Conv), SuppressUnmanagedCodeSecurity]
        delegate void DrawPlaneUnmanagedDelegate([In] ref Vector3 planeNormal, float planeConst, [In] ref Matrix transform, [In] ref Vector3 color);
        [UnmanagedFunctionPointer(Native.Conv), SuppressUnmanagedCodeSecurity]
        delegate void DrawSphereUnmanagedDelegate(float radius, [In] ref Matrix transform, [In] ref Vector3 color);
        [UnmanagedFunctionPointer(Native.Conv), SuppressUnmanagedCodeSecurity]
        delegate void DrawSpherePatchUnmanagedDelegate([In] ref Vector3 center, [In] ref Vector3 up, [In] ref Vector3 axis, float radius,
            float minTh, float maxTh, float minPs, float maxPs, [In] ref Vector3 color, float stepDegrees);
        [UnmanagedFunctionPointer(Native.Conv), SuppressUnmanagedCodeSecurity]
        delegate void DrawTransformUnmanagedDelegate([In] ref Matrix transform, float orthoLen);
        [UnmanagedFunctionPointer(Native.Conv), SuppressUnmanagedCodeSecurity]
        delegate void DrawTriangleUnmanagedDelegate([In] ref Vector3 v0, [In] ref Vector3 v1, [In] ref Vector3 v2, [In] ref Vector3 color, float alpha);
        [UnmanagedFunctionPointer(Native.Conv), SuppressUnmanagedCodeSecurity]
        delegate void SimpleCallback(int x);
        [UnmanagedFunctionPointer(Native.Conv), SuppressUnmanagedCodeSecurity]
        delegate DebugDrawModes GetDebugModeUnmanagedDelegate();

        DrawAabbUnmanagedDelegate _drawAabb;
        DrawArcUnmanagedDelegate _drawArc;
        DrawBoxUnmanagedDelegate _drawBox;
        DrawCapsuleUnmanagedDelegate _drawCapsule;
        DrawConeUnmanagedDelegate _drawCone;
        DrawContactPointUnmanagedDelegate _drawContactPoint;
        DrawCylinderUnmanagedDelegate _drawCylinder;
        DrawLineUnmanagedDelegate _drawLine;
        DrawPlaneUnmanagedDelegate _drawPlane;
        DrawSphereUnmanagedDelegate _drawSphere;
        DrawSpherePatchUnmanagedDelegate _drawSpherePatch;
        DrawTransformUnmanagedDelegate _drawTransform;
        DrawTriangleUnmanagedDelegate _drawTriangle;
        GetDebugModeUnmanagedDelegate _getDebugMode;
        SimpleCallback _cb;

        internal static IntPtr CreateWrapper(IDebugDraw value, bool weakReference)
        {
            DrawAabbUnmanagedDelegate a = new DrawAabbUnmanagedDelegate(value.DrawAabb);
            /*
            _drawArc = new DrawArcUnmanagedDelegate(DrawArc);
            _drawBox = new DrawBoxUnmanagedDelegate(DrawBox);
            _drawCapsule = new DrawCapsuleUnmanagedDelegate(DrawCapsule);
            _drawCone = new DrawConeUnmanagedDelegate(DrawCone);
            _drawContactPoint = new DrawContactPointUnmanagedDelegate(DrawContactPoint);
            _drawCylinder = new DrawCylinderUnmanagedDelegate(DrawCylinder);
            _drawLine = new DrawLineUnmanagedDelegate(DrawLine);
            _drawPlane = new DrawPlaneUnmanagedDelegate(DrawPlane);
            _drawSphere = new DrawSphereUnmanagedDelegate(DrawSphere);
            _drawSpherePatch = new DrawSpherePatchUnmanagedDelegate(DrawSpherePatch);
            _drawTransform = new DrawTransformUnmanagedDelegate(DrawTransform);
            _drawTriangle = new DrawTriangleUnmanagedDelegate(DrawTriangle);
            _getDebugMode = new GetDebugModeUnmanagedDelegate(GetDebugModeUnmanaged);
            _cb = new SimpleCallback(SimpleCallbackUnmanaged);

            _native = btIDebugDrawWrapper_new(
                GCHandle.ToIntPtr(GCHandle.Alloc(this)),
                Marshal.GetFunctionPointerForDelegate(_drawAabb),
                Marshal.GetFunctionPointerForDelegate(_drawArc),
                Marshal.GetFunctionPointerForDelegate(_drawBox),
                Marshal.GetFunctionPointerForDelegate(_drawCapsule),
                Marshal.GetFunctionPointerForDelegate(_drawCone),
                Marshal.GetFunctionPointerForDelegate(_drawContactPoint),
                Marshal.GetFunctionPointerForDelegate(_drawCylinder),
                Marshal.GetFunctionPointerForDelegate(_drawLine),
                Marshal.GetFunctionPointerForDelegate(_drawPlane),
                Marshal.GetFunctionPointerForDelegate(_drawSphere),
                Marshal.GetFunctionPointerForDelegate(_drawSpherePatch),
                Marshal.GetFunctionPointerForDelegate(_drawTransform),
                Marshal.GetFunctionPointerForDelegate(_drawTriangle),
                Marshal.GetFunctionPointerForDelegate(_getDebugMode),
                Marshal.GetFunctionPointerForDelegate(_cb));
            */
            return IntPtr.Zero;
        }

        internal static IntPtr GetUnmanaged(IDebugDraw debugDrawer)
        {
            if (debugDrawer == null)
            {
                return IntPtr.Zero;
            }

            if (debugDrawer is DebugDraw)
            {
                return (debugDrawer as DebugDraw)._native;
            }

            //if (ObjectTable.Contains(debugDraw))
		    //    return ObjectTable.GetUnmanagedObject(debugDraw);

            throw new NotImplementedException();
            //GCHandle handle = GCHandle.Alloc(debugDrawer);
            //IntPtr wrapper = btIDebugDrawWrapper_new(GCHandle.ToIntPtr(handle), IntPtr.Zero);
            //ObjectTable.Add(debugDraw, wrapper);
            //return wrapper;
        }

        internal static IDebugDraw GetManaged(IntPtr debugDrawer)
        {
            if (debugDrawer == IntPtr.Zero)
            {
                return null;
            }

            //if (ObjectTable.Contains(debugDrawer)
		    //    return ObjectTable.GetObject<IDebugDraw^>(debugDrawer);

            IntPtr handle = btIDebugDrawWrapper_getGCHandle(debugDrawer);
            return GCHandle.FromIntPtr(handle).Target as IDebugDraw;
        }
        
        void SimpleCallbackUnmanaged(int x)
        {
            throw new NotImplementedException();
        }

        DebugDrawModes GetDebugModeUnmanaged()
        {
            return DebugMode;
        }

        internal void InitTarget(IDebugDraw target)
        {
            _drawAabb = new DrawAabbUnmanagedDelegate(target.DrawAabb);
            _drawArc = new DrawArcUnmanagedDelegate(target.DrawArc);
            _drawBox = new DrawBoxUnmanagedDelegate(target.DrawBox);
            _drawCapsule = new DrawCapsuleUnmanagedDelegate(target.DrawCapsule);
            _drawCone = new DrawConeUnmanagedDelegate(target.DrawCone);
            _drawContactPoint = new DrawContactPointUnmanagedDelegate(target.DrawContactPoint);
            _drawCylinder = new DrawCylinderUnmanagedDelegate(target.DrawCylinder);
            _drawLine = new DrawLineUnmanagedDelegate(target.DrawLine);
            _drawPlane = new DrawPlaneUnmanagedDelegate(target.DrawPlane);
            _drawSphere = new DrawSphereUnmanagedDelegate(target.DrawSphere);
            _drawSpherePatch = new DrawSpherePatchUnmanagedDelegate(target.DrawSpherePatch);
            _drawTransform = new DrawTransformUnmanagedDelegate(target.DrawTransform);
            _drawTriangle = new DrawTriangleUnmanagedDelegate(target.DrawTriangle);
            _getDebugMode = new GetDebugModeUnmanagedDelegate(GetDebugModeUnmanaged);
            _cb = new SimpleCallback(SimpleCallbackUnmanaged);

            _native = btIDebugDrawWrapper_new(
                GCHandle.ToIntPtr(GCHandle.Alloc(this)),
                Marshal.GetFunctionPointerForDelegate(_drawAabb),
                Marshal.GetFunctionPointerForDelegate(_drawArc),
                Marshal.GetFunctionPointerForDelegate(_drawBox),
                Marshal.GetFunctionPointerForDelegate(_drawCapsule),
                Marshal.GetFunctionPointerForDelegate(_drawCone),
                Marshal.GetFunctionPointerForDelegate(_drawContactPoint),
                Marshal.GetFunctionPointerForDelegate(_drawCylinder),
                Marshal.GetFunctionPointerForDelegate(_drawLine),
                Marshal.GetFunctionPointerForDelegate(_drawPlane),
                Marshal.GetFunctionPointerForDelegate(_drawSphere),
                Marshal.GetFunctionPointerForDelegate(_drawSpherePatch),
                Marshal.GetFunctionPointerForDelegate(_drawTransform),
                Marshal.GetFunctionPointerForDelegate(_drawTriangle),
                Marshal.GetFunctionPointerForDelegate(_getDebugMode),
                Marshal.GetFunctionPointerForDelegate(_cb));
        }

        internal DebugDraw(IDebugDraw target)
        {
            InitTarget(target);
        }

        public DebugDraw()
        {
            InitTarget(this);
        }

        public abstract void DrawLine(ref Vector3 from, ref Vector3 to, ref Vector3 color);
        public abstract void Draw3dText(ref Vector3 location, String textString);
        public abstract void ReportErrorWarning(String warningString);
        public abstract DebugDrawModes DebugMode { get; set; }

        public void DrawLine(Vector3 from, Vector3 to, Vector3 color)
        {
            DrawLine(ref from, ref to, ref color);
        }

        public virtual void DrawLine(ref Vector3 from, ref Vector3 to, ref Vector3 fromColor, ref Vector3 toColor)
        {
            DrawLine(ref from, ref to, ref fromColor);
        }

        void DrawAabb(Vector3 from, Vector3 to, Vector3 color)
        {
            DrawAabb(ref from, ref to, ref color);
        }

        public virtual void DrawAabb(ref Vector3 from, ref Vector3 to, ref Vector3 color)
        {
            Vector3 halfExtents = (to - from) * 0.5f;
            Vector3 center = (to + from) * 0.5f;
            int i, j;

            Vector3 edgecoord = new Vector3(1.0f, 1.0f, 1.0f), pa, pb;
            for (i = 0; i < 4; i++)
            {
                for (j = 0; j < 3; j++)
                {
                    pa = new Vector3(edgecoord.X * halfExtents.X, edgecoord.Y * halfExtents.Y,
                           edgecoord.Z * halfExtents.Z);
                    pa += center;

                    int othercoord = j % 3;
                    edgecoord[othercoord] *= -1.0f;
                    pb = new Vector3(edgecoord.X * halfExtents.X, edgecoord.Y * halfExtents.Y,
                            edgecoord.Z * halfExtents.Z);
                    pb += center;

                    DrawLine(pa, pb, color);
                }
                edgecoord = new Vector3(-1.0f, -1.0f, -1.0f);
                if (i < 3)
                {
                    edgecoord[i] *= -1.0f;
                }
            }
        }

        public virtual void DrawArc(ref Vector3 center, ref Vector3 normal, ref Vector3 axis, float radiusA, float radiusB, float minAngle, float maxAngle,
            ref Vector3 color, bool drawSect)
        {
            DrawArc(ref center, ref normal, ref axis, radiusA, radiusB, minAngle, maxAngle, ref color, drawSect, 10f);
        }

        public virtual void DrawArc(ref Vector3 center, ref Vector3 normal, ref Vector3 axis, float radiusA, float radiusB, float minAngle, float maxAngle,
            ref Vector3 color, bool drawSect, float stepDegrees)
        {
            Vector3 vx = axis;
            Vector3 vy = Vector3.Cross(normal, axis);
            float step = stepDegrees * MathUtil.SIMD_RADS_PER_DEG;
            int nSteps = (int)((maxAngle - minAngle) / step);
            if (nSteps == 0)
            {
                nSteps = 1;
            }
            Vector3 prev = center + radiusA * vx * (float)System.Math.Cos(minAngle) + radiusB * vy * (float)System.Math.Sin(minAngle);
            if (drawSect)
            {
                DrawLine(ref center, ref prev, ref color);
            }
            for (int i = 1; i <= nSteps; i++)
            {
                float angle = minAngle + (maxAngle - minAngle) * i / nSteps;
                Vector3 next = center + radiusA * vx * (float)System.Math.Cos(angle) + radiusB * vy * (float)System.Math.Sin(angle);
                DrawLine(ref prev, ref next, ref color);
                prev = next;
            }
            if (drawSect)
            {
                DrawLine(ref center, ref prev, ref color);
            }
        }

        public virtual void DrawBox(ref Vector3 bbMin, ref Vector3 bbMax, ref Vector3 color)
        {
            DrawLine(bbMin, new Vector3(bbMax.X, bbMin.Y, bbMin.Z), color);
            DrawLine(new Vector3(bbMax.X, bbMin.Y, bbMin.Z), new Vector3(bbMax.X, bbMax.Y, bbMin.Z), color);
            DrawLine(new Vector3(bbMax.X, bbMax.Y, bbMin.Z), new Vector3(bbMin.X, bbMax.Y, bbMin.Z), color);
            DrawLine(new Vector3(bbMin.X, bbMax.Y, bbMin.Z), bbMin, color);
            DrawLine(bbMin, new Vector3(bbMin.X, bbMin.Y, bbMax.Z), color);
            DrawLine(new Vector3(bbMax.X, bbMin.Y, bbMin.Z), new Vector3(bbMax.X, bbMin.Y, bbMax.Z), color);
            DrawLine(new Vector3(bbMax.X, bbMax.Y, bbMin.Z), bbMax, color);
            DrawLine(new Vector3(bbMin.X, bbMax.Y, bbMin.Z), new Vector3(bbMin.X, bbMax.Y, bbMax.Z), color);
            DrawLine(new Vector3(bbMin.X, bbMin.Y, bbMax.Z), new Vector3(bbMax.X, bbMin.Y, bbMax.Z), color);
            DrawLine(new Vector3(bbMax.X, bbMin.Y, bbMax.Z), bbMax, color);
            DrawLine(bbMax, new Vector3(bbMin.X, bbMax.Y, bbMax.Z), color);
            DrawLine(new Vector3(bbMin.X, bbMax.Y, bbMax.Z), new Vector3(bbMin.X, bbMin.Y, bbMax.Z), color);
        }

        public virtual void DrawBox(ref Vector3 bbMin, ref Vector3 bbMax, ref Matrix trans, ref Vector3 color)
        {
            var p1 = Vector3.TransformCoordinate(bbMin, trans);
            var p2 = Vector3.TransformCoordinate(new Vector3(bbMax.X, bbMin.Y, bbMin.Z), trans);
            var p3 = Vector3.TransformCoordinate(new Vector3(bbMax.X, bbMax.Y, bbMin.Z), trans);
            var p4 = Vector3.TransformCoordinate(new Vector3(bbMin.X, bbMax.Y, bbMin.Z), trans);
            var p5 = Vector3.TransformCoordinate(new Vector3(bbMin.X, bbMin.Y, bbMax.Z), trans);
            var p6 = Vector3.TransformCoordinate(new Vector3(bbMax.X, bbMin.Y, bbMax.Z), trans);
            var p7 = Vector3.TransformCoordinate(bbMax, trans);
            var p8 = Vector3.TransformCoordinate(new Vector3(bbMin.X, bbMax.Y, bbMax.Z), trans);

            DrawLine(ref p1, ref p2, ref color);
            DrawLine(ref p2, ref p3, ref color);
            DrawLine(ref p3, ref p4, ref color);
            DrawLine(ref p4, ref p1, ref color);

            DrawLine(ref p1, ref p5, ref color);
            DrawLine(ref p2, ref p6, ref color);
            DrawLine(ref p3, ref p7, ref color);
            DrawLine(ref p4, ref p8, ref color);

            DrawLine(ref p5, ref p6, ref color);
            DrawLine(ref p6, ref p7, ref color);
            DrawLine(ref p7, ref p8, ref color);
            DrawLine(ref p8, ref p5, ref color);
        }

        public virtual void DrawCapsule(float radius, float halfHeight, int upAxis, ref Matrix transform, ref Vector3 color)
        {
            Vector3 capStart = Vector3.Zero; ;
            capStart[upAxis] = -halfHeight;

            Vector3 capEnd = Vector3.Zero;
            capEnd[upAxis] = halfHeight;

            // Draw the ends
            {
                Matrix childTransform = transform;
                childTransform.Origin = Vector3.TransformCoordinate(capStart, transform);
                DrawSphere(radius, ref childTransform, ref color);
            }

            {
                Matrix childTransform = transform;
                childTransform.Origin = Vector3.TransformCoordinate(capEnd, transform);
                DrawSphere(radius, ref childTransform, ref color);
            }

            // Draw some additional lines
            Vector3 start = transform.Origin;
            Matrix basis = transform.Basis;

            capStart[(upAxis + 1) % 3] = radius;
            capEnd[(upAxis + 1) % 3] = radius;
            DrawLine(start + Vector3.TransformCoordinate(capStart, basis), start + Vector3.TransformCoordinate(capEnd, basis), color);

            capStart[(upAxis + 1) % 3] = -radius;
            capEnd[(upAxis + 1) % 3] = -radius;
            DrawLine(start + Vector3.TransformCoordinate(capStart, basis), start + Vector3.TransformCoordinate(capEnd, basis), color);

            capStart[(upAxis + 2) % 3] = radius;
            capEnd[(upAxis + 2) % 3] = radius;
            DrawLine(start + Vector3.TransformCoordinate(capStart, basis), start + Vector3.TransformCoordinate(capEnd, basis), color);

            capStart[(upAxis + 2) % 3] = -radius;
            capEnd[(upAxis + 2) % 3] = -radius;
            DrawLine(start + Vector3.TransformCoordinate(capStart, basis), start + Vector3.TransformCoordinate(capEnd, basis), color);
        }

        public virtual void DrawCone(float radius, float height, int upAxis, ref Matrix transform, ref Vector3 color)
        {
            Vector3 start = transform.Origin;

            Vector3 offsetHeight = Vector3.Zero;
            offsetHeight[upAxis] = height * 0.5f;
            Vector3 offsetRadius = Vector3.Zero;
            offsetRadius[(upAxis + 1) % 3] = radius;

            Vector3 offset2Radius = Vector3.Zero;
            offsetRadius[(upAxis + 2) % 3] = radius;

            Matrix basis = transform.Basis;
            Vector3 from = start + Vector3.TransformCoordinate(offsetHeight, basis);
            DrawLine(from, start + Vector3.TransformCoordinate(-offsetHeight, basis) + offsetRadius, color);
            DrawLine(from, start + Vector3.TransformCoordinate(-offsetHeight, basis) - offsetRadius, color);
            DrawLine(from, start + Vector3.TransformCoordinate(-offsetHeight, basis) + offset2Radius, color);
            DrawLine(from, start + Vector3.TransformCoordinate(-offsetHeight, basis) - offset2Radius, color);
        }

        public virtual void DrawContactPoint(ref Vector3 pointOnB, ref Vector3 normalOnB, float distance, int lifeTime, ref Vector3 color)
        {
            Vector3 to = pointOnB + normalOnB * 1; // distance
            DrawLine(ref pointOnB, ref to, ref color);
        }

        public virtual void DrawCylinder(float radius, float halfHeight, int upAxis, ref Matrix transform, ref Vector3 color)
        {
            Vector3 start = transform.Origin;
            Matrix basis = transform.Basis;
            Vector3 offsetHeight = Vector3.Zero;
            offsetHeight[upAxis] = halfHeight;
            Vector3 offsetRadius = Vector3.Zero;
            offsetRadius[(upAxis + 1) % 3] = radius;
            DrawLine(start + Vector3.TransformCoordinate(offsetHeight + offsetRadius, basis), start + Vector3.TransformCoordinate(-offsetHeight + offsetRadius, basis), color);
            DrawLine(start + Vector3.TransformCoordinate(offsetHeight - offsetRadius, basis), start + Vector3.TransformCoordinate(-offsetHeight - offsetRadius, basis), color);
        }

        public virtual void DrawPlane(ref Vector3 planeNormal, float planeConst, ref Matrix transform, ref Vector3 color)
        {
            Vector3 planeOrigin = planeNormal * planeConst;
            Vector3 vec0, vec1;
            PlaneSpace1(ref planeNormal, out vec0, out vec1);
            float vecLen = 100f;
            Vector3 pt0 = planeOrigin + vec0 * vecLen;
            Vector3 pt1 = planeOrigin - vec0 * vecLen;
            Vector3 pt2 = planeOrigin + vec1 * vecLen;
            Vector3 pt3 = planeOrigin - vec1 * vecLen;
            DrawLine(Vector3.TransformCoordinate(pt0, transform), Vector3.TransformCoordinate(pt1, transform), color);
            DrawLine(Vector3.TransformCoordinate(pt2, transform), Vector3.TransformCoordinate(pt3, transform), color);
        }

        public virtual void DrawSphere(float radius, ref Matrix transform, ref Vector3 color)
        {
            Vector3 start = transform.Origin;
            Matrix basis = transform.Basis;

            Vector3 xoffs = Vector3.TransformCoordinate(new Vector3(radius, 0, 0), basis);
            Vector3 yoffs = Vector3.TransformCoordinate(new Vector3(0, radius, 0), basis);
            Vector3 zoffs = Vector3.TransformCoordinate(new Vector3(0, 0, radius), basis);

            // XY 
            DrawLine(start - xoffs, start + yoffs, color);
            DrawLine(start + yoffs, start + xoffs, color);
            DrawLine(start + xoffs, start - yoffs, color);
            DrawLine(start - yoffs, start - xoffs, color);

            // XZ
            DrawLine(start - xoffs, start + zoffs, color);
            DrawLine(start + zoffs, start + xoffs, color);
            DrawLine(start + xoffs, start - zoffs, color);
            DrawLine(start - zoffs, start - xoffs, color);

            // YZ
            DrawLine(start - yoffs, start + zoffs, color);
            DrawLine(start + zoffs, start + yoffs, color);
            DrawLine(start + yoffs, start - zoffs, color);
            DrawLine(start - zoffs, start - yoffs, color);
        }

        public virtual void DrawSphere(ref Vector3 p, float radius, ref Vector3 color)
        {
            Matrix tr = Matrix.Translation(p);
            DrawSphere(radius, ref tr, ref color);
        }

        public virtual void DrawSpherePatch(ref Vector3 center, ref Vector3 up, ref Vector3 axis, float radius,
            float minTh, float maxTh, float minPs, float maxPs, ref Vector3 color)
        {
            DrawSpherePatch(ref center, ref up, ref axis, radius, minTh, maxTh, minPs, maxPs, ref color, 10.0f);
        }

        public virtual void DrawSpherePatch(ref Vector3 center, ref Vector3 up, ref Vector3 axis, float radius,
            float minTh, float maxTh, float minPs, float maxPs, ref Vector3 color, float stepDegrees)
        {
            Vector3[] vA;
            Vector3[] vB;
            Vector3[] pvA, pvB, pT;
            Vector3 npole = center + up * radius;
            Vector3 spole = center - up * radius;
            Vector3 arcStart = Vector3.Zero;
            float step = stepDegrees * MathUtil.SIMD_RADS_PER_DEG;
            Vector3 kv = up;
            Vector3 iv = axis;

            Vector3 jv = Vector3.Cross(kv, iv);
            bool drawN = false;
            bool drawS = false;
            if (minTh <= -MathUtil.SIMD_HALF_PI)
            {
                minTh = -MathUtil.SIMD_HALF_PI + step;
                drawN = true;
            }
            if (maxTh >= MathUtil.SIMD_HALF_PI)
            {
                maxTh = MathUtil.SIMD_HALF_PI - step;
                drawS = true;
            }
            if (minTh > maxTh)
            {
                minTh = -MathUtil.SIMD_HALF_PI + step;
                maxTh = MathUtil.SIMD_HALF_PI - step;
                drawN = drawS = true;
            }
            int n_hor = (int)((maxTh - minTh) / step) + 1;
            if (n_hor < 2) n_hor = 2;
            float step_h = (maxTh - minTh) / (n_hor - 1);
            bool isClosed = false;
            if (minPs > maxPs)
            {
                minPs = -MathUtil.SIMD_PI + step;
                maxPs = MathUtil.SIMD_PI;
                isClosed = true;
            }
            else if ((maxPs - minPs) >= MathUtil.SIMD_PI * 2f)
            {
                isClosed = true;
            }
            else
            {
                isClosed = false;
            }
            int n_vert = (int)((maxPs - minPs) / step) + 1;
            if (n_vert < 2) n_vert = 2;

            vA = new Vector3[n_vert];
            vB = new Vector3[n_vert];
            pvA = vA; pvB = vB;

            float step_v = (maxPs - minPs) / (float)(n_vert - 1);
            for (int i = 0; i < n_hor; i++)
            {
                float th = minTh + i * step_h;
                float sth = radius * (float)System.Math.Sin(th);
                float cth = radius * (float)System.Math.Cos(th);
                for (int j = 0; j < n_vert; j++)
                {
                    float psi = minPs + (float)j * step_v;
                    float sps = (float)System.Math.Sin(psi);
                    float cps = (float)System.Math.Cos(psi);
                    pvB[j] = center + cth * cps * iv + cth * sps * jv + sth * kv;
                    if (i != 0)
                    {
                        DrawLine(pvA[j], pvB[j], color);
                    }
                    else if (drawS)
                    {
                        DrawLine(spole, pvB[j], color);
                    }
                    if (j != 0)
                    {
                        DrawLine(pvB[j - 1], pvB[j], color);
                    }
                    else
                    {
                        arcStart = pvB[j];
                    }
                    if ((i == (n_hor - 1)) && drawN)
                    {
                        DrawLine(npole, pvB[j], color);
                    }
                    if (isClosed)
                    {
                        if (j == (n_vert - 1))
                        {
                            DrawLine(arcStart, pvB[j], color);
                        }
                    }
                    else
                    {
                        if (((i == 0) || (i == (n_hor - 1))) && ((j == 0) || (j == (n_vert - 1))))
                        {
                            DrawLine(center, pvB[j], color);
                        }
                    }
                }
                pT = pvA; pvA = pvB; pvB = pT;
            }
        }

        public virtual void DrawTriangle(ref Vector3 v0, ref Vector3 v1, ref Vector3 v2, ref Vector3 n0, ref Vector3 n1, ref Vector3 n2, ref Vector3 color, float alpha)
        {
            DrawTriangle(ref v0, ref v1, ref v2, ref color, alpha);
        }

        public virtual void DrawTriangle(ref Vector3 v0, ref Vector3 v1, ref Vector3 v2, ref Vector3 color, float alpha)
        {
            DrawLine(ref v0, ref v1, ref color);
            DrawLine(ref v1, ref v2, ref color);
            DrawLine(ref v2, ref v0, ref color);
        }

        public virtual void DrawTransform(ref Matrix transform, float orthoLen)
        {
            Vector3 start = transform.Origin;
            Matrix basis = transform.Basis;

            Vector3 temp = start + Vector3.TransformCoordinate(new Vector3(orthoLen, 0, 0), basis);
            Vector3 colour = new Vector3(0.7f, 0, 0);
            DrawLine(ref start, ref temp, ref colour);
            temp = start + Vector3.TransformCoordinate(new Vector3(0, orthoLen, 0), basis);
            colour = new Vector3(0, 0.7f, 0);
            DrawLine(ref start, ref temp, ref colour);
            temp = start + Vector3.TransformCoordinate(new Vector3(0, 0, orthoLen), basis);
            colour = new Vector3(0, 0, 0.7f);
            DrawLine(ref start, ref temp, ref colour);
        }

        public static void PlaneSpace1(ref Vector3 n, out Vector3 p, out Vector3 q)
        {
            if (System.Math.Abs(n.Z) > MathUtil.SIMDSQRT12)
            {
                // choose p in y-z plane
                float a = n.Y * n.Y + n.Z * n.Z;
                float k = MathUtil.RecipSqrt(a);
                p = new Vector3(0, -n.Z * k, n.Y * k);
                // set q = n x p
                q = new Vector3(a * k, -n.X * p.Z, n.X * p.Y);
            }
            else
            {
                // choose p in x-y plane
                float a = n.X * n.X + n.Y * n.Y;
                float k = MathUtil.RecipSqrt(a);
                p = new Vector3(-n.Y * k, n.X * k, 0);
                // set q = n x p
                q = new Vector3(-n.Z * p.Y, n.Z * p.X, a * k);
            }
        }

        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btIDebugDrawWrapper_new(IntPtr debugDrawGCHandle, IntPtr drawAabbCallback,
            IntPtr drawArcCallback, IntPtr drawBoxCallback, IntPtr drawCapsuleCallback, IntPtr drawConeCallback, IntPtr drawContactPointCallback,
            IntPtr drawCylinderCallback, IntPtr drawLineCallback, IntPtr drawPlaneCallback, IntPtr drawSphereCallback, IntPtr drawSpherePatchCallback, IntPtr drawTransformCallback, IntPtr drawTriangleCallback, IntPtr getDebugModeCallback, IntPtr cb);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btIDebugDrawWrapper_getGCHandle(IntPtr obj);
    }
}
