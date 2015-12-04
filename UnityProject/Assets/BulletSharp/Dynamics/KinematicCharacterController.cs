using BulletSharp.Math;

namespace BulletSharp
{
    public class KinematicCharacterController : ICharacterController
    {
        protected Vector3 ComputeReflectionDirection(ref Vector3 direction, ref Vector3 normal)
        {
            float dot;
            Vector3.Dot(ref direction, ref normal, out dot);
            return direction - (2.0f * dot) * normal;
        }

        protected Vector3 ParallelComponent(ref Vector3 direction, ref Vector3 normal)
        {
            float magnitude;
            Vector3.Dot(ref direction, ref normal, out magnitude);
            return normal * magnitude;
        }

        protected Vector3 PerpindicularComponent(ref Vector3 direction, ref Vector3 normal)
        {
            return direction - ParallelComponent(ref direction, ref normal);
        }

        protected bool RecoverFromPenetration(CollisionWorld collisionWorld)
        {
            Vector3 minAabb, maxAabb;
            m_convexShape.GetAabb(m_ghostObject.WorldTransform, out minAabb, out maxAabb);
            collisionWorld.Broadphase.SetAabbRef(m_ghostObject.BroadphaseHandle,
                         ref minAabb,
                         ref maxAabb,
                         collisionWorld.Dispatcher);

            bool penetration = false;

            collisionWorld.Dispatcher.DispatchAllCollisionPairs(m_ghostObject.OverlappingPairCache, collisionWorld.DispatchInfo, collisionWorld.Dispatcher);

            m_currentPosition = m_ghostObject.WorldTransform.Origin;

            float maxPen = 0f;
            for (int i = 0; i < m_ghostObject.OverlappingPairCache.NumOverlappingPairs; i++)
            {
                m_manifoldArray.Clear();

                BroadphasePair collisionPair = m_ghostObject.OverlappingPairCache.OverlappingPairArray[i];

                CollisionObject obj0 = collisionPair.Proxy0.ClientObject as CollisionObject;
                CollisionObject obj1 = collisionPair.Proxy1.ClientObject as CollisionObject;

                if ((obj0 != null && !obj0.HasContactResponse) || (obj1 != null && !obj1.HasContactResponse))
                    continue;

                if (collisionPair.Algorithm != null)
                {
                    collisionPair.Algorithm.GetAllContactManifolds(m_manifoldArray);
                }

                for (int j = 0; j < m_manifoldArray.Count; j++)
                {
                    PersistentManifold manifold = m_manifoldArray[j];
                    float directionSign = manifold.Body0 == m_ghostObject ? -1f : 1f;
                    for (int p = 0; p < manifold.NumContacts; p++)
                    {
                        ManifoldPoint pt = manifold.GetContactPoint(p);

                        float dist = pt.Distance;

                        if (dist < 0.0f)
                        {
                            if (dist < maxPen)
                            {
                                maxPen = dist;
                                m_touchingNormal = pt.NormalWorldOnB * directionSign;//??

                            }
                            m_currentPosition += pt.NormalWorldOnB * directionSign * dist * 0.2f;
                            penetration = true;
                        }
                        else
                        {
                            //printf("touching %f\n", dist);
                        }
                    }

                    //manifold.ClearManifold();
                }
            }
            Matrix newTrans = m_ghostObject.WorldTransform;
            newTrans.Origin = m_currentPosition;
            m_ghostObject.WorldTransform = newTrans;
            //	printf("m_touchingNormal = %f,%f,%f\n",m_touchingNormal[0],m_touchingNormal[1],m_touchingNormal[2]);
            return penetration;
        }

