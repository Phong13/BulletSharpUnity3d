
using System;
using System.Collections;
using BulletSharp;
using BulletSharp.Math;
using System.Collections.Generic;
using BulletUnity;

/*
This class contains functions and classes that look like the ones the BulletSharp DemoFramework is
expecting. Some are stubs, some are wrappers for Unity classes. This allows the BulletSharp demo code to work with minimal changes.
*/
namespace BulletSharpExamples {


    public enum Keys {
            Escape,
            B,
            N,
            Q,
            F,
            G,
            P,
            T,
            F3,
            F8,
            F11,
            Control,
            Space,
            Return,
            ShiftKey,
            Up,
            Down,
            Left,
            Right,
            OemPeriod,
    }

    public enum MouseButtons {
        None,
        Right,
        Left,
        Middle,
    }

    public struct Size {
        public int Width;
        public int Height;
    }

    public struct Point {
        public int X;
        public int Y;
    }

    
    /*
    Todo implment these and get them to work in OnDrawGizmos
    I have no idea how that will work since they are called from the demo code not from inside OnDrawGizmos
    */
    /*
    public class DebugDrawStub : IDebugDraw {
        public void DrawLine(ref Vector3 from, ref Vector3 to, ref Vector3 fromColor) { }
        public void DrawLine(ref Vector3 from, ref Vector3 to, ref Vector3 fromColor, ref Vector3 toColor) { }
        public void DrawBox(ref Vector3 bbMin, ref Vector3 bbMax, ref Vector3 color) { }
        public void DrawBox(ref Vector3 bbMin, ref Vector3 bbMax, ref Matrix trans, ref Vector3 color) { }
        public void DrawSphere(ref Vector3 p, float radius, ref Vector3 color) { }
        public void DrawSphere(float radius, ref Matrix transform, ref Vector3 color) { }
        public void DrawTriangle(ref Vector3 v0, ref Vector3 v1, ref Vector3 v2, ref Vector3 n0, ref Vector3 n1, ref Vector3 n2, ref Vector3 color, float alpha) { }
        public void DrawTriangle(ref Vector3 v0, ref Vector3 v1, ref Vector3 v2, ref Vector3 color, float alpha) { }
        public void DrawContactPoint(ref Vector3 pointOnB, ref Vector3 normalOnB, float distance, int lifeTime, ref Vector3 color) { }
        public void ReportErrorWarning(String warningString) { }
        public void Draw3dText(ref Vector3 location, String textString) { }
        public DebugDrawModes DebugMode { get; set; }
        public void DrawAabb(ref Vector3 from, ref Vector3 to, ref Vector3 color) { }
        public void DrawTransform(ref Matrix transform, float orthoLen) { }
        public void DrawArc(ref Vector3 center, ref Vector3 normal, ref Vector3 axis, float radiusA, float radiusB, float minAngle, float maxAngle,
            ref Vector3 color, bool drawSect) { }
        public void DrawArc(ref Vector3 center, ref Vector3 normal, ref Vector3 axis, float radiusA, float radiusB, float minAngle, float maxAngle,
            ref Vector3 color, bool drawSect, float stepDegrees) { }
        public void DrawSpherePatch(ref Vector3 center, ref Vector3 up, ref Vector3 axis, float radius,
            float minTh, float maxTh, float minPs, float maxPs, ref Vector3 color) { }
        public void DrawSpherePatch(ref Vector3 center, ref Vector3 up, ref Vector3 axis, float radius,
            float minTh, float maxTh, float minPs, float maxPs, ref Vector3 color, float stepDegrees) { }
        public void DrawCapsule(float radius, float halfHeight, int upAxis, ref Matrix transform, ref Vector3 color) { }
        public void DrawCylinder(float radius, float halfHeight, int upAxis, ref Matrix transform, ref Vector3 color) { }
        public void DrawCone(float radius, float height, int upAxis, ref Matrix transform, ref Vector3 color) { }
        public void DrawPlane(ref Vector3 planeNormal, float planeConst, ref Matrix transform, ref Vector3 color) { }
    }
    */

