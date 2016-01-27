using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
	public class MultiBodyLinkCollider : CollisionObject
	{
        private MultiBody _multiBody;

		public MultiBodyLinkCollider(MultiBody multiBody, int link)
			: base(btMultiBodyLinkCollider_new(multiBody._native, link))
		{
            _multiBody = multiBody;
		}

		public static MultiBodyLinkCollider Upcast(CollisionObject colObj)
		{
            return CollisionObject.GetManaged(btMultiBodyLinkCollider_upcast(colObj._native)) as MultiBodyLinkCollider;
		}

		public int Link
		{
			get { return btMultiBodyLinkCollider_getLink(_native); }
			set { btMultiBodyLinkCollider_setLink(_native, value); }
		}

		public MultiBody MultiBody
		{
			get { return _multiBody; }
			set
			{
				btMultiBodyLinkCollider_setMultiBody(_native, value._native);
				_multiBody = value;
			}
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btMultiBodyLinkCollider_new(IntPtr multiBody, int link);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btMultiBodyLinkCollider_getLink(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btMultiBodyLinkCollider_getMultiBody(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBodyLinkCollider_setLink(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBodyLinkCollider_setMultiBody(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btMultiBodyLinkCollider_upcast(IntPtr colObj);
	}
}
