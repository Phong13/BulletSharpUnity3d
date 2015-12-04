using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp.SoftBody
{
	public class DefaultSoftBodySolver : SoftBodySolver
	{
		public DefaultSoftBodySolver()
			: base(btDefaultSoftBodySolver_new())
		{
		}
        /*
		public void CopySoftBodyToVertexBuffer(SoftBody softBody, VertexBufferDescriptor vertexBuffer)
		{
			btDefaultSoftBodySolver_copySoftBodyToVertexBuffer(_native, softBody._native, vertexBuffer._native);
		}
        */
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btDefaultSoftBodySolver_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDefaultSoftBodySolver_copySoftBodyToVertexBuffer(IntPtr obj, IntPtr softBody, IntPtr vertexBuffer);
	}
}
