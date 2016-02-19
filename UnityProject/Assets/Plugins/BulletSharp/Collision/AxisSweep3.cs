using System;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.Math;

namespace BulletSharp
{
	public class AxisSweep3 : BroadphaseInterface
	{
        private OverlappingPairCallback _overlappingPairUserCallback;

		public AxisSweep3(Vector3 worldAabbMin, Vector3 worldAabbMax)
            : base(btAxisSweep3_new(ref worldAabbMin, ref worldAabbMax))
		{
            _overlappingPairCache = new HashedOverlappingPairCache(btBroadphaseInterface_getOverlappingPairCache(_native), true);
		}

		public AxisSweep3(Vector3 worldAabbMin, Vector3 worldAabbMax, ushort maxHandles)
            : base(btAxisSweep3_new2(ref worldAabbMin, ref worldAabbMax, maxHandles))
		{
            _overlappingPairCache = new HashedOverlappingPairCache(btBroadphaseInterface_getOverlappingPairCache(_native), true);
		}

		public AxisSweep3(Vector3 worldAabbMin, Vector3 worldAabbMax, ushort maxHandles, OverlappingPairCache pairCache)
            : base(btAxisSweep3_new3(ref worldAabbMin, ref worldAabbMax, maxHandles,
            (pairCache != null) ? pairCache._native : IntPtr.Zero))
		{
            _overlappingPairCache = (pairCache != null) ? pairCache : new HashedOverlappingPairCache(
                btBroadphaseInterface_getOverlappingPairCache(_native), true);
		}

		public AxisSweep3(Vector3 worldAabbMin, Vector3 worldAabbMax, ushort maxHandles, OverlappingPairCache pairCache, bool disableRaycastAccelerator)
            : base(btAxisSweep3_new4(ref worldAabbMin, ref worldAabbMax, maxHandles,
            (pairCache != null) ? pairCache._native : IntPtr.Zero, disableRaycastAccelerator))
		{
            _overlappingPairCache = (pairCache != null) ? pairCache : new HashedOverlappingPairCache(
                btBroadphaseInterface_getOverlappingPairCache(_native), true);
		}

        ushort AddHandle(ref Vector3 aabbMin, ref Vector3 aabbMax, IntPtr owner, short collisionFilterGroup,
            short collisionFilterMask, Dispatcher dispatcher, IntPtr multiSapProxy)
        {
            return btAxisSweep3_addHandle(_native, ref aabbMin, ref aabbMax, owner, collisionFilterGroup, collisionFilterMask, dispatcher._native, multiSapProxy);
        }

        ushort AddHandle(ref Vector3 aabbMin, ref Vector3 aabbMax, IntPtr owner, CollisionFilterGroups collisionFilterGroup,
            CollisionFilterGroups collisionFilterMask, Dispatcher dispatcher, IntPtr multiSapProxy)
        {
            return AddHandle(ref aabbMin, ref aabbMax, owner, collisionFilterGroup, collisionFilterMask, dispatcher, multiSapProxy);
        }

        public override BroadphaseProxy CreateProxy(ref Vector3 aabbMin, ref Vector3 aabbMax, int shapeType, IntPtr userPtr, short collisionFilterGroup, short collisionFilterMask, Dispatcher dispatcher, IntPtr multiSapProxy)
        {
            //throw new NotImplementedException();
            return new BroadphaseProxy(btBroadphaseInterface_createProxy(_native, ref aabbMin, ref aabbMax, shapeType, userPtr, collisionFilterGroup, collisionFilterMask, dispatcher._native, multiSapProxy));
        }
        /*
        Handle GetHandle(ushort index)
        {
            return new Handle(btAxisSweep3_getHandle(_native, index._native), true);
        }

        void ProcessAllOverlappingPairs(OverlapCallback callback)
        {
            btAxisSweep3_processAllOverlappingPairs(_native, callback._native);
        }
        */
        void Quantize(out ushort o, ref Vector3 point, int isMax)
        {
            btAxisSweep3_quantize(_native, out o, ref point, isMax);
        }

        void RemoveHandle(ushort handle, Dispatcher dispatcher)
        {
            btAxisSweep3_removeHandle(_native, handle, dispatcher._native);
        }

        bool TestAabbOverlap(BroadphaseProxy proxy0, BroadphaseProxy proxy1)
        {
            return btAxisSweep3_testAabbOverlap(_native, proxy0._native, proxy1._native);
        }

        void UnQuantize(BroadphaseProxy proxy, Vector3 aabbMin, Vector3 aabbMax)
        {
            btAxisSweep3_unQuantize(_native, proxy._native, out aabbMin, out aabbMax);
        }

        void UpdateHandle(ushort handle, Vector3 aabbMin, Vector3 aabbMax, Dispatcher dispatcher)
        {
            btAxisSweep3_updateHandle(_native, handle, ref aabbMin, ref aabbMax, dispatcher._native);
        }

        public ushort NumHandles
        {
            get { return btAxisSweep3_getNumHandles(_native); }
        }

