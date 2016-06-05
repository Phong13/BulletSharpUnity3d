using System;
using UnityEngine;
//using BulletSharp;
//using BulletSharp.Math;
using System.Collections;
using BulletUnity;

namespace BulletUnity.Primitives
{
    /// <summary>
    /// BCylinder
    /// </summary>
    [RequireComponent(typeof(BRigidBody))]
    [RequireComponent(typeof(BCapsuleShape))]
    public class BCapsule : BPrimitive
    {

        public BCapsuleMeshSettings meshSettings = new BCapsuleMeshSettings();
        
        public static GameObject CreateNew(Vector3 position, Quaternion rotation)
        {
            GameObject go = new GameObject();
            BCapsule bCylinder = go.AddComponent<BCapsule>();
            CreateNewBase(go, position, rotation);
            bCylinder.BuildMesh();
            go.name = "BCapsule";
            return go;
        }

        public override void BuildMesh()
        {
            GetComponent<MeshFilter>().sharedMesh = meshSettings.Build();
            BCapsuleShape cs = GetComponent<BCapsuleShape>();
            cs.Height = meshSettings.height;
            cs.Radius = meshSettings.radius;
            cs.UpAxis = meshSettings.upAxis;
        }


    }
}
