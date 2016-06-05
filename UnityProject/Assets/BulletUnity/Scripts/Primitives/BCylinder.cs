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
    [RequireComponent(typeof(BCylinderShape))]
    public class BCylinder : BPrimitive
    {

        public BCylinderMeshSettings meshSettings = new BCylinderMeshSettings();
        
        public static GameObject CreateNew(Vector3 position, Quaternion rotation)
        {
            GameObject go = new GameObject();
            BCylinder bCylinder = go.AddComponent<BCylinder>();
            CreateNewBase(go, position, rotation);
            bCylinder.BuildMesh();
            go.name = "BCylinder";
            return go;
        }

        public override void BuildMesh()
        {
            GetComponent<MeshFilter>().sharedMesh = meshSettings.Build();
            GetComponent<BCylinderShape>().HalfExtent = meshSettings.halfExtent;
        }


    }
}
