using static BulletSharp.UnsafeNativeMethods;
namespace BulletSharp
{
	public class EmptyShape : ConcaveShape
	{
		public EmptyShape()
			: base(btEmptyShape_new())
		{
		}
	}
}
