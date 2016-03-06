using System;
using UnityEngine;
using System.Collections;
using BulletSharp;
using System.Runtime.InteropServices;

namespace BulletUnity {
    public class BHeightfieldTerrainShape : BCollisionShape {

        public int upIndex;
        GCHandle pinnedTerrainData;
        PhyScalarType scalarType = PhyScalarType.Float;

        public void Awake()
        {
            Terrain t = GetComponent<Terrain>();
            if (t == null)
            {
                Debug.LogError("BHeightfieldTerrainShape must be attached to an object with a terrain." + name);
            }
        }

        public override void OnDrawGizmosSelected() {

        }
        
        public override CollisionShape GetCollisionShape() {
            if (collisionShapePtr == null) {
                Terrain t = GetComponent<Terrain>();
                TerrainData td = t.terrainData;
                int width = td.heightmapWidth;
                int length = td.heightmapHeight;
                float maxHeight = td.size.y / 2f;

                //generate procedural data
                byte[] terr = new byte[width * length * sizeof(float)];
                System.IO.MemoryStream file = new System.IO.MemoryStream(terr);
                System.IO.BinaryWriter writer = new System.IO.BinaryWriter(file);

                for (int i = 0; i < length; i++)
                {
                    float[,] row = td.GetHeights(0, i, width, 1);
                    for (int j = 0; j < width; j++)
                    {
                        writer.Write((float)row[0, j] * 1000f);
                    }
                }

                //2nd variation I tried flip x and y axis was wrong
                /*
                for (int i = 0; i < width; i++)
                {
                    float[,] col = td.GetHeights(i, 0, 1, length);
                    for (int j = 0; j < length; j++)
                    {
                        writer.Write((float) col[j,0] * 1000f);
                    }
                }
                */


                /*
                for (int i = length - 1; i >= 0; i--)
                {
                    float[,] row = td.GetHeights(0, i, width, 1);
                    for (int j = length - 1; j >= 0; j--)
                    {
                        writer.Write((float)row[0, j] * 1000f);
                    }
                }
                
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < length; j++)
                    {
                        writer.Write((float)(i));
                    }
                }
                */

                writer.Flush();
                file.Position = 0;
                
                pinnedTerrainData = GCHandle.Alloc(terr,GCHandleType.Pinned);

                collisionShapePtr = new HeightfieldTerrainShape(width, length, pinnedTerrainData.AddrOfPinnedObject(), 1f, 0f, maxHeight, upIndex, scalarType, false);
                ((HeightfieldTerrainShape)collisionShapePtr).SetUseDiamondSubdivision(true);
                //just allocated several hundred float arrays. Garbage collect now since 99% likely we just loaded the scene
                GC.Collect();
            }
            return collisionShapePtr;
        }

        protected override void Dispose(bool isdisposing)
        {
            if (collisionShapePtr != null)
            {
                collisionShapePtr.Dispose();
                collisionShapePtr = null;
            }
            if (pinnedTerrainData != null)
            {
                pinnedTerrainData.Free();
            }
        }
    }
}
