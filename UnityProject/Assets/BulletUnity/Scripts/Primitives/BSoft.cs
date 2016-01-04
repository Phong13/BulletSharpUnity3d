using System;
using UnityEngine;
//using BulletSharp;
//using BulletSharp.Math;
using System.Collections;
using System.ComponentModel;
using BulletSharp.SoftBody;
using System.Collections.Generic;
using BulletUnity;

namespace BulletUnity.Primitives
{
    /// <summary>
    /// Basic BSoft
    /// </summary>
    [RequireComponent(typeof(BSoftBody))]
    public class BSoft : BPrimitive
    {
          
        public BSoftMeshSettings meshSettings = new BSoftMeshSettings();
        
        public static GameObject CreateNew(Vector3 position, Quaternion rotation)
        {
            GameObject go = new GameObject();
            BSoft bSoft = go.AddComponent<BSoft>();
            CreateNewBase(go, position, rotation);
            //bSoft.MeshSettings = new BSoftMeshSettings();
            
            bSoft.BuildMesh();
            go.name = "BSoft";
            return go;
        }

        public override void BuildMesh()
        {
            meshSettings.meshType = meshSettings.meshType;  //cannot call property in inspector, force property change?

            GetComponent<MeshFilter>().sharedMesh
             = ProceduralPrimitives.BuildSomething(meshSettings.meshType, meshSettings);
        }
    



      
    }
}
