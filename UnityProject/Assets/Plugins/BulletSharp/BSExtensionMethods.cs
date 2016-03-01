using System;
using System.IO;
using System.Collections;
using System.Reflection;

namespace BulletSharp
{
    public static class BSExtensionMethods
    {
        public static IntPtr Add(this IntPtr ptr, int amt)
        {
            return new IntPtr(ptr.ToInt64() + amt);
        }

        public static void Dispose(this BinaryReader reader)
        {
            MethodInfo dynMethod = reader.GetType().GetMethod("Dispose",
                            BindingFlags.NonPublic | BindingFlags.Instance,
                            null,
                            new Type[] { typeof(bool) },
                            null);
            dynMethod.Invoke(reader, new System.Object[] { true });
        }

        public static void Dispose(this BinaryWriter writer)
        {
        }
    }
}

