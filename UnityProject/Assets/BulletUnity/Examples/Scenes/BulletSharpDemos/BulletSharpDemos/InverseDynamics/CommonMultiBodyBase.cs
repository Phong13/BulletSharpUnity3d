
using BulletSharp;
using BulletSharp.Math;
using DemoFramework;
using System;
using BulletSharpExamples;


public enum MyFilterModes
{
	FILTER_GROUPAMASKB_AND_GROUPBMASKA2=0,
	FILTER_GROUPAMASKB_OR_GROUPBMASKA2
};

/*
struct MyOverlapFilterCallback2 : public btOverlapFilterCallback
{
	int m_filterMode;
	
	MyOverlapFilterCallback2()
	:m_filterMode(FILTER_GROUPAMASKB_AND_GROUPBMASKA2)
	{
	}
	
	virtual ~MyOverlapFilterCallback2()
	{}
	// return true when pairs need collision
	virtual bool	needBroadphaseCollision(btBroadphaseProxy* proxy0,btBroadphaseProxy* proxy1) const
	{
		if (m_filterMode==FILTER_GROUPAMASKB_AND_GROUPBMASKA2)
		{
			bool collides = (proxy0.m_collisionFilterGroup & proxy1.m_collisionFilterMask) != 0;
			collides = collides && (proxy1.m_collisionFilterGroup & proxy0.m_collisionFilterMask);
			return collides;
		}
		
		if (m_filterMode==FILTER_GROUPAMASKB_OR_GROUPBMASKA2)
		{
			bool collides = (proxy0.m_collisionFilterGroup & proxy1.m_collisionFilterMask) != 0;
			collides = collides || (proxy1.m_collisionFilterGroup & proxy0.m_collisionFilterMask);
			return collides;
		}
		return false;
	}
};
*/

public abstract class CommonMultiBodyBase : ISimulation, ISimulationCustomUpdate
{
    
		//keep the collision shapes, for deletion/cleanup
	protected AlignedCollisionObjectArray	m_collisionShapes;
    //MyOverlapFilterCallback2 m_filterCallback;
    protected OverlappingPairCache m_pairCache;
    protected BroadphaseInterface m_broadphase;
    protected CollisionDispatcher m_dispatcher;
    protected MultiBodyConstraintSolver m_solver;
    protected DefaultCollisionConfiguration m_collisionConfiguration;
    protected MultiBodyDynamicsWorld m_dynamicsWorld;

	//data for picking objects
	//class btRigidBody	m_pickedBody;
	//class btTypedConstraint m_pickedConstraint;
	//class btMultiBodyPoint2Point		m_pickingMultiBodyPoint2Point;

	BulletSharp.Math.Vector3 m_oldPickingPos;
	BulletSharp.Math.Vector3 m_hitPos;
	float m_oldPickingDist;
	bool m_prevCanSleep;

    public CollisionConfiguration CollisionConfiguration
    {
        get
        {
            return m_collisionConfiguration;
        }
    }

    public CollisionDispatcher Dispatcher
    {
        get
        {
            return m_dispatcher;
        }
    }

    public BroadphaseInterface Broadphase
    {
        get
        {
            return m_broadphase;
        }
    }

    public DiscreteDynamicsWorld World
    {
        get
        {
            return m_dynamicsWorld;
        }
    }

    //GUIHelperInterface m_guiHelper;

    public CommonMultiBodyBase()
	{
        //m_filterCallback = null;
        m_pairCache = null;
        m_broadphase = null;
        m_dispatcher = null;
        m_solver = null;
        m_collisionConfiguration = null;
        m_dynamicsWorld = null;
        //m_pickedBody = null;
        //m_pickedConstraint = null;
        //m_pickingMultiBodyPoint2Point = null;
        m_prevCanSleep = false;
        //m_guiHelper = null;
    
    }

