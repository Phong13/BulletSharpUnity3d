using System;

namespace BulletSharp
{
	public abstract class ActivatingCollisionAlgorithm : CollisionAlgorithm
	{
		internal ActivatingCollisionAlgorithm(IntPtr native, bool preventDelete = false)
			: base(native, preventDelete)
		{
		}
	}
}