        public OverlappingPairCallback OverlappingPairUserCallback
        {
            get
            {
                return _overlappingPairUserCallback;
            }
            set
            {
                _overlappingPairUserCallback = value;
                btAxisSweep3_setOverlappingPairUserCallback(_native, (value != null) ? value._native : IntPtr.Zero);
            }
        }

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btAxisSweep3_new([In] ref Vector3 worldAabbMin, [In] ref Vector3 worldAabbMax);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btAxisSweep3_new2([In] ref Vector3 worldAabbMin, [In] ref Vector3 worldAabbMax, ushort maxHandles);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btAxisSweep3_new3([In] ref Vector3 worldAabbMin, [In] ref Vector3 worldAabbMax, ushort maxHandles, IntPtr pairCache);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btAxisSweep3_new4([In] ref Vector3 worldAabbMin, [In] ref Vector3 worldAabbMax, ushort maxHandles, IntPtr pairCache, bool disableRaycastAccelerator);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern ushort btAxisSweep3_addHandle(IntPtr obj, [In] ref Vector3 aabbMin, [In] ref Vector3 aabbMax, IntPtr pOwner, short collisionFilterGroup, short collisionFilterMask, IntPtr dispatcher, IntPtr multiSapProxy);
		//[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		//static extern IntPtr btAxisSweep3_getHandle(IntPtr obj, ushort index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern ushort btAxisSweep3_getNumHandles(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btAxisSweep3_getOverlappingPairUserCallback(IntPtr obj);
		//[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		//static extern void btAxisSweep3_processAllOverlappingPairs(IntPtr obj, IntPtr callback);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btAxisSweep3_quantize(IntPtr obj, [Out] out ushort o, [In] ref Vector3 point, int isMax);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btAxisSweep3_removeHandle(IntPtr obj, ushort handle, IntPtr dispatcher);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btAxisSweep3_setOverlappingPairUserCallback(IntPtr obj, IntPtr pairCallback);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern bool btAxisSweep3_testAabbOverlap(IntPtr obj, IntPtr proxy0, IntPtr proxy1);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btAxisSweep3_unQuantize(IntPtr obj, IntPtr proxy, [Out] out Vector3 aabbMin, [Out] out Vector3 aabbMax);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btAxisSweep3_updateHandle(IntPtr obj, ushort handle, [In] ref Vector3 aabbMin, [In] ref Vector3 aabbMax, IntPtr dispatcher);

	}

	public class AxisSweep3_32Bit : BroadphaseInterface
	{
        private OverlappingPairCallback _overlappingPairUserCallback;

		public AxisSweep3_32Bit(Vector3 worldAabbMin, Vector3 worldAabbMax)
            : base(bt32BitAxisSweep3_new(ref worldAabbMin, ref worldAabbMax))
		{
            _overlappingPairCache = new HashedOverlappingPairCache(btBroadphaseInterface_getOverlappingPairCache(_native), true);
		}

		public AxisSweep3_32Bit(Vector3 worldAabbMin, Vector3 worldAabbMax, uint maxHandles)
            : base(bt32BitAxisSweep3_new2(ref worldAabbMin, ref worldAabbMax, maxHandles))
		{
            _overlappingPairCache = new HashedOverlappingPairCache(btBroadphaseInterface_getOverlappingPairCache(_native), true);
		}

		public AxisSweep3_32Bit(Vector3 worldAabbMin, Vector3 worldAabbMax, uint maxHandles, OverlappingPairCache pairCache)
            : base(bt32BitAxisSweep3_new3(ref worldAabbMin, ref worldAabbMax, maxHandles,
            (pairCache != null) ? pairCache._native : IntPtr.Zero))
		{
            _overlappingPairCache = (pairCache != null) ? pairCache : new HashedOverlappingPairCache(
                btBroadphaseInterface_getOverlappingPairCache(_native), true);
		}

		public AxisSweep3_32Bit(Vector3 worldAabbMin, Vector3 worldAabbMax, uint maxHandles, OverlappingPairCache pairCache, bool disableRaycastAccelerator)
            : base(bt32BitAxisSweep3_new4(ref worldAabbMin, ref worldAabbMax, maxHandles,
            (pairCache != null) ? pairCache._native : IntPtr.Zero, disableRaycastAccelerator))
		{
            _overlappingPairCache = (pairCache != null) ? pairCache : new HashedOverlappingPairCache(
                btBroadphaseInterface_getOverlappingPairCache(_native), true);
		}

        uint AddHandle(ref Vector3 aabbMin, ref Vector3 aabbMax, IntPtr owner, short collisionFilterGroup,
            short collisionFilterMask, Dispatcher dispatcher, IntPtr multiSapProxy)
        {
            return bt32BitAxisSweep3_addHandle(_native, ref aabbMin, ref aabbMax, owner, collisionFilterGroup, collisionFilterMask, dispatcher._native, multiSapProxy);
        }
		
