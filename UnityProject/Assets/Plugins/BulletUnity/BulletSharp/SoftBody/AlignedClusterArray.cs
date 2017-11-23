using System;
using System.Collections.Generic;
using System.Diagnostics;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp.SoftBody
{
	public class AlignedClusterArrayDebugView
	{
		private AlignedClusterArray _array;

		public AlignedClusterArrayDebugView(AlignedClusterArray array)
		{
			_array = array;
		}

		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public Cluster[] Items
		{
			get
			{
				int count = _array.Count;
				var array = new Cluster[count];
				for (int i = 0; i < count; i++)
				{
					array[i] = _array[i];
				}
				return array;
			}
		}
	}

	public class AlignedClusterArrayEnumerator : IEnumerator<Cluster>
	{
		private int _i;
		private int _count;
		private AlignedClusterArray _array;

		public AlignedClusterArrayEnumerator(AlignedClusterArray array)
		{
			_array = array;
			_count = array.Count;
			_i = -1;
		}

		public Cluster Current => _array[_i];

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

	[Serializable, DebuggerTypeProxy(typeof(AlignedClusterArrayDebugView)), DebuggerDisplay("Count = {Count}")]
	public class AlignedClusterArray : IList<Cluster>
	{
		private IntPtr _native;

		internal AlignedClusterArray(IntPtr native)
		{
			_native = native;
		}

		public int IndexOf(Cluster item)
		{
			throw new NotImplementedException();
		}

		public void Insert(int index, Cluster item)
		{
			throw new NotImplementedException();
		}

		public void RemoveAt(int index)
		{
			throw new NotImplementedException();
		}

		public Cluster this[int index]
		{
			get
			{
				if ((uint)index >= (uint)Count)
				{
					throw new ArgumentOutOfRangeException(nameof(index));
				}
				return new Cluster(btAlignedObjectArray_btSoftBody_ClusterPtr_at(_native, index));
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public void Add(Cluster item)
		{
			btAlignedObjectArray_btSoftBody_ClusterPtr_push_back(_native, item.Native);
		}

		public void Clear()
		{
			btAlignedObjectArray_btSoftBody_ClusterPtr_resizeNoInitialize(_native, 0);
		}

		public bool Contains(Cluster item)
		{
			throw new NotImplementedException();
		}

		public void CopyTo(Cluster[] array, int arrayIndex)
		{
			throw new NotImplementedException();
		}

		public int Count => btAlignedObjectArray_btSoftBody_ClusterPtr_size(_native);

		public bool IsReadOnly => false;

		public bool Remove(Cluster item)
		{
			throw new NotImplementedException();
		}

		public IEnumerator<Cluster> GetEnumerator()
		{
			return new AlignedClusterArrayEnumerator(this);
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return new AlignedClusterArrayEnumerator(this);
		}
	}
}
