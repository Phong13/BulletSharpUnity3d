using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;
using System;
using PyBullet;
[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]

//a robot is a list of links and a list of joints
//each link has a visual shape that represents the link
//load the URDF,
// create the visual shapes for each link
// Robot
//   VS0
//     cube, cube, sphere
//   VS1
//     cube, sphere
//   VS2
//     mesh blaa
//   VS3
//     
// get the number of joints/links
// call getLinkStates to 

    public struct PositionRotation
    {
        public double x, y, z;
        public double qx, qy, qz, qw;
    }

public class VisualShape
{
    public GameObject[] shapes;
}

public class Robot
{
    public int bodyIndex;
    
}

public class PhysicsCommand
{
    IntPtr _native;

    internal PhysicsCommand(IntPtr ip) {
        _native = ip;
    }
}

public class CommandStatus
{
    IntPtr _native;

    internal CommandStatus(IntPtr ip)
    {
        _native = ip;
    }
}

public class PhysicsClient : IDisposable
{


    internal IntPtr _native;

    public static PhysicsClient CreatePhysicsClientSharedMemory()
    {
        PhysicsClient pc = new PhysicsClient();
        pc._native = NativeMethods.b3ConnectSharedMemory(NativeConstants.SHARED_MEMORY_KEY);
        return pc;
    }

    public static PhysicsClient CreatePhysicsClientDirect()
    {
        PhysicsClient pc = new PhysicsClient();
        pc._native = NativeMethods.b3ConnectPhysicsDirect();
        return pc;
    }

    public bool CanSubmitCommand()
    {
        return NativeMethods.b3CanSubmitCommand(_native) != 0;
    }

    public PhysicsCommand InitResetSimulation()
    {
        IntPtr cmd = NativeMethods.b3InitResetSimulationCommand(_native);
        return new PhysicsCommand(cmd);
    }

    public CommandStatus SubmitClientCommandAndWaitStatus(PhysicsClient command)
    {
        IntPtr cmd = NativeMethods.b3SubmitClientCommandAndWaitStatus(_native, command._native);
        return new CommandStatus(cmd);
    }

    public void SetGravity(Vector3 gravity)
    {
        IntPtr command = NativeMethods.b3InitPhysicsParamCommand(_native);
        NativeMethods.b3PhysicsParamSetGravity(command, gravity.x, gravity.y, gravity.z);
        IntPtr statusHandle = NativeMethods.b3SubmitClientCommandAndWaitStatus(_native, command);
    }

    
    //TODO deal with loading status
    public int LoadUrdfAndGetBodyIdx(string modelName, Vector3 p, Quaternion qq)
    {
        IntPtr cmd = NativeMethods.b3LoadUrdfCommandInit(_native, modelName);
        //IntPtr cmd = NativeMethods.b3LoadMJCFCommandInit(_native, modelName);

        NativeMethods.b3LoadUrdfCommandSetStartOrientation(cmd, qq.x, qq.y, qq.z, qq.w);
        NativeMethods.b3LoadUrdfCommandSetStartPosition(cmd, p.x, p.y, p.z);
        IntPtr status = NativeMethods.b3SubmitClientCommandAndWaitStatus(_native, cmd);
        int bodyIdx = NativeMethods.b3GetStatusBodyIndex(status);
        Debug.Log("body index " + bodyIdx);
        cmd = NativeMethods.b3InitRequestVisualShapeInformation(_native, bodyIdx);
        status = NativeMethods.b3SubmitClientCommandAndWaitStatus(_native, cmd);
        EnumSharedMemoryServerStatus statusType = (EnumSharedMemoryServerStatus)NativeMethods.b3GetStatusType(status);
        if (statusType == EnumSharedMemoryServerStatus.CMD_VISUAL_SHAPE_INFO_COMPLETED ||
            statusType == EnumSharedMemoryServerStatus.CMD_VISUAL_SHAPE_UPDATE_COMPLETED)
        {
            b3VisualShapeInformation visuals = new b3VisualShapeInformation();
            NativeMethods.b3GetVisualShapeInformation(_native, ref visuals);
            Debug.Log("visuals.m_numVisualShapes=" + visuals.m_numVisualShapes);
            int numVisualShapes = visuals.m_numVisualShapes;
            System.IntPtr visualPtr = visuals.m_visualShapeData;

            //robot.bodies = new Body[visuals.m_numVisualShapes];

            for (int s = 0; s < visuals.m_numVisualShapes; s++)
            {
                b3VisualShapeData visual = (b3VisualShapeData)Marshal.PtrToStructure(visualPtr, typeof(b3VisualShapeData));
                visualPtr = new IntPtr(visualPtr.ToInt64() + (Marshal.SizeOf(typeof(b3VisualShapeData))));
                Vector3 scale;
                scale.x = (float)visual.m_dimensions[0];
                scale.y = (float)visual.m_dimensions[1];
                scale.z = (float)visual.m_dimensions[2];

                Debug.Log("visual.m_visualGeometryType =" + (eUrdfGeomTypes)visual.m_visualGeometryType +
                           " linkIndex=" + visual.m_linkIndex + " id=" + visual.m_objectUniqueId);

                Vector3 pos;
                pos.x = (float)visual.m_localVisualFrame[0];
                pos.y = (float)visual.m_localVisualFrame[1];
                pos.z = (float)visual.m_localVisualFrame[2];
                Quaternion rot;
                rot.x = (float)visual.m_localVisualFrame[3];
                rot.y = (float)visual.m_localVisualFrame[4];
                rot.z = (float)visual.m_localVisualFrame[5];
                rot.w = (float)visual.m_localVisualFrame[6];

                //robot.bodies[s] = CreateShape(visual, "cube", pos, rot, scale);

                if (visual.m_visualGeometryType == (int)eUrdfGeomTypes.GEOM_MESH)
                {
                    Debug.Log("visual.m_meshAssetFileName=" + visual.m_meshAssetFileName);

                }
            }
        }

        int numJoints = NativeMethods.b3GetNumJoints(_native, bodyIdx);

        Dictionary<string, int> jointNameToId = new Dictionary<string, int>();
        for (int i = 0; i < numJoints; i++) {
            b3JointInfo jointInfo = new b3JointInfo();
            NativeMethods.b3GetJointInfo(_native,bodyIdx, i, ref jointInfo);
            jointNameToId.Add(jointInfo.m_jointName, i);
            Debug.Log("Joint name " + jointInfo.m_jointName + " Link name " + jointInfo.m_linkName);
        }

        //NativeMethods.collisionshape(_native, bodyIdx);

        return bodyIdx;
    }

