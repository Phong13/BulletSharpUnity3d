using System;
using System.Collections.Generic;
using System.IO;

namespace BulletSharp
{
    public enum DnaID : int
    {
        Array = 0x59415241,
        BoxShape = 0x53584f42,
        CollisionObject = 0x4a424f43,
        Constraint = 0x534e4f43,
        Dna = 0x31414e44,
        DynamicsWorld = 0x444c5744,
        MultiBody = 0x5944424d,
        QuantizedBvh = 0x48564251,
        RigidBody = 0x59444252,
        SBMaterial = 0x544d4253,
        SBNode = 0x444e4253,
        Sdna = 0x414e4453,
        Shape = 0x50414853,
        SoftBody = 0x59444253,
        TriangleInfoMap = 0x50414d54
    }
    /*
    class DnaID
    {
        public static readonly int Sdna = MakeID("SDNA");

        public static readonly int Array = MakeID("ARAY");
        public static readonly int BoxShape = MakeID("BOXS");
        public static readonly int CollisionObject = MakeID("COBJ");
        public static readonly int Constraint = MakeID("CONS");
        public static readonly int Dna = MakeID("DNA1");
        public static readonly int DynamicsWorld = MakeID("DWLD");
        public static readonly int MultiBody = MakeID("MBDY");
        public static readonly int QuantizedBvh = MakeID("QBVH");
        public static readonly int RigidBody = MakeID("RBDY");
        public static readonly int SBMaterial = MakeID("SBMT");
        public static readonly int SBNode = MakeID("SBND");
        public static readonly int Shape = MakeID("SHAP");
        public static readonly int SoftBody = MakeID("SBDY");
        public static readonly int TriangleInfoMap = MakeID("TMAP");

        static int MakeID(string id)
        {
            if (BitConverter.IsLittleEndian)
            {
                return (id[0]) + (id[1]  << 8) + (id[2] << 16) + (id[3] << 24);
            }
            return (id[3]) + (id[2] << 8) + (id[1] << 16) + (id[0] << 24);
        }
    }
    */
    public class BulletFile : bFile
    {
        public List<byte[]> Bvhs = new List<byte[]>();
        public List<byte[]> CollisionObjects = new List<byte[]>();
        public List<byte[]> CollisionShapes = new List<byte[]>();
        public List<byte[]> Constraints = new List<byte[]>();
        public List<byte[]> DynamicsWorldInfo = new List<byte[]>();
        public List<byte[]> MultiBodies = new List<byte[]>();
        public List<byte[]> RigidBodies = new List<byte[]>();

        public BulletFile(string fileName)
            : base(fileName)
        {
        }

        public BulletFile(byte[] memoryBuffer, int len)
            : base(memoryBuffer, len)
        {
        }

        protected override string HeaderTag => "BULLET";

        public override void AddDataBlock(byte[] dataBlock)
        {
            //_dataBlocks.push_back(dataBlock);
        }

        /*
		public void AddStruct(char structType, IntPtr data, int len, IntPtr oldPtr, int code)
		{
			btBulletFile_addStruct(_native, structType._native, data, len, oldPtr, code);
		}
        */
        public override void Parse(FileVerboseMode verboseMode)
        {
            ParseInternal(verboseMode);

            //the parsing will convert to cpu endian
            Flags &= ~FileFlags.EndianSwap;

            _fileBuffer[8] = (byte)(BitConverter.IsLittleEndian ? 'v' : 'V');
        }

        protected override void ReadChunks()
        {
            bool brokenDna = (Flags & FileFlags.BrokenDna) != 0;

            _dataStart = SizeOfBlenderHeader;
            long chunkPtr = _dataStart;
            Stream stream = ChunkReader.BaseStream;
            stream.Position = chunkPtr;

            ChunkInd chunk = GetNextBlock(ChunkReader);
            while (chunk.Code != DnaID.Dna)
            {
                long chunkDataOffset = chunkPtr + ChunkUtils.GetChunkSize(Flags);

                if (!brokenDna || chunk.Code != DnaID.QuantizedBvh)
                {
                    // One behind
                    if (chunk.Code == DnaID.Sdna)
                    {
                        break;
                    }

                    byte[] chunkData;
                    if (chunk.StructIndex >= 0)
                    {
                        _chunks.Add(chunk);

                        chunkData = ReadChunkData(chunk, chunkDataOffset);
                        switch (chunk.Code)
                        {
                            case DnaID.CollisionObject:
                                CollisionObjects.Add(chunkData);
                                break;
                            case DnaID.Constraint:
                                Constraints.Add(chunkData);
                                break;
                            case DnaID.DynamicsWorld:
                                DynamicsWorldInfo.Add(chunkData);
                                break;
                            case DnaID.MultiBody:
                                MultiBodies.Add(chunkData);
                                break;
                            case DnaID.SoftBody:
                            case DnaID.TriangleInfoMap:
                                throw new NotImplementedException();
                            case DnaID.QuantizedBvh:
                                Bvhs.Add(chunkData);
                                break;
                            case DnaID.RigidBody:
                                RigidBodies.Add(chunkData);
                                break;
                            case DnaID.Shape:
                                CollisionShapes.Add(chunkData);
                                break;
                        }
                    }
                    else
                    {
#if DEBUG
                        Console.WriteLine("unknown chunk " + chunk.Code);
#endif
                        chunkData = new byte[chunk.Length];
                        ChunkReader.Read(chunkData, 0, chunk.Length);
                    }
                    LibPointers.Add(chunk.OldPtr, chunkData);
                }
                else
                {
#if DEBUG
                    Console.WriteLine("skipping B3_QUANTIZED_BVH_CODE due to broken DNA");
#endif
                }

                chunkPtr = chunkDataOffset + chunk.Length;
                stream.Position = chunkPtr;

                chunk = GetNextBlock(ChunkReader);
                if (chunk.Length < 0)
                    break;
            }
        }

        /*
		public void Bvhs
		{
			get { return btBulletFile_getBvhs(_native); }
		}

		public void CollisionObjects
		{
			get { return btBulletFile_getCollisionObjects(_native); }
		}

		public void CollisionShapes
		{
			get { return btBulletFile_getCollisionShapes(_native); }
		}

		public void Constraints
		{
			get { return btBulletFile_getConstraints(_native); }
		}

		public void DataBlocks
		{
			get { return btBulletFile_getDataBlocks(_native); }
		}

		public void DynamicsWorldInfo
		{
			get { return btBulletFile_getDynamicsWorldInfo(_native); }
		}

		public void RigidBodies
		{
			get { return btBulletFile_getRigidBodies(_native); }
		}

		public void SoftBodies
		{
			get { return btBulletFile_getSoftBodies(_native); }
		}

		public void TriangleInfoMaps
		{
			get { return btBulletFile_getTriangleInfoMaps(_native); }
		}
        */
	}
}