        protected void StepUp(CollisionWorld collisionWorld)
        {
            // phase 1: up
            Matrix start, end;
            m_targetPosition = m_currentPosition + upAxisDirection[m_upAxis] * (m_stepHeight + (m_verticalOffset > 0.0f ? m_verticalOffset : 0.0f));

            /* FIXME: Handle penetration properly */
            start = Matrix.Translation(m_currentPosition + upAxisDirection[m_upAxis] * (m_convexShape.Margin + m_addedMargin));
            end = Matrix.Translation(m_targetPosition);

            KinematicClosestNotMeConvexResultCallback callback = new KinematicClosestNotMeConvexResultCallback(m_ghostObject, -upAxisDirection[m_upAxis], 0.7071f);
            callback.CollisionFilterGroup = GhostObject.BroadphaseHandle.CollisionFilterGroup;
            callback.CollisionFilterMask = GhostObject.BroadphaseHandle.CollisionFilterMask;

            if (m_useGhostObjectSweepTest)
            {
                m_ghostObject.ConvexSweepTestRef(m_convexShape, ref start, ref end, callback, collisionWorld.DispatchInfo.AllowedCcdPenetration);
            }
            else
            {
                collisionWorld.ConvexSweepTestRef(m_convexShape, ref start, ref end, callback, 0f);
            }

            if (callback.HasHit)
            {
                // Only modify the position if the hit was a slope and not a wall or ceiling.
                if (Vector3.Dot(callback.HitNormalWorld, upAxisDirection[m_upAxis]) > 0.0)
                {
                    // we moved up only a fraction of the step height
                    m_currentStepOffset = m_stepHeight * callback.ClosestHitFraction;
                    if (m_interpolateUp)
                    {
                        Vector3.Lerp(ref m_currentPosition, ref m_targetPosition, callback.ClosestHitFraction, out m_currentPosition);
                    }
                    else
                    {
                        m_currentPosition = m_targetPosition;
                    }
                }
                m_verticalVelocity = 0.0f;
                m_verticalOffset = 0.0f;
            }
            else
            {
                m_currentStepOffset = m_stepHeight;
                m_currentPosition = m_targetPosition;
            }

        }
        protected void UpdateTargetPositionBasedOnCollision(ref Vector3 hitNormal, float tangentMag, float normalMag)
        {
            Vector3 movementDirection = m_targetPosition - m_currentPosition;
            float movementLength = movementDirection.Length;
            if (movementLength > MathUtil.SIMD_EPSILON)
            {
                movementDirection.Normalize();

                Vector3 reflectDir = ComputeReflectionDirection(ref movementDirection, ref hitNormal);
                reflectDir.Normalize();

                Vector3 parallelDir, perpindicularDir;

                parallelDir = ParallelComponent(ref reflectDir, ref hitNormal);
                perpindicularDir = PerpindicularComponent(ref reflectDir, ref hitNormal);

                m_targetPosition = m_currentPosition;
                /*
                if (tangentMag != 0.0)
                {
                    Vector3 parComponent = parallelDir * (tangentMag * movementLength);
                    //			printf("parComponent=%f,%f,%f\n",parComponent[0],parComponent[1],parComponent[2]);
                    m_targetPosition += parComponent;
                }
                */
                if (normalMag != 0.0f)
                {
                    Vector3 perpComponent = perpindicularDir * (normalMag * movementLength);
                    //			printf("perpComponent=%f,%f,%f\n",perpComponent[0],perpComponent[1],perpComponent[2]);
                    m_targetPosition += perpComponent;
                }
            }
            else
            {
                //		printf("movementLength don't normalize a zero vector\n");
            }
        }

