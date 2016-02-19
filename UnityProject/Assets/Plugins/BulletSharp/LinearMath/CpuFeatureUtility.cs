using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
	[Flags]
	public enum CpuFeature
	{
		None = 0,
		Fma3 = 1,
		Sse41 = 2,
		NeonHpfp = 4
	}

	public static class CpuFeatureUtility
	{
		public static int CpuFeatures
		{
			get { return btCpuFeatureUtility_getCpuFeatures(); }
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btCpuFeatureUtility_getCpuFeatures();
	}
}
