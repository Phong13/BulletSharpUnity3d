using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace BulletSharp.SoftBody
{
	public class AlignedNodeArrayDebugView
	{
		private AlignedNodeArray _array;

		public AlignedNodeArrayDebugView(AlignedNodeArray array)
		{
			_array = array;
		}

		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public Node[] Items
		{
			get
			{
				int count = _array.Count;
				var array = new Node[count];
				for (int i = 0; i < count; i++)
				{
					array[i] = _array[i];
				}
				return array;
			}
		}
	}

	public class AlignedNodeArrayEnumerator : IEnumerator<Node>
	{
		private int _i;
		private int _count;
		private AlignedNodeArray _array;

		public AlignedNodeArrayEnumerator(AlignedNodeArray array)
		{
			_array = array;
			_count = array.Count;
			_i = -1;
		}

		public Node Current => _array[_i];

		public void Dispose()
		{
		}

		object System.Collections.IEnumerator.Current => _array[_i];

		public bool MoveNext()
		{
			_i++;
			return _i != _count;
		}

		public void Reset()
		{
			_i = 0;
		}
	}

	[Serializable, DebuggerTypeProxy(typeof(AlignedNodeArrayDebugView)), DebuggerDisplay("Count = {Count}")]
	public class AlignedNodeArray : IList<Node>
	{
		private IntPtr _native;

		internal AlignedNodeArray(IntPtr native)
		{
			_native = native;
		}

		public int IndexOf(Node item)
		{
			return UnsafeNativeMethods.btAlignedObjectArray_btSoftBody_Node_index_of(_native, item.Native);
		}

		public void Insert(int index, Node item)
		{
			throw new NotImplementedException();
		}

		public void RemoveAt(int index)
		{
			throw new NotImplementedException();
		}

		public Node this[int index]
		{
			get
			{
				if ((uint)index >= (uint)Count)
				{
					throw new ArgumentOutOfRangeException(nameof(index));
				}
				return new Node(UnsafeNativeMethods.btAlignedObjectArray_btSoftBody_Node_at(_native, index));
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public void Add(Node item)
		{
			UnsafeNativeMethods.btAlignedObjectArray_btSoftBody_Node_push_back(_native, item.Native);
		}

		public void Clear()
		{
			UnsafeNativeMethods.btAlignedObjectArray_btSoftBody_Node_resizeNoInitialize(_native, 0);
		}

		public bool Contains(Node item)
		{
			throw new NotImplementedException();
		}

		public void CopyTo(Node[] array, int arrayIndex)
		{
			throw new NotImplementedException();
		}

		public int Count => UnsafeNativeMethods.btAlignedObjectArray_btSoftBody_Node_size(_native);

		public bool IsReadOnly => false;

		public bool Remove(Node item)
		{
			throw new NotImplementedException();
		}

		public IEnumerator<Node> GetEnumerator()
		{
			return new AlignedNodeArrayEnumerator(this);
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return new AlignedNodeArrayEnumerator(this);
		}
	}
}
