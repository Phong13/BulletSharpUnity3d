using System;
using System.Runtime.InteropServices;
using BulletSharp.Math;
using BulletSharp;

namespace BulletSharp.InverseDynamics
{

    public class MultiBodyTreeCreator : IDisposable
    {
        internal IntPtr Native;
        private bool _preventDelete;
        private bool _isDisposed;
        
		public MultiBodyTreeCreator(bool preventDelete = false)
		{
            Native = UnsafeNativeMethodsInverseDynamics.MultiBodyTreeCreator_new();
            if (preventDelete)
            {
                _preventDelete = true;
            }
        }

        public int CreateFromMultiBody(MultiBody multiBody)
        {
            return UnsafeNativeMethodsInverseDynamics.MultiBodyTreeCreator_createFromBtMultiBody(Native, multiBody.Native);
        }

        public MultiBodyTree CreateMultiBodyTree()
        {
            return new MultiBodyTree(UnsafeNativeMethodsInverseDynamics.MultiBodyTreeCreator_CreateMultiBodyTree(Native));
        }

        public int GetNumBodies()
        {
            return UnsafeNativeMethodsInverseDynamics.MultiBodyTreeCreator_getNumBodies(Native);
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                _isDisposed = true;

                if (!_preventDelete)
                {
                    //UnityEngine.Debug.LogError("TODO userPtr");
                    //IntPtr userPtr = UnsafeNativeMethods.btCollisionShape_getUserPointer(Native);
                    //GCHandle.FromIntPtr(userPtr).Free();
                    //UnsafeNativeMethodsInverseDynamics.MultiBodyTreeCreator_delete(Native);
                }
            }
        }

        ~MultiBodyTreeCreator()
        {
            Dispose(false);
        }
    }
}
