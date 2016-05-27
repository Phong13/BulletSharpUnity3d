using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
	public class ManifoldResult : DiscreteCollisionDetectorInterface.Result
	{
		internal ManifoldResult(IntPtr native)
			: base(native)
		{
		}

		public ManifoldResult()
			: base(btManifoldResult_new())
		{
		}

		public ManifoldResult(CollisionObjectWrapper body0Wrap, CollisionObjectWrapper body1Wrap)
			: base(btManifoldResult_new2(body0Wrap._native, body1Wrap._native))
		{
		}

		public static float CalculateCombinedFriction(CollisionObject body0, CollisionObject body1)
		{
			return btManifoldResult_calculateCombinedFriction(body0._native, body1._native);
		}

		public static float CalculateCombinedRestitution(CollisionObject body0, CollisionObject body1)
		{
			return btManifoldResult_calculateCombinedRestitution(body0._native, body1._native);
		}

		public void RefreshContactPoints()
		{
			btManifoldResult_refreshContactPoints(_native);
		}

		public CollisionObject Body0Internal
		{
			get { return CollisionObject.GetManaged(btManifoldResult_getBody0Internal(_native)); }
		}

		public CollisionObjectWrapper Body0Wrap
		{
			get { return new CollisionObjectWrapper(btManifoldResult_getBody0Wrap(_native)); }
			set { btManifoldResult_setBody0Wrap(_native, value._native); }
		}

		public CollisionObject Body1Internal
		{
			get { return CollisionObject.GetManaged(btManifoldResult_getBody1Internal(_native)); }
		}

		public CollisionObjectWrapper Body1Wrap
		{
			get { return new CollisionObjectWrapper(btManifoldResult_getBody1Wrap(_native)); }
			set { btManifoldResult_setBody1Wrap(_native, value._native); }
		}

		public PersistentManifold PersistentManifold
		{
            get { return new PersistentManifold(btManifoldResult_getPersistentManifold(_native), true); }
			set { btManifoldResult_setPersistentManifold(_native, value._native); }
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btManifoldResult_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btManifoldResult_new2(IntPtr body0Wrap, IntPtr body1Wrap);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btManifoldResult_calculateCombinedFriction(IntPtr body0, IntPtr body1);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btManifoldResult_calculateCombinedRestitution(IntPtr body0, IntPtr body1);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btManifoldResult_getBody0Internal(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btManifoldResult_getBody0Wrap(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btManifoldResult_getBody1Internal(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btManifoldResult_getBody1Wrap(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btManifoldResult_getPersistentManifold(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btManifoldResult_refreshContactPoints(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btManifoldResult_setBody0Wrap(IntPtr obj, IntPtr obj0Wrap);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btManifoldResult_setBody1Wrap(IntPtr obj, IntPtr obj1Wrap);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btManifoldResult_setPersistentManifold(IntPtr obj, IntPtr manifoldPtr);
	}
}
