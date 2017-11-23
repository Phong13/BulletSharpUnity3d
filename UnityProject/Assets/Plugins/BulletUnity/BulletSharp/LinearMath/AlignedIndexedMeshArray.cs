using System;
using System.Collections.Generic;
using System.Diagnostics;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class AlignedIndexedMeshArrayDebugView
	{
		private readonly AlignedIndexedMeshArray _array;

		public AlignedIndexedMeshArrayDebugView(AlignedIndexedMeshArray array)
		{
			_array = array;
		}

		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public IndexedMesh[] Items
		{
			get
			{
				var array = new IndexedMesh[_array.Count];
				for (int i = 0; i < _array.Count; i++)
				{
					array[i] = _array[i];
				}
				return array;
			}
		}
	}

	public class AlignedIndexedMeshArrayEnumerator : IEnumerator<IndexedMesh>
	{
		private int _i;
		private readonly int _count;
		private readonly AlignedIndexedMeshArray _array;

		public AlignedIndexedMeshArrayEnumerator(AlignedIndexedMeshArray array)
		{
			_array = array;
			_count = array.Count;
			_i = -1;
		}

		public IndexedMesh Current => _array[_i];

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

	[Serializable, DebuggerTypeProxy(typeof(AlignedIndexedMeshArrayDebugView)), DebuggerDisplay("Count = {Count}")]
	public class AlignedIndexedMeshArray : IList<IndexedMesh>
	{
		private IntPtr _native;

		internal AlignedIndexedMeshArray(IntPtr native)
		{
			_native = native;
		}

		public int IndexOf(IndexedMesh item)
		{
			throw new NotImplementedException();
		}

		public void Insert(int index, IndexedMesh item)
		{
			throw new NotImplementedException();
		}

		public void RemoveAt(int index)
		{
			throw new NotImplementedException();
		}

		public IndexedMesh this[int index]
		{
			get
			{
				if ((uint)index >= (uint)Count)
				{
					throw new ArgumentOutOfRangeException(nameof(index));
				}
				return new IndexedMesh(btAlignedObjectArray_btIndexedMesh_at(_native, index), true);
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public void Add(IndexedMesh item)
		{
			btAlignedObjectArray_btIndexedMesh_push_back(_native, item.Native);
		}

		public void Clear()
		{
			btAlignedObjectArray_btIndexedMesh_resizeNoInitialize(_native, 0);
		}

		public bool Contains(IndexedMesh item)
		{
			throw new NotImplementedException();
		}

		public void CopyTo(IndexedMesh[] array, int arrayIndex)
		{
			if (array == null)
				throw new ArgumentNullException(nameof(array));

			if (arrayIndex < 0)
				throw new ArgumentOutOfRangeException(nameof(array));

			int count = Count;
			if (arrayIndex + count > array.Length)
				throw new ArgumentException("Array too small.", "array");

			for (int i = 0; i < count; i++)
			{
				array[arrayIndex + i] = this[i];
			}
		}

		public int Count => btAlignedObjectArray_btIndexedMesh_size(_native);

		public bool IsReadOnly => false;

		public bool Remove(IndexedMesh item)
		{
			throw new NotImplementedException();
		}

		public IEnumerator<IndexedMesh> GetEnumerator()
		{
			return new AlignedIndexedMeshArrayEnumerator(this);
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return new AlignedIndexedMeshArrayEnumerator(this);
		}
	}
}
