using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BulletUnity;
using BulletSharp;
using BulletSharp.SoftBody;

//TODO preset must be shapeMatching
[Serializable]
public class BSoftBodyPartOnSkinnedMesh : BSoftBody
{
    public SkinnedMeshRenderer skinnedMesh;

    [Serializable]
    public class BAnchor
    {
        [Tooltip("A Bullet Physics rigid body")]
        public BRigidBody anchorRigidBody;
        [Tooltip("A range in the green channel. Vertices with a vertex color green value in this range will be bound to this anchor")]
        public float colRangeFrom = 0f;
        [Tooltip("A range in the green channel. Vertices with a vertex color green value in this range will be bound to this anchor")]
        public float colRangeTo = 1f;
        [HideInInspector]
        public List<int> anchorNodeIndexes = new List<int>();
        [HideInInspector]
        public List<float> anchorNodeStrength = new List<float>();
        [HideInInspector]
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

        //we need to track the edges leaving this node so we can fully orient the bone
        public Edge[] edges;
    }

    [Serializable]
    public class Edge: IComparable<Edge>
    {
        public int nodeIdx;
        public float r_angleFromPlane;
        public Vector3 edgeXnorm; //normalized cross product of edge with normal
        public Vector3 bindEdgeXnorm; //in world space,

        public Edge(int p1, int p2, Vector3[] normals, Vector3[] verts)
        {
            nodeIdx = p2;
            Vector3 e = verts[p2] - verts[p1];
            edgeXnorm = Vector3.Cross(e, normals[p1]);
            float sinTheta;
            if (e.magnitude < 10e-8f)
            {
                sinTheta = 0;
                r_angleFromPlane = Mathf.PI / 2f;
            } else {
                sinTheta = Mathf.Clamp(edgeXnorm.magnitude / (e.magnitude * normals[p1].magnitude), -1f, 1f);
                if (sinTheta > 1 || sinTheta < -1)
                {
                    Debug.LogError("Should never get here " + sinTheta.ToString("f15"));
                }
                r_angleFromPlane = Mathf.Abs(Mathf.Asin(sinTheta) - Mathf.PI/2);
            }
            edgeXnorm.Normalize();
        }

        public override bool Equals(object obj)
        {
            if (obj is Edge)
            {
                Edge o = (Edge)obj;
                if (o.nodeIdx == nodeIdx)
                {
                    return true;
                }
            }
            return false;
        }

        public override int GetHashCode()
        {
            return nodeIdx;
        }

        //want to choose edges that are closest to 90 degrees with Normal first
        public int CompareTo(Edge e)
        {
            return (int) Mathf.Sign(r_angleFromPlane - e.r_angleFromPlane);
        }
    }

    [Header("Debug Display")]
    [Tooltip("Render low poly soft body mesh in scene")]
    public bool debugDisplaySimulatedMesh;
    [Tooltip("Show bones that have been bound to the soft body mesh")]
    public bool debugDisplayMappedBoneGizmos;
    [Tooltip("Show anchor nodes in the soft body mesh")]
    public bool debugDisplayMappedAnchors;

    public Vector3 debugSimMeshOffset = Vector3.zero;

    [Header("Binding Bones To Soft Body Mesh Nodes Settings")]
    [Tooltip("Bones that are within 'radius' of soft body mesh vertices will be bound to those vertices")]
    public float radius = .0001f;
    MeshFilter physicsSimMesh;
    [Tooltip("Anchors are Bullet rigid bodies that some soft body nodes/vertices have been bound to. Vertex colors in the Soft Body mesh are used " +
             " to map the nodes/vertices to the anchors. The red channel defines the strength of the anchor. The green channel defines which anchor a" +
              " vertex will be bound to.")]
    public BAnchor[] anchors;

    [SerializeField]
    [HideInInspector]
    BoneAndNode[] bone2idxMap;

    //used for debugging if I want to display the the mesh distortions
    //can't use verts and norms because
    Mesh myMesh; 
    Vector3[] localVerts;
    Vector3[] localNorms;