        protected void StepForwardAndStrafe(CollisionWorld collisionWorld, ref Vector3 walkMove)
        {
            //	printf("originalDir=%f,%f,%f\n",originalDir[0],originalDir[1],originalDir[2]);
            // phase 2: forward and strafe
            Matrix start = Matrix.Identity, end = Matrix.Identity;
            m_targetPosition = m_currentPosition + walkMove;

            float fraction = 1.0f;
            float distance2 = (m_currentPosition - m_targetPosition).LengthSquared;
            //	printf("distance2=%f\n",distance2);

            if (m_touchingContact)
            {
                float dot;
                Vector3.Dot(ref m_normalizedDirection, ref m_touchingNormal, out dot);
                if (dot > 0.0f)
                {
                    //interferes with step movement
                    //UpdateTargetPositionBasedOnCollision(ref m_touchingNormal, 0.0f, 1.0f);
                }
            }

            int maxIter = 10;

            while (fraction > 0.01f && maxIter-- > 0)
            {
                start.Origin = (m_currentPosition);
                end.Origin = (m_targetPosition);

                Vector3 sweepDirNegative = m_currentPosition - m_targetPosition;

                KinematicClosestNotMeConvexResultCallback callback = new KinematicClosestNotMeConvexResultCallback(m_ghostObject, sweepDirNegative, 0f);
                callback.CollisionFilterGroup = GhostObject.BroadphaseHandle.CollisionFilterGroup;
                callback.CollisionFilterMask = GhostObject.BroadphaseHandle.CollisionFilterMask;


                float margin = m_convexShape.Margin;
                m_convexShape.Margin = margin + m_addedMargin;


                if (m_useGhostObjectSweepTest)
                {
                    m_ghostObject.ConvexSweepTestRef(m_convexShape, ref start, ref end, callback, collisionWorld.DispatchInfo.AllowedCcdPenetration);
                }
                else
                {
                    collisionWorld.ConvexSweepTestRef(m_convexShape, ref start, ref end, callback, collisionWorld.DispatchInfo.AllowedCcdPenetration);
                }

                m_convexShape.Margin = margin;


                fraction -= callback.ClosestHitFraction;

                if (callback.HasHit)
                {
                    // we moved only a fraction
                    float hitDistance = (callback.HitPointWorld - m_currentPosition).Length;

                    Vector3 hitNormalWorld = callback.HitNormalWorld;
                    UpdateTargetPositionBasedOnCollision(ref hitNormalWorld, 0f, 1f);
                    Vector3 currentDir = m_targetPosition - m_currentPosition;
                    distance2 = currentDir.LengthSquared;
                    if (distance2 > MathUtil.SIMD_EPSILON)
                    {
                        currentDir.Normalize();
                        /* See Quake2: "If velocity is against original velocity, stop ead to avoid tiny oscilations in sloping corners." */
                        float dot;
                        Vector3.Dot(ref currentDir, ref m_normalizedDirection, out dot);
                        if (dot <= 0.0f)
                        {
                            break;
                        }
                    }
                    else
                    {
                        //				printf("currentDir: don't normalize a zero vector\n");
                        break;
                    }
                }
                else
                {
                    // we moved whole way
                    m_currentPosition = m_targetPosition;
                }

                //	if (callback.m_closestHitFraction == 0.f)
                //		break;

            }

        }
        protected void StepDown(CollisionWorld collisionWorld, float dt)
        {
            Matrix start, end, end_double;
            bool runonce = false;

            // phase 3: down
            /*float additionalDownStep = (m_wasOnGround && !onGround()) ? m_stepHeight : 0.0;
            btVector3 step_drop = getUpAxisDirections()[m_upAxis] * (m_currentStepOffset + additionalDownStep);
            float downVelocity = (additionalDownStep == 0.0 && m_verticalVelocity<0.0?-m_verticalVelocity:0.0) * dt;
            btVector3 gravity_drop = getUpAxisDirections()[m_upAxis] * downVelocity; 
            m_targetPosition -= (step_drop + gravity_drop);*/

            Vector3 orig_position = m_targetPosition;

            float downVelocity = (m_verticalVelocity < 0.0f ? -m_verticalVelocity : 0.0f) * dt;
            if (downVelocity > 0.0 && downVelocity > m_fallSpeed
                && (m_wasOnGround || !m_wasJumping))
            {
                downVelocity = m_fallSpeed;
            }

            Vector3 step_drop = upAxisDirection[m_upAxis] * (m_currentStepOffset + downVelocity);
            m_targetPosition -= step_drop;

            KinematicClosestNotMeConvexResultCallback callback = new KinematicClosestNotMeConvexResultCallback(m_ghostObject, upAxisDirection[m_upAxis], m_maxSlopeCosine);
            callback.CollisionFilterGroup = GhostObject.BroadphaseHandle.CollisionFilterGroup;
            callback.CollisionFilterMask = GhostObject.BroadphaseHandle.CollisionFilterMask;

            KinematicClosestNotMeConvexResultCallback callback2 = new KinematicClosestNotMeConvexResultCallback(m_ghostObject, upAxisDirection[m_upAxis], m_maxSlopeCosine);
            callback2.CollisionFilterGroup = GhostObject.BroadphaseHandle.CollisionFilterGroup;
            callback2.CollisionFilterMask = GhostObject.BroadphaseHandle.CollisionFilterMask;

            while (true)
            {
                start = Matrix.Translation(m_currentPosition);
                end = Matrix.Translation(m_targetPosition);

                //set double test for 2x the step drop, to check for a large drop vs small drop
                end_double = Matrix.Translation(m_targetPosition - step_drop);

                if (m_useGhostObjectSweepTest)
                {
                    m_ghostObject.ConvexSweepTestRef(m_convexShape, ref start, ref end, callback, collisionWorld.DispatchInfo.AllowedCcdPenetration);

                    if (!callback.HasHit)
                    {
                        //test a double fall height, to see if the character should interpolate it's fall (full) or not (partial)
                        m_ghostObject.ConvexSweepTest(m_convexShape, start, end_double, callback2, collisionWorld.DispatchInfo.AllowedCcdPenetration);
                    }
                }
                else
                {
                    // this works....
                    collisionWorld.ConvexSweepTestRef(m_convexShape, ref start, ref end, callback, collisionWorld.DispatchInfo.AllowedCcdPenetration);

                    if (!callback.HasHit)
                    {
                        //test a double fall height, to see if the character should interpolate it's fall (large) or not (small)
                        m_ghostObject.ConvexSweepTest(m_convexShape, start, end_double, callback2, collisionWorld.DispatchInfo.AllowedCcdPenetration);
                    }
                }

                float downVelocity2 = (m_verticalVelocity < 0.0f ? -m_verticalVelocity : 0.0f) * dt;
                bool has_hit = false;
                if (bounce_fix == true)
                    has_hit = callback.HasHit || callback2.HasHit;
                else
                    has_hit = callback2.HasHit;

                if (downVelocity2 > 0.0f && downVelocity2 < m_stepHeight && has_hit == true && runonce == false
                            && (m_wasOnGround || !m_wasJumping))
                {
                    //redo the velocity calculation when falling a small amount, for fast stairs motion
                    //for larger falls, use the smoother/slower interpolated movement by not touching the target position

                    m_targetPosition = orig_position;
                    downVelocity = m_stepHeight;

                    Vector3 step_drop2 = upAxisDirection[m_upAxis] * (m_currentStepOffset + downVelocity);
                    m_targetPosition -= step_drop2;
                    runonce = true;
                    continue; //re-run previous tests
                }
                break;
            }

            if (callback.HasHit || runonce == true)
            {
                // we dropped a fraction of the height -> hit floor
                float fraction = (m_currentPosition.Y - callback.HitPointWorld.Y) / 2;

                //printf("hitpoint: %g - pos %g\n", callback.m_hitPointWorld.getY(), m_currentPosition.getY());

                if (bounce_fix == true)
                {
                    if (full_drop == true)
                    {
                        Vector3.Lerp(ref m_currentPosition, ref m_targetPosition, callback.ClosestHitFraction, out m_currentPosition);
                    }
                    else
                    {
                        //due to errors in the closestHitFraction variable when used with large polygons, calculate the hit fraction manually
                        Vector3.Lerp(ref m_currentPosition, ref m_targetPosition, fraction, out m_currentPosition);
                    }
                }
                else
                {
                    Vector3.Lerp(ref m_currentPosition, ref m_targetPosition, callback.ClosestHitFraction, out m_currentPosition);
                }

                full_drop = false;

                m_verticalVelocity = 0.0f;
                m_verticalOffset = 0.0f;
                m_wasJumping = false;

            }
            else
            {
                // we dropped the full height
                full_drop = true;

                if (bounce_fix == true)
                {
                    downVelocity = (m_verticalVelocity < 0.0f ? -m_verticalVelocity : 0.0f) * dt;
                    if (downVelocity > m_fallSpeed && (m_wasOnGround || !m_wasJumping))
                    {
                        m_targetPosition += step_drop; //undo previous target change
                        downVelocity = m_fallSpeed;
                        step_drop = upAxisDirection[m_upAxis] * (m_currentStepOffset + downVelocity);
                        m_targetPosition -= step_drop;
                    }
                }
                //printf("full drop - %g, %g\n", m_currentPosition.getY(), m_targetPosition.getY());

                m_currentPosition = m_targetPosition;
            }
        }

