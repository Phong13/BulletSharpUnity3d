using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
	public class BoxBoxDetector : DiscreteCollisionDetectorInterface
	{
		private BoxShape _box1;
		private BoxShape _box2;

		public BoxBoxDetector(BoxShape box1, BoxShape box2)
			: base(btBoxBoxDetector_new(box1._native, box2._native))
		{
			_box1 = box1;
			_box2 = box2;
		}

		public BoxShape Box1
		{
			get { return _box1; }
			set
			{
				btBoxBoxDetector_setBox1(_native, value._native);
				_box1 = value;
			}
		}

		public BoxShape Box2
		{
			get { return _box2; }
			set
			{
				btBoxBoxDetector_setBox2(_native, value._native);
				_box2 = value;
			}
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btBoxBoxDetector_new(IntPtr box1, IntPtr box2);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btBoxBoxDetector_setBox1(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btBoxBoxDetector_setBox2(IntPtr obj, IntPtr value);
	}
}
