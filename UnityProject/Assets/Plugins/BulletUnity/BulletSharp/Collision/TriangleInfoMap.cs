using System;
using System.Runtime.InteropServices;


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
			_native = UnsafeNativeMethods.btTriangleInfo_new();
		}

		public float EdgeV0V1Angle
		{
			get => UnsafeNativeMethods.btTriangleInfo_getEdgeV0V1Angle(_native);
			set => UnsafeNativeMethods.btTriangleInfo_setEdgeV0V1Angle(_native, value);
		}

		public float EdgeV1V2Angle
		{
			get => UnsafeNativeMethods.btTriangleInfo_getEdgeV1V2Angle(_native);
			set => UnsafeNativeMethods.btTriangleInfo_setEdgeV1V2Angle(_native, value);
		}

		public float EdgeV2V0Angle
		{
			get => UnsafeNativeMethods.btTriangleInfo_getEdgeV2V0Angle(_native);
			set => UnsafeNativeMethods.btTriangleInfo_setEdgeV2V0Angle(_native, value);
		}

		public int Flags
		{
			get => UnsafeNativeMethods.btTriangleInfo_getFlags(_native);
			set => UnsafeNativeMethods.btTriangleInfo_setFlags(_native, value);
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
				UnsafeNativeMethods.btTriangleInfo_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~TriangleInfo()
		{
			Dispose(false);
		}
	}

	public class TriangleInfoMap : IDisposable
	{
		internal IntPtr Native;
		bool _preventDelete;

		internal TriangleInfoMap(IntPtr native, bool preventDelete)
		{
			Native = native;
			_preventDelete = preventDelete;
		}

		public TriangleInfoMap()
		{
			Native = UnsafeNativeMethods.btTriangleInfoMap_new();
		}

		public int CalculateSerializeBufferSize()
		{
			return UnsafeNativeMethods.btTriangleInfoMap_calculateSerializeBufferSize(Native);
		}
		/*
		public void DeSerialize(TriangleInfoMapData data)
		{
			btTriangleInfoMap_deSerialize(Native, data._native);
		}
		*/
		public string Serialize(IntPtr dataBuffer, Serializer serializer)
		{
			return Marshal.PtrToStringAnsi(UnsafeNativeMethods.btTriangleInfoMap_serialize(Native, dataBuffer, serializer._native));
		}

		public float ConvexEpsilon
		{
			get => UnsafeNativeMethods.btTriangleInfoMap_getConvexEpsilon(Native);
			set => UnsafeNativeMethods.btTriangleInfoMap_setConvexEpsilon(Native, value);
		}

		public float EdgeDistanceThreshold
		{
			get => UnsafeNativeMethods.btTriangleInfoMap_getEdgeDistanceThreshold(Native);
			set => UnsafeNativeMethods.btTriangleInfoMap_setEdgeDistanceThreshold(Native, value);
		}

		public float EqualVertexThreshold
		{
			get => UnsafeNativeMethods.btTriangleInfoMap_getEqualVertexThreshold(Native);
			set => UnsafeNativeMethods.btTriangleInfoMap_setEqualVertexThreshold(Native, value);
		}

		public float MaxEdgeAngleThreshold
		{
			get => UnsafeNativeMethods.btTriangleInfoMap_getMaxEdgeAngleThreshold(Native);
			set => UnsafeNativeMethods.btTriangleInfoMap_setMaxEdgeAngleThreshold(Native, value);
		}

		public float PlanarEpsilon
		{
			get => UnsafeNativeMethods.btTriangleInfoMap_getPlanarEpsilon(Native);
			set => UnsafeNativeMethods.btTriangleInfoMap_setPlanarEpsilon(Native, value);
		}

		public float ZeroAreaThreshold
		{
			get => UnsafeNativeMethods.btTriangleInfoMap_getZeroAreaThreshold(Native);
			set => UnsafeNativeMethods.btTriangleInfoMap_setZeroAreaThreshold(Native, value);
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (Native != IntPtr.Zero)
			{
				if (!_preventDelete)
				{
					UnsafeNativeMethods.btTriangleInfoMap_delete(Native);
				}
				Native = IntPtr.Zero;
			}
		}

		~TriangleInfoMap()
		{
			Dispose(false);
		}
	}
}
