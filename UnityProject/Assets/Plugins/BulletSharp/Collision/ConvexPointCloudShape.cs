using System;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.Math;

namespace BulletSharp
{
	public class ConvexPointCloudShape : PolyhedralConvexAabbCachingShape
	{
        private Vector3Array _unscaledPoints;

		public ConvexPointCloudShape()
			: base(btConvexPointCloudShape_new())
		{
		}

        public ConvexPointCloudShape(Vector3Array points, int numPoints, Vector3 localScaling)
            : base(btConvexPointCloudShape_new2(points._native, numPoints, ref localScaling))
		{
            _unscaledPoints = points;
		}

        public ConvexPointCloudShape(Vector3Array points, int numPoints, Vector3 localScaling, bool computeAabb)
            : base(btConvexPointCloudShape_new3(points._native, numPoints, ref localScaling, computeAabb))
		{
            _unscaledPoints = points;
		}

		public void GetScaledPoint(int index, out Vector3 value)
		{
			btConvexPointCloudShape_getScaledPoint(_native, index, out value);
		}

		public Vector3 GetScaledPoint(int index)
		{
			Vector3 value;
			btConvexPointCloudShape_getScaledPoint(_native, index, out value);
			return value;
		}

        public void SetPoints(Vector3Array points, int numPoints)
		{
            _unscaledPoints = points;
			btConvexPointCloudShape_setPoints(_native, points._native, numPoints);
		}

        public void SetPoints(Vector3Array points, int numPoints, bool computeAabb)
		{
            _unscaledPoints = points;
            btConvexPointCloudShape_setPoints2(_native, points._native, numPoints, computeAabb);
		}

        public void SetPoints(Vector3Array points, int numPoints, bool computeAabb, Vector3 localScaling)
		{
            _unscaledPoints = points;
            btConvexPointCloudShape_setPoints3(_native, points._native, numPoints, computeAabb, ref localScaling);
		}

		public int NumPoints
		{
			get { return btConvexPointCloudShape_getNumPoints(_native); }
		}

		public Vector3Array UnscaledPoints
		{
			get
			{
                if (_unscaledPoints == null || _unscaledPoints.Count != NumPoints)
                {
                    IntPtr unscaledPointsPtr = btConvexPointCloudShape_getUnscaledPoints(_native);
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

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btConvexPointCloudShape_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btConvexPointCloudShape_new2(IntPtr points, int numPoints, [In] ref Vector3 localScaling);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btConvexPointCloudShape_new3(IntPtr points, int numPoints, [In] ref Vector3 localScaling, bool computeAabb);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btConvexPointCloudShape_getNumPoints(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConvexPointCloudShape_getScaledPoint(IntPtr obj, int index, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btConvexPointCloudShape_getUnscaledPoints(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btConvexPointCloudShape_setPoints(IntPtr obj, IntPtr points, int numPoints);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btConvexPointCloudShape_setPoints2(IntPtr obj, IntPtr points, int numPoints, bool computeAabb);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btConvexPointCloudShape_setPoints3(IntPtr obj, IntPtr points, int numPoints, bool computeAabb, [In] ref Vector3 localScaling);
	}
}
