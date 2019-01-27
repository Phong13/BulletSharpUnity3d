using System;
using UnityEngine;
using BulletSharp;
//using BulletSharp.Math;
using System.Collections;
using BulletUnity;

namespace BulletUnity.Primitives
{
    /// <summary>
    /// Base class for UnityBullet primatives
    /// </summary>
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    [System.Serializable]
    public abstract class BPrimitive : MonoBehaviour
    {
        public string info = "Information about this BPriitive";  //display in inspector

        public void Start()
        {

            if (Application.isPlaying)
            {
                //Destroy(this);  //Probably don't need this class during runtime?
            }

        }


        public static void CreateNewBase(GameObject go, Vector3 position, Quaternion rotation)
        {
            go.transform.position = position;
            go.transform.rotation = rotation;

            MeshRenderer meshRenderer = go.GetComponent<MeshRenderer>();
            UnityEngine.Material material = new UnityEngine.Material(Shader.Find("Standard"));
            meshRenderer.sharedMaterial = material;
        }

        /// <summary>
        /// Build object mesh and collider
        /// </summary>
        public virtual void BuildMesh()
        {

        }



    }
}
