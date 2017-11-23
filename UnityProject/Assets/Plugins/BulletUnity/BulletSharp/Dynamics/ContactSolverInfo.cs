using System;


namespace BulletSharp
{
	[Flags]
	public enum SolverModes
	{
		None = 0,
		RandomizeOrder = 1,
		FrictionSeparate = 2,
		UseWarmStarting = 4,
		Use2FrictionDirections = 16,
		EnableFrictionDirectionCaching = 32,
		DisableVelocityDependentFrictionDirection = 64,
		CacheFriendly = 128,
		Simd = 256,
		InterleaveContactAndFrictionConstraints = 512,
		AllowZeroLengthFrictionDirections = 1024
	}

	public class ContactSolverInfoData : IDisposable
	{
		internal IntPtr Native;
		private bool _preventDelete;

		internal ContactSolverInfoData(IntPtr native, bool preventDelete)
		{
			Native = native;
			_preventDelete = preventDelete;
		}

		public ContactSolverInfoData()
		{
			Native = UnsafeNativeMethods.btContactSolverInfoData_new();
		}

		public float Damping
		{
			get => UnsafeNativeMethods.btContactSolverInfoData_getDamping(Native);
			set => UnsafeNativeMethods.btContactSolverInfoData_setDamping(Native, value);
		}

		public float Erp
		{
			get => UnsafeNativeMethods.btContactSolverInfoData_getErp(Native);
			set => UnsafeNativeMethods.btContactSolverInfoData_setErp(Native, value);
		}

		public float Erp2
		{
			get => UnsafeNativeMethods.btContactSolverInfoData_getErp2(Native);
			set => UnsafeNativeMethods.btContactSolverInfoData_setErp2(Native, value);
		}

		public float Friction
		{
			get => UnsafeNativeMethods.btContactSolverInfoData_getFriction(Native);
			set => UnsafeNativeMethods.btContactSolverInfoData_setFriction(Native, value);
		}

		public float FrictionCfm
		{
			get => UnsafeNativeMethods.btContactSolverInfoData_getFrictionCfm(Native);
			set => UnsafeNativeMethods.btContactSolverInfoData_setFrictionCfm(Native, value);
		}

		public float FrictionErp
		{
			get => UnsafeNativeMethods.btContactSolverInfoData_getFrictionErp(Native);
			set => UnsafeNativeMethods.btContactSolverInfoData_setFrictionErp(Native, value);
		}

		public float GlobalCfm
		{
			get => UnsafeNativeMethods.btContactSolverInfoData_getGlobalCfm(Native);
			set => UnsafeNativeMethods.btContactSolverInfoData_setGlobalCfm(Native, value);
		}

		public float LinearSlop
		{
			get => UnsafeNativeMethods.btContactSolverInfoData_getLinearSlop(Native);
			set => UnsafeNativeMethods.btContactSolverInfoData_setLinearSlop(Native, value);
		}

		public float MaxErrorReduction
		{
			get => UnsafeNativeMethods.btContactSolverInfoData_getMaxErrorReduction(Native);
			set => UnsafeNativeMethods.btContactSolverInfoData_setMaxErrorReduction(Native, value);
		}

		public float MaxGyroscopicForce
		{
			get => UnsafeNativeMethods.btContactSolverInfoData_getMaxGyroscopicForce(Native);
			set => UnsafeNativeMethods.btContactSolverInfoData_setMaxGyroscopicForce(Native, value);
		}

		public int MinimumSolverBatchSize
		{
			get => UnsafeNativeMethods.btContactSolverInfoData_getMinimumSolverBatchSize(Native);
			set => UnsafeNativeMethods.btContactSolverInfoData_setMinimumSolverBatchSize(Native, value);
		}

		public int NumIterations
		{
			get => UnsafeNativeMethods.btContactSolverInfoData_getNumIterations(Native);
			set => UnsafeNativeMethods.btContactSolverInfoData_setNumIterations(Native, value);
		}

		public int RestingContactRestitutionThreshold
		{
			get => UnsafeNativeMethods.btContactSolverInfoData_getRestingContactRestitutionThreshold(Native);
			set => UnsafeNativeMethods.btContactSolverInfoData_setRestingContactRestitutionThreshold(Native, value);
		}

		public float Restitution
		{
			get => UnsafeNativeMethods.btContactSolverInfoData_getRestitution(Native);
			set => UnsafeNativeMethods.btContactSolverInfoData_setRestitution(Native, value);
		}

		public float SingleAxisRollingFrictionThreshold
		{
			get => UnsafeNativeMethods.btContactSolverInfoData_getSingleAxisRollingFrictionThreshold(Native);
			set => UnsafeNativeMethods.btContactSolverInfoData_setSingleAxisRollingFrictionThreshold(Native, value);
		}

		public SolverModes SolverMode
		{
			get => UnsafeNativeMethods.btContactSolverInfoData_getSolverMode(Native);
			set => UnsafeNativeMethods.btContactSolverInfoData_setSolverMode(Native, value);
		}

		public float Sor
		{
			get => UnsafeNativeMethods.btContactSolverInfoData_getSor(Native);
			set => UnsafeNativeMethods.btContactSolverInfoData_setSor(Native, value);
		}

		public int SplitImpulse
		{
			get => UnsafeNativeMethods.btContactSolverInfoData_getSplitImpulse(Native);
			set => UnsafeNativeMethods.btContactSolverInfoData_setSplitImpulse(Native, value);
		}

		public float SplitImpulsePenetrationThreshold
		{
			get => UnsafeNativeMethods.btContactSolverInfoData_getSplitImpulsePenetrationThreshold(Native);
			set => UnsafeNativeMethods.btContactSolverInfoData_setSplitImpulsePenetrationThreshold(Native, value);
		}

		public float SplitImpulseTurnErp
		{
			get => UnsafeNativeMethods.btContactSolverInfoData_getSplitImpulseTurnErp(Native);
			set => UnsafeNativeMethods.btContactSolverInfoData_setSplitImpulseTurnErp(Native, value);
		}

		public float Tau
		{
			get => UnsafeNativeMethods.btContactSolverInfoData_getTau(Native);
			set => UnsafeNativeMethods.btContactSolverInfoData_setTau(Native, value);
		}

		public float TimeStep
		{
			get => UnsafeNativeMethods.btContactSolverInfoData_getTimeStep(Native);
			set => UnsafeNativeMethods.btContactSolverInfoData_setTimeStep(Native, value);
		}

		public float WarmStartingFactor
		{
			get => UnsafeNativeMethods.btContactSolverInfoData_getWarmstartingFactor(Native);
			set => UnsafeNativeMethods.btContactSolverInfoData_setWarmstartingFactor(Native, value);
		}

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
					UnsafeNativeMethods.btContactSolverInfoData_delete(Native);
				}
				Native = IntPtr.Zero;
			}
		}

		~ContactSolverInfoData()
		{
			Dispose(false);
		}
	}

	public class ContactSolverInfo : ContactSolverInfoData
	{
		internal ContactSolverInfo(IntPtr native, bool preventDelete)
			: base(native, preventDelete)
		{
		}

		public ContactSolverInfo()
			: base(UnsafeNativeMethods.btContactSolverInfo_new(), false)
		{
		}
	}
}
