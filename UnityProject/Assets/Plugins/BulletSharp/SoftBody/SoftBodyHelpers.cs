using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.Math;

namespace BulletSharp.SoftBody
{
    public enum DrawFlags
    {
        None = 0x00,
        Nodex = 0x01,
        Links = 0x02,
        Faces = 0x04,
        Tetras = 0x08,
        Normals = 0x10,
        Contacts = 0x20,
        Anchors = 0x40,
        Notes = 0x80,
        Clusters = 0x100,
        NodeTree = 0x200,
        FaceTree = 0x400,
        ClusterTree = 0x800,
        Joints = 0x1000,
        Std = Links | Faces | Tetras | Anchors | Notes | Joints,
        StdTetra = Std - Faces - Tetras
    }

	public sealed class SoftBodyHelpers
	{
        private SoftBodyHelpers()
        {
        }

		public static float CalculateUV(int resx, int resy, int ix, int iy, int id)
		{
            switch (id)
            {
                case 0:
                    return 1.0f / ((resx - 1)) * ix;
                case 1:
                    return 1.0f / ((resy - 1)) * (resy - 1 - iy);
                case 2:
                    return 1.0f / ((resy - 1)) * (resy - 1 - iy - 1);
                case 3:
                    return 1.0f / ((resx - 1)) * (ix + 1);
                default:
                    return 0;
            }
		}

		public static SoftBody CreateEllipsoid(SoftBodyWorldInfo worldInfo, Vector3 center, Vector3 radius, int res)
		{
            int numVertices = res + 3;
            Vector3[] vtx = new Vector3[numVertices];
            for (int i = 0; i < numVertices; i++)
            {
                float p = 0.5f, t = 0;
                for (int j = i; j > 0; j >>= 1)
                {
                    if ((j & 1) != 0)
                        t += p;
                    p *= 0.5f;
                }
                float w = 2 * t - 1;
                float a = ((1 + 2 * i) * (float)System.Math.PI) / numVertices;
                float s = (float)System.Math.Sqrt(1 - w * w);
                vtx[i] = new Vector3(s * (float)System.Math.Cos(a), s * (float)System.Math.Sin(a), w) * radius + center;
            }
            return CreateFromConvexHull(worldInfo, vtx);
		}

        public static SoftBody CreateFromConvexHull(SoftBodyWorldInfo worldInfo, Vector3[] vertices, int nVertices, bool randomizeConstraints = true)
		{
            SoftBody body = new SoftBody(btSoftBodyHelpers_CreateFromConvexHull2(worldInfo._native, vertices, nVertices, randomizeConstraints));
            body.WorldInfo = worldInfo;
            return body;
        }

        public static SoftBody CreateFromConvexHull(SoftBodyWorldInfo worldInfo, Vector3[] vertices, bool randomizeConstraints = true)
        {
            SoftBody body = new SoftBody(btSoftBodyHelpers_CreateFromConvexHull2(worldInfo._native, vertices, vertices.Length, randomizeConstraints));
            body.WorldInfo = worldInfo;
            return body;
        }

