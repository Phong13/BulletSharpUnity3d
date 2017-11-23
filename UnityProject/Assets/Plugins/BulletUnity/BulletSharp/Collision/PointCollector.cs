using BulletSharp.Math;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class PointCollector : DiscreteCollisionDetectorInterface.Result
	{
		public PointCollector()
			: base(btPointCollector_new())
		{
		}

		public float Distance
		{
			get => btPointCollector_getDistance(Native);
			set => btPointCollector_setDistance(Native, value);
		}

		public bool HasResult
		{
			get => btPointCollector_getHasResult(Native);
			set => btPointCollector_setHasResult(Native, value);
		}

		public Vector3 NormalOnBInWorld
		{
			get
			{
				Vector3 value;
				btPointCollector_getNormalOnBInWorld(Native, out value);
				return value;
			}
			set => btPointCollector_setNormalOnBInWorld(Native, ref value);
		}

		public Vector3 PointInWorld
		{
			get
			{
				Vector3 value;
				btPointCollector_getPointInWorld(Native, out value);
				return value;
			}
			set => btPointCollector_setPointInWorld(Native, ref value);
		}
	}
}
