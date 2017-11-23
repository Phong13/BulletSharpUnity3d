using static BulletSharp.UnsafeNativeMethods;

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
			btDefaultSoftBodySolver_copySoftBodyToVertexBuffer(_native, softBody._native,
				vertexBuffer._native);
		}
		*/
	}
}
