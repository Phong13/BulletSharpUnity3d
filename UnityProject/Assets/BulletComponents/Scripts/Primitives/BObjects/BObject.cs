using System;
using UnityEngine;
using BulletSharp;
//using BulletSharp.Math;
using System.Collections;
using System.ComponentModel;

namespace BulletUnity.Primitives
{
    /// <summary>
    /// Base class for UnityBullet Objects
    /// Attempt to make a single generic class for everything (Maybe a bad idea?)
    /// </summary>
    [System.Serializable]
    public abstract class BObject : MonoBehaviour
    {
        public string info = "Information about this BObject";  //display in inspector

        public void Start()
        {

            if (Application.isPlaying)
            {
                //Destroy(this);  //Probably don't need this class during runtime?
            }

        }


        /// <summary>
        /// Build object mesh and collider
        /// </summary>
        public virtual void Build()
        {

        }


        /// <summary>
        /// Create a bullet primitive GameObject
        /// </summary>
        /// <param name="type"></param>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        public static GameObject CreateBObject(BroadphaseNativeType type, Vector3 position, Quaternion rotation)
        {
            GameObject go = null;

            switch (type)
            {
                case BroadphaseNativeType.BoxShape:
                    go = BBox.CreateNew(position, rotation);
                    break;
                case BroadphaseNativeType.Box2DShape:

                    break;
                case BroadphaseNativeType.CapsuleShape:

                    break;
                case BroadphaseNativeType.Convex2DShape:

                    break;
                case BroadphaseNativeType.ConvexHullShape:
                    //CreateConvexHull(shape as ConvexHullShape, mesh);
                    break;
                case BroadphaseNativeType.ConeShape:
                    go = BCone.CreateNew(position, rotation);
                    break;
                case BroadphaseNativeType.CylinderShape:
                    go = BCylinder.CreateNew(position, rotation);
                    break;
                case BroadphaseNativeType.GImpactShape:

                    break;
                case BroadphaseNativeType.MultiSphereShape:

                    break;
                case BroadphaseNativeType.SphereShape:
                    go = BSphere.CreateNew(position, rotation);

                    break;
                case BroadphaseNativeType.StaticPlaneShape:

                    break;
                case BroadphaseNativeType.TriangleMeshShape:

                    break;
                default:

                    break;
            }
            //if (shape is PolyhedralConvexShape)
            //{
            //    return;
            //}
            if (go == null)
            {
                Debug.LogError("Not Implemented " + type);
                throw new NotImplementedException();
            }


            return go;

   
        }


        //All modes must have 
        [Flags]
        public enum BMode //VoxModes
        {
            [Description("nothing")]
            none = 0,
            //inactive = VoxModeFlags.isKinematic,                            
            //mesh1 = VoxModeFlags.isKinematic | VoxModeFlags.showMesh,       
            //mesh2 = VoxModeFlags.isKinematic | VoxModeFlags.showMesh | VoxModeFlags.enBoxcolliders,      
            //mesh3 = VoxModeFlags.showMesh | VoxModeFlags.enBoxcolliders,      

        }


        [Flags]
        public enum BModeFlags
        {
            none = 0x00,
            useMeshRenderer = 1 << 0,
            // = 1 << 1,
            // = 1 << 2,
            // = 1 << 3,
            // = 1 << 4,
            // = 1 << 5,  
            // = 1 << 6,
            // = 1 << 7,
        }

    }


    public class ObjectSize
    {

    }




}
