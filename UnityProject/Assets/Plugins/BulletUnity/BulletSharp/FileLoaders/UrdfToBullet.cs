using BulletSharp;
using BulletSharp.Math;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace DemoFramework.FileLoaders
{
    public class UrdfToBullet
    {
        private static char[] _spaceSeparator = new[] { ' ' };

        public UrdfToBullet(DiscreteDynamicsWorld world)
        {
            World = world;
        }

        public DiscreteDynamicsWorld World { get; private set; }

        public void Convert(UrdfRobot robot, string baseDirectory)
        {
            foreach (UrdfLink link in robot.Links)
            {
                LoadLink(link, baseDirectory);
            }
        }

        private void LoadLink(UrdfLink link, string baseDirectory)
        {
            float mass = 0;
            UrdfInertial inertial = link.Inertial;
            if (inertial != null)
            {
                mass = (float)inertial.Mass;
            }

            UrdfCollision collision = link.Collision;
            if (collision != null)
            {
                Matrix origin = ParsePose(collision.Origin);
                UrdfGeometry geometry = collision.Geometry;
                switch (geometry.Type)
                {
                    case UrdfGeometryType.Box:
                        var box = geometry as UrdfBox;
                        Vector3 size = ParseVector3(box.Size);
                        var boxShape = new BoxShape(size * 0.5f);
                        PhysicsHelper.CreateBody(mass, origin, boxShape, World);
                        break;
                    case UrdfGeometryType.Cylinder:
                        var cylinder = geometry as UrdfCylinder;
                        float radius = (float)cylinder.Radius * 0.5f;
                        float length = (float)cylinder.Length * 0.5f;
                        var cylinderShape = new CylinderShape(radius, length, radius);
                        PhysicsHelper.CreateBody(mass, origin, cylinderShape, World);
                        break;
                    case UrdfGeometryType.Mesh:
                        var mesh = geometry as UrdfMesh;
                        LoadFile(mesh.FileName, baseDirectory, origin);
                        break;
                    case UrdfGeometryType.Sphere:
                        var sphere = geometry as UrdfSphere;
                        var sphereShape = new SphereShape((float)sphere.Radius);
                        PhysicsHelper.CreateBody(mass, origin, sphereShape, World);
                        break;
                }
            }
        }

        private Matrix ParsePose(UrdfPose pose)
        {
            if (pose == null)
            {
                return Matrix.Identity;
            }
            Vector3 rpy = ParseVector3(pose.RollPitchYaw);
            Matrix matrix = Matrix.RotationYawPitchRoll(rpy.Z, rpy.Y, rpy.X);
            matrix.Origin = ParseVector3(pose.Position);
            return matrix;
        }

        private static Vector3 ParseVector3(string vector)
        {
            string[] components = vector.Split(_spaceSeparator, StringSplitOptions.RemoveEmptyEntries);
            return new Vector3(
                float.Parse(components[0], CultureInfo.InvariantCulture),
                float.Parse(components[1], CultureInfo.InvariantCulture),
                float.Parse(components[2], CultureInfo.InvariantCulture));
        }

        private void LoadFile(string fileName, string baseDirectory, Matrix transform)
        {
            string fullPath = Path.Combine(baseDirectory, fileName);
            string extension = Path.GetExtension(fullPath);
            switch (extension)
            {
                case ".obj":
                    WavefrontObj obj = WavefrontObj.Load(fullPath);
                    var mesh = CreateTriangleMesh(obj.Indices, obj.Vertices, Vector3.One);
                    CreateMeshBody(mesh, transform);
                    break;
                default:
                    throw new NotSupportedException();
            }
        }

        private static TriangleMesh CreateTriangleMesh(List<int> indices, List<Vector3> vertices, Vector3 localScaling)
        {
            var triangleMesh = new TriangleMesh();

            int triangleCount = indices.Count / 3;
            for (int i = 0; i < triangleCount; i++)
            {
                int index0 = indices[i * 3];
                int index1 = indices[i * 3 + 1];
                int index2 = indices[i * 3 + 2];

                Vector3 vertex0 = vertices[index0] * localScaling;
                Vector3 vertex1 = vertices[index1] * localScaling;
                Vector3 vertex2 = vertices[index2] * localScaling;

                triangleMesh.AddTriangleRef(ref vertex0, ref vertex1, ref vertex2);
            }

            return triangleMesh;
        }

        private void CreateMeshBody(TriangleMesh mesh, Matrix transform)
        {
            const bool useQuantization = true;
            var concaveShape = new BvhTriangleMeshShape(mesh, useQuantization);
            PhysicsHelper.CreateStaticBody(transform, concaveShape, World);
        }
    }
}
