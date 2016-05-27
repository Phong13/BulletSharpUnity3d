using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.Math;

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
			btTetrahedronShapeEx_setVertices(_native, ref v0, ref v1, ref v2, ref v3);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btTetrahedronShapeEx_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTetrahedronShapeEx_setVertices(IntPtr obj, [In] ref Vector3 v0, [In] ref Vector3 v1, [In] ref Vector3 v2, [In] ref Vector3 v3);
	}

	public abstract class GImpactShapeInterface : ConcaveShape
	{
        protected List<CollisionShape> _childShapes = new List<CollisionShape>();

		internal GImpactShapeInterface(IntPtr native)
			: base(native)
		{
		}

		public void GetBulletTetrahedron(int primIndex, TetrahedronShapeEx tetrahedron)
		{
			btGImpactShapeInterface_getBulletTetrahedron(_native, primIndex, tetrahedron._native);
		}
		/*
		public void GetBulletTriangle(int primIndex, TriangleShapeEx triangle)
		{
			btGImpactShapeInterface_getBulletTriangle(_native, primIndex, triangle._native);
		}
		*/

		public void GetChildAabb(int childIndex, Matrix t, out Vector3 aabbMin, out Vector3 aabbMax)
		{
			btGImpactShapeInterface_getChildAabb(_native, childIndex, ref t, out aabbMin, out aabbMax);
		}

		public CollisionShape GetChildShape(int index)
		{
            return _childShapes[index];
		}

		public Matrix GetChildTransform(int index)
		{
			Matrix value;
			btGImpactShapeInterface_getChildTransform(_native, index, out value);
			return value;
		}
		/*
		public void GetPrimitiveTriangle(int index, PrimitiveTriangle triangle)
		{
			btGImpactShapeInterface_getPrimitiveTriangle(_native, index, triangle._native);
		}
		*/

		public void LockChildShapes()
		{
			btGImpactShapeInterface_lockChildShapes(_native);
		}

		public void PostUpdate()
		{
			btGImpactShapeInterface_postUpdate(_native);
		}

        public void ProcessAllTrianglesRay(TriangleCallback cb, ref Vector3 rayFrom, ref Vector3 rayTo)
		{
            btGImpactShapeInterface_processAllTrianglesRay(_native, cb._native, ref rayFrom, ref rayTo);
		}

		public void RayTest(Vector3 rayFrom, Vector3 rayTo, RayResultCallback resultCallback)
		{
			btGImpactShapeInterface_rayTest(_native, ref rayFrom, ref rayTo, resultCallback._native);
		}

		public void SetChildTransform(int index, Matrix transform)
		{
			btGImpactShapeInterface_setChildTransform(_native, index, ref transform);
		}

		public void UnlockChildShapes()
		{
			btGImpactShapeInterface_unlockChildShapes(_native);
		}

		public void UpdateBound()
		{
			btGImpactShapeInterface_updateBound(_native);
		}
        /*
		public GImpactBoxSet BoxSet
		{
			get { return btGImpactShapeInterface_getBoxSet(_native); }
		}
        */
        public bool ChildrenHasTransform
        {
            get{return btGImpactShapeInterface_childrenHasTransform(_native);}
        }

		public GImpactShapeType GImpactShapeType
		{
			get { return btGImpactShapeInterface_getGImpactShapeType(_native); }
		}

		public bool HasBoxSet
		{
			get { return btGImpactShapeInterface_hasBoxSet(_native); }
		}

		public Aabb LocalBox
		{
            get { return new Aabb(btGImpactShapeInterface_getLocalBox(_native), true); }
		}

		public bool NeedsRetrieveTetrahedrons
		{
			get { return btGImpactShapeInterface_needsRetrieveTetrahedrons(_native); }
		}

		public bool NeedsRetrieveTriangles
		{
			get { return btGImpactShapeInterface_needsRetrieveTriangles(_native); }
		}

		public int NumChildShapes
		{
			get { return btGImpactShapeInterface_getNumChildShapes(_native); }
		}

        public abstract PrimitiveManagerBase PrimitiveManager
        {
            get;
        }

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btGImpactShapeInterface_childrenHasTransform(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btGImpactShapeInterface_getBoxSet(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGImpactShapeInterface_getBulletTetrahedron(IntPtr obj, int prim_index, IntPtr tetrahedron);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGImpactShapeInterface_getBulletTriangle(IntPtr obj, int prim_index, IntPtr triangle);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGImpactShapeInterface_getChildAabb(IntPtr obj, int child_index, [In] ref Matrix t, [Out] out Vector3 aabbMin, [Out] out Vector3 aabbMax);
		//[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		//static extern IntPtr btGImpactShapeInterface_getChildShape(IntPtr obj, int index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGImpactShapeInterface_getChildTransform(IntPtr obj, int index, [Out] out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern GImpactShapeType btGImpactShapeInterface_getGImpactShapeType(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btGImpactShapeInterface_getLocalBox(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btGImpactShapeInterface_getNumChildShapes(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btGImpactShapeInterface_getPrimitiveManager(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGImpactShapeInterface_getPrimitiveTriangle(IntPtr obj, int index, IntPtr triangle);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btGImpactShapeInterface_hasBoxSet(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGImpactShapeInterface_lockChildShapes(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btGImpactShapeInterface_needsRetrieveTetrahedrons(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btGImpactShapeInterface_needsRetrieveTriangles(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGImpactShapeInterface_postUpdate(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGImpactShapeInterface_processAllTrianglesRay(IntPtr obj, IntPtr __unnamed0, [In] ref Vector3 __unnamed1, [In] ref Vector3 __unnamed2);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGImpactShapeInterface_rayTest(IntPtr obj, [In] ref Vector3 rayFrom, [In] ref Vector3 rayTo, IntPtr resultCallback);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGImpactShapeInterface_setChildTransform(IntPtr obj, int index, [In] ref Matrix transform);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGImpactShapeInterface_unlockChildShapes(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGImpactShapeInterface_updateBound(IntPtr obj);
	}

	public class CompoundPrimitiveManager : PrimitiveManagerBase
	{
        GImpactCompoundShape _compoundShape;

        internal CompoundPrimitiveManager(IntPtr native, GImpactCompoundShape compoundShape)
			: base(native)
		{
            _compoundShape = compoundShape;
		}

		public GImpactCompoundShape CompoundShape
		{
			get { return _compoundShape; }
		}
	}

	public class GImpactCompoundShape : GImpactShapeInterface
	{
        private CompoundPrimitiveManager _compoundPrimitiveManager;

		internal GImpactCompoundShape(IntPtr native)
			: base(native)
		{
		}

		public GImpactCompoundShape()
			: base(btGImpactCompoundShape_new())
		{
		}

		public GImpactCompoundShape(bool childrenHasTransform)
			: base(btGImpactCompoundShape_new2(childrenHasTransform))
		{
		}

		public void AddChildShape(Matrix localTransform, CollisionShape shape)
		{
            _childShapes.Add(shape);
			btGImpactCompoundShape_addChildShape(_native, ref localTransform, shape._native);
		}

		public void AddChildShape(CollisionShape shape)
		{
            _childShapes.Add(shape);
			btGImpactCompoundShape_addChildShape2(_native, shape._native);
		}

        public override PrimitiveManagerBase PrimitiveManager
        {
            get { return CompoundPrimitiveManager; }
        }

		public CompoundPrimitiveManager CompoundPrimitiveManager
		{
            get
            {
                if (_compoundPrimitiveManager == null)
                {
                    _compoundPrimitiveManager = new CompoundPrimitiveManager(btGImpactCompoundShape_getCompoundPrimitiveManager(_native), this);
                }
                return _compoundPrimitiveManager;
            }
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btGImpactCompoundShape_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btGImpactCompoundShape_new2(bool children_has_transform);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGImpactCompoundShape_addChildShape(IntPtr obj, [In] ref Matrix localTransform, IntPtr shape);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGImpactCompoundShape_addChildShape2(IntPtr obj, IntPtr shape);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btGImpactCompoundShape_getCompoundPrimitiveManager(IntPtr obj);
	}

	public class TrimeshPrimitiveManager : PrimitiveManagerBase
	{
		private StridingMeshInterface _meshInterface;

		internal TrimeshPrimitiveManager(IntPtr native)
			: base(native)
		{
		}

		public TrimeshPrimitiveManager(StridingMeshInterface meshInterface, int part)
			: base(btGImpactMeshShapePart_TrimeshPrimitiveManager_new(meshInterface._native, part))
		{
			_meshInterface = meshInterface;
		}

		public TrimeshPrimitiveManager(TrimeshPrimitiveManager manager)
			: base(btGImpactMeshShapePart_TrimeshPrimitiveManager_new2(manager._native))
		{
		}

		public TrimeshPrimitiveManager()
			: base(btGImpactMeshShapePart_TrimeshPrimitiveManager_new3())
		{
		}
		/*
		public void GetBulletTriangle(int primIndex, TriangleShapeEx triangle)
		{
			btGImpactMeshShapePart_TrimeshPrimitiveManager_get_bullet_triangle(_native, primIndex, triangle._native);
		}
		*/

        public void GetIndices(int faceIndex, out uint i0, out uint i1, out uint i2)
		{
            btGImpactMeshShapePart_TrimeshPrimitiveManager_get_indices(_native, faceIndex, out i0, out i1, out i2);
		}

		public void GetVertex(uint vertexIndex, out Vector3 vertex)
		{
			btGImpactMeshShapePart_TrimeshPrimitiveManager_get_vertex(_native, vertexIndex, out vertex);
		}

		public void Lock()
		{
			btGImpactMeshShapePart_TrimeshPrimitiveManager_lock(_native);
		}

		public void Unlock()
		{
			btGImpactMeshShapePart_TrimeshPrimitiveManager_unlock(_native);
		}

		public IntPtr IndexBase
		{
			get { return btGImpactMeshShapePart_TrimeshPrimitiveManager_getIndexbase(_native); }
			set { btGImpactMeshShapePart_TrimeshPrimitiveManager_setIndexbase(_native, value); }
		}

		public int IndexStride
		{
			get { return btGImpactMeshShapePart_TrimeshPrimitiveManager_getIndexstride(_native); }
			set { btGImpactMeshShapePart_TrimeshPrimitiveManager_setIndexstride(_native, value); }
		}

		public PhyScalarType IndicesType
		{
			get { return btGImpactMeshShapePart_TrimeshPrimitiveManager_getIndicestype(_native); }
			set { btGImpactMeshShapePart_TrimeshPrimitiveManager_setIndicestype(_native, value); }
		}

		public int LockCount
		{
			get { return btGImpactMeshShapePart_TrimeshPrimitiveManager_getLock_count(_native); }
			set { btGImpactMeshShapePart_TrimeshPrimitiveManager_setLock_count(_native, value); }
		}

		public float Margin
		{
			get { return btGImpactMeshShapePart_TrimeshPrimitiveManager_getMargin(_native); }
			set { btGImpactMeshShapePart_TrimeshPrimitiveManager_setMargin(_native, value); }
		}

		public StridingMeshInterface MeshInterface
		{
			get { return _meshInterface; }
			set
			{
				btGImpactMeshShapePart_TrimeshPrimitiveManager_setMeshInterface(_native, value._native);
				_meshInterface = value;
			}
		}

		public int Numfaces
		{
			get { return btGImpactMeshShapePart_TrimeshPrimitiveManager_getNumfaces(_native); }
			set { btGImpactMeshShapePart_TrimeshPrimitiveManager_setNumfaces(_native, value); }
		}

		public int Numverts
		{
			get { return btGImpactMeshShapePart_TrimeshPrimitiveManager_getNumverts(_native); }
			set { btGImpactMeshShapePart_TrimeshPrimitiveManager_setNumverts(_native, value); }
		}

		public int Part
		{
			get { return btGImpactMeshShapePart_TrimeshPrimitiveManager_getPart(_native); }
			set { btGImpactMeshShapePart_TrimeshPrimitiveManager_setPart(_native, value); }
		}

		public Vector3 Scale
		{
			get
			{
				Vector3 value;
				btGImpactMeshShapePart_TrimeshPrimitiveManager_getScale(_native, out value);
				return value;
			}
			set { btGImpactMeshShapePart_TrimeshPrimitiveManager_setScale(_native, ref value); }
		}

		public int Stride
		{
			get { return btGImpactMeshShapePart_TrimeshPrimitiveManager_getStride(_native); }
			set { btGImpactMeshShapePart_TrimeshPrimitiveManager_setStride(_native, value); }
		}

		public PhyScalarType Type
		{
			get { return btGImpactMeshShapePart_TrimeshPrimitiveManager_getType(_native); }
			set { btGImpactMeshShapePart_TrimeshPrimitiveManager_setType(_native, value); }
		}

		public IntPtr VertexBase
		{
			get { return btGImpactMeshShapePart_TrimeshPrimitiveManager_getVertexbase(_native); }
			set { btGImpactMeshShapePart_TrimeshPrimitiveManager_setVertexbase(_native, value); }
		}

		public int VertexCount
		{
			get { return btGImpactMeshShapePart_TrimeshPrimitiveManager_get_vertex_count(_native); }
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btGImpactMeshShapePart_TrimeshPrimitiveManager_new(IntPtr meshInterface, int part);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btGImpactMeshShapePart_TrimeshPrimitiveManager_new2(IntPtr manager);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btGImpactMeshShapePart_TrimeshPrimitiveManager_new3();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGImpactMeshShapePart_TrimeshPrimitiveManager_get_bullet_triangle(IntPtr obj, int prim_index, IntPtr triangle);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btGImpactMeshShapePart_TrimeshPrimitiveManager_get_indices(IntPtr obj, int face_index, [Out] out uint i0, [Out] out uint i1, [Out] out uint i2);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGImpactMeshShapePart_TrimeshPrimitiveManager_get_vertex(IntPtr obj, uint vertex_index, [Out] out Vector3 vertex);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btGImpactMeshShapePart_TrimeshPrimitiveManager_get_vertex_count(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btGImpactMeshShapePart_TrimeshPrimitiveManager_getIndexbase(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btGImpactMeshShapePart_TrimeshPrimitiveManager_getIndexstride(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern PhyScalarType btGImpactMeshShapePart_TrimeshPrimitiveManager_getIndicestype(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btGImpactMeshShapePart_TrimeshPrimitiveManager_getLock_count(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btGImpactMeshShapePart_TrimeshPrimitiveManager_getMargin(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btGImpactMeshShapePart_TrimeshPrimitiveManager_getMeshInterface(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btGImpactMeshShapePart_TrimeshPrimitiveManager_getNumfaces(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btGImpactMeshShapePart_TrimeshPrimitiveManager_getNumverts(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btGImpactMeshShapePart_TrimeshPrimitiveManager_getPart(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGImpactMeshShapePart_TrimeshPrimitiveManager_getScale(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btGImpactMeshShapePart_TrimeshPrimitiveManager_getStride(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern PhyScalarType btGImpactMeshShapePart_TrimeshPrimitiveManager_getType(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btGImpactMeshShapePart_TrimeshPrimitiveManager_getVertexbase(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGImpactMeshShapePart_TrimeshPrimitiveManager_lock(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGImpactMeshShapePart_TrimeshPrimitiveManager_setIndexbase(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGImpactMeshShapePart_TrimeshPrimitiveManager_setIndexstride(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGImpactMeshShapePart_TrimeshPrimitiveManager_setIndicestype(IntPtr obj, PhyScalarType value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGImpactMeshShapePart_TrimeshPrimitiveManager_setLock_count(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGImpactMeshShapePart_TrimeshPrimitiveManager_setMargin(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGImpactMeshShapePart_TrimeshPrimitiveManager_setMeshInterface(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGImpactMeshShapePart_TrimeshPrimitiveManager_setNumfaces(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGImpactMeshShapePart_TrimeshPrimitiveManager_setNumverts(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGImpactMeshShapePart_TrimeshPrimitiveManager_setPart(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGImpactMeshShapePart_TrimeshPrimitiveManager_setScale(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGImpactMeshShapePart_TrimeshPrimitiveManager_setStride(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGImpactMeshShapePart_TrimeshPrimitiveManager_setType(IntPtr obj, PhyScalarType value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGImpactMeshShapePart_TrimeshPrimitiveManager_setVertexbase(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGImpactMeshShapePart_TrimeshPrimitiveManager_unlock(IntPtr obj);
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
			: base(btGImpactMeshShapePart_new2(meshInterface._native, part))
		{
		}

		public void GetVertex(int vertexIndex, out Vector3 vertex)
		{
			btGImpactMeshShapePart_getVertex(_native, vertexIndex, out vertex);
		}

		public int Part
		{
			get { return btGImpactMeshShapePart_getPart(_native); }
		}

        public override PrimitiveManagerBase PrimitiveManager
        {
            get { return GImpactTrimeshPrimitiveManager; }
        }

        public TrimeshPrimitiveManager GImpactTrimeshPrimitiveManager
		{
            get
            {
                if (_gImpactTrimeshPrimitiveManager == null)
                {
                    _gImpactTrimeshPrimitiveManager = new TrimeshPrimitiveManager(btGImpactMeshShapePart_getTrimeshPrimitiveManager(_native));
                }
                return _gImpactTrimeshPrimitiveManager;
            }
		}

		public int VertexCount
		{
			get { return btGImpactMeshShapePart_getVertexCount(_native); }
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btGImpactMeshShapePart_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btGImpactMeshShapePart_new2(IntPtr meshInterface, int part);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btGImpactMeshShapePart_getPart(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btGImpactMeshShapePart_getTrimeshPrimitiveManager(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGImpactMeshShapePart_getVertex(IntPtr obj, int vertex_index, [Out] out Vector3 vertex);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btGImpactMeshShapePart_getVertexCount(IntPtr obj);
	}

	public class GImpactMeshShape : GImpactShapeInterface
	{
		private StridingMeshInterface _meshInterface;
        bool _disposeMeshInterface;

		internal GImpactMeshShape(IntPtr native)
			: base(native)
		{
		}

		public GImpactMeshShape(StridingMeshInterface meshInterface)
			: base(btGImpactMeshShape_new(meshInterface._native))
		{
			_meshInterface = meshInterface;
		}

		public GImpactMeshShapePart GetMeshPart(int index)
		{
			return new GImpactMeshShapePart(btGImpactMeshShape_getMeshPart(_native, index));
		}

		public StridingMeshInterface MeshInterface
		{
            get
            {
                if (_meshInterface == null)
                {
                    _meshInterface = new StridingMeshInterface(btGImpactMeshShape_getMeshInterface(_native));
                    _disposeMeshInterface = true;
                }
                return _meshInterface;
            }
		}

		public int MeshPartCount
		{
			get { return btGImpactMeshShape_getMeshPartCount(_native); }
		}

        public override PrimitiveManagerBase PrimitiveManager
        {
            get { return null; }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _disposeMeshInterface)
            {
                _meshInterface.Dispose();
                _disposeMeshInterface = false;
            }
            base.Dispose(disposing);
        }

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btGImpactMeshShape_new(IntPtr meshInterface);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btGImpactMeshShape_getMeshInterface(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btGImpactMeshShape_getMeshPart(IntPtr obj, int index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btGImpactMeshShape_getMeshPartCount(IntPtr obj);
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
