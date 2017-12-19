using System.Collections.Generic;
using UnityEngine;
namespace DemoFramework
{
    public class Input
    {
        public List<KeyCode> KeysPressed { get; set; }
        public List<KeyCode> KeysReleased { get; set; }
        public List<KeyCode> KeysDown { get; set; }
        //public MouseButtons MousePressed { get; private set; }
        //public MouseButtons MouseReleased { get; private set; }
        //public MouseButtons MouseDown { get; private set; }
        public Vector2 MousePoint { get; private set; }
        public int MouseWheelDelta { get; private set; }

        /*
        Control _control;
        public Control Control
        {
            get { return _control; }
            set
            {
                Release();
                _control = value;
                _control.KeyDown += ControlOnKeyDown;
                _control.KeyUp += ControlOnKeyUp;
                _control.MouseDown += ControlOnMouseDown;
                _control.MouseMove += ControlOnMouseMove;
                _control.MouseUp += ControlOnMouseUp;
                _control.MouseWheel += ControlOnMouseWheel;
            }
        }
        */

        public Input()
        {
            
            KeysPressed = new List<KeyCode>();
            KeysReleased = new List<KeyCode>();
            KeysDown = new List<KeyCode>();

            //Control = control;

            //MousePoint = control.PointToClient(Cursor.Position);
            //MouseWheelDelta = 0;
        }

        public void Release()
        {
            //if (_control == null)
            //    return;
            /*
            _control.KeyDown -= ControlOnKeyDown;
            _control.KeyUp -= ControlOnKeyUp;
            _control.MouseDown -= ControlOnMouseDown;
            _control.MouseMove -= ControlOnMouseMove;
            _control.MouseUp -= ControlOnMouseUp;
            _control.MouseWheel -= ControlOnMouseWheel;

            _control = null;
            */
        }

        void ControlOnKeyDown(object sender, Event e)
        {
            /*
            Keys key = e.KeyData & ~Keys.Shift;

            if (KeysDown.Contains(key) == false)
            {
                KeysPressed.Add(key);
                KeysDown.Add(key);
            }
            */
        }

        void ControlOnKeyUp(object sender, Event e)
        {
            /*
            Keys key = e.KeyData & ~Keys.Shift;

            KeysReleased.Add(key);
            KeysDown.Remove(key);
            */
        }

        public void ClearKeyCache()
        {

            KeysPressed.Clear();
            KeysReleased.Clear();
            /*
            MousePressed = MouseButtons.None;
            MouseReleased = MouseButtons.None;
            MouseWheelDelta = 0;
            */
        }

        void ControlOnMouseDown(object sender, Event e)
        {
            /*
            MousePoint = e.Location;

            if (e.Button == MouseButtons.None)
                return;

            // Don't consider mouse clicks outside of the client area
            if (_control.ClientRectangle.Contains(MousePoint) == false)
                return;

            //if (_control.Focused == false && _control.CanFocus)
            //    return;

            if (e.Button == MouseButtons.Left)
            {
                if ((MouseDown & MouseButtons.Left) != MouseButtons.Left)
                {
                    MouseDown |= MouseButtons.Left;
                    MousePressed |= MouseButtons.Left;
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                if ((MouseDown & MouseButtons.Right) != MouseButtons.Right)
                {
                    MouseDown |= MouseButtons.Right;
                    MousePressed |= MouseButtons.Right;
                }
            }
            */
        }

        void ControlOnMouseMove(object sender, Event e)
        {
            //MousePoint = e.Location;
        }

        void ControlOnMouseUp(object sender, Event e)
        {
            /*
            if (e.Button == MouseButtons.Left)
            {
                MouseDown &= ~MouseButtons.Left;
                MouseReleased |= MouseButtons.Left;
            }

            if (e.Button == MouseButtons.Right)
            {
                MouseDown &= ~MouseButtons.Right;
                MouseReleased |= MouseButtons.Right;
            }
            */
        }

        void ControlOnMouseWheel(object sender, Event e)
        {
            //MouseWheelDelta = e.Delta;
        }
    }
}
