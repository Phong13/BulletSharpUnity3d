using BulletSharp.Math;


namespace BulletSharp
{
	public class ScaledBvhTriangleMeshShape : ConcaveShape
	{
		public ScaledBvhTriangleMeshShape(BvhTriangleMeshShape childShape, Vector3 localScaling)
			: base(UnsafeNativeMethods.btScaledBvhTriangleMeshShape_new(childShape.Native, ref localScaling))
		{
			ChildShape = childShape;
		}

		public BvhTriangleMeshShape ChildShape { get; }
	}
}