        public KinematicCharacterController(PairCachingGhostObject ghostObject, ConvexShape convexShape, float stepHeight, int upAxis = 1)
        {
            m_upAxis = upAxis;
            m_addedMargin = 0.02f;
            m_walkDirection = Vector3.Zero;
            m_useGhostObjectSweepTest = true;
            m_ghostObject = ghostObject;
            m_stepHeight = stepHeight;
            m_turnAngle = 0f;
            m_convexShape = convexShape;
            m_useWalkDirection = true;	// use walk direction by default, legacy behavior
            m_velocityTimeInterval = 0.0f;
            m_verticalVelocity = 0.0f;
            m_verticalOffset = 0.0f;
            Gravity = 9.8f * 3; // 3G acceleration.
            m_fallSpeed = 55.0f; // Terminal velocity of a sky diver in m/s.
            m_jumpSpeed = 10.0f; // ?
            m_wasOnGround = false;
            m_wasJumping = false;
            m_interpolateUp = true;
            MaxSlope = MathUtil.DegToRadians(45.0f);
            m_currentStepOffset = 0;
            full_drop = false;
            bounce_fix = false;
        }

        ///btActionInterface interface
        public virtual void UpdateAction(CollisionWorld collisionWorld, float deltaTime)
        {
            PreStep(collisionWorld);
            PlayerStep(collisionWorld, deltaTime);
        }

