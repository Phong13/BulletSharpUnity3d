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

		internal Chunk(IntPtr native)
		{
			_native = native;
			_preventDelete = true;
		}

		public Chunk()
		{
			_native = UnsafeNativeMethods.btChunk_new();
		}

		public int ChunkCode
		{
			get => UnsafeNativeMethods.btChunk_getChunkCode(_native);
			set => UnsafeNativeMethods.btChunk_setChunkCode(_native, value);
		}

		public int DnaNr
		{
			get => UnsafeNativeMethods.btChunk_getDna_nr(_native);
			set => UnsafeNativeMethods.btChunk_setDna_nr(_native, value);
		}

		public int Length
		{
			get => UnsafeNativeMethods.btChunk_getLength(_native);
			set => UnsafeNativeMethods.btChunk_setLength(_native, value);
		}

		public int Number
		{
			get => UnsafeNativeMethods.btChunk_getNumber(_native);
			set => UnsafeNativeMethods.btChunk_setNumber(_native, value);
		}

		public IntPtr OldPtr
		{
			get => UnsafeNativeMethods.btChunk_getOldPtr(_native);
			set => UnsafeNativeMethods.btChunk_setOldPtr(_native, value);
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
					UnsafeNativeMethods.btChunk_delete(_native);
				}
				_native = IntPtr.Zero;
			}
		}

		~Chunk()
		{
			Dispose(false);
		}
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
		private delegate IntPtr AllocateUnmanagedDelegate(uint size, int numElements);
		[UnmanagedFunctionPointer(Native.Conv), SuppressUnmanagedCodeSecurity]
		private delegate void FinalizeChunkUnmanagedDelegate(IntPtr chunk, string structType, DnaID chunkCode, IntPtr oldPtr);
		[UnmanagedFunctionPointer(Native.Conv), SuppressUnmanagedCodeSecurity]
		private delegate IntPtr FindNameForPointerUnmanagedDelegate(IntPtr ptr);
		[UnmanagedFunctionPointer(Native.Conv), SuppressUnmanagedCodeSecurity]
		private delegate IntPtr FindPointerUnmanagedDelegate(IntPtr ptr);
		[UnmanagedFunctionPointer(Native.Conv), SuppressUnmanagedCodeSecurity]
		private delegate void FinishSerializationUnmanagedDelegate();
		[UnmanagedFunctionPointer(Native.Conv), SuppressUnmanagedCodeSecurity]
		private delegate IntPtr GetBufferPointerUnmanagedDelegate();
		[UnmanagedFunctionPointer(Native.Conv), SuppressUnmanagedCodeSecurity]
		private delegate IntPtr GetChunkUnmanagedDelegate(int chunkIndex);
		[UnmanagedFunctionPointer(Native.Conv), SuppressUnmanagedCodeSecurity]
		private delegate int GetCurrentBufferSizeUnmanagedDelegate();
		[UnmanagedFunctionPointer(Native.Conv), SuppressUnmanagedCodeSecurity]
		private delegate int GetNumChunksUnmanagedDelegate();
		[UnmanagedFunctionPointer(Native.Conv), SuppressUnmanagedCodeSecurity]
		private delegate int GetSerializationFlagsUnmanagedDelegate();
		[UnmanagedFunctionPointer(Native.Conv), SuppressUnmanagedCodeSecurity]
		private delegate IntPtr GetUniquePointerUnmanagedDelegate(IntPtr oldPtr);
		[UnmanagedFunctionPointer(Native.Conv), SuppressUnmanagedCodeSecurity]
		private delegate void RegisterNameForPointerUnmanagedDelegate(IntPtr ptr, string name);
		[UnmanagedFunctionPointer(Native.Conv), SuppressUnmanagedCodeSecurity]
		private delegate void SerializeNameUnmanagedDelegate(IntPtr ptr);
		[UnmanagedFunctionPointer(Native.Conv), SuppressUnmanagedCodeSecurity]
		private delegate void SetSerializationFlagsUnmanagedDelegate(int flags);
		[UnmanagedFunctionPointer(Native.Conv), SuppressUnmanagedCodeSecurity]
		private delegate void StartSerializationUnmanagedDelegate();

		private AllocateUnmanagedDelegate _allocate;
		private FinalizeChunkUnmanagedDelegate _finalizeChunk;
		private FindNameForPointerUnmanagedDelegate _findNameForPointer;
		private FindPointerUnmanagedDelegate _findPointer;
		private FinishSerializationUnmanagedDelegate _finishSerialization;
		private GetBufferPointerUnmanagedDelegate _getBufferPointer;
		private GetChunkUnmanagedDelegate _getChunk;
		private GetCurrentBufferSizeUnmanagedDelegate _getCurrentBufferSize;
		private GetNumChunksUnmanagedDelegate _getNumChunks;
		private GetSerializationFlagsUnmanagedDelegate _getSerializationFlags;
		private GetUniquePointerUnmanagedDelegate _getuniquePointer;
		private RegisterNameForPointerUnmanagedDelegate _registernameForPointer;
		private SerializeNameUnmanagedDelegate _serializeName;
		private SetSerializationFlagsUnmanagedDelegate _setSerializationFlags;
		private StartSerializationUnmanagedDelegate _startSerialization;

		private static byte[] dna;
		private static byte[] dna64;

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

			_native = UnsafeNativeMethods.btSerializerWrapper_new(
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
			FinalizeChunk(new Chunk(chunkPtr), structType, chunkCode, oldPtr);
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
		public abstract void RegisterNameForObject(object obj, string name);
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
				UnsafeNativeMethods.btSerializer_delete(_native);
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
				int length = UnsafeNativeMethods.getBulletDNAlen();
				dna = new byte[length];
				Marshal.Copy(UnsafeNativeMethods.getBulletDNAstr(), dna, 0, length);
			}
			return dna;
		}

		public static byte[] GetBulletDna64()
		{
			if (dna64 == null)
			{
				int length = UnsafeNativeMethods.getBulletDNAlen64();
				dna64 = new byte[length];
				Marshal.Copy(UnsafeNativeMethods.getBulletDNAstr64(), dna64, 0, length);
			}
			return dna64;
		}
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
		private Dictionary<object, IntPtr> _nameMap = new Dictionary<object, IntPtr>();
		private List<Chunk> _chunks = new List<Chunk>();

		private byte[] _dnaData;
		private Dna _dna;

		public DefaultSerializer()
			: this(0)
		{
		}

		public DefaultSerializer(int totalSize)
		{
			_currentSize = 0;
			_totalSize = totalSize;

			_buffer = (_totalSize != 0) ? Marshal.AllocHGlobal(_totalSize) : IntPtr.Zero;

			InitDna();
		}

		protected override void Dispose(bool disposing)
		{
			if (_buffer != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(_buffer);
				_buffer = IntPtr.Zero;
			}

			base.Dispose(disposing);
		}

		private IntPtr InternalAlloc(int size)
		{
			IntPtr ptr;
			if (_totalSize != 0)
			{
				ptr = _buffer + _currentSize;
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
			IntPtr data = ptr + ChunkInd.Size;
			var chunk = new Chunk(ptr)
			{
				ChunkCode = 0,
				OldPtr = data,
				Length = length,
				Number = numElements
			};
			_chunks.Add(chunk);
			return chunk;
		}

		public override void FinalizeChunk(Chunk chunk, string structType, DnaID chunkCode, IntPtr oldPtr)
		{
			if ((SerializationFlags & SerializationFlags.NoDuplicateAssert) == 0)
			{
				Debug.Assert(FindPointer(oldPtr) == IntPtr.Zero);
			}

			Dna.StructDecl structDecl = _dna.GetStruct(structType);
			for (int i = 0; i < _dna.NumStructs; i++)
			{
				if (_dna.GetStruct(i) == structDecl)
				{
					chunk.DnaNr = i;
					break;
				}
			}

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
				currentPtr += 12;
				foreach (Chunk chunk in _chunks)
				{
					if (IntPtr.Size == 8)
					{
						var chunkPtr = new Chunk8();
						Marshal.PtrToStructure(chunk._native, chunkPtr);
						Marshal.StructureToPtr(chunkPtr, currentPtr, false);
					}
					else
					{
						var chunkPtr = new Chunk4();
						Marshal.PtrToStructure(chunk._native, chunkPtr);
						Marshal.StructureToPtr(chunkPtr, currentPtr, false);
					}
					currentPtr += ChunkInd.Size + chunk.Length;
				}
			}

			foreach (IntPtr ptr in _nameMap.Values)
			{
				Marshal.FreeHGlobal(ptr);
			}

			_chunkP.Clear();
			_nameMap.Clear();
			_uniquePointers.Clear();
			_chunks.Clear();
		}

		public override IntPtr GetChunk(int chunkIndex)
		{
			return _chunks[chunkIndex]._native;
		}

		public override int GetNumChunks()
		{
			return _chunks.Count;
		}

		public override IntPtr GetUniquePointer(IntPtr oldPtr)
		{
			if (oldPtr == IntPtr.Zero) return IntPtr.Zero;

			IntPtr uniquePtr;
			if (_uniquePointers.TryGetValue(oldPtr, out uniquePtr))
			{
				return uniquePtr;
			}
			
			_uniqueIdGenerator = IntPtr.Add(_uniqueIdGenerator, 1);
			_uniquePointers.Add(oldPtr, _uniqueIdGenerator);

			return _uniqueIdGenerator;
		}

		private void InitDna()
		{
			_dnaData = IntPtr.Size == 8 ? GetBulletDna64() : GetBulletDna();
			bool swap = !BitConverter.IsLittleEndian;
			using (var stream = new MemoryStream(_dnaData))
			{
				using (var reader = new BulletReader(stream))
				{
					_dna = Dna.Load(reader, swap);
				}
			}
		}

		public override void RegisterNameForObject(object obj, string name)
		{
			IntPtr ptr;
			if (obj is CollisionObject)
			{
				ptr = (obj as CollisionObject).Native;
			}
			else if (obj is CollisionShape)
			{
				ptr = (obj as CollisionShape).Native;
			}
			else if (obj is TypedConstraint)
			{
				ptr = (obj as TypedConstraint).Native;
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
			Chunk dnaChunk = Allocate((uint)_dnaData.Length, 1);
			GCHandle dnaHandle = GCHandle.Alloc(_dnaData, GCHandleType.Pinned);
			FinalizeChunk(dnaChunk, "DNA1", DnaID.Dna, dnaHandle.AddrOfPinnedObject());
			dnaHandle.Free();
		}

		public unsafe void WriteHeader(IntPtr buffer)
		{
			byte[] header = Encoding.ASCII.GetBytes("BULLETf_v286");
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

		public override IntPtr BufferPointer => _buffer;

		public override int CurrentBufferSize => _currentSize;

		public override SerializationFlags SerializationFlags
		{
			get => _serializationFlags;
			set => _serializationFlags = value;
		}
	}
}
