/*
Bullet Continuous Collision Detection and Physics Library
Copyright (c) 2015 Google Inc. http://bulletphysics.org

This software is provided 'as-is', without any express or implied warranty.
In no event will the authors be held liable for any damages arising from the use of this software.
Permission is granted to anyone to use this software for any purpose,
including commercial applications, and to alter it and redistribute it freely,
subject to the following restrictions:

1. The origin of this software must not be misrepresented; you must not claim that you wrote the original software. If you use this software in a product, an acknowledgment in the product documentation would be appreciated but is not required.
2. Altered source versions must be plainly marked as such, and must not be misrepresented as being the original software.
3. This notice may not be removed or altered from any source distribution.
*/

using DemoFramework;
using BulletSharp;
using BulletSharp.Math;
using System;
using System.Globalization;
using System.Runtime.InteropServices;
using UnityEngine;
using System.Collections.Generic;

namespace InverseDynamicsExample
{

    public enum btInverseDynamicsExampleOptions
    {
        BT_ID_LOAD_URDF = 0,
        BT_ID_PROGRAMMATICALLY = 1
    }

    internal sealed class InverseDynamicsExample : IDemoConfiguration
    {
        public ISimulation CreateSimulation(Demo demo)
        {
            demo.FreeLook.Eye = new BulletSharp.Math.Vector3(0, 5, 10);
            demo.FreeLook.Target = BulletSharp.Math.Vector3.Zero;
            demo.Graphics.WindowTitle = "BulletSharp - FeatherStone Demo";
            return new InverseDynamicsExampleSimulation(btInverseDynamicsExampleOptions.BT_ID_PROGRAMMATICALLY);
        }
    }

    class InverseDynamicsExampleSimulation : CommonMultiBodyBase, ISimulationCustomUpdate
    {
        btInverseDynamicsExampleOptions m_option;
        MultiBody m_multiBody;
        InverseDynamicsBullet3.MultiBodyTree m_inverseModel;

        // the UI interface makes it easier to use static variables & free functions
        // as parameters and callbacks
        static float kp = 10 * 10f;
        static float kd = 2 * 10f;
        static bool useInverseModel = true;
        float[] qd;
        string[] qd_name;
        string[] q_name;
        public float radius = 1f;

        static BulletSharp.Math.Vector4[] sJointCurveColors = new BulletSharp.Math.Vector4[]
        {
        new BulletSharp.Math.Vector4(1, 0.3f, 0.3f, 1),
    new BulletSharp.Math.Vector4(0.3f,1,0.3f,1),
    new BulletSharp.Math.Vector4(0.3f,0.3f,1,1),
     new BulletSharp.Math.Vector4(0.3f,1,1,1),
    new BulletSharp.Math.Vector4(1,0.3f,1,1),
     new BulletSharp.Math.Vector4(1,1,0.3f,1),
     new BulletSharp.Math.Vector4(1,0.7f,0.7f,1),
     new BulletSharp.Math.Vector4(0.7f,1,1,1),

        };


        void toggleUseInverseModel(int buttonId, bool buttonState)
        {
            useInverseModel = !useInverseModel;
            // todo(thomas) is there a way to get a toggle button with changing text?
            Debug.LogFormat("switched inverse model {0}", useInverseModel ? "on" : "off");
        }

        /*
        virtual void resetCamera()
        {
            float dist = 1.5;
            float pitch = -10;
            float yaw = -80;
            float[] targetPos = new float{0,0,0};
            m_guiHelper.resetCamera(dist,yaw,pitch,targetPos[0],targetPos[1],targetPos[2]);
        }
        */


        public InverseDynamicsExampleSimulation(btInverseDynamicsExampleOptions option) : base()
        {
            m_option = option;
            m_multiBody = null;
            m_inverseModel = null;
            //m_timeSeriesCanvas = null;
            initPhysics();
        }

        public override void Dispose()
        {
            //exitPhysics();
            if (m_inverseModel != null) m_inverseModel.Dispose();
            this.StandardCleanup();
            //delete m_timeSeriesCanvas;
        }