	public virtual void createEmptyDynamicsWorld()
	{
		///collision configuration contains default setup for memory, collision setup
		m_collisionConfiguration = new DefaultCollisionConfiguration();
		//m_collisionConfiguration.setConvexConvexMultipointIterations();
		//m_filterCallback = new MyOverlapFilterCallback2();
		
		///use the default collision dispatcher. For parallel processing you can use a diffent dispatcher (see Extras/BulletMultiThreaded)
		m_dispatcher = new	CollisionDispatcher(m_collisionConfiguration);

		m_pairCache = new HashedOverlappingPairCache();

		//m_pairCache.OverlapFilterCallback = (m_filterCallback);
		
		m_broadphase = new DbvtBroadphase(m_pairCache);//btSimpleBroadphase();

		m_solver = new MultiBodyConstraintSolver();

		m_dynamicsWorld = new MultiBodyDynamicsWorld(m_dispatcher, m_broadphase, m_solver, m_collisionConfiguration);

		m_dynamicsWorld.Gravity = ( new BulletSharp.Math.Vector3(0, -10, 0));
	}


	public virtual void OnUpdate()
	{
		if (m_dynamicsWorld != null)
		{
			m_dynamicsWorld.StepSimulation(UnityEngine.Time.fixedDeltaTime);
		}
	}


	public virtual void exitPhysics()
	{
		removePickingConstraint();
		//cleanup in the reverse order of creation/initialization

		//remove the rigidbodies from the dynamics world and delete them

		if (m_dynamicsWorld == null)
		{

            int i;
            for (i = m_dynamicsWorld.NumConstraints - 1; i >= 0; i--)
            {
                m_dynamicsWorld.RemoveConstraint(m_dynamicsWorld.GetConstraint(i));
            }
			
			for (i = m_dynamicsWorld.NumMultiBodyConstraints - 1; i >= 0; i--)
			{
				MultiBodyConstraint mbc = m_dynamicsWorld.GetMultiBodyConstraint(i);
				m_dynamicsWorld.RemoveMultiBodyConstraint(mbc);
				mbc.Dispose();
			}

			for (i = m_dynamicsWorld.NumMultibodies - 1; i >= 0; i--)
			{
				MultiBody mb = m_dynamicsWorld.GetMultiBody(i);
				m_dynamicsWorld.RemoveMultiBody(mb);
				mb.Dispose();
			}
			for (i = m_dynamicsWorld.NumCollisionObjects - 1; i >= 0; i--)
			{
				CollisionObject obj = m_dynamicsWorld.CollisionObjectArray[i];
				RigidBody body = RigidBody.Upcast(obj);
				if (body != null && body.MotionState != null)
				{
					body.MotionState.Dispose();
				}
				m_dynamicsWorld.RemoveCollisionObject(obj);
				obj.Dispose();
			}
		}
		//delete collision shapes
		for (int j = 0; j<m_collisionShapes.Count; j++)
		{
			CollisionObject shape = m_collisionShapes[j];
			shape.Dispose();
		}
		m_collisionShapes.Clear();

		m_dynamicsWorld.Dispose();
		m_dynamicsWorld = null;

		m_solver.Dispose();
		m_solver=null;

		m_broadphase.Dispose();
		m_broadphase=null;

		m_dispatcher.Dispose();
		m_dispatcher=null;
		
		m_pairCache.Dispose();
		m_pairCache = null;

		//m_filterCallback.Dispose();
		//m_filterCallback = null;
		
		m_collisionConfiguration.Dispose();
		m_collisionConfiguration=null;
	}

    /*
	public virtual void syncPhysicsToGraphics()
	{
		if (m_dynamicsWorld)
		{
			m_guiHelper.syncPhysicsToGraphics(m_dynamicsWorld);
		}
	}
    */
    /*
	public virtual void renderScene()
	{
        if (m_dynamicsWorld)
        {
		m_guiHelper.syncPhysicsToGraphics(m_dynamicsWorld);

		m_guiHelper.render(m_dynamicsWorld);
        }
	
	}
    */

