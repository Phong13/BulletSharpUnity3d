using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Collections.Generic;
using BulletSharp.Math;

namespace BulletSharp
{
	public class ConvexHullShape : PolyhedralConvexAabbCachingShape
	{
        private Vector3Array _points;
        private Vector3Array _unscaledPoints;

		public ConvexHullShape()
			: base(btConvexHullShape_new())
		{
		}

        public ConvexHullShape(float[] points)
            : this(points, points.Length/3, 3 * sizeof(float))
        {
        }

        public ConvexHullShape(float[] points, int numPoints)
            : this(points, numPoints, 3 * sizeof(float))
        {
        }

		public ConvexHullShape(float[] points, int numPoints, int stride)
			: base(btConvexHullShape_new4(points, numPoints, stride))
		{
		}

        public ConvexHullShape(IEnumerable<Vector3> points, int numPoints)
            : base(btConvexHullShape_new())
		{
            int i = 0;
            foreach (Vector3 v in points)
            {
                Vector3 viter = v;
                btConvexHullShape_addPoint(_native, ref viter);
                i++;
                if (i == numPoints)
                {
                    break;
                }
            }
            RecalcLocalAabb();
		}

		public ConvexHullShape(IEnumerable<Vector3> points)
            : base(btConvexHullShape_new())
		{
		    foreach (Vector3 v in points)
            {
                Vector3 viter = v;
                btConvexHullShape_addPoint(_native, ref viter);
            }
		    RecalcLocalAabb();
		}

        public void AddPointRef(ref Vector3 point)
        {
            btConvexHullShape_addPoint(_native, ref point);
        }

		public void AddPoint(Vector3 point)
		{
			btConvexHullShape_addPoint(_native, ref point);
		}

        public void AddPointRef(ref Vector3 point, bool recalculateLocalAabb)
        {
            btConvexHullShape_addPoint2(_native, ref point, recalculateLocalAabb);
        }

		public void AddPoint(Vector3 point, bool recalculateLocalAabb)
		{
			btConvexHullShape_addPoint2(_native, ref point, recalculateLocalAabb);
		}

		public void GetScaledPoint(int i, out Vector3 value)
		{
			btConvexHullShape_getScaledPoint(_native, i, out value);
		}

        public Vector3 GetScaledPoint(int i)
        {
            Vector3 value;
            btConvexHullShape_getScaledPoint(_native, i, out value);
            return value;
        }
		/*
        public void Project(ref Matrix trans, ref Vector3 dir, out float minProj, out float maxProj, out Vector3 witnesPtMin, out Vector3 witnesPtMax)
        {
            btConvexHullShape_project(_native, ref trans, ref dir, out minProj, out maxProj, out witnesPtMin, out witnesPtMax);
        }
*/
		public int NumPoints
		{
			get { return btConvexHullShape_getNumPoints(_native); }
		}

		public Vector3Array Points
		{
			get
			{
                if (_points == null || _points.Count != NumPoints)
			    {
			        _points = new Vector3Array(btConvexHullShape_getPoints(_native), NumPoints);
			    }
                return _points;
			}
		}

        public Vector3Array UnscaledPoints
		{
			get
			{
                if (_unscaledPoints == null || _unscaledPoints.Count != NumPoints)
                {
                    _unscaledPoints = new Vector3Array(btConvexHullShape_getUnscaledPoints(_native), NumPoints);
                }
                return _unscaledPoints;
			}
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btConvexHullShape_new();
		//[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		//static extern IntPtr btConvexHullShape_new2(Vector3[] points);
		//[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		//static extern IntPtr btConvexHullShape_new3(Vector3[] points, int numPoints);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btConvexHullShape_new4(float[] points, int numPoints, int stride);
		//[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		//static extern IntPtr btConvexHullShape_new4(Vector3[] points, int numPoints, int stride);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConvexHullShape_addPoint(IntPtr obj, [In] ref Vector3 point);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConvexHullShape_addPoint2(IntPtr obj, [In] ref Vector3 point, bool recalculateLocalAabb);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btConvexHullShape_getNumPoints(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btConvexHullShape_getPoints(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConvexHullShape_getScaledPoint(IntPtr obj, int i, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btConvexHullShape_getUnscaledPoints(IntPtr obj);
	//	[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
    //    static extern void btConvexHullShape_project(IntPtr obj, [In] ref Matrix trans, [In] ref Vector3 dir, [Out] out float minProj, [Out] out float maxProj, [Out] out Vector3 witnesPtMin, [Out] out Vector3 witnesPtMax);
	}

    [StructLayout(LayoutKind.Sequential)]
    internal struct ConvexHullShapeFloatData
    {
        public ConvexInternalShapeFloatData ConvexInternalShapeData;
        public IntPtr UnscaledPointsFloatPtr;
        public IntPtr UnscaledPointsDoublePtr;
        public int NumUnscaledPoints;
        public int Padding;

        public static int Offset(string fieldName) { return Marshal.OffsetOf(typeof(ConvexHullShapeFloatData), fieldName).ToInt32(); }
    }
}