        public static SoftBody CreateFromTetGenData(SoftBodyWorldInfo worldInfo, string ele, string face, string node, bool faceLinks, bool tetraLinks, bool facesFromTetras)
		{
            CultureInfo culture = CultureInfo.InvariantCulture;
            char[] separator = new[] { ' ' };
            Vector3[] pos;

            using (StringReader nodeReader = new StringReader(node))
            {
                string[] nodeHeader = nodeReader.ReadLine().Split(separator, StringSplitOptions.RemoveEmptyEntries);
                int numNodes = int.Parse(nodeHeader[0]);
                //int numDims = int.Parse(nodeHeader[1]);
                //int numAttrs = int.Parse(nodeHeader[2]);
                //bool hasBounds = !nodeHeader[3].Equals("0");

                pos = new Vector3[numNodes];
                for (int n = 0; n < numNodes; n++)
                {
                    string[] nodeLine = nodeReader.ReadLine().Split(separator, StringSplitOptions.RemoveEmptyEntries);
                    pos[int.Parse(nodeLine[0])] = new Vector3(
                        float.Parse(nodeLine[1], culture),
                        float.Parse(nodeLine[2], culture),
                        float.Parse(nodeLine[3], culture));
                }
            }
            SoftBody psb = new SoftBody(worldInfo, pos.Length, pos, null);
            /*
            if (!string.IsNullOrEmpty(face))
            {
                throw new NotImplementedException();
            }
            */
            if (!string.IsNullOrEmpty(ele))
            {
                using (StringReader eleReader = new StringReader(ele))
                {
                    string[] eleHeader = eleReader.ReadLine().Split(separator, StringSplitOptions.RemoveEmptyEntries);
                    int numTetras = int.Parse(eleHeader[0]);
                    //int numCorners = int.Parse(eleHeader[1]);
                    //int numAttrs = int.Parse(eleHeader[2]);

                    for (int n = 0; n < numTetras; n++)
                    {
                        string[] eleLine = eleReader.ReadLine().Split(separator, StringSplitOptions.RemoveEmptyEntries);
                        //int index = int.Parse(eleLine[0], culture);
                        int ni0 = int.Parse(eleLine[1], culture);
                        int ni1 = int.Parse(eleLine[2], culture);
                        int ni2 = int.Parse(eleLine[3], culture);
                        int ni3 = int.Parse(eleLine[4], culture);
                        psb.AppendTetra(ni0, ni1, ni2, ni3);
                        if (tetraLinks)
                        {
                            psb.AppendLink(ni0, ni1, null, true);
                            psb.AppendLink(ni1, ni2, null, true);
                            psb.AppendLink(ni2, ni0, null, true);
                            psb.AppendLink(ni0, ni3, null, true);
                            psb.AppendLink(ni1, ni3, null, true);
                            psb.AppendLink(ni2, ni3, null, true);
                        }
                    }
                }
            }

            //Console.WriteLine("Nodes: {0}", psb.Nodes.Count);
            //Console.WriteLine("Links: {0}", psb.Links.Count);
            //Console.WriteLine("Faces: {0}", psb.Faces.Count);
            //Console.WriteLine("Tetras: {0}", psb.Tetras.Count);

            return psb;
		}

        public static SoftBody CreateFromTetGenFile(SoftBodyWorldInfo worldInfo, string elementFilename, string faceFilename, string nodeFilename, bool faceLinks, bool tetraLinks, bool facesFromTetras)
        {
            string ele = (elementFilename != null) ? File.ReadAllText(elementFilename) : null;
            string face = (faceFilename != null) ? File.ReadAllText(faceFilename) : null;

            return CreateFromTetGenData(worldInfo, ele, face, File.ReadAllText(nodeFilename), faceLinks, tetraLinks, facesFromTetras);
        }

		public static SoftBody CreateFromTriMesh(SoftBodyWorldInfo worldInfo, float[] vertices, int[] triangles, bool randomizeConstraints = true)
		{
            int numVertices = vertices.Length / 3;
            Vector3[] vtx = new Vector3[numVertices];
            for (int i = 0, j = 0; j < numVertices; j++, i += 3)
            {
                vtx[j] = new Vector3(vertices[i], vertices[i + 1], vertices[i + 2]);
            }
            return CreateFromTriMesh(worldInfo, vtx, triangles, randomizeConstraints);
        }

        public static SoftBody CreateFromTriMesh(SoftBodyWorldInfo worldInfo, Vector3[] vertices, int[] triangles, bool randomizeConstraints = true)
        {
            int numTriangleIndices = triangles.Length;
            int numTriangles = numTriangleIndices / 3;

            int maxIndex = 0; // triangles.Max() + 1;
            for (int i = 0; i < numTriangleIndices; i++)
            {
                if (triangles[i] > maxIndex)
                {
                    maxIndex = triangles[i];
                }
            }
            maxIndex++;

            SoftBody psb = new SoftBody(worldInfo, maxIndex, vertices, null);

            BitArray chks = new BitArray(maxIndex * maxIndex);
            for (int i = 0; i < numTriangleIndices; i += 3)
            {
                int[] idx = new int[] { triangles[i], triangles[i + 1], triangles[i + 2] };
                for (int j = 2, k = 0; k < 3; j = k++)
                {
                    int chkIndex = maxIndex * idx[k] + idx[j];
                    if (!chks[chkIndex])
                    {
                        chks[chkIndex] = true;
                        chks[maxIndex * idx[j] + idx[k]] = true;
                        psb.AppendLink(idx[j], idx[k]);
                    }
                }
                psb.AppendFace(idx[0], idx[1], idx[2]);
            }

            if (randomizeConstraints)
            {
                psb.RandomizeConstraints();
            }
            return psb;
        }

