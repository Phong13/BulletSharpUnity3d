using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.Math;


namespace BulletSharp
{
	public class Hacd : IDisposable
	{
		internal IntPtr _native;

		[UnmanagedFunctionPointer(Native.Conv), SuppressUnmanagedCodeSecurity]
		delegate bool CallbackFunctionUnmanagedDelegate(IntPtr message, double progress, double globalConcavity, IntPtr numVertices);

		public delegate bool CallbackFunction(string message, double progress, double globalConcavity, int numVertices);

		CallbackFunctionUnmanagedDelegate _callbackFunctionUnmanaged;
		CallbackFunction _callbackFunction;

		public Hacd()
		{
			_native = UnsafeNativeMethods.HACD_HACD_new();
		}

		private bool CallbackFunctionUnmanaged(IntPtr msg, double progress, double globalConcavity, IntPtr n)
		{
			string msg2 = Marshal.PtrToStringAnsi(msg);
			return _callbackFunction(msg2, progress, globalConcavity, n.ToInt32());
		}

		public bool Compute()
		{
			return UnsafeNativeMethods.HACD_HACD_Compute(_native);
		}

		public bool Compute(bool fullCH)
		{
			return UnsafeNativeMethods.HACD_HACD_Compute2(_native, fullCH);
		}

		public bool Compute(bool fullCH, bool exportDistPoints)
		{
			return UnsafeNativeMethods.HACD_HACD_Compute3(_native, fullCH, exportDistPoints);
		}

		public void DenormalizeData()
		{
			UnsafeNativeMethods.HACD_HACD_DenormalizeData(_native);
		}

		public bool GetCH(int numCH, double[] points, long[] triangles)
		{
			if (points.Length < GetNPointsCH(numCH))
			{
				return false;
			}

			if (triangles.Length < GetNTrianglesCH(numCH))
			{
				return false;
			}

			GCHandle pointsArray = GCHandle.Alloc(points, GCHandleType.Pinned);
			GCHandle trianglesArray = GCHandle.Alloc(triangles, GCHandleType.Pinned);
			bool ret = UnsafeNativeMethods.HACD_HACD_GetCH(_native, numCH, pointsArray.AddrOfPinnedObject(), trianglesArray.AddrOfPinnedObject());
			pointsArray.Free();
			trianglesArray.Free();
			return ret;
		}

		public int GetNPointsCH(int numCH)
		{
			return UnsafeNativeMethods.HACD_HACD_GetNPointsCH(_native, numCH);
		}

		public int GetNTrianglesCH(int numCH)
		{
			return UnsafeNativeMethods.HACD_HACD_GetNTrianglesCH(_native, numCH);
		}

		public double[] GetPoints()
		{
			IntPtr pointsPtr = UnsafeNativeMethods.HACD_HACD_GetPoints(_native);
			int pointsLen = NPoints * 3;
			if (pointsLen == 0 || pointsPtr == IntPtr.Zero)
			{
				return new double[0];
			}
			double[] pointsArray = new double[pointsLen];
			Marshal.Copy(pointsPtr, pointsArray, 0, pointsLen);
			return pointsArray;
		}

		public long[] GetTriangles()
		{
			IntPtr trianglesPtr = UnsafeNativeMethods.HACD_HACD_GetTriangles(_native);
			int trianglesLen = NTriangles * 3;
			if (trianglesLen == 0 || trianglesPtr == IntPtr.Zero)
			{
				return new long[0];
			}
			long[] trianglesArray = new long[trianglesLen];
			Marshal.Copy(trianglesPtr, trianglesArray, 0, trianglesLen);
			return trianglesArray;
		}

		public void NormalizeData()
		{
			UnsafeNativeMethods.HACD_HACD_NormalizeData(_native);
		}

		public bool Save(string fileName, bool uniColor)
		{
			IntPtr filenameTemp = Marshal.StringToHGlobalAnsi(fileName);
			bool ret = UnsafeNativeMethods.HACD_HACD_Save(_native, filenameTemp, uniColor);
			Marshal.FreeHGlobal(filenameTemp);
			return ret;
		}

		public bool Save(string fileName, bool uniColor, long numCluster)
		{
			IntPtr filenameTemp = Marshal.StringToHGlobalAnsi(fileName);
			bool ret = UnsafeNativeMethods.HACD_HACD_Save2(_native, filenameTemp, uniColor, numCluster);
			Marshal.FreeHGlobal(filenameTemp);
			return ret;
		}

		public void SetPoints(ICollection<double> points)
		{
			double[] pointsArray;
			int arrayLen = points.Count;
			pointsArray = points as double[];
			if (pointsArray == null)
			{
				pointsArray = new double[arrayLen];
				points.CopyTo(pointsArray, 0);
			}

			IntPtr pointsPtr = UnsafeNativeMethods.HACD_HACD_GetPoints(_native);
			if (pointsPtr != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(pointsPtr);
			}

			pointsPtr = Marshal.AllocHGlobal(sizeof(double) * arrayLen);
			Marshal.Copy(pointsArray, 0, pointsPtr, arrayLen);
			UnsafeNativeMethods.HACD_HACD_SetPoints(_native, pointsPtr);
			NPoints = arrayLen / 3;
		}

		public void SetPoints(ICollection<Vector3> points)
		{
			double[] pointsArray = new double[points.Count * 3];
			int i = 0;
			foreach (Vector3 v in points)
			{
				pointsArray[i++] = v.X;
				pointsArray[i++] = v.Y;
				pointsArray[i++] = v.Z;
			}
			SetPoints(pointsArray);
		}

