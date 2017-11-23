using System;
using System.Runtime.InteropServices;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public abstract class TaskScheduler
	{
		internal readonly IntPtr Native;

		internal TaskScheduler(IntPtr native)
		{
			Native = native;
		}

		public int MaxNumThreads => btITaskScheduler_getMaxNumThreads(Native);

		public string Name => Marshal.PtrToStringAnsi(btITaskScheduler_getName(Native));

		public int NumThreads
		{
			get => btITaskScheduler_getNumThreads(Native);
			set => btITaskScheduler_setNumThreads(Native, value);
		}
	}

	public sealed class TaskSchedulerSequential : TaskScheduler
	{
		internal TaskSchedulerSequential(IntPtr native)
			: base(native)
		{
		}
	}

	public sealed class TaskSchedulerOpenMP : TaskScheduler
	{
		internal TaskSchedulerOpenMP(IntPtr native)
			: base(native)
		{
		}
	}

	public sealed class TaskSchedulerTbb : TaskScheduler
	{
		internal TaskSchedulerTbb(IntPtr native)
			: base(native)
		{
		}
	}

	public sealed class TaskSchedulerPpl : TaskScheduler
	{
		internal TaskSchedulerPpl(IntPtr native)
			: base(native)
		{
		}
	}

	public class Threads
	{
		private static TaskSchedulerOpenMP _taskSchedulerOpenMP;
		private static TaskSchedulerPpl _taskSchedulerPpl;
		private static TaskSchedulerSequential _taskSchedulerSequential;
		private static TaskSchedulerTbb _taskSchedulerTbb;
		private static TaskScheduler _taskScheduler;

		public static TaskScheduler TaskScheduler
		{
			get => _taskScheduler;
			set
			{
				_taskScheduler = value;
				btThreads_btSetTaskScheduler(value != null ? value.Native : IntPtr.Zero);
			}
		}

		public static TaskSchedulerOpenMP GetOpenMPTaskScheduler()
		{
			if (_taskSchedulerOpenMP == null)
			{
				IntPtr native = btThreads_btGetOpenMPTaskScheduler();
				if (native != IntPtr.Zero)
				{
					_taskSchedulerOpenMP = new TaskSchedulerOpenMP(native);
				}
			}
			return _taskSchedulerOpenMP;
		}


		public static TaskSchedulerPpl GetPplTaskScheduler()
		{
			if (_taskSchedulerPpl == null)
			{
				IntPtr native = btThreads_btGetPPLTaskScheduler();
				if (native != IntPtr.Zero)
				{
					_taskSchedulerPpl = new TaskSchedulerPpl(native);
				}
			}
			return _taskSchedulerPpl;
		}

		public static TaskSchedulerSequential GetSequentialTaskScheduler()
		{
			if (_taskSchedulerSequential == null)
			{
				_taskSchedulerSequential = new TaskSchedulerSequential(btThreads_btGetSequentialTaskScheduler());
			}
			return _taskSchedulerSequential;
		}

		public static TaskSchedulerTbb GetTbbTaskScheduler()
		{
			if (_taskSchedulerTbb == null)
			{
				IntPtr native = btThreads_btGetTBBTaskScheduler();
				if (native != IntPtr.Zero)
				{
					_taskSchedulerTbb = new TaskSchedulerTbb(native);
				}
			}
			return _taskSchedulerTbb;
		}
	}
}
