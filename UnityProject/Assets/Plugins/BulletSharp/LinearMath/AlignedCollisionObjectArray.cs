using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using System.Diagnostics;

namespace BulletSharp
{
    public class AlignedCollisionObjectArrayDebugView
    {
        private readonly AlignedCollisionObjectArray _array;

        public AlignedCollisionObjectArrayDebugView(AlignedCollisionObjectArray array)
        {
            _array = array;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public CollisionObject[] Items
        {
            get
            {
                CollisionObject[] array = new CollisionObject[_array.Count];
                for (int i = 0; i < _array.Count; i++)
                {
                    array[i] = _array[i];
                }
                return array;
            }
        }
    }

    public class AlignedCollisionObjectArrayEnumerator : IEnumerator<CollisionObject>
    {
        int _i;
        readonly int _count;
        readonly IList<CollisionObject> _array;

        public AlignedCollisionObjectArrayEnumerator(IList<CollisionObject> array)
        {
            _array = array;
            _count = array.Count;
            _i = -1;
        }

        public CollisionObject Current
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

    [Serializable, DebuggerTypeProxy(typeof(AlignedCollisionObjectArrayDebugView)), DebuggerDisplay("Count = {Count}")]
    public class AlignedCollisionObjectArray : IList<CollisionObject>
    {
        private IntPtr _native;
        private CollisionWorld _collisionWorld;
        private List<CollisionObject> _backingList;

        internal AlignedCollisionObjectArray(IntPtr native)
        {
            _native = native;
        }

        internal AlignedCollisionObjectArray(IntPtr native, CollisionWorld collisionWorld)
        {
            _native = native;
            if (collisionWorld != null)
            {
                _collisionWorld = collisionWorld;
                _backingList = new List<CollisionObject>();
            }
        }

