using System;
using BulletSharp.Math;


namespace BulletSharp
{
	public enum PhyScalarType
	{
		Single,
		Double,
		Int32,
		Int16,
		FixedPoint88,
		Byte
	}

	public abstract class ConcaveShape : CollisionShape
	{
		internal ConcaveShape(IntPtr native)
			: base(native)
		{
		}

		public void ProcessAllTriangles(TriangleCallback callback, Vector3 aabbMin,
			Vector3 aabbMax)
		{
			UnsafeNativeMethods.btConcaveShape_processAllTriangles(Native, callback.Native, ref aabbMin,
				ref aabbMax);
		}
	}
}
