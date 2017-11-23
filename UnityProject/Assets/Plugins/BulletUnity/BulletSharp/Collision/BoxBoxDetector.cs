
namespace BulletSharp
{
	public class BoxBoxDetector : DiscreteCollisionDetectorInterface
	{
		private BoxShape _box1;
		private BoxShape _box2;

		public BoxBoxDetector(BoxShape box1, BoxShape box2)
			: base(UnsafeNativeMethods.btBoxBoxDetector_new(box1.Native, box2.Native))
		{
			_box1 = box1;
			_box2 = box2;
		}

		public BoxShape Box1
		{
			get { return _box1; }
			set
			{
                UnsafeNativeMethods.btBoxBoxDetector_setBox1(Native, value.Native);
				_box1 = value;
			}
		}

		public BoxShape Box2
		{
			get { return _box2; }
			set
			{
                UnsafeNativeMethods.btBoxBoxDetector_setBox2(Native, value.Native);
				_box2 = value;
			}
		}
	}
}
