using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;

namespace BulletSharp
{
    [Flags]
    public enum FileFlags
    {
        None = 0,
        EndianSwap = 1,
        File64 = 2,
        BitsVaries = 4,
        DoublePrecision = 8,
        BrokenDna = 0x10
    }

    [Flags]
    public enum FileVerboseMode
    {
        None = 0,
        ExportXml = 1,
        DumpDnaTypeDefinitions = 2,
        DumpChunks = 4,
        DumpFileInfo = 8
    }

    class PointerFixup
    {
        public byte[] StructAlloc { get; set; }
        public long[] Offsets { get; set; }

        public PointerFixup(byte[] structAlloc, long[] offsets)
        {
            StructAlloc = structAlloc;
            Offsets = offsets;
        }
    }

    public abstract class bFile
    {
        protected const int SizeOfBlenderHeader = 12;
        private const int MaxArrayLength = 512;
        private const int MaxStringLength = 1024;

        protected List<ChunkInd> _chunks = new List<ChunkInd>();
        protected long _dataStart;
        protected byte[] _fileBuffer;
        protected Dna _fileDna, _memoryDna;
        private bool[] _structChanged;
        protected int _version;

        public bFile(string filename)
        {
            try
            {
                _fileBuffer = File.ReadAllBytes(filename);
                ParseHeader();
            }
            catch
            {

            }
        }

        public bFile(byte[] memoryBuffer, int len)
        {
            _fileBuffer = memoryBuffer;

            ParseHeader();
        }

        protected abstract string HeaderTag { get; }
        protected BinaryReader ChunkReader { get; private set; }
        public bool OK { get; protected set; }
        public FileFlags Flags { get; protected set; }
        public Dictionary<long, byte[]> LibPointers { get; } = new Dictionary<long, byte[]>();
        public int Version => _version;

        public abstract void AddDataBlock(byte[] dataBlock);

        /*
		public void DumpChunks(bDNA dna)
		{
			bFile_dumpChunks(_native, dna._native);
		}
        */
        public byte[] FindLibPointer(long ptr)
        {
            byte[] data;
            if (LibPointers.TryGetValue(ptr, out data))
            {
                return data;
            }
            return null;
        }

        private void GetElement(BinaryReader reader, int ArrayLen, Dna.TypeDecl type, double[] data)
        {
            if (type.Name.Equals("float"))
            {
                for (int i = 0; i < ArrayLen; i++)
                {
                    data[i] = reader.ReadSingle();
                }
            }
            else
            {
                for (int i = 0; i < ArrayLen; i++)
                {
                    data[i] = reader.ReadDouble();
                }
            }
        }

        protected ChunkInd GetNextBlock(BinaryReader reader)
        {
            bool swap = (Flags & FileFlags.EndianSwap) != 0;
            bool varies = (Flags & FileFlags.BitsVaries) != 0;

            if (swap)
            {
                throw new NotImplementedException();
            }

            if (IntPtr.Size == 8)
            {
                if (varies)
                {
                    Chunk4 c = new Chunk4(reader);
                    return new ChunkInd(c);
                }
                else
                {
                    Chunk8 c = new Chunk8(reader);
                    return new ChunkInd(c);
                }
            }
            else
            {
                if (varies)
                {
                    Chunk8 c = new Chunk8(reader);
                    if (c.UniqueInt1 == c.UniqueInt2)
                    {
                        c.UniqueInt2 = 0;
                    }
                    else
                    {
                        throw new NotImplementedException();
                    }
                    return new ChunkInd(c);
                }
                else
                {
                    Chunk4 c = new Chunk4(reader);
                    return new ChunkInd(c);
                }
            }
        }

        public abstract void Parse(FileVerboseMode verboseMode);
        protected abstract void ReadChunks();