        public void initPhysics()
        {
            //roboticists like Z up
            int upAxis = 2;
            //m_guiHelper.setUpAxis(upAxis);

            createEmptyDynamicsWorld();
            BulletSharp.Math.Vector3 gravity = new BulletSharp.Math.Vector3(0, 0, 0);
            // gravity[upAxis]=-9.8;
            m_dynamicsWorld.Gravity = (gravity);

            //m_guiHelper.createPhysicsDebugDrawer(m_dynamicsWorld);
            {
                /*
            SliderParams slider("Kp",kp);
            slider.m_minVal=0;
            slider.m_maxVal=2000;
            if (m_guiHelper.getParameterInterface())
                m_guiHelper.getParameterInterface().registerSliderFloatParameter(slider);
                */
            }
            {
                /*
            SliderParams slider("Kd",kd);
            slider.m_minVal=0;
            slider.m_maxVal=50;
            if (m_guiHelper.getParameterInterface())
                m_guiHelper.getParameterInterface().registerSliderFloatParameter(slider);
                */
            }

            if (m_option == btInverseDynamicsExampleOptions.BT_ID_PROGRAMMATICALLY)
            {
               /*
            ButtonParams button("toggle inverse model",0,true);
            button.m_callback = toggleUseInverseModel;
            m_guiHelper.getParameterInterface().registerButtonParameter(button);
            */
            }


            switch (m_option)
            {
                case btInverseDynamicsExampleOptions.BT_ID_LOAD_URDF:
                    {

                        Debug.LogError("Not implemented");
                        /*
                        BulletURDFImporter u2b = new BulletURDFImporter(m_guiHelper, 0, 1);
                        bool loadOk = u2b.loadURDF("kuka_iiwa/model.urdf");// lwr / kuka.urdf");
                        if (loadOk)
                        {
                            int rootLinkIndex = u2b.getRootLinkIndex();
                            Debug.Log("urdf root link index = {0}\n", rootLinkIndex);
                            MyMultiBodyCreator creation = new MyMultiBodyCreator(m_guiHelper);
                            Transform identityTrans;
                            identityTrans.setIdentity();
                            ConvertURDF2Bullet(u2b, creation, identityTrans, m_dynamicsWorld, true, u2b.getPathPrefix());
                            for (int i = 0; i < u2b.getNumAllocatedCollisionShapes(); i++)
                            {
                                m_collisionShapes.push_back(u2b.getAllocatedCollisionShape(i));
                            }
                            m_multiBody = creation.getBulletMultiBody();
                            if (m_multiBody)
                            {
                                //kuka without joint control/constraints will gain energy explode soon due to timestep/integrator
                                //temporarily set some extreme damping factors until we have some joint control or constraints
                                m_multiBody.AngularDamping = (0 * 0.99);
                                m_multiBody.LinearDamping = (0 * 0.99);
                                Debug.Log("Root link name = {0}", u2b.getLinkName(u2b.getRootLinkIndex()).c_str());
                            }
                        }
                        */
                        break;
                    }
                case btInverseDynamicsExampleOptions.BT_ID_PROGRAMMATICALLY:
                    {
                        Matrix baseWorldTrans = new Matrix();
                        baseWorldTrans.ScaleVector = BulletSharp.Math.Vector3.One;
                        m_multiBody = createInvertedPendulumMultiBody(radius, m_dynamicsWorld, baseWorldTrans, false);
                        break;
                    }
                default:
                    {
                        Debug.LogError("Unknown option in initPhysics");
                        Debug.Assert(false);
                        break;
                    }
            };


            if (false)//(m_multiBody != null)
            {
                {
                    //if (m_guiHelper.getAppInterface() && m_guiHelper.getParameterInterface())
                    //{
                    //m_timeSeriesCanvas = new TimeSeriesCanvas(m_guiHelper.getAppInterface().m_2dCanvasInterface, 512, 230, "Joint Space Trajectory");
                    //m_timeSeriesCanvas.setupTimeSeries(3, 100, 0);
                    //}
                }

                // construct inverse model
                InverseDynamicsBullet3.MultiBodyTreeCreator id_creator = new InverseDynamicsBullet3.MultiBodyTreeCreator();
                if (-1 == id_creator.CreateFromMultiBody(m_multiBody))
                {
                    Debug.LogError("error creating tree\n");
                }
                else
                {
                    m_inverseModel = id_creator.CreateMultiBodyTree();
                }
                // add joint target controls
                qd = new float[m_multiBody.NumDofs];

                qd_name = new string[m_multiBody.NumDofs];
                q_name = new string[m_multiBody.NumDofs];

                /*
                            if (m_timeSeriesCanvas && m_guiHelper.getParameterInterface())
                            {
                                for (std.size_t dof = 0; dof < qd.size(); dof++)
                                {

                                    qd[dof] = 0;
                                    char tmp[25];
                                    sprintf(tmp,"q_desired[%lu]",dof);
                                    qd_name[dof] = tmp;
                                    SliderParams slider(qd_name[dof].c_str(),&qd[dof]);
                                    slider.m_minVal=-3.14;
                                    slider.m_maxVal=3.14;

                                    sprintf(tmp,"q[%lu]",dof); 
                                    q_name[dof] = tmp;   
                                    m_guiHelper.getParameterInterface().registerSliderFloatParameter(slider);
                                    btVector4 color = sJointCurveColors[dof&7];
                                    m_timeSeriesCanvas.addDataSource(q_name[dof].c_str(), color[0]*255,color[1]*255,color[2]*255);

                                }
                            }
                 */
            }

            // m_guiHelper.autogenerateGraphicsObjects(m_dynamicsWorld);

        }

