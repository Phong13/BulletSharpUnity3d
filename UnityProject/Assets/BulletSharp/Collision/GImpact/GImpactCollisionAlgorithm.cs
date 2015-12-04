using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
	public class GImpactCollisionAlgorithm : ActivatingCollisionAlgorithm
	{
		public class CreateFunc : CollisionAlgorithmCreateFunc
		{
			internal CreateFunc(IntPtr native)
				: base(native, true)
			{
			}

			public CreateFunc()
				: base(btGImpactCollisionAlgorithm_CreateFunc_new(), false)
			{
			}

			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern IntPtr btGImpactCollisionAlgorithm_CreateFunc_new();
		}

		internal GImpactCollisionAlgorithm(IntPtr native)
			: base(native)
		{
		}

		public GImpactCollisionAlgorithm(CollisionAlgorithmConstructionInfo ci, CollisionObjectWrapper body0Wrap, CollisionObjectWrapper body1Wrap)
			: base(btGImpactCollisionAlgorithm_new(ci._native, body0Wrap._native, body1Wrap._native))
		{
		}

		public void GImpactVsCompoundShape(CollisionObjectWrapper body0Wrap, CollisionObjectWrapper body1Wrap, GImpactShapeInterface shape0, CompoundShape shape1, bool swapped)
		{
			btGImpactCollisionAlgorithm_gimpact_vs_compoundshape(_native, body0Wrap._native, body1Wrap._native, shape0._native, shape1._native, swapped);
		}

		public void GImpactVsConcave(CollisionObjectWrapper body0Wrap, CollisionObjectWrapper body1Wrap, GImpactShapeInterface shape0, ConcaveShape shape1, bool swapped)
		{
			btGImpactCollisionAlgorithm_gimpact_vs_concave(_native, body0Wrap._native, body1Wrap._native, shape0._native, shape1._native, swapped);
		}

		public void GImpactVsGImpact(CollisionObjectWrapper body0Wrap, CollisionObjectWrapper body1Wrap, GImpactShapeInterface shape0, GImpactShapeInterface shape1)
		{
			btGImpactCollisionAlgorithm_gimpact_vs_gimpact(_native, body0Wrap._native, body1Wrap._native, shape0._native, shape1._native);
		}

		public void GImpactVsShape(CollisionObjectWrapper body0Wrap, CollisionObjectWrapper body1Wrap, GImpactShapeInterface shape0, CollisionShape shape1, bool swapped)
		{
			btGImpactCollisionAlgorithm_gimpact_vs_shape(_native, body0Wrap._native, body1Wrap._native, shape0._native, shape1._native, swapped);
		}

		public ManifoldResult InternalGetResultOut()
		{
            return new ManifoldResult(btGImpactCollisionAlgorithm_internalGetResultOut(_native));
		}

		public static void RegisterAlgorithm(CollisionDispatcher dispatcher)
		{
			btGImpactCollisionAlgorithm_registerAlgorithm(dispatcher._native);
		}

		public int Face0
		{
			get { return btGImpactCollisionAlgorithm_getFace0(_native); }
			set { btGImpactCollisionAlgorithm_setFace0(_native, value); }
		}

		public int Face1
		{
			get { return btGImpactCollisionAlgorithm_getFace1(_native); }
			set { btGImpactCollisionAlgorithm_setFace1(_native, value); }
		}

		public int Part0
		{
			get { return btGImpactCollisionAlgorithm_getPart0(_native); }
			set { btGImpactCollisionAlgorithm_setPart0(_native, value); }
		}

		public int Part1
		{
			get { return btGImpactCollisionAlgorithm_getPart1(_native); }
			set { btGImpactCollisionAlgorithm_setPart1(_native, value); }
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btGImpactCollisionAlgorithm_new(IntPtr ci, IntPtr body0Wrap, IntPtr body1Wrap);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btGImpactCollisionAlgorithm_getFace0(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btGImpactCollisionAlgorithm_getFace1(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btGImpactCollisionAlgorithm_getPart0(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btGImpactCollisionAlgorithm_getPart1(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGImpactCollisionAlgorithm_gimpact_vs_compoundshape(IntPtr obj, IntPtr body0Wrap, IntPtr body1Wrap, IntPtr shape0, IntPtr shape1, bool swapped);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGImpactCollisionAlgorithm_gimpact_vs_concave(IntPtr obj, IntPtr body0Wrap, IntPtr body1Wrap, IntPtr shape0, IntPtr shape1, bool swapped);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGImpactCollisionAlgorithm_gimpact_vs_gimpact(IntPtr obj, IntPtr body0Wrap, IntPtr body1Wrap, IntPtr shape0, IntPtr shape1);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGImpactCollisionAlgorithm_gimpact_vs_shape(IntPtr obj, IntPtr body0Wrap, IntPtr body1Wrap, IntPtr shape0, IntPtr shape1, bool swapped);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btGImpactCollisionAlgorithm_internalGetResultOut(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGImpactCollisionAlgorithm_registerAlgorithm(IntPtr dispatcher);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGImpactCollisionAlgorithm_setFace0(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGImpactCollisionAlgorithm_setFace1(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGImpactCollisionAlgorithm_setPart0(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGImpactCollisionAlgorithm_setPart1(IntPtr obj, int value);
	}
}
