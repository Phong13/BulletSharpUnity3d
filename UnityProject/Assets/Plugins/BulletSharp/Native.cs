using System.Runtime.InteropServices;

namespace BulletSharp
{
    public static class Native
    {
#if	UNITY_IOS && !UNITY_EDITOR
        public const string Dll = "__Internal";
#else
		public const string Dll = "libbulletc";
#endif
		public const CallingConvention Conv = CallingConvention.Cdecl;
    }
}
