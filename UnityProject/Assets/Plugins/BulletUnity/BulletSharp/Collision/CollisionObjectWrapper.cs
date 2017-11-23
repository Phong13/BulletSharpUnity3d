using BulletSharp.Math;
using System;


namespace BulletSharp
{
	public class CollisionObjectWrapper
	{
		internal IntPtr Native;

		internal CollisionObjectWrapper(IntPtr native)
		{
			Native = native;
		}

		public CollisionObject CollisionObject
		{
			get { return CollisionObject.GetManaged(UnsafeNativeMethods.btCollisionObjectWrapper_getCollisionObject(Native)); }
			set { UnsafeNativeMethods.btCollisionObjectWrapper_setCollisionObject(Native, value.Native); }
		}

		public CollisionShape CollisionShape
		{
            get
            { return CollisionShape.GetManaged(UnsafeNativeMethods.btCollisionObjectWrapper_getCollisionShape(Native)); }
			set { UnsafeNativeMethods.btCollisionObjectWrapper_setShape(Native, value.Native); }
		}

		public int Index
		{
            get
            { return UnsafeNativeMethods.btCollisionObjectWrapper_getIndex(Native); }
			set { UnsafeNativeMethods.btCollisionObjectWrapper_setIndex(Native, value); }
		}

		public CollisionObjectWrapper Parent
		{
            get
            { return new CollisionObjectWrapper(UnsafeNativeMethods.btCollisionObjectWrapper_getParent(Native)); }
			set { UnsafeNativeMethods.btCollisionObjectWrapper_setParent(Native, value.Native); }
		}

		public int PartId
		{
            get
            { return UnsafeNativeMethods.btCollisionObjectWrapper_getPartId(Native); }
			set { UnsafeNativeMethods.btCollisionObjectWrapper_setPartId(Native, value); }
		}

		public Matrix WorldTransform
		{
			get
			{
				Matrix value;
                UnsafeNativeMethods.btCollisionObjectWrapper_getWorldTransform(Native, out value);
				return value;
			}
		}
	}
}