		public void SetTriangles(ICollection<long> triangles)
		{
			long[] trianglesLong;
			int arrayLen = triangles.Count;
			trianglesLong = triangles as long[];
			if (trianglesLong == null)
			{
				trianglesLong = new long[arrayLen];
				triangles.CopyTo(trianglesLong, 0);
			}

			IntPtr trianglesPtr = UnsafeNativeMethods.HACD_HACD_GetTriangles(_native);
			if (trianglesPtr != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(trianglesPtr);
			}

			trianglesPtr = Marshal.AllocHGlobal(sizeof(long) * arrayLen);
			Marshal.Copy(trianglesLong, 0, trianglesPtr, arrayLen);
			UnsafeNativeMethods.HACD_HACD_SetTriangles(_native, trianglesPtr);
			NTriangles = arrayLen / 3;
		}

		public void SetTriangles(ICollection<int> triangles)
		{
			int n = triangles.Count;
			long[] trianglesLong = new long[n];
			int i = 0;
			foreach (int t in triangles)
			{
				trianglesLong[i++] = t;
			}
			SetTriangles(trianglesLong);
		}

		public bool AddExtraDistPoints
		{
			get { return  UnsafeNativeMethods.HACD_HACD_GetAddExtraDistPoints(_native);}
			set {  UnsafeNativeMethods.HACD_HACD_SetAddExtraDistPoints(_native, value);}
		}

		public bool AddFacesPoints
		{
			get { return  UnsafeNativeMethods.HACD_HACD_GetAddFacesPoints(_native);}
			set {  UnsafeNativeMethods.HACD_HACD_SetAddFacesPoints(_native, value);}
		}

		public bool AddNeighboursDistPoints
		{
			get { return  UnsafeNativeMethods.HACD_HACD_GetAddNeighboursDistPoints(_native);}
			set {  UnsafeNativeMethods.HACD_HACD_SetAddNeighboursDistPoints(_native, value);}
		}

		public CallbackFunction Callback
		{
			get { return  _callbackFunction;}
			set
			{
				_callbackFunctionUnmanaged = CallbackFunctionUnmanaged;
				_callbackFunction = value;
				if (value != null)
				{
					UnsafeNativeMethods.HACD_HACD_SetCallBack(_native, Marshal.GetFunctionPointerForDelegate(_callbackFunctionUnmanaged));
				}
				else
				{
					UnsafeNativeMethods.HACD_HACD_SetCallBack(_native, IntPtr.Zero);
				}
			}
		}

		public double CompacityWeight
		{
			get { return  UnsafeNativeMethods.HACD_HACD_GetCompacityWeight(_native);}
			set {  UnsafeNativeMethods.HACD_HACD_SetCompacityWeight(_native, value);}
		}

		public double Concavity
		{
			get { return  UnsafeNativeMethods.HACD_HACD_GetConcavity(_native);}
			set {  UnsafeNativeMethods.HACD_HACD_SetConcavity(_native, value);}
		}

		public double ConnectDist
		{
			get { return  UnsafeNativeMethods.HACD_HACD_GetConnectDist(_native);}
			set {  UnsafeNativeMethods.HACD_HACD_SetConnectDist(_native, value);}
		}

		public int NClusters
		{
			get { return  UnsafeNativeMethods.HACD_HACD_GetNClusters(_native);}
			set {  UnsafeNativeMethods.HACD_HACD_SetNClusters(_native, value);}
		}

		public int NPoints
		{
			get { return  UnsafeNativeMethods.HACD_HACD_GetNPoints(_native);}
			set {  UnsafeNativeMethods.HACD_HACD_SetNPoints(_native, value);}
		}

		public int NTriangles
		{
			get { return  UnsafeNativeMethods.HACD_HACD_GetNTriangles(_native);}
			set {  UnsafeNativeMethods.HACD_HACD_SetNTriangles(_native, value);}
		}

		public int VerticesPerConvexHull
		{
			get { return  UnsafeNativeMethods.HACD_HACD_GetNVerticesPerCH(_native);}
			set {  UnsafeNativeMethods.HACD_HACD_SetNVerticesPerCH(_native, value);}
		}
		/*
		public long Partition
		{
			get { return  UnsafeNativeMethods.HACD_HACD_GetPartition(_native);}
		}
		*/
		public double ScaleFactor
		{
			get { return  UnsafeNativeMethods.HACD_HACD_GetScaleFactor(_native);}
			set {  UnsafeNativeMethods.HACD_HACD_SetScaleFactor(_native, value);}
		}

		public double VolumeWeight
		{
			get { return  UnsafeNativeMethods.HACD_HACD_GetVolumeWeight(_native);}
			set {  UnsafeNativeMethods.HACD_HACD_SetVolumeWeight(_native, value);}
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (_native != IntPtr.Zero)
			{
				IntPtr pointsPtr = UnsafeNativeMethods.HACD_HACD_GetPoints(_native);
				if (pointsPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(pointsPtr);
					UnsafeNativeMethods.HACD_HACD_SetPoints(_native, IntPtr.Zero);
				}

				IntPtr trianglesPtr = UnsafeNativeMethods.HACD_HACD_GetTriangles(_native);
				if (trianglesPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(trianglesPtr);
					UnsafeNativeMethods.HACD_HACD_SetTriangles(_native, IntPtr.Zero);
				}

				UnsafeNativeMethods.HACD_HACD_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~Hacd()
		{
			Dispose(false);
		}
	}
}
