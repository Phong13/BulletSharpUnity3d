using BulletSharp.Math;
using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace BulletSharp
{
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(Vector3ListDebugView))]
	public class AlignedVector3Array : IList<Vector3>, IDisposable
	{
		internal IntPtr _native;
		private bool _preventDelete;

		internal AlignedVector3Array(IntPtr native)
		{
			_native = native;
			_preventDelete = true;
		}

		public AlignedVector3Array()
		{
			_native = UnsafeNativeMethods.btAlignedObjectArray_btVector3_new();
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
					UnsafeNativeMethods.btAlignedObjectArray_btVector3_delete(_native);
				}
				_native = IntPtr.Zero;
			}
		}

		~AlignedVector3Array()
		{
			Dispose(false);
		}

		public int IndexOf(Vector3 item)
		{
			throw new NotImplementedException();
		}

		public void Insert(int index, Vector3 item)
		{
			throw new NotImplementedException();
		}

		public void RemoveAt(int index)
		{
			throw new NotImplementedException();
		}

		public Vector3 this[int index]
		{
			get
			{
				if ((uint)index >= (uint)Count)
				{
					throw new ArgumentOutOfRangeException(nameof(index));
				}
				Vector3 value;
				UnsafeNativeMethods.btAlignedObjectArray_btVector3_at(_native, index, out value);
				return value;
			}
			set
			{
				if ((uint)index >= (uint)Count)
				{
					throw new ArgumentOutOfRangeException(nameof(index));
				}
				UnsafeNativeMethods.btAlignedObjectArray_btVector3_set(_native, index, ref value);
			}
		}

		public void Add(Vector3 item)
		{
			UnsafeNativeMethods.btAlignedObjectArray_btVector3_push_back(_native, ref item);
		}

		public void Clear()
		{
			throw new NotImplementedException();
		}

		public bool Contains(Vector3 item)
		{
			throw new NotImplementedException();
		}

		public void CopyTo(Vector3[] array, int arrayIndex)
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

		public int Count => UnsafeNativeMethods.btAlignedObjectArray_btVector3_size(_native);

		public bool IsReadOnly => false;

		public bool Remove(Vector3 item)
		{
			throw new NotImplementedException();
		}

		public IEnumerator<Vector3> GetEnumerator()
		{
			return new Vector3ArrayEnumerator(this);
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return new Vector3ArrayEnumerator(this);
		}
	}
}