    public int NumBodies()
    {
        return NativeMethods.b3GetNumBodies(_native);
    }

    public void RequestActualState(int bodyId, out Vector3 position, out Quaternion rotation)
    {
        IntPtr cmd_handle = NativeMethods.b3RequestActualStateCommandInit(_native, bodyId);
        IntPtr status_handle = NativeMethods.b3SubmitClientCommandAndWaitStatus(_native, cmd_handle);
        
        EnumSharedMemoryServerStatus status_type = (EnumSharedMemoryServerStatus)NativeMethods.b3GetStatusType(status_handle);
        //Debug.Log("request status " + status_type + " body " + bodyId);
        if (status_type == EnumSharedMemoryServerStatus.CMD_ACTUAL_STATE_UPDATE_COMPLETED)
        {
        IntPtr p = IntPtr.Zero;
            int objUid = 0;
            int numDofQ = 0;
            int numDofU = 0;
            IntPtr inertialFrame = IntPtr.Zero;
            IntPtr actualStateQ = IntPtr.Zero;
            IntPtr actualStateQdot = IntPtr.Zero;
            IntPtr joint_reaction_forces = IntPtr.Zero;

            NativeMethods.b3GetStatusActualState(
                status_handle, ref objUid, ref numDofQ, ref numDofU,
                ref inertialFrame, ref actualStateQ,
                ref actualStateQdot, ref joint_reaction_forces);
            PositionRotation mpos = (PositionRotation)Marshal.PtrToStructure(actualStateQ, typeof(PositionRotation));
            position = new Vector3((float)mpos.x, (float)mpos.y, (float)mpos.z);
            rotation = new Quaternion((float)mpos.qx, (float)mpos.qy, (float)mpos.qz, (float)mpos.qw);
        } else
        {
            position = Vector3.zero;
            rotation = Quaternion.identity;
        }
    }

    public void StepSimulation()
    {
        IntPtr cmd = NativeMethods.b3InitStepSimulationCommand(_native);
        IntPtr status = NativeMethods.b3SubmitClientCommandAndWaitStatus(_native, cmd);
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
            NativeMethods.b3DisconnectSharedMemory(_native);
            _native = IntPtr.Zero;
        }
    }

    ~PhysicsClient()
    {
        Dispose(false);
    }
}

public class NewBehaviourScript : MonoBehaviour {
    public GameObject[] PrimitivePrefabs;

    public class Robot1
    {
        public int bodyIndex;
        public Transform unityProxy;
    }

    PhysicsClient pybullet;
    Robot1 cubeBody;
    Robot1 planeBody;
    List<Robot1> bodies;

    public Robot1 CreateRobot(string modelName, Vector3 p, Quaternion qq)
    {
        /*
        GameObject prefab = null;
        for (int i = 0; i < PrimitivePrefabs.Length; i++)
        {
            if (PrimitivePrefabs[i].name == modelName)
            {
                prefab = PrimitivePrefabs[i];
            }
        }
        if (prefab == null)
        {
            Debug.LogError("Could not find a prefab named " + modelName);
            return null;
        }
        */
        Robot1 bdy = new Robot1();
        bdy.bodyIndex = pybullet.LoadUrdfAndGetBodyIdx(modelName + ".urdf", p, qq);
        if (bdy.bodyIndex != -1)
        {
            /*
            bdy.unityProxy = GameObject.Instantiate<GameObject>(prefab).transform;
            bdy.unityProxy.position = p;
            bdy.unityProxy.rotation = qq;
            */
            return bdy;
        } else
        {
            Debug.LogError("Error creating bullet primitive for  " + modelName);
        }
        return null;
    }

    // Use this for initialization
    void Start () {
        Debug.Log("start begin");
        Debug.Log("bb");
        bodies = new List<Robot1>();
        pybullet = PhysicsClient.CreatePhysicsClientSharedMemory();
        if (!pybullet.CanSubmitCommand())
        {
            pybullet = PhysicsClient.CreatePhysicsClientDirect();
        }
        pybullet.SetGravity(Physics.gravity);

        Robot1 cube = CreateRobot("quadruped/minitaur", new Vector3(0, 15, 0), Quaternion.Euler(35, 0, 0));
        //Robot1 plane = CreateRobot("plane", Vector3.zero, Quaternion.Euler(-90, 0, 0));
        bodies.Add(cube);
        //bodies.Add(plane);
    }

    void LateUpdate () {
        pybullet.StepSimulation();
        for (int i = 0; i < bodies.Count; i++)
        {
            Vector3 p;
            Quaternion q;
            pybullet.RequestActualState(bodies[i].bodyIndex, out p, out q);
            bodies[i].unityProxy.position = p;
            bodies[i].unityProxy.rotation = q;
        }
    }

    void OnDestroy()
    {
        pybullet.Dispose();
    }
}
