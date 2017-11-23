using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class NncgConstraintSolver : SequentialImpulseConstraintSolver
	{
		public NncgConstraintSolver()
			: base(btNNCGConstraintSolver_new(), false)
		{
		}

		public bool OnlyForNoneContact
		{
			get => btNNCGConstraintSolver_getOnlyForNoneContact(Native);
			set => btNNCGConstraintSolver_setOnlyForNoneContact(Native, value);
		}
	}
}
