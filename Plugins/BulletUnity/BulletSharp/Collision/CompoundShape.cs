using System;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.Math;

namespace BulletSharp
{
	public class CompoundShapeChild
	{
		internal IntPtr _native;

		private CollisionShape _childShape;

        internal CompoundShapeChild(IntPtr native, CollisionShape childShape)
		{
			_native = native;
            _childShape = childShape;
		}

		public float ChildMargin
		{
			get { return btCompoundShapeChild_getChildMargin(_native); }
			set { btCompoundShapeChild_setChildMargin(_native, value); }
		}

		public CollisionShape ChildShape
		{
			get { return _childShape; }
			set
			{
				btCompoundShapeChild_setChildShape(_native, value._native);
				_childShape = value;
			}
		}

        public BroadphaseNativeType ChildShapeType
		{
			get { return btCompoundShapeChild_getChildShapeType(_native); }
			set { btCompoundShapeChild_setChildShapeType(_native, value); }
		}

		public DbvtNode Node
		{
            get
            {
                IntPtr ptr = btCompoundShapeChild_getNode(_native);
                return (ptr != IntPtr.Zero) ? new DbvtNode(ptr) : null;
            }
            set { btCompoundShapeChild_setNode(_native, (value != null) ? value._native : IntPtr.Zero); }
		}

		public Matrix Transform
		{
			get
			{
				Matrix value;
				btCompoundShapeChild_getTransform(_native, out value);
				return value;
			}
			set { btCompoundShapeChild_setTransform(_native, ref value); }
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btCompoundShapeChild_getChildMargin(IntPtr obj);
		//[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		//static extern IntPtr btCompoundShapeChild_getChildShape(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern BroadphaseNativeType btCompoundShapeChild_getChildShapeType(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btCompoundShapeChild_getNode(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCompoundShapeChild_getTransform(IntPtr obj, [Out] out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCompoundShapeChild_setChildMargin(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCompoundShapeChild_setChildShape(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCompoundShapeChild_setChildShapeType(IntPtr obj, BroadphaseNativeType value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCompoundShapeChild_setNode(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCompoundShapeChild_setTransform(IntPtr obj, [In] ref Matrix value);
	}

	public class CompoundShape : CollisionShape
	{
        private CompoundShapeChildArray _childList;

		public CompoundShape()
			: base(btCompoundShape_new())
		{
            _childList = new CompoundShapeChildArray(_native);
		}

		public CompoundShape(bool enableDynamicAabbTree)
			: base(btCompoundShape_new2(enableDynamicAabbTree))
		{
            _childList = new CompoundShapeChildArray(_native);
		}

		public CompoundShape(bool enableDynamicAabbTree, int initialChildCapacity)
			: base(btCompoundShape_new3(enableDynamicAabbTree, initialChildCapacity))
		{
            _childList = new CompoundShapeChildArray(_native);
		}

        public void AddChildShapeRef(ref Matrix localTransform, CollisionShape shape)
        {
            _childList.AddChildShape(ref localTransform, shape);
        }

		public void AddChildShape(Matrix localTransform, CollisionShape shape)
		{
            _childList.AddChildShape(ref localTransform, shape);
		}

        public void CalculatePrincipalAxisTransform(float[] masses, ref Matrix principal, ref Vector3 inertia)
		{
            btCompoundShape_calculatePrincipalAxisTransform(_native, masses, ref principal, ref inertia);
		}

		public void CreateAabbTreeFromChildren()
		{
			btCompoundShape_createAabbTreeFromChildren(_native);
		}

		public CollisionShape GetChildShape(int index)
		{
            return _childList[index].ChildShape;
		}

        public void GetChildTransform(int index, out Matrix value)
        {
            btCompoundShape_getChildTransform(_native, index, out value);
        }

		public Matrix GetChildTransform(int index)
		{
			Matrix value;
			btCompoundShape_getChildTransform(_native, index, out value);
			return value;
		}

		public void RecalculateLocalAabb()
		{
			btCompoundShape_recalculateLocalAabb(_native);
		}

		public void RemoveChildShape(CollisionShape shape)
		{
            _childList.RemoveChildShape(shape);
		}

		public void RemoveChildShapeByIndex(int childShapeIndex)
		{
            _childList.RemoveChildShapeByIndex(childShapeIndex);
		}

		public void UpdateChildTransform(int childIndex, Matrix newChildTransform)
		{
			btCompoundShape_updateChildTransform(_native, childIndex, ref newChildTransform);
		}

		public void UpdateChildTransform(int childIndex, Matrix newChildTransform, bool shouldRecalculateLocalAabb)
		{
			btCompoundShape_updateChildTransform2(_native, childIndex, ref newChildTransform, shouldRecalculateLocalAabb);
		}

		public CompoundShapeChildArray ChildList
		{
            get { return _childList; }
		}

		public Dbvt DynamicAabbTree
		{
			get { return new Dbvt(btCompoundShape_getDynamicAabbTree(_native), true); }
		}

		public int NumChildShapes
		{
            get { return _childList.Count; }
		}

		public int UpdateRevision
		{
			get { return btCompoundShape_getUpdateRevision(_native); }
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btCompoundShape_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btCompoundShape_new2(bool enableDynamicAabbTree);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btCompoundShape_new3(bool enableDynamicAabbTree, int initialChildCapacity);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCompoundShape_calculatePrincipalAxisTransform(IntPtr obj, float[] masses, [In] ref Matrix principal, [In] ref Vector3 inertia);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCompoundShape_createAabbTreeFromChildren(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btCompoundShape_getChildShape(IntPtr obj, int index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCompoundShape_getChildTransform(IntPtr obj, int index, [Out] out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btCompoundShape_getDynamicAabbTree(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btCompoundShape_getUpdateRevision(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCompoundShape_recalculateLocalAabb(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCompoundShape_updateChildTransform(IntPtr obj, int childIndex, [In] ref Matrix newChildTransform);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCompoundShape_updateChildTransform2(IntPtr obj, int childIndex, [In] ref Matrix newChildTransform, bool shouldRecalculateLocalAabb);
	}

    [StructLayout(LayoutKind.Sequential)]
    internal struct CompoundShapeFloatData
    {
        public CollisionShapeFloatData CollisionShapeData;
        public IntPtr ChildShapePtr;
        public int NumChildShapes;
        public float CollisionMargin;

        public static int Offset(string fieldName) { return Marshal.OffsetOf(typeof(CompoundShapeFloatData), fieldName).ToInt32(); }
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct CompoundShapeChildFloatData
    {
        public TransformFloatData Transform;
        public IntPtr ChildShape;
        public int ChildShapeType;
        public float ChildMargin;

        public static int Offset(string fieldName) { return Marshal.OffsetOf(typeof(CompoundShapeChildFloatData), fieldName).ToInt32(); }
    }
}