    public virtual void    physicsDebugDraw(DebugDrawModes debugDrawFlags)
    {
     	if (m_dynamicsWorld != null)
        {
			if (m_dynamicsWorld.DebugDrawer != null)
			{
				m_dynamicsWorld.DebugDrawer.DebugMode = (debugDrawFlags);
			}
            m_dynamicsWorld.DebugDrawWorld();
        }

    }

	public virtual bool	keyboardCallback(int key, int state)
	{
        /*
		if ((key==B3G_F3) && state && m_dynamicsWorld)
		{
			btDefaultSerializer*	serializer = new btDefaultSerializer();
			m_dynamicsWorld.serialize(serializer);

			FILE* file = fopen("testFile.bullet","wb");
			fwrite(serializer.getBufferPointer(),serializer.getCurrentBufferSize(),1, file);
			fclose(file);
			//b3Printf("btDefaultSerializer wrote testFile.bullet");
			serializer.Dispose();
			return true;

		}
        */
		return false;//don't handle this key
	}


	BulletSharp.Math.Vector3	getRayTo(int x,int y)
	{
        /*
		CommonRenderInterface* renderer = m_guiHelper.getRenderInterface();
		
		if (!renderer)
		{
			btAssert(0);
			return BulletSharp.Math.Vector3(0,0,0);
		}

		float top = 1.f;
		float bottom = -1.f;
		float nearPlane = 1.f;
		float tanFov = (top-bottom)*0.5f / nearPlane;
		float fov = 2.0f * btAtan(tanFov);

		BulletSharp.Math.Vector3 camPos,camTarget;
		renderer.getActiveCamera().getCameraPosition(camPos);
		renderer.getActiveCamera().getCameraTargetPosition(camTarget);

		BulletSharp.Math.Vector3	rayFrom = camPos;
		BulletSharp.Math.Vector3 rayForward = (camTarget-camPos);
		rayForward.normalize();
		float farPlane = 10000.f;
		rayForward*= farPlane;

		BulletSharp.Math.Vector3 rightOffset;
		BulletSharp.Math.Vector3 cameraUp=BulletSharp.Math.Vector3(0,0,0);
		cameraUp[m_guiHelper.getAppInterface().getUpAxis()]=1;

		BulletSharp.Math.Vector3 vertical = cameraUp;

		BulletSharp.Math.Vector3 hor;
		hor = rayForward.cross(vertical);
		hor.normalize();
		vertical = hor.cross(rayForward);
		vertical.normalize();

		float tanfov = tanf(0.5f*fov);


		hor *= 2.f * farPlane * tanfov;
		vertical *= 2.f * farPlane * tanfov;

		float aspect;
		float width = renderer.getScreenWidth();
		float height = renderer.getScreenHeight();

		aspect =  width / height;

		hor*=aspect;


		BulletSharp.Math.Vector3 rayToCenter = rayFrom + rayForward;
		BulletSharp.Math.Vector3 dHor = hor * 1.f/width;
		BulletSharp.Math.Vector3 dVert = vertical * 1.f/height;


		BulletSharp.Math.Vector3 rayTo = rayToCenter - 0.5f * hor + 0.5f * vertical;
		rayTo += x * dHor;
		rayTo -= y * dVert;
        */
        return new BulletSharp.Math.Vector3();
		//return rayTo;
	}
    /*
	virtual bool	mouseMoveCallback(float x,float y)
	{
		CommonRenderInterface* renderer = m_guiHelper.getRenderInterface();
		
		if (!renderer)
		{
			btAssert(0);
			return false;
		}

		BulletSharp.Math.Vector3 rayTo = getRayTo(int(x), int(y));
		BulletSharp.Math.Vector3 rayFrom;
		renderer.getActiveCamera().getCameraPosition(rayFrom);
		movePickedBody(rayFrom,rayTo);

		return false;
	}

	virtual bool	mouseButtonCallback(int button, int state, float x, float y)
	{
		CommonRenderInterface* renderer = m_guiHelper.getRenderInterface();
		
		if (!renderer)
		{
			btAssert(0);
			return false;
		}
		
		CommonWindowInterface* window = m_guiHelper.getAppInterface().m_window;

	
		if (state==1)
		{
			if(button==0 && (!window.isModifierKeyPressed(B3G_ALT) && !window.isModifierKeyPressed(B3G_CONTROL) ))
			{
				BulletSharp.Math.Vector3 camPos;
				renderer.getActiveCamera().getCameraPosition(camPos);

				BulletSharp.Math.Vector3 rayFrom = camPos;
				BulletSharp.Math.Vector3 rayTo = getRayTo(int(x),int(y));

				pickBody(rayFrom, rayTo);


			}
		} else
		{
			if (button==0)
			{
				removePickingConstraint();
				//remove p2p
			}
		}

		//printf("button=%d, state=%d\n",button,state);
		return false;
	}


	virtual bool pickBody(const BulletSharp.Math.Vector3& rayFromWorld, const BulletSharp.Math.Vector3& rayToWorld)
	{
		if (m_dynamicsWorld==0)
			return false;

		btCollisionWorld::ClosestRayResultCallback rayCallback(rayFromWorld, rayToWorld);

		m_dynamicsWorld.rayTest(rayFromWorld, rayToWorld, rayCallback);
		if (rayCallback.hasHit())
		{

			BulletSharp.Math.Vector3 pickPos = rayCallback.m_hitPointWorld;
			btRigidBody* body = (btRigidBody*)btRigidBody::upcast(rayCallback.m_collisionObject);
			if (body)
			{
				//other exclusions?
				if (!(body.isStaticObject() || body.isKinematicObject()))
				{
					m_pickedBody = body;
					m_pickedBody.setActivationState(DISABLE_DEACTIVATION);
					//printf("pickPos=%f,%f,%f\n",pickPos.getX(),pickPos.getY(),pickPos.getZ());
					BulletSharp.Math.Vector3 localPivot = body.getCenterOfMassTransform().inverse() * pickPos;
					btPoint2PointConstraint* p2p = new btPoint2PointConstraint(*body, localPivot);
					m_dynamicsWorld.addConstraint(p2p, true);
					m_pickedConstraint = p2p;
					float mousePickClamping = 30.f;
					p2p.m_setting.m_impulseClamp = mousePickClamping;
					//very weak constraint for picking
					p2p.m_setting.m_tau = 0.001f;
				}
			} else
			{
				btMultiBodyLinkCollider* multiCol = (btMultiBodyLinkCollider*)btMultiBodyLinkCollider::upcast(rayCallback.m_collisionObject);
				if (multiCol && multiCol.m_multiBody)
				{
						
					m_prevCanSleep = multiCol.m_multiBody.getCanSleep();
					multiCol.m_multiBody.setCanSleep(false);

					BulletSharp.Math.Vector3 pivotInA = multiCol.m_multiBody.worldPosToLocal(multiCol.m_link, pickPos);

					btMultiBodyPoint2Point* p2p = new btMultiBodyPoint2Point(multiCol.m_multiBody,multiCol.m_link,0,pivotInA,pickPos);
					//if you add too much energy to the system, causing high angular velocities, simulation 'explodes'
					//see also http://www.bulletphysics.org/Bullet/phpBB3/viewtopic.php?f=4&t=949
					//so we try to avoid it by clamping the maximum impulse (force) that the mouse pick can apply
					//it is not satisfying, hopefully we find a better solution (higher order integrator, using joint friction using a zero-velocity target motor with limited force etc?)
					float scaling=1;
					p2p.setMaxAppliedImpulse(2*scaling);
		
					btMultiBodyDynamicsWorld* world = (btMultiBodyDynamicsWorld*) m_dynamicsWorld;
					world.addMultiBodyConstraint(p2p);
					m_pickingMultiBodyPoint2Point =p2p; 
				}
			}



			//					pickObject(pickPos, rayCallback.m_collisionObject);
			m_oldPickingPos = rayToWorld;
			m_hitPos = pickPos;
			m_oldPickingDist = (pickPos - rayFromWorld).length();
			//					printf("hit !\n");
			//add p2p
		}
		return false;
	}
	virtual bool movePickedBody(const BulletSharp.Math.Vector3& rayFromWorld, const BulletSharp.Math.Vector3& rayToWorld)
	{
		if (m_pickedBody  && m_pickedConstraint)
		{
			btPoint2PointConstraint* pickCon = static_cast<btPoint2PointConstraint*>(m_pickedConstraint);
			if (pickCon)
			{
				//keep it at the same picking distance
		
				BulletSharp.Math.Vector3 dir = rayToWorld-rayFromWorld;
				dir.normalize();
				dir *= m_oldPickingDist;

				BulletSharp.Math.Vector3 newPivotB = rayFromWorld + dir;
				pickCon.setPivotB(newPivotB);
			}
		}
		
		if (m_pickingMultiBodyPoint2Point)
		{
			//keep it at the same picking distance

		
			BulletSharp.Math.Vector3 dir = rayToWorld-rayFromWorld;
			dir.normalize();
			dir *= m_oldPickingDist;

			BulletSharp.Math.Vector3 newPivotB = rayFromWorld + dir;
			
			m_pickingMultiBodyPoint2Point.setPivotInB(newPivotB);
		}
		
		return false;
	}
    */

