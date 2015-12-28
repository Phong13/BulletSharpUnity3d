using UnityEngine;
using System.Collections;

namespace BulletUnity{
    public class BSoftBodyRopeLineRenderer : MonoBehaviour {
        public float width = .25f;
        public Color color = Color.white;

        BSoftBodyRope sd;
        public LineRenderer lr;
        int lrVertexCount = 0;

        void Awake() {
            sd = GetComponent<BSoftBodyRope>();
            lr = GetComponent<LineRenderer>();
            if (sd == null) {
                Debug.LogError("Must have a BSoftBodyRope component.");
            }
            if (lr == null) {
                Debug.LogError("Must have a LinRenderer Component");
            }
            
        }

        void Update() {
            if (lr == null || sd == null) {
                return;
            }
            if (lr.enabled == false) {
                lr.enabled = true;
            }
            sd.DumpDataFromBullet();

            if (lrVertexCount != sd.verts.Length) {
                lrVertexCount = sd.verts.Length;
                lr.SetVertexCount(lrVertexCount);
                lr.SetWidth(width, width);
                lr.SetColors(color, color);
            }
            for (int i = 0; i < sd.verts.Length; i++) {
                lr.SetPosition(i, sd.verts[i]);
            }
        }
    }
}
