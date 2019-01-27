using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
	public class TriangleInfo : IDisposable
	{
		internal IntPtr _native;

		internal TriangleInfo(IntPtr native)
		{
			_native = native;
		}

		public TriangleInfo()
		{
			_native = btTriangleInfo_new();
		}

		public float EdgeV0V1Angle
		{
			get { return btTriangleInfo_getEdgeV0V1Angle(_native); }
			set { btTriangleInfo_setEdgeV0V1Angle(_native, value); }
		}

		public float EdgeV1V2Angle
		{
			get { return btTriangleInfo_getEdgeV1V2Angle(_native); }
			set { btTriangleInfo_setEdgeV1V2Angle(_native, value); }
		}

		public float EdgeV2V0Angle
		{
			get { return btTriangleInfo_getEdgeV2V0Angle(_native); }
			set { btTriangleInfo_setEdgeV2V0Angle(_native, value); }
		}

		public int Flags
		{
			get { return btTriangleInfo_getFlags(_native); }
			set { btTriangleInfo_setFlags(_native, value); }
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
				btTriangleInfo_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~TriangleInfo()
		{
			Dispose(false);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btTriangleInfo_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btTriangleInfo_getEdgeV0V1Angle(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btTriangleInfo_getEdgeV1V2Angle(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btTriangleInfo_getEdgeV2V0Angle(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btTriangleInfo_getFlags(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTriangleInfo_setEdgeV0V1Angle(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTriangleInfo_setEdgeV1V2Angle(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTriangleInfo_setEdgeV2V0Angle(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTriangleInfo_setFlags(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTriangleInfo_delete(IntPtr obj);
	}

	public class TriangleInfoMap : IDisposable
	{
		internal IntPtr _native;
		bool _preventDelete;

		internal TriangleInfoMap(IntPtr native, bool preventDelete)
		{
			_native = native;
			_preventDelete = preventDelete;
		}

		public TriangleInfoMap()
		{
			_native = btTriangleInfoMap_new();
		}

		public int CalculateSerializeBufferSize()
		{
			return btTriangleInfoMap_calculateSerializeBufferSize(_native);
		}
        /*
		public void DeSerialize(TriangleInfoMapData data)
		{
			btTriangleInfoMap_deSerialize(_native, data._native);
		}
        */
		public string Serialize(IntPtr dataBuffer, Serializer serializer)
		{
			return Marshal.PtrToStringAnsi(btTriangleInfoMap_serialize(_native, dataBuffer, serializer._native));
		}

		public float ConvexEpsilon
		{
			get { return btTriangleInfoMap_getConvexEpsilon(_native); }
			set { btTriangleInfoMap_setConvexEpsilon(_native, value); }
		}

		public float EdgeDistanceThreshold
		{
			get { return btTriangleInfoMap_getEdgeDistanceThreshold(_native); }
			set { btTriangleInfoMap_setEdgeDistanceThreshold(_native, value); }
		}

		public float EqualVertexThreshold
		{
			get { return btTriangleInfoMap_getEqualVertexThreshold(_native); }
			set { btTriangleInfoMap_setEqualVertexThreshold(_native, value); }
		}

		public float MaxEdgeAngleThreshold
		{
			get { return btTriangleInfoMap_getMaxEdgeAngleThreshold(_native); }
			set { btTriangleInfoMap_setMaxEdgeAngleThreshold(_native, value); }
		}

		public float PlanarEpsilon
		{
			get { return btTriangleInfoMap_getPlanarEpsilon(_native); }
			set { btTriangleInfoMap_setPlanarEpsilon(_native, value); }
		}

		public float ZeroAreaThreshold
		{
			get { return btTriangleInfoMap_getZeroAreaThreshold(_native); }
			set { btTriangleInfoMap_setZeroAreaThreshold(_native, value); }
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
				if (!_preventDelete)
				{
					btTriangleInfoMap_delete(_native);
				}
				_native = IntPtr.Zero;
			}
		}

		~TriangleInfoMap()
		{
			Dispose(false);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btTriangleInfoMap_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btTriangleInfoMap_calculateSerializeBufferSize(IntPtr obj);
		//[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		//static extern void btTriangleInfoMap_deSerialize(IntPtr obj, IntPtr data);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btTriangleInfoMap_getConvexEpsilon(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btTriangleInfoMap_getEdgeDistanceThreshold(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btTriangleInfoMap_getEqualVertexThreshold(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btTriangleInfoMap_getMaxEdgeAngleThreshold(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btTriangleInfoMap_getPlanarEpsilon(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btTriangleInfoMap_getZeroAreaThreshold(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btTriangleInfoMap_serialize(IntPtr obj, IntPtr dataBuffer, IntPtr serializer);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTriangleInfoMap_setConvexEpsilon(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTriangleInfoMap_setEdgeDistanceThreshold(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTriangleInfoMap_setEqualVertexThreshold(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTriangleInfoMap_setMaxEdgeAngleThreshold(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTriangleInfoMap_setPlanarEpsilon(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTriangleInfoMap_setZeroAreaThreshold(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTriangleInfoMap_delete(IntPtr obj);
	}
}
