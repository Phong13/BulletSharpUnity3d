using System;
using BulletSharp.Math;


namespace BulletSharp
{
	public class ConvexCast : IDisposable
	{
		public class CastResult : IDisposable
		{
			internal IntPtr Native;

			internal CastResult(IntPtr native)
			{
				Native = native;
			}

			public CastResult()
			{
				Native = UnsafeNativeMethods.btConvexCast_CastResult_new();
			}

			public void DebugDraw(float fraction)
			{
				UnsafeNativeMethods.btConvexCast_CastResult_DebugDraw(Native, fraction);
			}

			public void DrawCoordSystem(Matrix trans)
			{
				UnsafeNativeMethods.btConvexCast_CastResult_drawCoordSystem(Native, ref trans);
			}

			public void ReportFailure(int errNo, int numIterations)
			{
				UnsafeNativeMethods.btConvexCast_CastResult_reportFailure(Native, errNo, numIterations);
			}

			public float AllowedPenetration
			{
				get => UnsafeNativeMethods.btConvexCast_CastResult_getAllowedPenetration(Native);
				set => UnsafeNativeMethods.btConvexCast_CastResult_setAllowedPenetration(Native, value);
			}

			public IDebugDraw DebugDrawer
			{
				get => BulletSharp.DebugDraw.GetManaged(UnsafeNativeMethods.btConvexCast_CastResult_getDebugDrawer(Native));
				set => UnsafeNativeMethods.btConvexCast_CastResult_setDebugDrawer(Native, BulletSharp.DebugDraw.GetUnmanaged(value));
			}

			public float Fraction
			{
				get => UnsafeNativeMethods.btConvexCast_CastResult_getFraction(Native);
				set => UnsafeNativeMethods.btConvexCast_CastResult_setFraction(Native, value);
			}

			public Vector3 HitPoint
			{
				get
				{
					Vector3 value;
					UnsafeNativeMethods.btConvexCast_CastResult_getHitPoint(Native, out value);
					return value;
				}
				set => UnsafeNativeMethods.btConvexCast_CastResult_setHitPoint(Native, ref value);
			}

			public Matrix HitTransformA
			{
				get
				{
					Matrix value;
					UnsafeNativeMethods.btConvexCast_CastResult_getHitTransformA(Native, out value);
					return value;
				}
				set => UnsafeNativeMethods.btConvexCast_CastResult_setHitTransformA(Native, ref value);
			}

			public Matrix HitTransformB
			{
				get
				{
					Matrix value;
					UnsafeNativeMethods.btConvexCast_CastResult_getHitTransformB(Native, out value);
					return value;
				}
				set => UnsafeNativeMethods.btConvexCast_CastResult_setHitTransformB(Native, ref value);
			}

			public Vector3 Normal
			{
				get
				{
					Vector3 value;
					UnsafeNativeMethods.btConvexCast_CastResult_getNormal(Native, out value);
					return value;
				}
				set => UnsafeNativeMethods.btConvexCast_CastResult_setNormal(Native, ref value);
			}

			public void Dispose()
			{
				Dispose(true);
				GC.SuppressFinalize(this);
			}

			protected virtual void Dispose(bool disposing)
			{
				if (Native != IntPtr.Zero)
				{
					UnsafeNativeMethods.btConvexCast_CastResult_delete(Native);
					Native = IntPtr.Zero;
				}
			}

			~CastResult()
			{
				Dispose(false);
			}
		}

		internal IntPtr Native;

		internal ConvexCast(IntPtr native)
		{
			Native = native;
		}

		public bool CalcTimeOfImpact(Matrix fromA, Matrix toA, Matrix fromB, Matrix toB,
			CastResult result)
		{
			return UnsafeNativeMethods.btConvexCast_calcTimeOfImpact(Native, ref fromA, ref toA, ref fromB,
				ref toB, result.Native);
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (Native != IntPtr.Zero)
			{
				UnsafeNativeMethods.btConvexCast_delete(Native);
				Native = IntPtr.Zero;
			}
		}

		~ConvexCast()
		{
			Dispose(false);
		}
	}
}
