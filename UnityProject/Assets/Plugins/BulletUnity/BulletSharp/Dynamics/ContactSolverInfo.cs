using System;
using static BulletSharp.UnsafeNativeMethods;

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
			Native = btContactSolverInfoData_new();
		}

		public float Damping
		{
			get => btContactSolverInfoData_getDamping(Native);
			set => btContactSolverInfoData_setDamping(Native, value);
		}

		public float Erp
		{
			get => btContactSolverInfoData_getErp(Native);
			set => btContactSolverInfoData_setErp(Native, value);
		}

		public float Erp2
		{
			get => btContactSolverInfoData_getErp2(Native);
			set => btContactSolverInfoData_setErp2(Native, value);
		}

		public float Friction
		{
			get => btContactSolverInfoData_getFriction(Native);
			set => btContactSolverInfoData_setFriction(Native, value);
		}

		public float FrictionCfm
		{
			get => btContactSolverInfoData_getFrictionCfm(Native);
			set => btContactSolverInfoData_setFrictionCfm(Native, value);
		}

		public float FrictionErp
		{
			get => btContactSolverInfoData_getFrictionErp(Native);
			set => btContactSolverInfoData_setFrictionErp(Native, value);
		}

		public float GlobalCfm
		{
			get => btContactSolverInfoData_getGlobalCfm(Native);
			set => btContactSolverInfoData_setGlobalCfm(Native, value);
		}

		public float LinearSlop
		{
			get => btContactSolverInfoData_getLinearSlop(Native);
			set => btContactSolverInfoData_setLinearSlop(Native, value);
		}

		public float MaxErrorReduction
		{
			get => btContactSolverInfoData_getMaxErrorReduction(Native);
			set => btContactSolverInfoData_setMaxErrorReduction(Native, value);
		}

		public float MaxGyroscopicForce
		{
			get => btContactSolverInfoData_getMaxGyroscopicForce(Native);
			set => btContactSolverInfoData_setMaxGyroscopicForce(Native, value);
		}

		public int MinimumSolverBatchSize
		{
			get => btContactSolverInfoData_getMinimumSolverBatchSize(Native);
			set => btContactSolverInfoData_setMinimumSolverBatchSize(Native, value);
		}

		public int NumIterations
		{
			get => btContactSolverInfoData_getNumIterations(Native);
			set => btContactSolverInfoData_setNumIterations(Native, value);
		}

		public int RestingContactRestitutionThreshold
		{
			get => btContactSolverInfoData_getRestingContactRestitutionThreshold(Native);
			set => btContactSolverInfoData_setRestingContactRestitutionThreshold(Native, value);
		}

		public float Restitution
		{
			get => btContactSolverInfoData_getRestitution(Native);
			set => btContactSolverInfoData_setRestitution(Native, value);
		}

		public float SingleAxisRollingFrictionThreshold
		{
			get => btContactSolverInfoData_getSingleAxisRollingFrictionThreshold(Native);
			set => btContactSolverInfoData_setSingleAxisRollingFrictionThreshold(Native, value);
		}

		public SolverModes SolverMode
		{
			get => btContactSolverInfoData_getSolverMode(Native);
			set => btContactSolverInfoData_setSolverMode(Native, value);
		}

		public float Sor
		{
			get => btContactSolverInfoData_getSor(Native);
			set => btContactSolverInfoData_setSor(Native, value);
		}

		public int SplitImpulse
		{
			get => btContactSolverInfoData_getSplitImpulse(Native);
			set => btContactSolverInfoData_setSplitImpulse(Native, value);
		}

		public float SplitImpulsePenetrationThreshold
		{
			get => btContactSolverInfoData_getSplitImpulsePenetrationThreshold(Native);
			set => btContactSolverInfoData_setSplitImpulsePenetrationThreshold(Native, value);
		}

		public float SplitImpulseTurnErp
		{
			get => btContactSolverInfoData_getSplitImpulseTurnErp(Native);
			set => btContactSolverInfoData_setSplitImpulseTurnErp(Native, value);
		}

		public float Tau
		{
			get => btContactSolverInfoData_getTau(Native);
			set => btContactSolverInfoData_setTau(Native, value);
		}

		public float TimeStep
		{
			get => btContactSolverInfoData_getTimeStep(Native);
			set => btContactSolverInfoData_setTimeStep(Native, value);
		}

		public float WarmStartingFactor
		{
			get => btContactSolverInfoData_getWarmstartingFactor(Native);
			set => btContactSolverInfoData_setWarmstartingFactor(Native, value);
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
					btContactSolverInfoData_delete(Native);
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
			: base(btContactSolverInfo_new(), false)
		{
		}
	}
}
