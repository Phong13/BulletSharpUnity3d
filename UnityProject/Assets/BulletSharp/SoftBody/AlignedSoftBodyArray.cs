using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using System.Diagnostics;

namespace BulletSharp.SoftBody
{
    public class AlignedSoftBodyArrayDebugView
    {
        private AlignedSoftBodyArray _array;

        public AlignedSoftBodyArrayDebugView(AlignedSoftBodyArray array)
        {
            _array = array;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public SoftBody[] Items
        {
            get
            {
                int count = _array.Count;
                SoftBody[] array = new SoftBody[count];
                for (int i = 0; i < count; i++)
                {
                    array[i] = _array[i];
                }
                return array;
            }
        }
    }

    public class AlignedSoftBodyArrayEnumerator : IEnumerator<SoftBody>
    {
        int _i;
        int _count;
        AlignedSoftBodyArray _array;

        public AlignedSoftBodyArrayEnumerator(AlignedSoftBodyArray array)
        {
            _array = array;
            _count = array.Count;
            _i = -1;
        }

        public SoftBody Current
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

    [Serializable, DebuggerTypeProxy(typeof(AlignedSoftBodyArrayDebugView)), DebuggerDisplay("Count = {Count}")]
    public class AlignedSoftBodyArray : IList<SoftBody>
    {
        internal IntPtr _native;

        internal AlignedSoftBodyArray(IntPtr native)
        {
            _native = native;
        }

        public int IndexOf(SoftBody item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, SoftBody item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public SoftBody this[int index]
        {
            get
            {
                if ((uint)index >= (uint)Count)
                {
                    throw new ArgumentOutOfRangeException("index");
                }
                return CollisionObject.GetManaged(btAlignedSoftBodyArray_at(_native, index)) as SoftBody;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void Add(SoftBody item)
        {
            btAlignedSoftBodyArray_push_back(_native, item._native);
        }

        public void Clear()
        {
            btAlignedSoftBodyArray_resizeNoInitialize(_native, 0);
        }

        public bool Contains(SoftBody item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(SoftBody[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { return btAlignedSoftBodyArray_size(_native); }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(SoftBody item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<SoftBody> GetEnumerator()
        {
            return new AlignedSoftBodyArrayEnumerator(this);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return new AlignedSoftBodyArrayEnumerator(this);
        }

        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btAlignedSoftBodyArray_at(IntPtr obj, int n);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btAlignedSoftBodyArray_push_back(IntPtr obj, IntPtr val);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btAlignedSoftBodyArray_resizeNoInitialize(IntPtr obj, int newSize);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern int btAlignedSoftBodyArray_size(IntPtr obj);
    }
}