		public static SoftBody CreatePatch(SoftBodyWorldInfo worldInfo, Vector3 corner00, Vector3 corner10, Vector3 corner01, Vector3 corner11, int resx, int resy, int fixeds, bool gendiags)
		{
            // Create nodes
            if ((resx < 2) || (resy < 2))
                return null;

            int rx = resx;
            int ry = resy;
            int tot = rx * ry;
            Vector3[] x = new Vector3[tot];
            float[] m = new float[tot];

            for (int iy = 0; iy < ry; iy++)
            {
                float ty = iy / (float)(ry - 1);
                Vector3 py0, py1;
                Vector3.Lerp(ref corner00, ref corner01, ty, out py0);
                Vector3.Lerp(ref corner10, ref corner11, ty, out py1);
                for (int ix = 0; ix < rx; ix++)
                {
                    float tx = ix / (float)(rx - 1);
                    int index = rx * iy + ix;
                    Vector3.Lerp(ref py0, ref py1, tx, out x[index]);
                    m[index] = 1;
                }
            }

            SoftBody psb = new SoftBody(worldInfo, tot, x, m);

            if ((fixeds & 1) != 0)
                psb.SetMass(0, 0);
            if ((fixeds & 2) != 0)
                psb.SetMass(rx - 1, 0);
            if ((fixeds & 4) != 0)
                psb.SetMass(rx * (ry - 1), 0);
            if ((fixeds & 8) != 0)
                psb.SetMass(rx * (ry - 1) + rx - 1, 0);

            // Create links and faces
            for (int iy = 0; iy < ry; ++iy)
            {
                for (int ix = 0; ix < rx; ++ix)
                {
                    int ixy = rx * iy + ix;
                    int ix1y = ixy + 1;
                    int ixy1 = rx * (iy + 1) + ix;

                    bool mdx = (ix + 1) < rx;
                    bool mdy = (iy + 1) < ry;
                    if (mdx)
                        psb.AppendLink(ixy, ix1y);
                    if (mdy)
                        psb.AppendLink(ixy, ixy1);
                    if (mdx && mdy)
                    {
                        int ix1y1 = ixy1 + 1;
                        if (((ix + iy) & 1) != 0)
                        {
                            psb.AppendFace(ixy, ix1y, ix1y1);
                            psb.AppendFace(ixy, ix1y1, ixy1);
                            if (gendiags)
                            {
                                psb.AppendLink(ixy, ix1y1);
                            }
                        }
                        else
                        {
                            psb.AppendFace(ixy1, ixy, ix1y);
                            psb.AppendFace(ixy1, ix1y, ix1y1);
                            if (gendiags)
                            {
                                psb.AppendLink(ix1y, ixy1);
                            }
                        }
                    }
                }
            }

            return psb;
		}

		public static SoftBody CreatePatchUV(SoftBodyWorldInfo worldInfo, Vector3 corner00, Vector3 corner10, Vector3 corner01, Vector3 corner11, int resx, int resy, int fixeds, bool gendiags)
		{
			SoftBody body = new SoftBody(btSoftBodyHelpers_CreatePatchUV(worldInfo._native, ref corner00, ref corner10, ref corner01, ref corner11, resx, resy, fixeds, gendiags));
            body.WorldInfo = worldInfo;
            return body;
		}

        public static SoftBody CreatePatchUV(SoftBodyWorldInfo worldInfo, Vector3 corner00, Vector3 corner10, Vector3 corner01, Vector3 corner11, int resx, int resy, int fixeds, bool gendiags, float[] texCoords)
		{
			SoftBody body = new SoftBody(btSoftBodyHelpers_CreatePatchUV2(worldInfo._native, ref corner00, ref corner10, ref corner01, ref corner11, resx, resy, fixeds, gendiags, texCoords));
            body.WorldInfo = worldInfo;
            return body;
		}

		public static SoftBody CreateRope(SoftBodyWorldInfo worldInfo, Vector3 from, Vector3 to, int res, int fixeds)
		{
            // Create nodes
            int r = res + 2;
            Vector3[] x = new Vector3[r];
            float[] m = new float[r];

            for (int i = 0; i < r; i++)
            {
                Vector3.Lerp(ref from, ref to, i / (float)(r - 1), out x[i]);
                m[i] = 1;
            }

            SoftBody psb = new SoftBody(worldInfo, r, x, m);
            if ((fixeds & 1) != 0)
                psb.SetMass(0, 0);
            if ((fixeds & 2) != 0)
                psb.SetMass(r - 1, 0);

            // Create links
            for (int i = 1; i < r; i++)
            {
                psb.AppendLink(i - 1, i);
            }

            return psb;
		}

