using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BulletSharp;
using BulletSharp.Math;
using BulletUnity;
using BulletSharp.InverseDynamics;

public class InverseDynamicsController : MonoBehaviour {

    public class Pose
    {
        public float[] q;
        public float[] qdot;
    }

    BMultiBody m_cachedMultibody;
    MultiBodyTree m_inverseModel;

    Pose startPose;
    Pose endPose;

    static float kp = 10 * 10f;
    static float kd = 2 * 10f;
    
    float[] q;
    float[] qdot;

    float[] desiredQ;
    float[] deisredQdot;
    float[] desiredAcceleration;
    float[] pd_control;

	// Use this for initialization
	void Start () {
        m_cachedMultibody = GetComponent<BMultiBody>();
        startPose = new Pose();
        startPose.q = new float[] { 0f, 0f, 0f };
        startPose.qdot = new float[] { 0f, 0f, 0f };
        endPose = new Pose();
        endPose.q = new float[] { -7f * Mathf.Deg2Rad, -6f * Mathf.Deg2Rad, 13f * Mathf.Deg2Rad };
        endPose.qdot = new float[] { 0f, 0f, 0f };
    }

    void ReadCurrentPose()
    {
        MultiBody mb = m_cachedMultibody.GetMultiBody();
        int numDofs = mb.NumDofs;
        if (q == null || q.Length != numDofs)
        {
            q = new float[numDofs];
            qdot = new float[numDofs];
            desiredAcceleration = new float[numDofs];
            pd_control = new float[numDofs];
            desiredQ = new float[numDofs];
            deisredQdot = new float[numDofs];
        }

        for (int dof = 0; dof < numDofs; dof++)
        {
            q[dof] = mb.GetJointPos(dof);
            qdot[dof] = mb.GetJointVel(dof);
            //Debug.Log("DOF " + dof + " is " + q[dof] * Mathf.Rad2Deg);
        }
    }

    void GetDesiredPose()
    {
        MultiBody mb = m_cachedMultibody.GetMultiBody();
        int baseDofs = mb.HasFixedBase ? 0 : 6;
        int numDofs = mb.NumDofs;
        for (int dof = 0; dof < numDofs; dof++)
        {
            desiredQ[dof] = endPose.q[dof];
            deisredQdot[dof] = endPose.qdot[dof];
        }
    }

    void CalculatedDesiredAccelerations()
    {
        MultiBody mb = m_cachedMultibody.GetMultiBody();
        int numDofs = mb.NumDofs;
        for (int dof = 0; dof < numDofs; dof++)
        {
            // pd_control is either desired joint torque for pd control,
            // or the feedback contribution to nu
            pd_control[dof] = kd * (deisredQdot[dof] - qdot[dof]) + kp * (desiredQ[dof] - q[dof]);
            desiredAcceleration[dof] = pd_control[dof];
        }
    }
	
	// Update is called once per frame
	void FixedUpdate() {
        MultiBody mb = m_cachedMultibody.GetMultiBody();
        
        if (m_inverseModel == null)
        {
            // construct inverse model
            MultiBodyTreeCreator id_creator = new MultiBodyTreeCreator();
            if (-1 == id_creator.CreateFromMultiBody(mb))
            {
                Debug.LogError("error creating tree\n");
            }
            else
            {
                m_inverseModel = id_creator.CreateMultiBodyTree();
            }
            Debug.Log("Created inverse model");
        }


        ReadCurrentPose();
        GetDesiredPose();
        CalculatedDesiredAccelerations();

        int baseDofs = mb.HasFixedBase ? 0 : 6;
        int numDofs = mb.NumDofs;

        float[] qq, qqdot, ddesiredAcceleration;

        if (mb.HasFixedBase)
        {
            qq = q;
            qqdot = qdot;
            ddesiredAcceleration = desiredAcceleration;
        } else
        {
            // inverseModel stores dofs for the base if floating base. Need to prepad to handle this.
            qq = new float[baseDofs + numDofs];
            qqdot = new float[baseDofs + numDofs];
            ddesiredAcceleration = new float[baseDofs + numDofs];
            for (int i = 0; i < numDofs; i++)
            {
                qq[i + baseDofs] = q[i];
                qqdot[i + baseDofs] = qdot[i];
                ddesiredAcceleration[i + baseDofs] = desiredAcceleration[i];
            }
        }

        float[] joint_force = new float[numDofs + baseDofs]; 
        if (-1 != m_inverseModel.CalculateInverseDynamics(mb.HasFixedBase, qq, qqdot, ddesiredAcceleration, joint_force))
        {
            // use inverse model: apply joint force corresponding to
            // desired acceleration nu. for floating base will have been padded by baseDofs
            for (int dof = 0; dof < numDofs; dof++)
            {
                mb.AddJointTorque(dof, joint_force[dof + baseDofs]);
            }
        }
        else
        {
            Debug.LogError("Bad return from CalculateInverseDynamics");
        }
    }
}
