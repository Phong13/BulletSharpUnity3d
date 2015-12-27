using BulletSharp.Math;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using System.Diagnostics;

namespace BulletSharp
{
    [DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(Vector3ListDebugView))]
    public class AlignedVector3Array : IList<Vector3>, IDisposable
    {
        internal IntPtr _native;

        internal AlignedVector3Array(IntPtr native)
        {
            _native = native;
        }

        public AlignedVector3Array()
        {
            _native = btAlignedVector3Array_new();
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
                btAlignedVector3Array_delete(_native);
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
                    throw new ArgumentOutOfRangeException("index");
                }
                Vector3 value;
                btAlignedVector3Array_at(_native, index, out value);
                return value;
            }
            set
            {
                if ((uint)index >= (uint)Count)
                {
                    throw new ArgumentOutOfRangeException("index");
                }
                btAlignedVector3Array_set(_native, index, ref value);
            }
        }

        public void Add(Vector3 item)
        {
            btAlignedVector3Array_push_back(_native, ref item);
        }

        public void Add(Vector4 item)
        {
            btAlignedVector3Array_push_back2(_native, ref item);
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
            get { return btAlignedVector3Array_size(_native); }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

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

        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btAlignedVector3Array_new();
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btAlignedVector3Array_at(IntPtr obj, int n, [Out] out Vector3 value);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btAlignedVector3Array_push_back(IntPtr obj, [In] ref Vector3 value);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btAlignedVector3Array_push_back2(IntPtr obj, [In] ref Vector4 value);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btAlignedVector3Array_set(IntPtr obj, int n, [In] ref Vector3 value);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern int btAlignedVector3Array_size(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btAlignedVector3Array_delete(IntPtr obj);
    }
}