        public virtual void ParseData()
        {
            //Console.WriteLine("Building datablocks");
            //Console.WriteLine("Chunk size = {0}", CHUNK_HEADER_LEN);
            //Console.WriteLine("File chunk size = {0}", ChunkUtils.GetOffset(_flags));

            bool swap = (Flags & FileFlags.EndianSwap) != 0;
            if (swap)
            {
                throw new NotImplementedException();
                //swapLen(dataPtr);
            }

            using (var memory = new MemoryStream(_fileBuffer, false))
            {
                using (ChunkReader = new BinaryReader(memory))
                {
                    ReadChunks();
                }
                ChunkReader = null;
            }
        }

        protected void ParseHeader()
        {
            string header = Encoding.UTF8.GetString(_fileBuffer, 0, SizeOfBlenderHeader);
            if (header.Substring(0, 6) != HeaderTag)
            {
                return;
            }

            if (header[6] == 'd')
            {
                Flags |= FileFlags.DoublePrecision;
            }

            int.TryParse(header.Substring(9), out _version);

            // swap ptr sizes...
            if (header[7] == '-')
            {
                Flags |= FileFlags.File64;
                if (IntPtr.Size != 8)
                    Flags |= FileFlags.BitsVaries;
            }
            else if (IntPtr.Size == 8)
            {
                Flags |= FileFlags.BitsVaries;
            }

            // swap endian...
            if (header[8] == 'V')
            {
                if (BitConverter.IsLittleEndian)
                    Flags |= FileFlags.EndianSwap;
            }
            else
            {
                if (!BitConverter.IsLittleEndian)
                    Flags |= FileFlags.EndianSwap;
            }

            OK = true;
        }

        protected void ParseInternal(FileVerboseMode verboseMode)
        {
            if (!OK) return;

            LoadDna(verboseMode);
            if (!OK) return;

            ParseData();
            //ResolvePointers(verboseMode);
        }

        private void LoadDna(FileVerboseMode verboseMode)
        {
            bool swap = (Flags & FileFlags.EndianSwap) != 0;

            using (var stream = new MemoryStream(_fileBuffer, false))
            {
                using (var reader = new BulletReader(stream))
                {
                    long dnaStart = FindDnaChunk(reader);
                    OK = dnaStart != -1;
                    if (!OK)
                    {
                        return;
                    }

                    stream.Position = dnaStart;
                    _fileDna = Dna.Load(reader, swap);
                }
            }

            if (_fileDna.IsBroken(_version))
            {
                Console.WriteLine("Warning: broken DNA version");
                Flags |= FileFlags.BrokenDna;
            }

            //if ((verboseMode & FileVerboseMode.DumpDnaTypeDefinitions) != 0)
            //    _fileDna.DumpTypeDefinitions();

            byte[] memoryDnaData = IntPtr.Size == 8
                ? Serializer.GetBulletDna64()
                : Serializer.GetBulletDna();
            _memoryDna = Dna.Load(memoryDnaData, !BitConverter.IsLittleEndian);

            _structChanged = _fileDna.Compare(_memoryDna);
        }

        private long FindDnaChunk(BinaryReader reader)
        {
            var stream = reader.BaseStream;

            int i = 0;
            while (i < stream.Length)
            {
                stream.Position = i;

                // looking for the data's starting position
                // and the start of SDNA decls
                byte[] codeData = reader.ReadBytes(4);
                string code = Encoding.ASCII.GetString(codeData);

                if (_dataStart == 0 && code.Equals("REND"))
                {
                    _dataStart = stream.Position;
                }

                if (code == "DNA1")
                {
                    // read the DNA1 block and extract SDNA
                    stream.Position = i;
                    if (GetNextBlock(reader) != null)
                    {
                        string sdnaname = Encoding.ASCII.GetString(reader.ReadBytes(8));
                        if (sdnaname == "SDNANAME")
                        {
                            return i + ChunkUtils.GetChunkSize(Flags);
                        }
                    }
                }
                else if (code == "SDNA")
                {
                    // Some Bullet files are missing the DNA1 block
                    // In Blender it's DNA1 + ChunkUtils.GetOffset() + SDNA + NAME
                    // In Bullet tests its SDNA + NAME
                    return i;
                }

                i++;
            }
            Console.WriteLine("Failed to find DNA1+SDNA pair");
            return -1;
        }