        uint AddHandle(ref Vector3 aabbMin, ref Vector3 aabbMax, IntPtr owner, CollisionFilterGroups collisionFilterGroup,
            CollisionFilterGroups collisionFilterMask, Dispatcher dispatcher, IntPtr multiSapProxy)
        {
            return AddHandle(ref aabbMin, ref aabbMax, owner, collisionFilterGroup, collisionFilterMask, dispatcher, multiSapProxy);
        }

        public override BroadphaseProxy CreateProxy(ref Vector3 aabbMin, ref Vector3 aabbMax, int shapeType, IntPtr userPtr, short collisionFilterGroup, short collisionFilterMask, Dispatcher dispatcher, IntPtr multiSapProxy)
        {
            //throw new NotImplementedException();
            return new BroadphaseProxy(btBroadphaseInterface_createProxy(_native, ref aabbMin, ref aabbMax, shapeType, userPtr, collisionFilterGroup, collisionFilterMask, dispatcher._native, multiSapProxy));
        }
        /*
        Handle GetHandle(uint index)
        {
            return new Handle(bt32BitAxisSweep3_getHandle(_native, index._native), true);
        }

        void ProcessAllOverlappingPairs(OverlapCallback callback)
        {
            bt32BitAxisSweep3_processAllOverlappingPairs(_native, callback._native);
        }
        */
        void Quantize(out uint o, ref Vector3 point, int isMax)
        {
            bt32BitAxisSweep3_quantize(_native, out o, ref point, isMax);
        }

        void RemoveHandle(uint handle, Dispatcher dispatcher)
        {
            bt32BitAxisSweep3_removeHandle(_native, handle, dispatcher._native);
        }
		
        bool TestAabbOverlap(BroadphaseProxy proxy0, BroadphaseProxy proxy1)
        {
            return bt32BitAxisSweep3_testAabbOverlap(_native, proxy0._native, proxy1._native);
        }

        void UnQuantize(BroadphaseProxy proxy, Vector3 aabbMin, Vector3 aabbMax)
        {
            bt32BitAxisSweep3_unQuantize(_native, proxy._native, out aabbMin, out aabbMax);
        }

        void UpdateHandle(uint handle, Vector3 aabbMin, Vector3 aabbMax, Dispatcher dispatcher)
        {
            bt32BitAxisSweep3_updateHandle(_native, handle, ref aabbMin, ref aabbMax, dispatcher._native);
        }

        public uint NumHandles
        {
            get { return bt32BitAxisSweep3_getNumHandles(_native); }
        }

        public OverlappingPairCallback OverlappingPairUserCallback
        {
            get
            {
                return _overlappingPairUserCallback;
            }
            set
            {
                _overlappingPairUserCallback = value;
                bt32BitAxisSweep3_setOverlappingPairUserCallback(_native, (value != null) ? value._native : IntPtr.Zero);
            }
        }

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr bt32BitAxisSweep3_new([In] ref Vector3 worldAabbMin, [In] ref Vector3 worldAabbMax);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr bt32BitAxisSweep3_new2([In] ref Vector3 worldAabbMin, [In] ref Vector3 worldAabbMax, uint maxHandles);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr bt32BitAxisSweep3_new3([In] ref Vector3 worldAabbMin, [In] ref Vector3 worldAabbMax, uint maxHandles, IntPtr pairCache);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr bt32BitAxisSweep3_new4([In] ref Vector3 worldAabbMin, [In] ref Vector3 worldAabbMax, uint maxHandles, IntPtr pairCache, bool disableRaycastAccelerator);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern uint bt32BitAxisSweep3_addHandle(IntPtr obj, [In] ref Vector3 aabbMin, [In] ref Vector3 aabbMax, IntPtr pOwner, short collisionFilterGroup, short collisionFilterMask, IntPtr dispatcher, IntPtr multiSapProxy);
        //[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        //static extern IntPtr bt32BitAxisSweep3_getHandle(IntPtr obj, uint index);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern uint bt32BitAxisSweep3_getNumHandles(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr bt32BitAxisSweep3_getOverlappingPairUserCallback(IntPtr obj);
        //[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        //static extern void bt32BitAxisSweep3_processAllOverlappingPairs(IntPtr obj, IntPtr callback);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void bt32BitAxisSweep3_quantize(IntPtr obj, [Out] out uint o, [In] ref Vector3 point, int isMax);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void bt32BitAxisSweep3_removeHandle(IntPtr obj, uint handle, IntPtr dispatcher);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void bt32BitAxisSweep3_setOverlappingPairUserCallback(IntPtr obj, IntPtr pairCallback);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern bool bt32BitAxisSweep3_testAabbOverlap(IntPtr obj, IntPtr proxy0, IntPtr proxy1);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void bt32BitAxisSweep3_unQuantize(IntPtr obj, IntPtr proxy, [Out] out Vector3 aabbMin, [Out] out Vector3 aabbMax);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void bt32BitAxisSweep3_updateHandle(IntPtr obj, uint handle, [In] ref Vector3 aabbMin, [In] ref Vector3 aabbMax, IntPtr dispatcher);
	}
}
