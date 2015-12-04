using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using System.Diagnostics;

namespace BulletSharp.SoftBody
{
    public class AlignedFaceArrayDebugView
    {
        private AlignedFaceArray _array;

        public AlignedFaceArrayDebugView(AlignedFaceArray array)
        {
            _array = array;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public Face[] Items
        {
            get
            {
                int count = _array.Count;
                Face[] array = new Face[count];
                for (int i = 0; i < count; i++)
                {
                    array[i] = _array[i];
                }
                return array;
            }
        }
    }

    public class AlignedFaceArrayEnumerator : IEnumerator<Face>
    {
        int _i;
        int _count;
        AlignedFaceArray _array;

        public AlignedFaceArrayEnumerator(AlignedFaceArray array)
        {
            _array = array;
            _count = array.Count;
            _i = -1;
        }

        public Face Current
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

    [Serializable, DebuggerTypeProxy(typeof(AlignedFaceArrayDebugView)), DebuggerDisplay("Count = {Count}")]
    public class AlignedFaceArray : IList<Face>
    {
        private IntPtr _native;

        internal AlignedFaceArray(IntPtr native)
        {
            _native = native;
        }

        public int IndexOf(Face item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, Face item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public Face this[int index]
        {
            get
            {
                if ((uint)index >= (uint)Count)
                {
                    throw new ArgumentOutOfRangeException("index");
                }
                return new Face(btAlignedSoftBodyFaceArray_at(_native, index));
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void Add(Face item)
        {
            btAlignedSoftBodyFaceArray_push_back(_native, item._native);
        }

        public void Clear()
        {
            btAlignedSoftBodyFaceArray_resizeNoInitialize(_native, 0);
        }

        public bool Contains(Face item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(Face[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { return btAlignedSoftBodyFaceArray_size(_native); }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(Face item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<Face> GetEnumerator()
        {
            return new AlignedFaceArrayEnumerator(this);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return new AlignedFaceArrayEnumerator(this);
        }

        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btAlignedSoftBodyFaceArray_at(IntPtr obj, int n);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btAlignedSoftBodyFaceArray_push_back(IntPtr obj, IntPtr val);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btAlignedSoftBodyFaceArray_resizeNoInitialize(IntPtr obj, int newSize);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern int btAlignedSoftBodyFaceArray_size(IntPtr obj);
    }
}
