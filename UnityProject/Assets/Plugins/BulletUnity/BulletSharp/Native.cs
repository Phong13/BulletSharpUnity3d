using System.Runtime.InteropServices;

namespace BulletSharp
{
    public static class Native
    {
#if	UNITY_IOS && !UNITY_EDITOR
        public const string Dll = "__Internal";
#elif UNITY_EDITOR_LINUX || UNITY_STANDALONE_LINUX
        public const string Dll = "bulletc";
#else
		public const string Dll = "libbulletc";
#endif
		public const CallingConvention Conv = CallingConvention.Cdecl;
    }
}
