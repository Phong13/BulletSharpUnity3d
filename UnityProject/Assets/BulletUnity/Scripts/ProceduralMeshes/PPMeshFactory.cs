using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace BulletUnity
{
    [Serializable]
    public class BPrimitiveMeshSettings
    {

        public virtual Mesh Build()
        {
            Mesh mesh = null;
            return mesh;
        }
    }


    [Serializable]
    public class BBoxMeshSettings : BPrimitiveMeshSettings
    {
        [Header("Box Mesh settings:")]
        public Vector3 extents = Vector3.one;

        public override Mesh Build()
        {
            Mesh mesh = ProceduralPrimitives.CreateMeshBox(extents.x, extents.y, extents.z);
            return mesh;
        }
    }

    [Serializable]
    public class BConeMeshSettings : BPrimitiveMeshSettings
    {
        [Header("Cone Mesh settings:")]
        [Range(0, 1000)]
        public float height = 1f;
        [Range(0, 1000)]
        public float radius = 0.5f;
        [Range(2, 100)]
        public int nbSides = 18;

        public override Mesh Build()
        {
            Mesh mesh = ProceduralPrimitives.CreateMeshCone(height, radius, 0f, nbSides);
            return mesh;
        }

    }

    [Serializable]
    public class BCylinderMeshSettings : BPrimitiveMeshSettings
    {
        [Header("Cylinder Mesh settings:")]
        [Range(0, 1000)]
        public float height = 1f;
        [Range(0, 1000)]
        public float radius = 0.5f;
        [Range(2, 100)]
        public int nbSides = 18;

        public Vector3 halfExtent  //Bullet dimensions
        {
            //TODO: This is wrong, ???
            //http://bulletphysics.org/Bullet/phpBB3/viewtopic.php?t=6493
            //http://bulletphysics.com/Bullet/BulletFull/classbtCylinderShape.html
            get { return new Vector3((height / 2), radius / 2, radius / 2); }
        }

        public override Mesh Build()
        {
            Mesh mesh = ProceduralPrimitives.CreateMeshCylinder(height, radius, nbSides);
            return mesh;
        }

    }

    [Serializable]
    public class BSphereMeshSettings : BPrimitiveMeshSettings
    {
        [Header("Sphere Mesh settings:")]
        [Range(0, 1000)]
        public float radius = 0.5f;
        [Range(2, 100)]
        public int numLongitudeLines = 24;
        [Range(2, 100)]
        public int numLatitudeLines = 16;

        public override Mesh Build()
        {
            Mesh mesh = ProceduralPrimitives.CreateMeshSphere(radius, numLongitudeLines, numLatitudeLines);
            return mesh;
        }

    }


    /// <summary>
    /// Useful for creating something random for examples in the editor
    /// Instance remebers last settings
    /// </summary>
    [Serializable]
    public class BAnyMeshSettings : BPrimitiveMeshSettings
    {

        [Header("BAny Mesh settings:")]
        //[SerializeField]
        public PrimitiveMeshOptions meshType = PrimitiveMeshOptions.Bunny;

        //Unity wont allow switching classes in editor, so this class has all parameters in one pile, ick!
        public Vector3 extents = Vector3.one; //cube
        [Range(0, 1000)]
        public float radius = 0.5f; //sphere, cone, cylinder
        [Range(0, 1000)]
        public float height = 1f; //cone, cylinder
        [Range(0, 1000)]
        public float length = 5f;
        [Range(0, 1000)]
        public float width = 5f; //Plane
        [Range(2, 100)]
        public int numLongitudeLines = 10; //sphere
        [Range(2, 100)]
        public int numLatitudeLines = 8; //sphere
        [Range(2, 100)]
        public int nbSides = 18;  //cone/cylinder sides
        [Range(2, 1000)]
        public int resX = 5;
        [Range(2, 1000)]
        public int resZ = 5;

        public Mesh userMesh;
        //Unity cant display this due to serialization, figure it out later
        //public BPrimitiveMeshSettings meshSettings = new BPrimitiveMeshSettings();

        [Header("Mesh post processing")]
        public bool autoWeldVertices = false;
        public float autoWeldThreshold = 0.001f; //TODO
        [Tooltip("Should use this if autoWeldVertices is selected.")]
        public bool recalculateNormals = false;
        public bool addBackFaceTriangles = false;
        public bool recalculateBounds = true;
        public bool optimize = true;

        public override Mesh Build()
        {
            Mesh mesh = null;
            switch (meshType)
            {
                case PrimitiveMeshOptions.UserDefinedMesh:
                    //Need to copy mesh from sharedMesh or we cant modify the mesh!
                    if (userMesh == null) //fill in something
                    {
                        mesh = ProceduralPrimitives.CreateMeshBox(extents.x, extents.x, extents.x);
                        Debug.Log("Must provide a mesh for UserDefinedMesh setting.");
                    }

                    mesh = (Mesh)GameObject.Instantiate(userMesh);
                    break;
                case PrimitiveMeshOptions.Box:
                    mesh = ProceduralPrimitives.CreateMeshBox(extents.x, extents.y, extents.z);
                    break;
                case PrimitiveMeshOptions.Sphere:
                    mesh = ProceduralPrimitives.CreateMeshSphere(radius, numLongitudeLines, numLatitudeLines);
                    break;
                case PrimitiveMeshOptions.Cylinder:
                    mesh = ProceduralPrimitives.CreateMeshCylinder(height, radius, nbSides);
                    break;
                case PrimitiveMeshOptions.Cone:
                    mesh = ProceduralPrimitives.CreateMeshCone(height, radius, 0f, nbSides);
                    break;
                case PrimitiveMeshOptions.Pyramid:
                    mesh = ProceduralPrimitives.CreateMeshPyramid(height, radius);
                    break;
                case PrimitiveMeshOptions.Bunny:
                    mesh = ProceduralPrimitives.BuildMeshFromData(SoftDemo.BunnyMesh.Vertices, SoftDemo.BunnyMesh.Indices);
                    break;
                case PrimitiveMeshOptions.Plane:
                    mesh = ProceduralPrimitives.CreateMeshPlane(length, width, resX, resZ);
                    break;
                default:
                    break;
            }

            return mesh;
        }


    }

    /// <summary>
    /// For editor configurations
    /// </summary>
    [Serializable]
    public class BAnyMeshSettingsForEditor : BAnyMeshSettings
    {

        public bool imediateUpdate = true;

        protected static BAnyMeshSettingsForEditor instance;

        public static BAnyMeshSettingsForEditor Instance
        {
            get { return instance = instance ?? new BAnyMeshSettingsForEditor(); }
        }

    }


    [Serializable]
    public class BPlaneMeshSettings : BPrimitiveMeshSettings
    {
        [Range(0, 1000)]
        public float length = 1f;
        [Range(0, 1000)]
        public float width = 1f;
        [Range(2, 1000)]
        public int resX = 5;
        [Range(2, 1000)]
        public int resZ = 5;


        public override Mesh Build()
        {
            Mesh mesh = ProceduralPrimitives.CreateMeshPlane(length, width, resX, resZ);
            return mesh;
        }
    }


    [Flags]
    public enum PrimitiveMeshOptions
    {
        [Description("User needs to provide a mesh in MeshFilter")]
        UserDefinedMesh,
        Box,
        Sphere,
        Cylinder,
        Cone,
        Pyramid,
        Bunny,
        Plane,
    }


}

