using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using System.Diagnostics;

namespace BulletSharp.SoftBody
{
    public class AlignedTetraArrayDebugView
    {
        private AlignedTetraArray _array;

        public AlignedTetraArrayDebugView(AlignedTetraArray array)
        {
            _array = array;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public Tetra[] Items
        {
            get
            {
                int count = _array.Count;
                Tetra[] array = new Tetra[count];
                for (int i = 0; i < count; i++)
                {
                    array[i] = _array[i];
                }
                return array;
            }
        }
    }

    public class AlignedTetraArrayEnumerator : IEnumerator<Tetra>
    {
        int _i;
        int _count;
        AlignedTetraArray _array;

        public AlignedTetraArrayEnumerator(AlignedTetraArray array)
        {
            _array = array;
            _count = array.Count;
            _i = -1;
        }

        public Tetra Current
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

    [Serializable, DebuggerTypeProxy(typeof(AlignedTetraArrayDebugView)), DebuggerDisplay("Count = {Count}")]
    public class AlignedTetraArray : IList<Tetra>
    {
        private IntPtr _native;

        internal AlignedTetraArray(IntPtr native)
        {
            _native = native;
        }

        public int IndexOf(Tetra item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, Tetra item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public Tetra this[int index]
        {
            get
            {
                if ((uint)index >= (uint)Count)
                {
                    throw new ArgumentOutOfRangeException("index");
                }
                return new Tetra(btAlignedSoftBodyTetraArray_at(_native, index));
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void Add(Tetra item)
        {
            btAlignedSoftBodyTetraArray_push_back(_native, item._native);
        }

        public void Clear()
        {
            btAlignedSoftBodyTetraArray_resizeNoInitialize(_native, 0);
        }

        public bool Contains(Tetra item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(Tetra[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { return btAlignedSoftBodyTetraArray_size(_native); }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(Tetra item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<Tetra> GetEnumerator()
        {
            return new AlignedTetraArrayEnumerator(this);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return new AlignedTetraArrayEnumerator(this);
        }

        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btAlignedSoftBodyTetraArray_at(IntPtr obj, int n);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btAlignedSoftBodyTetraArray_push_back(IntPtr obj, IntPtr val);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btAlignedSoftBodyTetraArray_resizeNoInitialize(IntPtr obj, int newSize);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern int btAlignedSoftBodyTetraArray_size(IntPtr obj);
    }
}
