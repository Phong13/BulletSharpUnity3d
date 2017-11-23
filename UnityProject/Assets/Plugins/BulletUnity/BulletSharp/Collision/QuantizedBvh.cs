using System;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.Math;


namespace BulletSharp
{
	public class QuantizedBvhNode : IDisposable
	{
		internal IntPtr _native;

		internal QuantizedBvhNode(IntPtr native)
		{
			_native = native;
		}

		public QuantizedBvhNode()
		{
			_native = UnsafeNativeMethods.btQuantizedBvhNode_new();
		}

		public int EscapeIndex => UnsafeNativeMethods.btQuantizedBvhNode_getEscapeIndex(_native);

		public int EscapeIndexOrTriangleIndex
		{
			get => UnsafeNativeMethods.btQuantizedBvhNode_getEscapeIndexOrTriangleIndex(_native);
			set => UnsafeNativeMethods.btQuantizedBvhNode_setEscapeIndexOrTriangleIndex(_native, value);
		}

		public bool IsLeafNode => UnsafeNativeMethods.btQuantizedBvhNode_isLeafNode(_native);

		public int PartId => UnsafeNativeMethods.btQuantizedBvhNode_getPartId(_native);
		/*
		public UShortArray QuantizedAabbMax
		{
			get { return UnsafeNativeMethods.btQuantizedBvhNode_getQuantizedAabbMax(_native); }
		}

		public UShortArray QuantizedAabbMin
		{
			get { return UnsafeNativeMethods.btQuantizedBvhNode_getQuantizedAabbMin(_native); }
		}
		*/
		public int TriangleIndex => UnsafeNativeMethods.btQuantizedBvhNode_getTriangleIndex(_native);

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (_native != IntPtr.Zero)
			{
				UnsafeNativeMethods.btQuantizedBvhNode_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~QuantizedBvhNode()
		{
			Dispose(false);
		}
	}

	public class OptimizedBvhNode : IDisposable
	{
		internal IntPtr _native;

		internal OptimizedBvhNode(IntPtr native)
		{
			_native = native;
		}

		public OptimizedBvhNode()
		{
			_native = UnsafeNativeMethods.btOptimizedBvhNode_new();
		}

		public Vector3 AabbMaxOrg
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btOptimizedBvhNode_getAabbMaxOrg(_native, out value);
				return value;
			}
			set => UnsafeNativeMethods.btOptimizedBvhNode_setAabbMaxOrg(_native, ref value);
		}

