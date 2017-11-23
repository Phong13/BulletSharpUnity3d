using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class MultiBodyLinkCollider : CollisionObject
	{
		private MultiBody _multiBody;

		public MultiBodyLinkCollider(MultiBody multiBody, int link)
			: base(btMultiBodyLinkCollider_new(multiBody.Native, link))
		{
			_multiBody = multiBody;
		}

		public static MultiBodyLinkCollider Upcast(CollisionObject colObj)
		{
			return GetManaged(btMultiBodyLinkCollider_upcast(colObj.Native)) as MultiBodyLinkCollider;
		}

		public int Link
		{
			get => btMultiBodyLinkCollider_getLink(Native);
			set => btMultiBodyLinkCollider_setLink(Native, value);
		}

		public MultiBody MultiBody
		{
			get => _multiBody;
			set
			{
				btMultiBodyLinkCollider_setMultiBody(Native, value.Native);
				_multiBody = value;
			}
		}
	}
}
