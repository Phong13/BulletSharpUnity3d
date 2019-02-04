using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace BulletSharp
{
    [Flags]
    public enum FileFlags
    {
        None = 0x0,
        OK = 0x1,
        VoidIs8 = 0x2,
        EndianSwap = 0x4,
        File64 = 0x8,
        BitsVaries = 0x10,
        VersionVaries = 0x20,
        DoublePrecision = 0x40,
        BrokenDna = 0x80
    }

    [Flags]
    public enum FileVerboseMode
    {
        None = 0x0,
        ExportXml = 0x1,
        DumpDnaTypeDefinitions = 0x2,
        DumpChunks = 0x4,
        DumpFileInfo = 0x8
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
        const int SizeOfBlenderHeader = 12;
        const int MaxArrayLength = 512;
        const int MaxStringLength = 1024;

        protected List<ChunkInd> _chunks = new List<ChunkInd>();
        protected long _dataStart;
        protected byte[] _fileBuffer;
        protected Dna _fileDna;
        protected FileFlags _flags;
        protected string _headerString;
        protected Dictionary<long, byte[]> _libPointers = new Dictionary<long,byte[]>();
        protected Dna _memoryDna;
        protected int _version;

        public bFile(string filename, string headerString)
        {
            _headerString = headerString;
            try
            {
                _fileBuffer = File.ReadAllBytes(filename);
                ParseHeader();
            }
            catch
            {

            }
        }

        public bFile(byte[] memoryBuffer, int len, string headerString)
        {
            _headerString = headerString;
            _fileBuffer = memoryBuffer;

            ParseHeader();
        }

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

        protected long GetFileElement(Dna.StructDecl firstStruct, Dna.ElementDecl lookupElement, long data, out Dna.ElementDecl found)
        {
            foreach (Dna.ElementDecl element in firstStruct.Elements)
            {
                if (element.Name.Equals(lookupElement.Name))
                {
                    if (element.Type.Name.Equals(lookupElement.Type.Name))
                    {
                        found = element;
                        return data;
                    }
                    found = null;
                    return 0;
                }
                data += _fileDna.GetElementSize(element);
            }
            found = null;
            return 0;
        }

        protected void GetMatchingFileDna(Dna.StructDecl dna, Dna.ElementDecl lookupElement, BinaryWriter strcData, long data, bool fixupPointers)
        {
            // find the matching memory dna data
            // to the file being loaded. Fill the
            // memory with the file data...

            MemoryStream dataStream = new MemoryStream(_fileBuffer, false);
            BinaryReader dataReader = new BinaryReader(dataStream);

            foreach (Dna.ElementDecl element in dna.Elements)
            {
                int eleLen = _fileDna.GetElementSize(element);
                if ((_flags & FileFlags.BrokenDna) != 0)
                {
                    if (element.Type.Name.Equals("short") && element.Name.Name.Equals("int"))
                    {
                        eleLen = 0;
                    }
                }

                if (lookupElement.Name.Equals(element.Name))
                {
                    int arrayLen = element.Name.ArraySizeNew;

                    dataStream.Position = data;

                    if (element.Name.Name[0] == '*')
                    {
                        SafeSwapPtr(strcData, dataReader);

                        if (fixupPointers)
                        {
                            if (arrayLen > 1)
                            {
                                throw new NotImplementedException();
                            }
                            else
                            {
                                if (element.Name.Name[1] == '*')
                                {
                                    throw new NotImplementedException();
                                }
                                else
                                {
                                    //_chunkPointerFixupArray.Add(strcData.BaseStream.Position);
                                }
                            }
                        }
                        else
                        {
                            //Console.WriteLine("skipped {0} {1} : {2:X}", element.Type.Name, element.Name.Name, strcData.BaseStream.Position);
                        }
                    }
                    else if (element.Type.Name.Equals(lookupElement.Type.Name))
                    {
                        byte[] mem = new byte[eleLen];
                        dataReader.Read(mem, 0, eleLen);
                        strcData.Write(mem);
                    }
                    else
                    {
                        throw new NotImplementedException();
                        //GetElement(arrayLen, lookupType, type, data, strcData);
                    }
                    break;
                }
                data += eleLen;
            }

            dataReader.Dispose();
            dataStream.Dispose();
        }

        // buffer offset util
		protected int GetNextBlock(out ChunkInd dataChunk, BinaryReader reader, FileFlags flags)
        {
            bool swap = (flags & FileFlags.EndianSwap) != 0;
            bool varies = (flags & FileFlags.BitsVaries) != 0;

            if (swap)
            {
                throw new NotImplementedException();
            }

            dataChunk = new ChunkInd();

            if (IntPtr.Size == 8)
            {
                if (varies)
                {
                    ChunkPtr4 c = new ChunkPtr4(reader);
                    dataChunk = new ChunkInd(ref c);
                }
                else
                {
                    ChunkPtr8 c = new ChunkPtr8(reader);
                    dataChunk = new ChunkInd(ref c);
                }
            }
            else
            {
                if (varies)
                {
                    ChunkPtr8 c = new ChunkPtr8(reader);
                    if (c.UniqueInt1 == c.UniqueInt2)
                    {
                        c.UniqueInt2 = 0;
                    }
                    else
                    {
                        throw new NotImplementedException();
                    }
                    dataChunk = new ChunkInd(ref c);
                }
                else
                {
                    ChunkPtr4 c = new ChunkPtr4(reader);
                    dataChunk = new ChunkInd(ref c);
                }
            }

            if (dataChunk.Length < 0)
                return -1;

            return dataChunk.Length + ChunkUtils.GetOffset(flags);
        }

		public bool OK
		{
            get { return (_flags & FileFlags.OK) != 0; }
		}

		public abstract void Parse(FileVerboseMode verboseMode);
        public abstract void ParseData();

        protected void ParseHeader()
        {
            string header = Encoding.UTF8.GetString(_fileBuffer, 0, SizeOfBlenderHeader);
            if (!header.Substring(0, 6).Equals(_headerString.Substring(0, 6)))
            {
                return;
            }

            if (header[6] == 'd')
            {
                _flags |= FileFlags.DoublePrecision;
            }

            int.TryParse(header.Substring(9), out _version);

            // swap ptr sizes...
	        if (header[7] == '-')
	        {
		        _flags |= FileFlags.File64;
		        if (IntPtr.Size != 8)
			        _flags |= FileFlags.BitsVaries;
	        }
            else if (IntPtr.Size == 8)
            {
                _flags |= FileFlags.BitsVaries;
            }

            // swap endian...
            if (header[8] == 'V')
            {
                if (BitConverter.IsLittleEndian)
                    _flags |= FileFlags.EndianSwap;
            }
            else
            {
                if (!BitConverter.IsLittleEndian)
                    _flags |= FileFlags.EndianSwap;
            }

            _flags |= FileFlags.OK;
        }

        protected void ParseInternal(FileVerboseMode verboseMode, byte[] memDna)
        {
            if ((_flags & FileFlags.OK) != FileFlags.OK)
            {
                return;
            }

            ChunkInd dna = new ChunkInd();
            dna.OldPtr = 0;

            MemoryStream memory = new MemoryStream(_fileBuffer, false);
            BinaryReader reader = new BinaryReader(memory);

            int i = 0;
            while (memory.Position < memory.Length)
            {
                // looking for the data's starting position
                // and the start of SDNA decls

                byte[] code = reader.ReadBytes(4);
                string codes = ASCIIEncoding.ASCII.GetString(code);

                if (_dataStart == 0 && codes.Equals("REND"))
                {
                    _dataStart = memory.Position;
                }

                if (codes.Equals("DNA1"))
                {
                    // read the DNA1 block and extract SDNA
                    reader.BaseStream.Position = i;
                    if (GetNextBlock(out dna, reader, _flags) > 0)
                    {
                        string sdnaname = ASCIIEncoding.ASCII.GetString(reader.ReadBytes(8));
                        if (sdnaname.Equals("SDNANAME"))
                        {
                            dna.OldPtr = i + ChunkUtils.GetOffset(_flags);
                        }
                        else
                        {
                            dna.OldPtr = 0;
                        }
                    }
                    else
                    {
                        dna.OldPtr = 0;
                    }
                }
                else if (codes.Equals("SDNA"))
                {
                    // Some Bullet files are missing the DNA1 block
                    // In Blender it's DNA1 + ChunkUtils::getOffset() + SDNA + NAME
                    // In Bullet tests its SDNA + NAME

                    dna.OldPtr = i;
                    dna.Length = (int)memory.Length - i;

                    // Also no REND block, so exit now.
                    if (_version == 276)
                    {
                        break;
                    }
                }

                if (_dataStart != 0 && dna.OldPtr != 0)
                {
                    break;
                }

                i++;
                memory.Position = i;
            }

            if (dna.OldPtr == 0 || dna.Length == 0)
            {
                //Console.WriteLine("Failed to find DNA1+SDNA pair");
                _flags &= ~FileFlags.OK;
                reader.Dispose();
                memory.Dispose();
                return;
            }

            _fileDna = new Dna();

            // _fileDna.Init will convert part of DNA file endianness to current CPU endianness if necessary
            memory.Position = dna.OldPtr;
            _fileDna.Init(reader, (_flags & FileFlags.EndianSwap) != 0);

            if (_version == 276)
            {
                for (i = 0; i < _fileDna.NumNames; i++)
                {
                    if (_fileDna.GetName(i).Equals("int"))
                    {
                        _flags |= FileFlags.BrokenDna;
                    }
                }
                if ((_flags & FileFlags.BrokenDna) == FileFlags.BrokenDna)
                {
                    //Console.WriteLine("warning: fixing some broken DNA version");
                }
            }

            //if ((verboseMode & FileVerboseMode.DumpDnaTypeDefinitions) == FileVerboseMode.DumpDnaTypeDefinitions)
            //    _fileDna.DumpTypeDefinitions();

            _memoryDna = new Dna();
            using (MemoryStream memory2 = new MemoryStream(memDna, false))
            {
                using (BinaryReader reader2 = new BinaryReader(memory2))
                {
                    _memoryDna.Init(reader2, !BitConverter.IsLittleEndian);
                }
            }

            if (_memoryDna.NumNames != _fileDna.NumNames)
            {
                _flags |= FileFlags.VersionVaries;
                //Console.WriteLine ("Warning, file DNA is different than built in, performance is reduced. Best to re-export file with a matching version/platform");
            }

            if (_memoryDna.LessThan(_fileDna))
            {
                //Console.WriteLine ("Warning, file DNA is newer than built in.");
            }

            _fileDna.InitCmpFlags(_memoryDna);

            ParseData();

            ResolvePointers(verboseMode);

            UpdateOldPointers();

            reader.Dispose();
            memory.Dispose();
        }

        protected void ParseStruct(BinaryWriter strc, BinaryReader data, Dna.StructDecl fileStruct, Dna.StructDecl memoryStruct, bool fixupPointers)
        {
            if (fileStruct == null) return;
            if (memoryStruct == null) return;

            long dataPtr = data.BaseStream.Position;
            long strcPtr = strc.BaseStream.Position;

            foreach (Dna.ElementDecl element in memoryStruct.Elements)
            {
                int memorySize = _memoryDna.GetElementSize(element);
                if (element.Type.Struct != null && element.Name.Name[0] != '*')
                {
                    Dna.ElementDecl elementOld;
                    long elementOffset = GetFileElement(fileStruct, element, dataPtr, out elementOld);
                    if (elementOffset != 0)
                    {
                        Dna.StructDecl oldStruct = _fileDna.GetStruct(_fileDna.GetReverseType(element.Type.Name));
                        data.BaseStream.Position = elementOffset;
                        int arrayLen = elementOld.Name.ArraySizeNew;
                        if (arrayLen == 1)
                        {
                            strc.BaseStream.Position = strcPtr;
                            ParseStruct(strc, data, oldStruct, element.Type.Struct, fixupPointers);
                            strcPtr += memorySize;
                        }
                        else
                        {
                            int fileSize = _fileDna.GetElementSize(elementOld) / arrayLen;
                            memorySize /= arrayLen;
                            for (int i = 0; i < arrayLen; i++)
                            {
                                strc.BaseStream.Position = strcPtr;
                                ParseStruct(strc, data, oldStruct, element.Type.Struct, fixupPointers);
                                data.BaseStream.Position += fileSize;
                                strcPtr += memorySize;
                            }
                        }
                    }
                }
                else
                {
                    strc.BaseStream.Position = strcPtr;
                    GetMatchingFileDna(fileStruct, element, strc, dataPtr, fixupPointers);
                    strcPtr += memorySize;
                }
            }
        }

		public void PreSwap()
		{
            throw new NotImplementedException();
		}

        protected byte[] ReadStruct(BinaryReader head, ChunkInd dataChunk)
        {
            //bool ignoreEndianFlag = false;

            if ((_flags & FileFlags.EndianSwap) == FileFlags.EndianSwap)
            {
                //swap(head, dataChunk, ignoreEndianFlag);
            }

            if (!_fileDna.FlagEqual(dataChunk.DnaNR))
            {
                // Ouch! need to rebuild the struct
                Dna.StructDecl oldStruct = _fileDna.GetStruct(dataChunk.DnaNR);

                if ((_flags & FileFlags.BrokenDna) != 0)
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
                    int reverseOld = _memoryDna.GetReverseType(oldStruct.Type.Name);
                    if (reverseOld != -1)
                    {
                        Dna.StructDecl curStruct = _memoryDna.GetStruct(reverseOld);
                        byte[] structAlloc = new byte[dataChunk.NR * curStruct.Type.Length];
                        AddDataBlock(structAlloc);
                        using (MemoryStream stream = new MemoryStream(structAlloc))
                        {
                            using (BinaryWriter writer = new BinaryWriter(stream))
                            {
                                long headerPtr = head.BaseStream.Position;
                                for (int block = 0; block < dataChunk.NR; block++)
                                {
                                    head.BaseStream.Position = headerPtr;
                                    ParseStruct(writer, head, oldStruct, curStruct, true);
                                    headerPtr += oldStruct.Type.Length;
                                    //_libPointers.Add(old, cur);
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
            head.Read(dataAlloc, 0, dataAlloc.Length);
            return dataAlloc;
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
                if (_fileDna == null || fileDna.FlagEqual(dataChunk.DnaNR))
                {
                    Dna.StructDecl oldStruct = fileDna.GetStruct(dataChunk.DnaNR);

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

            Dna.StructDecl oldStruct = fileDna.GetStruct(dataChunk.DnaNR);
            int oldLen = oldStruct.Type.Length;

            byte[] cur = FindLibPointer(dataChunk.OldPtr);
            using (MemoryStream stream = new MemoryStream(cur, false))
            {
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    for (int block = 0; block < dataChunk.NR; block++)
                    {
                        long streamPosition = stream.Position;
                        ResolvePointersStructRecursive(reader, dataChunk.DnaNR, verboseMode, 1);
                        System.Diagnostics.Debug.Assert(stream.Position == streamPosition + oldLen);
                    }
                }
            }
        }

        protected int ResolvePointersStructRecursive(BinaryReader reader, int dnaNr, FileVerboseMode verboseMode, int recursion)
        {
            Dna fileDna = (_fileDna != null) ? _fileDna : _memoryDna;
            Dna.StructDecl oldStruct = fileDna.GetStruct(dnaNr);

            int totalSize = 0;

            foreach (Dna.ElementDecl element in oldStruct.Elements)
            {
                int size = fileDna.GetElementSize(element);
                int arrayLen = element.Name.ArraySizeNew;

                if (element.Name.Name[0] == '*')
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
                            Console.WriteLine("<{0} type=\"pointer\"> {1} </{0}>", element.Name.Name.Substring(1), ptr);
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
                                Console.WriteLine("<{0} type=\"{1}\" count={2}>", element.Name.CleanName, element.Type.Name, arrayLen);
                            }
                            else
                            {
                                Console.WriteLine("<{0} type=\"{1}\">", element.Name.CleanName, element.Type.Name);
                            }
                        }

                        for (int i = 0; i < arrayLen; i++)
                        {
                            int revType = _fileDna.GetReverseType(element.Type.Name);
                            ResolvePointersStructRecursive(reader, revType, verboseMode, recursion + 1);
                            //throw new NotImplementedException();
                        }

                        if ((verboseMode & FileVerboseMode.ExportXml) != 0)
                        {
                            for (int i = 0; i < recursion; i++)
                            {
                                Console.Write("  ");
                            }
                            Console.WriteLine("</{0}>", element.Name.CleanName);
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
                                        Console.Write("<{0} type=\"{1}\">", element.Name.Name, element.Type.Name);
                                    }
                                    else
                                    {
                                        Console.Write("<{0} type=\"{1}\" count=\"{2}\">", element.Name.CleanName, element.Type.Name, arrayLen);
                                    }
                                    for (int i = 0; i < arrayLen; i++)
                                    {
                                        Console.Write(" {0} ", dbArray[i].ToString(CultureInfo.InvariantCulture));
                                    }
                                    Console.WriteLine("</{0}>", element.Name.CleanName);
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

        protected void SafeSwapPtr(BinaryWriter strcData, BinaryReader data)
        {
            int ptrFile = _fileDna.PointerSize;
            int ptrMem = _memoryDna.PointerSize;

            if (ptrFile == ptrMem)
            {
                byte[] mem = new byte[ptrMem];
                data.Read(mem, 0, ptrMem);
                strcData.Write(mem);
            }
            else if (ptrMem == 4 && ptrFile == 8)
            {
                int uniqueId1 = data.ReadInt32();
                int uniqueId2 = data.ReadInt32();
                if (uniqueId1 == uniqueId2)
                {
                    strcData.Write(uniqueId1);
                    data.BaseStream.Position -= 4;
                }
                else
                {
                    data.BaseStream.Position -= 8;
                    long longValue = data.ReadInt64();
                    data.BaseStream.Position -= 4;
                    if ((Flags & FileFlags.EndianSwap) != 0)
                    {
                        throw new NotImplementedException();
                    }
                    longValue = longValue >> 3;
                    int intValue = (int) longValue;
                    strcData.Write(intValue);
                }
            }
            else if (ptrMem == 8 && ptrFile == 4)
            {
                int uniqueId1 = data.ReadInt32();
                int uniqueId2 = data.ReadInt32();
                data.BaseStream.Position -= 4;
                if (uniqueId1 == uniqueId2)
                {
                    strcData.Write(uniqueId1);
                    strcData.Write(0);
                }
                else
                {
                    strcData.Write((long)uniqueId1);
                }
            }
            else
            {
                Console.WriteLine("Invalid pointer len {0} {1}", ptrFile, ptrMem);
            }
        }

		public void UpdateOldPointers()
		{
            for (int i = 0; i < _chunks.Count; i++)
            {
                //_chunks[i].OldPtr
                byte[] data = FindLibPointer(_chunks[i].OldPtr);
                data.ToString();
            }
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
        public FileFlags Flags
		{
            get { return _flags; }
		}
        
		public Dictionary<long, byte[]> LibPointers
		{
            get { return _libPointers; }
		}
        
		public int Version
		{
            get { return _version; }
		}
	}
}
