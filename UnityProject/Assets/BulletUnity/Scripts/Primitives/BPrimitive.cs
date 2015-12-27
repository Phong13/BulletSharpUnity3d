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


        ///// <summary>
        ///// Create a bullet primitive GameObject
        ///// </summary>
        ///// <param name="type"></param>
        ///// <param name="position"></param>
        ///// <param name="rotation"></param>
        //public static GameObject CreateBPrimitive(BroadphaseNativeType type, Vector3 position, Quaternion rotation)
        //{
        //    GameObject go = null;

        //    switch (type)
        //    {
        //        case BroadphaseNativeType.BoxShape:
        //            go = BBox.CreateNew(position, rotation);
        //            break;
        //        case BroadphaseNativeType.Box2DShape:

        //            break;
        //        case BroadphaseNativeType.CapsuleShape:

        //            break;
        //        case BroadphaseNativeType.Convex2DShape:

        //            break;
        //        case BroadphaseNativeType.ConvexHullShape:
        //            //CreateConvexHull(shape as ConvexHullShape, mesh);
        //            break;
        //        case BroadphaseNativeType.ConeShape:
        //            go = BCone.CreateNew(position, rotation);
        //            break;
        //        case BroadphaseNativeType.CylinderShape:
        //            go = BCylinder.CreateNew(position, rotation);
        //            break;
        //        case BroadphaseNativeType.GImpactShape:

        //            break;
        //        case BroadphaseNativeType.MultiSphereShape:

        //            break;
        //        case BroadphaseNativeType.SphereShape:
        //            go = BSphere.CreateNew(position, rotation);

        //            break;
        //        case BroadphaseNativeType.StaticPlaneShape:

        //            break;
        //        case BroadphaseNativeType.TriangleMeshShape:

        //            break;
        //        default:

        //            break;
        //    }
        //    //if (shape is PolyhedralConvexShape)
        //    //{
        //    //    return;
        //    //}
        //    if (go == null)
        //    {
        //        Debug.LogError("Not Implemented " + type);
        //        throw new NotImplementedException();
        //    }


        //    return go;

        //}

    }
}