        protected byte[] ReadChunkData(ChunkInd dataChunk, long chunkDataOffset)
        {
            //bool ignoreEndianFlag = false;

            if ((Flags & FileFlags.EndianSwap) != 0)
            {
                //swap(head, dataChunk, ignoreEndianFlag);
            }

            if (StructChanged(dataChunk.StructIndex))
            {
                // Ouch! need to rebuild the struct
                Dna.StructDecl oldStruct = _fileDna.GetStruct(dataChunk.StructIndex);

                if ((Flags & FileFlags.BrokenDna) != 0)
                {
                    if (oldStruct.Type.Name.Equals("btQuantizedBvhNodeData") && oldStruct.Type.Length == 28)
                    {
                        throw new NotImplementedException();
                    }

                    if (oldStruct.Type.Name.Equals("btShortIntIndexData"))
                    {
                        throw new NotImplementedException();
                    }
                }

                // Don't try to convert Link block data, just memcpy it. Other data can be converted.
                if (oldStruct.Type.Name.Equals("Link"))
                {
                    //Console.WriteLine("Link found");
                }
                else
                {
                    Dna.StructDecl curStruct = _memoryDna.GetStruct(oldStruct.Type.Name);
                    if (curStruct != null)
                    {
                        byte[] structAlloc = new byte[dataChunk.NumBlocks * curStruct.Type.Length];
                        AddDataBlock(structAlloc);
                        using (var stream = new MemoryStream(structAlloc))
                        {
                            using (var writer = new BinaryWriter(stream))
                            {
                                long structOffset = chunkDataOffset;
                                for (int block = 0; block < dataChunk.NumBlocks; block++)
                                {
                                    ParseStruct(writer, curStruct, oldStruct, structOffset);
                                    structOffset += oldStruct.Type.Length;
                                }
                            }
                        }
                        return structAlloc;
                    }
                }
            }
            else
            {
#if DEBUG_EQUAL_STRUCTS
#endif
            }

            byte[] dataAlloc = new byte[dataChunk.Length];
            ChunkReader.Read(dataAlloc, 0, dataChunk.Length);
            return dataAlloc;
        }

        protected void ParseStruct(BinaryWriter writer, Dna.StructDecl memoryStruct, Dna.StructDecl fileStruct, long structOffset)
        {
            Debug.Assert(memoryStruct != null);
            Debug.Assert(fileStruct != null);

            foreach (Dna.ElementDecl element in memoryStruct.Elements)
            {
                if (element.Type.Struct != null && element.NameInfo.Name[0] != '*')
                {
                    ParseElement(writer, fileStruct, element, structOffset);
                }
                else
                {
                    WriteElement(writer, fileStruct, element, structOffset);
                }
            }
        }

        private void ParseElement(BinaryWriter writer, Dna.StructDecl fileStruct, Dna.ElementDecl memoryElement, long structOffset)
        {
            long elementOffset;
            Dna.ElementDecl elementOld = GetFileElement(fileStruct, memoryElement, out elementOffset);
            if (elementOld != null)
            {
                Dna.StructDecl typeStructOld = _fileDna.GetStruct(memoryElement.Type.Name);
                int arrayLength = elementOld.NameInfo.ArrayLength;
                for (int i = 0; i < arrayLength; i++)
                {
                    long subStructOffset = structOffset + i * typeStructOld.Type.Length + elementOffset;
                    ParseStruct(writer, memoryElement.Type.Struct, typeStructOld, subStructOffset);
                }
            }
        }

