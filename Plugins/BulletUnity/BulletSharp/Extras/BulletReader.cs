using System;
using System.IO;
using BulletSharp.Math;

namespace BulletSharp
{
    class BulletReader : BinaryReader
    {
        public BulletReader(Stream stream)
            : base(stream)
        {
        }

        public float ReadByte(int position)
        {
            BaseStream.Position = position;
            return ReadByte();
        }

        public float ReadSingle(int position)
        {
            BaseStream.Position = position;
            return ReadSingle();
        }

        public int ReadInt32(int position)
        {
            BaseStream.Position = position;
            return ReadInt32();
        }

        public long ReadInt64(int position)
        {
            BaseStream.Position = position;
            return ReadInt64();
        }

        public Matrix ReadMatrix()
        {
            float[] m = new float[16];
            m[0] = ReadSingle();
            m[4] = ReadSingle();
            m[8] = ReadSingle();
            ReadSingle();
            m[1] = ReadSingle();
            m[5] = ReadSingle();
            m[9] = ReadSingle();
            ReadSingle();
            m[2] = ReadSingle();
            m[6] = ReadSingle();
            m[10] = ReadSingle();
            ReadSingle();
            m[12] = ReadSingle();
            m[13] = ReadSingle();
            m[14] = ReadSingle();
            ReadSingle();
            m[15] = 1;
            return new Matrix(m);
        }

        public Matrix ReadMatrix(int position)
        {
            BaseStream.Position = position;
            return ReadMatrix();
        }

        public long ReadPtr()
        {
            return (IntPtr.Size == 8) ? ReadInt64() : ReadInt32();
        }

        public long ReadPtr(int position)
        {
            BaseStream.Position = position;
            return ReadPtr();
        }

        public Vector3 ReadVector3()
        {
            float x = ReadSingle();
            float y = ReadSingle();
            float z = ReadSingle();
            BaseStream.Position += 4; // float w = ReadSingle();
            return new Vector3(x, y, z);
        }

        public Vector3 ReadVector3(int position)
        {
            BaseStream.Position = position;
            return ReadVector3();
        }
    }
}
