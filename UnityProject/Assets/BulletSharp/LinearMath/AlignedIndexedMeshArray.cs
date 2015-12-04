using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using System.Diagnostics;

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
                IndexedMesh[] array = new IndexedMesh[_array.Count];
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
        int _i;
        readonly int _count;
        readonly AlignedIndexedMeshArray _array;

        public AlignedIndexedMeshArrayEnumerator(AlignedIndexedMeshArray array)
        {
            _array = array;
            _count = array.Count;
            _i = -1;
        }

        public IndexedMesh Current
        {
            get { return _array[_i]; }
        }

        public void Dispose()
        {
        }

        object System.Collections.IEnumerator.Current
        {
            get { return _array[_i]; }
        }

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
                    throw new ArgumentOutOfRangeException("index");
                }
                return new IndexedMesh(btAlignedIndexedMeshArray_at(_native, index), true);
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void Add(IndexedMesh item)
        {
            btAlignedIndexedMeshArray_push_back(_native, item._native);
        }

        public void Clear()
        {
            btAlignedIndexedMeshArray_resizeNoInitialize(_native, 0);
        }

        public bool Contains(IndexedMesh item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(IndexedMesh[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException("array");

            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException("array");

            int count = Count;
            if (arrayIndex + count > array.Length)
                throw new ArgumentException("Array too small.", "array");

            for (int i = 0; i < count; i++)
            {
                array[arrayIndex + i] = this[i];
            }
        }

        public int Count
        {
            get { return btAlignedIndexedMeshArray_size(_native); }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

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

        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btAlignedIndexedMeshArray_at(IntPtr obj, int n);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btAlignedIndexedMeshArray_push_back(IntPtr obj, IntPtr val);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btAlignedIndexedMeshArray_resizeNoInitialize(IntPtr obj, int newSize);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern int btAlignedIndexedMeshArray_size(IntPtr obj);
    }
}