		public Vector3 AabbMinOrg
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btOptimizedBvhNode_getAabbMinOrg(_native, out value);
				return value;
			}
			set => UnsafeNativeMethods.btOptimizedBvhNode_setAabbMinOrg(_native, ref value);
		}

		public int EscapeIndex
		{
			get => UnsafeNativeMethods.btOptimizedBvhNode_getEscapeIndex(_native);
			set => UnsafeNativeMethods.btOptimizedBvhNode_setEscapeIndex(_native, value);
		}

		public int SubPart
		{
			get => UnsafeNativeMethods.btOptimizedBvhNode_getSubPart(_native);
			set => UnsafeNativeMethods.btOptimizedBvhNode_setSubPart(_native, value);
		}

		public int TriangleIndex
		{
			get => UnsafeNativeMethods.btOptimizedBvhNode_getTriangleIndex(_native);
			set => UnsafeNativeMethods.btOptimizedBvhNode_setTriangleIndex(_native, value);
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
				UnsafeNativeMethods.btOptimizedBvhNode_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~OptimizedBvhNode()
		{
			Dispose(false);
		}
	}

	public abstract class NodeOverlapCallback : IDisposable
	{
		internal IntPtr _native;

		internal NodeOverlapCallback(IntPtr native)
		{
			_native = native;
		}

		public void ProcessNode(int subPart, int triangleIndex)
		{
			UnsafeNativeMethods.btNodeOverlapCallback_processNode(_native, subPart, triangleIndex);
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
				UnsafeNativeMethods.btNodeOverlapCallback_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~NodeOverlapCallback()
		{
			Dispose(false);
		}
	}

	public class QuantizedBvh : IDisposable
	{
		public enum TraversalMode
		{
			Stackless,
			StacklessCacheFriendly,
			Recursive
		}

		internal IntPtr _native;
		bool _preventDelete;

		internal QuantizedBvh(IntPtr native, bool preventDelete)
		{
			_native = native;
			_preventDelete = preventDelete;
		}

		public QuantizedBvh()
		{
			_native = UnsafeNativeMethods.btQuantizedBvh_new();
		}

		public void BuildInternal()
		{
			UnsafeNativeMethods.btQuantizedBvh_buildInternal(_native);
		}

		public uint CalculateSerializeBufferSize()
		{
			return UnsafeNativeMethods.btQuantizedBvh_calculateSerializeBufferSize(_native);
		}

		public int CalculateSerializeBufferSizeNew()
		{
			return UnsafeNativeMethods.btQuantizedBvh_calculateSerializeBufferSizeNew(_native);
		}

		public void DeSerializeDouble(IntPtr quantizedBvhDoubleData)
		{
			UnsafeNativeMethods.btQuantizedBvh_deSerializeDouble(_native, quantizedBvhDoubleData);
		}

		public void DeSerializeFloat(IntPtr quantizedBvhFloatData)
		{
			UnsafeNativeMethods.btQuantizedBvh_deSerializeFloat(_native, quantizedBvhFloatData);
		}
		/*
		public static QuantizedBvh DeSerializeInPlace(IntPtr alignedDataBuffer, uint dataBufferSize,
			bool swapEndian)
		{
			return UnsafeNativeMethods.btQuantizedBvh_deSerializeInPlace(alignedDataBuffer, dataBufferSize,
				swapEndian);
		}

		public void Quantize(unsigned short out, Vector3 point, int isMax)
		{
			btQuantizedBvh_quantize(_native, out._native, ref point, isMax);
		}

		public void QuantizeWithClamp(unsigned short out, Vector3 point2, int isMax)
		{
			btQuantizedBvh_quantizeWithClamp(_native, out._native, ref point2, isMax);
		}
		*/
		public void ReportAabbOverlappingNodex(NodeOverlapCallback nodeCallback,
			Vector3 aabbMin, Vector3 aabbMax)
		{
			UnsafeNativeMethods.btQuantizedBvh_reportAabbOverlappingNodex(_native, nodeCallback._native,
				ref aabbMin, ref aabbMax);
		}

		public void ReportBoxCastOverlappingNodex(NodeOverlapCallback nodeCallback,
			Vector3 raySource, Vector3 rayTarget, Vector3 aabbMin, Vector3 aabbMax)
		{
			UnsafeNativeMethods.btQuantizedBvh_reportBoxCastOverlappingNodex(_native, nodeCallback._native,
				ref raySource, ref rayTarget, ref aabbMin, ref aabbMax);
		}

		public void ReportRayOverlappingNodex(NodeOverlapCallback nodeCallback, Vector3 raySource,
			Vector3 rayTarget)
		{
			UnsafeNativeMethods.btQuantizedBvh_reportRayOverlappingNodex(_native, nodeCallback._native,
				ref raySource, ref rayTarget);
		}

		public bool Serialize(IntPtr alignedDataBuffer, uint dataBufferSize, bool swapEndian)
		{
			return UnsafeNativeMethods.btQuantizedBvh_serialize(_native, alignedDataBuffer, dataBufferSize,
				swapEndian);
		}

		public string Serialize(IntPtr dataBuffer, Serializer serializer)
		{
			return Marshal.PtrToStringAnsi(UnsafeNativeMethods.btQuantizedBvh_serialize2(_native, dataBuffer, serializer._native));
		}

		public void SetQuantizationValues(Vector3 bvhAabbMin, Vector3 bvhAabbMax,
			float quantizationMargin = 1.0f)
		{
			UnsafeNativeMethods.btQuantizedBvh_setQuantizationValues(_native, ref bvhAabbMin, ref bvhAabbMax,
				quantizationMargin);
		}

		public void SetTraversalMode(TraversalMode traversalMode)
		{
			UnsafeNativeMethods.btQuantizedBvh_setTraversalMode(_native, traversalMode);
		}
		/*
		public Vector3 UnQuantize(unsigned short vecIn)
		{
			Vector3 value;
			UnsafeNativeMethods.btQuantizedBvh_unQuantize(_native, vecIn._native, out value);
			return value;
		}
		*/
		public static uint AlignmentSerializationPadding => UnsafeNativeMethods.btQuantizedBvh_getAlignmentSerializationPadding();

		public bool IsQuantized => UnsafeNativeMethods.btQuantizedBvh_isQuantized(_native);
		/*
		public QuantizedNodeArray LeafNodeArray
		{
			get { return UnsafeNativeMethods.btQuantizedBvh_getLeafNodeArray(_native); }
		}

		public QuantizedNodeArray QuantizedNodeArray
		{
			get { return UnsafeNativeMethods.btQuantizedBvh_getQuantizedNodeArray(_native); }
		}

		public BvhSubtreeInfoArray SubtreeInfoArray
		{
			get { return UnsafeNativeMethods.btQuantizedBvh_getSubtreeInfoArray(_native); }
		}
		*/
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
					UnsafeNativeMethods.btQuantizedBvh_delete(_native);
				}
				_native = IntPtr.Zero;
			}
		}

		~QuantizedBvh()
		{
			Dispose(false);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	internal struct QuantizedBvhFloatData
	{
		public Vector3FloatData BvhAabbMin;
		public Vector3FloatData BvhAabbMax;
		public Vector3FloatData BvhQuantization;
		public int CurNodeIndex;
		public int UseQuantization;
		public int NumContiguousLeafNodes;
		public int NumQuantizedContiguousNodes;
		public IntPtr ContiguousNodesPtr;
		public IntPtr QuantizedContiguousNodesPtr;
		public IntPtr SubTreeInfoPtr;
		public int TraversalMode;
		public int NumSubtreeHeaders;

		public static int Offset(string fieldName) { return Marshal.OffsetOf(typeof(QuantizedBvhFloatData), fieldName).ToInt32(); }
	}
}
