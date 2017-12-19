using BulletSharp;
using BulletSharp.Math;
using System.Collections.Generic;
using System.Linq;

namespace DemoFramework.FileLoaders
{
    public abstract class BspConverter
    {
        public abstract void AddCollider(List<Vector3> vertices);

        public void ConvertBsp(BspLoader bspLoader, float scaling)
        {
            foreach (BspLeaf leaf in bspLoader.Leaves)
            {
                foreach (int brushId in bspLoader.LeafBrushes
                    .Skip(leaf.FirstLeafBrush)
                    .Take(leaf.NumLeafBrushes)
                    .Where(brushId => IsBrushSolid(bspLoader, brushId)))
                {
                    bspLoader.Brushes[brushId].ShaderNum = -1;
                    OutputBrushAsCollider(bspLoader, scaling, brushId);
                }
            }
        }

        private bool IsBrushSolid(BspLoader bspLoader, int brushId)
        {
            BspBrush brush = bspLoader.Brushes[brushId];
            if (brush.ShaderNum == -1) return false;

            ContentFlags flags = bspLoader.IsVbsp
                ? (ContentFlags)brush.ShaderNum
                : bspLoader.Shaders[brush.ShaderNum].ContentFlags;
            return (flags & ContentFlags.Solid) != 0;
        }

        private void OutputBrushAsCollider(BspLoader bspLoader, float scaling, int brushId)
        {
            BspBrush brush = bspLoader.Brushes[brushId];
            var sides = bspLoader.BrushSides
                        .Skip(brush.FirstSide)
                        .Take(brush.NumSides);

            var planeEquations = sides.Select(side =>
            {
                BspPlane plane = bspLoader.Planes[side.PlaneNum];
                return new Vector4(plane.NormalX, plane.NormalY, plane.NormalZ, scaling * -plane.Distance);
            }).ToList();

            if (planeEquations.Count != 0)
            {
                List<Vector3> vertices = GeometryUtil.GetVerticesFromPlaneEquations(planeEquations);
                AddCollider(vertices);
            }
        }
    }
}
