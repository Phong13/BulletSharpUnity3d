using System;


namespace BulletSharp
{
	public class HeightfieldTerrainShape : ConcaveShape
	{
		public HeightfieldTerrainShape(int heightStickWidth, int heightStickLength,
			IntPtr heightfieldData, float heightScale, float minHeight, float maxHeight,
			int upAxis, PhyScalarType heightDataType, bool flipQuadEdges)
			: base(UnsafeNativeMethods.btHeightfieldTerrainShape_new(heightStickWidth, heightStickLength,
				heightfieldData, heightScale, minHeight, maxHeight, upAxis, heightDataType,
				flipQuadEdges))
		{
		}

		public void SetUseDiamondSubdivision()
		{
			UnsafeNativeMethods.btHeightfieldTerrainShape_setUseDiamondSubdivision(Native);
		}

		public void SetUseDiamondSubdivision(bool useDiamondSubdivision)
		{
			UnsafeNativeMethods.btHeightfieldTerrainShape_setUseDiamondSubdivision2(Native, useDiamondSubdivision);
		}

		public void SetUseZigzagSubdivision()
		{
			UnsafeNativeMethods.btHeightfieldTerrainShape_setUseZigzagSubdivision(Native);
		}

		public void SetUseZigzagSubdivision(bool useZigzagSubdivision)
		{
			UnsafeNativeMethods.btHeightfieldTerrainShape_setUseZigzagSubdivision2(Native, useZigzagSubdivision);
		}
	}
}
