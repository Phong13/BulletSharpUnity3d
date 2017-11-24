

namespace BulletSharp
{
	public class MultiBodyLinkCollider : CollisionObject
	{
		private MultiBody _multiBody;

		public MultiBodyLinkCollider(MultiBody multiBody, int link)
			: base(UnsafeNativeMethods.btMultiBodyLinkCollider_new(multiBody.Native, link))
		{
			_multiBody = multiBody;
		}

		public static MultiBodyLinkCollider Upcast(CollisionObject colObj)
		{
			return GetManaged(UnsafeNativeMethods.btMultiBodyLinkCollider_upcast(colObj.Native)) as MultiBodyLinkCollider;
		}

		public int Link
		{
			get { return  UnsafeNativeMethods.btMultiBodyLinkCollider_getLink(Native);}
			set {  UnsafeNativeMethods.btMultiBodyLinkCollider_setLink(Native, value);}
		}

		public MultiBody MultiBody
		{
			get { return  _multiBody;}
			set
			{
				UnsafeNativeMethods.btMultiBodyLinkCollider_setMultiBody(Native, value.Native);
				_multiBody = value;
			}
		}
	}
}
