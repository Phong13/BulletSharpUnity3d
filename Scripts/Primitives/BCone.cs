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
    [RequireComponent(typeof(BConeShape))]
    public class BCone : BPrimitive
    {

        public BConeMeshSettings meshSettings = new BConeMeshSettings();
 
        public static GameObject CreateNew(Vector3 position, Quaternion rotation)
        {
            GameObject go = new GameObject();
            BCone bCone = go.AddComponent<BCone>();
            CreateNewBase(go, position, rotation);
            bCone.BuildMesh();
            go.name = "BCone";

            return go;
        }

        public override void BuildMesh()
        {
            GetComponent<MeshFilter>().sharedMesh = meshSettings.Build();
            BConeShape bCone = GetComponent<BConeShape>();
            bCone.Radius = meshSettings.radius;
            bCone.Height  = meshSettings.height;
        }


    }
}
