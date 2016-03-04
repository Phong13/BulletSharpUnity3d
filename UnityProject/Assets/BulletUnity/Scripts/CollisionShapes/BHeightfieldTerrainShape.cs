using System;
using UnityEngine;
using System.Collections;
using BulletSharp;
using System.Runtime.InteropServices;

namespace BulletUnity {
    public class BHeightfieldTerrainShape : BCollisionShape {
        public int width;
        public int length;
        public float heightScale;
        public float maxHeight;
        public int upIndex;
        public GCHandle pinnedTerrainData;
        PhyScalarType scalarType = PhyScalarType.Float;


        public override void OnDrawGizmosSelected() {

        }
        
        public override CollisionShape GetCollisionShape() {
            if (collisionShapePtr == null) {
                
                //generate procedural data
                byte[] terr = new byte[width * length * 4];
                System.IO.MemoryStream file = new System.IO.MemoryStream(terr);
                System.IO.BinaryWriter writer = new System.IO.BinaryWriter(file);
                for (int i = 0; i < width; i++)
                    for (int j = 0; j < length; j++)
                        writer.Write((float)((maxHeight / 2) + 4 * Math.Sin(j * 0.5f) * Math.Cos(i)));
                writer.Flush();
                file.Position = 0;
                
                pinnedTerrainData = GCHandle.Alloc(terr,GCHandleType.Pinned);
                collisionShapePtr = new HeightfieldTerrainShape(128, 128, pinnedTerrainData.AddrOfPinnedObject(), heightScale, 0, maxHeight, upIndex, scalarType, false);
                ((HeightfieldTerrainShape)collisionShapePtr).SetUseDiamondSubdivision(true);
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