		public static void Draw(SoftBody psb, IDebugDraw iDraw)
		{
			btSoftBodyHelpers_Draw(psb._native, DebugDraw.GetUnmanaged(iDraw));
		}

		public static void Draw(SoftBody psb, IDebugDraw iDraw, int drawFlags)
		{
			btSoftBodyHelpers_Draw2(psb._native, DebugDraw.GetUnmanaged(iDraw), drawFlags);
		}

		public static void DrawClusterTree(SoftBody psb, IDebugDraw iDraw)
		{
			btSoftBodyHelpers_DrawClusterTree(psb._native, DebugDraw.GetUnmanaged(iDraw));
		}

		public static void DrawClusterTree(SoftBody psb, IDebugDraw iDraw, int minDepth)
		{
			btSoftBodyHelpers_DrawClusterTree2(psb._native, DebugDraw.GetUnmanaged(iDraw), minDepth);
		}

		public static void DrawClusterTree(SoftBody psb, IDebugDraw iDraw, int minDepth, int maxDepth)
		{
			btSoftBodyHelpers_DrawClusterTree3(psb._native, DebugDraw.GetUnmanaged(iDraw), minDepth, maxDepth);
		}

		public static void DrawFaceTree(SoftBody psb, IDebugDraw iDraw)
		{
			btSoftBodyHelpers_DrawFaceTree(psb._native, DebugDraw.GetUnmanaged(iDraw));
		}

		public static void DrawFaceTree(SoftBody psb, IDebugDraw iDraw, int minDepth)
		{
			btSoftBodyHelpers_DrawFaceTree2(psb._native, DebugDraw.GetUnmanaged(iDraw), minDepth);
		}

		public static void DrawFaceTree(SoftBody psb, IDebugDraw iDraw, int minDepth, int maxDepth)
		{
			btSoftBodyHelpers_DrawFaceTree3(psb._native, DebugDraw.GetUnmanaged(iDraw), minDepth, maxDepth);
		}

		public static void DrawFrame(SoftBody psb, IDebugDraw iDraw)
		{
			btSoftBodyHelpers_DrawFrame(psb._native, DebugDraw.GetUnmanaged(iDraw));
		}

		public static void DrawInfos(SoftBody psb, IDebugDraw iDraw, bool masses, bool areas, bool stress)
		{
			btSoftBodyHelpers_DrawInfos(psb._native, DebugDraw.GetUnmanaged(iDraw), masses, areas, stress);
		}

		public static void DrawNodeTree(SoftBody psb, IDebugDraw iDraw)
		{
			btSoftBodyHelpers_DrawNodeTree(psb._native, DebugDraw.GetUnmanaged(iDraw));
		}

		public static void DrawNodeTree(SoftBody psb, IDebugDraw iDraw, int minDepth)
		{
			btSoftBodyHelpers_DrawNodeTree2(psb._native, DebugDraw.GetUnmanaged(iDraw), minDepth);
		}

		public static void DrawNodeTree(SoftBody psb, IDebugDraw iDraw, int minDepth, int maxDepth)
		{
			btSoftBodyHelpers_DrawNodeTree3(psb._native, DebugDraw.GetUnmanaged(iDraw), minDepth, maxDepth);
		}

        private class LinkDep
        {
            public bool LinkB { get; set; }
            public LinkDep Next { get; set; }
            public Link Value { get; set; }
        }