        public override void OnUpdate()
        {
            Debug.Log("Step Simulation");
            if (false) //(m_multiBody != null)
            {
                int num_dofs = m_multiBody.NumDofs;
                float[] nu = new float[num_dofs];
                float[] qdot = new float[num_dofs];
                float[] q = new float[num_dofs];
                float[] joint_force = new float[num_dofs];
                float[] pd_control = new float[num_dofs];

                // compute joint forces from one of two control laws:
                // 1) "computed torque" control, which gives perfect, decoupled,
                //    linear second order error dynamics per dof in case of a
                //    perfect model and (and negligible time discretization effects)
                // 2) decoupled PD control per joint, without a model
                for (int dof = 0; dof < num_dofs; dof++)
                {
                    q[dof] = m_multiBody.GetJointPos(dof);
                    qdot[dof] = m_multiBody.GetJointVel(dof);

                    float qd_dot = 0;
                    float qd_ddot = 0;
                    //if (m_timeSeriesCanvas)
                    //    m_timeSeriesCanvas.insertDataAtCurrentTime(q[dof], dof, true);

                    // pd_control is either desired joint torque for pd control,
                    // or the feedback contribution to nu
                    pd_control[dof] = kd * (qd_dot - qdot[dof]) + kp * (qd[dof] - q[dof]);
                    // nu is the desired joint acceleration for computed torque control
                    nu[dof] = qd_ddot + pd_control[dof];

                }
                if (useInverseModel)
                {
                    // calculate joint forces corresponding to desired accelerations nu
                    if (m_multiBody.HasFixedBase)
                    {
                        if (-1 != m_inverseModel.CalculateInverseDynamics(q, qdot, nu, joint_force))
                        {
                            //joint_force(dof) += damping*dot_q(dof);
                            // use inverse model: apply joint force corresponding to
                            // desired acceleration nu

                            for (int dof = 0; dof < num_dofs; dof++)
                            {
                                m_multiBody.AddJointTorque(dof, joint_force[dof]);
                            }
                        }

                    }
                    else
                    {
                        //the inverse dynamics model represents the 6 DOFs of the base, unlike btMultiBody.
                        //append some dummy values to represent the 6 DOFs of the base
                        float[] nu6 = new float[num_dofs + 6], qdot6 = new float[num_dofs + 6], q6 = new float[num_dofs + 6], joint_force6 = new float[num_dofs + 6];
                        for (int i = 0; i < num_dofs; i++)
                        {
                            nu6[6 + i] = nu[i];
                            qdot6[6 + i] = qdot[i];
                            q6[6 + i] = q[i];
                            joint_force6[6 + i] = joint_force[i];
                        }
                        if (-1 != m_inverseModel.CalculateInverseDynamics(q6, qdot6, nu6, joint_force6))
                        {
                            //joint_force(dof) += damping*dot_q(dof);
                            // use inverse model: apply joint force corresponding to
                            // desired acceleration nu

                            for (int dof = 0; dof < num_dofs; dof++)
                            {
                                m_multiBody.AddJointTorque(dof, joint_force6[dof + 6]);
                            }
                        }

                    }


                }
                else
                {
                    for (int dof = 0; dof < num_dofs; dof++)
                    {
                        // no model: just apply PD control law
                        m_multiBody.AddJointTorque(dof, pd_control[dof]);
                    }
                }
            }

            // if (m_timeSeriesCanvas)
            //     m_timeSeriesCanvas.nextTick();

            //todo: joint damping for btMultiBody, tune parameters

            // step the simulation
            if (m_dynamicsWorld != null)
            {
                // todo(thomas) check that this is correct:
                // want to advance by 10ms, with 1ms timesteps
                m_dynamicsWorld.StepSimulation(1e-3f, 0);//,1e-3);
                                                         /*
                                                         btAlignedObjectArray<BulletSharp.Math.Quaternion> scratch_q;
                                                         btAlignedObjectArray<BulletSharp.Math.Vector3> scratch_m;
                                                         m_multiBody.ForwardKinematics(scratch_q, scratch_m);
                                                         */
                Debug.LogError("TODO forward kinematics");
               /*
                #if 0
                        for (int i = 0; i < m_multiBody.getNumLinks(); i++)
                        {
                            //BulletSharp.Math.Vector3 pos = m_multiBody.getLink(i).m_cachedWorldTransform.getOrigin();
                            btTransform tr = m_multiBody.getLink(i).m_cachedWorldTransform;
                            BulletSharp.Math.Vector3 pos = tr.getOrigin() - quatRotate(tr.getRotation(), m_multiBody.getLink(i).m_dVector);
                            BulletSharp.Math.Vector3 localAxis = m_multiBody.getLink(i).m_axes[0].m_topVec;
                            //printf("link %d: %f,%f,%f, local axis:%f,%f,%f\n", i, pos.x(), pos.y(), pos.z(), localAxis.x(), localAxis.y(), localAxis.z());




                        }
                #endif
                */
            }
        }

