using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace BulletUnity
{
    [Serializable]
    public abstract class BPrimitiveMeshSettings
    {

        public abstract Mesh Build();
    }

    [Serializable]
    public class BUserMeshSettings : BPrimitiveMeshSettings
    {

        [SerializeField]
        private Mesh _userMesh;
        public Mesh UserMesh
        {
            get { return _userMesh; }
            set { _userMesh = value; }
        }

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

            if (UserMesh == null) //fill in something
            {
                Debug.Log("Must provide a mesh or create one!");
                return null;
            }

            Mesh mesh = (Mesh)GameObject.Instantiate(UserMesh);  //create a copy of UserMesh, dont overwrite prefabs

            mesh.ApplyMeshPostProcessing(autoWeldVertices, autoWeldThreshold, addBackFaceTriangles,
                 recalculateNormals, recalculateBounds, optimize);

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
    public class BCapsuleMeshSettings : BPrimitiveMeshSettings
    {
        [Header("Capsule Mesh settings:")]
        [Range(0, 1000)]
        public float height = 1f;
        [Range(0, 1000)]
        public float radius = 0.5f;
        [Range(2, 100)]
        public int nbSides = 18;
        public BCapsuleShape.CapsuleAxis upAxis = BCapsuleShape.CapsuleAxis.y;

        public Vector3 halfExtent  //Bullet dimensions
        {
            get { return new Vector3(radius, height / 2, radius); }
        }

        public override Mesh Build()
        {
            Mesh mesh = ProceduralPrimitives.CreateMeshCapsule(height, radius, nbSides, (int) upAxis);
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
            get { return new Vector3(radius, height / 2, radius); }
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
            BAnyMeshSettings settings = this;
            Mesh mesh = null;
            switch (settings.meshType)
            {
                case PrimitiveMeshOptions.UserDefinedMesh:
                    //Need to copy mesh from sharedMesh or we cant modify the mesh!
                    if (settings.userMesh == null) //fill in something
                    {
                        settings.userMesh = ProceduralPrimitives.CreateMeshBox(settings.extents.x, settings.extents.x, settings.extents.x);
                        Debug.Log("Must provide a mesh for UserDefinedMesh setting.");
                    }

                    mesh = (Mesh)GameObject.Instantiate(settings.userMesh);
                    break;
                case PrimitiveMeshOptions.Box:
                    mesh = ProceduralPrimitives.CreateMeshBox(settings.extents.x, settings.extents.y, settings.extents.z);
                    break;
                case PrimitiveMeshOptions.Sphere:
                    mesh = ProceduralPrimitives.CreateMeshSphere(settings.radius, settings.numLongitudeLines, settings.numLatitudeLines);
                    break;
                case PrimitiveMeshOptions.Cylinder:
                    mesh = ProceduralPrimitives.CreateMeshCylinder(settings.height, settings.radius, settings.nbSides);
                    break;
                case PrimitiveMeshOptions.Cone:
                    mesh = ProceduralPrimitives.CreateMeshCone(settings.height, settings.radius, 0f, settings.nbSides);
                    break;
                case PrimitiveMeshOptions.Pyramid:
                    mesh = ProceduralPrimitives.CreateMeshPyramid(settings.height, settings.radius);
                    break;
                case PrimitiveMeshOptions.Bunny:
                    mesh = ProceduralPrimitives.BuildMeshFromData(SoftDemo.BunnyMesh.Vertices, SoftDemo.BunnyMesh.Indices);
                    break;
                case PrimitiveMeshOptions.Plane:
                    mesh = ProceduralPrimitives.CreateMeshPlane(settings.length, settings.width, settings.resX, settings.resZ);
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

