using BulletSharp.Math;
using System;
using UnityEngine;

namespace DemoFramework
{
    public class MouseController
    {
        public BulletSharp.Math.Vector3 Vector { get; set; }
        public float Sensitivity { get; set; }

        BulletSharpExamples.Input input;
        Vector2 mouseOrigin;
        double angleOriginX, angleOriginY;

        public MouseController(BulletSharpExamples.Input input)
        {
            this.input = input;
            Sensitivity = 0.005f;
            SetByAngles(0, 0);
        }

        // HorizontalAngle - left-right movement (parallel to XZ-plane)
        // VerticalAngle - up-down movement (angle between Vector and Y-axis)
        public void SetByAngles(double horizontalAngle, double verticalAngle)
        {
            Vector = new BulletSharp.Math.Vector3(
                (float)(Math.Cos(horizontalAngle) * Math.Cos(verticalAngle)),
                (float)Math.Sin(verticalAngle),
                (float)(Math.Sin(horizontalAngle) * Math.Cos(verticalAngle)));
        }

        public bool Update()
        {
            /*
            if ((input.MouseDown & MouseButtons.Left) == 0)
                return false;

            // When mouse button is clicked, store cursor position and angles
            if ((input.MousePressed & MouseButtons.Left) != 0)
            {
                mouseOrigin = input.MousePoint;

                // Calculate angles from the vector
                angleOriginX = Math.Atan2(Vector.Z, Vector.X);
                angleOriginY = Math.Asin(Vector.Y);
            }

            // Calculate how much to change the angles
            double angleDeltaX = -(input.MousePoint.X - mouseOrigin.X) * Sensitivity;
            double angleDeltaY = (input.MousePoint.Y - mouseOrigin.Y) * Sensitivity;

            SetByAngles(angleOriginX + angleDeltaX, angleOriginY + angleDeltaY);
            */
            return true;
        }
    }
}
