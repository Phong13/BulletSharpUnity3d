using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.Math;
using AOT;

namespace BulletSharp.SoftBody
{
	public class SoftBodyCollisionShape : ConvexShape
	{
		internal SoftBodyCollisionShape(IntPtr native)
			: base(native, true)
		{
		}
	}

	public enum AeroModel
	{
        VertexPoint,
        VertexTwoSided,
        VertexTwoSidedLiftDrag,
        VertexOneSided,
        FaceTwoSided,
        FaceTwoSidedLiftDrag,
        FaceOneSided
	}

    [Flags]
	public enum CollisionFlags
	{
        None = 0,
        RigidSoftMask = 0x000f,
        SdfRigidSoft = 0x0001,
        ClusterConvexRigidSoft = 0x0002,
        SoftSoftMask = 0x0030,
        VertexFaceSoftSoft = 0x0010,
        ClusterClusterSoftSoft = 0x0020,
        ClusterSelf = 0x0040,
        Default = SdfRigidSoft
	}

	public enum FeatureType
	{
		None,
		Node,
		Link,
		Face,
		Tetra
	}

	public enum JointType
	{
		Linear,
		Angular,
		Contact
	}

    [Flags]
	public enum MaterialFlags
	{
        None = 0,
		DebugDraw = 0x0001,
        Default = DebugDraw
	}

	public enum PositionSolver
	{
		Linear,
		Anchors,
        RigidContacts,
        SoftContacts
	}

	public enum SolverPresets
	{
		Positions,
		Velocities,
		Default
	}

	public enum VelocitySolver
	{
		Linear
	}

	public class SoftBodyWorldInfo : IDisposable
	{
		internal IntPtr _native;
        private bool _preventDelete;

		private BroadphaseInterface _broadphase;
		private Dispatcher _dispatcher;
        private SparseSdf _sparseSdf;

        internal SoftBodyWorldInfo(IntPtr native, bool preventDelete)
		{
			_native = native;
            _preventDelete = preventDelete;
		}

		public SoftBodyWorldInfo()
		{
			_native = btSoftBodyWorldInfo_new();
		}

		public float AirDensity
		{
			get { return btSoftBodyWorldInfo_getAir_density(_native); }
			set { btSoftBodyWorldInfo_setAir_density(_native, value); }
		}

		public BroadphaseInterface Broadphase
		{
			get { return _broadphase; }
			set
			{
				btSoftBodyWorldInfo_setBroadphase(_native, (value != null) ? value._native : IntPtr.Zero);
				_broadphase = value;
			}
		}

		public Dispatcher Dispatcher
		{
			get { return _dispatcher; }
			set
			{
				btSoftBodyWorldInfo_setDispatcher(_native, (value != null) ? value._native : IntPtr.Zero);
				_dispatcher = value;
			}
		}

		public Vector3 Gravity
		{
			get
			{
				Vector3 value;
				btSoftBodyWorldInfo_getGravity(_native, out value);
				return value;
			}
			set { btSoftBodyWorldInfo_setGravity(_native, ref value); }
		}

		public float MaxDisplacement
		{
			get { return btSoftBodyWorldInfo_getMaxDisplacement(_native); }
			set { btSoftBodyWorldInfo_setMaxDisplacement(_native, value); }
		}

        public SparseSdf SparseSdf
		{
            get
            {
                if (_sparseSdf == null)
                {
                    _sparseSdf = new SparseSdf(btSoftBodyWorldInfo_getSparsesdf(_native));
                }
                return _sparseSdf;
            }
		}

		public float WaterDensity
		{
			get { return btSoftBodyWorldInfo_getWater_density(_native); }
			set { btSoftBodyWorldInfo_setWater_density(_native, value); }
		}

		public Vector3 WaterNormal
		{
			get
			{
				Vector3 value;
				btSoftBodyWorldInfo_getWater_normal(_native, out value);
				return value;
			}
			set { btSoftBodyWorldInfo_setWater_normal(_native, ref value); }
		}

