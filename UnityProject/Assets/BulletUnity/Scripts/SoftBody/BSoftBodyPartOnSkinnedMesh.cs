using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BulletUnity;
using BulletSharp;
using BulletSharp.SoftBody;

[Serializable]
public class BSoftBodyPartOnSkinnedMesh : BSoftBody
{
    SkinnedMeshRenderer skinnedMesh;

    [Serializable]
    public class BoneAndNode
    {
        public Transform bone;
        public int nodeIdx;
        //vertex bind normal
        public Vector3 bindNormal;
        public Quaternion bindBoneRotation;
    }

    [Header("Mapping Bones To Physics Sim Mesh Verts Settings")]
    public float radius = .0001f;
    public MeshFilter physicsSimMesh;
    public BRigidBody anchorRigidBody;

    public bool debugDisplaySimulatedMesh;
    public bool debugShowMappedBoneGizmos;

    [SerializeField]
    BoneAndNode[] bone2idxMap;
    [SerializeField]
    int[] anchorNodeIndexes;

    Vector3[] bindPoseNormal;

    Mesh myMesh; //used for debugging if I want to display the the mesh distortions
    Vector3[] localVerts;
    Vector3[] localNorms;

    [ContextMenu("Build Bone 2 Node Map")]
    // Use this for initialization
    void BuildBoneToNodeIdxMap() {
        skinnedMesh = GetComponent<SkinnedMeshRenderer>();
        if (skinnedMesh == null)
        {
            Debug.LogError("must be attached to a skinned mesh");
        }

        if (physicsSimMesh == null)
        {
            Debug.LogError("must add the physics sim mesh bone");
        }

        //get bones and mesh verts
        //compare these in world space to see which ones line up
        //TODO warn if physicsSimMesh has split vertices
        Transform[] bones = skinnedMesh.bones;
        Mesh m = physicsSimMesh.sharedMesh;
        Vector3[] verts = m.vertices;
        Vector3[] norms = m.normals;
        Color[] cols = m.colors;
        if (cols.Length != verts.Length)
        {
            Debug.LogError("The physics sim mesh had no vertex colors. Vertex colors are needed to identify the anchor bones.");
        }
        List<BoneAndNode> foundMatches = new List<BoneAndNode>();
        for (int i = 0; i < verts.Length; i++)
        {
            for (int j = 0; j < bones.Length; j++)
            {
                Vector3 worldSpaceVert = physicsSimMesh.transform.TransformPoint(verts[i]);
                Vector3 worldSpaceBone = bones[j].position;
                if (Vector3.Distance(worldSpaceBone, worldSpaceVert) < radius)
                {
                    Debug.Log("found a bone that is aligned with a vertex " + bones[j]);
                    BoneAndNode ban = new BoneAndNode();
                    ban.bone = bones[j];
                    ban.nodeIdx = i;
                    foundMatches.Add(ban);
                }
            }
        }
        bone2idxMap = foundMatches.ToArray();
        List<int> foundMatchesNodes = new List<int>();
        for (int i = 0; i < cols.Length; i++)
        {
            if (cols[i].r < .2f && cols[i].g < .2f && cols[i].b < .2f)
            {
                foundMatchesNodes.Add(i);
            }
        }
        anchorNodeIndexes = foundMatchesNodes.ToArray();

        Debug.LogFormat("Done Building Bone To Node Index Map. Found: {0} bones and {1} anchor nodes", bone2idxMap.Length, anchorNodeIndexes.Length);
	}

    public void OnDrawGizmosSelected()
    {
        if (debugShowMappedBoneGizmos)
        {
            Gizmos.color = Color.blue;
            for (int i = 0; i < bone2idxMap.Length; i++)
            {
                if (bone2idxMap[i].bone != null) {
                    Gizmos.DrawWireSphere(bone2idxMap[i].bone.transform.position, .1f);
                }
            }
        }
    }

