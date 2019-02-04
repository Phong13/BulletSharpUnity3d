using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using System.Diagnostics;

namespace BulletSharp.SoftBody
{
    public class AlignedJointArrayDebugView
    {
        private AlignedJointArray _array;

        public AlignedJointArrayDebugView(AlignedJointArray array)
        {
            _array = array;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public Joint[] Items
        {
            get
            {
                int count = _array.Count;
                Joint[] array = new Joint[count];
                for (int i = 0; i < count; i++)
                {
                    array[i] = _array[i];
                }
                return array;
            }
        }
    }

    public class AlignedJointArrayEnumerator : IEnumerator<Joint>
    {
        int _i;
        int _count;
        AlignedJointArray _array;

        public AlignedJointArrayEnumerator(AlignedJointArray array)
        {
            _array = array;
            _count = array.Count;
            _i = -1;
        }

        public Joint Current
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

    [Serializable, DebuggerTypeProxy(typeof(AlignedJointArrayDebugView)), DebuggerDisplay("Count = {Count}")]
    public class AlignedJointArray : IList<Joint>
    {
        private IntPtr _native;

        internal AlignedJointArray(IntPtr native)
        {
            _native = native;
        }

        public int IndexOf(Joint item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, Joint item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public Joint this[int index]
        {
            get
            {
                if ((uint)index >= (uint)Count)
                {
                    throw new ArgumentOutOfRangeException("index");
                }
                return Joint.GetManaged(btAlignedSoftBodyJointArray_at(_native, index));
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void Add(Joint item)
        {
            btAlignedSoftBodyJointArray_push_back(_native, item._native);
        }

        public void Clear()
        {
            btAlignedSoftBodyJointArray_resizeNoInitialize(_native, 0);
        }

        public bool Contains(Joint item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(Joint[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { return btAlignedSoftBodyJointArray_size(_native); }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(Joint item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<Joint> GetEnumerator()
        {
            return new AlignedJointArrayEnumerator(this);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return new AlignedJointArrayEnumerator(this);
        }

        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btAlignedSoftBodyJointArray_at(IntPtr obj, int n);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btAlignedSoftBodyJointArray_push_back(IntPtr obj, IntPtr val);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btAlignedSoftBodyJointArray_resizeNoInitialize(IntPtr obj, int newSize);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern int btAlignedSoftBodyJointArray_size(IntPtr obj);
    }
}