        ///btActionInterface interface
        public void DebugDraw(IDebugDraw debugDrawer)
        {
        }

        public void SetUpAxis(int axis)
        {
            if (axis < 0)
                axis = 0;
            if (axis > 2)
                axis = 2;
            m_upAxis = axis;
        }

        public void SetUpInterpolate(bool v)
        {
        }

        public virtual void SetWalkDirection(ref Vector3 walkDirection)
        {
            m_useWalkDirection = true;
            m_walkDirection = walkDirection;
            m_normalizedDirection = GetNormalizedVector(ref m_walkDirection);
        }

        public virtual void SetWalkDirection(Vector3 walkDirection)
        {
            SetWalkDirection(ref walkDirection);
        }

        public void SetVelocityForTimeInterval(ref Vector3 velocity, float timeInterval)
        {
            //	printf("setVelocity!\n");
            //	printf("  interval: %f\n", timeInterval);
            //	printf("  velocity: (%f, %f, %f)\n",
            //	    velocity.x(), velocity.y(), velocity.z());

            m_useWalkDirection = false;
            m_walkDirection = velocity;
            m_normalizedDirection = GetNormalizedVector(ref m_walkDirection);
            m_velocityTimeInterval = timeInterval;
        }

        public void SetVelocityForTimeInterval(Vector3 velocity, float timeInterval)
        {
            SetVelocityForTimeInterval(ref velocity, timeInterval);
        }

        public void Reset(CollisionWorld collisionWorld)
        {
            m_verticalVelocity = 0.0f;
            m_verticalOffset = 0.0f;
            m_wasOnGround = false;
            m_wasJumping = false;
            m_walkDirection = Vector3.Zero;
            m_velocityTimeInterval = 0.0f;

            //clear pair cache
            HashedOverlappingPairCache cache = m_ghostObject.OverlappingPairCache;
            while (cache.OverlappingPairArray.Count > 0)
            {
                cache.RemoveOverlappingPair(cache.OverlappingPairArray[0].Proxy0, cache.OverlappingPairArray[0].Proxy1, collisionWorld.Dispatcher);
            }
        }

        public void Warp(ref Vector3 origin)
        {
            m_ghostObject.WorldTransform = Matrix.Translation(origin);
        }

