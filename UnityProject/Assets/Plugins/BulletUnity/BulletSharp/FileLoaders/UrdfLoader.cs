using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;

namespace DemoFramework.FileLoaders
{
    public static class UrdfLoader
    {
        public static UrdfRobot FromFile(string filename)
        {
            var document = new XmlDocument();
            document.Load(filename);
            return ParseRobot(document["robot"]);
        }

        private static UrdfRobot ParseRobot(XmlElement element)
        {
            UrdfRobot robot = new UrdfRobot
            {
                Name = element.GetAttribute("name")
            };

            foreach (XmlElement linkElement in element.SelectNodes("link"))
            {
                var link = ParseLink(linkElement);
                robot.Links.Add(link);
            }

            return robot;
        }

        private static UrdfLink ParseLink(XmlElement element)
        {
            return new UrdfLink
            {
                Name = element.GetAttribute("name"),
                Collision = ParseCollision(element["collision"]),
                Inertial = ParseInertial(element["inertial"]),
            };
        }

        private static UrdfCollision ParseCollision(XmlElement element)
        {
            if (element == null)
            {
                return null;
            }
            return new UrdfCollision
            {
                Geometry = ParseGeometry(element["geometry"]),
                Origin = ParseOrigin(element["origin"])
            };
        }

        private static UrdfInertial ParseInertial(XmlElement element)
        {
            if (element == null)
            {
                return null;
            }
            return new UrdfInertial
            {
                Mass = ParseMass(element["mass"])
            };
        }

        private static double ParseMass(XmlElement element)
        {
            return double.Parse(element.Attributes["value"].Value, CultureInfo.InvariantCulture);
        }

        private static UrdfGeometry ParseGeometry(XmlElement element)
        {
            var shapeElement = element.SelectSingleNode("box|cylinder|mesh|sphere");
            switch (shapeElement.Name)
            {
                case "box":
                    return new UrdfBox
                    {
                        Size = shapeElement.Attributes["size"].Value
                    };
                case "cylinder":
                    return new UrdfCylinder
                    {
                        Radius = double.Parse(
                            shapeElement.Attributes["radius"].Value,
                            CultureInfo.InvariantCulture),
                        Length = double.Parse(
                            shapeElement.Attributes["length"].Value,
                            CultureInfo.InvariantCulture)
                    };
                case "mesh":
                    return new UrdfMesh
                    {
                        FileName = shapeElement.Attributes["filename"].Value,
                        Scale = shapeElement.Attributes["scale"].Value
                    };
                case "sphere":
                    return new UrdfSphere
                    {
                        Radius = double.Parse(
                            shapeElement.Attributes["radius"].Value,
                            CultureInfo.InvariantCulture)
                    };
            }
            throw new NotSupportedException();
        }

        private static UrdfPose ParseOrigin(XmlElement element)
        {
            if (element == null)
            {
                return null;
            }
            return new UrdfPose
            {
                Position = element.Attributes["xyz"].Value,
                RollPitchYaw = element.Attributes["rpy"].Value
            };
        }
    }

    public class UrdfRobot
    {
        public string Name { get; set; }

        public List<UrdfLink> Links = new List<UrdfLink>();

        public override string ToString()
        {
            return Name;
        }
    }

    public class UrdfLink
    {
        public string Name { get; set; }
        public UrdfCollision Collision { get; set; }
        public UrdfInertial Inertial { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

    public class UrdfCollision
    {
        public UrdfGeometry Geometry { get; set; }
        public UrdfPose Origin { get; set; }
    }

    public class UrdfInertial
    {
        public double Mass { get; set; }
    }

    public enum UrdfGeometryType
    {
        Box,
        Cylinder,
        Mesh,
        Sphere
    }

    public abstract class UrdfGeometry
    {
        public abstract UrdfGeometryType Type { get; set; }
    }

    public class UrdfBox : UrdfGeometry
    {
        UrdfGeometryType _type = UrdfGeometryType.Box;
        public override UrdfGeometryType Type {
            get { return _type; }
            set { _type = value; } 
        }

        public string Size { get; set; }
    }

    public class UrdfCylinder : UrdfGeometry
    {
        UrdfGeometryType _type = UrdfGeometryType.Cylinder;
        public override UrdfGeometryType Type
        {
            get { return _type; }
            set { _type = value; }
        }
        public double Length { get; set; }
        public double Radius { get; set; }
    }

    public class UrdfMesh : UrdfGeometry
    {
        UrdfGeometryType _type = UrdfGeometryType.Mesh;
        public override UrdfGeometryType Type {
            get { return _type; }
            set { _type = value; }
        }

        public string FileName { get; set; }
        public string Scale { get; set; }
    }

    public class UrdfSphere : UrdfGeometry
    {
        UrdfGeometryType _type = UrdfGeometryType.Sphere;
        public override UrdfGeometryType Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public double Radius { get; set; }
    }

    public class UrdfPose
    {
        public string Position { get; set; }
        public string RollPitchYaw { get; set; }
    }
}