    internal override bool _BuildCollisionObject()
    {
        if (World == null)
        {
            return false;
        }
        if (bone2idxMap == null || bone2idxMap.Length == 0)
        {
            Debug.LogError("No bones have been mapped to soft body nodes for object " + name);
        }
        if (anchorNodeIndexes == null || anchorNodeIndexes.Length == 0)
        {
            Debug.LogError("No nodes have been identified as anchors. Soft body will not be attached to rigidBodyAnchor anywhere " + name);
        }
        if (anchorRigidBody == null)
        {
            Debug.LogError("No anchor rigid body has been set for object " + name);
        }

        Mesh mesh = physicsSimMesh.sharedMesh;

        //convert the mesh data to Bullet data and create DoftBody
        BulletSharp.Math.Vector3[] bVerts = new BulletSharp.Math.Vector3[mesh.vertexCount];
        Vector3[] verts = mesh.vertices;
        for (int i = 0; i < mesh.vertexCount; i++)
        {
            bVerts[i] = verts[i].ToBullet();
        }

        SoftBody m_BSoftBody = SoftBodyHelpers.CreateFromTriMesh(World.WorldInfo, bVerts, mesh.triangles);
        m_collisionObject = m_BSoftBody;
        SoftBodySettings.ConfigureSoftBody(m_BSoftBody);         //Set SB settings

        //Set SB position to GO position
        m_BSoftBody.Rotate(physicsSimMesh.transform.rotation.ToBullet());
        m_BSoftBody.Translate(physicsSimMesh.transform.position.ToBullet());
        m_BSoftBody.Scale(physicsSimMesh.transform.localScale.ToBullet());

        if (anchorRigidBody != null)
        {
            for (int i = 0; i < anchorNodeIndexes.Length; i++)
            {
                m_BSoftBody.AppendAnchor(anchorNodeIndexes[i], (RigidBody)anchorRigidBody.GetCollisionObject(), false);
            }
        }

        MeshRenderer mr = physicsSimMesh.GetComponent<MeshRenderer>();
        if (mr != null)
        {
            mr.enabled = false;
        }

        if (norms.Length == 0 || norms.Length != verts.Length)
        {
            norms = new Vector3[m_BSoftBody.Nodes.Count];
        }
        for (int i = 0; i < m_BSoftBody.Nodes.Count; i++)
        {
            norms[i] = m_BSoftBody.Nodes[i].Normal.ToUnity();
        }
        for (int i = 0; i < bone2idxMap.Length; i++)
        {
            bone2idxMap[i].bindNormal = norms[bone2idxMap[i].nodeIdx];
            bone2idxMap[i].bindBoneRotation = bone2idxMap[i].bone.rotation;
        }

        return true;
    }

    public void LateUpdate()
    {

        // read the positions of the bones from the physics simulation
        DumpDataFromBullet(); 
        //Update bone positions based on bullet data
        for (int i = 0; i < bone2idxMap.Length; i++)
        {
            bone2idxMap[i].bone.position = verts[bone2idxMap[i].nodeIdx];
            Quaternion q = Quaternion.FromToRotation(bone2idxMap[i].bindNormal, norms[bone2idxMap[i].nodeIdx]);
            bone2idxMap[i].bone.rotation = bone2idxMap[i].bindBoneRotation * q;
        }

        if (debugDisplaySimulatedMesh)
        {
            if (myMesh == null)
            {
                myMesh = GameObject.Instantiate<Mesh>(physicsSimMesh.sharedMesh);
                MeshFilter mf = physicsSimMesh.GetComponent<MeshFilter>();
                mf.sharedMesh = myMesh;
            }
            if (localVerts == null || localVerts.Length != verts.Length)
            {
                localVerts = new Vector3[verts.Length];
                localNorms = new Vector3[norms.Length];
            }
            for (int i = 0; i < verts.Length; i++)
            {
                localVerts[i] = physicsSimMesh.transform.InverseTransformPoint(verts[i]);
                localNorms[i] = physicsSimMesh.transform.InverseTransformDirection(norms[i]);
            }
            myMesh.vertices = localVerts;
            myMesh.normals = localVerts;
            myMesh.RecalculateBounds();
        }
    }

    public override void UpdateMesh()
    {
        //do nothing since we arn't updating the mesh, we are updating the bones
    }

    public override void Update()
    {
        //don't do anything here overriding to disable the default behavior
    }

    protected override void Dispose(bool isdisposing)
    {
        base.Dispose(isdisposing);
        if (myMesh != null)
        {
            Destroy(myMesh);
        }
    }
}