        public void PreStep(CollisionWorld collisionWorld)
        {
            int numPenetrationLoops = 0;
            m_touchingContact = false;
            while (RecoverFromPenetration(collisionWorld))
            {
                numPenetrationLoops++;
                m_touchingContact = true;
                if (numPenetrationLoops > 4)
                {
                    //			printf("character could not recover from penetration = %d\n", numPenetrationLoops);
                    break;
                }
            }

            m_currentPosition = m_ghostObject.WorldTransform.Origin;
            m_targetPosition = m_currentPosition;

        }

        public void PlayerStep(CollisionWorld collisionWorld, float dt)
        {
            // quick check...
            if (!m_useWalkDirection && m_velocityTimeInterval <= 0.0)
            {
                //		printf("\n");
                return;		// no motion
            }

            m_wasOnGround = OnGround;

            // Update fall velocity.
            m_verticalVelocity -= Gravity * dt;
            if (m_verticalVelocity > 0.0f && m_verticalVelocity > m_jumpSpeed)
            {
                m_verticalVelocity = m_jumpSpeed;
            }
            if (m_verticalVelocity < 0.0f && System.Math.Abs(m_verticalVelocity) > System.Math.Abs(m_fallSpeed))
            {
                m_verticalVelocity = -System.Math.Abs(m_fallSpeed);
            }
            m_verticalOffset = m_verticalVelocity * dt;


            Matrix xform = m_ghostObject.WorldTransform;

            //	printf("walkDirection(%f,%f,%f)\n",walkDirection[0],walkDirection[1],walkDirection[2]);
            //	printf("walkSpeed=%f\n",walkSpeed);

            StepUp(collisionWorld);
            if (m_useWalkDirection)
            {
                StepForwardAndStrafe(collisionWorld, ref m_walkDirection);
            }
            else
            {
                //printf("  time: %f", m_velocityTimeInterval);
                // still have some time left for moving!
                float dtMoving =
                   (dt < m_velocityTimeInterval) ? dt : m_velocityTimeInterval;
                m_velocityTimeInterval -= dt;

                // how far will we move while we are moving?
                Vector3 move = m_walkDirection * dtMoving;

                // printf("  dtMoving: %f", dtMoving);

                // okay, step
                StepForwardAndStrafe(collisionWorld, ref move);
            }
            StepDown(collisionWorld, dt);

            xform.Origin = m_currentPosition;
            m_ghostObject.WorldTransform = xform;
        }

        public void SetFallSpeed(float fallSpeed)
        {
            m_fallSpeed = fallSpeed;
        }

        public void SetJumpSpeed(float jumpSpeed)
        {
            m_jumpSpeed = jumpSpeed;
        }

        public void SetMaxJumpHeight(float maxJumpHeight)
        {
            m_maxJumpHeight = maxJumpHeight;
        }

        public bool CanJump
        {
            get { return OnGround; }
        }

        public void Jump()
        {
            if (CanJump)
            {
                m_verticalVelocity = m_jumpSpeed;
                m_wasJumping = true;

                //currently no jumping.
                //Matrix xform;
                //m_rigidBody.getMotionState().getWorldTransform (out xform);
                //Vector3 up = xform.Up;
                //up.Normalize ();
                //float magnitude = (1.0f/m_rigidBody.getInvMass()) * 8.0f;
                //m_rigidBody.applyCentralImpulse (up * magnitude);
            }

        }

        public float Gravity { get; set; }

        /// The max slope determines the maximum angle that the controller can walk up.
        /// The slope angle is measured in radians.
        public float MaxSlope
        {
            get { return m_maxSlopeRadians; }
            set
            {
                m_maxSlopeRadians = value;
                m_maxSlopeCosine = (float)System.Math.Cos(value);
            }
        }

        public PairCachingGhostObject GhostObject
        {
            get { return m_ghostObject; }
        }

        public void SetUseGhostSweepTest(bool useGhostObjectSweepTest)
        {
            m_useGhostObjectSweepTest = useGhostObjectSweepTest;
        }

        public bool OnGround
        {
            get { return m_verticalVelocity == 0.0f && m_verticalOffset == 0.0f; }
        }

        public static Vector3 GetNormalizedVector(ref Vector3 v)
        {
            if (v.Length < MathUtil.SIMD_EPSILON)
            {
                return Vector3.Zero;
            }
            return Vector3.Normalize(v);
        }


