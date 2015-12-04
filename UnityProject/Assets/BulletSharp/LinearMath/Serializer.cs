using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace BulletSharp
{
	public class Chunk
	{
		internal IntPtr _native;
        private bool _preventDelete;

		internal Chunk(IntPtr native, bool preventDelete)
		{
			_native = native;
            _preventDelete = preventDelete;
		}

		public Chunk()
		{
			_native = btChunk_new();
		}

		public int ChunkCode
		{
			get { return btChunk_getChunkCode(_native); }
			set { btChunk_setChunkCode(_native, value); }
		}

		public int DnaNr
		{
			get { return btChunk_getDna_nr(_native); }
			set { btChunk_setDna_nr(_native, value); }
		}

		public int Length
		{
			get { return btChunk_getLength(_native); }
			set { btChunk_setLength(_native, value); }
		}

		public int Number
		{
			get { return btChunk_getNumber(_native); }
			set { btChunk_setNumber(_native, value); }
		}

		public IntPtr OldPtr
		{
			get { return btChunk_getOldPtr(_native); }
			set { btChunk_setOldPtr(_native, value); }
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (_native != IntPtr.Zero)
			{
                if (!_preventDelete)
                {
                    btChunk_delete(_native);
                }
				_native = IntPtr.Zero;
			}
		}

		~Chunk()
		{
			Dispose(false);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btChunk_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btChunk_getChunkCode(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btChunk_getDna_nr(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btChunk_getLength(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btChunk_getNumber(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btChunk_getOldPtr(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btChunk_setChunkCode(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btChunk_setDna_nr(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btChunk_setLength(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btChunk_setNumber(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btChunk_setOldPtr(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btChunk_delete(IntPtr obj);
	}

    [Flags]
    public enum SerializationFlags
    {
        NoBvh = 1,
        NoTriangleInfoMap = 2,
        NoDuplicateAssert = 4
    }

	public abstract class Serializer : IDisposable
	{
        [UnmanagedFunctionPointer(Native.Conv), SuppressUnmanagedCodeSecurity]
        delegate IntPtr AllocateUnmanagedDelegate(uint size, int numElements);
        [UnmanagedFunctionPointer(Native.Conv), SuppressUnmanagedCodeSecurity]
        delegate void FinalizeChunkUnmanagedDelegate(IntPtr chunk, string structType, DnaID chunkCode, IntPtr oldPtr);
        [UnmanagedFunctionPointer(Native.Conv), SuppressUnmanagedCodeSecurity]
        delegate IntPtr FindNameForPointerUnmanagedDelegate(IntPtr ptr);
        [UnmanagedFunctionPointer(Native.Conv), SuppressUnmanagedCodeSecurity]
        delegate IntPtr FindPointerUnmanagedDelegate(IntPtr ptr);
        [UnmanagedFunctionPointer(Native.Conv), SuppressUnmanagedCodeSecurity]
        delegate void FinishSerializationUnmanagedDelegate();
        [UnmanagedFunctionPointer(Native.Conv), SuppressUnmanagedCodeSecurity]
        delegate IntPtr GetBufferPointerUnmanagedDelegate();
        [UnmanagedFunctionPointer(Native.Conv), SuppressUnmanagedCodeSecurity]
        delegate IntPtr GetChunkUnmanagedDelegate(int chunkIndex);
        [UnmanagedFunctionPointer(Native.Conv), SuppressUnmanagedCodeSecurity]
        delegate int GetCurrentBufferSizeUnmanagedDelegate();
        [UnmanagedFunctionPointer(Native.Conv), SuppressUnmanagedCodeSecurity]
        delegate int GetNumChunksUnmanagedDelegate();
        [UnmanagedFunctionPointer(Native.Conv), SuppressUnmanagedCodeSecurity]
        delegate int GetSerializationFlagsUnmanagedDelegate();
        [UnmanagedFunctionPointer(Native.Conv), SuppressUnmanagedCodeSecurity]
        delegate IntPtr GetUniquePointerUnmanagedDelegate(IntPtr oldPtr);
        [UnmanagedFunctionPointer(Native.Conv), SuppressUnmanagedCodeSecurity]
        delegate void RegisterNameForPointerUnmanagedDelegate(IntPtr ptr, string name);
        [UnmanagedFunctionPointer(Native.Conv), SuppressUnmanagedCodeSecurity]
        delegate void SerializeNameUnmanagedDelegate(IntPtr ptr);
        [UnmanagedFunctionPointer(Native.Conv), SuppressUnmanagedCodeSecurity]
        delegate void SetSerializationFlagsUnmanagedDelegate(int flags);
        [UnmanagedFunctionPointer(Native.Conv), SuppressUnmanagedCodeSecurity]
        delegate void StartSerializationUnmanagedDelegate();

        AllocateUnmanagedDelegate _allocate;
        FinalizeChunkUnmanagedDelegate _finalizeChunk;
        FindNameForPointerUnmanagedDelegate _findNameForPointer;
        FindPointerUnmanagedDelegate _findPointer;
        FinishSerializationUnmanagedDelegate _finishSerialization;
        GetBufferPointerUnmanagedDelegate _getBufferPointer;
        GetChunkUnmanagedDelegate _getChunk;
        GetCurrentBufferSizeUnmanagedDelegate _getCurrentBufferSize;
        GetNumChunksUnmanagedDelegate _getNumChunks;
        GetSerializationFlagsUnmanagedDelegate _getSerializationFlags;
        GetUniquePointerUnmanagedDelegate _getuniquePointer;
        RegisterNameForPointerUnmanagedDelegate _registernameForPointer;
        SerializeNameUnmanagedDelegate _serializeName;
        SetSerializationFlagsUnmanagedDelegate _setSerializationFlags;
        StartSerializationUnmanagedDelegate _startSerialization;

        static byte[] dna;
        static byte[] dna64;

		internal IntPtr _native;

		public Serializer()
		{
            _allocate = new AllocateUnmanagedDelegate(AllocateUnmanaged);
            _finalizeChunk = new FinalizeChunkUnmanagedDelegate(FinalizeChunk);
            _findNameForPointer = new FindNameForPointerUnmanagedDelegate(FindNameForPointer);
            _findPointer = new FindPointerUnmanagedDelegate(FindPointer);
            _finishSerialization = new FinishSerializationUnmanagedDelegate(FinishSerialization);
            _getBufferPointer = new GetBufferPointerUnmanagedDelegate(GetBufferPointer);
            _getChunk = new GetChunkUnmanagedDelegate(GetChunk);
            _getCurrentBufferSize = new GetCurrentBufferSizeUnmanagedDelegate(GetCurrentBufferSize);
            _getNumChunks = new GetNumChunksUnmanagedDelegate(GetNumChunks);
            _getSerializationFlags = new GetSerializationFlagsUnmanagedDelegate(GetSerializationFlags);
            _getuniquePointer = new GetUniquePointerUnmanagedDelegate(GetUniquePointer);
            _registernameForPointer = new RegisterNameForPointerUnmanagedDelegate(RegisterNameForPointer);
            _serializeName = new SerializeNameUnmanagedDelegate(SerializeName);
            _setSerializationFlags = new SetSerializationFlagsUnmanagedDelegate(SetSerializationFlags);
            _startSerialization = new StartSerializationUnmanagedDelegate(StartSerialization);

            _native = btSerializerWrapper_new(
                Marshal.GetFunctionPointerForDelegate(_allocate),
                Marshal.GetFunctionPointerForDelegate(_finalizeChunk),
                Marshal.GetFunctionPointerForDelegate(_findNameForPointer),
                Marshal.GetFunctionPointerForDelegate(_findPointer),
                Marshal.GetFunctionPointerForDelegate(_finishSerialization),
                Marshal.GetFunctionPointerForDelegate(_getBufferPointer),
                Marshal.GetFunctionPointerForDelegate(_getChunk),
                Marshal.GetFunctionPointerForDelegate(_getCurrentBufferSize),
                Marshal.GetFunctionPointerForDelegate(_getNumChunks),
                Marshal.GetFunctionPointerForDelegate(_getSerializationFlags),
                Marshal.GetFunctionPointerForDelegate(_getuniquePointer),
                Marshal.GetFunctionPointerForDelegate(_registernameForPointer),
                Marshal.GetFunctionPointerForDelegate(_serializeName),
                Marshal.GetFunctionPointerForDelegate(_setSerializationFlags),
                Marshal.GetFunctionPointerForDelegate(_startSerialization));
		}

        private IntPtr AllocateUnmanaged(uint size, int numElements)
        {
            return Allocate(size, numElements)._native;
        }

        private void FinalizeChunk(IntPtr chunkPtr, string structType, DnaID chunkCode, IntPtr oldPtr)
        {
            FinalizeChunk(new Chunk(chunkPtr, true), structType, chunkCode, oldPtr);
        }

        private IntPtr GetBufferPointer()
        {
            throw new NotImplementedException();
        }

        private int GetCurrentBufferSize()
        {
            return CurrentBufferSize;
        }

        private int GetSerializationFlags()
        {
            return (int)SerializationFlags;
        }

        private void RegisterNameForPointer(IntPtr ptr, string name)
        {
            throw new NotImplementedException();
        }

        private void SetSerializationFlags(int flags)
        {
            SerializationFlags = (SerializationFlags)flags;
        }

        public abstract Chunk Allocate(uint size, int numElements);
        public abstract void FinalizeChunk(Chunk chunkPtr, string structType, DnaID chunkCode, IntPtr oldPtr);
        public abstract IntPtr FindNameForPointer(IntPtr ptr);
        public abstract IntPtr FindPointer(IntPtr oldPtr);
		public abstract void FinishSerialization();
        public abstract IntPtr GetChunk(int chunkIndex);
        public abstract IntPtr GetUniquePointer(IntPtr oldPtr);
        public abstract int GetNumChunks();
        public abstract void RegisterNameForObject(Object obj, string name);
        public abstract void SerializeName(IntPtr ptr);
        public abstract void StartSerialization();
        
        public abstract IntPtr BufferPointer { get; }
        public abstract int CurrentBufferSize { get; }
        public abstract SerializationFlags SerializationFlags { get; set; }

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (_native != IntPtr.Zero)
			{
				btSerializer_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~Serializer()
		{
			Dispose(false);
		}

        public static byte[] GetBulletDna()
        {
            if (dna == null)
            {
                int length = getBulletDNAlen();
                dna = new byte[length];
                Marshal.Copy(getBulletDNAstr(), dna, 0, length);
            }
            return dna;
        }

        public static byte[] GetBulletDna64()
        {
            if (dna64 == null)
            {
                int length = getBulletDNAlen64();
                dna64 = new byte[length];
                Marshal.Copy(getBulletDNAstr64(), dna64, 0, length);
            }
            return dna64;
        }

        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btSerializerWrapper_new(IntPtr allocateCallback, IntPtr finalizeChunkCallback,
            IntPtr findNameForPointerCallback, IntPtr findPointerCallback, IntPtr finishSerializationCallback,
            IntPtr getBufferPointerCallback, IntPtr getChunkCallback, IntPtr getCurrentBufferSizeCallback,
            IntPtr getNumChunksCallback, IntPtr getSerializationFlagsCallback, IntPtr getUniquePointerCallback,
            IntPtr registerNameForPointerCallback, IntPtr serializeNameCallback, IntPtr setSerializationFlagsCallback,
            IntPtr startSerializationCallback);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSerializer_delete(IntPtr obj);

        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr getBulletDNAstr();
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern int getBulletDNAlen();
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr getBulletDNAstr64();
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern int getBulletDNAlen64();
	}

	public class DefaultSerializer : Serializer
	{
        private IntPtr _buffer;
        private int _currentSize;
        private IntPtr _uniqueIdGenerator;
        private int _totalSize;
        private SerializationFlags _serializationFlags;

        private Dictionary<IntPtr, IntPtr> _chunkP = new Dictionary<IntPtr, IntPtr>();
        private Dictionary<IntPtr, IntPtr> _uniquePointers = new Dictionary<IntPtr, IntPtr>();
        private Dictionary<Object, IntPtr> _nameMap = new Dictionary<object, IntPtr>();
        private List<Chunk> _chunkPtrs = new List<Chunk>();

        IntPtr _dna;
        int _dnaLength;

        private Dna.NameInfo[] _names;
        private Dna.StructDecl[] _structs;
        private Dna.TypeDecl[] _types;
        private Dictionary<string, Dna.StructDecl> _structReverse;

		public DefaultSerializer(int totalSize)
		{
            _currentSize = 0;
            _totalSize = totalSize;

            _buffer = (_totalSize != 0) ? Marshal.AllocHGlobal(_totalSize) : IntPtr.Zero;

            InitDna((IntPtr.Size == 8) ? GetBulletDna64() : GetBulletDna());
		}

		public DefaultSerializer()
            : this(0)
		{
		}

        protected override void Dispose(bool disposing)
        {
            if (_buffer != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(_buffer);
                _buffer = IntPtr.Zero;
            }

            if (_dna != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(_dna);
                _dna = IntPtr.Zero;
            }

            base.Dispose(disposing);
        }

        private IntPtr InternalAlloc(int size)
        {
            IntPtr ptr;
            if (_totalSize != 0)
            {
                ptr = _buffer.Add(_currentSize);
                _currentSize += size;
                Debug.Assert(_currentSize < _totalSize);
            }
            else
            {
                ptr = Marshal.AllocHGlobal(size);
                _currentSize += size;
            }
            return ptr;
        }

        public override Chunk Allocate(uint size, int numElements)
        {
            int length = (int)size * numElements;
            IntPtr ptr = InternalAlloc(length + ChunkInd.Size);
            IntPtr data = ptr.Add( ChunkInd.Size );
            Chunk chunk = new Chunk(ptr, true)
            {
                ChunkCode = 0,
                OldPtr = data,
                Length = length,
                Number = numElements
            };
            _chunkPtrs.Add(chunk);
            return chunk;
        }

        public override void FinalizeChunk(Chunk chunk, string structType, DnaID chunkCode, IntPtr oldPtr)
        {
            if ((SerializationFlags & SerializationFlags.NoDuplicateAssert) == 0)
            {
                Debug.Assert(FindPointer(oldPtr) == IntPtr.Zero);
            }

            chunk.DnaNr = Array.IndexOf(_structs, GetReverseType(structType));
            chunk.ChunkCode = (int)chunkCode;
            IntPtr uniquePtr = GetUniquePointer(oldPtr);

            _chunkP.Add(oldPtr, uniquePtr);//chunk->m_oldPtr);
            chunk.OldPtr = uniquePtr;//oldPtr;
        }

        public override IntPtr FindNameForPointer(IntPtr ptr)
        {
            IntPtr name;
            _nameMap.TryGetValue(ptr, out name);
            return name;
        }

        public override IntPtr FindPointer(IntPtr oldPtr)
        {
            IntPtr ptr;
            _chunkP.TryGetValue(oldPtr, out ptr);
            return ptr;
        }

        public override void FinishSerialization()
		{
            WriteDna();

			//if we didn't pre-allocate a buffer, we need to create a contiguous buffer now
			int mysize = 0;
			if (_totalSize == 0)
			{
				if (_buffer != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(_buffer);
                }

				_currentSize += 12; // header
				_buffer = Marshal.AllocHGlobal(_currentSize);

				IntPtr currentPtr = _buffer;
                WriteHeader(_buffer);
				currentPtr = currentPtr.Add(12);
                mysize += 12;
				for (int i=0;i<	_chunkPtrs.Count;i++)
				{
					int curLength = ChunkInd.Size + _chunkPtrs[i].Length;
                    Marshal.StructureToPtr(_chunkPtrs[i], currentPtr, false);
					currentPtr=currentPtr.Add(curLength);
					mysize+=curLength;
				}
			}

            foreach (IntPtr ptr in _nameMap.Values)
            {
                Marshal.FreeHGlobal(ptr);
            }

			_structReverse.Clear();
			_chunkP.Clear();
			_nameMap.Clear();
			_uniquePointers.Clear();
			_chunkPtrs.Clear();
		}

        public override IntPtr GetChunk(int chunkIndex)
        {
            return _chunkPtrs[chunkIndex]._native;
        }

        public override int GetNumChunks()
        {
            return _chunkPtrs.Count;
        }

        public Dna.StructDecl GetReverseType(string typeName)
        {
            Dna.StructDecl s;
            _structReverse.TryGetValue(typeName, out s);
            return s;
        }

        public override IntPtr GetUniquePointer(IntPtr oldPtr)
        {
            if (oldPtr == IntPtr.Zero)
                return IntPtr.Zero;

            IntPtr uniquePtr;
            if (_uniquePointers.TryGetValue(oldPtr, out uniquePtr))
            {
                return uniquePtr;
            }
            
            _uniqueIdGenerator = _uniqueIdGenerator.Add(1);
            _uniquePointers.Add(oldPtr, _uniqueIdGenerator);

            return _uniqueIdGenerator;
        }

        protected unsafe void InitDna(byte[] bdnaOrg)
        {
            if (_dna != IntPtr.Zero)
            {
                return;
            }

            _dnaLength = bdnaOrg.Length;
            _dna = Marshal.AllocHGlobal(bdnaOrg.Length);
            Marshal.Copy(bdnaOrg, 0, _dna, _dnaLength);

            Stream stream = new UnmanagedMemoryStream((byte*)_dna.ToPointer(), _dnaLength);
            BinaryReader reader = new BinaryReader(stream);

            // SDNA
            byte[] code = reader.ReadBytes(8);
            string codes = ASCIIEncoding.ASCII.GetString(code);

            // NAME
            if (!codes.Equals("SDNANAME"))
            {
                throw new InvalidDataException();
            }
            int dataLen = reader.ReadInt32();
            _names = new Dna.NameInfo[dataLen];
            for (int i = 0; i < dataLen; i++)
            {
                List<byte> name = new List<byte>();
                byte ch = reader.ReadByte();
                while (ch != 0)
                {
                    name.Add(ch);
                    ch = reader.ReadByte();
                }

                _names[i] = new Dna.NameInfo(ASCIIEncoding.ASCII.GetString(name.ToArray()));
            }
            stream.Position = (stream.Position + 3) & ~3;

            // TYPE
            code = reader.ReadBytes(4);
            codes = ASCIIEncoding.ASCII.GetString(code);
            if (!codes.Equals("TYPE"))
            {
                throw new InvalidDataException();
            }
            dataLen = reader.ReadInt32();
            _types = new Dna.TypeDecl[dataLen];
            for (int i = 0; i < dataLen; i++)
            {
                List<byte> name = new List<byte>();
                byte ch = reader.ReadByte();
                while (ch != 0)
                {
                    name.Add(ch);
                    ch = reader.ReadByte();
                }
                string type = ASCIIEncoding.ASCII.GetString(name.ToArray());
                _types[i] = new Dna.TypeDecl(type);
            }
            stream.Position = (stream.Position + 3) & ~3;

            // TLEN
            code = reader.ReadBytes(4);
            codes = ASCIIEncoding.ASCII.GetString(code);
            if (!codes.Equals("TLEN"))
            {
                throw new InvalidDataException();
            }
            for (int i = 0; i < _types.Length; i++)
            {
                _types[i].Length = reader.ReadInt16();
            }
            stream.Position = (stream.Position + 3) & ~3;

            // STRC
            code = reader.ReadBytes(4);
            codes = ASCIIEncoding.ASCII.GetString(code);
            if (!codes.Equals("STRC"))
            {
                throw new InvalidDataException();
            }
            dataLen = reader.ReadInt32();
            _structs = new Dna.StructDecl[dataLen];
            long shtPtr = stream.Position;
            for (int i = 0; i < dataLen; i++)
            {
                Dna.StructDecl structDecl = new Dna.StructDecl();
                _structs[i] = structDecl;
                if (!BitConverter.IsLittleEndian)
                {
                    throw new NotImplementedException();
                }
                else
                {
                    short typeNr = reader.ReadInt16();
                    structDecl.Type = _types[typeNr];
                    structDecl.Type.Struct = structDecl;
                    int numElements = reader.ReadInt16();
                    structDecl.Elements = new Dna.ElementDecl[numElements];
                    for (int j = 0; j < numElements; j++)
                    {
                        typeNr = reader.ReadInt16();
                        short nameNr = reader.ReadInt16();
                        structDecl.Elements[j] = new Dna.ElementDecl(_types[typeNr], _names[nameNr]);
                    }
                }
            }

            reader.Dispose();
            stream.Dispose();

            // build reverse lookups
            _structReverse = new Dictionary<string, Dna.StructDecl>(_structs.Length);
            foreach (Dna.StructDecl s in _structs)
            {
                _structReverse.Add(s.Type.Name, s);
            }
        }

        public override void RegisterNameForObject(Object obj, string name)
		{
            IntPtr ptr;
            if (obj is CollisionObject)
            {
                ptr = (obj as CollisionObject)._native;
            }
            else if (obj is CollisionShape)
            {
                ptr = (obj as CollisionShape)._native;
            }
            else if (obj is TypedConstraint)
            {
                ptr = (obj as TypedConstraint)._native;
            }
            else
            {
                throw new NotImplementedException();
            }
            IntPtr namePtr = Marshal.StringToHGlobalAnsi(name);
            _nameMap.Add(ptr, namePtr);
		}

        public override void SerializeName(IntPtr namePtr)
        {
            if (namePtr == IntPtr.Zero)
            {
                return;
            }

            //don't serialize name twice
            if (FindPointer(namePtr) != IntPtr.Zero)
            {
                return;
            }

            string name = Marshal.PtrToStringAnsi(namePtr);
            int length = name.Length;
            if (length == 0)
            {
                return;
            }

            int newLen = length + 1;
            int padding = ((newLen + 3) & ~3) - newLen;
            newLen += padding;

            //serialize name string now
            Chunk chunk = Allocate(sizeof(char), newLen);
            IntPtr destPtr = chunk.OldPtr;
            for (int i = 0; i < length; i++)
            {
                Marshal.WriteByte(destPtr, i, (byte)name[i]);
            }
            FinalizeChunk(chunk, "char", DnaID.Array, namePtr);
        }

        public override void StartSerialization()
		{
            _uniqueIdGenerator = new IntPtr(1);
			if (_totalSize != 0)
			{
				IntPtr buffer = InternalAlloc(12);
				WriteHeader(buffer);
			}
		}

        public void WriteDna()
        {
            Chunk dnaChunk = Allocate((uint)_dnaLength, 1);
            byte[] tempDna = new byte[_dnaLength];
            Marshal.Copy(_dna, tempDna, 0, _dnaLength);
            Marshal.Copy(tempDna, 0, dnaChunk.OldPtr, _dnaLength);
            FinalizeChunk(dnaChunk, "DNA1", DnaID.Dna, _dna);
        }

		public unsafe void WriteHeader(IntPtr buffer)
		{
            byte[] header = new byte[] {
                (byte)'B', (byte)'U', (byte)'L', (byte)'L', (byte)'E', (byte)'T',
                (byte)'f', (byte)'_',
                (byte)'v', (byte)'2', (byte)'8', (byte)'2' };
            if (IntPtr.Size == 8)
            {
                header[7] = (byte)'-';
            }
            if (!BitConverter.IsLittleEndian)
            {
                header[8] = (byte)'V';
            }
            Marshal.Copy(header, 0, buffer, header.Length);
		}

        public override IntPtr BufferPointer
        {
            get { return _buffer; }
        }

        public override int CurrentBufferSize
        {
            get { return _currentSize; }
        }

        public override SerializationFlags SerializationFlags
        {
            get
            {
                return _serializationFlags;
            }
            set
            {
                _serializationFlags = value;
            }
        }
    }
}