		public float WaterOffset
		{
			get { return btSoftBodyWorldInfo_getWater_offset(_native); }
			set { btSoftBodyWorldInfo_setWater_offset(_native, value); }
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
                    btSoftBodyWorldInfo_delete(_native);
                }
				_native = IntPtr.Zero;
			}
		}

		~SoftBodyWorldInfo()
		{
			Dispose(false);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBodyWorldInfo_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btSoftBodyWorldInfo_getAir_density(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBodyWorldInfo_getGravity(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btSoftBodyWorldInfo_getMaxDisplacement(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBodyWorldInfo_getSparsesdf(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btSoftBodyWorldInfo_getWater_density(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBodyWorldInfo_getWater_normal(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btSoftBodyWorldInfo_getWater_offset(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBodyWorldInfo_setAir_density(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBodyWorldInfo_setBroadphase(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBodyWorldInfo_setDispatcher(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBodyWorldInfo_setGravity(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBodyWorldInfo_setMaxDisplacement(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBodyWorldInfo_setWater_density(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBodyWorldInfo_setWater_normal(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBodyWorldInfo_setWater_offset(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBodyWorldInfo_delete(IntPtr obj);
	}

	public class AngularJoint : Joint
	{
		public class IControl : IDisposable
		{
			internal IntPtr _native;
            private bool _preventDelete;

            [UnmanagedFunctionPointer(Native.Conv), SuppressUnmanagedCodeSecurity]
            delegate void PrepareUnmanagedDelegate(IntPtr thisPtr, IntPtr aJoint);
            [UnmanagedFunctionPointer(Native.Conv), SuppressUnmanagedCodeSecurity]
            delegate float SpeedUnmanagedDelegate(IntPtr thisPtr, IntPtr aJoint, float current);

            PrepareUnmanagedDelegate _prepare;
            SpeedUnmanagedDelegate _speed;

            internal static IControl GetManaged(IntPtr native)
            {
                if (native == IntPtr.Zero)
                {
                    return null;
                }

                if (native == Default._native)
                {
                    return Default;
                }

                IntPtr handle = btSoftBody_AJoint_IControlWrapper_getWrapperData(native);
                return GCHandle.FromIntPtr(handle).Target as IControl;
            }

            internal IControl(IntPtr native, bool preventDelete)
			{
				_native = native;
                _preventDelete = preventDelete;
			}

			public IControl()
			{
                _prepare = PrepareUnmanaged;
                _speed = SpeedUnmanaged;

                _native = btSoftBody_AJoint_IControlWrapper_new(
                    Marshal.GetFunctionPointerForDelegate(_prepare),
                    Marshal.GetFunctionPointerForDelegate(_speed)
				);
                GCHandle handle = GCHandle.Alloc(this, GCHandleType.Normal);
                btSoftBody_AJoint_IControlWrapper_setWrapperData(_native, GCHandle.ToIntPtr(handle));
			}

            static IControl _default;
			public static IControl Default
			{
                get
                {
                    if (_default == null)
                    {
                        _default = new IControl(btSoftBody_AJoint_IControl_Default(), true);
                    }
                    return _default;
                }
			}

			[MonoPInvokeCallback(typeof(PrepareUnmanagedDelegate))]
            static private void PrepareUnmanaged(IntPtr thisPtr,IntPtr aJoint)
            {
				IControl ms = GCHandle.FromIntPtr(thisPtr).Target as IControl;
                ms.Prepare(new AngularJoint(aJoint));
            }

			[MonoPInvokeCallback(typeof(SpeedUnmanagedDelegate))]
            static public float SpeedUnmanaged(IntPtr thisPtr, IntPtr aJoint, float current)
            {
				IControl ms = GCHandle.FromIntPtr(thisPtr).Target as IControl;
                return ms.Speed(new AngularJoint(aJoint), current);
            }

            public virtual void Prepare(AngularJoint angularJoint)
            {
            }

            public virtual float Speed(AngularJoint angularJoint, float current)
            {
                return current;
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
                        IntPtr handle = btSoftBody_AJoint_IControlWrapper_getWrapperData(_native);
                        GCHandle.FromIntPtr(handle).Free();
                        btSoftBody_AJoint_IControl_delete(_native);
                    }
					_native = IntPtr.Zero;
				}
			}

			~IControl()
			{
				Dispose(false);
			}

            [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
            static extern IntPtr btSoftBody_AJoint_IControlWrapper_new(IntPtr prepareCallback, IntPtr speedCallback);
            [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
            static extern IntPtr btSoftBody_AJoint_IControlWrapper_getWrapperData(IntPtr obj);
            [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
            static extern void btSoftBody_AJoint_IControlWrapper_setWrapperData(IntPtr obj, IntPtr data);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern IntPtr btSoftBody_AJoint_IControl_Default();
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btSoftBody_AJoint_IControl_Prepare(IntPtr obj, IntPtr __unnamed0);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern float btSoftBody_AJoint_IControl_Speed(IntPtr obj, IntPtr __unnamed0, float current);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btSoftBody_AJoint_IControl_delete(IntPtr obj);
		}

		public new class Specs : Joint.Specs
		{
            private IControl _iControl;

			public Specs()
				: base(btSoftBody_AJoint_Specs_new())
			{
			}

			public Vector3 Axis
			{
				get
				{
					Vector3 value;
					btSoftBody_AJoint_Specs_getAxis(_native, out value);
					return value;
				}
				set { btSoftBody_AJoint_Specs_setAxis(_native, ref value); }
			}

			public IControl Control
			{
                get
                {
                    if (_iControl == null)
                    {
                        _iControl = IControl.GetManaged(btSoftBody_AJoint_Specs_getIcontrol(_native));
                    }
                    return _iControl;
                }
                set
                {
                    _iControl = value;
                    btSoftBody_AJoint_Specs_setIcontrol(_native, value._native);
                }
			}

			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern IntPtr btSoftBody_AJoint_Specs_new();
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btSoftBody_AJoint_Specs_getAxis(IntPtr obj, [Out] out Vector3 value);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern IntPtr btSoftBody_AJoint_Specs_getIcontrol(IntPtr obj);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btSoftBody_AJoint_Specs_setAxis(IntPtr obj, [In] ref Vector3 value);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btSoftBody_AJoint_Specs_setIcontrol(IntPtr obj, IntPtr value);
		}

        private IControl _iControl;
        private Vector3Array _axis;

		internal AngularJoint(IntPtr native)
			: base(native)
		{
		}

		public Vector3Array Axis
		{
			get
			{
                if (_axis == null)
                {
                    _axis = new Vector3Array(btSoftBody_AJoint_getAxis(_native), 2);
                }
                return _axis;
			}
		}

		public IControl Control
		{
            get
            {
                if (_iControl == null)
                {
                    _iControl = IControl.GetManaged(btSoftBody_AJoint_getIcontrol(_native));
                }
                return _iControl;
            }
            set
            {
                _iControl = value;
                btSoftBody_AJoint_setIcontrol(_native, value._native);
            }
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBody_AJoint_getAxis(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBody_AJoint_getIcontrol(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_AJoint_setIcontrol(IntPtr obj, IntPtr value);
	}

	public class Anchor
	{
		internal IntPtr _native;

		private Node _node;

		internal Anchor(IntPtr native)
		{
			_native = native;
		}

		public RigidBody Body
		{
            get { return CollisionObject.GetManaged(btSoftBody_Anchor_getBody(_native)) as RigidBody; }
            set { btSoftBody_Anchor_setBody(_native, (value != null) ? value._native : IntPtr.Zero); }
		}

		public Matrix C0
		{
			get
			{
				Matrix value;
				btSoftBody_Anchor_getC0(_native, out value);
				return value;
			}
			set { btSoftBody_Anchor_setC0(_native, ref value); }
		}

		public Vector3 C1
		{
			get
			{
				Vector3 value;
				btSoftBody_Anchor_getC1(_native, out value);
				return value;
			}
			set { btSoftBody_Anchor_setC1(_native, ref value); }
		}

		public float C2
		{
			get { return btSoftBody_Anchor_getC2(_native); }
			set { btSoftBody_Anchor_setC2(_native, value); }
		}

		public float Influence
		{
			get { return btSoftBody_Anchor_getInfluence(_native); }
			set { btSoftBody_Anchor_setInfluence(_native, value); }
		}

		public Vector3 Local
		{
			get
			{
				Vector3 value;
				btSoftBody_Anchor_getLocal(_native, out value);
				return value;
			}
			set { btSoftBody_Anchor_setLocal(_native, ref value); }
		}

		public Node Node
		{
            get
            {
                IntPtr nodePtr = btSoftBody_Anchor_getNode(_native);
                if (_node != null && _node._native == nodePtr) return _node;
                if (nodePtr == IntPtr.Zero) return null;
                _node = new Node(nodePtr);
                return _node;
            }
			set
			{
                btSoftBody_Anchor_setNode(_native, (value != null) ? value._native : IntPtr.Zero);
				_node = value;
			}
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBody_Anchor_getBody(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Anchor_getC0(IntPtr obj, [Out] out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Anchor_getC1(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btSoftBody_Anchor_getC2(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btSoftBody_Anchor_getInfluence(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Anchor_getLocal(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBody_Anchor_getNode(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Anchor_setBody(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Anchor_setC0(IntPtr obj, [In] ref Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Anchor_setC1(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Anchor_setC2(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Anchor_setInfluence(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Anchor_setLocal(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Anchor_setNode(IntPtr obj, IntPtr value);
	}

	public class Body : IDisposable
	{
		internal IntPtr _native;

		private Cluster _soft;

		internal Body(IntPtr native)
		{
			_native = native;
		}

		public Body()
		{
			_native = btSoftBody_Body_new();
		}

		public Body(CollisionObject colObj)
		{
			_native = btSoftBody_Body_new2(colObj._native);
		}

		public Body(Cluster p)
		{
			_native = btSoftBody_Body_new3(p._native);
		}

		public void Activate()
		{
			btSoftBody_Body_activate(_native);
		}

		public void ApplyAImpulse(Impulse impulse)
		{
			btSoftBody_Body_applyAImpulse(_native, impulse._native);
		}

		public void ApplyDAImpulse(Vector3 impulse)
		{
			btSoftBody_Body_applyDAImpulse(_native, ref impulse);
		}

		public void ApplyDCImpulse(Vector3 impulse)
		{
			btSoftBody_Body_applyDCImpulse(_native, ref impulse);
		}

		public void ApplyDImpulse(Vector3 impulse, Vector3 rpos)
		{
			btSoftBody_Body_applyDImpulse(_native, ref impulse, ref rpos);
		}

		public void ApplyImpulse(Impulse impulse, Vector3 rpos)
		{
			btSoftBody_Body_applyImpulse(_native, impulse._native, ref rpos);
		}

		public void ApplyVAImpulse(Vector3 impulse)
		{
			btSoftBody_Body_applyVAImpulse(_native, ref impulse);
		}

		public void ApplyVImpulse(Vector3 impulse, Vector3 rpos)
		{
			btSoftBody_Body_applyVImpulse(_native, ref impulse, ref rpos);
		}

        public Vector3 GetAngularVelocity(Vector3 rpos)
        {
            Vector3 value;
            btSoftBody_Body_angularVelocity(_native, ref rpos, out value);
            return value;
        }

		public Vector3 Velocity(Vector3 rpos)
		{
			Vector3 value;
			btSoftBody_Body_velocity(_native, ref rpos, out value);
			return value;
		}

		public Vector3 AngularVelocity
		{
			get
			{
				Vector3 value;
				btSoftBody_Body_angularVelocity2(_native, out value);
				return value;
			}
		}

		public CollisionObject CollisionObject
		{
            get { return CollisionObject.GetManaged(btSoftBody_Body_getCollisionObject(_native)); }
			set { btSoftBody_Body_setCollisionObject(_native, value._native); }
		}

		public float InverseMass
		{
			get { return btSoftBody_Body_invMass(_native); }
		}

		public Matrix InverseWorldInertia
		{
			get
			{
				Matrix value;
				btSoftBody_Body_invWorldInertia(_native, out value);
				return value;
			}
		}

		public Vector3 LinearVelocity
		{
			get
			{
				Vector3 value;
				btSoftBody_Body_linearVelocity(_native, out value);
				return value;
			}
		}

		public RigidBody Rigid
		{
            get { return CollisionObject.GetManaged(btSoftBody_Body_getRigid(_native)) as RigidBody; }
			set { btSoftBody_Body_setRigid(_native, value._native); }
		}

		public Cluster Soft
		{
            get
            {
                IntPtr softPtr = btSoftBody_Body_getSoft(_native);
                if (_soft != null && _soft._native == softPtr) return _soft;
                if (softPtr == IntPtr.Zero) return null;
                _soft = new Cluster(softPtr);
                return _soft;
            }
			set
			{
                btSoftBody_Body_setSoft(_native, (value != null) ? value._native : IntPtr.Zero);
				_soft = value;
			}
		}

		public Matrix Transform
		{
			get
			{
				Matrix value;
				btSoftBody_Body_xform(_native, out value);
				return value;
			}
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
				btSoftBody_Body_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~Body()
		{
			Dispose(false);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBody_Body_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBody_Body_new2(IntPtr colObj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBody_Body_new3(IntPtr p);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Body_activate(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Body_angularVelocity(IntPtr obj, [In] ref Vector3 rpos, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Body_angularVelocity2(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Body_applyAImpulse(IntPtr obj, IntPtr impulse);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Body_applyDAImpulse(IntPtr obj, [In] ref Vector3 impulse);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Body_applyDCImpulse(IntPtr obj, [In] ref Vector3 impulse);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Body_applyDImpulse(IntPtr obj, [In] ref Vector3 impulse, [In] ref Vector3 rpos);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Body_applyImpulse(IntPtr obj, IntPtr impulse, [In] ref Vector3 rpos);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Body_applyVAImpulse(IntPtr obj, [In] ref Vector3 impulse);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Body_applyVImpulse(IntPtr obj, [In] ref Vector3 impulse, [In] ref Vector3 rpos);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBody_Body_getCollisionObject(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBody_Body_getRigid(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBody_Body_getSoft(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btSoftBody_Body_invMass(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Body_invWorldInertia(IntPtr obj, [Out] out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Body_linearVelocity(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Body_setCollisionObject(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Body_setRigid(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Body_setSoft(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Body_velocity(IntPtr obj, [In] ref Vector3 rpos, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Body_xform(IntPtr obj, [Out] out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Body_delete(IntPtr obj);
	}

	public class ContactJoint : Joint
	{
        private Vector3Array _rPos;

		internal ContactJoint(IntPtr native)
			: base(native)
		{
		}

		public float Friction
		{
			get { return btSoftBody_CJoint_getFriction(_native); }
			set { btSoftBody_CJoint_setFriction(_native, value); }
		}

		public int Life
		{
			get { return btSoftBody_CJoint_getLife(_native); }
			set { btSoftBody_CJoint_setLife(_native, value); }
		}

		public int MaxLife
		{
			get { return btSoftBody_CJoint_getMaxlife(_native); }
			set { btSoftBody_CJoint_setMaxlife(_native, value); }
		}

		public Vector3 Normal
		{
			get
			{
				Vector3 value;
				btSoftBody_CJoint_getNormal(_native, out value);
				return value;
			}
			set { btSoftBody_CJoint_setNormal(_native, ref value); }
		}

		public Vector3Array RPosition
		{
			get
			{
                if (_rPos == null)
                {
                    _rPos = new Vector3Array(btSoftBody_CJoint_getRpos(_native), 2);
                }
                return _rPos;
			}
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btSoftBody_CJoint_getFriction(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btSoftBody_CJoint_getLife(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btSoftBody_CJoint_getMaxlife(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_CJoint_getNormal(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBody_CJoint_getRpos(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_CJoint_setFriction(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_CJoint_setLife(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_CJoint_setMaxlife(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_CJoint_setNormal(IntPtr obj, [In] ref Vector3 value);
	}

	public class Cluster
	{
		internal IntPtr _native;

        private AlignedVector3Array _framerefs;
        private Vector3Array _dImpulses;
		private DbvtNode _leaf;
        //private AlignedScalarArray _masses;
        private AlignedNodeArray _nodes;
        private Vector3Array _vImpulses;

		internal Cluster(IntPtr native)
		{
			_native = native;
		}

        public float AngularDamping
		{
			get { return btSoftBody_Cluster_getAdamping(_native); }
			set { btSoftBody_Cluster_setAdamping(_native, value); }
		}

        public Vector3 AngularVelocity
		{
			get
			{
				Vector3 value;
				btSoftBody_Cluster_getAv(_native, out value);
				return value;
			}
			set { btSoftBody_Cluster_setAv(_native, ref value); }
		}

		public int ClusterIndex
		{
			get { return btSoftBody_Cluster_getClusterIndex(_native); }
			set { btSoftBody_Cluster_setClusterIndex(_native, value); }
		}

		public bool Collide
		{
			get { return btSoftBody_Cluster_getCollide(_native); }
			set { btSoftBody_Cluster_setCollide(_native, value); }
		}

		public Vector3 CenterOfMass
		{
			get
			{
				Vector3 value;
				btSoftBody_Cluster_getCom(_native, out value);
				return value;
			}
			set { btSoftBody_Cluster_setCom(_native, ref value); }
		}

		public bool ContainsAnchor
		{
			get { return btSoftBody_Cluster_getContainsAnchor(_native); }
			set { btSoftBody_Cluster_setContainsAnchor(_native, value); }
		}

		public Vector3Array DImpulses
		{
			get
			{
                if (_dImpulses == null)
                {
                    _dImpulses = new Vector3Array(btSoftBody_Cluster_getDimpulses(_native), 2);
                }
                return _dImpulses;
			}
		}

        public AlignedVector3Array FrameRefs
		{
            get
            {
                if (_framerefs == null)
                {
                    _framerefs = new AlignedVector3Array(btSoftBody_Cluster_getFramerefs(_native));
                }
                return _framerefs;
            }
		}

		public Matrix FrameTransform
		{
			get
			{
				Matrix value;
				btSoftBody_Cluster_getFramexform(_native, out value);
				return value;
			}
			set { btSoftBody_Cluster_setFramexform(_native, ref value); }
		}

		public float Idmass
		{
			get { return btSoftBody_Cluster_getIdmass(_native); }
			set { btSoftBody_Cluster_setIdmass(_native, value); }
		}

		public float InverseMass
		{
			get { return btSoftBody_Cluster_getImass(_native); }
			set { btSoftBody_Cluster_setImass(_native, value); }
		}

		public Matrix InverseWorldInertia
		{
			get
			{
				Matrix value;
				btSoftBody_Cluster_getInvwi(_native, out value);
				return value;
			}
			set { btSoftBody_Cluster_setInvwi(_native, ref value); }
		}

		public float LinearDamping
		{
			get { return btSoftBody_Cluster_getLdamping(_native); }
			set { btSoftBody_Cluster_setLdamping(_native, value); }
		}

		public DbvtNode Leaf
		{
            get
            {
                IntPtr leafPtr = btSoftBody_Cluster_getLeaf(_native);
                if (_leaf != null && _leaf._native == leafPtr) return _leaf;
                if (leafPtr == IntPtr.Zero) return null;
                _leaf = new DbvtNode(leafPtr);
                return _leaf;
            }
			set
			{
                btSoftBody_Cluster_setLeaf(_native, (value != null) ? value._native : IntPtr.Zero);
				_leaf = value;
			}
		}

        public Vector3 LinearVelocity
        {
            get
            {
                Vector3 value;
                btSoftBody_Cluster_getLv(_native, out value);
                return value;
            }
            set { btSoftBody_Cluster_setLv(_native, ref value); }
        }

		public Matrix Locii
		{
			get
			{
				Matrix value;
				btSoftBody_Cluster_getLocii(_native, out value);
				return value;
			}
			set { btSoftBody_Cluster_setLocii(_native, ref value); }
		}

        /*
        public AlignedScalarArray Masses
		{
            get
            {
                if (_masses == null)
                {
                    _masses = new AlignedVector3Array(btSoftBody_Cluster_getMasses(_native));
                }
                return _masses;
            }
		}
        */
		public float Matching
		{
			get { return btSoftBody_Cluster_getMatching(_native); }
			set { btSoftBody_Cluster_setMatching(_native, value); }
		}

		public float MaxSelfCollisionImpulse
		{
			get { return btSoftBody_Cluster_getMaxSelfCollisionImpulse(_native); }
			set { btSoftBody_Cluster_setMaxSelfCollisionImpulse(_native, value); }
		}

		public float NodeDamping
		{
			get { return btSoftBody_Cluster_getNdamping(_native); }
			set { btSoftBody_Cluster_setNdamping(_native, value); }
		}

        public AlignedNodeArray Nodes
		{
            get
            {
                if (_nodes == null)
                {
                    _nodes = new AlignedNodeArray(btSoftBody_Cluster_getNodes(_native));
                }
                return _nodes;
            }
		}

        public int NumDImpulses
        {
            get { return btSoftBody_Cluster_getNdimpulses(_native); }
            set { btSoftBody_Cluster_setNdimpulses(_native, value); }
        }

        public int NumVImpulses
		{
			get { return btSoftBody_Cluster_getNvimpulses(_native); }
			set { btSoftBody_Cluster_setNvimpulses(_native, value); }
		}

		public float SelfCollisionImpulseFactor
		{
			get { return btSoftBody_Cluster_getSelfCollisionImpulseFactor(_native); }
			set { btSoftBody_Cluster_setSelfCollisionImpulseFactor(_native, value); }
		}

		public Vector3Array VImpulses
		{
			get
			{
                if (_vImpulses == null)
                {
                    _vImpulses = new Vector3Array(btSoftBody_Cluster_getVimpulses(_native), 2);
                }
                return _vImpulses;
			}
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btSoftBody_Cluster_getAdamping(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Cluster_getAv(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btSoftBody_Cluster_getClusterIndex(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btSoftBody_Cluster_getCollide(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Cluster_getCom(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btSoftBody_Cluster_getContainsAnchor(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBody_Cluster_getDimpulses(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBody_Cluster_getFramerefs(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Cluster_getFramexform(IntPtr obj, [Out] out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btSoftBody_Cluster_getIdmass(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btSoftBody_Cluster_getImass(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Cluster_getInvwi(IntPtr obj, [Out] out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btSoftBody_Cluster_getLdamping(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBody_Cluster_getLeaf(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Cluster_getLocii(IntPtr obj, [Out] out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Cluster_getLv(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBody_Cluster_getMasses(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btSoftBody_Cluster_getMatching(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btSoftBody_Cluster_getMaxSelfCollisionImpulse(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btSoftBody_Cluster_getNdamping(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btSoftBody_Cluster_getNdimpulses(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBody_Cluster_getNodes(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btSoftBody_Cluster_getNvimpulses(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btSoftBody_Cluster_getSelfCollisionImpulseFactor(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBody_Cluster_getVimpulses(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Cluster_setAdamping(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Cluster_setAv(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Cluster_setClusterIndex(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Cluster_setCollide(IntPtr obj, bool value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Cluster_setCom(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Cluster_setContainsAnchor(IntPtr obj, bool value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Cluster_setFramexform(IntPtr obj, [In] ref Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Cluster_setIdmass(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Cluster_setImass(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Cluster_setInvwi(IntPtr obj, [In] ref Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Cluster_setLdamping(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Cluster_setLeaf(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Cluster_setLocii(IntPtr obj, [In] ref Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Cluster_setLv(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Cluster_setMatching(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Cluster_setMaxSelfCollisionImpulse(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Cluster_setNdamping(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Cluster_setNdimpulses(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Cluster_setNvimpulses(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Cluster_setSelfCollisionImpulseFactor(IntPtr obj, float value);
	}

	public class Config
	{
		internal IntPtr _native;

        //private AlignedPSolverArray _dSequence;
        //private AlignedPSolverArray _pSequence;
        //private AlignedVSolverArray _vSequence;

		internal Config(IntPtr native)
		{
			_native = native;
		}

		public AeroModel AeroModel
		{
			get { return btSoftBody_Config_getAeromodel(_native); }
			set { btSoftBody_Config_setAeromodel(_native, value); }
		}

        public float AnchorHardness
        {
            get { return btSoftBody_Config_getKAHR(_native); }
            set { btSoftBody_Config_setKAHR(_native, value); }
        }

        public int ClusterIterations
		{
			get { return btSoftBody_Config_getCiterations(_native); }
			set { btSoftBody_Config_setCiterations(_native, value); }
		}

		public CollisionFlags Collisions
		{
			get { return btSoftBody_Config_getCollisions(_native); }
			set { btSoftBody_Config_setCollisions(_native, value); }
		}

        public float Damping
        {
            get { return btSoftBody_Config_getKDP(_native); }
            set { btSoftBody_Config_setKDP(_native, value); }
        }

        public float DynamicFriction
        {
            get { return btSoftBody_Config_getKDF(_native); }
            set { btSoftBody_Config_setKDF(_native, value); }
        }

        public float Drag
        {
            get { return btSoftBody_Config_getKDG(_native); }
            set { btSoftBody_Config_setKDG(_native, value); }
        }

        public int DriftIterations
		{
			get { return btSoftBody_Config_getDiterations(_native); }
			set { btSoftBody_Config_setDiterations(_native, value); }
		}
        /*
        public AlignedPSolverArray DriftSequence
		{
            get
            {
                if (_dSequence == null)
                {
                    _dSequence = new AlignedPSolverArray(btSoftBody_Config_getDsequence(_native));
                }
                return _dsequence;
            }
		}
        */
        public float KineticContactHardness
		{
			get { return btSoftBody_Config_getKKHR(_native); }
			set { btSoftBody_Config_setKKHR(_native, value); }
		}

        public float Lift
		{
			get { return btSoftBody_Config_getKLF(_native); }
			set { btSoftBody_Config_setKLF(_native, value); }
		}

        public float MaxVolume
        {
            get { return btSoftBody_Config_getMaxvolume(_native); }
            set { btSoftBody_Config_setMaxvolume(_native, value); }
        }

		public float PoseMatching
		{
			get { return btSoftBody_Config_getKMT(_native); }
			set { btSoftBody_Config_setKMT(_native, value); }
		}

        public int PositionIterations
        {
            get { return btSoftBody_Config_getPiterations(_native); }
            set { btSoftBody_Config_setPiterations(_native, value); }
        }
        /*
        public AlignedPSolverArray PositionSequence
        {
            get
            {
                if (_pSequence == null)
                {
                    _pSequence = new AlignedPSolverArray(btSoftBody_Config_getPsequence(_native));
                }
                return _psequence;
            }
        }
        */
		public float Pressure
		{
			get { return btSoftBody_Config_getKPR(_native); }
			set { btSoftBody_Config_setKPR(_native, value); }
		}

        public float RigidContactHardness
        {
            get { return btSoftBody_Config_getKCHR(_native); }
            set { btSoftBody_Config_setKCHR(_native, value); }
        }

		public float SoftContactHardness
		{
			get { return btSoftBody_Config_getKSHR(_native); }
			set { btSoftBody_Config_setKSHR(_native, value); }
		}

        public float SoftKineticHardness
		{
			get { return btSoftBody_Config_getKSKHR_CL(_native); }
			set { btSoftBody_Config_setKSKHR_CL(_native, value); }
		}

        public float SoftKineticImpulseSplit
		{
			get { return btSoftBody_Config_getKSK_SPLT_CL(_native); }
			set { btSoftBody_Config_setKSK_SPLT_CL(_native, value); }
		}

        public float SoftRigidHardness
		{
			get { return btSoftBody_Config_getKSRHR_CL(_native); }
			set { btSoftBody_Config_setKSRHR_CL(_native, value); }
		}

        public float SoftRigidImpulseSplit
		{
			get { return btSoftBody_Config_getKSR_SPLT_CL(_native); }
			set { btSoftBody_Config_setKSR_SPLT_CL(_native, value); }
		}

        public float SoftSoftHardness
		{
			get { return btSoftBody_Config_getKSSHR_CL(_native); }
			set { btSoftBody_Config_setKSSHR_CL(_native, value); }
		}

        public float SoftSoftImpulseSplit
		{
			get { return btSoftBody_Config_getKSS_SPLT_CL(_native); }
			set { btSoftBody_Config_setKSS_SPLT_CL(_native, value); }
		}

		public float VolumeConversation
		{
			get { return btSoftBody_Config_getKVC(_native); }
			set { btSoftBody_Config_setKVC(_native, value); }
		}

		public float VelocityCorrectionFactor
		{
			get { return btSoftBody_Config_getKVCF(_native); }
			set { btSoftBody_Config_setKVCF(_native, value); }
		}

		public float Timescale
		{
			get { return btSoftBody_Config_getTimescale(_native); }
			set { btSoftBody_Config_setTimescale(_native, value); }
		}

		public int VelocityIterations
		{
			get { return btSoftBody_Config_getViterations(_native); }
			set { btSoftBody_Config_setViterations(_native, value); }
		}
        /*
        public AlignedVSolverArray VelocitySequence
		{
            get
            {
                if (_vSequence == null)
                {
                    _vSequence = new AlignedPSolverArray(btSoftBody_Config_getVsequence(_native));
                }
                return _vsequence;
            }
		}
        */
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern AeroModel btSoftBody_Config_getAeromodel(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btSoftBody_Config_getCiterations(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern CollisionFlags btSoftBody_Config_getCollisions(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btSoftBody_Config_getDiterations(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBody_Config_getDsequence(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btSoftBody_Config_getKAHR(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btSoftBody_Config_getKCHR(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btSoftBody_Config_getKDF(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btSoftBody_Config_getKDG(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btSoftBody_Config_getKDP(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btSoftBody_Config_getKKHR(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btSoftBody_Config_getKLF(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btSoftBody_Config_getKMT(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btSoftBody_Config_getKPR(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btSoftBody_Config_getKSHR(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btSoftBody_Config_getKSK_SPLT_CL(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btSoftBody_Config_getKSKHR_CL(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btSoftBody_Config_getKSR_SPLT_CL(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btSoftBody_Config_getKSRHR_CL(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btSoftBody_Config_getKSS_SPLT_CL(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btSoftBody_Config_getKSSHR_CL(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btSoftBody_Config_getKVC(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btSoftBody_Config_getKVCF(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btSoftBody_Config_getMaxvolume(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btSoftBody_Config_getPiterations(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBody_Config_getPsequence(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btSoftBody_Config_getTimescale(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btSoftBody_Config_getViterations(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBody_Config_getVsequence(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Config_setAeromodel(IntPtr obj, AeroModel value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Config_setCiterations(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btSoftBody_Config_setCollisions(IntPtr obj, CollisionFlags value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Config_setDiterations(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Config_setKAHR(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Config_setKCHR(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Config_setKDF(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Config_setKDG(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Config_setKDP(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Config_setKKHR(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Config_setKLF(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Config_setKMT(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Config_setKPR(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Config_setKSHR(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Config_setKSK_SPLT_CL(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Config_setKSKHR_CL(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Config_setKSR_SPLT_CL(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Config_setKSRHR_CL(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Config_setKSS_SPLT_CL(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Config_setKSSHR_CL(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Config_setKVC(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Config_setKVCF(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Config_setMaxvolume(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Config_setPiterations(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Config_setTimescale(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Config_setViterations(IntPtr obj, int value);
	}

	public class Element
	{
		internal IntPtr _native;

		internal Element(IntPtr native)
		{
			_native = native;
		}

		public IntPtr Tag
		{
			get { return btSoftBody_Element_getTag(_native); }
			set { btSoftBody_Element_setTag(_native, value); }
		}

        public override bool Equals(object obj)
        {
            Element element = obj as Element;
            if (element == null)
            {
                return false;
            }
            return _native == element._native;
        }

        public override int GetHashCode()
        {
            return _native.GetHashCode();
        }

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBody_Element_getTag(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Element_setTag(IntPtr obj, IntPtr value);
	}

	public class Face : Feature
	{
		private DbvtNode _leaf;
        private NodePtrArray _n;

		internal Face(IntPtr native)
			: base(native)
		{
		}

		public DbvtNode Leaf
		{
            get
            {
                IntPtr leafPtr = btSoftBody_Face_getLeaf(_native);
                if (_leaf != null && _leaf._native == leafPtr) return _leaf;
                if (leafPtr == IntPtr.Zero) return null;
                _leaf = new DbvtNode(leafPtr);
                return _leaf;
            }
			set
			{
				btSoftBody_Face_setLeaf(_native, (value != null) ? value._native : IntPtr.Zero);
				_leaf = value;
			}
		}

        public NodePtrArray Nodes
		{
            get
            {
                if (_n == null)
                {
                    _n = new NodePtrArray(btSoftBody_Face_getN(_native), 3);
                }
                return _n;
            }
		}

		public Vector3 Normal
		{
			get
			{
				Vector3 value;
				btSoftBody_Face_getNormal(_native, out value);
				return value;
			}
			set { btSoftBody_Face_setNormal(_native, ref value); }
		}

        public float RestArea
		{
			get { return btSoftBody_Face_getRa(_native); }
			set { btSoftBody_Face_setRa(_native, value); }
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBody_Face_getLeaf(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBody_Face_getN(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Face_getNormal(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btSoftBody_Face_getRa(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Face_setLeaf(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Face_setNormal(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Face_setRa(IntPtr obj, float value);
	}

	public class Feature : Element
	{
		private Material _material;

		internal Feature(IntPtr native)
			: base(native)
		{
		}

		public Material Material
		{
            get
            {
                IntPtr materialPtr = btSoftBody_Feature_getMaterial(_native);
                if (_material != null && _material._native == materialPtr) return _material;
                if (materialPtr == IntPtr.Zero) return null;
                _material = new Material(materialPtr);
                return _material;
            }
			set
			{
				btSoftBody_Feature_setMaterial(_native, (value != null) ? value._native : IntPtr.Zero);
				_material = value;
			}
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBody_Feature_getMaterial(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Feature_setMaterial(IntPtr obj, IntPtr value);
	}

	public abstract class ImplicitFn : IDisposable
	{
		internal IntPtr _native;

        [UnmanagedFunctionPointer(Native.Conv), SuppressUnmanagedCodeSecurity]
        delegate float EvalUnmanagedDelegate([In] ref Vector3 x);

        EvalUnmanagedDelegate _eval;

		protected ImplicitFn()
		{
            _eval = Eval;

            _native = btSoftBody_ImplicitFnWrapper_new(Marshal.GetFunctionPointerForDelegate(_eval));
		}

        public abstract float Eval(ref Vector3 x);

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (_native != IntPtr.Zero)
			{
				btSoftBody_ImplicitFn_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~ImplicitFn()
		{
			Dispose(false);
		}

        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btSoftBody_ImplicitFnWrapper_new(IntPtr evalCallback);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_ImplicitFn_delete(IntPtr obj);
	}

	public class Impulse : IDisposable
	{
		internal IntPtr _native;

		internal Impulse(IntPtr native)
		{
			_native = native;
		}

		public Impulse()
		{
			_native = btSoftBody_Impulse_new();
		}
        /*
		public Impulse operator-()
		{
			return btSoftBody_Impulse_operator_n(_native);
		}

		public Impulse operator*(float x)
		{
			return btSoftBody_Impulse_operator_m(_native, x);
		}
        */
		public int AsDrift
		{
			get { return btSoftBody_Impulse_getAsDrift(_native); }
			set { btSoftBody_Impulse_setAsDrift(_native, value); }
		}

		public int AsVelocity
		{
			get { return btSoftBody_Impulse_getAsVelocity(_native); }
			set { btSoftBody_Impulse_setAsVelocity(_native, value); }
		}

		public Vector3 Drift
		{
			get
			{
				Vector3 value;
				btSoftBody_Impulse_getDrift(_native, out value);
				return value;
			}
			set { btSoftBody_Impulse_setDrift(_native, ref value); }
		}

		public Vector3 Velocity
		{
			get
			{
				Vector3 value;
				btSoftBody_Impulse_getVelocity(_native, out value);
				return value;
			}
			set { btSoftBody_Impulse_setVelocity(_native, ref value); }
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
				btSoftBody_Impulse_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~Impulse()
		{
			Dispose(false);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBody_Impulse_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btSoftBody_Impulse_getAsDrift(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btSoftBody_Impulse_getAsVelocity(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Impulse_getDrift(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Impulse_getVelocity(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Impulse_operator_n(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Impulse_operator_m(IntPtr obj, float x);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Impulse_setAsDrift(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Impulse_setAsVelocity(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Impulse_setDrift(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Impulse_setVelocity(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Impulse_delete(IntPtr obj);
	}

	public abstract class Joint
	{
		public class Specs : IDisposable
		{
			internal IntPtr _native;

			internal Specs(IntPtr native)
			{
				_native = native;
			}

            public float ConstraintForceMixing
			{
				get { return btSoftBody_Joint_Specs_getCfm(_native); }
				set { btSoftBody_Joint_Specs_setCfm(_native, value); }
			}

            public float ErrorReductionParameter
			{
				get { return btSoftBody_Joint_Specs_getErp(_native); }
				set { btSoftBody_Joint_Specs_setErp(_native, value); }
			}

			public float Split
			{
				get { return btSoftBody_Joint_Specs_getSplit(_native); }
				set { btSoftBody_Joint_Specs_setSplit(_native, value); }
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
					btSoftBody_Joint_Specs_delete(_native);
					_native = IntPtr.Zero;
				}
			}

			~Specs()
			{
				Dispose(false);
			}

			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern float btSoftBody_Joint_Specs_getCfm(IntPtr obj);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern float btSoftBody_Joint_Specs_getErp(IntPtr obj);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern float btSoftBody_Joint_Specs_getSplit(IntPtr obj);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btSoftBody_Joint_Specs_setCfm(IntPtr obj, float value);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btSoftBody_Joint_Specs_setErp(IntPtr obj, float value);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btSoftBody_Joint_Specs_setSplit(IntPtr obj, float value);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btSoftBody_Joint_Specs_delete(IntPtr obj);
		}

		internal IntPtr _native;

        private Vector3Array _refs;

        internal static Joint GetManaged(IntPtr native)
        {
            if (native == IntPtr.Zero)
            {
                return null;
            }

            switch (btSoftBody_Joint_Type(native))
            {
                case JointType.Angular:
                    return new AngularJoint(native);
                case JointType.Contact:
                    return new ContactJoint(native);
                case JointType.Linear:
                    return new LinearJoint(native);
                default:
                    throw new NotImplementedException();
            }
        }

		internal Joint(IntPtr native)
		{
			_native = native;
		}

		public void Prepare(float deltaTime, int iterations)
		{
			btSoftBody_Joint_Prepare(_native, deltaTime, iterations);
		}

		public void Solve(float deltaTime, float sor)
		{
			btSoftBody_Joint_Solve(_native, deltaTime, sor);
		}

		public void Terminate(float deltaTime)
		{
			btSoftBody_Joint_Terminate(_native, deltaTime);
		}
        /*
		public BodyArray Bodies
		{
			get { return btSoftBody_Joint_getBodies(_native); }
		}
        */
        public float ConstraintForceMixing
		{
			get { return btSoftBody_Joint_getCfm(_native); }
			set { btSoftBody_Joint_setCfm(_native, value); }
		}

		public bool Delete
		{
			get { return btSoftBody_Joint_getDelete(_native); }
			set { btSoftBody_Joint_setDelete(_native, value); }
		}

		public Vector3 Drift
		{
			get
			{
				Vector3 value;
				btSoftBody_Joint_getDrift(_native, out value);
				return value;
			}
			set { btSoftBody_Joint_setDrift(_native, ref value); }
		}

        public float ErrorReductionParameter
		{
			get { return btSoftBody_Joint_getErp(_native); }
			set { btSoftBody_Joint_setErp(_native, value); }
		}

		public Matrix MassMatrix
		{
			get
			{
				Matrix value;
				btSoftBody_Joint_getMassmatrix(_native, out value);
				return value;
			}
			set { btSoftBody_Joint_setMassmatrix(_native, ref value); }
		}

		public Vector3Array Refs
		{
			get
			{
                if (_refs == null)
                {
                    _refs = new Vector3Array(btSoftBody_Joint_getRefs(_native), 2);
                }
                return _refs;
			}
		}

        public Vector3 SplitDrift
		{
			get
			{
				Vector3 value;
				btSoftBody_Joint_getSdrift(_native, out value);
				return value;
			}
			set { btSoftBody_Joint_setSdrift(_native, ref value); }
		}

		public float Split
		{
			get { return btSoftBody_Joint_getSplit(_native); }
			set { btSoftBody_Joint_setSplit(_native, value); }
		}

        public JointType Type
        {
            get { return btSoftBody_Joint_Type(_native); }
        }

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBody_Joint_getBodies(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btSoftBody_Joint_getCfm(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btSoftBody_Joint_getDelete(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Joint_getDrift(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btSoftBody_Joint_getErp(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Joint_getMassmatrix(IntPtr obj, [Out] out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBody_Joint_getRefs(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Joint_getSdrift(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btSoftBody_Joint_getSplit(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Joint_Prepare(IntPtr obj, float dt, int iterations);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Joint_setCfm(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Joint_setDelete(IntPtr obj, bool value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Joint_setDrift(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Joint_setErp(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Joint_setMassmatrix(IntPtr obj, [In] ref Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Joint_setSdrift(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Joint_setSplit(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Joint_Solve(IntPtr obj, float dt, float sor);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Joint_Terminate(IntPtr obj, float dt);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern JointType btSoftBody_Joint_Type(IntPtr obj);
	}

	public class Link : Feature
	{
        private NodePtrArray _n;

		internal Link(IntPtr native)
			: base(native)
		{
		}

		public float C0
		{
			get { return btSoftBody_Link_getC0(_native); }
			set { btSoftBody_Link_setC0(_native, value); }
		}

		public float C1
		{
			get { return btSoftBody_Link_getC1(_native); }
			set { btSoftBody_Link_setC1(_native, value); }
		}

		public float C2
		{
			get { return btSoftBody_Link_getC2(_native); }
			set { btSoftBody_Link_setC2(_native, value); }
		}

		public Vector3 C3
		{
			get
			{
				Vector3 value;
				btSoftBody_Link_getC3(_native, out value);
				return value;
			}
			set { btSoftBody_Link_setC3(_native, ref value); }
		}

        public int IsBending
        {
            get { return btSoftBody_Link_getBbending(_native); }
            set { btSoftBody_Link_setBbending(_native, value); }
        }

        public NodePtrArray Nodes
		{
            get
            {
                if (_n == null)
                {
                    _n = new NodePtrArray(btSoftBody_Link_getN(_native), 2);
                }
                return _n;
            }
		}

        public float RestLength
		{
			get { return btSoftBody_Link_getRl(_native); }
			set { btSoftBody_Link_setRl(_native, value); }
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btSoftBody_Link_getBbending(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btSoftBody_Link_getC0(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btSoftBody_Link_getC1(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btSoftBody_Link_getC2(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Link_getC3(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBody_Link_getN(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btSoftBody_Link_getRl(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Link_setBbending(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Link_setC0(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Link_setC1(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Link_setC2(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Link_setC3(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Link_setRl(IntPtr obj, float value);
	}

	public class LinearJoint : Joint
	{
		public new class Specs : Joint.Specs
		{
			public Specs()
				: base(btSoftBody_LJoint_Specs_new())
			{
			}

			public Vector3 Position
			{
				get
				{
					Vector3 value;
					btSoftBody_LJoint_Specs_getPosition(_native, out value);
					return value;
				}
				set { btSoftBody_LJoint_Specs_setPosition(_native, ref value); }
			}

			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern IntPtr btSoftBody_LJoint_Specs_new();
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btSoftBody_LJoint_Specs_getPosition(IntPtr obj, [Out] out Vector3 value);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btSoftBody_LJoint_Specs_setPosition(IntPtr obj, [In] ref Vector3 value);
		}

        private Vector3Array _rPos;

		internal LinearJoint(IntPtr native)
			: base(native)
		{
		}

		public Vector3Array RPos
		{
			get
			{
                if (_rPos == null)
                {
                    _rPos = new Vector3Array(btSoftBody_LJoint_getRpos(_native), 2);
                }
                return _rPos;
			}
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBody_LJoint_getRpos(IntPtr obj);
	}

	public class Material : Element
	{
		internal Material(IntPtr native)
			: base(native)
		{
		}

        public float AngularStiffness
        {
            get { return btSoftBody_Material_getKAST(_native); }
            set { btSoftBody_Material_setKAST(_native, value); }
        }

        public MaterialFlags Flags
		{
			get { return btSoftBody_Material_getFlags(_native); }
			set { btSoftBody_Material_setFlags(_native, value); }
		}

        public float LinearStiffness
		{
			get { return btSoftBody_Material_getKLST(_native); }
			set { btSoftBody_Material_setKLST(_native, value); }
		}

        public float VolumeStiffness
		{
			get { return btSoftBody_Material_getKVST(_native); }
			set { btSoftBody_Material_setKVST(_native, value); }
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern MaterialFlags btSoftBody_Material_getFlags(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btSoftBody_Material_getKAST(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btSoftBody_Material_getKLST(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btSoftBody_Material_getKVST(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btSoftBody_Material_setFlags(IntPtr obj, MaterialFlags value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Material_setKAST(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Material_setKLST(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Material_setKVST(IntPtr obj, float value);
	}

	public class Node : Feature
	{
		private DbvtNode _leaf;

		internal Node(IntPtr native)
			: base(native)
		{
		}

		public float Area
		{
			get { return btSoftBody_Node_getArea(_native); }
			set { btSoftBody_Node_setArea(_native, value); }
		}

		public Vector3 Force
		{
			get
			{
				Vector3 value;
				btSoftBody_Node_getF(_native, out value);
				return value;
			}
			set { btSoftBody_Node_setF(_native, ref value); }
		}

		public float InverseMass
		{
			get { return btSoftBody_Node_getIm(_native); }
			set { btSoftBody_Node_setIm(_native, value); }
		}

        public int IsAttached
        {
            get { return btSoftBody_Node_getBattach(_native); }
            set { btSoftBody_Node_setBattach(_native, value); }
        }

		public DbvtNode Leaf
		{
            get
            {
                IntPtr leafPtr = btSoftBody_Node_getLeaf(_native);
                if (_leaf != null && _leaf._native == leafPtr) return _leaf;
                if (leafPtr == IntPtr.Zero) return null;
                _leaf = new DbvtNode(leafPtr);
                return _leaf;
            }
			set
			{
                btSoftBody_Node_setLeaf(_native, (value != null) ? value._native : IntPtr.Zero);
				_leaf = value;
			}
		}

		public Vector3 Normal
		{
			get
			{
				Vector3 value;
				btSoftBody_Node_getN(_native, out value);
				return value;
			}
			set { btSoftBody_Node_setN(_native, ref value); }
		}

        public Vector3 Position
        {
            get
            {
                Vector3 value;
                btSoftBody_Node_getX(_native, out value);
                return value;
            }
            set { btSoftBody_Node_setX(_native, ref value); }
        }

		public Vector3 Q
		{
			get
			{
				Vector3 value;
				btSoftBody_Node_getQ(_native, out value);
				return value;
			}
			set { btSoftBody_Node_setQ(_native, ref value); }
		}

		public Vector3 Velocity
		{
			get
			{
				Vector3 value;
				btSoftBody_Node_getV(_native, out value);
				return value;
			}
			set { btSoftBody_Node_setV(_native, ref value); }
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btSoftBody_Node_getArea(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btSoftBody_Node_getBattach(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Node_getF(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btSoftBody_Node_getIm(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBody_Node_getLeaf(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Node_getN(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Node_getQ(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Node_getV(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Node_getX(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Node_setArea(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Node_setBattach(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Node_setF(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Node_setIm(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Node_setLeaf(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Node_setN(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Node_setQ(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Node_setV(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Node_setX(IntPtr obj, [In] ref Vector3 value);
	}

	public class Note : Element
	{
        private NodePtrArray _nodes;

		internal Note(IntPtr native)
			: base(native)
		{
		}
        /*
		public FloatArray Coords
		{
			get { return btSoftBody_Note_getCoords(_native); }
		}
        */
		public NodePtrArray Nodes
		{
            get
            {
                if (_nodes == null)
                {
                    _nodes = new NodePtrArray(btSoftBody_Note_getNodes(_native), 4);
                }
                return _nodes;
            }
		}

		public Vector3 Offset
		{
			get
			{
				Vector3 value;
				btSoftBody_Note_getOffset(_native, out value);
				return value;
			}
			set { btSoftBody_Note_setOffset(_native, ref value); }
		}

		public int Rank
		{
			get { return btSoftBody_Note_getRank(_native); }
			set { btSoftBody_Note_setRank(_native, value); }
		}

		public string Text
		{
			get { return btSoftBody_Note_getText(_native); }
			set { btSoftBody_Note_setText(_native, value); }
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBody_Note_getCoords(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBody_Note_getNodes(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Note_getOffset(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btSoftBody_Note_getRank(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern string btSoftBody_Note_getText(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Note_setOffset(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Note_setRank(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Note_setText(IntPtr obj, string value);
	}

	public class Pose
	{
		internal IntPtr _native;

        private AlignedVector3Array _pos;
        //private AlignedScalarArray _wgh;

		internal Pose(IntPtr native)
		{
			_native = native;
		}

		public Matrix Aqq
		{
			get
			{
				Matrix value;
				btSoftBody_Pose_getAqq(_native, out value);
				return value;
			}
			set { btSoftBody_Pose_setAqq(_native, ref value); }
		}

		public Vector3 Com
		{
			get
			{
				Vector3 value;
				btSoftBody_Pose_getCom(_native, out value);
				return value;
			}
			set { btSoftBody_Pose_setCom(_native, ref value); }
		}

        public bool IsFrameValid
        {
            get { return btSoftBody_Pose_getBframe(_native); }
            set { btSoftBody_Pose_setBframe(_native, value); }
        }

        public bool IsVolumeValid
        {
            get { return btSoftBody_Pose_getBvolume(_native); }
            set { btSoftBody_Pose_setBvolume(_native, value); }
        }

        public AlignedVector3Array Positions
		{
            get
            {
                if (_pos == null)
                {
                    _pos = new AlignedVector3Array(btSoftBody_Pose_getPos(_native));
                }
                return _pos;
            }
		}

		public Matrix Rotation
		{
			get
			{
				Matrix value;
				btSoftBody_Pose_getRot(_native, out value);
				return value;
			}
			set { btSoftBody_Pose_setRot(_native, ref value); }
		}

		public Matrix Scale
		{
			get
			{
				Matrix value;
				btSoftBody_Pose_getScl(_native, out value);
				return value;
			}
			set { btSoftBody_Pose_setScl(_native, ref value); }
		}
        /*
        public AlignedScalarArray Weights
		{
            get
            {
                if (_wgh == null)
                {
                    _wgh = new AlignedScalarArray(btSoftBody_Pose_getWgh(_native));
                }
                return _wgh;
            }
		}
        */
		public float Volume
		{
			get { return btSoftBody_Pose_getVolume(_native); }
			set { btSoftBody_Pose_setVolume(_native, value); }
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Pose_getAqq(IntPtr obj, [Out] out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btSoftBody_Pose_getBframe(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btSoftBody_Pose_getBvolume(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Pose_getCom(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBody_Pose_getPos(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Pose_getRot(IntPtr obj, [Out] out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Pose_getScl(IntPtr obj, [Out] out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBody_Pose_getWgh(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btSoftBody_Pose_getVolume(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Pose_setAqq(IntPtr obj, [In] ref Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Pose_setBframe(IntPtr obj, bool value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Pose_setBvolume(IntPtr obj, bool value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Pose_setCom(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Pose_setRot(IntPtr obj, [In] ref Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Pose_setScl(IntPtr obj, [In] ref Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Pose_setVolume(IntPtr obj, float value);
	}

	public class RayFromToCaster : Dbvt.ICollide
	{
		private Face _face;

		internal RayFromToCaster(IntPtr native)
			: base(native)
		{
		}

		public RayFromToCaster(Vector3 rayFrom, Vector3 rayTo, float mxt)
			: base(btSoftBody_RayFromToCaster_new(ref rayFrom, ref rayTo, mxt))
		{
		}

		public static float RayFromToTriangle(Vector3 rayFrom, Vector3 rayTo, Vector3 rayNormalizedDirection, Vector3 a, Vector3 b, Vector3 c)
		{
			return btSoftBody_RayFromToCaster_rayFromToTriangle(ref rayFrom, ref rayTo, ref rayNormalizedDirection, ref a, ref b, ref c);
		}

		public static float RayFromToTriangle(Vector3 rayFrom, Vector3 rayTo, Vector3 rayNormalizedDirection, Vector3 a, Vector3 b, Vector3 c, float maxt)
		{
			return btSoftBody_RayFromToCaster_rayFromToTriangle2(ref rayFrom, ref rayTo, ref rayNormalizedDirection, ref a, ref b, ref c, maxt);
		}

		public Face Face
		{
            get
            {
                if (_face == null)
                {
                    IntPtr facePtr = btSoftBody_RayFromToCaster_getFace(_native);
                    if (facePtr != IntPtr.Zero)
                    {
                        _face = new Face(facePtr);
                    }
                }
                return _face;
            }
			set
			{
				btSoftBody_RayFromToCaster_setFace(_native, value._native);
				_face = value;
			}
		}

		public float Mint
		{
			get { return btSoftBody_RayFromToCaster_getMint(_native); }
			set { btSoftBody_RayFromToCaster_setMint(_native, value); }
		}

		public Vector3 RayFrom
		{
			get
			{
				Vector3 value;
				btSoftBody_RayFromToCaster_getRayFrom(_native, out value);
				return value;
			}
			set { btSoftBody_RayFromToCaster_setRayFrom(_native, ref value); }
		}

		public Vector3 RayNormalizedDirection
		{
			get
			{
				Vector3 value;
				btSoftBody_RayFromToCaster_getRayNormalizedDirection(_native, out value);
				return value;
			}
			set { btSoftBody_RayFromToCaster_setRayNormalizedDirection(_native, ref value); }
		}

		public Vector3 RayTo
		{
			get
			{
				Vector3 value;
				btSoftBody_RayFromToCaster_getRayTo(_native, out value);
				return value;
			}
			set { btSoftBody_RayFromToCaster_setRayTo(_native, ref value); }
		}

		public int Tests
		{
			get { return btSoftBody_RayFromToCaster_getTests(_native); }
			set { btSoftBody_RayFromToCaster_setTests(_native, value); }
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBody_RayFromToCaster_new([In] ref Vector3 rayFrom, [In] ref Vector3 rayTo, float mxt);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBody_RayFromToCaster_getFace(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btSoftBody_RayFromToCaster_getMint(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_RayFromToCaster_getRayFrom(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_RayFromToCaster_getRayNormalizedDirection(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_RayFromToCaster_getRayTo(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btSoftBody_RayFromToCaster_getTests(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btSoftBody_RayFromToCaster_rayFromToTriangle([In] ref Vector3 rayFrom, [In] ref Vector3 rayTo, [In] ref Vector3 rayNormalizedDirection, [In] ref Vector3 a, [In] ref Vector3 b, [In] ref Vector3 c);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btSoftBody_RayFromToCaster_rayFromToTriangle2([In] ref Vector3 rayFrom, [In] ref Vector3 rayTo, [In] ref Vector3 rayNormalizedDirection, [In] ref Vector3 a, [In] ref Vector3 b, [In] ref Vector3 c, float maxt);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_RayFromToCaster_setFace(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_RayFromToCaster_setMint(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_RayFromToCaster_setRayFrom(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_RayFromToCaster_setRayNormalizedDirection(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_RayFromToCaster_setRayTo(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_RayFromToCaster_setTests(IntPtr obj, int value);
	}

	public class RigidContact : IDisposable
	{
		internal IntPtr _native;

		private Node _node;
        private sCti _cti;

		internal RigidContact(IntPtr native)
		{
			_native = native;
		}

		public RigidContact()
		{
			_native = btSoftBody_RContact_new();
		}

		public Matrix C0
		{
			get
			{
				Matrix value;
				btSoftBody_RContact_getC0(_native, out value);
				return value;
			}
			set { btSoftBody_RContact_setC0(_native, ref value); }
		}

		public Vector3 C1
		{
			get
			{
				Vector3 value;
				btSoftBody_RContact_getC1(_native, out value);
				return value;
			}
			set { btSoftBody_RContact_setC1(_native, ref value); }
		}

		public float C2
		{
			get { return btSoftBody_RContact_getC2(_native); }
			set { btSoftBody_RContact_setC2(_native, value); }
		}

		public float C3
		{
			get { return btSoftBody_RContact_getC3(_native); }
			set { btSoftBody_RContact_setC3(_native, value); }
		}

		public float C4
		{
			get { return btSoftBody_RContact_getC4(_native); }
			set { btSoftBody_RContact_setC4(_native, value); }
		}

		public sCti Cti
		{
            get
            {
                if (_cti == null)
                {
                    _cti = new sCti(btSoftBody_RContact_getCti(_native));
                }
                return _cti;
            }
		}

		public Node Node
		{
            get
            {
                IntPtr nodePtr = btSoftBody_RContact_getNode(_native);
                if (_node != null && _node._native == nodePtr) return _node;
                if (nodePtr == IntPtr.Zero) return null;
                _node = new Node(nodePtr);
                return _node;
            }
			set
			{
				btSoftBody_RContact_setNode(_native, (value != null) ? value._native : IntPtr.Zero);
				_node = value;
			}
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
				btSoftBody_RContact_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~RigidContact()
		{
			Dispose(false);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBody_RContact_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_RContact_getC0(IntPtr obj, [Out] out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_RContact_getC1(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btSoftBody_RContact_getC2(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btSoftBody_RContact_getC3(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btSoftBody_RContact_getC4(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBody_RContact_getCti(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBody_RContact_getNode(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_RContact_setC0(IntPtr obj, [In] ref Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_RContact_setC1(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_RContact_setC2(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_RContact_setC3(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_RContact_setC4(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_RContact_setNode(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_RContact_delete(IntPtr obj);
	}

	public class sCti : IDisposable
	{
		internal IntPtr _native;

        internal sCti(IntPtr native)
		{
			_native = native;
		}

		public sCti()
		{
			_native = btSoftBody_sCti_new();
		}

		public CollisionObject CollisionObject
		{
            get { return CollisionObject.GetManaged(btSoftBody_sCti_getColObj(_native)); }
            set { btSoftBody_sCti_setColObj(_native, (value != null) ? value._native : IntPtr.Zero); }
		}

		public Vector3 Normal
		{
			get
			{
				Vector3 value;
				btSoftBody_sCti_getNormal(_native, out value);
				return value;
			}
			set { btSoftBody_sCti_setNormal(_native, ref value); }
		}

		public float Offset
		{
			get { return btSoftBody_sCti_getOffset(_native); }
			set { btSoftBody_sCti_setOffset(_native, value); }
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
				btSoftBody_sCti_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~sCti()
		{
			Dispose(false);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBody_sCti_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBody_sCti_getColObj(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_sCti_getNormal(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btSoftBody_sCti_getOffset(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_sCti_setColObj(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_sCti_setNormal(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_sCti_setOffset(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_sCti_delete(IntPtr obj);
	}

	public class SoftContact : IDisposable
	{
		internal IntPtr _native;

        //private ScalarArray _cfm;
		private Face _face;
		private Node _node;

		internal SoftContact(IntPtr native)
		{
			_native = native;
		}

		public SoftContact()
		{
			_native = btSoftBody_SContact_new();
		}
        /*
        public ScalarArray ConstraintForceMixing
		{
            get
            {
                if (_cfm == null)
                {
                    _cfm = new ScalarArray(btSoftBody_SContact_getCfm(_native), 2);
                }
                return _cfm;
            }
		}
        */
		public Face Face
		{
            get
            {
                if (_face == null)
                {
                    IntPtr facePtr = btSoftBody_SContact_getFace(_native);
                    if (facePtr != IntPtr.Zero)
                    {
                        _face = new Face(facePtr);
                    }
                }
                return _face;
            }
			set
			{
				btSoftBody_SContact_setFace(_native, (value != null) ? value._native : IntPtr.Zero);
				_face = value;
			}
		}

		public float Friction
		{
			get { return btSoftBody_SContact_getFriction(_native); }
			set { btSoftBody_SContact_setFriction(_native, value); }
		}

		public float Margin
		{
			get { return btSoftBody_SContact_getMargin(_native); }
			set { btSoftBody_SContact_setMargin(_native, value); }
		}

		public Node Node
		{
            get
            {
                IntPtr nodePtr = btSoftBody_SContact_getNode(_native);
                if (_node != null && _node._native == nodePtr) return _node;
                if (nodePtr == IntPtr.Zero) return null;
                _node = new Node(nodePtr);
                return _node;
            }
			set
			{
				btSoftBody_SContact_setNode(_native, value._native);
				_node = value;
			}
		}

		public Vector3 Normal
		{
			get
			{
				Vector3 value;
				btSoftBody_SContact_getNormal(_native, out value);
				return value;
			}
			set { btSoftBody_SContact_setNormal(_native, ref value); }
		}

		public Vector3 Weights
		{
			get
			{
				Vector3 value;
				btSoftBody_SContact_getWeights(_native, out value);
				return value;
			}
			set { btSoftBody_SContact_setWeights(_native, ref value); }
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
				btSoftBody_SContact_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~SoftContact()
		{
			Dispose(false);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBody_SContact_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBody_SContact_getCfm(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBody_SContact_getFace(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btSoftBody_SContact_getFriction(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btSoftBody_SContact_getMargin(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBody_SContact_getNode(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_SContact_getNormal(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_SContact_getWeights(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_SContact_setFace(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_SContact_setFriction(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_SContact_setMargin(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_SContact_setNode(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_SContact_setNormal(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_SContact_setWeights(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_SContact_delete(IntPtr obj);
	}

	public class SolverState
	{
		internal IntPtr _native;

		internal SolverState(IntPtr native)
		{
			_native = native;
		}

        public float InverseSdt
		{
			get { return btSoftBody_SolverState_getIsdt(_native); }
			set { btSoftBody_SolverState_setIsdt(_native, value); }
		}

        public float RadialMargin
		{
			get { return btSoftBody_SolverState_getRadmrg(_native); }
			set { btSoftBody_SolverState_setRadmrg(_native, value); }
		}

		public float Sdt
		{
			get { return btSoftBody_SolverState_getSdt(_native); }
			set { btSoftBody_SolverState_setSdt(_native, value); }
		}

        public float UpdateMargin
		{
			get { return btSoftBody_SolverState_getUpdmrg(_native); }
			set { btSoftBody_SolverState_setUpdmrg(_native, value); }
		}

        public float VelocityMargin
		{
			get { return btSoftBody_SolverState_getVelmrg(_native); }
			set { btSoftBody_SolverState_setVelmrg(_native, value); }
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btSoftBody_SolverState_getIsdt(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btSoftBody_SolverState_getRadmrg(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btSoftBody_SolverState_getSdt(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btSoftBody_SolverState_getUpdmrg(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btSoftBody_SolverState_getVelmrg(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_SolverState_setIsdt(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_SolverState_setRadmrg(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_SolverState_setSdt(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_SolverState_setUpdmrg(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_SolverState_setVelmrg(IntPtr obj, float value);
	}

	public class SRayCast
	{
        public SoftBody Body { get; set; }
        public FeatureType Feature { get; set; }
        public float Fraction { get; set; }
        public int Index { get; set; }
	}

	public class Tetra : Feature
	{
        private Vector3Array _c0;
		private DbvtNode _leaf;
        private NodePtrArray _nodes;

		internal Tetra(IntPtr native)
			: base(native)
		{
		}

		public Vector3Array C0
		{
			get
			{
                if (_c0 == null)
                {
                    _c0 = new Vector3Array(btSoftBody_Tetra_getC0(_native), 4);
                }
                return _c0;
			}
		}

		public float C1
		{
			get { return btSoftBody_Tetra_getC1(_native); }
			set { btSoftBody_Tetra_setC1(_native, value); }
		}

		public float C2
		{
			get { return btSoftBody_Tetra_getC2(_native); }
			set { btSoftBody_Tetra_setC2(_native, value); }
		}

		public DbvtNode Leaf
		{
            get
            {
                IntPtr leafPtr = btSoftBody_Tetra_getLeaf(_native);
                if (_leaf != null && _leaf._native == leafPtr) return _leaf;
                if (leafPtr == IntPtr.Zero) return null;
                _leaf = new DbvtNode(leafPtr);
                return _leaf;
            }
			set
			{
				btSoftBody_Tetra_setLeaf(_native, (value != null) ? value._native : IntPtr.Zero);
				_leaf = value;
			}
		}

        public NodePtrArray Nodes
		{
            get
            {
                if (_nodes == null)
                {
                    _nodes = new NodePtrArray(btSoftBody_Tetra_getN(_native), 4);
                }
                return _nodes;
            }
		}

        public float RestVolume
		{
			get { return btSoftBody_Tetra_getRv(_native); }
			set { btSoftBody_Tetra_setRv(_native, value); }
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBody_Tetra_getC0(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btSoftBody_Tetra_getC1(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btSoftBody_Tetra_getC2(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBody_Tetra_getLeaf(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBody_Tetra_getN(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btSoftBody_Tetra_getRv(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Tetra_setC1(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Tetra_setC2(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Tetra_setLeaf(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_Tetra_setRv(IntPtr obj, float value);
	}

	public class SoftBody : CollisionObject
	{
        private AlignedAnchorArray _anchors;
        private Vector3Array _bounds;
        private Dbvt _clusterDbvt;
        private Config _config;
        //private AlignedBoolArray _clusterConnectivity;
        private AlignedClusterArray _clusters;
        //private AlignedCollisionObjectArray _collisionDisabledObjects;
        private Dbvt _faceDbvt;
        private AlignedFaceArray _faces;
        private AlignedJointArray _joints;
        private AlignedLinkArray _links;
        private AlignedMaterialArray _materials;
        private Dbvt _nodeDbvt;
        private AlignedNodeArray _nodes;
        //private AlignedNoteArray _notes;
		private Pose _pose;
        //private AlignedRigidContactArray _rigidContacts;
        //private AlignedSoftContactArray _softContacts;
		private SoftBodySolver _softBodySolver;
        private SolverState _solverState;
        private AlignedTetraArray _tetras;
        //private AlignedIntArray _userIndexMapping;
		private SoftBodyWorldInfo _worldInfo;

        private List<AngularJoint.IControl> _aJointControls = new List<AngularJoint.IControl>();

		internal SoftBody(IntPtr native)
			: base(native)
		{
            _collisionShape = new SoftBodyCollisionShape(btCollisionObject_getCollisionShape(_native));
		}

		public SoftBody(SoftBodyWorldInfo worldInfo, int nodeCount, Vector3[] x, float[] m)
			: base(btSoftBody_new(worldInfo._native, nodeCount, x, m))
		{
            _collisionShape = new SoftBodyCollisionShape(btCollisionObject_getCollisionShape(_native));
			_worldInfo = worldInfo;
		}

		public SoftBody(SoftBodyWorldInfo worldInfo)
			: base(btSoftBody_new2(worldInfo._native))
		{
            _collisionShape = new SoftBodyCollisionShape(btCollisionObject_getCollisionShape(_native));
			_worldInfo = worldInfo;
		}

		public void AddAeroForceToFace(Vector3 windVelocity, int faceIndex)
		{
			btSoftBody_addAeroForceToFace(_native, ref windVelocity, faceIndex);
		}

		public void AddAeroForceToNode(Vector3 windVelocity, int nodeIndex)
		{
			btSoftBody_addAeroForceToNode(_native, ref windVelocity, nodeIndex);
		}

		public void AddForce(Vector3 force)
		{
			btSoftBody_addForce(_native, ref force);
		}

		public void AddForce(Vector3 force, int node)
		{
			btSoftBody_addForce2(_native, ref force, node);
		}

		public void AddVelocity(Vector3 velocity, int node)
		{
			btSoftBody_addVelocity(_native, ref velocity, node);
		}

		public void AddVelocity(Vector3 velocity)
		{
			btSoftBody_addVelocity2(_native, ref velocity);
		}

		public void AppendAnchor(int node, RigidBody body, Vector3 localPivot)
		{
			btSoftBody_appendAnchor(_native, node, body._native, ref localPivot);
		}

		public void AppendAnchor(int node, RigidBody body, Vector3 localPivot, bool disableCollisionBetweenLinkedBodies)
		{
			btSoftBody_appendAnchor2(_native, node, body._native, ref localPivot, disableCollisionBetweenLinkedBodies);
		}

		public void AppendAnchor(int node, RigidBody body, Vector3 localPivot, bool disableCollisionBetweenLinkedBodies, float influence)
		{
			btSoftBody_appendAnchor3(_native, node, body._native, ref localPivot, disableCollisionBetweenLinkedBodies, influence);
		}

		public void AppendAnchor(int node, RigidBody body)
		{
			btSoftBody_appendAnchor4(_native, node, body._native);
		}

		public void AppendAnchor(int node, RigidBody body, bool disableCollisionBetweenLinkedBodies)
		{
			btSoftBody_appendAnchor5(_native, node, body._native, disableCollisionBetweenLinkedBodies);
		}

		public void AppendAnchor(int node, RigidBody body, bool disableCollisionBetweenLinkedBodies, float influence)
		{
			btSoftBody_appendAnchor6(_native, node, body._native, disableCollisionBetweenLinkedBodies, influence);
		}

        private void StoreAngularJointControlRef(AngularJoint.Specs specs)
        {
            if (specs.Control != null && specs.Control != AngularJoint.IControl.Default)
            {
                _aJointControls.Add(specs.Control);
            }
        }

        public void AppendAngularJoint(AngularJoint.Specs specs)
		{
            StoreAngularJointControlRef(specs);
			btSoftBody_appendAngularJoint(_native, specs._native);
		}

        public void AppendAngularJoint(AngularJoint.Specs specs, Body body)
		{
            StoreAngularJointControlRef(specs);
			btSoftBody_appendAngularJoint2(_native, specs._native, body._native);
		}

        public void AppendAngularJoint(AngularJoint.Specs specs, SoftBody body)
		{
            StoreAngularJointControlRef(specs);
			btSoftBody_appendAngularJoint3(_native, specs._native, body._native);
		}

        public void AppendAngularJoint(AngularJoint.Specs specs, Cluster body0, Body body1)
		{
            StoreAngularJointControlRef(specs);
			btSoftBody_appendAngularJoint4(_native, specs._native, body0._native, body1._native);
		}

		public void AppendFace()
		{
			btSoftBody_appendFace(_native);
		}

		public void AppendFace(int model)
		{
			btSoftBody_appendFace2(_native, model);
		}

		public void AppendFace(int model, Material mat)
		{
			btSoftBody_appendFace3(_native, model, mat._native);
		}

		public void AppendFace(int node0, int node1, int node2)
		{
			btSoftBody_appendFace4(_native, node0, node1, node2);
		}

		public void AppendFace(int node0, int node1, int node2, Material mat)
		{
			btSoftBody_appendFace5(_native, node0, node1, node2, mat._native);
		}

		public void AppendLinearJoint(LinearJoint.Specs specs, SoftBody body)
		{
			btSoftBody_appendLinearJoint(_native, specs._native, body._native);
		}

        public void AppendLinearJoint(LinearJoint.Specs specs)
		{
			btSoftBody_appendLinearJoint2(_native, specs._native);
		}

        public void AppendLinearJoint(LinearJoint.Specs specs, Body body)
		{
			btSoftBody_appendLinearJoint3(_native, specs._native, body._native);
		}

        public void AppendLinearJoint(LinearJoint.Specs specs, Cluster body0, Body body1)
		{
			btSoftBody_appendLinearJoint4(_native, specs._native, body0._native, body1._native);
		}

		public void AppendLink(int node0, int node1)
		{
			btSoftBody_appendLink(_native, node0, node1);
		}

		public void AppendLink(int node0, int node1, Material mat)
		{
			btSoftBody_appendLink2(_native, node0, node1, (mat != null) ? mat._native : IntPtr.Zero);
		}

		public void AppendLink(int node0, int node1, Material mat, bool checkExist)
		{
            btSoftBody_appendLink3(_native, node0, node1, (mat != null) ? mat._native : IntPtr.Zero, checkExist);
		}

		public void AppendLink()
		{
			btSoftBody_appendLink4(_native);
		}

		public void AppendLink(int model)
		{
			btSoftBody_appendLink5(_native, model);
		}

		public void AppendLink(int model, Material mat)
		{
            btSoftBody_appendLink6(_native, model, (mat != null) ? mat._native : IntPtr.Zero);
		}

		public void AppendLink(Node node0, Node node1)
		{
			btSoftBody_appendLink7(_native, node0._native, node1._native);
		}

		public void AppendLink(Node node0, Node node1, Material mat)
		{
            btSoftBody_appendLink8(_native, node0._native, node1._native, (mat != null) ? mat._native : IntPtr.Zero);
		}

		public void AppendLink(Node node0, Node node1, Material mat, bool checkExist)
		{
            btSoftBody_appendLink9(_native, node0._native, node1._native, (mat != null) ? mat._native : IntPtr.Zero, checkExist);
		}

		public Material AppendMaterial()
		{
			return new Material(btSoftBody_appendMaterial(_native));
		}

		public void AppendNode(Vector3 x, float m)
		{
			btSoftBody_appendNode(_native, ref x, m);
		}

		public void AppendNote(string text, Vector3 o, Face feature)
		{
			btSoftBody_appendNote(_native, text, ref o, feature._native);
		}

        public void AppendNote(string text, Vector3 o, Link feature)
		{
			btSoftBody_appendNote2(_native, text, ref o, feature._native);
		}

        public void AppendNote(string text, Vector3 o, Node feature)
		{
			btSoftBody_appendNote3(_native, text, ref o, feature._native);
		}

        public void AppendNote(string text, Vector3 o)
		{
			btSoftBody_appendNote4(_native, text, ref o);
		}

        public void AppendNote(string text, Vector3 o, Vector4 c)
		{
			btSoftBody_appendNote5(_native, text, ref o, ref c);
		}

        public void AppendNote(string text, Vector3 o, Vector4 c, Node n0)
		{
			btSoftBody_appendNote6(_native, text, ref o, ref c, n0._native);
		}

        public void AppendNote(string text, Vector3 o, Vector4 c, Node n0, Node n1)
		{
			btSoftBody_appendNote7(_native, text, ref o, ref c, n0._native, n1._native);
		}

        public void AppendNote(string text, Vector3 o, Vector4 c, Node n0, Node n1, Node n2)
		{
			btSoftBody_appendNote8(_native, text, ref o, ref c, n0._native, n1._native, n2._native);
		}

        public void AppendNote(string text, Vector3 o, Vector4 c, Node n0, Node n1, Node n2, Node n3)
		{
			btSoftBody_appendNote9(_native, text, ref o, ref c, n0._native, n1._native, n2._native, n3._native);
		}

		public void AppendTetra(int model, Material mat)
		{
			btSoftBody_appendTetra(_native, model, mat._native);
		}

		public void AppendTetra(int node0, int node1, int node2, int node3)
		{
			btSoftBody_appendTetra2(_native, node0, node1, node2, node3);
		}

		public void AppendTetra(int node0, int node1, int node2, int node3, Material mat)
		{
			btSoftBody_appendTetra3(_native, node0, node1, node2, node3, mat._native);
		}

		public void ApplyClusters(bool drift)
		{
			btSoftBody_applyClusters(_native, drift);
		}

		public void ApplyForces()
		{
			btSoftBody_applyForces(_native);
		}

		public bool CheckContact(CollisionObjectWrapper colObjWrap, Vector3 x, float margin, sCti cti)
		{
			return btSoftBody_checkContact(_native, colObjWrap._native, ref x, margin, cti._native);
		}

		public bool CheckFace(int node0, int node1, int node2)
		{
			return btSoftBody_checkFace(_native, node0, node1, node2);
		}

		public bool CheckLink(Node node0, Node node1)
		{
			return btSoftBody_checkLink(_native, node0._native, node1._native);
		}

		public bool CheckLink(int node0, int node1)
		{
			return btSoftBody_checkLink2(_native, node0, node1);
		}

		public void CleanupClusters()
		{
            _aJointControls.Clear();
			btSoftBody_cleanupClusters(_native);
		}

		public static void ClusterAImpulse(Cluster cluster, Impulse impulse)
		{
			btSoftBody_clusterAImpulse(cluster._native, impulse._native);
		}

		public Vector3 ClusterCom(int cluster)
		{
			Vector3 value;
			btSoftBody_clusterCom(_native, cluster, out value);
			return value;
		}

		public static Vector3 ClusterCom(Cluster cluster)
		{
			Vector3 value;
			btSoftBody_clusterCom2(cluster._native, out value);
			return value;
		}

		public int ClusterCount()
		{
			return btSoftBody_clusterCount(_native);
		}

		public static void ClusterDAImpulse(Cluster cluster, Vector3 impulse)
		{
			btSoftBody_clusterDAImpulse(cluster._native, ref impulse);
		}

		public static void ClusterDCImpulse(Cluster cluster, Vector3 impulse)
		{
			btSoftBody_clusterDCImpulse(cluster._native, ref impulse);
		}

		public static void ClusterDImpulse(Cluster cluster, Vector3 rpos, Vector3 impulse)
		{
			btSoftBody_clusterDImpulse(cluster._native, ref rpos, ref impulse);
		}

		public static void ClusterImpulse(Cluster cluster, Vector3 rpos, Impulse impulse)
		{
			btSoftBody_clusterImpulse(cluster._native, ref rpos, impulse._native);
		}

		public static void ClusterVAImpulse(Cluster cluster, Vector3 impulse)
		{
			btSoftBody_clusterVAImpulse(cluster._native, ref impulse);
		}

		public static Vector3 ClusterVelocity(Cluster cluster, Vector3 rpos)
		{
			Vector3 value;
			btSoftBody_clusterVelocity(cluster._native, ref rpos, out value);
			return value;
		}

		public static void ClusterVImpulse(Cluster cluster, Vector3 rpos, Vector3 impulse)
		{
			btSoftBody_clusterVImpulse(cluster._native, ref rpos, ref impulse);
		}

		public bool CutLink(Node node0, Node node1, float position)
		{
			return btSoftBody_cutLink(_native, node0._native, node1._native, position);
		}

		public bool CutLink(int node0, int node1, float position)
		{
			return btSoftBody_cutLink2(_native, node0, node1, position);
		}

		public void DampClusters()
		{
			btSoftBody_dampClusters(_native);
		}

		public void DefaultCollisionHandler(CollisionObjectWrapper pcoWrap)
		{
			btSoftBody_defaultCollisionHandler(_native, pcoWrap._native);
		}

		public void DefaultCollisionHandler(SoftBody psb)
		{
			btSoftBody_defaultCollisionHandler2(_native, psb._native);
		}

		public Vector3 EvaluateCom()
		{
			Vector3 value;
			btSoftBody_evaluateCom(_native, out value);
			return value;
		}

		public int GenerateBendingConstraints(int distance)
		{
			return btSoftBody_generateBendingConstraints(_native, distance);
		}

		public int GenerateBendingConstraints(int distance, Material mat)
		{
			return btSoftBody_generateBendingConstraints2(_native, distance, mat._native);
		}

		public int GenerateClusters(int k)
		{
			return btSoftBody_generateClusters(_native, k);
		}

		public int GenerateClusters(int k, int maxIterations)
		{
			return btSoftBody_generateClusters2(_native, k, maxIterations);
		}

		public void GetAabb(out Vector3 aabbMin, out Vector3 aabbMax)
		{
			btSoftBody_getAabb(_native, out aabbMin, out aabbMax);
		}

		public float GetMass(int node)
		{
			return btSoftBody_getMass(_native, node);
		}
        /*
		public static psolver_t GetSolver(PositionSolver solver)
		{
			return btSoftBody_getSolver(solver._native);
		}

		public static vsolver_t GetSolver(VelocitySolver solver)
		{
			return btSoftBody_getSolver2(solver._native);
		}
        */
		public void IndicesToPointers()
		{
			btSoftBody_indicesToPointers(_native);
		}
        /*
		public void IndicesToPointers(int map)
		{
			btSoftBody_indicesToPointers2(_native, map._native);
		}
        */
		public void InitDefaults()
		{
			btSoftBody_initDefaults(_native);
		}

		public void InitializeClusters()
		{
			btSoftBody_initializeClusters(_native);
		}

		public void InitializeFaceTree()
		{
			btSoftBody_initializeFaceTree(_native);
		}

		public void IntegrateMotion()
		{
			btSoftBody_integrateMotion(_native);
		}

		public void PointersToIndices()
		{
			btSoftBody_pointersToIndices(_native);
		}

		public void PredictMotion(float deltaTime)
		{
			btSoftBody_predictMotion(_native, deltaTime);
		}

		public void PrepareClusters(int iterations)
		{
			btSoftBody_prepareClusters(_native, iterations);
		}

		public static void PSolveAnchors(SoftBody psb, float kst, float ti)
		{
			btSoftBody_PSolve_Anchors(psb._native, kst, ti);
		}

		public static void PSolveLinks(SoftBody psb, float kst, float ti)
		{
			btSoftBody_PSolve_Links(psb._native, kst, ti);
		}

		public static void PSolveRContacts(SoftBody psb, float kst, float ti)
		{
			btSoftBody_PSolve_RContacts(psb._native, kst, ti);
		}

		public static void PSolveSContacts(SoftBody psb, float __unnamed1, float ti)
		{
			btSoftBody_PSolve_SContacts(psb._native, __unnamed1, ti);
		}

		public void RandomizeConstraints()
		{
			btSoftBody_randomizeConstraints(_native);
		}

        public bool RayTest(ref Vector3 rayFrom, ref Vector3 rayTo, SRayCast results)
        {
            IntPtr rayCast = btSoftBody_sRayCast_new();
            bool ret = btSoftBody_rayTest(_native, ref rayFrom, ref rayTo, rayCast);
            results.Body = this;
            results.Feature = btSoftBody_sRayCast_getFeature(rayCast);
            results.Fraction = btSoftBody_sRayCast_getFraction(rayCast);
            results.Index = btSoftBody_sRayCast_getIndex(rayCast);
            btSoftBody_sRayCast_delete(rayCast);
            return ret;
        }

        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btSoftBody_sRayCast_new();
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern FeatureType btSoftBody_sRayCast_getFeature(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern float btSoftBody_sRayCast_getFraction(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern int btSoftBody_sRayCast_getIndex(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btSoftBody_sRayCast_delete(IntPtr obj);

        /*
		public int RayTest(Vector3 rayFrom, Vector3 rayTo, float mint, EFeature feature, int index, bool countOnly)
		{
			return btSoftBody_rayTest2(_native, ref rayFrom, ref rayTo, mint._native, feature._native, index._native, countOnly);
		}
        */
		public void Refine(ImplicitFn ifn, float accurary, bool cut)
		{
			btSoftBody_refine(_native, ifn._native, accurary, cut);
		}

		public void ReleaseCluster(int index)
		{
			btSoftBody_releaseCluster(_native, index);
		}

		public void ReleaseClusters()
		{
			btSoftBody_releaseClusters(_native);
		}

		public void ResetLinkRestLengths()
		{
			btSoftBody_resetLinkRestLengths(_native);
		}

		public void Rotate(Quaternion rot)
		{
			btSoftBody_rotate(_native, ref rot);
		}

		public void Scale(Vector3 scl)
		{
			btSoftBody_scale(_native, ref scl);
		}

		public void SetMass(int node, float mass)
		{
			btSoftBody_setMass(_native, node, mass);
		}

		public void SetPose(bool bvolume, bool bframe)
		{
			btSoftBody_setPose(_native, bvolume, bframe);
		}
        /*
		public void SetSolver(SolverPresets preset)
		{
			btSoftBody_setSolver(_native, preset._native);
		}
        */
		public void SetTotalDensity(float density)
		{
			btSoftBody_setTotalDensity(_native, density);
		}

		public void SetTotalMass(float mass, bool fromfaces)
		{
			btSoftBody_setTotalMass2(_native, mass, fromfaces);
		}

		public void SetVelocity(Vector3 velocity)
		{
			btSoftBody_setVelocity(_native, ref velocity);
		}

		public void SetVolumeDensity(float density)
		{
			btSoftBody_setVolumeDensity(_native, density);
		}

		public void SetVolumeMass(float mass)
		{
			btSoftBody_setVolumeMass(_native, mass);
		}

		public static void SolveClusters(AlignedSoftBodyArray bodies)
		{
			btSoftBody_solveClusters(bodies._native);
		}

		public void SolveClusters(float sor)
		{
			btSoftBody_solveClusters2(_native, sor);
		}

		public static void SolveCommonConstraints(SoftBody bodies, int count, int iterations)
		{
			btSoftBody_solveCommonConstraints(bodies._native, count, iterations);
		}

		public void SolveConstraints()
		{
			btSoftBody_solveConstraints(_native);
		}

		public void StaticSolve(int iterations)
		{
			btSoftBody_staticSolve(_native, iterations);
		}

		public void Transform(Matrix trs)
		{
			btSoftBody_transform(_native, ref trs);
		}

		public void Translate(Vector3 trs)
		{
			btSoftBody_translate(_native, ref trs);
		}

        public void Translate(float x, float y, float z)
        {
            Vector3 trs = new Vector3(x, y, z);
            btSoftBody_translate(_native, ref trs);
        }
        /*
		public static SoftBody Upcast(CollisionObject colObj)
		{
			return btSoftBody_upcast(colObj._native);
		}
        */
		public void UpdateArea()
		{
			btSoftBody_updateArea(_native);
		}

		public void UpdateArea(bool averageArea)
		{
			btSoftBody_updateArea2(_native, averageArea);
		}

		public void UpdateBounds()
		{
			btSoftBody_updateBounds(_native);
		}

		public void UpdateClusters()
		{
			btSoftBody_updateClusters(_native);
		}

		public void UpdateConstants()
		{
			btSoftBody_updateConstants(_native);
		}

		public void UpdateLinkConstants()
		{
			btSoftBody_updateLinkConstants(_native);
		}

		public void UpdateNormals()
		{
			btSoftBody_updateNormals(_native);
		}

		public void UpdatePose()
		{
			btSoftBody_updatePose(_native);
		}

		public static void VSolveLinks(SoftBody psb, float kst)
		{
			btSoftBody_VSolve_Links(psb._native, kst);
		}

        public int GetFaceVertexData(ref float[] vertices)
        {
            int floatCount = Faces.Count * 3 * 3;

            // Do not use Array.Resize, because it copies the old data
            if (vertices == null || vertices.Length != floatCount)
            {
                vertices = new float[floatCount];
            }

            return btSoftBody_getFaceVertexData(_native, vertices);
        }

        public int GetFaceVertexData(ref Vector3[] vertices)
        {
            int vertexCount = Faces.Count * 3;

            if (vertices == null || vertices.Length != vertexCount)
            {
                vertices = new Vector3[vertexCount];
            }

            return btSoftBody_getFaceVertexData(_native, vertices);
        }

        public int GetFaceVertexNormalData(ref Vector3[] vertices)
        {
            int vertexNormalCount = Faces.Count * 3 * 2;

            if (vertices == null || vertices.Length != vertexNormalCount)
            {
                vertices = new Vector3[vertexNormalCount];
            }

            return btSoftBody_getFaceVertexNormalData(_native, vertices);
        }

        public int GetFaceVertexNormalData(ref Vector3[] vertices, ref Vector3[] normals)
        {
            int vertexCount = Faces.Count * 3;

            if (vertices == null || vertices.Length != vertexCount)
            {
                vertices = new Vector3[vertexCount];
            }
            if (normals == null || normals.Length != vertexCount)
            {
                normals = new Vector3[vertexCount];
            }

            return btSoftBody_getFaceVertexNormalData2(_native, vertices, normals);
        }

		public int GetLinkVertexData(ref float[] vertices)
		{
			int vertexCount = Links.Count * 2 * 3;

			if (vertices == null || vertices.Length != vertexCount)
			{
				vertices = new float[vertexCount];
			}
			return btSoftBody_getLinkVertexData(_native, vertices);
		}

        public int GetLinkVertexData(ref Vector3[] vertices)
        {
            int vertexCount = Links.Count * 2;
            if (vertices == null || vertices.Length != vertexCount)
            {
                vertices = new Vector3[vertexCount];
            }

            return btSoftBody_getLinkVertexData(_native, vertices);
        }

        public int GetLinkVertexNormalData(ref Vector3[] vertices)
        {
            int vertexNormalCount = Links.Count * 2 * 2;

            if (vertices == null || vertices.Length != vertexNormalCount)
            {
                vertices = new Vector3[vertexNormalCount];
            }

            return btSoftBody_getLinkVertexNormalData(_native, vertices);
        }

        int GetTetraVertexData(ref Vector3[] vertices)
        {
            int vertexCount = Tetras.Count * 12;

            if (vertices == null || vertices.Length != vertexCount)
            {
                vertices = new Vector3[vertexCount];
            }

            return btSoftBody_getTetraVertexData(_native, vertices);
        }

        int GetTetraVertexNormalData(ref Vector3[] vertices)
        {
            int vertexNormalCount = Tetras.Count * 12 * 2;

            if (vertices == null || vertices.Length != vertexNormalCount)
            {
                vertices = new Vector3[vertexNormalCount];
            }

            return btSoftBody_getTetraVertexNormalData(_native, vertices);
        }

        int GetTetraVertexNormalData(ref Vector3[] vertices, ref Vector3[] normals)
        {
            int vertexCount = Tetras.Count * 12;

            if (vertices == null || vertices.Length != vertexCount)
            {
                vertices = new Vector3[vertexCount];
            }
            if (normals == null || normals.Length != vertexCount)
            {
                normals = new Vector3[vertexCount];
            }

            return btSoftBody_getTetraVertexNormalData2(_native, vertices, normals);
        }

        public int GetVertexNormalData(ref Vector3[] data)
        {
            if (Faces.Count != 0)
            {
                return GetFaceVertexNormalData(ref data);
            }
            else if (Tetras.Count != 0)
            {
                return GetTetraVertexNormalData(ref data);
            }
            return GetLinkVertexNormalData(ref data);
        }

        public int GetVertexNormalData(ref Vector3[] vertices, ref Vector3[] normals)
        {
            if (Faces.Count != 0)
            {
                return GetFaceVertexNormalData(ref vertices, ref normals);
            }
            else if (Tetras.Count != 0)
            {
                return GetTetraVertexNormalData(ref vertices, ref normals);
            }
            normals = null;
            return GetLinkVertexData(ref vertices);
        }

		public AlignedAnchorArray Anchors
		{
            get
            {
                if (_anchors == null)
                {
                    _anchors = new AlignedAnchorArray(btSoftBody_getAnchors(_native));
                }
                return _anchors;
            }
		}

		public Vector3Array Bounds
		{
			get
			{
                if (_bounds == null)
                {
                    _bounds = new Vector3Array(btSoftBody_getBounds(_native), 2);
                }
                return _bounds;
			}
		}

		public Dbvt ClusterDbvt
		{
            get
            {
                if (_clusterDbvt == null)
                {
                    _clusterDbvt = new Dbvt(btSoftBody_getCdbvt(_native), true);
                }
                return _clusterDbvt;
            }
		}

		public Config Cfg
		{
            get
            {
                if (_config == null)
                {
                    _config = new Config(btSoftBody_getCfg(_native));
                }
                return _config;
            }
		}
        /*
		public AlignedObjectArray ClusterConnectivity
		{
			get { return btSoftBody_getClusterConnectivity(_native); }
			set { btSoftBody_setClusterConnectivity(_native, value._native); }
		}
        */
		public AlignedClusterArray Clusters
		{
            get
            {
                if (_clusters == null)
                {
                    _clusters = new AlignedClusterArray(btSoftBody_getClusters(_native));
                }
                return _clusters;
            }
		}
        /*
		public AlignedObjectArray<CollisionObject> CollisionDisabledObjects
		{
			get { return btSoftBody_getCollisionDisabledObjects(_native); }
			set { btSoftBody_setCollisionDisabledObjects(_native, value._native); }
		}
        */
		public AlignedFaceArray Faces
		{
            get
            {
                if (_faces == null)
                {
                    _faces = new AlignedFaceArray(btSoftBody_getFaces(_native));
                }
                return _faces;
            }
		}

		public Dbvt FaceDbvt
		{
            get
            {
                if (_faceDbvt == null)
                {
                    _faceDbvt = new Dbvt(btSoftBody_getFdbvt(_native), true);
                }
                return _faceDbvt;
            }
		}

		public Matrix InitialWorldTransform
		{
			get
			{
				Matrix value;
				btSoftBody_getInitialWorldTransform(_native, out value);
				return value;
			}
			set { btSoftBody_setInitialWorldTransform(_native, ref value); }
		}

		public AlignedJointArray Joints
		{
            get
            {
                if (_joints == null)
                {
                    _joints = new AlignedJointArray(btSoftBody_getJoints(_native));
                }
                return _joints;
            }
		}

		public AlignedLinkArray Links
		{
            get
            {
                if (_links == null)
                {
                    _links = new AlignedLinkArray(btSoftBody_getLinks(_native));
                }
                return _links;
            }
		}

		public AlignedMaterialArray Materials
		{
            get
            {
                if (_materials == null)
                {
                    _materials = new AlignedMaterialArray(btSoftBody_getMaterials(_native), true);
                }
                return _materials;
            }
		}

		public Dbvt NodeDbvt
		{
            get
            {
                if (_nodeDbvt == null)
                {
                    _nodeDbvt = new Dbvt(btSoftBody_getNdbvt(_native), true);
                }
                return _nodeDbvt;
            }
		}

		public AlignedNodeArray Nodes
		{
            get
            {
                if (_nodes == null)
                {
                    _nodes = new AlignedNodeArray(btSoftBody_getNodes(_native));
                }
                return _nodes;
            }
		}
        /*
		public tNoteArray Notes
		{
			get { return btSoftBody_getNotes(_native); }
			set { btSoftBody_setNotes(_native, value._native); }
		}
        */
		public Pose Pose
		{
            get
            {
                if (_pose == null)
                {
                    _pose = new Pose(btSoftBody_getPose(_native));
                }
                return _pose;
            }
		}
        /*
		public tRContactArray Rcontacts
		{
			get { return btSoftBody_getRcontacts(_native); }
			set { btSoftBody_setRcontacts(_native, value._native); }
		}
        */
		public float RestLengthScale
		{
			get { return btSoftBody_getRestLengthScale(_native); }
			set { btSoftBody_setRestLengthScale(_native, value); }
		}
        /*
		public tSContactArray Scontacts
		{
			get { return btSoftBody_getScontacts(_native); }
			set { btSoftBody_setScontacts(_native, value._native); }
		}
        */
		public SoftBodySolver SoftBodySolver
		{
			get { return _softBodySolver; }
			set
			{
                btSoftBody_setSoftBodySolver(_native, (value != null) ? value._native : IntPtr.Zero);
				_softBodySolver = value;
			}
		}

		public SolverState SolverState
		{
            get
            {
                if (_solverState == null)
                {
                    _solverState = new SolverState(btSoftBody_getSst(_native));
                }
                return _solverState;
            }
		}

        public Object Tag { get; set; }

        public AlignedTetraArray Tetras
		{
            get
            {
                if (_tetras == null)
                {
                    _tetras = new AlignedTetraArray(btSoftBody_getTetras(_native));
                }
                return _tetras;
            }
		}

		public float Timeacc
		{
			get { return btSoftBody_getTimeacc(_native); }
			set { btSoftBody_setTimeacc(_native, value); }
		}

		public float TotalMass
		{
			get { return btSoftBody_getTotalMass(_native); }
            set { btSoftBody_setTotalMass(_native, value); }
		}

        public bool UpdateRuntimeConstants
        {
            get { return btSoftBody_getBUpdateRtCst(_native); }
            set { btSoftBody_setBUpdateRtCst(_native, value); }
        }
        /*
		public AlignedObjectArray UserIndexMapping
		{
			get { return btSoftBody_getUserIndexMapping(_native); }
			set { btSoftBody_setUserIndexMapping(_native, value._native); }
		}
        */
		public Vector3 WindVelocity
		{
			get
			{
				Vector3 value;
				btSoftBody_getWindVelocity(_native, out value);
				return value;
			}
			set { btSoftBody_setWindVelocity(_native, ref value); }
		}

		public float Volume
		{
			get { return btSoftBody_getVolume(_native); }
		}

		public SoftBodyWorldInfo WorldInfo
		{
			get { return _worldInfo; }
			set
			{
                btSoftBody_setWorldInfo(_native, value._native);
				_worldInfo = value;
			}
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBody_new(IntPtr worldInfo, int node_count, [In] Vector3[] x, [In] float[] m);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBody_new2(IntPtr worldInfo);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_addAeroForceToFace(IntPtr obj, [In] ref Vector3 windVelocity, int faceIndex);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_addAeroForceToNode(IntPtr obj, [In] ref Vector3 windVelocity, int nodeIndex);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_addForce(IntPtr obj, [In] ref Vector3 force);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_addForce2(IntPtr obj, [In] ref Vector3 force, int node);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_addVelocity(IntPtr obj, [In] ref Vector3 velocity, int node);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_addVelocity2(IntPtr obj, [In] ref Vector3 velocity);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_appendAnchor(IntPtr obj, int node, IntPtr body, [In] ref Vector3 localPivot);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_appendAnchor2(IntPtr obj, int node, IntPtr body, [In] ref Vector3 localPivot, bool disableCollisionBetweenLinkedBodies);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_appendAnchor3(IntPtr obj, int node, IntPtr body, [In] ref Vector3 localPivot, bool disableCollisionBetweenLinkedBodies, float influence);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_appendAnchor4(IntPtr obj, int node, IntPtr body);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_appendAnchor5(IntPtr obj, int node, IntPtr body, bool disableCollisionBetweenLinkedBodies);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_appendAnchor6(IntPtr obj, int node, IntPtr body, bool disableCollisionBetweenLinkedBodies, float influence);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_appendAngularJoint(IntPtr obj, IntPtr specs);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btSoftBody_appendAngularJoint2(IntPtr obj, IntPtr specs, IntPtr body);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_appendAngularJoint3(IntPtr obj, IntPtr specs, IntPtr body);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_appendAngularJoint4(IntPtr obj, IntPtr specs, IntPtr body0, IntPtr body1);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_appendFace(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_appendFace2(IntPtr obj, int model);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_appendFace3(IntPtr obj, int model, IntPtr mat);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_appendFace4(IntPtr obj, int node0, int node1, int node2);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_appendFace5(IntPtr obj, int node0, int node1, int node2, IntPtr mat);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_appendLinearJoint(IntPtr obj, IntPtr specs, IntPtr body);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_appendLinearJoint2(IntPtr obj, IntPtr specs);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_appendLinearJoint3(IntPtr obj, IntPtr specs, IntPtr body);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btSoftBody_appendLinearJoint4(IntPtr obj, IntPtr specs, IntPtr body0, IntPtr body1);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_appendLink(IntPtr obj, int node0, int node1);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_appendLink2(IntPtr obj, int node0, int node1, IntPtr mat);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_appendLink3(IntPtr obj, int node0, int node1, IntPtr mat, bool bcheckexist);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_appendLink4(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_appendLink5(IntPtr obj, int model);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_appendLink6(IntPtr obj, int model, IntPtr mat);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_appendLink7(IntPtr obj, IntPtr node0, IntPtr node1);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_appendLink8(IntPtr obj, IntPtr node0, IntPtr node1, IntPtr mat);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_appendLink9(IntPtr obj, IntPtr node0, IntPtr node1, IntPtr mat, bool bcheckexist);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBody_appendMaterial(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_appendNode(IntPtr obj, [In] ref Vector3 x, float m);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btSoftBody_appendNote(IntPtr obj, string text, [In] ref Vector3 o, IntPtr feature);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btSoftBody_appendNote2(IntPtr obj, string text, [In] ref Vector3 o, IntPtr feature);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btSoftBody_appendNote3(IntPtr obj, string text, [In] ref Vector3 o, IntPtr feature);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btSoftBody_appendNote4(IntPtr obj, string text, [In] ref Vector3 o);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btSoftBody_appendNote5(IntPtr obj, string text, [In] ref Vector3 o, [In] ref Vector4 c);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btSoftBody_appendNote6(IntPtr obj, string text, [In] ref Vector3 o, [In] ref Vector4 c, IntPtr n0);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btSoftBody_appendNote7(IntPtr obj, string text, [In] ref Vector3 o, [In] ref Vector4 c, IntPtr n0, IntPtr n1);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btSoftBody_appendNote8(IntPtr obj, string text, [In] ref Vector3 o, [In] ref Vector4 c, IntPtr n0, IntPtr n1, IntPtr n2);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btSoftBody_appendNote9(IntPtr obj, string text, [In] ref Vector3 o, [In] ref Vector4 c, IntPtr n0, IntPtr n1, IntPtr n2, IntPtr n3);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_appendTetra(IntPtr obj, int model, IntPtr mat);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_appendTetra2(IntPtr obj, int node0, int node1, int node2, int node3);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_appendTetra3(IntPtr obj, int node0, int node1, int node2, int node3, IntPtr mat);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_applyClusters(IntPtr obj, bool drift);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_applyForces(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btSoftBody_checkContact(IntPtr obj, IntPtr colObjWrap, [In] ref Vector3 x, float margin, IntPtr cti);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btSoftBody_checkFace(IntPtr obj, int node0, int node1, int node2);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btSoftBody_checkLink(IntPtr obj, IntPtr node0, IntPtr node1);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btSoftBody_checkLink2(IntPtr obj, int node0, int node1);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_cleanupClusters(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_clusterAImpulse(IntPtr cluster, IntPtr impulse);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_clusterCom(IntPtr obj, int cluster, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_clusterCom2(IntPtr cluster, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btSoftBody_clusterCount(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_clusterDAImpulse(IntPtr cluster, [In] ref Vector3 impulse);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_clusterDCImpulse(IntPtr cluster, [In] ref Vector3 impulse);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_clusterDImpulse(IntPtr cluster, [In] ref Vector3 rpos, [In] ref Vector3 impulse);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_clusterImpulse(IntPtr cluster, [In] ref Vector3 rpos, IntPtr impulse);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_clusterVAImpulse(IntPtr cluster, [In] ref Vector3 impulse);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_clusterVelocity(IntPtr cluster, [In] ref Vector3 rpos, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_clusterVImpulse(IntPtr cluster, [In] ref Vector3 rpos, [In] ref Vector3 impulse);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btSoftBody_cutLink(IntPtr obj, IntPtr node0, IntPtr node1, float position);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btSoftBody_cutLink2(IntPtr obj, int node0, int node1, float position);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_dampClusters(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_defaultCollisionHandler(IntPtr obj, IntPtr pcoWrap);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_defaultCollisionHandler2(IntPtr obj, IntPtr psb);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_evaluateCom(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btSoftBody_generateBendingConstraints(IntPtr obj, int distance);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btSoftBody_generateBendingConstraints2(IntPtr obj, int distance, IntPtr mat);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btSoftBody_generateClusters(IntPtr obj, int k);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btSoftBody_generateClusters2(IntPtr obj, int k, int maxiterations);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_getAabb(IntPtr obj, [Out] out Vector3 aabbMin, [Out] out Vector3 aabbMax);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBody_getAnchors(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBody_getBounds(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btSoftBody_getBUpdateRtCst(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBody_getCdbvt(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBody_getCfg(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_getClusterConnectivity(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBody_getClusters(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_getCollisionDisabledObjects(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBody_getFaces(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btSoftBody_getFdbvt(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_getInitialWorldTransform(IntPtr obj, [Out] out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBody_getJoints(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBody_getLinks(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btSoftBody_getMass(IntPtr obj, int node);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBody_getMaterials(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btSoftBody_getNdbvt(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBody_getNodes(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBody_getNotes(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBody_getPose(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBody_getRcontacts(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btSoftBody_getRestLengthScale(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBody_getScontacts(IntPtr obj);
		//[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		//static extern IntPtr btSoftBody_getSolver(_ solver);
		//[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		//static extern IntPtr btSoftBody_getSolver2(_ solver);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBody_getSst(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBody_getTag(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBody_getTetras(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btSoftBody_getTimeacc(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btSoftBody_getTotalMass(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_getUserIndexMapping(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_getWindVelocity(IntPtr obj, [Out] out Vector3 velocity);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btSoftBody_getVolume(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBody_getWorldInfo(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_indicesToPointers(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_indicesToPointers2(IntPtr obj, IntPtr map);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_initDefaults(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_initializeClusters(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_initializeFaceTree(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_integrateMotion(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_pointersToIndices(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_predictMotion(IntPtr obj, float dt);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_prepareClusters(IntPtr obj, int iterations);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_PSolve_Anchors(IntPtr psb, float kst, float ti);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_PSolve_Links(IntPtr psb, float kst, float ti);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_PSolve_RContacts(IntPtr psb, float kst, float ti);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_PSolve_SContacts(IntPtr psb, float __unnamed1, float ti);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_randomizeConstraints(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btSoftBody_rayTest(IntPtr obj, [In] ref Vector3 rayFrom, [In] ref Vector3 rayTo, IntPtr results);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btSoftBody_rayTest2(IntPtr obj, [In] ref Vector3 rayFrom, [In] ref Vector3 rayTo, IntPtr mint, IntPtr feature, IntPtr index, bool bcountonly);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_refine(IntPtr obj, IntPtr ifn, float accurary, bool cut);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_releaseCluster(IntPtr obj, int index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_releaseClusters(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_resetLinkRestLengths(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_rotate(IntPtr obj, [In] ref Quaternion rot);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_scale(IntPtr obj, [In] ref Vector3 scl);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_setBUpdateRtCst(IntPtr obj, bool value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_setInitialWorldTransform(IntPtr obj, [In] ref Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_setMass(IntPtr obj, int node, float mass);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_setPose(IntPtr obj, bool bvolume, bool bframe);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_setRestLengthScale(IntPtr obj, float restLength);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_setSoftBodySolver(IntPtr obj, IntPtr softBodySolver);
		//[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		//static extern void btSoftBody_setSolver(IntPtr obj, btSoftBody::eSolverPresets::_ preset);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_setTag(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_setTimeacc(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_setTotalDensity(IntPtr obj, float density);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_setTotalMass(IntPtr obj, float mass);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_setTotalMass2(IntPtr obj, float mass, bool fromfaces);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_setVelocity(IntPtr obj, [In] ref Vector3 velocity);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_setWindVelocity(IntPtr obj, [In] ref Vector3 velocity);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_setVolumeDensity(IntPtr obj, float density);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_setVolumeMass(IntPtr obj, float mass);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_setWorldInfo(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_solveClusters(IntPtr bodies);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_solveClusters2(IntPtr obj, float sor);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_solveCommonConstraints(IntPtr bodies, int count, int iterations);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_solveConstraints(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_staticSolve(IntPtr obj, int iterations);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_transform(IntPtr obj, [In] ref Matrix trs);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_translate(IntPtr obj, [In] ref Vector3 trs);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBody_upcast(IntPtr colObj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_updateArea(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_updateArea2(IntPtr obj, bool averageArea);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_updateBounds(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_updateClusters(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_updateConstants(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_updateLinkConstants(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_updateNormals(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_updatePose(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBody_VSolve_Links(IntPtr psb, float kst);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern int btSoftBody_getFaceVertexData(IntPtr obj, [In,Out] float[] vertices);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern int btSoftBody_getFaceVertexData(IntPtr obj, [In,Out] Vector3[] vertices);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        public static extern int btSoftBody_getFaceVertexNormalData(IntPtr obj, [In,Out] float[] data);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern int btSoftBody_getFaceVertexNormalData2(IntPtr obj, [In,Out] float[] vertices, [In,Out] float[] normals);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        public static extern int btSoftBody_getFaceVertexNormalData(IntPtr obj, [In,Out] Vector3[] data);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern int btSoftBody_getFaceVertexNormalData2(IntPtr obj, [In,Out] Vector3[] vertices, [In,Out] Vector3[] normals);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern int btSoftBody_getLinkVertexData(IntPtr obj, [In,Out] float[] vertices);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern int btSoftBody_getLinkVertexData(IntPtr obj, [In,Out] Vector3[] vertices);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern int btSoftBody_getLinkVertexNormalData(IntPtr obj, [In,Out] float[] data);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern int btSoftBody_getLinkVertexNormalData(IntPtr obj, [In,Out] Vector3[] data);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern int btSoftBody_getTetraVertexData(IntPtr obj, [In,Out] float[] vertices);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern int btSoftBody_getTetraVertexData(IntPtr obj, [In,Out] Vector3[] vertices);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern int btSoftBody_getTetraVertexNormalData(IntPtr obj, [In,Out] float[] data);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern int btSoftBody_getTetraVertexNormalData2(IntPtr obj, [In,Out] float[] vectors, [In,Out] float[] normals);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern int btSoftBody_getTetraVertexNormalData(IntPtr obj, [In,Out] Vector3[] value);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern int btSoftBody_getTetraVertexNormalData2(IntPtr obj, [In,Out] Vector3[] vectors, [In,Out] Vector3[] normals);

        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btCollisionObject_getCollisionShape(IntPtr obj);
	}
}
