using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
	public class SimulationIslandManager : IDisposable
	{
		public abstract class IslandCallback : IDisposable
		{
			internal IntPtr _native;

			internal IslandCallback(IntPtr native)
			{
				_native = native;
			}
            /*
			public void ProcessIsland(CollisionObject bodies, int numBodies, PersistentManifold manifolds, int numManifolds, int islandId)
			{
				btSimulationIslandManager_IslandCallback_processIsland(_native, bodies._native, numBodies, manifolds._native, numManifolds, islandId);
			}
            */
			public void Dispose()
			{
				Dispose(true);
				GC.SuppressFinalize(this);
			}

			protected virtual void Dispose(bool disposing)
			{
				if (_native != IntPtr.Zero)
				{
					btSimulationIslandManager_IslandCallback_delete(_native);
					_native = IntPtr.Zero;
				}
			}

			~IslandCallback()
			{
				Dispose(false);
			}

			//[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			//static extern void btSimulationIslandManager_IslandCallback_processIsland(IntPtr obj, IntPtr bodies, int numBodies, IntPtr manifolds, int numManifolds, int islandId);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btSimulationIslandManager_IslandCallback_delete(IntPtr obj);
		}

		internal IntPtr _native;
		bool _preventDelete;

		internal SimulationIslandManager(IntPtr native, bool preventDelete)
		{
			_native = native;
			_preventDelete = preventDelete;
		}

		public SimulationIslandManager()
		{
			_native = btSimulationIslandManager_new();
		}

		public void BuildAndProcessIslands(Dispatcher dispatcher, CollisionWorld collisionWorld, IslandCallback callback)
		{
			btSimulationIslandManager_buildAndProcessIslands(_native, dispatcher._native, collisionWorld._native, callback._native);
		}

		public void BuildIslands(Dispatcher dispatcher, CollisionWorld colWorld)
		{
			btSimulationIslandManager_buildIslands(_native, dispatcher._native, colWorld._native);
		}

		public void FindUnions(Dispatcher dispatcher, CollisionWorld colWorld)
		{
			btSimulationIslandManager_findUnions(_native, dispatcher._native, colWorld._native);
		}

		public void InitUnionFind(int n)
		{
			btSimulationIslandManager_initUnionFind(_native, n);
		}

		public void StoreIslandActivationState(CollisionWorld world)
		{
			btSimulationIslandManager_storeIslandActivationState(_native, world._native);
		}

		public void UpdateActivationState(CollisionWorld colWorld, Dispatcher dispatcher)
		{
			btSimulationIslandManager_updateActivationState(_native, colWorld._native, dispatcher._native);
		}

		public bool SplitIslands
		{
			get { return btSimulationIslandManager_getSplitIslands(_native); }
			set { btSimulationIslandManager_setSplitIslands(_native, value); }
		}

		public UnionFind UnionFind
		{
			get { return new UnionFind(btSimulationIslandManager_getUnionFind(_native)); }
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (_native != IntPtr.Zero)
			{
				if (!_preventDelete)
				{
					btSimulationIslandManager_delete(_native);
				}
				_native = IntPtr.Zero;
			}
		}

		~SimulationIslandManager()
		{
			Dispose(false);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSimulationIslandManager_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSimulationIslandManager_buildAndProcessIslands(IntPtr obj, IntPtr dispatcher, IntPtr collisionWorld, IntPtr callback);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSimulationIslandManager_buildIslands(IntPtr obj, IntPtr dispatcher, IntPtr colWorld);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSimulationIslandManager_findUnions(IntPtr obj, IntPtr dispatcher, IntPtr colWorld);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btSimulationIslandManager_getSplitIslands(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSimulationIslandManager_getUnionFind(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSimulationIslandManager_initUnionFind(IntPtr obj, int n);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSimulationIslandManager_setSplitIslands(IntPtr obj, bool doSplitIslands);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSimulationIslandManager_storeIslandActivationState(IntPtr obj, IntPtr world);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSimulationIslandManager_updateActivationState(IntPtr obj, IntPtr colWorld, IntPtr dispatcher);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSimulationIslandManager_delete(IntPtr obj);
	}
}
