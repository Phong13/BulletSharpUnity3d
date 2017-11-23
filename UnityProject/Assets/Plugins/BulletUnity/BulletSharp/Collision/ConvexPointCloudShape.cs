using System;
using BulletSharp.Math;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class ConvexPointCloudShape : PolyhedralConvexAabbCachingShape
	{
		private Vector3Array _unscaledPoints;

		public ConvexPointCloudShape()
			: base(btConvexPointCloudShape_new())
		{
		}

		public ConvexPointCloudShape(Vector3Array points, int numPoints, Vector3 localScaling,
			bool computeAabb = true)
			: base(btConvexPointCloudShape_new2(points._native, numPoints, ref localScaling,
				computeAabb))
		{
			_unscaledPoints = points;
		}

		public void GetScaledPoint(int index, out Vector3 value)
		{
			btConvexPointCloudShape_getScaledPoint(Native, index, out value);
		}

		public Vector3 GetScaledPoint(int index)
		{
			Vector3 value;
			btConvexPointCloudShape_getScaledPoint(Native, index, out value);
			return value;
		}

		public void SetPoints(Vector3Array points, int numPoints, bool computeAabb = true)
		{
			btConvexPointCloudShape_setPoints(Native, points._native, numPoints,
				computeAabb);
			_unscaledPoints = points;
		}

		public void SetPoints(Vector3Array points, int numPoints, bool computeAabb, Vector3 localScaling)
		{
			btConvexPointCloudShape_setPoints2(Native, points._native, numPoints,
				computeAabb, ref localScaling);
			_unscaledPoints = points;
		}

		public int NumPoints => btConvexPointCloudShape_getNumPoints(Native);

		public Vector3Array UnscaledPoints
		{
			get
			{
				if (_unscaledPoints == null || _unscaledPoints.Count != NumPoints)
				{
					IntPtr unscaledPointsPtr = btConvexPointCloudShape_getUnscaledPoints(Native);
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
