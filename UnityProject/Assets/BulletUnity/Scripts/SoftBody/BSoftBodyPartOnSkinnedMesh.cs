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
    public SkinnedMeshRenderer skinnedMesh;

    [Serializable]
    public class BAnchor
    {
        public BRigidBody anchorRigidBody;
        public float colRangeFrom = 0f;
        public float colRangeTo = 1f;
        public List<int> anchorNodeIndexes = new List<int>();
        public List<float> anchorNodeStrength = new List<float>();
        public List<Vector3> anchorPosition = new List<Vector3>();
    }

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
    MeshFilter physicsSimMesh;
    public BAnchor[] anchors;

    public bool debugDisplaySimulatedMesh;
    public bool debugShowMappedBoneGizmos;
    public bool debugShowMappedAnchors;

    [SerializeField]
    BoneAndNode[] bone2idxMap;

    Vector3[] bindPoseNormal;

    Mesh myMesh; //used for debugging if I want to display the the mesh distortions
    Vector3[] localVerts;
    Vector3[] localNorms;

    [ContextMenu("Build Bone 2 Node Map")]
    // Use this for initialization
    void BuildBoneToNodeIdxMap() {
        if (skinnedMesh == null)
        {
            Debug.LogError("must be attached to a skinned mesh");
        }

        physicsSimMesh = GetComponent<MeshFilter>();
        if (physicsSimMesh == null)
        {
            Debug.LogError("Must be attached to an object with a MeshRenderer");
        }

        if (physicsSimMesh == null)
        {
            Debug.LogError("must add the physics sim mesh bone");
        }

        //get bones and mesh verts
        //compare these in world space to see which ones line up
        //TODO warn if physicsSimMesh has split vertices
        //TODO validate anchors
        //TODO why does other mesh shape work better than mine.
        Transform[] bones = skinnedMesh.bones;
        Mesh m = physicsSimMesh.sharedMesh;
        Vector3[] verts = m.vertices;
        Vector3[] norms = m.normals;
        //todo make list of which UV map
        Color[] cols = m.colors;
        if (cols.Length != verts.Length)
        {
            Debug.LogError("The physics sim mesh had no colors. Colors are needed to identify the anchor bones.");
        }
        //check for duplicate verts
        int numDuplicated = 0;
        for (int i = 0; i < verts.Length; i++)
        {
            for (int j = i+1; j < verts.Length; j++)
            {
                if (verts[i] == verts[j])
                {
                    numDuplicated++;
                }
            }
        }
        if (numDuplicated > 0)
        {
            Debug.LogError("The physics sim mesh has " + numDuplicated + " duplicated vertices. Check that the mesh does not have hard edges and that there are no UVs.");
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
        int numAnchorNodes = 0;
        List<int> foundMatchesNodes = new List<int>();
        for (int i = 0; i < cols.Length; i++)
        {
            for (int j = 0; j < anchors.Length; j++)
            {
                if (cols[i].g > anchors[j].colRangeFrom &&
                    cols[i].g < anchors[j].colRangeTo)
                {
                    anchors[j].anchorNodeIndexes.Add(i);
                    anchors[j].anchorNodeStrength.Add(cols[i].r);
                    anchors[j].anchorPosition.Add(verts[i]);
                    numAnchorNodes++;
                }
            }
        }

        Debug.LogFormat("Done Building Bone To Node Index Map. Found: {0} bones and {1} anchor nodes.", bone2idxMap.Length, numAnchorNodes);
	}

    public void OnDrawGizmosSelected()
    {
        
        if (debugShowMappedBoneGizmos)
        {
            if (norms.Length > 0 && bone2idxMap.Length > 0) {
                Gizmos.color = Color.blue;
                for (int i = 0; i < bone2idxMap.Length; i++)
                {
                    if (bone2idxMap[i].bone != null) {
                        //Gizmos.DrawWireSphere(bone2idxMap[i].bone.transform.position, .1f);
                        Gizmos.DrawRay(bone2idxMap[i].bone.position, bone2idxMap[i].bindNormal);
                        Gizmos.DrawRay(bone2idxMap[i].bone.position, norms[bone2idxMap[i].nodeIdx]);
                    }
                }
            }
        }
        if (debugShowMappedAnchors)
        {
            Gizmos.color = Color.blue;
            for (int i = 0; i < anchors.Length; i++)
            {
                for (int j = 0; j < anchors[i].anchorNodeIndexes.Count; j++)
                {
                    Vector3 pos = transform.TransformPoint(anchors[i].anchorPosition[j]);
                    Gizmos.DrawWireSphere(pos, .1f);
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
        for (int i = 0; i < anchors.Length; i++)
        {
            if (anchors[i].anchorRigidBody == null)
            {
                Debug.LogError("No anchor rigid body has been set for anchor " + i);
            }
            if (anchors[i].anchorNodeIndexes == null || anchors[i].anchorNodeIndexes.Count == 0)
            {
                Debug.LogError("No nodes have been identified as anchors. Soft body will not be attached to RigidBody anchor " + anchors[i].anchorRigidBody);
            }
        }

        if (physicsSimMesh == null) physicsSimMesh = GetComponent<MeshFilter>();
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

        for (int i = 0; i < anchors.Length; i++)
        {
            BAnchor a = anchors[i];
            for (int j = 0; j < a.anchorNodeIndexes.Count; j++)
            {
                m_BSoftBody.AppendAnchor(a.anchorNodeIndexes[j], (RigidBody) a.anchorRigidBody.GetCollisionObject(), false, a.anchorNodeStrength[j]);
            }
        }

        MeshRenderer mr = physicsSimMesh.GetComponent<MeshRenderer>();
        if (mr != null)
        {
            if (debugDisplaySimulatedMesh)
            {
                mr.enabled = true;
            }
            else
            {
                mr.enabled = false;
            }
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
        if (isInWorld)
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
