using System;
using UnityEngine;
//using BulletSharp;
//using BulletSharp.Math;
using System.Collections;
using BulletUnity;

namespace BulletUnity.Primitives
{
    /// <summary>
    /// Basic BBox
    /// </summary>
    [RequireComponent(typeof(BRigidBody))]
    [RequireComponent(typeof(BBoxShape))]
    public class BBox : BPrimitive
    {
           
        public BBoxMeshSettings meshSettings = new BBoxMeshSettings();
  
        public static GameObject CreateNew(Vector3 position, Quaternion rotation)
        {
            GameObject go = new GameObject();
            BBox bBox = go.AddComponent<BBox>();
            CreateNewBase(go, position, rotation);
            bBox.BuildMesh();
            go.name = "BBox";
            return go;
        }

        public override void BuildMesh()
        {
            GetComponent<MeshFilter>().sharedMesh = meshSettings.Build();
            GetComponent<BBoxShape>().Extents = meshSettings.extents / 2f;
        }

    }
}
