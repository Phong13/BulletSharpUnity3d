using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using System.Diagnostics;

namespace BulletSharp.SoftBody
{
    public class AlignedNodeArrayDebugView
    {
        private AlignedNodeArray _array;

        public AlignedNodeArrayDebugView(AlignedNodeArray array)
        {
            _array = array;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public Node[] Items
        {
            get
            {
                int count = _array.Count;
                Node[] array = new Node[count];
                for (int i = 0; i < count; i++)
                {
                    array[i] = _array[i];
                }
                return array;
            }
        }
    }

    public class AlignedNodeArrayEnumerator : IEnumerator<Node>
    {
        int _i;
        int _count;
        AlignedNodeArray _array;

        public AlignedNodeArrayEnumerator(AlignedNodeArray array)
        {
            _array = array;
            _count = array.Count;
            _i = -1;
        }

        public Node Current
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

    [Serializable, DebuggerTypeProxy(typeof(AlignedNodeArrayDebugView)), DebuggerDisplay("Count = {Count}")]
    public class AlignedNodeArray : IList<Node>
    {
        private IntPtr _native;

        internal AlignedNodeArray(IntPtr native)
        {
            _native = native;
        }

        public int IndexOf(Node item)
        {
            return btAlignedSoftBodyNodeArray_index_of(_native, item._native);
        }

        public void Insert(int index, Node item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public Node this[int index]
        {
            get
            {
                if ((uint)index >= (uint)Count)
                {
                    throw new ArgumentOutOfRangeException("index");
                }
                return new Node(btAlignedSoftBodyNodeArray_at(_native, index));
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void Add(Node item)
        {
            btAlignedSoftBodyNodeArray_push_back(_native, item._native);
        }

        public void Clear()
        {
            btAlignedSoftBodyNodeArray_resizeNoInitialize(_native, 0);
        }

        public bool Contains(Node item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(Node[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { return btAlignedSoftBodyNodeArray_size(_native); }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(Node item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<Node> GetEnumerator()
        {
            return new AlignedNodeArrayEnumerator(this);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return new AlignedNodeArrayEnumerator(this);
        }

        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btAlignedSoftBodyNodeArray_at(IntPtr obj, int n);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern int btAlignedSoftBodyNodeArray_index_of(IntPtr obj, IntPtr val);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btAlignedSoftBodyNodeArray_push_back(IntPtr obj, IntPtr val);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btAlignedSoftBodyNodeArray_resizeNoInitialize(IntPtr obj, int newSize);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern int btAlignedSoftBodyNodeArray_size(IntPtr obj);
    }
}
