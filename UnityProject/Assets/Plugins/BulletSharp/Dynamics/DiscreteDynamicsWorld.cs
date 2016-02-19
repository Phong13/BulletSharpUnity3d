using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
	public class DiscreteDynamicsWorld : DynamicsWorld
	{
		private SimulationIslandManager _simulationIslandManager;

		internal DiscreteDynamicsWorld(IntPtr native)
			: base(native)
		{
		}

		public DiscreteDynamicsWorld(Dispatcher dispatcher, BroadphaseInterface pairCache, ConstraintSolver constraintSolver, CollisionConfiguration collisionConfiguration)
            : base(btDiscreteDynamicsWorld_new(
            dispatcher != null ? dispatcher._native : IntPtr.Zero,
            pairCache != null ? pairCache._native : IntPtr.Zero,
            constraintSolver != null ? constraintSolver._native : IntPtr.Zero,
            collisionConfiguration != null ? collisionConfiguration._native : IntPtr.Zero))
		{
			_constraintSolver = constraintSolver;
            Dispatcher = dispatcher;
            Broadphase = pairCache;
		}

		public void ApplyGravity()
		{
			btDiscreteDynamicsWorld_applyGravity(_native);
		}

		public void DebugDrawConstraint(TypedConstraint constraint)
		{
			btDiscreteDynamicsWorld_debugDrawConstraint(_native, constraint._native);
		}

        private unsafe void SerializeDynamicsWorldInfo(Serializer serializer)
        {
            int len = 88;
            Chunk chunk = serializer.Allocate((uint)len, 1);

            using (UnmanagedMemoryStream stream = new UnmanagedMemoryStream((byte*)chunk.OldPtr.ToPointer(), len, len, FileAccess.Write))
            {
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    ContactSolverInfo solverInfo = SolverInfo;
                    writer.Write(solverInfo.Tau);
                    writer.Write(solverInfo.Damping);
                    writer.Write(solverInfo.Friction);
                    writer.Write(solverInfo.TimeStep);

                    writer.Write(solverInfo.Restitution);
                    writer.Write(solverInfo.MaxErrorReduction);
                    writer.Write(solverInfo.Sor);
                    writer.Write(solverInfo.Erp);

                    writer.Write(solverInfo.Erp2);
                    writer.Write(solverInfo.GlobalCfm);
                    writer.Write(solverInfo.SplitImpulsePenetrationThreshold);
                    writer.Write(solverInfo.SplitImpulseTurnErp);

                    writer.Write(solverInfo.LinearSlop);
                    writer.Write(solverInfo.WarmStartingFactor);
                    writer.Write(solverInfo.MaxGyroscopicForce);
                    writer.Write(solverInfo.SingleAxisRollingFrictionThreshold);

                    writer.Write(solverInfo.NumIterations);
                    writer.Write((int)solverInfo.SolverMode);
                    writer.Write(solverInfo.RestingContactRestitutionThreshold);
                    writer.Write(solverInfo.MinimumSolverBatchSize);

                    writer.Write(solverInfo.SplitImpulse);
                    writer.Write((int)0); // padding
                }
            }

            serializer.FinalizeChunk(chunk, "btDynamicsWorldFloatData", DnaID.DynamicsWorld, chunk.OldPtr);
        }

        void SerializeRigidBodies(Serializer serializer)
        {
            foreach (CollisionObject colObj in CollisionObjectArray)
            {
                if (colObj.InternalType == CollisionObjectTypes.RigidBody)
                {
                    int len = colObj.CalculateSerializeBufferSize();
                    Chunk chunk = serializer.Allocate((uint)len, 1);
                    string structType = colObj.Serialize(chunk.OldPtr, serializer);
                    serializer.FinalizeChunk(chunk, structType, DnaID.RigidBody, colObj._native);
                }
            }

            for (int i = 0; i < NumConstraints; i++)
            {
                TypedConstraint constraint = GetConstraint(i);
                int len = constraint.CalculateSerializeBufferSize();
                Chunk chunk = serializer.Allocate((uint)len, 1);
                string structType = constraint.Serialize(chunk.OldPtr, serializer);
                serializer.FinalizeChunk(chunk, structType, DnaID.Constraint, constraint._native);
            }
        }

        public override void Serialize(Serializer serializer)
        {
            serializer.StartSerialization();
            SerializeDynamicsWorldInfo(serializer);
            SerializeCollisionObjects(serializer);
            SerializeRigidBodies(serializer);
            serializer.FinishSerialization();
        }

		public void SetNumTasks(int numTasks)
		{
			btDiscreteDynamicsWorld_setNumTasks(_native, numTasks);
		}

		public void SynchronizeSingleMotionState(RigidBody body)
		{
			btDiscreteDynamicsWorld_synchronizeSingleMotionState(_native, body._native);
		}

		public void UpdateVehicles(float timeStep)
		{
			btDiscreteDynamicsWorld_updateVehicles(_native, timeStep);
		}

		public bool ApplySpeculativeContactRestitution
		{
			get { return btDiscreteDynamicsWorld_getApplySpeculativeContactRestitution(_native); }
			set { btDiscreteDynamicsWorld_setApplySpeculativeContactRestitution(_native, value); }
		}
        /*
		public CollisionWorld CollisionWorld
		{
			get { return btDiscreteDynamicsWorld_getCollisionWorld(_native); }
		}
        */
		public bool LatencyMotionStateInterpolation
		{
			get { return btDiscreteDynamicsWorld_getLatencyMotionStateInterpolation(_native); }
			set { btDiscreteDynamicsWorld_setLatencyMotionStateInterpolation(_native, value); }
		}

		public SimulationIslandManager SimulationIslandManager
		{
			get
			{
				if (_simulationIslandManager == null)
				{
					_simulationIslandManager = new SimulationIslandManager(btDiscreteDynamicsWorld_getSimulationIslandManager(_native), true);
				}
				return _simulationIslandManager;
			}
		}

		public bool SynchronizeAllMotionStates
		{
			get { return btDiscreteDynamicsWorld_getSynchronizeAllMotionStates(_native); }
			set { btDiscreteDynamicsWorld_setSynchronizeAllMotionStates(_native, value); }
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btDiscreteDynamicsWorld_new(IntPtr dispatcher, IntPtr pairCache, IntPtr constraintSolver, IntPtr collisionConfiguration);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDiscreteDynamicsWorld_applyGravity(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDiscreteDynamicsWorld_debugDrawConstraint(IntPtr obj, IntPtr constraint);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btDiscreteDynamicsWorld_getApplySpeculativeContactRestitution(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btDiscreteDynamicsWorld_getCollisionWorld(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btDiscreteDynamicsWorld_getLatencyMotionStateInterpolation(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btDiscreteDynamicsWorld_getSimulationIslandManager(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btDiscreteDynamicsWorld_getSynchronizeAllMotionStates(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDiscreteDynamicsWorld_setApplySpeculativeContactRestitution(IntPtr obj, bool enable);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDiscreteDynamicsWorld_setLatencyMotionStateInterpolation(IntPtr obj, bool latencyInterpolation);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDiscreteDynamicsWorld_setNumTasks(IntPtr obj, int numTasks);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDiscreteDynamicsWorld_setSynchronizeAllMotionStates(IntPtr obj, bool synchronizeAll);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDiscreteDynamicsWorld_synchronizeSingleMotionState(IntPtr obj, IntPtr body);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDiscreteDynamicsWorld_updateVehicles(IntPtr obj, float timeStep);
	}
}
