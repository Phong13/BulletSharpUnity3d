using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BulletUnity;
using BulletSharp;
using BulletSharp.SoftBody;

[Serializable]
public class BSoftBodyRopeToSkinnedMesh : BSoftBody
{
    SkinnedMeshRenderer skinnedMesh;

    [Serializable]
    public class RopeEdge
    {
        public RopeNode RopeNodeA;
        public RopeNode RopeNodeB;
    }

    [Serializable]
    public class RopeNode
    {
        public int idxSrcMesh;
        public int idxReducedMesh;
        public float anchorStrength;
        public Vector3 localPos;
    }

    [Serializable]
    public class BoneAndNode
    {
        public Transform bone;
        public int nodeIdx;
    }

    [Header("Mapping Bones To Physics Sim Mesh Verts Settings")]
    public float radius = .0001f;
    public MeshFilter physicsSimMesh;
    public BRigidBody anchorRigidBody;

    public bool debugDisplaySimulatedMesh;
    public bool debugShowMappedBoneGizmos;

    [SerializeField]
    BoneAndNode[] bone2idxMap;

    Mesh myMesh; //used for debugging if I want to display the the mesh distortions
    Vector3[] localVerts;
    Vector3[] localNorms;

    [SerializeField]
    RopeNode[] ropeNodes;
    [SerializeField]
    RopeEdge[] ropeEdges;

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
        int[] tris = m.triangles;
        RopeNode[] ropeVerts = new RopeNode[verts.Length];
        if (cols.Length != verts.Length)
        {
            Debug.LogError("The physics sim mesh had no vertex colors. Vertex colors are needed to identify the anchor bones.");
        }

        //build ropes this is a hack because Unity will not import line meshes.
        //Work around this by creating a triangle mesh and marking the lines with vertexColor.b > .5f
        List<RopeNode> nodes = new List<RopeNode>();
        List<RopeEdge> edges = new List<RopeEdge>();
        // create a set of verts with blue channel > .5f 
        for (int i = 0; i < verts.Length; i++)
        {
            if (cols[i].b > .5f)
            {
                RopeNode r = new RopeNode();
                r.idxSrcMesh = i;
                nodes.Add(r);
                ropeVerts[i] = r;
                r.anchorStrength = cols[i].r;
                r.localPos = verts[i];
            } else
            {
                ropeVerts[i] = null;
            }
        }
        // traverse triangles and find edges with both ends in set
        for (int i = 0; i < tris.Length; i+=3)
        {
            _AddEdgeIfExists(tris[i],tris[i+1], ropeVerts, edges);
            _AddEdgeIfExists(tris[i+1], tris[i + 2], ropeVerts, edges);
            _AddEdgeIfExists(tris[i+2], tris[i + 0], ropeVerts, edges);
        }
        ropeNodes = nodes.ToArray();
        ropeEdges = edges.ToArray();

        for (int i = 0; i < ropeNodes.Length; i++)
        {
            ropeNodes[i].idxReducedMesh = i;
        }

        int anchorCount = 0;
        List<BoneAndNode> foundMatches = new List<BoneAndNode>();
        for (int i = 0; i < ropeNodes.Length; i++)
        {
            for (int j = 0; j < bones.Length; j++)
            {
                Vector3 worldSpaceVert = physicsSimMesh.transform.TransformPoint(verts[ropeNodes[i].idxSrcMesh]);
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
            if (ropeNodes[i].anchorStrength >= .5f) anchorCount++;
        }
        bone2idxMap = foundMatches.ToArray();

        Debug.LogFormat("Done Building Bone To Node Index Map. Found: {0} bones, {1} anchors", bone2idxMap.Length, anchorCount);
	}

