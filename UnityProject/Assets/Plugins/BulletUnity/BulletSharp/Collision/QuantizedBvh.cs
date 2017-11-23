using System;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.Math;
using static BulletSharp.UnsafeNativeMethods;

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
			_native = btQuantizedBvhNode_new();
		}

		public int EscapeIndex => btQuantizedBvhNode_getEscapeIndex(_native);

		public int EscapeIndexOrTriangleIndex
		{
			get => btQuantizedBvhNode_getEscapeIndexOrTriangleIndex(_native);
			set => btQuantizedBvhNode_setEscapeIndexOrTriangleIndex(_native, value);
		}

		public bool IsLeafNode => btQuantizedBvhNode_isLeafNode(_native);

		public int PartId => btQuantizedBvhNode_getPartId(_native);
		/*
		public UShortArray QuantizedAabbMax
		{
			get { return btQuantizedBvhNode_getQuantizedAabbMax(_native); }
		}

		public UShortArray QuantizedAabbMin
		{
			get { return btQuantizedBvhNode_getQuantizedAabbMin(_native); }
		}
		*/
		public int TriangleIndex => btQuantizedBvhNode_getTriangleIndex(_native);

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (_native != IntPtr.Zero)
			{
				btQuantizedBvhNode_delete(_native);
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
			_native = btOptimizedBvhNode_new();
		}

		public Vector3 AabbMaxOrg
		{
			get
			{
				Vector3 value;
				btOptimizedBvhNode_getAabbMaxOrg(_native, out value);
				return value;
			}
			set => btOptimizedBvhNode_setAabbMaxOrg(_native, ref value);
		}

		public Vector3 AabbMinOrg
		{
			get
			{
				Vector3 value;
				btOptimizedBvhNode_getAabbMinOrg(_native, out value);
				return value;
			}
			set => btOptimizedBvhNode_setAabbMinOrg(_native, ref value);
		}

		public int EscapeIndex
		{
			get => btOptimizedBvhNode_getEscapeIndex(_native);
			set => btOptimizedBvhNode_setEscapeIndex(_native, value);
		}

		public int SubPart
		{
			get => btOptimizedBvhNode_getSubPart(_native);
			set => btOptimizedBvhNode_setSubPart(_native, value);
		}

		public int TriangleIndex
		{
			get => btOptimizedBvhNode_getTriangleIndex(_native);
			set => btOptimizedBvhNode_setTriangleIndex(_native, value);
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
				btOptimizedBvhNode_delete(_native);
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
			btNodeOverlapCallback_processNode(_native, subPart, triangleIndex);
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
				btNodeOverlapCallback_delete(_native);
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
			_native = btQuantizedBvh_new();
		}

		public void BuildInternal()
		{
			btQuantizedBvh_buildInternal(_native);
		}

		public uint CalculateSerializeBufferSize()
		{
			return btQuantizedBvh_calculateSerializeBufferSize(_native);
		}

		public int CalculateSerializeBufferSizeNew()
		{
			return btQuantizedBvh_calculateSerializeBufferSizeNew(_native);
		}

		public void DeSerializeDouble(IntPtr quantizedBvhDoubleData)
		{
			btQuantizedBvh_deSerializeDouble(_native, quantizedBvhDoubleData);
		}

		public void DeSerializeFloat(IntPtr quantizedBvhFloatData)
		{
			btQuantizedBvh_deSerializeFloat(_native, quantizedBvhFloatData);
		}
		/*
		public static QuantizedBvh DeSerializeInPlace(IntPtr alignedDataBuffer, uint dataBufferSize,
			bool swapEndian)
		{
			return btQuantizedBvh_deSerializeInPlace(alignedDataBuffer, dataBufferSize,
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
			btQuantizedBvh_reportAabbOverlappingNodex(_native, nodeCallback._native,
				ref aabbMin, ref aabbMax);
		}

		public void ReportBoxCastOverlappingNodex(NodeOverlapCallback nodeCallback,
			Vector3 raySource, Vector3 rayTarget, Vector3 aabbMin, Vector3 aabbMax)
		{
			btQuantizedBvh_reportBoxCastOverlappingNodex(_native, nodeCallback._native,
				ref raySource, ref rayTarget, ref aabbMin, ref aabbMax);
		}

		public void ReportRayOverlappingNodex(NodeOverlapCallback nodeCallback, Vector3 raySource,
			Vector3 rayTarget)
		{
			btQuantizedBvh_reportRayOverlappingNodex(_native, nodeCallback._native,
				ref raySource, ref rayTarget);
		}

		public bool Serialize(IntPtr alignedDataBuffer, uint dataBufferSize, bool swapEndian)
		{
			return btQuantizedBvh_serialize(_native, alignedDataBuffer, dataBufferSize,
				swapEndian);
		}

		public string Serialize(IntPtr dataBuffer, Serializer serializer)
		{
			return Marshal.PtrToStringAnsi(btQuantizedBvh_serialize2(_native, dataBuffer, serializer._native));
		}

		public void SetQuantizationValues(Vector3 bvhAabbMin, Vector3 bvhAabbMax,
			float quantizationMargin = 1.0f)
		{
			btQuantizedBvh_setQuantizationValues(_native, ref bvhAabbMin, ref bvhAabbMax,
				quantizationMargin);
		}

		public void SetTraversalMode(TraversalMode traversalMode)
		{
			btQuantizedBvh_setTraversalMode(_native, traversalMode);
		}
		/*
		public Vector3 UnQuantize(unsigned short vecIn)
		{
			Vector3 value;
			btQuantizedBvh_unQuantize(_native, vecIn._native, out value);
			return value;
		}
		*/
		public static uint AlignmentSerializationPadding => btQuantizedBvh_getAlignmentSerializationPadding();

		public bool IsQuantized => btQuantizedBvh_isQuantized(_native);
		/*
		public QuantizedNodeArray LeafNodeArray
		{
			get { return btQuantizedBvh_getLeafNodeArray(_native); }
		}

		public QuantizedNodeArray QuantizedNodeArray
		{
			get { return btQuantizedBvh_getQuantizedNodeArray(_native); }
		}

		public BvhSubtreeInfoArray SubtreeInfoArray
		{
			get { return btQuantizedBvh_getSubtreeInfoArray(_native); }
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
					btQuantizedBvh_delete(_native);
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
