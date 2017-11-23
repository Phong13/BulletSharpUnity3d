
namespace BulletSharp
{
	public class EmptyShape : ConcaveShape
	{
		public EmptyShape()
			: base(UnsafeNativeMethods.btEmptyShape_new())
		{
		}
	}
}