        protected Dna.ElementDecl GetFileElement(Dna.StructDecl fileStruct, Dna.ElementDecl lookupElement, out long elementOffset)
        {
            elementOffset = 0;
            foreach (Dna.ElementDecl element in fileStruct.Elements)
            {
                if (element.NameInfo.Equals(lookupElement.NameInfo))
                {
                    if (element.Type.Name.Equals(lookupElement.Type.Name))
                    {
                        return element;
                    }
                    break;
                }
                elementOffset += _fileDna.GetElementSize(element);
            }
            return null;
        }

        protected void WriteElement(BinaryWriter writer, Dna.StructDecl fileStruct, Dna.ElementDecl memoryElement, long structOffset)
        {
            bool brokenDna = (Flags & FileFlags.BrokenDna) != 0;

            int elementOffset;
            Dna.ElementDecl fileElement = fileStruct.FindElement(_fileDna, brokenDna, memoryElement.NameInfo, out elementOffset);

            ChunkReader.BaseStream.Position = structOffset + elementOffset;

            if (fileElement == null)
            {
                int elementLength = _memoryDna.GetElementSize(memoryElement);
                writer.BaseStream.Position += elementLength;
            }
            else if (fileElement.NameInfo.Name[0] == '*')
            {
                SafeSwapPtr(writer, ChunkReader);
            }
            else if (fileElement.Type.Name.Equals(memoryElement.Type.Name))
            {
                int elementLength = _fileDna.GetElementSize(fileElement);
                if (elementLength != _memoryDna.GetElementSize(memoryElement))
                {
                    throw new InvalidDataException();
                }
                byte[] mem = new byte[elementLength];
                ChunkReader.Read(mem, 0, elementLength);
                writer.Write(mem);
            }
            else
            {
                throw new InvalidDataException();
                //GetElement(arrayLen, lookupType, type, data, strcData);
            }
        }

        public void PreSwap()
        {
            throw new NotImplementedException();
        }

        public void ResolvePointers(FileVerboseMode verboseMode)
        {
            Dna fileDna = (_fileDna != null) ? _fileDna : _memoryDna;

            if (true) // && ((_flags & FileFlags.BitsVaries | FileFlags.VersionVaries) != 0))
            {
                //ResolvePointersMismatch();
            }

            if ((verboseMode & FileVerboseMode.ExportXml) != 0)
            {
                Console.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                Console.WriteLine("<bullet_physics version=\"{0}\" itemcount=\"{1}\">", Version, _chunks.Count);
            }

            foreach (ChunkInd dataChunk in _chunks)
            {
                if (_fileDna == null || !StructChanged(dataChunk.StructIndex))
                {
                    Dna.StructDecl oldStruct = fileDna.GetStruct(dataChunk.StructIndex);

                    if ((verboseMode & FileVerboseMode.ExportXml) != 0)
                    {
                        Console.WriteLine(" <{0} pointer=\"{1}\">", oldStruct.Type.Name, dataChunk.OldPtr);
                    }

                    ResolvePointersChunk(dataChunk, verboseMode);

                    if ((verboseMode & FileVerboseMode.ExportXml) != 0)
                    {
                        Console.WriteLine(" </{0}>", oldStruct.Type.Name);
                    }
                }
                else
                {
                    //Console.WriteLine("skipping struct");
                }
            }

            if ((verboseMode & FileVerboseMode.ExportXml) != 0)
            {
                Console.WriteLine("</bullet_physics>");
            }
        }

        protected void ResolvePointersChunk(ChunkInd dataChunk, FileVerboseMode verboseMode)
        {
            Dna fileDna = (_fileDna != null) ? _fileDna : _memoryDna;

            Dna.StructDecl oldStruct = fileDna.GetStruct(dataChunk.StructIndex);
            int oldLen = oldStruct.Type.Length;

            byte[] cur = FindLibPointer(dataChunk.OldPtr);
            using (var stream = new MemoryStream(cur, false))
            {
                using (var reader = new BinaryReader(stream))
                {
                    for (int block = 0; block < dataChunk.NumBlocks; block++)
                    {
                        long streamPosition = stream.Position;
                        ResolvePointersStructRecursive(reader, fileDna.GetStruct(dataChunk.StructIndex), verboseMode, 1);
                        Debug.Assert(stream.Position == streamPosition + oldLen);
                    }
                }
            }
        }

