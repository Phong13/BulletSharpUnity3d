using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
	public class HeightfieldTerrainShape : ConcaveShape
	{
		public HeightfieldTerrainShape(int heightStickWidth, int heightStickLength, IntPtr heightfieldData, float heightScale, float minHeight, float maxHeight, int upAxis, PhyScalarType heightDataType, bool flipQuadEdges)
			: base(btHeightfieldTerrainShape_new(heightStickWidth, heightStickLength, heightfieldData, heightScale, minHeight, maxHeight, upAxis, heightDataType, flipQuadEdges))
		{
		}

		public HeightfieldTerrainShape(int heightStickWidth, int heightStickLength, IntPtr heightfieldData, float maxHeight, int upAxis, bool useFloatData, bool flipQuadEdges)
			: base(btHeightfieldTerrainShape_new2(heightStickWidth, heightStickLength, heightfieldData, maxHeight, upAxis, useFloatData, flipQuadEdges))
		{
		}

		public void SetUseDiamondSubdivision()
		{
			btHeightfieldTerrainShape_setUseDiamondSubdivision(_native);
		}

		public void SetUseDiamondSubdivision(bool useDiamondSubdivision)
		{
			btHeightfieldTerrainShape_setUseDiamondSubdivision2(_native, useDiamondSubdivision);
		}

		public void SetUseZigzagSubdivision()
		{
			btHeightfieldTerrainShape_setUseZigzagSubdivision(_native);
		}

		public void SetUseZigzagSubdivision(bool useZigzagSubdivision)
		{
			btHeightfieldTerrainShape_setUseZigzagSubdivision2(_native, useZigzagSubdivision);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btHeightfieldTerrainShape_new(int heightStickWidth, int heightStickLength, IntPtr heightfieldData, float heightScale, float minHeight, float maxHeight, int upAxis, PhyScalarType heightDataType, bool flipQuadEdges);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btHeightfieldTerrainShape_new2(int heightStickWidth, int heightStickLength, IntPtr heightfieldData, float maxHeight, int upAxis, bool useFloatData, bool flipQuadEdges);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btHeightfieldTerrainShape_setUseDiamondSubdivision(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btHeightfieldTerrainShape_setUseDiamondSubdivision2(IntPtr obj, bool useDiamondSubdivision);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btHeightfieldTerrainShape_setUseZigzagSubdivision(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btHeightfieldTerrainShape_setUseZigzagSubdivision2(IntPtr obj, bool useZigzagSubdivision);
	}
}
