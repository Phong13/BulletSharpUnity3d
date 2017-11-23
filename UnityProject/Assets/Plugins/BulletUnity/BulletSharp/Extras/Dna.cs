using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BulletSharp
{
    public class Dna
    {
        public class ElementDecl
        {
            public ElementDecl(TypeDecl type, NameInfo nameInfo)
            {
                Type = type;
                NameInfo = nameInfo;
            }

            public TypeDecl Type { get; }
            public NameInfo NameInfo { get; }

            public override bool Equals(object obj)
            {
                ElementDecl other = obj as ElementDecl;
                if (other == null)
                {
                    return false;
                }
                return Type.Equals(other.Type) && NameInfo.Equals(other.NameInfo);
            }

            public override int GetHashCode()
            {
                return Type.GetHashCode() ^ NameInfo.GetHashCode();
            }

            public override string ToString()
            {
                return Type + ": " + NameInfo.ToString();
            }
        }

        public class StructDecl
        {
            public StructDecl(TypeDecl type, ElementDecl[] elements)
            {
                Type = type;
                Elements = elements;
            }

            public TypeDecl Type { get; }
            public ElementDecl[] Elements { get; }

            public ElementDecl FindElement(Dna dna, bool brokenDna, NameInfo name, out int offset)
            {
                offset = 0;
                foreach (ElementDecl element in Elements)
                {
                    if (element.NameInfo.Equals(name))
                    {
                        return element;
                    }
                    int eleLen = dna.GetElementSize(element);
                    if (brokenDna)
                    {
                        if (element.Type.Name.Equals("short") && element.NameInfo.Name.Equals("int"))
                        {
                            eleLen = 0;
                        }
                    }
                    offset += eleLen;
                }
                return null;
            }

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
                return Type.GetHashCode() ^ Elements.Length;
            }

            public override string ToString()
            {
                return Type.ToString();
            }
        }

        public class TypeDecl
        {
            public StructDecl Struct { get; set; }

            public TypeDecl(string name, short length)
            {
                Name = name;
                Length = length;
            }

            public string Name { get; }
            public short Length { get; }

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
                return Name.GetHashCode() ^ Length;
            }

            public override string ToString()
            {
                return Name;
            }
        }

        public class NameInfo
        {
            public NameInfo(string name)
            {
                Name = name;
                IsPointer = name[0] == '*' || name[1] == '*';
                
                int bracketStart = name.IndexOf('[') + 1;
                if (bracketStart == 0)
                {
                    Dimension0 = 1;
                    Dimension1 = 1;
                    return;
                }
                int bracketEnd = name.IndexOf(']', bracketStart);
                Dimension1 = int.Parse(name.Substring(bracketStart, bracketEnd - bracketStart));

                // find second dimension, if any
                bracketStart = name.IndexOf('[', bracketEnd) + 1;
                if (bracketStart == 0)
                {
                    Dimension0 = 1;
                    return;
                }
                bracketEnd = name.IndexOf(']', bracketStart);
                Dimension0 = Dimension1;
                Dimension1 = int.Parse(name.Substring(bracketStart, bracketEnd - bracketStart));
            }

            public string Name { get; }
            public bool IsPointer { get; }
            public int Dimension0 { get; }
            public int Dimension1 { get; }

            public int ArrayLength { get { return Dimension0 * Dimension1; } }

            public string CleanName
            {
                get
                {
                    int bracketStart = Name.IndexOf('[');
                    if (bracketStart != -1)
                    {
                        return Name.Substring(0, bracketStart);
                    }
                    return Name;
                }
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

        private StructDecl[] _structs;
        private Dictionary<string, StructDecl> _structByTypeName;

        private bool _hasIntType;
        private int _ptrLen;

        private Dna()
        {
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

        public int GetElementSize(ElementDecl element)
		{
            int typeLength = element.NameInfo.IsPointer ? _ptrLen : element.Type.Length;
            return element.NameInfo.ArrayLength * typeLength;
		}

        public StructDecl GetStruct(int i)
        {
            return _structs[i];
        }

        public StructDecl GetStruct(string typeName)
        {
            StructDecl s;
            if (_structByTypeName.TryGetValue(typeName, out s))
            {
                return s;
            }
            return null;
        }

        public static Dna Load(byte[] dnaData, bool swap)
        {
            using (var stream = new MemoryStream(dnaData))
            {
                using (var reader = new BulletReader(stream))
                {
                    return Load(reader, swap);
                }
            }
        }

        public static Dna Load(BulletReader dnaReader, bool swap)
        {
            var dna = new Dna();
            dna.Init(dnaReader, swap);
            return dna;
        }

        private void Init(BulletReader reader, bool swap)
        {
            if (swap)
            {
                throw new NotImplementedException();
            }

            Stream stream = reader.BaseStream;
            reader.ReadTag("SDNA");

            // Element names
            reader.ReadTag("NAME");
            string[] names = reader.ReadStringList();
            var nameInfos = names
                .Select(n => new NameInfo(n))
                .ToArray();
            _hasIntType = names.Contains("int");

            // Types
            reader.ReadTag("TYPE");
            string[] typeNames = reader.ReadStringList();
            stream.Position = (stream.Position + 3) & ~3;

            reader.ReadTag("TLEN");
            TypeDecl[] types = typeNames.Select(name =>
            {
                short length = reader.ReadInt16();
                if (_ptrLen == 0 && name == "ListBase")
                {
                    _ptrLen = length / 2;
                }
                return new TypeDecl(name, length);
            }).ToArray();
            stream.Position = (stream.Position + 3) & ~3;

            // Structs
            reader.ReadTag("STRC");
            int numStructs = reader.ReadInt32();
            _structs = new StructDecl[numStructs];
            _structByTypeName = new Dictionary<string, StructDecl>(numStructs);
            for (int i = 0; i < numStructs; i++)
            {
                short typeIndex = reader.ReadInt16();
                TypeDecl structType = types[typeIndex];

                int numElements = reader.ReadInt16();
                var elements = new ElementDecl[numElements];
                for (int j = 0; j < numElements; j++)
                {
                    typeIndex = reader.ReadInt16();
                    short nameIndex = reader.ReadInt16();
                    elements[j] = new ElementDecl(types[typeIndex], nameInfos[nameIndex]);
                }

                var structDecl = new StructDecl(structType, elements);
                structType.Struct = structDecl;
                _structs[i] = structDecl;
                _structByTypeName.Add(structType.Name, structDecl);
            }
        }

        public bool[] Compare(Dna memoryDna)
        {
            bool[] _structChanged = new bool[_structs.Length];

            for (int i = 0; i < _structs.Length; i++)
            {
                StructDecl oldStruct = _structs[i];
                StructDecl curStruct = memoryDna.GetStruct(oldStruct.Type.Name);

                _structChanged[i] = !_structs[i].Equals(curStruct);
            }

            // Recurse in
            for (int i = 0; i < _structs.Length; i++)
            {
                if (_structChanged[i])
                {
                    CompareStruct(_structs[i], _structChanged);
                }
            }

            return _structChanged;
        }

        // Structs containing non-equal structs are also non-equal
        private void CompareStruct(StructDecl iter, bool[] _structChanged)
        {
            for (int i = 0; i < _structs.Length; i++)
            {
                StructDecl curStruct = _structs[i];
                if (curStruct != iter && !_structChanged[i])
                {
                    foreach (ElementDecl element in curStruct.Elements)
                    {
                        if (curStruct.Type == iter.Type && element.NameInfo.IsPointer)
                        {
                            _structChanged[i] = true;
                            CompareStruct(curStruct, _structChanged);
                        }
                    }
                }
            }
        }

        public bool IsBroken(int fileVersion)
        {
            return fileVersion == 276 && _hasIntType;
        }

        public int PointerSize
        {
            get { return _ptrLen; }
        }
    }
}
