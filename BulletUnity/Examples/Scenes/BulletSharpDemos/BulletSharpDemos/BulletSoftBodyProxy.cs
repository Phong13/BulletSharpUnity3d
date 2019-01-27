using UnityEngine;
using System.Collections;
using BulletSharp.SoftBody;
using BulletUnity;

public class BulletSoftBodyProxy : MonoBehaviour {
    public SoftBody target;
    public UnityEngine.Vector3[] verts;
    public UnityEngine.Vector3[] norms;
    public int[] tris;
    public Mesh mesh;
    
    void Start() {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    void OnDestroy() {
        Destroy(mesh);
    }

    void Update() {
        if (verts.Length != target.Nodes.Count) {
            verts = new Vector3[target.Nodes.Count];
        }
        if (norms.Length != target.Nodes.Count) {
            norms = new Vector3[target.Nodes.Count];
        }
        for (int i = 0; i < target.Nodes.Count; i++) {
            verts[i] = target.Nodes[i].Position.ToUnity();
            norms[i] = target.Nodes[i].Normal.ToUnity();
        }
        mesh.vertices = verts;
        mesh.normals = norms;
        mesh.triangles = tris;
        mesh.RecalculateBounds();
    }
}
