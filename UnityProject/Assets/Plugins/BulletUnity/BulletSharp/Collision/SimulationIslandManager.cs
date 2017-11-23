using System;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class SimulationIslandManager : IDisposable
	{
		public abstract class IslandCallback : IDisposable
		{
			internal IntPtr Native;

			internal IslandCallback(IntPtr native)
			{
				Native = native;
			}
			/*
			public void ProcessIsland(CollisionObject bodies, int numBodies, PersistentManifold manifolds,
				int numManifolds, int islandId)
			{
				btSimulationIslandManager_IslandCallback_processIsland(Native, bodies.Native,
					numBodies, manifolds.Native, numManifolds, islandId);
			}
			*/
			public void Dispose()
			{
				Dispose(true);
				GC.SuppressFinalize(this);
			}

			protected virtual void Dispose(bool disposing)
			{
				if (Native != IntPtr.Zero)
				{
					btSimulationIslandManager_IslandCallback_delete(Native);
					Native = IntPtr.Zero;
				}
			}

			~IslandCallback()
			{
				Dispose(false);
			}
		}

		internal IntPtr Native;
		bool _preventDelete;

		internal SimulationIslandManager(IntPtr native, bool preventDelete)
		{
			Native = native;
			_preventDelete = preventDelete;
		}

		public SimulationIslandManager()
		{
			Native = btSimulationIslandManager_new();
		}

		public void BuildAndProcessIslands(Dispatcher dispatcher, CollisionWorld collisionWorld,
			IslandCallback callback)
		{
			btSimulationIslandManager_buildAndProcessIslands(Native, dispatcher.Native,
				collisionWorld.Native, callback.Native);
		}

		public void BuildIslands(Dispatcher dispatcher, CollisionWorld colWorld)
		{
			btSimulationIslandManager_buildIslands(Native, dispatcher.Native, colWorld.Native);
		}

		public void FindUnions(Dispatcher dispatcher, CollisionWorld colWorld)
		{
			btSimulationIslandManager_findUnions(Native, dispatcher.Native, colWorld.Native);
		}

		public void InitUnionFind(int n)
		{
			btSimulationIslandManager_initUnionFind(Native, n);
		}

		public void StoreIslandActivationState(CollisionWorld world)
		{
			btSimulationIslandManager_storeIslandActivationState(Native, world.Native);
		}

		public void UpdateActivationState(CollisionWorld colWorld, Dispatcher dispatcher)
		{
			btSimulationIslandManager_updateActivationState(Native, colWorld.Native,
				dispatcher.Native);
		}

		public bool SplitIslands
		{
			get => btSimulationIslandManager_getSplitIslands(Native);
			set => btSimulationIslandManager_setSplitIslands(Native, value);
		}

		public UnionFind UnionFind => new UnionFind(btSimulationIslandManager_getUnionFind(Native));

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (Native != IntPtr.Zero)
			{
				if (!_preventDelete)
				{
					btSimulationIslandManager_delete(Native);
				}
				Native = IntPtr.Zero;
			}
		}

		~SimulationIslandManager()
		{
			Dispose(false);
		}
	}
}