        //todo(erwincoumans) Quick hack, reference to InvertedPendulumPDControl implementation. Will create a separate header/source file for this.
        public static MultiBody createInvertedPendulumMultiBody(float radius, MultiBodyDynamicsWorld world, Matrix baseWorldTrans, bool fixedBase)
        {
            BulletSharp.Math.Vector4[] colors = new BulletSharp.Math.Vector4[]
            {
            new BulletSharp.Math.Vector4(1,0,0,1),
            new BulletSharp.Math.Vector4(0,1,0,1),
            new BulletSharp.Math.Vector4(0,1,1,1),
            new BulletSharp.Math.Vector4(1,1,0,1),
            };
            int curColor = 0;

            bool damping = false;
            bool gyro = false;
            int numLinks = 2;
            bool spherical = false;                 //set it ot false -to use 1DoF hinges instead of 3DoF sphericals
            bool canSleep = false;
            bool selfCollide = false;
            BulletSharp.Math.Vector3 linkHalfExtents = new BulletSharp.Math.Vector3(0.05f, 0.37f, 0.1f);
            BulletSharp.Math.Vector3 baseHalfExtents = new BulletSharp.Math.Vector3(0.04f, 0.35f, 0.08f);


            //mbC.forceMultiDof();							//if !spherical, you can comment this line to check the 1DoF algorithm
            //init the base
            BulletSharp.Math.Vector3 baseInertiaDiag = new BulletSharp.Math.Vector3(0.0f, 0.0f, 0.0f);
            float baseMass = fixedBase ? 0.0f : 10.0f;

            if (baseMass != 0)
            {
                //CollisionShape *shape = new btSphereShape(baseHalfExtents[0]);// btBoxShape(BulletSharp.Math.Vector3(baseHalfExtents[0], baseHalfExtents[1], baseHalfExtents[2]));
                CollisionShape shape = new BoxShape(new BulletSharp.Math.Vector3(baseHalfExtents[0], baseHalfExtents[1], baseHalfExtents[2]));
                shape.CalculateLocalInertia(baseMass, out baseInertiaDiag);
                shape.Dispose();
            }


            MultiBody pMultiBody = new MultiBody(numLinks, 0, baseInertiaDiag, fixedBase, canSleep);



            pMultiBody.BaseWorldTransform = baseWorldTrans;
            BulletSharp.Math.Vector3 vel = new BulletSharp.Math.Vector3(0, 0, 0);
            //	pMultiBody.setBaseVel(vel);

            //init the links
            BulletSharp.Math.Vector3 hingeJointAxis = new BulletSharp.Math.Vector3(1, 0, 0);

            //y-axis assumed up
            BulletSharp.Math.Vector3 parentComToCurrentCom = new BulletSharp.Math.Vector3(0, -linkHalfExtents[1] * 2.0f, 0);                       //par body's COM to cur body's COM offset
            BulletSharp.Math.Vector3 currentPivotToCurrentCom = new BulletSharp.Math.Vector3(0, -linkHalfExtents[1], 0);                          //cur body's COM to cur body's PIV offset
            BulletSharp.Math.Vector3 parentComToCurrentPivot = parentComToCurrentCom - currentPivotToCurrentCom;   //par body's COM to cur body's PIV offset

            //////
            float q0 = 1.0f * Mathf.PI / 180.0f;
            BulletSharp.Math.Quaternion quat0 = new BulletSharp.Math.Quaternion(new BulletSharp.Math.Vector3(1, 0, 0), q0);
            quat0.Normalize();
            /////

            for (int i = 0; i < numLinks; ++i)
            {
                float linkMass = 1.0f;
                //if (i==3 || i==2)
                //	linkMass= 1000;
                BulletSharp.Math.Vector3 linkInertiaDiag = new BulletSharp.Math.Vector3(0.0f, 0.0f, 0.0f);

                CollisionShape shape = null;
                if (i == 0)
                {
                    shape = new BoxShape(new BulletSharp.Math.Vector3(linkHalfExtents[0], linkHalfExtents[1], linkHalfExtents[2]));//
                }
                else
                {
                    shape = new SphereShape(radius);
                }
                shape.CalculateLocalInertia(linkMass, out linkInertiaDiag);
                shape.Dispose();


                if (!spherical)
                {
                    //pMultiBody.setupRevolute(i, linkMass, linkInertiaDiag, i - 1, BulletSharp.Math.Quaternion(0.f, 0.f, 0.f, 1.f), hingeJointAxis, parentComToCurrentPivot, currentPivotToCurrentCom, false);

                    if (i == 0)
                    {
                        pMultiBody.SetupRevolute(i, linkMass, linkInertiaDiag, i - 1,
                            new BulletSharp.Math.Quaternion(0.0f, 0.0f, 0.0f, 1.0f),
                            hingeJointAxis,
                            parentComToCurrentPivot,
                            currentPivotToCurrentCom, false);
                    }
                    else
                    {
                        parentComToCurrentCom = new BulletSharp.Math.Vector3(0, -radius * 2.0f, 0);                        //par body's COM to cur body's COM offset
                        currentPivotToCurrentCom = new BulletSharp.Math.Vector3(0, -radius, 0);                          //cur body's COM to cur body's PIV offset
                        parentComToCurrentPivot = parentComToCurrentCom - currentPivotToCurrentCom;   //par body's COM to cur body's PIV offset


                        pMultiBody.SetupFixed(i, linkMass, linkInertiaDiag, i - 1,
                                        new BulletSharp.Math.Quaternion(0.0f, 0.0f, 0.0f, 1.0f),
                                        parentComToCurrentPivot,
                                        currentPivotToCurrentCom);
                    }

                    //pMultiBody.setupFixed(i,linkMass,linkInertiaDiag,i-1,BulletSharp.Math.Quaternion(0,0,0,1),parentComToCurrentPivot,currentPivotToCurrentCom,false);

                }
                else
                {
                    //pMultiBody.setupPlanar(i, linkMass, linkInertiaDiag, i - 1, BulletSharp.Math.Quaternion(0.f, 0.f, 0.f, 1.f)/*quat0*/, BulletSharp.Math.Vector3(1, 0, 0), parentComToCurrentPivot*2, false);
                    pMultiBody.SetupSpherical(i, linkMass, linkInertiaDiag, i - 1, new BulletSharp.Math.Quaternion(0.0f, 0.0f, 0.0f, 1.0f), parentComToCurrentPivot, currentPivotToCurrentCom, false);
                }
            }

            pMultiBody.FinalizeMultiDof();



            ///
            world.AddMultiBody(pMultiBody);
            MultiBody mbC = pMultiBody;
            mbC.CanSleep = (canSleep);
            mbC.HasSelfCollision = (selfCollide);
            mbC.UseGyroTerm = (gyro);
            //
            if (!damping)
            {
                mbC.LinearDamping = (0.0f);
                mbC.AngularDamping = (0.0f);
            }
            else
            {
                mbC.LinearDamping = (0.1f);
                mbC.AngularDamping = (0.9f);
            }
            //


            //////////////////////////////////////////////
            if (numLinks > 0)
            {
                q0 = 180.0f * Mathf.PI / 180.0f;
                if (!spherical)
                {
                    mbC.SetJointPosMultiDof(0, new float[] { q0 });
                }
                else
                {
                    BulletSharp.Math.Vector3 vv = new BulletSharp.Math.Vector3(1, 1, 0);
                    vv.Normalize();
                    quat0 = new BulletSharp.Math.Quaternion(vv, q0);
                    quat0.Normalize();
                    float[] quat0fs = new float[] { quat0.X, quat0.Y, quat0.Z, quat0.W };
                    mbC.SetJointPosMultiDof(0, quat0fs);
                }
            }
            ///
            BulletSharp.Math.Quaternion[] world_to_local; //btAlignedObjectArray<BulletSharp.Math.Quaternion>
            world_to_local = new BulletSharp.Math.Quaternion[pMultiBody.NumLinks + 1];

            BulletSharp.Math.Vector3[] local_origin; //btAlignedObjectArray<BulletSharp.Math.Vector3>
            local_origin = new BulletSharp.Math.Vector3[pMultiBody.NumLinks + 1];
            world_to_local[0] = pMultiBody.WorldToBaseRot;
            local_origin[0] = pMultiBody.BasePosition;
            //  double friction = 1;
            {

                //	float pos[4]={local_origin[0].x(),local_origin[0].y(),local_origin[0].z(),1};
                // float quat[4]={-world_to_local[0].x(),-world_to_local[0].y(),-world_to_local[0].z(),world_to_local[0].w()};


                if (true)
                {
                    CollisionShape shape = new BoxShape(new BulletSharp.Math.Vector3(baseHalfExtents[0], baseHalfExtents[1], baseHalfExtents[2]));//new btSphereShape(baseHalfExtents[0]);
                                                                                                                                                  // guiHelper.createCollisionShapeGraphicsObject(shape);

                    MultiBodyLinkCollider col = new MultiBodyLinkCollider(pMultiBody, -1);
                    col.CollisionShape = shape;

                    Matrix tr = new Matrix();
                    tr.ScaleVector = BulletSharp.Math.Vector3.One;
                    //if we don't set the initial pose of the btCollisionObject, the simulator will do this 
                    //when syncing the btMultiBody link transforms to the btMultiBodyLinkCollider

                    tr.Origin = local_origin[0];
                    BulletSharp.Math.Quaternion orn = new BulletSharp.Math.Quaternion(new BulletSharp.Math.Vector3(0, 0, 1), 0.25f * 3.1415926538f);

                    tr.Rotation = (orn);
                    col.WorldTransform = (tr);

                    bool isDynamic = (baseMass > 0 && !fixedBase);
                    CollisionFilterGroups collisionFilterGroup = isDynamic ? CollisionFilterGroups.DefaultFilter : CollisionFilterGroups.StaticFilter;
                    CollisionFilterGroups collisionFilterMask = isDynamic ? CollisionFilterGroups.AllFilter : CollisionFilterGroups.AllFilter ^ CollisionFilterGroups.StaticFilter;


                    world.AddCollisionObject(col, collisionFilterGroup, collisionFilterMask);//, 2,1+2);

                    BulletSharp.Math.Vector4 color = new BulletSharp.Math.Vector4(0.0f, 0.0f, 0.5f, 1f);
                    //guiHelper.createCollisionObjectGraphicsObject(col, color);

                    //                col.setFriction(friction);
                    pMultiBody.BaseCollider = (col);

                }
            }


            for (int i = 0; i < pMultiBody.NumLinks; ++i)
            {
                int parent = pMultiBody.GetParent(i);
                world_to_local[i + 1] = pMultiBody.GetParentToLocalRot(i) * world_to_local[parent + 1];
                BulletSharp.Math.Vector3 vv = world_to_local[i + 1].Inverse.Rotate(pMultiBody.GetRVector(i));
                local_origin[i + 1] = local_origin[parent + 1] + vv;
            }


            for (int i = 0; i < pMultiBody.NumLinks; ++i)
            {

                BulletSharp.Math.Vector3 posr = local_origin[i + 1];
                //	float pos[4]={posr.x(),posr.y(),posr.z(),1};

                float[] quat = new float[] { -world_to_local[i + 1].X, -world_to_local[i + 1].Y, -world_to_local[i + 1].Z, world_to_local[i + 1].W };
                CollisionShape shape = null;

                if (i == 0)
                {
                    shape = new BoxShape(new BulletSharp.Math.Vector3(linkHalfExtents[0], linkHalfExtents[1], linkHalfExtents[2]));//btSphereShape(linkHalfExtents[0]);
                }
                else
                {

                    shape = new SphereShape(radius);
                }

                //guiHelper.createCollisionShapeGraphicsObject(shape);
                MultiBodyLinkCollider col = new MultiBodyLinkCollider(pMultiBody, i);

                col.CollisionShape = (shape);
                Matrix tr = new Matrix();
                tr.ScaleVector = new BulletSharp.Math.Vector3();
                tr.Origin = (posr);
                tr.Rotation = (new BulletSharp.Math.Quaternion(quat[0], quat[1], quat[2], quat[3]));
                col.WorldTransform = (tr);
                //       col.setFriction(friction);
                bool isDynamic = true;//(linkMass > 0);
                CollisionFilterGroups collisionFilterGroup = isDynamic ? CollisionFilterGroups.DefaultFilter : CollisionFilterGroups.StaticFilter;
                CollisionFilterGroups collisionFilterMask = isDynamic ? CollisionFilterGroups.AllFilter : CollisionFilterGroups.AllFilter ^ CollisionFilterGroups.StaticFilter;

                //if (i==0||i>numLinks-2)
                {
                    world.AddCollisionObject(col, collisionFilterGroup, collisionFilterMask);//,2,1+2);
                    BulletSharp.Math.Vector4 color = colors[curColor];
                    curColor++;
                    curColor &= 3;
                    //guiHelper.createCollisionObjectGraphicsObject(col, color);


                    pMultiBody.GetLink(i).Collider = col;
                }

            }

            return pMultiBody;
        }

        


        
        /*
        DemoFramework.ISimulation InverseDynamicsExampleCreateFunc(CommonExampleOptions options)
        {
            return new InverseDynamicsExample(options.m_guiHelper, btInverseDynamicsExampleOptions(options.m_option));
        }
        */

    }
}
