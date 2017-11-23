

namespace BulletSharp
{
	public class NncgConstraintSolver : SequentialImpulseConstraintSolver
	{
		public NncgConstraintSolver()
			: base(UnsafeNativeMethods.btNNCGConstraintSolver_new(), false)
		{
		}

		public bool OnlyForNoneContact
		{
			get => UnsafeNativeMethods.btNNCGConstraintSolver_getOnlyForNoneContact(Native);
			set => UnsafeNativeMethods.btNNCGConstraintSolver_setOnlyForNoneContact(Native, value);
		}
	}
}