    // Use this for initialization
    public void BindBonesToSoftBodyAndNodesToAnchors() {
        if (skinnedMesh == null)
        {
            Debug.LogError("The Skinned Mesh field has not been assigned.");
            return;
        }

        physicsSimMesh = GetComponent<MeshFilter>();
        if (physicsSimMesh == null)
        {
            Debug.LogError("Must be attached to an object with a MeshRenderer");
            return;
        }

        if (physicsSimMesh == null)
        {
            Debug.LogError("must add the physics sim mesh bone");
            return;
        }

        for (int i = 0; i < anchors.Length; i++)
        {
            BAnchor a = anchors[i];
            if (a.colRangeTo <= a.colRangeFrom)
            {
                Debug.LogError("Error with Anchor row " + i + " ColRangeTo must be greater than colRangeFrom.");
            }
            for (int j = i+1; j < anchors.Length; j++)
            {
                BAnchor b = anchors[j];
                if (b.colRangeFrom >= a.colRangeTo && b.colRangeTo >= a.colRangeTo){
                    //good
                } else if (b.colRangeFrom <= a.colRangeFrom && b.colRangeTo <= a.colRangeFrom)
                {
                    //good
                } else
                {
                    Debug.LogErrorFormat("The color ranges of Anchors {0} and {1} overlap",i,j);
                }
            }
        }
        //get bones and mesh verts
        //compare these in world space to see which ones line up
        //TODO why does other mesh shape work better than this one.
        Transform[] bones = skinnedMesh.bones;
        Mesh m = physicsSimMesh.sharedMesh;
        Vector3[] verts = m.vertices;
        Vector3[] norms = m.normals;
        Color[] cols = m.colors;
        int[] triangles = m.triangles;
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
                    //Debug.Log("found a bone that is aligned with a vertex " + bones[j]);
                    BoneAndNode ban = new BoneAndNode();
                    ban.bone = bones[j];
                    ban.nodeIdx = i;
                    foundMatches.Add(ban);
                }
            }
        }
        bone2idxMap = foundMatches.ToArray();
        for (int i = 0; i < bone2idxMap.Length; i++)
        {
            int idx = bone2idxMap[i].nodeIdx;
            List<Edge> edges = new List<Edge>();
            for (int j = 0; j < triangles.Length; j+=3)
            {
                if (triangles[j] == idx)
                {
                    _addEdges(idx, triangles[j + 1], triangles[j + 2], edges, norms, verts);
                } else if (triangles[j+1] == idx)
                {
                    _addEdges(idx, triangles[j], triangles[j + 2], edges, norms, verts);
                } else if (triangles[j+2] == idx)
                {
                    _addEdges(idx, triangles[j], triangles[j + 1], edges, norms, verts);
                }
            }
            edges.Sort();
            bone2idxMap[i].edges = edges.ToArray();
        }
        // clear old values
        for (int j = 0; j < anchors.Length; j++)
        {
            anchors[j].anchorNodeIndexes.Clear();
            anchors[j].anchorNodeStrength.Clear();
            anchors[j].anchorPosition.Clear();
        }

        int numAnchorNodes = 0;
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

        Debug.LogFormat("Done binding bones to nodes and nodes to anchors. Found: {0} bones and {1} anchor nodes.", bone2idxMap.Length, numAnchorNodes);
	}

    void _addEdges(int p, int a, int b, List<Edge> edges, Vector3[] ns, Vector3[] vs)
    {
        Edge aa = new Edge(p, a, ns, vs);
        Edge bb = new Edge(p, b, ns, vs);
        if (!edges.Contains(aa)) edges.Add(aa);
        if (!edges.Contains(bb)) edges.Add(bb);
    }

    public string DescribeBonesAndAnchors()
    {
        int numMappedBones = 0;
        int numAnchorNodes = 0;
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        if (bone2idxMap != null)
        {
            numMappedBones = bone2idxMap.Length;
        }
        if (anchors != null)
        {
            for (int i = 0; i < anchors.Length; i++)
            {
                if (anchors[i].anchorNodeIndexes != null)
                {
                    numAnchorNodes += anchors[i].anchorNodeIndexes.Count;
                }
            }
        }
        return String.Format("{0} bones have been bound\n{1} anchors have been bound",numMappedBones,numAnchorNodes);
    }

    public void OnDrawGizmosSelected()
    {
        /*
        for (int i = 0; i < bone2idxMap.Length; i++)
        {
            if (i == 1 && verts != null && verts.Length > 0)
            {
                Gizmos.color = Color.magenta;
                for (int j = 0; j < bone2idxMap[i].edges.Length; j++)
                {
                    Gizmos.DrawLine(verts[bone2idxMap[i].nodeIdx], verts[bone2idxMap[i].edges[j].nodeIdx]);
                    Gizmos.color = Gizmos.color * .75f;
                }
            }
        }
        */
        if (debugDisplayMappedBoneGizmos)
        {
                Gizmos.color = Color.blue;
                for (int i = 0; i < bone2idxMap.Length; i++)
                {
                    if (bone2idxMap[i].bone != null) {
                        BoneAndNode bn = bone2idxMap[i];
                        Gizmos.color = Color.blue;
                        Gizmos.DrawWireSphere(bn.bone.transform.position, .1f);

                        /*
                        Gizmos.DrawRay(bone2idxMap[i].bone.position, bone2idxMap[i].bindNormal);
                        Gizmos.color = Color.magenta * .6f;
                        Gizmos.DrawRay(bone2idxMap[i].bone.position, bone2idxMap[i].edges[0].bindEdgeXnorm);
                        Gizmos.color = Color.green;
                        Gizmos.DrawRay(bone2idxMap[i].bone.position, norms[bone2idxMap[i].nodeIdx]);
                        Vector3 edgeXnorm = Vector3.Cross(verts[bn.edges[0].nodeIdx] - verts[bn.nodeIdx], norms[bn.nodeIdx]);
                        edgeXnorm.Normalize();
                        Gizmos.color = Color.green * .6f;
                        Gizmos.DrawRay(bone2idxMap[i].bone.position, edgeXnorm);
                        */
                        /*
                        Gizmos.color = Color.red;
                        Gizmos.DrawRay(bone2idxMap[i].bone.position, bone2idxMap[i].bone.right);
                        Gizmos.color = Color.green;
                        Gizmos.DrawRay(bone2idxMap[i].bone.position, bone2idxMap[i].bone.up);
                        Gizmos.color = Color.blue;
                        Gizmos.DrawRay(bone2idxMap[i].bone.position, bone2idxMap[i].bone.forward);
                        
                        Gizmos.color = Color.red * .6f;
                        Gizmos.DrawRay(bone2idxMap[i].bone.position, bone2idxMap[i].bindBoneRotation * Vector3.forward);
                        Gizmos.color = Color.green * .6f; ;
                        Gizmos.DrawRay(bone2idxMap[i].bone.position, bone2idxMap[i].bindBoneRotation * Vector3.up);
                        Gizmos.color = Color.blue * .6f;
                        Gizmos.DrawRay(bone2idxMap[i].bone.position, bone2idxMap[i].bindBoneRotation * Vector3.right);
                        */
                        
                        
                    }
                }
        }
        if (debugDisplayMappedAnchors)
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
        //todo should these be in world coordinates
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
            verts = new Vector3[m_BSoftBody.Nodes.Count];
        }
        for (int i = 0; i < m_BSoftBody.Nodes.Count; i++)
        {
            norms[i] = m_BSoftBody.Nodes[i].Normal.ToUnity();
            verts[i] = m_BSoftBody.Nodes[i].Position.ToUnity();
        }
        for (int i = 0; i < bone2idxMap.Length; i++)
        {
            bone2idxMap[i].bindNormal = norms[bone2idxMap[i].nodeIdx];
            bone2idxMap[i].bindBoneRotation = bone2idxMap[i].bone.rotation;
            
            for (int j = 0; j < bone2idxMap[i].edges.Length; j++)
            {
                bone2idxMap[i].edges[j].bindEdgeXnorm = Vector3.Cross(verts[bone2idxMap[i].edges[j].nodeIdx] - verts[bone2idxMap[i].nodeIdx], norms[bone2idxMap[i].nodeIdx]).normalized;
            }
        }

        return true;
    }

    /**
    moves soft body nodes and restores shape to modeled shape in current location,rotation
    */
    public void ResetNodesAfterTeleportJump(Vector3 jumpOffset)
    {
        if (m_collisionObject != null)
        {
            if (physicsSimMesh == null)
            {
                Debug.LogError("MeshFilter was null trying ResetNodesAfterTeleportJump.");
                return;
            }
            SoftBody sb = (SoftBody)m_collisionObject;
            for (int i = 0; i < sb.Nodes.Count; i++)
            {
                sb.Nodes[i].Position += jumpOffset.ToBullet(); //verts[i].ToBullet();
                //sb.Nodes[i].Normal = norms[i].ToBullet();
                //TODO deal with rotation
            }
            //sb.Rotate(physicsSimMesh.transform.rotation.ToBullet());
            //sb.Translate(physicsSimMesh.transform.position.ToBullet());
        }
    }

    public void LateUpdate()
    {
        if (isInWorld)
        {
            // read the positions of the bones from the physics simulation
            DumpDataFromBullet();
            //Update bone positions and orientaion based on bullet data
            for (int i = 0; i < bone2idxMap.Length; i++)
            {
                BoneAndNode bn = bone2idxMap[i];
                bn.bone.position = verts[bn.nodeIdx];
                // to update the orientation we need to see how the normal and one vertex moved
                //todo check magnitude and loop over edges if first doesn't work
                Vector3 edgeXnorm = Vector3.Cross(verts[bn.edges[0].nodeIdx] - verts[bn.nodeIdx], norms[bn.nodeIdx]);
                edgeXnorm.Normalize();
                Quaternion q = WahbasSolution(bn.bindNormal, bn.edges[0].bindEdgeXnorm,
                                              norms[bn.nodeIdx], edgeXnorm);
            
                bone2idxMap[i].bone.rotation = q * bn.bindBoneRotation;
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
                    localVerts[i] = physicsSimMesh.transform.InverseTransformPoint(verts[i]) + debugSimMeshOffset;
                    localNorms[i] = physicsSimMesh.transform.InverseTransformDirection(norms[i]);
                }
                myMesh.vertices = localVerts;
                myMesh.normals = localNorms;
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

    Quaternion WahbasSolution(Vector3 b1, Vector3 b2, Vector3 r1, Vector3 r2)
    {
        Vector3 r1Xr2 = Vector3.Cross(r1, r2);
        Vector3 b1Xb2 = Vector3.Cross(b1, b2);
        float b1Dotr1 = Vector3.Dot(b1, r1);
        Vector3 b1Xr1 = Vector3.Cross(b1, r1);
        float mu = (1 + b1Dotr1) *
                    Vector3.Dot(b1Xb2, r1Xr2) -
                    Vector3.Dot(b1, r1Xr2) *
                    Vector3.Dot(r1, b1Xb2);
        float v = Vector3.Dot((b1 + r1),Vector3.Cross(b1Xb2,r1Xr2));
        float p = Mathf.Sqrt(mu * mu + v * v);
        Vector3 qV;
        float qs;
        if (mu >= 0)
        {
            float a = 1 / (2 * Mathf.Sqrt(p * (p + mu) * (1 + b1Dotr1)));
            qV = a * ((p + mu) * b1Xr1 + v * (b1 + r1));
            qs = a * ((p + mu) * (1 + b1Dotr1));
        } else
        {
            float a = 1 / (2 * Mathf.Sqrt(p * (p - mu) * (1 + b1Dotr1)));
            qV = a * ((v) * b1Xr1 + (p - mu) * (b1 + r1));
            qs = a * (v * (1 + b1Dotr1));
        }
        Quaternion q = new Quaternion(qV.x, qV.y, qV.z, qs);
        return q;
    }
}
