using System;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.Math;

namespace BulletSharp
{
	public class GjkPairDetector : DiscreteCollisionDetectorInterface
	{
		internal GjkPairDetector(IntPtr native)
			: base(native)
		{
		}

		public GjkPairDetector(ConvexShape objectA, ConvexShape objectB, VoronoiSimplexSolver simplexSolver, ConvexPenetrationDepthSolver penetrationDepthSolver)
            : base(btGjkPairDetector_new(objectA._native, objectB._native, simplexSolver._native, (penetrationDepthSolver != null) ? penetrationDepthSolver._native : IntPtr.Zero))
		{
		}

		public GjkPairDetector(ConvexShape objectA, ConvexShape objectB, int shapeTypeA, int shapeTypeB, float marginA, float marginB, VoronoiSimplexSolver simplexSolver, ConvexPenetrationDepthSolver penetrationDepthSolver)
            : base(btGjkPairDetector_new2(objectA._native, objectB._native, shapeTypeA, shapeTypeB, marginA, marginB, simplexSolver._native, (penetrationDepthSolver != null) ? penetrationDepthSolver._native : IntPtr.Zero))
		{
		}

		public void GetClosestPointsNonVirtual(ClosestPointInput input, Result output, IDebugDraw debugDraw)
		{
			btGjkPairDetector_getClosestPointsNonVirtual(_native, input._native, output._native, DebugDraw.GetUnmanaged(debugDraw));
		}

		public void SetIgnoreMargin(bool ignoreMargin)
		{
			btGjkPairDetector_setIgnoreMargin(_native, ignoreMargin);
		}

		public void SetMinkowskiA(ConvexShape minkA)
		{
			btGjkPairDetector_setMinkowskiA(_native, minkA._native);
		}

		public void SetMinkowskiB(ConvexShape minkB)
		{
			btGjkPairDetector_setMinkowskiB(_native, minkB._native);
		}

		public void SetPenetrationDepthSolver(ConvexPenetrationDepthSolver penetrationDepthSolver)
		{
			btGjkPairDetector_setPenetrationDepthSolver(_native, penetrationDepthSolver._native);
		}

		public Vector3 CachedSeparatingAxis
		{
			get
			{
				Vector3 value;
				btGjkPairDetector_getCachedSeparatingAxis(_native, out value);
				return value;
			}
            set { btGjkPairDetector_setCachedSeparatingAxis(_native, ref value); }
		}

		public float CachedSeparatingDistance
		{
			get { return btGjkPairDetector_getCachedSeparatingDistance(_native); }
		}

		public int CatchDegeneracies
		{
			get { return btGjkPairDetector_getCatchDegeneracies(_native); }
			set { btGjkPairDetector_setCatchDegeneracies(_native, value); }
		}

		public int CurIter
		{
			get { return btGjkPairDetector_getCurIter(_native); }
			set { btGjkPairDetector_setCurIter(_native, value); }
		}

		public int DegenerateSimplex
		{
			get { return btGjkPairDetector_getDegenerateSimplex(_native); }
			set { btGjkPairDetector_setDegenerateSimplex(_native, value); }
		}

		public int FixContactNormalDirection
		{
			get { return btGjkPairDetector_getFixContactNormalDirection(_native); }
			set { btGjkPairDetector_setFixContactNormalDirection(_native, value); }
		}

		public int LastUsedMethod
		{
			get { return btGjkPairDetector_getLastUsedMethod(_native); }
			set { btGjkPairDetector_setLastUsedMethod(_native, value); }
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btGjkPairDetector_new(IntPtr objectA, IntPtr objectB, IntPtr simplexSolver, IntPtr penetrationDepthSolver);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btGjkPairDetector_new2(IntPtr objectA, IntPtr objectB, int shapeTypeA, int shapeTypeB, float marginA, float marginB, IntPtr simplexSolver, IntPtr penetrationDepthSolver);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGjkPairDetector_getCachedSeparatingAxis(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btGjkPairDetector_getCachedSeparatingDistance(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btGjkPairDetector_getCatchDegeneracies(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGjkPairDetector_getClosestPointsNonVirtual(IntPtr obj, IntPtr input, IntPtr output, IntPtr debugDraw);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btGjkPairDetector_getCurIter(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btGjkPairDetector_getDegenerateSimplex(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btGjkPairDetector_getFixContactNormalDirection(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btGjkPairDetector_getLastUsedMethod(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGjkPairDetector_setCachedSeparatingAxis(IntPtr obj, [In] ref Vector3 seperatingAxis);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGjkPairDetector_setCatchDegeneracies(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGjkPairDetector_setCurIter(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGjkPairDetector_setDegenerateSimplex(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGjkPairDetector_setFixContactNormalDirection(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGjkPairDetector_setIgnoreMargin(IntPtr obj, bool ignoreMargin);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGjkPairDetector_setLastUsedMethod(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGjkPairDetector_setMinkowskiA(IntPtr obj, IntPtr minkA);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGjkPairDetector_setMinkowskiB(IntPtr obj, IntPtr minkB);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGjkPairDetector_setPenetrationDepthSolver(IntPtr obj, IntPtr penetrationDepthSolver);
	}
}