    public class GraphicsLibraryManager {
        public static bool ExitWithReload;

        public static Graphics GetGraphics(DemoFramework.Demo d) {
            return new Graphics();
        }

        public static void Run(DemoFramework.Demo demo) {
            
        }
    }

    public class GraphicsForm {
        public void Close() {

        }

        public Size ClientSize;
    }

    //TODO set formtext and setInfotext should write text to the GUI 
    public class Graphics : IDisposable {
        public float FieldOfView;
        public bool CullingEnabled;

        public GraphicsForm Form;

        public float FarPlane = 600f;

        public bool IsFullScreen;

        //public MouseButtons MousePressed;
        //public MouseButtons MouseReleased;
        //public MouseButtons MouseDown;

        public IDebugDraw GetPhysicsDebugDrawer() {
            return new DebugDrawUnity();
        }

        public void Initialize() {

        }

        public void UpdateView() {

        }

        public void Run() {

        }

        public void Dispose() {

        }

        public void SetFormText(string text) {

        }

        public void SetInfoText(string text) {

        }
    }

    //TODO this should position the camera
    public class FreeLook {
        public FreeLook(Input i) {

        }

        public bool Update(float frameDelta) {
            return false;
        }

        public void SetEyeTarget(Vector3 eye, Vector3 targ) {
            UnityEngine.Transform t = UnityEngine.Camera.main.transform;
            t.position = eye.ToUnity();
            t.rotation = UnityEngine.Quaternion.LookRotation((targ - eye).ToUnity().normalized, UnityEngine.Vector3.up);

        }

        public Vector3 Up;
        public Vector3 Eye;
        public Vector3 Target;
    }

    //TODO this should be a wrapper for the Input class
    public class Input {

        public static UnityEngine.KeyCode[] UnityKeys = new UnityEngine.KeyCode[]{
            UnityEngine.KeyCode.Escape,
            UnityEngine.KeyCode.B,
            UnityEngine.KeyCode.N,
            UnityEngine.KeyCode.Q,
            UnityEngine.KeyCode.F,
            UnityEngine.KeyCode.G,
            UnityEngine.KeyCode.P,
            UnityEngine.KeyCode.T,
            UnityEngine.KeyCode.F3,
            UnityEngine.KeyCode.F8,
            UnityEngine.KeyCode.F11,
            UnityEngine.KeyCode.LeftControl,
            UnityEngine.KeyCode.RightControl,
            UnityEngine.KeyCode.Space,
            UnityEngine.KeyCode.Return,
            UnityEngine.KeyCode.RightShift,
            UnityEngine.KeyCode.LeftShift,
            UnityEngine.KeyCode.UpArrow,
            UnityEngine.KeyCode.DownArrow,
            UnityEngine.KeyCode.LeftArrow,
            UnityEngine.KeyCode.RightArrow,
            UnityEngine.KeyCode.Period,
        };

        public static Keys[] BSKeys = new Keys[]{
            Keys.Escape,
            Keys.B,
            Keys.N,
            Keys.Q,
            Keys.F,
            Keys.G,
            Keys.P,
            Keys.T,
            Keys.F3,
            Keys.F8,
            Keys.F11,
            Keys.Control,
            Keys.Control,
            Keys.Space,
            Keys.Return,
            Keys.ShiftKey,
            Keys.ShiftKey,
            Keys.Up,
            Keys.Down,
            Keys.Left,
            Keys.Right,
            Keys.OemPeriod,
        };

        public Point MousePoint;
        public MouseButtons MousePressed;
        public MouseButtons MouseDown;
        public MouseButtons MouseReleased;

        public List<Keys> KeysPressed = new List<Keys>();
        public List<Keys> KeysDown = new List<Keys>();
        public List<Keys> KeysReleased = new List<Keys>();

        public Input(GraphicsForm f) {

        }

        public void ClearKeyCache() {
            KeysPressed.Clear();
            KeysDown.Clear();
            KeysReleased.Clear();
        }
    }

    public class Clock {
        public void Start() {

        }

        public float Update() {
            return UnityEngine.Time.fixedDeltaTime;
        }
    }
}