	public virtual void removePickingConstraint()
	{
        /*
		if (m_pickedConstraint)
		{
			m_dynamicsWorld.removeConstraint(m_pickedConstraint);
			delete m_pickedConstraint;
			m_pickedConstraint = 0;
			m_pickedBody = 0;
		}
		if (m_pickingMultiBodyPoint2Point)
		{
			m_pickingMultiBodyPoint2Point.getMultiBodyA().setCanSleep(m_prevCanSleep);
			btMultiBodyDynamicsWorld* world = (btMultiBodyDynamicsWorld*) m_dynamicsWorld;
			world.removeMultiBodyConstraint(m_pickingMultiBodyPoint2Point);
			delete m_pickingMultiBodyPoint2Point;
			m_pickingMultiBodyPoint2Point = 0;
		}
        */
	}

    public abstract void Dispose();

    /*

	btBoxShape* createBoxShape(const BulletSharp.Math.Vector3& halfExtents)
	{
		btBoxShape* box = new btBoxShape(halfExtents);
		return box;
	}

	btRigidBody*	createRigidBody(float mass, const btTransform& startTransform, btCollisionShape* shape,  const btVector4& color = btVector4(1, 0, 0, 1))
	{
		btAssert((!shape || shape.getShapeType() != INVALID_SHAPE_PROXYTYPE));

		//rigidbody is dynamic if and only if mass is non zero, otherwise static
		bool isDynamic = (mass != 0.f);

		BulletSharp.Math.Vector3 localInertia(0, 0, 0);
		if (isDynamic)
			shape.calculateLocalInertia(mass, localInertia);

		//using motionstate is recommended, it provides interpolation capabilities, and only synchronizes 'active' objects

#define USE_MOTIONSTATE 1
#ifdef USE_MOTIONSTATE
		btDefaultMotionState* myMotionState = new btDefaultMotionState(startTransform);

		btRigidBody::btRigidBodyConstructionInfo cInfo(mass, myMotionState, shape, localInertia);

		btRigidBody* body = new btRigidBody(cInfo);
		//body.setContactProcessingThreshold(m_defaultContactProcessingThreshold);

#else
		btRigidBody* body = new btRigidBody(mass, 0, shape, localInertia);
		body.setWorldTransform(startTransform);
#endif//

		body.setUserIndex(-1);
		m_dynamicsWorld.addRigidBody(body);
		return body;
	}
    */
}

