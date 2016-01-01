//using System;
//using UnityEngine;
////using BulletSharp;
using System;
using UnityEngine;
//using BulletSharp;
//using BulletSharp.Math;
using System.Collections;
using BulletUnity.Primitives;

namespace BulletUnity
{
    /// <summary>
    /// Basic BSoft can morph into any object and use any SoftBody mode
    /// </summary>
    [RequireComponent(typeof(BSoftBody))]
    public class BAnySoftObject : BSoftBodyWMesh
    {
        [SerializeField]
        public PrimitiveMeshOptions meshType = PrimitiveMeshOptions.Bunny;

        [Tooltip("Only certain settings are used for each mesh")]
        public BAnyMeshSettings meshSettings = new BAnyMeshSettings();

        /// <summary>
        /// Create new SoftBody object
        /// </summary>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        /// <returns></returns>
        public static GameObject CreateNew(Vector3 position, Quaternion rotation, PrimitiveMeshOptions meshType = PrimitiveMeshOptions.Bunny, SBSettingsPresets sBpresetSelect = SBSettingsPresets.ShapeMatching)
        {
            GameObject go = new GameObject();
            go.transform.position = position;
            go.transform.rotation = rotation;
            BAnySoftObject bAny = go.AddComponent<BAnySoftObject>();

            MeshRenderer meshRenderer = go.GetComponent<MeshRenderer>();
            UnityEngine.Material material = new UnityEngine.Material(Shader.Find("Standard"));
            meshRenderer.material = material;

            bAny.meshType = meshType;  //default 
            bAny.SoftBodySettings.ResetToSoftBodyPresets(sBpresetSelect);

            if (meshType == PrimitiveMeshOptions.Bunny || meshType == PrimitiveMeshOptions.Plane)
            {
                bAny.meshSettings.autoWeldVertices = false;
            }
            else
                bAny.meshSettings.autoWeldVertices = true;

            bAny.BuildSoftBody();
            go.name = "BAnySoftObject";
            return go;
        }

        /// <summary>
        /// Function Builds the selected mesh, 
        /// </summary>
        /// <returns></returns>
        public override bool BuildSoftBody()
        {
            if (meshType != PrimitiveMeshOptions.UserDefinedMesh)
            {
                 meshFilter.sharedMesh                  
                 = ProceduralPrimitives.BuildSomething(meshType, meshSettings, meshSettings.autoWeldVertices);


            }

            base.BuildSoftBody();
            return true;
        }

    }
}
