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
            go.name = "BCylinder";

            return go;
        }

        public override void BuildMesh()
        {
            GetComponent<MeshFilter>().sharedMesh = ProceduralPrimitives.CreateMeshCone(meshSettings.height, meshSettings.radius, 0, meshSettings.nbSides);
            BConeShape bCone = GetComponent<BConeShape>();
            bCone.radius = meshSettings.radius;
            bCone.height  = meshSettings.height;
        }


    }
}
