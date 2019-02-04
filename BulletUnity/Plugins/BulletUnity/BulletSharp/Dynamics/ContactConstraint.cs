/*
using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
	public abstract class ContactConstraint : TypedConstraint
	{
		public PersistentManifold ContactManifold
		{
            get { return new PersistentManifold(btContactConstraint_getContactManifold(_native), true); }
			set { btContactConstraint_setContactManifold(_native, value._native); }
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btContactConstraint_getContactManifold(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btContactConstraint_setContactManifold(IntPtr obj, IntPtr contactManifold);
	}
}
*/