        protected int ResolvePointersStructRecursive(BinaryReader reader, Dna.StructDecl oldStruct, FileVerboseMode verboseMode, int recursion)
        {
            Dna fileDna = (_fileDna != null) ? _fileDna : _memoryDna;

            int totalSize = 0;

            foreach (Dna.ElementDecl element in oldStruct.Elements)
            {
                int size = fileDna.GetElementSize(element);
                int arrayLen = element.NameInfo.ArrayLength;

                if (element.NameInfo.Name[0] == '*')
                {
                    if (arrayLen > 1)
                    {
                        throw new NotImplementedException();
                    }
                    else
                    {
                        long ptr = (IntPtr.Size == 8) ? reader.ReadInt64() : reader.ReadInt32();
                        if ((verboseMode & FileVerboseMode.ExportXml) != 0)
                        {
                            for (int i = 0; i < recursion; i++)
                            {
                                Console.Write("  ");
                            }
                            Console.WriteLine("<{0} type=\"pointer\"> {1} </{0}>", element.NameInfo.Name.Substring(1), ptr);
                        }
                        byte[] ptrChunk = FindLibPointer(ptr);
                        if (ptrChunk != null)
                        {
                            //throw new NotImplementedException();
                        }
                        else
                        {
                            //Console.WriteLine("Cannot fixup pointer at {0} from {1} to {2}!", ptrptr, *ptrptr, ptr);
                        }
                    }
                }
                else
                {
                    if (element.Type.Struct != null)
                    {
                        if ((verboseMode & FileVerboseMode.ExportXml) != 0)
                        {
                            for (int i = 0; i < recursion; i++)
                            {
                                Console.Write("  ");
                            }

                            if (arrayLen > 1)
                            {
                                Console.WriteLine("<{0} type=\"{1}\" count={2}>", element.NameInfo.CleanName, element.Type.Name, arrayLen);
                            }
                            else
                            {
                                Console.WriteLine("<{0} type=\"{1}\">", element.NameInfo.CleanName, element.Type.Name);
                            }
                        }

                        for (int i = 0; i < arrayLen; i++)
                        {
                            Dna.StructDecl revType = _fileDna.GetStruct(element.Type.Name);
                            ResolvePointersStructRecursive(reader, revType, verboseMode, recursion + 1);
                            //throw new NotImplementedException();
                        }

                        if ((verboseMode & FileVerboseMode.ExportXml) != 0)
                        {
                            for (int i = 0; i < recursion; i++)
                            {
                                Console.Write("  ");
                            }
                            Console.WriteLine("</{0}>", element.NameInfo.CleanName);
                        }
                    }
                    else
                    {
                        // export a simple type
                        if ((verboseMode & FileVerboseMode.ExportXml) != 0)
                        {
                            if (arrayLen > MaxArrayLength)
                            {
                                Console.WriteLine("too long");
                            }
                            else
                            {
                                bool isIntegerType;
                                switch (element.Type.Name)
                                {
                                    case "char":
                                    case "short":
                                    case "int":
                                        isIntegerType = true;
                                        break;
                                    default:
                                        isIntegerType = false;
                                        break;
                                }

                                if (isIntegerType)
                                {
                                    throw new NotImplementedException();
                                    /*
                                    const char* newtype="int";
							        int dbarray[MAX_ARRAY_LENGTH];
							        int* dbPtr = 0;
							        char* tmp = elemPtr;
							        dbPtr = &dbarray[0];
							        if (dbPtr)
							        {
								        char cleanName[MAX_STRLEN];
								        getCleanName(memName,cleanName);

								        int i;
								        getElement(arrayLen, newtype,memType, tmp, (char*)dbPtr);
								        for (i=0;i<recursion;i++)
									        printf("  ");
								        if (arrayLen==1)
									        printf("<%s type=\"%s\">",cleanName,memType);
								        else
									        printf("<%s type=\"%s\" count=%d>",cleanName,memType,arrayLen);
								        for (i=0;i<arrayLen;i++)
									        printf(" %d ",dbPtr[i]);
								        printf("</%s>\n",cleanName);
							        }*/
                                }
                                else
                                {
                                    double[] dbArray = new double[arrayLen];
                                    GetElement(reader, arrayLen, element.Type, dbArray);
                                    for (int i = 0; i < recursion; i++)
                                    {
                                        Console.Write("  ");
                                    }
                                    if (arrayLen == 1)
                                    {
                                        Console.Write("<{0} type=\"{1}\">", element.NameInfo.Name, element.Type.Name);
                                    }
                                    else
                                    {
                                        Console.Write("<{0} type=\"{1}\" count=\"{2}\">", element.NameInfo.CleanName, element.Type.Name, arrayLen);
                                    }
                                    for (int i = 0; i < arrayLen; i++)
                                    {
                                        Console.Write(" {0} ", dbArray[i].ToString(CultureInfo.InvariantCulture));
                                    }
                                    Console.WriteLine("</{0}>", element.NameInfo.CleanName);
                                }
                            }
                        }
                        reader.BaseStream.Position += size;
                    }
                }
                totalSize += size;
            }

            return totalSize;
        }

