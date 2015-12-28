using UnityEngine;
using System.Collections;
using BulletSharp.SoftBody;
using System;
using BulletSharp;
using System.Collections.Generic;
using BulletSharp.SoftBody;

namespace BulletUnity {

    public class BSoftBodyRope : BSoftBody {
        public float linearStiffnesscoefficient = .5f;
        public float totalMass = 1f;
        public int numPointsInRope = 10;
        public Vector3 startPoint;
        public Vector3 endPoint;
        [HideInInspector]
        public UnityEngine.Vector3[] verts = new UnityEngine.Vector3[0];
        [HideInInspector]
        public UnityEngine.Vector3[] norms = new UnityEngine.Vector3[0];

        public void DumpDataFromBullet() {
            if (isInWorld) {
                if (verts.Length != m_BSoftBody.Nodes.Count) {
                    verts = new Vector3[m_BSoftBody.Nodes.Count];
                }
                if (norms.Length != verts.Length) {
                    norms = new Vector3[m_BSoftBody.Nodes.Count];
                }
                for (int i = 0; i < m_BSoftBody.Nodes.Count; i++) {
                    verts[i] = m_BSoftBody.Nodes[i].Position.ToUnity();
                    norms[i] = m_BSoftBody.Nodes[i].Normal.ToUnity();
                }
            }
        }

        internal override bool _BuildSoftBody() {
            if (numPointsInRope < 2) {
                Debug.LogError("There must be at least two points in the rope");
                return false;
            }
            if (totalMass <= 0f) {
                Debug.LogError("The rope must have a positive mass");
                return false;
            }
            SoftRigidDynamicsWorld world = (SoftRigidDynamicsWorld)BPhysicsWorld.Get().World;
            SoftBody ropeSoftBody = SoftBodyHelpers.CreateRope(world.WorldInfo,
                startPoint.ToBullet(), endPoint.ToBullet(), numPointsInRope, 0);
            ropeSoftBody.Cfg.PIterations = 16;
            ropeSoftBody.Materials[0].Lst = linearStiffnesscoefficient;
            ropeSoftBody.TotalMass = totalMass;
            m_BSoftBody = ropeSoftBody;
            return true;
        }
    }
}