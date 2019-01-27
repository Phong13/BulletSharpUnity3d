using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using System.Diagnostics;

namespace BulletSharp
{
    public class AlignedManifoldArrayDebugView
    {
        private readonly AlignedManifoldArray _array;

        public AlignedManifoldArrayDebugView(AlignedManifoldArray array)
        {
            _array = array;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public PersistentManifold[] Items
        {
            get
            {
                PersistentManifold[] array = new PersistentManifold[_array.Count];
                for (int i = 0; i < _array.Count; i++)
                {
                    array[i] = _array[i];
                }
                return array;
            }
        }
    }

    public class AlignedManifoldArrayEnumerator : IEnumerator<PersistentManifold>
    {
        int _i;
        readonly int _count;
        readonly AlignedManifoldArray _array;

        public AlignedManifoldArrayEnumerator(AlignedManifoldArray array)
        {
            _array = array;
            _count = array.Count;
            _i = -1;
        }

        public PersistentManifold Current
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

    [Serializable, DebuggerTypeProxy(typeof(AlignedManifoldArrayDebugView)), DebuggerDisplay("Count = {Count}")]
    public class AlignedManifoldArray : IList<PersistentManifold>, IDisposable
    {
        internal IntPtr _native;

        internal AlignedManifoldArray(IntPtr native)
        {
            _native = native;
        }

        public AlignedManifoldArray()
        {
            _native = btAlignedManifoldArray_new();
        }

        public int IndexOf(PersistentManifold item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, PersistentManifold item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public PersistentManifold this[int index]
        {
            get
            {
                if ((uint)index >= (uint)Count)
                {
                    throw new ArgumentOutOfRangeException("index");
                }
                return new PersistentManifold(btAlignedManifoldArray_at(_native, index), true);
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void Add(PersistentManifold item)
        {
            btAlignedManifoldArray_push_back(_native, item._native);
        }

        public void Clear()
        {
            btAlignedManifoldArray_resizeNoInitialize(_native, 0);
        }

        public bool Contains(PersistentManifold item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(PersistentManifold[] array, int arrayIndex)
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
            get { return btAlignedManifoldArray_size(_native); }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(PersistentManifold item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<PersistentManifold> GetEnumerator()
        {
            return new AlignedManifoldArrayEnumerator(this);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return new AlignedManifoldArrayEnumerator(this);
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
                btAlignedManifoldArray_delete(_native);
                _native = IntPtr.Zero;
            }
        }

        ~AlignedManifoldArray()
        {
            Dispose(false);
        }

        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btAlignedManifoldArray_new();
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern int btAlignedManifoldArray_size(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btAlignedManifoldArray_at(IntPtr obj, int n);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btAlignedManifoldArray_push_back(IntPtr obj, IntPtr val);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btAlignedManifoldArray_resizeNoInitialize(IntPtr obj, int newSize);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btAlignedManifoldArray_delete(IntPtr obj);
    }
}
