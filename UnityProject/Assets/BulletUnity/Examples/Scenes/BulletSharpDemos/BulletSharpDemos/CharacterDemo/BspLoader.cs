using BulletSharp.Math;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace CharacterDemo
{
    public struct BspBrush
    {
        public int FirstSide { get; set; }
        public int NumSides { get; set; }
        public int ShaderNum { get; set; }
    }

    public struct BspBrushSide
    {
        public int PlaneNum { get; set; }
        public int ShaderNum { get; set; }
    }

    [DebuggerDisplay("ClassName: {ClassName}")]
    public class BspEntity
    {
        public string ClassName { get; set; }
        public Vector3 Origin { get; set; }

        public Dictionary<string, string> KeyValues { get; set; }

        public BspEntity()
        {
            KeyValues = new Dictionary<string, string>();
        }
    }

    public struct BspLeaf
    {
        public int Cluster;
        public int Area;
        public Vector3 Min;
        public Vector3 Max;
        public int FirstLeafFace;
        public int NumLeafFaces;
        public int FirstLeafBrush;
        public int NumLeafBrushes;
    }

    [DebuggerDisplay("Offset: {Offset}, Length: {Length}")]
    public struct BspLump
    {
        public int Offset;
        public int Length;
    }

    public struct BspPlane
    {
        public Vector3 Normal;
        public float Distance;
    }

    [Flags]
    public enum ContentFlags
    {
        Solid = 1,
        AreaPortal = 0x8000,
        MonsterClip = 0x20000,
        Detail = 0x8000000
    }

    public class BspShader
    {
        public string Shader;
        public int SurfaceFlags;
        public ContentFlags ContentFlags;
    }

    public enum IBspLumpType
    {
        Entities = 0,
        Shaders,
        Planes,
        Nodes,
        Leaves,
        LeafFaces,
        LeafBrushes,
        Models,
        Brushes,
        BrushSides,
        Vertices,
        MeshIndices,
        Faces,
        Lightmaps,
        LightVols,
        VisData
    }

    public enum VBspLumpType
    {
        Entities = 0,
        Planes,
        Texdata,
        Vertexes,
        Visibility,
        Nodes,
        Texinfo,
        Faces,
        Lighting,
        Occlusion,
        Leafs,
        Unused1,
        Edges,
        Surfedges,
        Models,
        Worldlights,
        LeafFaces,
        LeafBrushes,
        Brushes,
        BrushSides,
        Area,
        AreaPortals,
        Portals,
        Clusters,
        PortalVerts,
        ClusterPortals,
        Dispinfo,
        OriginalFaces,
        Unused2,
        PhysCollide,
        VertNormals,
        VertNormalIndices,
        DispLightmapAlphas,
        DispVerts,
        DispLightmapSamplePos,
        GameLump,
        LeafWaterData,
        Primitives,
        PrimVerts,
        PrimIndices,
        Pakfile,
        ClipPortalVerts,
        Cubemaps,
        TexdataStringData,
        TexdataStringTable,
        Overlays,
        LeafMinDistToWater,
        FaceMacroTextureInfo,
        DispTris,
        PhysCollideSurface,
        Unused3,
        Unused4,
        Unused5,
        LightingHDR,
        WorldlightsHDR,
        LeaflightHDR1,
        LeaflightHDR2
    }

    public class BspLoader
    {
        public BspBrush[] Brushes { get; set; }
        public BspBrushSide[] BrushSides { get; set; }
        public List<BspEntity> Entities { get; set; }
        public BspLeaf[] Leaves { get; set; }
        public int[] LeafBrushes { get; set; }
        public BspPlane[] Planes { get; set; }
        public List<BspShader> Shaders { get; set; }
        public bool IsVbsp { get; private set; }

        public bool LoadBspFile(string filename)
        {
            return LoadBspFile(new FileStream(filename, FileMode.Open, FileAccess.Read));
        }

        public bool LoadBspFile(Stream buffer)
        {
            BinaryReader reader = new BinaryReader(buffer);


            // read header
            string id = Encoding.ASCII.GetString(reader.ReadBytes(4), 0, 4);
            int version = reader.ReadInt32();

            if (id != "IBSP" && id != "VBSP")
                return false;

            int nHeaderLumps;

            if (id == "IBSP")
            {
                if (version != 0x2E)
                {
                    return false;
                }
                nHeaderLumps = 17;
            }
            else// if (id == "VBSP")
            {
                if (version != 0x14)
                {
                    return false;
                }
                nHeaderLumps = 64;
                IsVbsp = true;
            }

            BspLump[] lumps = new BspLump[nHeaderLumps];
            for (int i = 0; i < lumps.Length; i++)
            {
                lumps[i].Offset = reader.ReadInt32();
                lumps[i].Length = reader.ReadInt32();
                if (IsVbsp)
                {
                    reader.ReadInt32(); // lump format version
                    reader.ReadInt32(); // lump ident code
                }
            }


            // read brushes
            int lumpHeaderOffset = IsVbsp ? (int)VBspLumpType.Brushes : (int)IBspLumpType.Brushes;
            buffer.Position = lumps[lumpHeaderOffset].Offset;
            int length = lumps[lumpHeaderOffset].Length / Marshal.SizeOf(typeof(BspBrush));
            Brushes = new BspBrush[length];

            for (int i = 0; i < length; i++)
            {
                Brushes[i].FirstSide = reader.ReadInt32();
                Brushes[i].NumSides = reader.ReadInt32();
                Brushes[i].ShaderNum = reader.ReadInt32();
            }

            // read brush sides
            lumpHeaderOffset = IsVbsp ? (int)VBspLumpType.BrushSides : (int)IBspLumpType.BrushSides;
            buffer.Position = lumps[lumpHeaderOffset].Offset;
            length = lumps[lumpHeaderOffset].Length / Marshal.SizeOf(typeof(BspBrushSide));
            BrushSides = new BspBrushSide[length];

            for (int i = 0; i < length; i++)
            {
                if (IsVbsp)
                {
                    BrushSides[i].PlaneNum = reader.ReadUInt16();
                    reader.ReadInt16(); // texinfo
                    BrushSides[i].ShaderNum = reader.ReadInt16();
                    reader.ReadInt16(); // bevel
                }
                else
                {
                    BrushSides[i].PlaneNum = reader.ReadInt32();
                    BrushSides[i].ShaderNum = reader.ReadInt32();
                }
            }


            // read entities
            Entities = new List<BspEntity>();
            buffer.Position = lumps[(int)IBspLumpType.Entities].Offset;
            length = lumps[(int)IBspLumpType.Entities].Length;

            byte[] entityBytes = new byte[length];
            reader.Read(entityBytes, 0, length);

            string entityString = Encoding.ASCII.GetString(entityBytes);
            string[] entityStrings = entityString.Split('\n');

            BspEntity bspEntity = null;
            foreach (string entity in entityStrings)
            {
                switch (entity)
                {
                    case "\0":
                        continue;

                    case "{":
                        bspEntity = new BspEntity();
                        break;

                    case "}":
                        Entities.Add(bspEntity);
                        break;

                    default:
                        string[] keyValue = entity.Trim('\"').Split(new[] { "\" \"" }, 2, 0);
                        switch (keyValue[0])
                        {
                            case "classname":
                                bspEntity.ClassName = keyValue[1];
                                break;
                            case "origin":
                                string[] originStrings = keyValue[1].Split(' ');
                                bspEntity.Origin = new Vector3(
                                    float.Parse(originStrings[0], CultureInfo.InvariantCulture),
                                    float.Parse(originStrings[1], CultureInfo.InvariantCulture),
                                    float.Parse(originStrings[2], CultureInfo.InvariantCulture));
                                break;
                            default:
                                if (!bspEntity.KeyValues.ContainsKey(keyValue[0]))
                                {
                                    if (keyValue.Length == 1)
                                    {
                                        bspEntity.KeyValues.Add(keyValue[0], "");
                                    }
                                    else
                                    {
                                        bspEntity.KeyValues.Add(keyValue[0], keyValue[1]);
                                    }
                                }
                                break;
                        }
                        break;
                }
            }


            // read leaves
            lumpHeaderOffset = IsVbsp ? (int)VBspLumpType.Leafs : (int)IBspLumpType.Leaves;
            buffer.Position = lumps[lumpHeaderOffset].Offset;
            length = lumps[lumpHeaderOffset].Length;
            length /= IsVbsp ? 32 : Marshal.SizeOf(typeof(BspLeaf));
            Leaves = new BspLeaf[length];

            for (int i = 0; i < length; i++)
            {
                if (IsVbsp)
                {
                    reader.ReadInt32(); // contents

                    Leaves[i].Cluster = reader.ReadInt16();
                    Leaves[i].Area = reader.ReadInt16();

                    //Swap Y and Z; invert Z
                    Leaves[i].Min.X = reader.ReadInt16();
                    Leaves[i].Min.Z = -reader.ReadInt16();
                    Leaves[i].Min.Y = reader.ReadInt16();

                    //Swap Y and Z; invert Z
                    Leaves[i].Max.X = reader.ReadInt16();
                    Leaves[i].Max.Z = -reader.ReadInt16();
                    Leaves[i].Max.Y = reader.ReadInt16();

                    Leaves[i].FirstLeafFace = reader.ReadUInt16();
                    Leaves[i].NumLeafFaces = reader.ReadUInt16();
                    Leaves[i].FirstLeafBrush = reader.ReadUInt16();
                    Leaves[i].NumLeafBrushes = reader.ReadUInt16();

                    reader.ReadInt16(); // leafWaterDataID
                    //reader.ReadInt16(); // ambientLighting
                    //reader.ReadSByte(); // ambientLighting
                    reader.ReadInt16(); // padding
                }
                else
                {
                    Leaves[i].Cluster = reader.ReadInt32();
                    Leaves[i].Area = reader.ReadInt32();

                    //Swap Y and Z; invert Z
                    Leaves[i].Min.X = reader.ReadInt32();
                    Leaves[i].Min.Z = -reader.ReadInt32();
                    Leaves[i].Min.Y = reader.ReadInt32();

                    //Swap Y and Z; invert Z
                    Leaves[i].Max.X = reader.ReadInt32();
                    Leaves[i].Max.Z = -reader.ReadInt32();
                    Leaves[i].Max.Y = reader.ReadInt32();

                    Leaves[i].FirstLeafFace = reader.ReadInt32();
                    Leaves[i].NumLeafFaces = reader.ReadInt32();
                    Leaves[i].FirstLeafBrush = reader.ReadInt32();
                    Leaves[i].NumLeafBrushes = reader.ReadInt32();
                }
            }


            // read leaf brushes
            lumpHeaderOffset = IsVbsp ? (int)VBspLumpType.LeafBrushes : (int)IBspLumpType.LeafBrushes;
            buffer.Position = lumps[lumpHeaderOffset].Offset;
            length = lumps[lumpHeaderOffset].Length;
            length /= IsVbsp ? sizeof(short) : sizeof(int);
            LeafBrushes = new int[length];

            for (int i = 0; i < length; i++)
            {
                if (IsVbsp)
                {
                    LeafBrushes[i] = reader.ReadInt16();
                }
                else
                {
                    LeafBrushes[i] = reader.ReadInt32();
                }
            }


            // read planes
            lumpHeaderOffset = IsVbsp ? (int)VBspLumpType.Planes : (int)IBspLumpType.Planes;
            buffer.Position = lumps[lumpHeaderOffset].Offset;
            length = lumps[lumpHeaderOffset].Length;
            length /= IsVbsp ? (Marshal.SizeOf(typeof(BspPlane)) + sizeof(int)) : Marshal.SizeOf(typeof(BspPlane));
            Planes = new BspPlane[length];

            for (int i = 0; i < length; i++)
            {
                Planes[i].Normal.X = reader.ReadSingle();
                Planes[i].Normal.Y = reader.ReadSingle();
                Planes[i].Normal.Z = reader.ReadSingle();
                Planes[i].Distance = reader.ReadSingle();
                if (IsVbsp)
                {
                    reader.ReadInt32(); // type
                }
            }


            if (!IsVbsp)
            {
                // read shaders
                Shaders = new List<BspShader>();
                buffer.Position = lumps[(int)IBspLumpType.Shaders].Offset;
                length = lumps[(int)IBspLumpType.Shaders].Length / (64 + 2 * sizeof(int));

                byte[] shaderBytes = new byte[64];
                for (int i = 0; i < length; i++)
                {
                    BspShader shader = new BspShader();
                    reader.Read(shaderBytes, 0, 64);
                    shader.Shader = Encoding.ASCII.GetString(shaderBytes, 0, Array.IndexOf(shaderBytes, (byte)0));
                    shader.SurfaceFlags = reader.ReadInt32();
                    shader.ContentFlags = (ContentFlags)reader.ReadInt32();
                    Shaders.Add(shader);
                }
            }

            return true;
        }

        public bool FindVectorByName(string name, ref Vector3 outVector)
        {
            foreach (BspEntity entity in Entities)
            {
                if (entity.ClassName == name &&
                    (entity.ClassName == "info_player_start" ||
                    entity.ClassName == "info_player_deathmatch"))
                {
                    outVector = entity.Origin;
                    return true;
                }
            }

            return false;
        }
    }
}
