using BulletSharp.Math;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public enum GImpactShapeType
	{
		CompoundShape,
		TrimeshShapePart,
		TrimeshShape
	}

	public class TetrahedronShapeEx : BuSimplex1To4
	{
		public TetrahedronShapeEx()
			: base(btTetrahedronShapeEx_new())
		{
		}

		public void SetVertices(Vector3 v0, Vector3 v1, Vector3 v2, Vector3 v3)
		{
			btTetrahedronShapeEx_setVertices(Native, ref v0, ref v1, ref v2, ref v3);
		}
	}

	public abstract class GImpactShapeInterface : ConcaveShape
	{
		internal GImpactShapeInterface(IntPtr native)
			: base(native)
		{
		}

		public void GetBulletTetrahedron(int primitiveIndex, TetrahedronShapeEx tetrahedron)
		{
			btGImpactShapeInterface_getBulletTetrahedron(Native, primitiveIndex, tetrahedron.Native);
		}

		public void GetBulletTriangle(int primitiveIndex, TriangleShapeEx triangle)
		{
			btGImpactShapeInterface_getBulletTriangle(Native, primitiveIndex, triangle.Native);
		}

		public void GetChildAabb(int childIndex, Matrix transform, out Vector3 aabbMin, out Vector3 aabbMax)
		{
			btGImpactShapeInterface_getChildAabb(Native, childIndex, ref transform,
				out aabbMin, out aabbMax);
		}

		public abstract CollisionShape GetChildShape(int index);

		public Matrix GetChildTransform(int index)
		{
			Matrix value;
			btGImpactShapeInterface_getChildTransform(Native, index, out value);
			return value;
		}

		public void GetPrimitiveTriangle(int index, PrimitiveTriangle triangle)
		{
			btGImpactShapeInterface_getPrimitiveTriangle(Native, index, triangle.Native);
		}

		public void LockChildShapes()
		{
			btGImpactShapeInterface_lockChildShapes(Native);
		}

		public void PostUpdate()
		{
			btGImpactShapeInterface_postUpdate(Native);
		}

		public void ProcessAllTrianglesRayRef(TriangleCallback callback, ref Vector3 rayFrom,
			ref Vector3 rayTo)
		{
			btGImpactShapeInterface_processAllTrianglesRay(Native, callback.Native,
				ref rayFrom, ref rayTo);
		}

		public void ProcessAllTrianglesRay(TriangleCallback callback, Vector3 rayFrom,
			Vector3 rayTo)
		{
			btGImpactShapeInterface_processAllTrianglesRay(Native, callback.Native,
				ref rayFrom, ref rayTo);
		}

		public void RayTestRef(ref Vector3 rayFrom, ref Vector3 rayTo, RayResultCallback resultCallback)
		{
			btGImpactShapeInterface_rayTest(Native, ref rayFrom, ref rayTo, resultCallback.Native);
		}

		public void RayTest(Vector3 rayFrom, Vector3 rayTo, RayResultCallback resultCallback)
		{
			btGImpactShapeInterface_rayTest(Native, ref rayFrom, ref rayTo, resultCallback.Native);
		}

		public void SetChildTransform(int index, Matrix transform)
		{
			btGImpactShapeInterface_setChildTransform(Native, index, ref transform);
		}

		public void UnlockChildShapes()
		{
			btGImpactShapeInterface_unlockChildShapes(Native);
		}

		public void UpdateBound()
		{
			btGImpactShapeInterface_updateBound(Native);
		}
		/*
		public GImpactBoxSet BoxSet
		{
			get { return btGImpactShapeInterface_getBoxSet(_native); }
		}
		*/
		public bool ChildrenHasTransform => btGImpactShapeInterface_childrenHasTransform(Native);

		public abstract GImpactShapeType GImpactShapeType { get; }

		public bool HasBoxSet => btGImpactShapeInterface_hasBoxSet(Native);

		public Aabb LocalBox => new Aabb(btGImpactShapeInterface_getLocalBox(Native));

		public bool NeedsRetrieveTetrahedrons => btGImpactShapeInterface_needsRetrieveTetrahedrons(Native);

		public bool NeedsRetrieveTriangles => btGImpactShapeInterface_needsRetrieveTriangles(Native);

		public int NumChildShapes => btGImpactShapeInterface_getNumChildShapes(Native);

		public abstract PrimitiveManagerBase PrimitiveManager { get; }
	}

	public sealed class CompoundPrimitiveManager : PrimitiveManagerBase
	{
		internal CompoundPrimitiveManager(IntPtr native, GImpactCompoundShape compoundShape)
			: base(native)
		{
			CompoundShape = compoundShape;
		}

		public GImpactCompoundShape CompoundShape { get; }
	}

	public class GImpactCompoundShape : GImpactShapeInterface
	{
		private CompoundPrimitiveManager _compoundPrimitiveManager;
		private List<CollisionShape> _childShapes = new List<CollisionShape>();

		internal GImpactCompoundShape(IntPtr native)
			: base(native)
		{
		}

		public GImpactCompoundShape(bool childrenHasTransform = true)
			: base(btGImpactCompoundShape_new(childrenHasTransform))
		{
		}

		public void AddChildShape(Matrix localTransform, CollisionShape shape)
		{
			_childShapes.Add(shape);
			btGImpactCompoundShape_addChildShape(Native, ref localTransform, shape.Native);
		}

		public void AddChildShape(CollisionShape shape)
		{
			_childShapes.Add(shape);
			btGImpactCompoundShape_addChildShape2(Native, shape.Native);
		}

		public override CollisionShape GetChildShape(int index)
		{
			return _childShapes[index];
		}

		public override PrimitiveManagerBase PrimitiveManager => CompoundPrimitiveManager;

		public CompoundPrimitiveManager CompoundPrimitiveManager
		{
			get
			{
				if (_compoundPrimitiveManager == null)
				{
					_compoundPrimitiveManager = new CompoundPrimitiveManager(
						btGImpactCompoundShape_getCompoundPrimitiveManager(Native), this);
				}
				return _compoundPrimitiveManager;
			}
		}

		public override GImpactShapeType GImpactShapeType => GImpactShapeType.CompoundShape;
	}

	public sealed class TrimeshPrimitiveManager : PrimitiveManagerBase
	{
		private StridingMeshInterface _meshInterface;

		internal TrimeshPrimitiveManager(IntPtr native)
			: base(native)
		{
		}

		public TrimeshPrimitiveManager(StridingMeshInterface meshInterface, int part)
			: base(btGImpactMeshShapePart_TrimeshPrimitiveManager_new(meshInterface.Native,
				part))
		{
			_meshInterface = meshInterface;
		}

		public TrimeshPrimitiveManager(TrimeshPrimitiveManager manager)
			: base(btGImpactMeshShapePart_TrimeshPrimitiveManager_new2(manager.Native))
		{
		}

		public TrimeshPrimitiveManager()
			: base(btGImpactMeshShapePart_TrimeshPrimitiveManager_new3())
		{
		}

		public void GetBulletTriangle(int primIndex, TriangleShapeEx triangle)
		{
			btGImpactMeshShapePart_TrimeshPrimitiveManager_get_bullet_triangle(
				Native, primIndex, triangle.Native);
		}

		public void GetIndices(int faceIndex, out uint i0, out uint i1, out uint i2b)
		{
			btGImpactMeshShapePart_TrimeshPrimitiveManager_get_indices(Native,
				faceIndex, out i0, out i1, out i2b);
		}

		public void GetVertex(uint vertexIndex, out Vector3 vertex)
		{
			btGImpactMeshShapePart_TrimeshPrimitiveManager_get_vertex(Native,
				vertexIndex, out vertex);
		}

		public void Lock()
		{
			btGImpactMeshShapePart_TrimeshPrimitiveManager_lock(Native);
		}

		public void Unlock()
		{
			btGImpactMeshShapePart_TrimeshPrimitiveManager_unlock(Native);
		}

		public IntPtr IndexBase
		{
			get => btGImpactMeshShapePart_TrimeshPrimitiveManager_getIndexbase(Native);
			set => btGImpactMeshShapePart_TrimeshPrimitiveManager_setIndexbase(Native, value);
		}

		public int IndexStride
		{
			get => btGImpactMeshShapePart_TrimeshPrimitiveManager_getIndexstride(Native);
			set => btGImpactMeshShapePart_TrimeshPrimitiveManager_setIndexstride(Native, value);
		}

		public PhyScalarType IndicesType
		{
			get => btGImpactMeshShapePart_TrimeshPrimitiveManager_getIndicestype(Native);
			set => btGImpactMeshShapePart_TrimeshPrimitiveManager_setIndicestype(Native, value);
		}

		public int LockCount
		{
			get => btGImpactMeshShapePart_TrimeshPrimitiveManager_getLock_count(Native);
			set => btGImpactMeshShapePart_TrimeshPrimitiveManager_setLock_count(Native, value);
		}

		public float Margin
		{
			get => btGImpactMeshShapePart_TrimeshPrimitiveManager_getMargin(Native);
			set => btGImpactMeshShapePart_TrimeshPrimitiveManager_setMargin(Native, value);
		}

		public StridingMeshInterface MeshInterface
		{
			get => _meshInterface;
			set
			{
				btGImpactMeshShapePart_TrimeshPrimitiveManager_setMeshInterface(Native, value.Native);
				_meshInterface = value;
			}
		}

		public int Numfaces
		{
			get => btGImpactMeshShapePart_TrimeshPrimitiveManager_getNumfaces(Native);
			set => btGImpactMeshShapePart_TrimeshPrimitiveManager_setNumfaces(Native, value);
		}

		public int Numverts
		{
			get => btGImpactMeshShapePart_TrimeshPrimitiveManager_getNumverts(Native);
			set => btGImpactMeshShapePart_TrimeshPrimitiveManager_setNumverts(Native, value);
		}

		public int Part
		{
			get => btGImpactMeshShapePart_TrimeshPrimitiveManager_getPart(Native);
			set => btGImpactMeshShapePart_TrimeshPrimitiveManager_setPart(Native, value);
		}

		public Vector3 Scale
		{
			get
			{
				Vector3 value;
				btGImpactMeshShapePart_TrimeshPrimitiveManager_getScale(Native, out value);
				return value;
			}
			set => btGImpactMeshShapePart_TrimeshPrimitiveManager_setScale(Native, ref value);
		}

		public int Stride
		{
			get => btGImpactMeshShapePart_TrimeshPrimitiveManager_getStride(Native);
			set => btGImpactMeshShapePart_TrimeshPrimitiveManager_setStride(Native, value);
		}

		public PhyScalarType Type
		{
			get => btGImpactMeshShapePart_TrimeshPrimitiveManager_getType(Native);
			set => btGImpactMeshShapePart_TrimeshPrimitiveManager_setType(Native, value);
		}

		public IntPtr VertexBase
		{
			get => btGImpactMeshShapePart_TrimeshPrimitiveManager_getVertexbase(Native);
			set => btGImpactMeshShapePart_TrimeshPrimitiveManager_setVertexbase(Native, value);
		}

		public int VertexCount => btGImpactMeshShapePart_TrimeshPrimitiveManager_get_vertex_count(Native);
	}

	public class GImpactMeshShapePart : GImpactShapeInterface
	{
		private TrimeshPrimitiveManager _gImpactTrimeshPrimitiveManager;

		internal GImpactMeshShapePart(IntPtr native)
			: base(native)
		{
		}

		public GImpactMeshShapePart()
			: base(btGImpactMeshShapePart_new())
		{
		}

		public GImpactMeshShapePart(StridingMeshInterface meshInterface, int part)
			: base(btGImpactMeshShapePart_new2(meshInterface.Native, part))
		{
		}

		public override CollisionShape GetChildShape(int index)
		{
			throw new InvalidOperationException();
		}

		public void GetVertex(int vertexIndex, out Vector3 vertex)
		{
			btGImpactMeshShapePart_getVertex(Native, vertexIndex, out vertex);
		}

		public override GImpactShapeType GImpactShapeType => GImpactShapeType.TrimeshShapePart;

		public TrimeshPrimitiveManager GImpactTrimeshPrimitiveManager
		{
			get
			{
				if (_gImpactTrimeshPrimitiveManager == null)
				{
					_gImpactTrimeshPrimitiveManager = new TrimeshPrimitiveManager(
						btGImpactMeshShapePart_getTrimeshPrimitiveManager(Native));
				}
				return _gImpactTrimeshPrimitiveManager;
			}
		}

		public int Part => btGImpactMeshShapePart_getPart(Native);

		public override PrimitiveManagerBase PrimitiveManager => GImpactTrimeshPrimitiveManager;

		public int VertexCount => btGImpactMeshShapePart_getVertexCount(Native);
	}

	public class GImpactMeshShape : GImpactShapeInterface
	{
		private StridingMeshInterface _meshInterface;
		private bool _disposeMeshInterface;

		internal GImpactMeshShape(IntPtr native)
			: base(native)
		{
		}

		public GImpactMeshShape(StridingMeshInterface meshInterface)
			: base(btGImpactMeshShape_new(meshInterface.Native))
		{
			_meshInterface = meshInterface;
		}

		public override CollisionShape GetChildShape(int index)
		{
			throw new InvalidOperationException();
		}

		public GImpactMeshShapePart GetMeshPart(int index)
		{
			return new GImpactMeshShapePart(btGImpactMeshShape_getMeshPart(Native, index));
		}

		public StridingMeshInterface MeshInterface
		{
			get
			{
				if (_meshInterface == null)
				{
					_meshInterface = new StridingMeshInterface(btGImpactMeshShape_getMeshInterface(Native));
					_disposeMeshInterface = true;
				}
				return _meshInterface;
			}
		}

		public int MeshPartCount => btGImpactMeshShape_getMeshPartCount(Native);

		public override PrimitiveManagerBase PrimitiveManager => null;

		public override GImpactShapeType GImpactShapeType => GImpactShapeType.TrimeshShape;

		protected override void Dispose(bool disposing)
		{
			if (disposing && _disposeMeshInterface)
			{
				_meshInterface.Dispose();
				_disposeMeshInterface = false;
			}
			base.Dispose(disposing);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	internal unsafe struct GImpactMeshShapeData
	{
		public CollisionShapeFloatData CollisionShapeData;
		public StridingMeshInterfaceData MeshInterface;
		public Vector3FloatData LocalScaling;
		public float CollisionMargin;
		public int GImpactSubType;

		public static int Offset(string fieldName) { return Marshal.OffsetOf(typeof(GImpactMeshShapeData), fieldName).ToInt32(); }
	}
}
