using System;
using System.Collections.Generic;
using System.Diagnostics;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp.SoftBody
{
	public class AlignedMaterialArrayDebugView
	{
		private AlignedMaterialArray _array;

		public AlignedMaterialArrayDebugView(AlignedMaterialArray array)
		{
			_array = array;
		}

		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public Material[] Items
		{
			get
			{
				int count = _array.Count;
				var array = new Material[count];
				for (int i = 0; i < count; i++)
				{
					array[i] = _array[i];
				}
				return array;
			}
		}
	}

	public class AlignedMaterialArrayEnumerator : IEnumerator<Material>
	{
		private int _i;
		private int _count;
		private AlignedMaterialArray _array;

		public AlignedMaterialArrayEnumerator(AlignedMaterialArray array)
		{
			_array = array;
			_count = array.Count;
			_i = -1;
		}

		public Material Current => _array[_i];

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

	[Serializable, DebuggerTypeProxy(typeof(AlignedMaterialArrayDebugView)), DebuggerDisplay("Count = {Count}")]
	public class AlignedMaterialArray : IList<Material>
	{
		private IntPtr _native;

		internal AlignedMaterialArray(IntPtr native, bool preventDelete = false)
		{
			_native = native;
		}

		public int IndexOf(Material item)
		{
			throw new NotImplementedException();
		}

		public void Insert(int index, Material item)
		{
			throw new NotImplementedException();
		}

		public void RemoveAt(int index)
		{
			throw new NotImplementedException();
		}

		public Material this[int index]
		{
			get
			{
				if ((uint)index >= (uint)Count)
				{
					throw new ArgumentOutOfRangeException(nameof(index));
				}
				return new Material(btAlignedObjectArray_btSoftBody_MaterialPtr_at(_native, index));
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public void Add(Material item)
		{
			btAlignedObjectArray_btSoftBody_MaterialPtr_push_back(_native, item.Native);
		}

		public void Clear()
		{
			btAlignedObjectArray_btSoftBody_MaterialPtr_resizeNoInitialize(_native, 0);
		}

		public bool Contains(Material item)
		{
			throw new NotImplementedException();
		}

		public void CopyTo(Material[] array, int arrayIndex)
		{
			throw new NotImplementedException();
		}

		public int Count => btAlignedObjectArray_btSoftBody_MaterialPtr_size(_native);

		public bool IsReadOnly => false;

		public bool Remove(Material item)
		{
			throw new NotImplementedException();
		}

		public IEnumerator<Material> GetEnumerator()
		{
			return new AlignedMaterialArrayEnumerator(this);
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return new AlignedMaterialArrayEnumerator(this);
		}
	}
}
