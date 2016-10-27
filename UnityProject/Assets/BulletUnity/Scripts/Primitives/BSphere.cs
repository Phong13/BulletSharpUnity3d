using System;
using UnityEngine;
//using BulletSharp;
//using BulletSharp.Math;
using System.Collections;
using BulletUnity;

namespace BulletUnity.Primitives
{
    /// <summary>
    /// Basic BSphere
    /// </summary>
    [RequireComponent(typeof(BRigidBody))]
    [RequireComponent(typeof(BSphereShape))]
    public class BSphere : BPrimitive
    {
        public BSphereMeshSettings meshSettings = new BSphereMeshSettings();

        public static GameObject CreateNew(Vector3 position, Quaternion rotation)
        {
            GameObject go = new GameObject();
            go.AddComponent<BSphereShape>();
            BSphere bSphere = go.AddComponent<BSphere>();
            CreateNewBase(go, position, rotation);
            bSphere.BuildMesh();
            go.name = "BSphere";
            return go;
        }

        public override void BuildMesh()
        {
            GetComponent<MeshFilter>().sharedMesh = meshSettings.Build();
            GetComponent<BSphereShape>().Radius = meshSettings.radius;
        }


    }
}
