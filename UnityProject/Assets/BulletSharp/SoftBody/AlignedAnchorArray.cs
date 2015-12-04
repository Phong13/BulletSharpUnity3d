using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using System.Diagnostics;

namespace BulletSharp.SoftBody
{
    public class AlignedAnchorArrayDebugView
    {
        private AlignedAnchorArray _array;

        public AlignedAnchorArrayDebugView(AlignedAnchorArray array)
        {
            _array = array;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public Anchor[] Items
        {
            get
            {
                int count = _array.Count;
                Anchor[] array = new Anchor[count];
                for (int i = 0; i < count; i++)
                {
                    array[i] = _array[i];
                }
                return array;
            }
        }
    }

    public class AlignedAnchorArrayEnumerator : IEnumerator<Anchor>
    {
        int _i;
        int _count;
        AlignedAnchorArray _array;

        public AlignedAnchorArrayEnumerator(AlignedAnchorArray array)
        {
            _array = array;
            _count = array.Count;
            _i = -1;
        }

        public Anchor Current
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

    [Serializable, DebuggerTypeProxy(typeof(AlignedAnchorArrayDebugView)), DebuggerDisplay("Count = {Count}")]
    public class AlignedAnchorArray : IList<Anchor>
    {
        private IntPtr _native;

        internal AlignedAnchorArray(IntPtr native)
        {
            _native = native;
        }

        public int IndexOf(Anchor item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, Anchor item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public Anchor this[int index]
        {
            get
            {
                if ((uint)index >= (uint)Count)
                {
                    throw new ArgumentOutOfRangeException("index");
                }
                return new Anchor(btAlignedSoftBodyAnchorArray_at(_native, index));
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void Add(Anchor item)
        {
            btAlignedSoftBodyAnchorArray_push_back(_native, item._native);
        }

        public void Clear()
        {
            btAlignedSoftBodyAnchorArray_resizeNoInitialize(_native, 0);
        }

        public bool Contains(Anchor item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(Anchor[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { return btAlignedSoftBodyAnchorArray_size(_native); }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(Anchor item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<Anchor> GetEnumerator()
        {
            return new AlignedAnchorArrayEnumerator(this);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return new AlignedAnchorArrayEnumerator(this);
        }

        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btAlignedSoftBodyAnchorArray_at(IntPtr obj, int n);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btAlignedSoftBodyAnchorArray_push_back(IntPtr obj, IntPtr val);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btAlignedSoftBodyAnchorArray_resizeNoInitialize(IntPtr obj, int newSize);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern int btAlignedSoftBodyAnchorArray_size(IntPtr obj);
    }
}
