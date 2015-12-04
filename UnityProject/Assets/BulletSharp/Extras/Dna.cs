using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace BulletSharp
{
    public class Dna
    {
        public class ElementDecl
        {
            public TypeDecl Type { get; private set; }
            public NameInfo Name { get; private set; }

            public ElementDecl(TypeDecl type, NameInfo name)
            {
                Type = type;
                Name = name;
            }

            public override bool Equals(object obj)
            {
                ElementDecl other = obj as ElementDecl;
                if (other == null)
                {
                    return false;
                }
                return Type.Equals(other.Type) && Name.Equals(other.Name);
            }

            public override int GetHashCode()
            {
                return Type.GetHashCode() + Name.GetHashCode();
            }

            public override string ToString()
            {
                return Type + ": " + Name.ToString();
            }
        }

        public class StructDecl
        {
            public TypeDecl Type { get; set; }
            public ElementDecl[] Elements { get; set; }

            public override bool Equals(object obj)
            {
                StructDecl other = obj as StructDecl;
                if (other == null)
                {
                    return false;
                }

                int elementCount = Elements.Length;
                if (elementCount != other.Elements.Length)
                {
                    return false;
                }

                for (int i = 0; i < elementCount; i++)
                {
                    if (!Elements[i].Equals(other.Elements[i]))
                    {
                        return false;
                    }
                }

                return Type.Equals(other.Type);
            }

            public override int GetHashCode()
            {
                return Type.GetHashCode() + Elements.Length;
            }

            public override string ToString()
            {
                return Type.ToString();
            }
        }

        public class TypeDecl
        {
            public Dna.StructDecl Struct { get; set; }
            public short Length { get; set; }
            public string Name { get; private set; }

            public TypeDecl(string name)
            {
                Name = name;
            }

            public override bool Equals(object obj)
            {
                TypeDecl other = obj as TypeDecl;
                if (other == null)
                {
                    return false;
                }
                return Name.Equals(other.Name) && Length == other.Length;
            }

            public override int GetHashCode()
            {
                return Name.GetHashCode() + Length;
            }

            public override string ToString()
            {
                return Name;
            }
        }

        private enum FileDnaFlags
        {
            None = 0,
            StructNotEqual,
            StructEqual
        };

        public class NameInfo
        {
            public string Name { get; private set; }
            public bool IsPointer { get; private set; }
            public int Dim0 { get; set; }
            public int Dim1 { get; set; }

            public int ArraySizeNew { get { return Dim0 * Dim1; } }

            public string CleanName
            {
                get
                {
                    int p = Name.IndexOf('[');
                    if (p != -1)
                    {
                        return Name.Substring(0, Name.IndexOf('['));
                    }
                    return Name;
                }
            }

            public NameInfo(string name)
            {
                Name = name;
                IsPointer = name[0] == '*' || name[1] == '*';
                
                int bp = name.IndexOf('[') + 1;
                if (bp == 0)
                {
                    Dim0 = 1;
                    Dim1 = 1;
                    return;
                }
                int bp2 = name.IndexOf(']');
                Dim1 = int.Parse(name.Substring(bp, bp2 - bp));

                // find second dim, if any
                bp = name.IndexOf('[', bp2) + 1;
                if (bp == 0)
                {
                    Dim0 = 1;
                    return;
                }
                bp2 = name.IndexOf(']');
                Dim0 = Dim1;
                Dim1 = int.Parse(name.Substring(bp, bp2 - bp));
            }

            public override bool Equals(object obj)
            {
                NameInfo other = obj as NameInfo;
                if (other == null)
                {
                    return false;
                }
                return Name.Equals(other.Name);
            }

            public override int GetHashCode()
            {
                return Name.GetHashCode();
            }

            public override string ToString()
            {
                return Name;
            }
        }

        private FileDnaFlags[] _cmpFlags;
        private NameInfo[] _names;
        private StructDecl[] _structs;
        private TypeDecl[] _types;
        private Dictionary<string, int> _structReverse;

        private int _ptrLen;

        public Dna()
        {
        }

        public bool FlagEqual(int dnaNR)
        {
            Debug.Assert(dnaNR < _cmpFlags.Length);
            return _cmpFlags[dnaNR] == FileDnaFlags.StructEqual;
        }

        public int GetElementSize(ElementDecl element)
		{
            return element.Name.ArraySizeNew * (element.Name.IsPointer ? _ptrLen : element.Type.Length);
		}

        public string GetName(int i)
        {
            return _names[i].Name;
        }

        public StructDecl GetStruct(int i)
        {
            return _structs[i];
        }

        public int GetReverseType(string typeName)
        {
            int s;
            if (_structReverse.TryGetValue(typeName, out s))
            {
                return s;
            }
            return -1;
        }

        public void Init(BinaryReader reader, bool swap)
        {
            Stream stream = reader.BaseStream;

            // SDNA
            byte[] code = reader.ReadBytes(8);
            string codes = ASCIIEncoding.ASCII.GetString(code);

            // NAME
            if (!codes.Equals("SDNANAME"))
            {
                throw new InvalidDataException();
            }
            int dataLen = reader.ReadInt32();
            _names = new NameInfo[dataLen];
            for (int i = 0; i < dataLen; i++)
            {
                List<byte> name = new List<byte>();
                byte ch = reader.ReadByte();
                while (ch != 0)
                {
                    name.Add(ch);
                    ch = reader.ReadByte();
                }

                _names[i] = new NameInfo(ASCIIEncoding.ASCII.GetString(name.ToArray()));
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
            _types = new TypeDecl[dataLen];
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
                _types[i] = new TypeDecl(type);
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
            _structs = new StructDecl[dataLen];
            long shtPtr = stream.Position;
            for (int i = 0; i < dataLen; i++)
            {
                StructDecl structDecl = new StructDecl();
                _structs[i] = structDecl;
                if (swap)
                {
                    throw new NotImplementedException();
                }
                else
                {
                    short typeNr = reader.ReadInt16();
                    structDecl.Type = _types[typeNr];
                    structDecl.Type.Struct = structDecl;
                    int numElements = reader.ReadInt16();
                    structDecl.Elements = new ElementDecl[numElements];
                    for (int j = 0; j < numElements; j++)
                    {
                        typeNr = reader.ReadInt16();
                        short nameNr = reader.ReadInt16();
                        structDecl.Elements[j] = new ElementDecl(_types[typeNr], _names[nameNr]);
                    }
                }
            }

            // build reverse lookups
            _structReverse = new Dictionary<string, int>(_structs.Length);
            for (int i = 0; i < _structs.Length; i++)
            {
                StructDecl s = _structs[i];
                if (_ptrLen == 0 && s.Type.Name.Equals("ListBase"))
                {
                    _ptrLen = s.Type.Length / 2;
                }
                _structReverse.Add(s.Type.Name, i);
            }
        }

        public void InitCmpFlags(Dna memoryDna)
        {
            // compare the file to memory
            // this ptr should be the file data

            Debug.Assert(_names.Length != 0); // SDNA empty!
            _cmpFlags = new FileDnaFlags[_structs.Length];

            for (int i = 0; i < _structs.Length; i++)
            {
                Dna.StructDecl oldStruct = _structs[i];
                int oldLookup = GetReverseType(oldStruct.Type.Name);
                if (oldLookup == -1)
                {
                    _cmpFlags[i] = FileDnaFlags.None;
                    continue;
                }

                if (oldLookup < memoryDna._structs.Length)
                {
                    Dna.StructDecl curStruct = memoryDna.GetStruct(oldLookup);

                    _cmpFlags[i] = oldStruct.Equals(curStruct) ? FileDnaFlags.StructEqual : FileDnaFlags.StructNotEqual;
                }
            }

            // Recurse in
            for (int i = 0; i < _structs.Length; i++)
            {
                if (_cmpFlags[i] == FileDnaFlags.StructNotEqual)
                {
                    InitRecurseCmpFlags(_structs[i]);
                }
            }
        }

        // Structs containing non-equal structs are also non-equal
        private void InitRecurseCmpFlags(Dna.StructDecl iter)
        {
            for (int i = 0; i < _structs.Length; i++)
            {
                Dna.StructDecl curStruct = _structs[i];
                if (curStruct != iter && _cmpFlags[i] == FileDnaFlags.StructEqual)
                {
                    foreach (Dna.ElementDecl element in curStruct.Elements)
                    {
                        if (curStruct.Type == iter.Type && element.Name.IsPointer)
                        {
                            _cmpFlags[i] = FileDnaFlags.StructNotEqual;
                            InitRecurseCmpFlags(curStruct);
                        }
                    }
                }
            }
        }

        public bool LessThan(Dna file)
        {
            return _names.Length < _names.Length;
        }

        public int NumNames
        {
            get
            {
                if (_names == null)
                {
                    return 0;
                }
                return _names.Length;
            }
        }

        public int NumStructs
        {
            get
            {
                if (_structs == null)
                {
                    return 0;
                }
                return _structs.Length;
            }
        }

        public int PointerSize
        {
            get { return _ptrLen; }
        }
    }
}
