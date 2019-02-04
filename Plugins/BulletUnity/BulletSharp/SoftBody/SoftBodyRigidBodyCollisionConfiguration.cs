using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
	public class SoftBodyRigidBodyCollisionConfiguration : DefaultCollisionConfiguration
	{
		public SoftBodyRigidBodyCollisionConfiguration()
			: base(btSoftBodyRigidBodyCollisionConfiguration_new())
		{
		}

		public SoftBodyRigidBodyCollisionConfiguration(DefaultCollisionConstructionInfo constructionInfo)
			: base(btSoftBodyRigidBodyCollisionConfiguration_new2(constructionInfo._native))
		{
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBodyRigidBodyCollisionConfiguration_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBodyRigidBodyCollisionConfiguration_new2(IntPtr constructionInfo);
	}
}
