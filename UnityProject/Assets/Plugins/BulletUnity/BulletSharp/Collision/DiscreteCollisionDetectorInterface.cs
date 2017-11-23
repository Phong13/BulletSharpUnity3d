using System;
using BulletSharp.Math;


namespace BulletSharp
{
	public abstract class DiscreteCollisionDetectorInterface : IDisposable
	{
		public class ClosestPointInput : IDisposable
		{
			internal IntPtr Native;

			public ClosestPointInput()
			{
				Native = UnsafeNativeMethods.btDiscreteCollisionDetectorInterface_ClosestPointInput_new();
			}

			public float MaximumDistanceSquared
			{
				get => UnsafeNativeMethods.btDiscreteCollisionDetectorInterface_ClosestPointInput_getMaximumDistanceSquared(Native);
				set => UnsafeNativeMethods.btDiscreteCollisionDetectorInterface_ClosestPointInput_setMaximumDistanceSquared(Native, value);
			}

			public Matrix TransformA
			{
				get
				{
					Matrix value;
					UnsafeNativeMethods.btDiscreteCollisionDetectorInterface_ClosestPointInput_getTransformA(Native, out value);
					return value;
				}
				set => UnsafeNativeMethods.btDiscreteCollisionDetectorInterface_ClosestPointInput_setTransformA(Native, ref value);
			}

			public Matrix TransformB
			{
				get
				{
					Matrix value;
					UnsafeNativeMethods.btDiscreteCollisionDetectorInterface_ClosestPointInput_getTransformB(Native, out value);
					return value;
				}
				set => UnsafeNativeMethods.btDiscreteCollisionDetectorInterface_ClosestPointInput_setTransformB(Native, ref value);
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
					UnsafeNativeMethods.btDiscreteCollisionDetectorInterface_ClosestPointInput_delete(Native);
					Native = IntPtr.Zero;
				}
			}

			~ClosestPointInput()
			{
				Dispose(false);
			}
		}

		public abstract class Result : IDisposable
		{
			internal IntPtr Native;

			internal Result(IntPtr native)
			{
				Native = native;
			}

			public void AddContactPoint(Vector3 normalOnBInWorld, Vector3 pointInWorld,
				float depth)
			{
				UnsafeNativeMethods.btDiscreteCollisionDetectorInterface_Result_addContactPoint(Native,
					ref normalOnBInWorld, ref pointInWorld, depth);
			}

			public void SetShapeIdentifiersA(int partId0, int index0)
			{
				UnsafeNativeMethods.btDiscreteCollisionDetectorInterface_Result_setShapeIdentifiersA(
					Native, partId0, index0);
			}

			public void SetShapeIdentifiersB(int partId1, int index1)
			{
				UnsafeNativeMethods.btDiscreteCollisionDetectorInterface_Result_setShapeIdentifiersB(
					Native, partId1, index1);
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
					UnsafeNativeMethods.btDiscreteCollisionDetectorInterface_Result_delete(Native);
					Native = IntPtr.Zero;
				}
			}

			~Result()
			{
				Dispose(false);
			}
		}

		internal IntPtr Native;

		internal DiscreteCollisionDetectorInterface(IntPtr native)
		{
			Native = native;
		}

		public void GetClosestPoints(ClosestPointInput input, Result output, IDebugDraw debugDraw,
			bool swapResults = false)
		{
			UnsafeNativeMethods.btDiscreteCollisionDetectorInterface_getClosestPoints(Native, input.Native,
				output.Native, DebugDraw.GetUnmanaged(debugDraw), swapResults);
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
				UnsafeNativeMethods.btDiscreteCollisionDetectorInterface_delete(Native);
				Native = IntPtr.Zero;
			}
		}

		~DiscreteCollisionDetectorInterface()
		{
			Dispose(false);
		}
	}

	public abstract class StorageResult : DiscreteCollisionDetectorInterface.Result
	{
		internal StorageResult(IntPtr native)
			: base(native)
		{
		}
		/*
		public StorageResult()
			: base(btStorageResultWrapper_new())
		{
		}
		*/
		public Vector3 ClosestPointInB
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btStorageResult_getClosestPointInB(Native, out value);
				return value;
			}
			set => UnsafeNativeMethods.btStorageResult_setClosestPointInB(Native, ref value);
		}

		public float Distance
		{
			get => UnsafeNativeMethods.btStorageResult_getDistance(Native);
			set => UnsafeNativeMethods.btStorageResult_setDistance(Native, value);
		}

		public Vector3 NormalOnSurfaceB
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btStorageResult_getNormalOnSurfaceB(Native, out value);
				return value;
			}
			set => UnsafeNativeMethods.btStorageResult_setNormalOnSurfaceB(Native, ref value);
		}
	}
}
