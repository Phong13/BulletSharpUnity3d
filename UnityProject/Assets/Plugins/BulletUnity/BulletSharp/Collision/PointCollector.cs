using BulletSharp.Math;


namespace BulletSharp
{
	public class PointCollector : DiscreteCollisionDetectorInterface.Result
	{
		public PointCollector()
			: base(UnsafeNativeMethods.btPointCollector_new())
		{
		}

		public float Distance
		{
			get { return  UnsafeNativeMethods.btPointCollector_getDistance(Native);}
			set {  UnsafeNativeMethods.btPointCollector_setDistance(Native, value);}
		}

		public bool HasResult
		{
			get { return  UnsafeNativeMethods.btPointCollector_getHasResult(Native);}
			set {  UnsafeNativeMethods.btPointCollector_setHasResult(Native, value);}
		}

		public Vector3 NormalOnBInWorld
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btPointCollector_getNormalOnBInWorld(Native, out value);
				return value;
			}
			set {  UnsafeNativeMethods.btPointCollector_setNormalOnBInWorld(Native, ref value);}
		}

		public Vector3 PointInWorld
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btPointCollector_getPointInWorld(Native, out value);
				return value;
			}
			set {  UnsafeNativeMethods.btPointCollector_setPointInWorld(Native, ref value);}
		}
	}
}
