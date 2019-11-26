using UnityEngine;
using System.Collections;
using BulletSharp.SoftBody;
using BulletUnity;

public class BulletRopeProxy : MonoBehaviour {
    public SoftBody target;
    int numVerts = 0;
    //public BulletSharp.Math.Vector3[] linkVerts = new BulletSharp.Math.Vector3[0];
	public float[] linkVerts = new float[0];
	LineRenderer line;

    void Start() {
        line = GetComponent<LineRenderer>();
    }

    void Update() {
        target.GetLinkVertexData(ref linkVerts);
		if (numVerts != linkVerts.Length / (3 * 2)) {
			numVerts = linkVerts.Length / (3 * 2) + 1;
            line.SetVertexCount(numVerts);
        }
		if (linkVerts.Length > 0){
			//link verts are in pairs marking the ends of the links.
			line.SetPosition(0, new Vector3(linkVerts[0],linkVerts[1], linkVerts[2]));
	        for (int i = 0; i < numVerts-1; i++) {
					int idx = (i * 2 + 1)* 3;
				line.SetPosition(i+1, new Vector3(linkVerts[idx],linkVerts[idx+1], linkVerts[idx+2]));
	        }
		}
    }
    
	
}
