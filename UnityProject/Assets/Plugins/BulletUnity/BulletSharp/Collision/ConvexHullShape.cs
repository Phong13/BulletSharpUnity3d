using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using BulletSharp.Math;


namespace BulletSharp
{
	public class ConvexHullShape : PolyhedralConvexAabbCachingShape
	{
		private Vector3Array _points;
		private Vector3Array _unscaledPoints;

		public ConvexHullShape()
			: base(UnsafeNativeMethods.btConvexHullShape_new())
		{
		}

		public ConvexHullShape(float[] points)
			: this(points, points.Length / 3, 3 * sizeof(float))
		{
		}

		public ConvexHullShape(float[] points, int numPoints, int stride = 3 * sizeof(float))
			: base(UnsafeNativeMethods.btConvexHullShape_new4(points, numPoints, stride))
		{
		}

		public ConvexHullShape(IEnumerable<Vector3> points, int numPoints)
			: base(UnsafeNativeMethods.btConvexHullShape_new())
		{
			int i = 0;
			foreach (Vector3 v in points)
			{
				Vector3 viter = v;
				AddPointRef(ref viter, false);
				i++;
				if (i == numPoints)
				{
					break;
				}
			}
			RecalcLocalAabb();
		}

		public ConvexHullShape(IEnumerable<Vector3> points)
			: base(UnsafeNativeMethods.btConvexHullShape_new())
		{
			foreach (Vector3 v in points)
			{
				Vector3 viter = v;
				AddPointRef(ref viter, false);
			}
			RecalcLocalAabb();
		}

		public void AddPointRef(ref Vector3 point, bool recalculateLocalAabb = true)
		{
			UnsafeNativeMethods.btConvexHullShape_addPoint(Native, ref point, recalculateLocalAabb);
		}

		public void AddPoint(Vector3 point, bool recalculateLocalAabb = true)
		{
			UnsafeNativeMethods.btConvexHullShape_addPoint(Native, ref point, recalculateLocalAabb);
		}

		public void GetScaledPoint(int i, out Vector3 value)
		{
			UnsafeNativeMethods.btConvexHullShape_getScaledPoint(Native, i, out value);
		}

		public Vector3 GetScaledPoint(int i)
		{
			Vector3 value;
			UnsafeNativeMethods.btConvexHullShape_getScaledPoint(Native, i, out value);
			return value;
		}

		public void OptimizeConvexHull()
		{
			UnsafeNativeMethods.btConvexHullShape_optimizeConvexHull(Native);
		}

		public int NumPoints{ get { return  UnsafeNativeMethods.btConvexHullShape_getNumPoints(Native);} }

		public Vector3Array Points
		{
			get
			{
				if (_points == null || _points.Count != NumPoints)
				{
					_points = new Vector3Array(UnsafeNativeMethods.btConvexHullShape_getPoints(Native), NumPoints);
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
					_unscaledPoints = new Vector3Array(UnsafeNativeMethods.btConvexHullShape_getUnscaledPoints(Native), NumPoints);
				}
				return _unscaledPoints;
			}
		}
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
