using UnityEngine;
using System.Collections;
using BulletUnity;

public class TestTeleportJump : MonoBehaviour {

    public Vector3 jump = new Vector3(0f, 10f, 20f);
    public Transform root;
    public BSoftBodyPartOnSkinnedMesh[] softBodyParts;
    public BRigidBody[] dynamicAnchors; 

    public void OnGUI()
    {
        if (GUILayout.Button("Teleport jump"))
        {
            root.transform.position += new Vector3(0f, 10f, 20f);
            for (int i = 0; i < softBodyParts.Length; i++)
            {
                softBodyParts[i].ResetNodesAfterTeleportJump(jump);
            }
            for (int i = 0; i < dynamicAnchors.Length; i++)
            {
                BulletSharp.Math.Matrix m = dynamicAnchors[i].GetCollisionObject().WorldTransform;
                m.Origin += jump.ToBullet();
                dynamicAnchors[i].GetCollisionObject().WorldTransform = m;
            }
        }
    }
}