        protected float m_halfHeight;

        protected PairCachingGhostObject m_ghostObject;
        protected ConvexShape m_convexShape;//is also in m_ghostObject, but it needs to be convex, so we store it here to avoid upcast

        protected float m_verticalVelocity;
        protected float m_verticalOffset;


        protected float m_fallSpeed;
        protected float m_jumpSpeed;
        protected float m_maxJumpHeight;
        protected float m_maxSlopeRadians; // Slope angle that is set (used for returning the exact value)
        protected float m_maxSlopeCosine;  // Cosine equivalent of m_maxSlopeRadians (calculated once when set, for optimization)

        protected float m_turnAngle;

        protected float m_stepHeight;

        protected float m_addedMargin;//@todo: remove this and fix the code

        ///this is the desired walk direction, set by the user
        protected Vector3 m_walkDirection;
        protected Vector3 m_normalizedDirection;

        //some internal variables
        protected Vector3 m_currentPosition;
        float m_currentStepOffset;
        protected Vector3 m_targetPosition;

        ///keep track of the contact manifolds
        protected AlignedManifoldArray m_manifoldArray = new AlignedManifoldArray();

        protected bool m_touchingContact;
        protected Vector3 m_touchingNormal;
        protected bool m_wasOnGround;
        protected bool m_wasJumping;

        protected bool m_useGhostObjectSweepTest;

        protected int m_upAxis;
        protected bool m_useWalkDirection;
        protected float m_velocityTimeInterval;
        
        protected bool m_interpolateUp;
        protected bool full_drop;
        protected bool bounce_fix;



        protected static Vector3[] upAxisDirection = { new Vector3(1, 0, 0), new Vector3(0, 1, 0), new Vector3(0, 0, 1) };

    }


    ///@todo Interact with dynamic objects,
    ///Ride kinematicly animated platforms properly
    ///More realistic (or maybe just a config option) falling
    /// -> Should integrate falling velocity manually and use that in stepDown()
    ///Support jumping
    ///Support ducking
    public class KinematicClosestNotMeRayResultCallback : ClosestRayResultCallback
    {
        static Vector3 zero = new Vector3();

        public KinematicClosestNotMeRayResultCallback(CollisionObject me)
            : base(ref zero, ref zero)
        {
            _me = me;
        }

        public override float AddSingleResult(LocalRayResult rayResult, bool normalInWorldSpace)
        {
            if (rayResult.CollisionObject == _me)
                return 1.0f;

            return base.AddSingleResult(rayResult, normalInWorldSpace);
        }

        protected CollisionObject _me;
    }

    public class KinematicClosestNotMeConvexResultCallback : ClosestConvexResultCallback
    {
        static Vector3 zero = new Vector3();

        public KinematicClosestNotMeConvexResultCallback(CollisionObject me, Vector3 up, float minSlopeDot)
            : base(ref zero, ref zero)
        {
            _me = me;
            _up = up;
            _minSlopeDot = minSlopeDot;
        }

        public override float AddSingleResult(LocalConvexResult convexResult, bool normalInWorldSpace)
        {
            if (convexResult.HitCollisionObject == _me)
            {
                return 1.0f;
            }

            if (!convexResult.HitCollisionObject.HasContactResponse)
            {
                return 1.0f;
            }

            Vector3 hitNormalWorld;
            if (normalInWorldSpace)
            {
                hitNormalWorld = convexResult.HitNormalLocal;
            }
            else
            {
                // need to transform normal into worldspace
                hitNormalWorld = Vector3.TransformCoordinate(convexResult.HitNormalLocal, convexResult.HitCollisionObject.WorldTransform.Basis);
            }

            float dotUp;
            Vector3.Dot(ref _up, ref hitNormalWorld, out dotUp);
            if (dotUp < _minSlopeDot)
            {
                return 1.0f;
            }

            return base.AddSingleResult(convexResult, normalInWorldSpace);
        }

        protected CollisionObject _me;
        protected Vector3 _up;
        protected float _minSlopeDot;
    }
}
