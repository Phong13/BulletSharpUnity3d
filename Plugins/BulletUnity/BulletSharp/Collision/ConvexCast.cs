using System;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.Math;

namespace BulletSharp
{
	public class ConvexCast : IDisposable
	{
		public class CastResult : IDisposable
		{
			internal IntPtr _native;

			internal CastResult(IntPtr native)
			{
				_native = native;
			}

			public CastResult()
			{
				_native = btConvexCast_CastResult_new();
			}

			public void DebugDraw(float fraction)
			{
				btConvexCast_CastResult_DebugDraw(_native, fraction);
			}

			public void DrawCoordSystem(Matrix trans)
			{
				btConvexCast_CastResult_drawCoordSystem(_native, ref trans);
			}

			public void ReportFailure(int errNo, int numIterations)
			{
				btConvexCast_CastResult_reportFailure(_native, errNo, numIterations);
			}

			public float AllowedPenetration
			{
				get { return btConvexCast_CastResult_getAllowedPenetration(_native); }
				set { btConvexCast_CastResult_setAllowedPenetration(_native, value); }
			}

			public IDebugDraw DebugDrawer
			{
                get { return BulletSharp.DebugDraw.GetManaged(btConvexCast_CastResult_getDebugDrawer(_native)); }
                set { btConvexCast_CastResult_setDebugDrawer(_native, BulletSharp.DebugDraw.GetUnmanaged(value)); }
			}

			public float Fraction
			{
				get { return btConvexCast_CastResult_getFraction(_native); }
				set { btConvexCast_CastResult_setFraction(_native, value); }
			}

			public Vector3 HitPoint
			{
				get
				{
					Vector3 value;
					btConvexCast_CastResult_getHitPoint(_native, out value);
					return value;
				}
				set { btConvexCast_CastResult_setHitPoint(_native, ref value); }
			}

			public Matrix HitTransformA
			{
				get
				{
					Matrix value;
					btConvexCast_CastResult_getHitTransformA(_native, out value);
					return value;
				}
				set { btConvexCast_CastResult_setHitTransformA(_native, ref value); }
			}

			public Matrix HitTransformB
			{
				get
				{
					Matrix value;
					btConvexCast_CastResult_getHitTransformB(_native, out value);
					return value;
				}
				set { btConvexCast_CastResult_setHitTransformB(_native, ref value); }
			}

			public Vector3 Normal
			{
				get
				{
					Vector3 value;
					btConvexCast_CastResult_getNormal(_native, out value);
					return value;
				}
				set { btConvexCast_CastResult_setNormal(_native, ref value); }
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
					btConvexCast_CastResult_delete(_native);
					_native = IntPtr.Zero;
				}
			}

			~CastResult()
			{
				Dispose(false);
			}

			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern IntPtr btConvexCast_CastResult_new();
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btConvexCast_CastResult_DebugDraw(IntPtr obj, float fraction);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btConvexCast_CastResult_drawCoordSystem(IntPtr obj, [In] ref Matrix trans);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern float btConvexCast_CastResult_getAllowedPenetration(IntPtr obj);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern IntPtr btConvexCast_CastResult_getDebugDrawer(IntPtr obj);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern float btConvexCast_CastResult_getFraction(IntPtr obj);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btConvexCast_CastResult_getHitPoint(IntPtr obj, [Out] out Vector3 value);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btConvexCast_CastResult_getHitTransformA(IntPtr obj, [Out] out Matrix value);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btConvexCast_CastResult_getHitTransformB(IntPtr obj, [Out] out Matrix value);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btConvexCast_CastResult_getNormal(IntPtr obj, [Out] out Vector3 value);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btConvexCast_CastResult_reportFailure(IntPtr obj, int errNo, int numIterations);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btConvexCast_CastResult_setAllowedPenetration(IntPtr obj, float value);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btConvexCast_CastResult_setDebugDrawer(IntPtr obj, IntPtr value);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btConvexCast_CastResult_setFraction(IntPtr obj, float value);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btConvexCast_CastResult_setHitPoint(IntPtr obj, [In] ref Vector3 value);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btConvexCast_CastResult_setHitTransformA(IntPtr obj, [In] ref Matrix value);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btConvexCast_CastResult_setHitTransformB(IntPtr obj, [In] ref Matrix value);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btConvexCast_CastResult_setNormal(IntPtr obj, [In] ref Vector3 value);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btConvexCast_CastResult_delete(IntPtr obj);
		}

		internal IntPtr _native;

		internal ConvexCast(IntPtr native)
		{
			_native = native;
		}

		public bool CalcTimeOfImpact(Matrix fromA, Matrix toA, Matrix fromB, Matrix toB, CastResult result)
		{
			return btConvexCast_calcTimeOfImpact(_native, ref fromA, ref toA, ref fromB, ref toB, result._native);
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
				btConvexCast_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~ConvexCast()
		{
			Dispose(false);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btConvexCast_calcTimeOfImpact(IntPtr obj, [In] ref Matrix fromA, [In] ref Matrix toA, [In] ref Matrix fromB, [In] ref Matrix toB, IntPtr result);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConvexCast_delete(IntPtr obj);
	}
}
