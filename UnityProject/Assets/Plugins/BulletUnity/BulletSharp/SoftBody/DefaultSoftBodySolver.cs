

namespace BulletSharp.SoftBody
{
	public class DefaultSoftBodySolver : SoftBodySolver
	{
		public DefaultSoftBodySolver()
			: base(UnsafeNativeMethods.btDefaultSoftBodySolver_new())
		{
		}
		/*
		public void CopySoftBodyToVertexBuffer(SoftBody softBody, VertexBufferDescriptor vertexBuffer)
		{
			UnsafeNativeMethods.btDefaultSoftBodySolver_copySoftBodyToVertexBuffer(_native, softBody._native,
				vertexBuffer._native);
		}
		*/
	}
}
