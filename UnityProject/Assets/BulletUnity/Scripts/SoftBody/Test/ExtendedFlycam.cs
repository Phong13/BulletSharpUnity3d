using UnityEngine;
using System.Collections;

//http://wiki.unity3d.com/index.php/FlyCam_Extended

namespace BulletUnity
{


    public class ExtendedFlycam : MonoBehaviour
    {

        /*
        EXTENDED FLYCAM
            Desi Quintans (CowfaceGames.com), 17 August 2012.
            Based on FlyThrough.js by Slin (http://wiki.unity3d.com/index.php/FlyThrough), 17 May 2011.

        LICENSE
            Free as in speech, and free as in beer.

        FEATURES
            WASD/Arrows:    Movement
                      Q:    Climb
                      E:    Drop
                          Shift:    Move faster
                        Control:    Move slower
                            End:    Toggle cursor locking to screen (you can also press Ctrl+P to toggle play mode on and off).
        */

        public float cameraSensitivity = 90;
        public float climbSpeed = 4;
        public float normalMoveSpeed = 10;
        public float slowMoveFactor = 0.25f;
        public float fastMoveFactor = 3;

        private float rotationX = 0.0f;
        private float rotationY = 0.0f;

        [Tooltip("Allows flying cam to work even during slow motion mode")]
        public float timeDeltaTime = 0.01f;
        //public float lastUpdateTime = 0f;
        //public float currentDeltaTime;

        void Start()
        {
            //Screen.lockCursor = true;
        }

        //void Update()
        void FixedUpdate()
        {
            //timeDeltaTime = Time.time - lastUpdateTime;
            //lastUpdateTime = Time.time;

            rotationX += Input.GetAxis("Mouse X") * cameraSensitivity * timeDeltaTime;
            rotationY += Input.GetAxis("Mouse Y") * cameraSensitivity * timeDeltaTime;
            rotationY = Mathf.Clamp(rotationY, -90, 90);

            transform.localRotation = Quaternion.AngleAxis(rotationX, Vector3.up);
            transform.localRotation *= Quaternion.AngleAxis(rotationY, Vector3.left);

            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                transform.position += transform.forward * (normalMoveSpeed * fastMoveFactor) * Input.GetAxis("Vertical") * timeDeltaTime;
                transform.position += transform.right * (normalMoveSpeed * fastMoveFactor) * Input.GetAxis("Horizontal") * timeDeltaTime;
            }
            else if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
            {
                transform.position += transform.forward * (normalMoveSpeed * slowMoveFactor) * Input.GetAxis("Vertical") * timeDeltaTime;
                transform.position += transform.right * (normalMoveSpeed * slowMoveFactor) * Input.GetAxis("Horizontal") * timeDeltaTime;
            }
            else
            {
                transform.position += transform.forward * normalMoveSpeed * Input.GetAxis("Vertical") * timeDeltaTime;
                transform.position += transform.right * normalMoveSpeed * Input.GetAxis("Horizontal") * timeDeltaTime;
            }
            
            if (Input.GetKey(KeyCode.Q)) { transform.position += transform.up * climbSpeed * timeDeltaTime; }
            if (Input.GetKey(KeyCode.E)) { transform.position -= transform.up * climbSpeed * timeDeltaTime; }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                //Screen.lockCursor = !Screen.lockCursor;
                Cursor.visible = !Cursor.visible;
               
            }
        }
    }
}