        protected void SafeSwapPtr(BinaryWriter writer, BinaryReader reader)
        {
            int filePtrSize = _fileDna.PointerSize;
            int memPtrSize = _memoryDna.PointerSize;

            if (filePtrSize == memPtrSize)
            {
                byte[] mem = new byte[memPtrSize];
                reader.Read(mem, 0, memPtrSize);
                writer.Write(mem);
            }
            else if (memPtrSize == 4 && filePtrSize == 8)
            {
                int uniqueId1 = reader.ReadInt32();
                int uniqueId2 = reader.ReadInt32();
                if (uniqueId1 == uniqueId2)
                {
                    writer.Write(uniqueId1);
                    reader.BaseStream.Position -= 4;
                }
                else
                {
                    reader.BaseStream.Position -= 8;
                    long longValue = reader.ReadInt64();
                    reader.BaseStream.Position -= 4;
                    if ((Flags & FileFlags.EndianSwap) != 0)
                    {
                        throw new NotImplementedException();
                    }
                    longValue = longValue >> 3;
                    int intValue = (int)longValue;
                    writer.Write(intValue);
                }
            }
            else if (memPtrSize == 8 && filePtrSize == 4)
            {
                int uniqueId1 = reader.ReadInt32();
                int uniqueId2 = reader.ReadInt32();
                reader.BaseStream.Position -= 4;
                if (uniqueId1 == uniqueId2)
                {
                    writer.Write(uniqueId1);
                    writer.Write(0);
                }
                else
                {
                    writer.Write((long)uniqueId1);
                }
            }
            else
            {
                Console.WriteLine("Invalid pointer len {0} {1}", filePtrSize, memPtrSize);
            }
        }

        protected bool StructChanged(int structIndex)
        {
            Debug.Assert(structIndex < _structChanged.Length);
            return _structChanged[structIndex];
        }
        /*
		public int Write(char fileName, bool fixupPointers)
		{
			return bFile_write(_native, fileName._native, fixupPointers);
		}

		public int Write(char fileName)
		{
			return bFile_write2(_native, fileName._native);
		}

		public void WriteChunks(FILE fp, bool fixupPointers)
		{
			bFile_writeChunks(_native, fp._native, fixupPointers);
		}

		public void WriteDNA(FILE fp)
		{
			bFile_writeDNA(_native, fp._native);
		}

		public void WriteFile(char fileName)
		{
			bFile_writeFile(_native, fileName._native);
		}

		public bDNA FileDNA
		{
			get { return bFile_getFileDNA(_native); }
		}
        */
    }
}
