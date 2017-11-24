using System;


namespace BulletSharp
{
	[Flags]
	public enum CpuFeatures
	{
		None = 0,
		Fma3 = 1,
		Sse41 = 2,
		NeonHpfp = 4
	}

	public static class CpuFeatureUtility
	{
		public static CpuFeatures CpuFeatures{ get { return  UnsafeNativeMethods.btCpuFeatureUtility_getCpuFeatures();} }
	}
}
