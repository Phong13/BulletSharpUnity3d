using System;
using System.IO;
using System.Runtime.InteropServices;

namespace BulletSharp
{
    [StructLayout(LayoutKind.Sequential)]
    public struct ChunkPtr4
    {
        public ChunkPtr4(BinaryReader reader)
        {
            Code = reader.ReadInt32();
            Length = reader.ReadInt32();
            UniqueInt1 = reader.ReadInt32();
            DnaNR = reader.ReadInt32();
            NR = reader.ReadInt32();
        }

        public int Code;
        public int Length;
        public int UniqueInt1;
        public int DnaNR;
        public int NR;
    };

    [StructLayout(LayoutKind.Sequential)]
    public struct ChunkPtr8
	{
        public ChunkPtr8(BinaryReader reader)
        {
            Code = reader.ReadInt32();
            Length = reader.ReadInt32();
            UniqueInt1 = reader.ReadInt32();
            UniqueInt2 = reader.ReadInt32();
            DnaNR = reader.ReadInt32();
            NR = reader.ReadInt32();
        }

        public int Code;
        public int Length;
        public int UniqueInt1;
        public int UniqueInt2;
        public int DnaNR;
        public int NR;
	};

    [StructLayout(LayoutKind.Sequential)]
    public class ChunkInd
    {
        public ChunkInd()
        {
        }

        public ChunkInd(ref ChunkPtr4 c)
        {
            Code = (DnaID)c.Code;
            Length = c.Length;
            OldPtr = c.UniqueInt1;
            DnaNR = c.DnaNR;
            NR = c.NR;
        }

        public ChunkInd(ref ChunkPtr8 c)
        {
            Code = (DnaID)c.Code;
            Length = c.Length;
            OldPtr = c.UniqueInt1 + ((long)c.UniqueInt2 << 32);
            DnaNR = c.DnaNR;
            NR = c.NR;
        }

        public DnaID Code;
        public int Length;
        public long OldPtr;
        public int DnaNR;
        public int NR;

        public static int Size
        {
            get { return Marshal.SizeOf((IntPtr.Size == 8) ? typeof(ChunkPtr8) : typeof(ChunkPtr4)); }
        }
    }

    public static class ChunkUtils
    {
        public static int GetOffset(FileFlags flags)
        {
            // if the file is saved in a
            // different format, get the
            // file's chunk size

            if (IntPtr.Size == 8)
            {
                if ((flags & FileFlags.BitsVaries) != 0)
                    return Marshal.SizeOf(typeof(ChunkPtr4));
                else
                    return Marshal.SizeOf(typeof(ChunkPtr8));
            }

            if ((flags & FileFlags.BitsVaries) != 0)
                return Marshal.SizeOf(typeof(ChunkPtr8));
            return Marshal.SizeOf(typeof(ChunkPtr4));
        }
    }
}
