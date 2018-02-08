using BulletSharp.Math;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace DemoFramework.FileLoaders
{
    public sealed class WavefrontObj
    {
        private readonly char[] _faceSplitSchars = { '/' };
        private readonly char[] _lineSplitChars = { ' ' };

        private List<Vector3> _vertices = new List<Vector3>();
        private Dictionary<Vector3, int> _vertexMap = new Dictionary<Vector3, int>();

        private WavefrontObj(string filename)
        {
            using (var file = File.OpenRead(filename))
            {
                var reader = new StreamReader(file);
                while (!reader.EndOfStream)
                {
                    ProcessLine(reader.ReadLine());
                }
            }
        }

        public List<int> Indices = new List<int>();
        public List<Vector3> Vertices = new List<Vector3>();
        public List<Vector3> Normals = new List<Vector3>();
        //public List<Vector2> Texels { get; } = new List<Vector2>();

        public static WavefrontObj Load(string filename)
        {
            return new WavefrontObj(filename);
        }

        void ProcessLine(string line)
        {
            if (line.Length == 0)
            {
                return;
            }

            string[] parts = line.Split(_lineSplitChars, StringSplitOptions.RemoveEmptyEntries);
            string command = parts[0];

            switch (command)
            {
                case "v":
                    _vertices.Add(ToVector3(parts[1], parts[2], parts[3]));
                    break;
                case "vn":
                    Normals.Add(ToVector3(parts[1], parts[2], parts[3]));
                    break;
                case "vt":
                    //Texels.Add(ToVector2(parts[1], parts[2]));
                    break;
                case "f":
                    int numVertices = parts.Length - 1;
                    if (numVertices < 3 || numVertices > 4)
                    {
                        break;
                    }

                    int[] face = new int[numVertices];

                    face[0] = GetVertex(parts[1].Split(_faceSplitSchars, StringSplitOptions.RemoveEmptyEntries));
                    face[1] = GetVertex(parts[2].Split(_faceSplitSchars, StringSplitOptions.RemoveEmptyEntries));
                    face[2] = GetVertex(parts[3].Split(_faceSplitSchars, StringSplitOptions.RemoveEmptyEntries));

                    if (numVertices == 4)
                    {
                        Indices.Add(face[0]);
                        Indices.Add(face[2]);
                        face[3] = GetVertex(parts[4].Split(_faceSplitSchars, StringSplitOptions.RemoveEmptyEntries));
                    }
                    break;
            }
        }

        //Vector2 ToVector2(string f0, string f1)
        //{
        //    return new Vector2(
        //        float.Parse(f0, CultureInfo.InvariantCulture),
        //        float.Parse(f1, CultureInfo.InvariantCulture));
        //}

        Vector3 ToVector3(string f0, string f1, string f2)
        {
            return new Vector3(
                float.Parse(f0, CultureInfo.InvariantCulture),
                float.Parse(f1, CultureInfo.InvariantCulture),
                float.Parse(f2, CultureInfo.InvariantCulture));
        }

        private int GetVertex(string[] faceVertex)
        {
            int vertexIndex = int.Parse(faceVertex[0]);
            if (vertexIndex < 0)
            {
                Indices.Add(Indices[Indices.Count + vertexIndex]);
            }
            Vector3 position = _vertices[vertexIndex - 1];

            int existingIndex;
            if (_vertexMap.TryGetValue(position, out existingIndex))
            {
                Indices.Add(existingIndex);
                return existingIndex;
            }

            int newIndex = Vertices.Count;
            Vertices.Add(position);
            _vertexMap.Add(position, newIndex);
            Indices.Add(newIndex);
            return newIndex;
        }
    }
}
