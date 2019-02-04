using BulletSharp;
using BulletSharp.Math;

namespace CharacterDemo
{
    public abstract class BspConverter
    {
        public void ConvertBsp(BspLoader bspLoader, float scaling)
        {
            Vector3 playerStart = new Vector3(0, 0, 100);
            if (bspLoader.FindVectorByName("info_player_start", ref playerStart) == false)
            {
                bspLoader.FindVectorByName("info_player_deathmatch", ref playerStart);
            }
            playerStart[2] += 20.0f; //start a bit higher
            playerStart *= scaling;

            foreach (BspLeaf leaf in bspLoader.Leaves)
            {
                bool isValidBrush = false;

                for (int b = 0; b < leaf.NumLeafBrushes; b++)
                {
                    AlignedVector3Array planeEquations = new AlignedVector3Array();

                    int brushID = bspLoader.LeafBrushes[leaf.FirstLeafBrush + b];
                    BspBrush brush = bspLoader.Brushes[brushID];

                    if (brush.ShaderNum != -1)
                    {
                        ContentFlags flags = bspLoader.IsVbsp ? (ContentFlags)brush.ShaderNum : bspLoader.Shaders[brush.ShaderNum].ContentFlags;
                        if ((flags & ContentFlags.Solid) == ContentFlags.Solid)
                        {
                            brush.ShaderNum = -1;

                            for (int p = 0; p < brush.NumSides; p++)
                            {
                                int sideid = brush.FirstSide + p;

                                BspBrushSide brushside = bspLoader.BrushSides[sideid];
                                int planeid = brushside.PlaneNum;
                                BspPlane plane = bspLoader.Planes[planeid];
                                Vector4 planeEq = new Vector4(plane.Normal, scaling * -plane.Distance);
                                planeEquations.Add(planeEq);
                                isValidBrush = true;
                            }
                            if (isValidBrush)
                            {
                                AlignedVector3Array vertices = new AlignedVector3Array();
                                GeometryUtil.GetVerticesFromPlaneEquations(planeEquations, vertices);

                                const bool isEntity = false;
                                Vector3 entityTarget = Vector3.Zero;
                                AddConvexVerticesCollider(vertices, isEntity, entityTarget);
                            }
                        }
                    }
                }
            }
            /*
            foreach (BspEntity entity in bspLoader.Entities)
            {
                if (entity.ClassName == "trigger_push")
                {
                }
            }
            */
        }

        public abstract void AddConvexVerticesCollider(AlignedVector3Array vertices, bool isEntity, Vector3 entityTargetLocation);
    }
}