    void _AddEdgeIfExists(int a, int b, RopeNode[] ropeVerts, List<RopeEdge> edges)
    {
        if (ropeVerts[a] != null && ropeVerts[b] != null)
        {
            RopeEdge re = new RopeEdge();
            re.RopeNodeA = ropeVerts[a];
            re.RopeNodeB = ropeVerts[b];
            edges.Add(re);
        }
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
        if (debugDisplaySimulatedMesh)
        {
            Gizmos.color = Color.blue;
            DumpDataFromBullet();
            if (m_collisionObject != null && Application.isPlaying)
            {
                for (int i = 0; i < ropeEdges.Length; i++)
                {
                    Gizmos.DrawLine(verts[ropeEdges[i].RopeNodeA.idxReducedMesh], verts[ropeEdges[i].RopeNodeB.idxReducedMesh]);
                }
            }
            else {
                for (int i = 0; i < ropeEdges.Length; i++)
                {
                    Vector3 a = physicsSimMesh.transform.TransformPoint(ropeEdges[i].RopeNodeA.localPos);
                    Vector3 b = physicsSimMesh.transform.TransformPoint(ropeEdges[i].RopeNodeB.localPos);
                    Gizmos.DrawLine(a, b);
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
        if (ropeNodes == null || ropeNodes.Length == 0)
        {
            Debug.LogError("No ropeNodes have been found. Soft body will not be attached to rigidBodyAnchor anywhere " + name);
        }
        if (anchorRigidBody == null)
        {
            Debug.LogError("No anchor rigid body has been set for object " + name);
        }

        //-----------
        BulletSharp.Math.Vector3[] x = new BulletSharp.Math.Vector3[ropeNodes.Length];
        float[] m = new float[ropeNodes.Length];

        for (int i = 0; i < ropeNodes.Length; i++)
        {
            x[i] = physicsSimMesh.transform.TransformPoint(ropeNodes[i].localPos).ToBullet();
            //todo
            m[i] = .1f;
        }
        SoftBody m_BSoftBody = new SoftBody(World.WorldInfo, ropeNodes.Length, x, m);
        // Create links
        for (int i = 1; i < ropeEdges.Length; i++)
        {
            m_BSoftBody.AppendLink(ropeEdges[i].RopeNodeA.idxReducedMesh, ropeEdges[i].RopeNodeB.idxReducedMesh);
        }

        Debug.Log(m_BSoftBody.Nodes.Count + " " + ropeNodes.Length);

        m_collisionObject = m_BSoftBody;

        verts = new Vector3[m_BSoftBody.Nodes.Count];
        norms = new Vector3[m_BSoftBody.Nodes.Count];

        for (int i = 0; i < m_BSoftBody.Nodes.Count; i++)
        {
            verts[i] = m_BSoftBody.Nodes[i].Position.ToUnity();
            norms[i] = m_BSoftBody.Nodes[i].Normal.ToUnity();
        }

        //Set SB settings
        SoftBodySettings.ConfigureSoftBody(m_BSoftBody);

        for(int i = 0; i < ropeNodes.Length; i++)
        {
            //anchorNode point 0 to 1, rounds to node # 
            //int node = (int)Mathf.Floor(Mathf.Lerp(0, m_BSoftBody.Nodes.Count - 1, anchor.anchorNodePoint));

            //if (anchor.body != null)
            if (ropeNodes[i].anchorStrength > .5f)
            {

                m_BSoftBody.AppendAnchor(i, (RigidBody)anchorRigidBody.GetCollisionObject(),true);
            }
            //else
            //{
            //    m_BSoftBody.SetMass(node, 0);  //setting node mass to 0 fixes it in space apparently
            //}

        }
        
        //-----------

        //Set SB position to GO position
        //m_BSoftBody.Rotate(physicsSimMesh.transform.rotation.ToBullet());
        //m_BSoftBody.Translate(physicsSimMesh.transform.position.ToBullet());
        //m_BSoftBody.Scale(physicsSimMesh.transform.localScale.ToBullet());

        MeshRenderer mr = physicsSimMesh.GetComponent<MeshRenderer>();
        if (mr != null)
        {
            mr.enabled = false;
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
