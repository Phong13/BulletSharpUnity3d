using BulletSharp.Math;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace ConvexDecompositionDemo
{
    class WavefrontObj
    {
        readonly char[] faceSplitSchars = { '/' };
        readonly char[] lineSplitChars = { ' ' };

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

        int GetVertex(string[] faceVertex)
        {
            int vindex = int.Parse(faceVertex[0]);
            if (vindex < 0)
            {
                indices.Add(indices[indices.Count + vindex]);
            }
            Vector3 position = vertices[vindex - 1];

            // Search for a duplicate
            int i;
            for (i = 0; i < finalVertices.Count; i++)
            {
                if (finalVertices[i].Equals(position))
                {
                    indices.Add(i);
                    return i;
                }
            }

            finalVertices.Add(position);
            indices.Add(i);
            return i;
        }

        void ProcessLine(string line)
        {
            if (line.Length == 0)
            {
                return;
            }

            string[] parts = line.Split(lineSplitChars, System.StringSplitOptions.RemoveEmptyEntries);
            string cmd = parts[0];

            switch (cmd)
            {
                case "v":
                    vertices.Add(ToVector3(parts[1], parts[2], parts[3]));
                    break;
                case "vn":
                    normals.Add(ToVector3(parts[1], parts[2], parts[3]));
                    break;
                case "vt":
                    //texels.Add(ToVector2(parts[1], parts[2]));
                    break;
                case "f":
                    int[] face = new int[parts.Length - 1];

                    face[0] = GetVertex(parts[1].Split(faceSplitSchars, System.StringSplitOptions.RemoveEmptyEntries));
                    face[1] = GetVertex(parts[2].Split(faceSplitSchars, System.StringSplitOptions.RemoveEmptyEntries));
                    face[2] = GetVertex(parts[3].Split(faceSplitSchars, System.StringSplitOptions.RemoveEmptyEntries));

                    if (face.Length == 4)
                    {
                        indices.Add(face[0]);
                        indices.Add(face[2]);
                        face[3] = GetVertex(parts[4].Split(faceSplitSchars, System.StringSplitOptions.RemoveEmptyEntries));
                    }
                    break;
            }
        }

        public int LoadObj(Stream stream)
        {
            indices = new List<int>();
            normals = new List<Vector3>();
            //texels = new List<Vector2>();
            vertices = new List<Vector3>();
            finalVertices = new List<Vector3>();

            StreamReader reader = new StreamReader(stream);
            while (!reader.EndOfStream)
            {
                ProcessLine(reader.ReadLine());
            }

            vertices = null;
            return indices.Count / 3;
        }

        // load a wavefront obj returns number of triangles that were loaded.  Data is persists until the class is destructed.
        public int LoadObj(string fname)
        {
            indices = new List<int>();
            normals = new List<Vector3>();
            //texels = new List<Vector2>();
            vertices = new List<Vector3>();
            finalVertices = new List<Vector3>();

            using (var file = File.OpenRead(fname))
            {
                StreamReader reader = new StreamReader(file);
                while (!reader.EndOfStream)
                {
                    ProcessLine(reader.ReadLine());
                }
            }

            vertices = null;

            return indices.Count / 3;
        }

        List<int> indices;
        List<Vector3> normals;
        //List<Vector2> texels;
        List<Vector3> vertices;
        List<Vector3> finalVertices;

        public List<int> Indices
        {
            get { return indices; }
        }

        public List<Vector3> Vertices
        {
            get { return finalVertices; }
        }
    }
}
