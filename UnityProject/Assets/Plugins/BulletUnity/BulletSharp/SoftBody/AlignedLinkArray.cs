using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using System.Diagnostics;

namespace BulletSharp.SoftBody
{
    public class AlignedLinkArrayDebugView
    {
        private AlignedLinkArray _array;

        public AlignedLinkArrayDebugView(AlignedLinkArray array)
        {
            _array = array;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public Link[] Items
        {
            get
            {
                int count = _array.Count;
                Link[] array = new Link[count];
                for (int i = 0; i < count; i++)
                {
                    array[i] = _array[i];
                }
                return array;
            }
        }
    }

    public class AlignedLinkArrayEnumerator : IEnumerator<Link>
    {
        int _i;
        int _count;
        AlignedLinkArray _array;

        public AlignedLinkArrayEnumerator(AlignedLinkArray array)
        {
            _array = array;
            _count = array.Count;
            _i = -1;
        }

        public Link Current
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

    [Serializable, DebuggerTypeProxy(typeof(AlignedLinkArrayDebugView)), DebuggerDisplay("Count = {Count}")]
    public class AlignedLinkArray : IList<Link>
    {
        private IntPtr _native;

        internal AlignedLinkArray(IntPtr native)
        {
            _native = native;
        }

        public int IndexOf(Link item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, Link item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public Link this[int index]
        {
            get
            {
                if ((uint)index >= (uint)Count)
                {
                    throw new ArgumentOutOfRangeException("index");
                }
                return new Link(btAlignedSoftBodyLinkArray_at(_native, index));
            }
            set
            {
                btAlignedSoftBodyLinkArray_set(_native, value._native, index);
            }
        }

        public void Add(Link item)
        {
            btAlignedSoftBodyLinkArray_push_back(_native, item._native);
        }

        public void Clear()
        {
            btAlignedSoftBodyLinkArray_resizeNoInitialize(_native, 0);
        }

        public bool Contains(Link item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(Link[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }
            if (arrayIndex < 0)
            {
                throw new ArgumentOutOfRangeException("arrayIndex");
            }
            int count = Count;
            if (array.Length - arrayIndex < count)
            {
                throw new ArgumentException("The number of elements in the source is greater than the available space from arrayIndex to the end of the destination array.");
            }

            for (int i = 0; i < count; i++)
            {
                array.SetValue(new Link(btAlignedSoftBodyLinkArray_at(_native, i)), i + arrayIndex);
            }
        }

        public int Count
        {
            get { return btAlignedSoftBodyLinkArray_size(_native); }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(Link item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<Link> GetEnumerator()
        {
            return new AlignedLinkArrayEnumerator(this);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return new AlignedLinkArrayEnumerator(this);
        }

        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btAlignedSoftBodyLinkArray_at(IntPtr obj, int n);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btAlignedSoftBodyLinkArray_push_back(IntPtr obj, IntPtr val);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btAlignedSoftBodyLinkArray_resizeNoInitialize(IntPtr obj, int newSize);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btAlignedSoftBodyLinkArray_set(IntPtr obj, IntPtr val, int index);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern int btAlignedSoftBodyLinkArray_size(IntPtr obj);
    }
}
