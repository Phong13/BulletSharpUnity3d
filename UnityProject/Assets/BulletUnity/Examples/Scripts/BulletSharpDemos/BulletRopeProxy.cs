using UnityEngine;
using System.Collections;
using BulletSharp.SoftBody;
using BulletUnity;

public class BulletRopeProxy : MonoBehaviour {
    public SoftBody target;
    int numVerts = 0;
    public BulletSharp.Math.Vector3[] nodes = new BulletSharp.Math.Vector3[0];
    LineRenderer line;

    void Start() {
        line = GetComponent<LineRenderer>();
    }

    void Update() {
        target.GetLinkVertexData(ref nodes);
        if (numVerts != target.Nodes.Count) {
            numVerts = target.Nodes.Count;
            line.SetVertexCount(numVerts);
        }
        line.SetPosition(0, nodes[0].ToUnity());
        for (int i = 0; i < numVerts-1; i++) {
            line.SetPosition(i+1, nodes[i * 2 + 1].ToUnity());
        }
        
    }
    
	
}