        public int IndexOf(CollisionObject item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, CollisionObject item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public CollisionObject this[int index]
        {
            get
            {
                if (_backingList != null)
                {
                    return _backingList[index];
                }
                if ((uint)index >= (uint)Count)
                {
                    throw new ArgumentOutOfRangeException("index");
                }
                return CollisionObject.GetManaged(btAlignedCollisionObjectArray_at(_native, index));
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void Add(CollisionObject item)
        {
            if (_collisionWorld != null)
            {
                if (item is RigidBody)
                {
                    if (item.CollisionShape == null)
                    {
                        return;
                    }
                    btDynamicsWorld_addRigidBody(_collisionWorld._native, item._native);
                }
                else if (item is BulletSharp.SoftBody.SoftBody)
                {
                    btSoftRigidDynamicsWorld_addSoftBody(_collisionWorld._native, item._native);
                }
                else
                {
                    btCollisionWorld_addCollisionObject(_collisionWorld._native, item._native);
                }
                SetBodyBroadphaseHandle(item, _collisionWorld.Broadphase);
                _backingList.Add(item);
            }
            else
            {
                btAlignedCollisionObjectArray_push_back(_native, item._native);
            }
        }

        internal void Add(CollisionObject item, short group, short mask)
        {
            if (item is RigidBody)
            {
                if (item.CollisionShape == null)
                {
                    return;
                }
                btDynamicsWorld_addRigidBody2(_collisionWorld._native, item._native, group, mask);
            }
            else if (item is BulletSharp.SoftBody.SoftBody)
            {
                btSoftRigidDynamicsWorld_addSoftBody3(_collisionWorld._native, item._native, group, mask);
            }
            else
            {
                btCollisionWorld_addCollisionObject3(_collisionWorld._native, item._native, group, mask);
            }
            SetBodyBroadphaseHandle(item, _collisionWorld.Broadphase);
            _backingList.Add(item);
        }

        public void Clear()
        {
            if (_backingList != null)
            {
                _backingList.Clear();
            }
            btAlignedCollisionObjectArray_resizeNoInitialize(_native, 0);
        }

        public bool Contains(CollisionObject item)
        {
            //return btAlignedCollisionObjectArray_findLinearSearch(_native, item._native) != Count;
            throw new NotImplementedException();
        }

        public void CopyTo(CollisionObject[] array, int arrayIndex)
        {
            if (array == null)
		        throw new ArgumentNullException("array");

	        if (arrayIndex < 0)
		        throw new ArgumentOutOfRangeException("array");

	        int count = Count;
            if (arrayIndex + count > array.Length)
		        throw new ArgumentException("Array too small.", "array");

	        int i;
            for (i = 0; i < count; i++)
	        {
                array[arrayIndex + i] = this[i];
	        }
        }

        public int Count
        {
            get { return btAlignedCollisionObjectArray_size(_native); }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(CollisionObject item)
        {
            IntPtr itemPtr = item._native;

            if (_backingList == null)
            {
                throw new NotImplementedException();
                //btAlignedCollisionObjectArray_remove(itemPtr);
            }

            int count = _backingList.Count;
            for (int i = 0; i < count; i++)
            {
                if (_backingList[i]._native == itemPtr)
                {
                    if (item is BulletSharp.SoftBody.SoftBody)
                    {
                        btSoftRigidDynamicsWorld_removeSoftBody(_collisionWorld._native, itemPtr);
                    }
                    else if (item is RigidBody)
                    {
                        btDynamicsWorld_removeRigidBody(_collisionWorld._native, itemPtr);
                    }
                    else
                    {
                        btCollisionWorld_removeCollisionObject(_collisionWorld._native, itemPtr);
                    }
                    _backingList[i].BroadphaseHandle = null;
                    count--;

                    // Swap the last item with the item to be removed like Bullet does.
                    if (i != count)
                    {
                        _backingList[i] = _backingList[count];
                    }
                    _backingList.RemoveAt(count);
                    return true;
                }
            }
            return false;
        }

        private void SetBodyBroadphaseHandle(CollisionObject item, BroadphaseInterface broadphase)
        {
            IntPtr broadphaseHandle = btCollisionObject_getBroadphaseHandle(item._native);
            if (broadphase is DbvtBroadphase)
            {
                item.BroadphaseHandle = new DbvtProxy(broadphaseHandle);
            }
            // TODO: implement AxisSweep3::Handle
            /*
	        else if (broadphase is AxisSweep3)
            {
		        item.BroadphaseHandle = new AxisSweep3::Handle(broadphaseHandle);
	        }
            else if (broadphase is AxisSweep3_32Bit)
            {
		        item.BroadphaseHandle = new AxisSweep3_32Bit::Handle(broadphaseHandle);
	        }
            */
            else
            {
                item.BroadphaseHandle = new BroadphaseProxy(broadphaseHandle);
	        }
        }

        public IEnumerator<CollisionObject> GetEnumerator()
        {
            if (_backingList != null)
            {
                return _backingList.GetEnumerator();
            }
            else
            {
                return new AlignedCollisionObjectArrayEnumerator(this);
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            if (_backingList != null)
            {
                return _backingList.GetEnumerator();
            }
            else
            {
                return new AlignedCollisionObjectArrayEnumerator(this);
            }
        }

        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btAlignedCollisionObjectArray_at(IntPtr obj, int n);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btAlignedCollisionObjectArray_push_back(IntPtr obj, IntPtr val);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btAlignedCollisionObjectArray_resizeNoInitialize(IntPtr obj, int newSize);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern int btAlignedCollisionObjectArray_size(IntPtr obj);

        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btCollisionObject_getBroadphaseHandle(IntPtr obj);

        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCollisionWorld_addCollisionObject(IntPtr obj, IntPtr collisionObject);
        //[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        //static extern void btCollisionWorld_addCollisionObject2(IntPtr obj, IntPtr collisionObject, short collisionFilterGroup);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCollisionWorld_addCollisionObject3(IntPtr obj, IntPtr collisionObject, short collisionFilterGroup, short collisionFilterMask);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btCollisionWorld_getBroadphase(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCollisionWorld_removeCollisionObject(IntPtr obj, IntPtr collisionObject);

        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btDynamicsWorld_addRigidBody(IntPtr obj, IntPtr body);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btDynamicsWorld_addRigidBody2(IntPtr obj, IntPtr body, short group, short mask);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btDynamicsWorld_removeRigidBody(IntPtr obj, IntPtr body);

        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btSoftRigidDynamicsWorld_addSoftBody(IntPtr obj, IntPtr body);
        //[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        //static extern void btSoftRigidDynamicsWorld_addSoftBody2(IntPtr obj, IntPtr body, short collisionFilterGroup);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btSoftRigidDynamicsWorld_addSoftBody3(IntPtr obj, IntPtr body, short collisionFilterGroup, short collisionFilterMask);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btSoftRigidDynamicsWorld_removeSoftBody(IntPtr obj, IntPtr body);
    }
}