        // ReoptimizeLinkOrder minimizes the cases where links L and L+1 share a common node.
		public static void ReoptimizeLinkOrder(SoftBody psb)
		{
            AlignedLinkArray links = psb.Links;
            AlignedNodeArray nodes = psb.Nodes;
            int nLinks = links.Count;
            int nNodes = nodes.Count;

            List<Link> readyList = new List<Link>();
            Dictionary<Link, Link> linkBuffer = new Dictionary<Link, Link>();
            Dictionary<Link, LinkDep> linkDeps = new Dictionary<Link, LinkDep>();
            Dictionary<Node, Link> nodeWrittenAt = new Dictionary<Node, Link>();

            Dictionary<Link, Link> linkDepA = new Dictionary<Link, Link>(); // Link calculation input is dependent upon prior calculation #N
            Dictionary<Link, Link> linkDepB = new Dictionary<Link, Link>();

            foreach (Link link in links)
            {
                Node ar = link.Nodes[0];
                Node br = link.Nodes[1];
                linkBuffer.Add(link, new Link(btSoftBody_Link_new2(link._native)));

                LinkDep linkDep;
                if (nodeWrittenAt.ContainsKey(ar))
                {
                    linkDepA[link] = nodeWrittenAt[ar];
                    linkDeps.TryGetValue(nodeWrittenAt[ar], out linkDep);
                    linkDeps[nodeWrittenAt[ar]] = new LinkDep() { Next = linkDep, Value = link};
                }

                if (nodeWrittenAt.ContainsKey(br))
                {
                    linkDepB[link] = nodeWrittenAt[br];
                    linkDeps.TryGetValue(nodeWrittenAt[br], out linkDep);
                    linkDeps[nodeWrittenAt[br]] = new LinkDep() { Next = linkDep, Value = link, LinkB = true };
                }

                if (!linkDepA.ContainsKey(link) && !linkDepB.ContainsKey(link))
                {
                    readyList.Add(link);
                }

                // Update the nodes to mark which ones are calculated by this link
                nodeWrittenAt[ar] = link;
                nodeWrittenAt[br] = link;
            }

            int i = 0;
            while (readyList.Count != 0)
            {
                Link link = readyList[0];
                links[i++] = linkBuffer[link];
                readyList.RemoveAt(0);

                LinkDep linkDep;
                linkDeps.TryGetValue(link, out linkDep);
                while (linkDep != null)
                {
                    link = linkDep.Value;

                    if (linkDep.LinkB)
                    {
                        linkDepB.Remove(link);
                    }
                    else
                    {
                        linkDepA.Remove(link);
                    }

                    // Add this dependent link calculation to the ready list if *both* inputs are clear
                    if (!linkDepA.ContainsKey(link) && !linkDepB.ContainsKey(link))
                    {
                        readyList.Add(link);
                    }
                    linkDep = linkDep.Next;
                }
            }

            foreach (Link link in linkBuffer.Values)
            {
                btSoftBody_Element_delete(link._native);
            }
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btSoftBodyHelpers_CreateFromConvexHull(IntPtr worldInfo, [In] Vector3[] vertices, int nvertices);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btSoftBodyHelpers_CreateFromConvexHull2(IntPtr worldInfo, [In] Vector3[] vertices, int nvertices, bool randomizeConstraints);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBodyHelpers_CreatePatchUV(IntPtr worldInfo, [In] ref Vector3 corner00, [In] ref Vector3 corner10, [In] ref Vector3 corner01, [In] ref Vector3 corner11, int resx, int resy, int fixeds, bool gendiags);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btSoftBodyHelpers_CreatePatchUV2(IntPtr worldInfo, [In] ref Vector3 corner00, [In] ref Vector3 corner10, [In] ref Vector3 corner01, [In] ref Vector3 corner11, int resx, int resy, int fixeds, bool gendiags, float[] tex_coords);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBodyHelpers_Draw(IntPtr psb, IntPtr idraw);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBodyHelpers_Draw2(IntPtr psb, IntPtr idraw, int drawflags);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBodyHelpers_DrawClusterTree(IntPtr psb, IntPtr idraw);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBodyHelpers_DrawClusterTree2(IntPtr psb, IntPtr idraw, int minDepth);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBodyHelpers_DrawClusterTree3(IntPtr psb, IntPtr idraw, int minDepth, int maxDepth);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBodyHelpers_DrawFaceTree(IntPtr psb, IntPtr idraw);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBodyHelpers_DrawFaceTree2(IntPtr psb, IntPtr idraw, int minDepth);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBodyHelpers_DrawFaceTree3(IntPtr psb, IntPtr idraw, int minDepth, int maxDepth);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBodyHelpers_DrawFrame(IntPtr psb, IntPtr idraw);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBodyHelpers_DrawInfos(IntPtr psb, IntPtr idraw, bool masses, bool areas, bool stress);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBodyHelpers_DrawNodeTree(IntPtr psb, IntPtr idraw);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBodyHelpers_DrawNodeTree2(IntPtr psb, IntPtr idraw, int minDepth);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBodyHelpers_DrawNodeTree3(IntPtr psb, IntPtr idraw, int minDepth, int maxDepth);

        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btSoftBody_Link_new2(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btSoftBody_Element_delete(IntPtr obj);
	}
}
