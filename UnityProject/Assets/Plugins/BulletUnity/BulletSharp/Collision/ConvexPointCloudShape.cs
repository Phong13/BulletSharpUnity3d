using System;
using BulletSharp.Math;


namespace BulletSharp
{
	public class ConvexPointCloudShape : PolyhedralConvexAabbCachingShape
	{
		private Vector3Array _unscaledPoints;

		public ConvexPointCloudShape()
			: base(UnsafeNativeMethods.btConvexPointCloudShape_new())
		{
		}

		public ConvexPointCloudShape(Vector3Array points, int numPoints, Vector3 localScaling,
			bool computeAabb = true)
			: base(UnsafeNativeMethods.btConvexPointCloudShape_new2(points._native, numPoints, ref localScaling,
				computeAabb))
		{
			_unscaledPoints = points;
		}

		public void GetScaledPoint(int index, out Vector3 value)
		{
			UnsafeNativeMethods.btConvexPointCloudShape_getScaledPoint(Native, index, out value);
		}

		public Vector3 GetScaledPoint(int index)
		{
			Vector3 value;
			UnsafeNativeMethods.btConvexPointCloudShape_getScaledPoint(Native, index, out value);
			return value;
		}

		public void SetPoints(Vector3Array points, int numPoints, bool computeAabb = true)
		{
			UnsafeNativeMethods.btConvexPointCloudShape_setPoints(Native, points._native, numPoints,
				computeAabb);
			_unscaledPoints = points;
		}

		public void SetPoints(Vector3Array points, int numPoints, bool computeAabb, Vector3 localScaling)
		{
			UnsafeNativeMethods.btConvexPointCloudShape_setPoints2(Native, points._native, numPoints,
				computeAabb, ref localScaling);
			_unscaledPoints = points;
		}

		public int NumPoints => UnsafeNativeMethods.btConvexPointCloudShape_getNumPoints(Native);

		public Vector3Array UnscaledPoints
		{
			get
			{
				if (_unscaledPoints == null || _unscaledPoints.Count != NumPoints)
				{
					IntPtr unscaledPointsPtr = UnsafeNativeMethods.btConvexPointCloudShape_getUnscaledPoints(Native);
					if (unscaledPointsPtr != IntPtr.Zero)
					{
						_unscaledPoints = new Vector3Array(unscaledPointsPtr, NumPoints);
					}
				}
				return _unscaledPoints;
			}
			set
			{
				SetPoints(value, value.Count);
			}
		}
	}
}
