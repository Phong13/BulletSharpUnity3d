using UnityEngine;
using System.Collections;
using BulletSharp.SoftBody;
using System;
using BulletSharp;
using System.Collections.Generic;
using BulletUnity.Primitives;



namespace BulletUnity
{
    /// <summary>
    /// Demonstrate rope "sticks" or grass similar to SoftDemo "InitSticks"
    /// </summary>
    public class RopeSticksArray : MonoBehaviour
    {

        public Vector3 centerPos = Vector3.zero;  //use this position
        [Range(1f, 20f)]
        public int numSticksXDir = 5;
        [Range(1f, 20f)]
        public int numSticksZDir = 5;
        [Range(0.1f, 20f)]
        public float stickSpacing = 0.25f;
        [Range(0.1f, 20f)]
        public float stickHeight = 1.0f;

        public BSoftBodyRope.RopeSettings ropeSettings = new BSoftBodyRope.RopeSettings();

        public SBSettings softBodySettings = new SBSettings();      //SoftBodyEditor will display this when needed

        void Start()
        {
            //Vector3 centerPos = transform.position;  //place at this transform position

            Vector3 stickStartPos = new Vector3(
                                (centerPos.x - (numSticksXDir * stickSpacing) / 2),
                                centerPos.y,
                                centerPos.z - ((numSticksZDir * stickSpacing) / 2));

            for (int x = 0; x < numSticksXDir; x++)
            {
                for (int z = 0; z < numSticksZDir; z++)
                {
                    Vector3 stickPos = new Vector3(
                        (stickStartPos.x + stickSpacing * x),
                        centerPos.y,
                        (stickStartPos.z + stickSpacing * z));

                    //create but dont build yet
                    GameObject go = BSoftBodyRope.CreateNew(Vector3.zero, Quaternion.identity, false);
                    go.transform.parent = transform;

                    BSoftBodyRope bRope = go.GetComponent<BSoftBodyRope>();

                    //copy relavent settings
                    bRope.meshSettings.numPointsInRope = ropeSettings.numPointsInRope;
                    bRope.meshSettings.width = ropeSettings.width;
                    bRope.meshSettings.startColor = ropeSettings.startColor;
                    bRope.meshSettings.endColor = ropeSettings.endColor;

                    bRope.meshSettings.startPoint = stickPos;
                    bRope.meshSettings.endPoint = new Vector3(stickPos.x, stickPos.y + stickHeight, stickPos.z);

                    //Anchor the start node
                    Array.Resize(ref bRope.ropeAnchors, 2);
                    bRope.ropeAnchors[0] = new RopeAnchor();
                    bRope.ropeAnchors[0].anchorNodePoint = 0f;  //start node point

                    bRope.ropeAnchors[1] = new RopeAnchor();
                    bRope.ropeAnchors[1].anchorNodePoint = 1f / ropeSettings.numPointsInRope;

                    //bRope.SoftBodySettings.ResetToSoftBodyPresets(SBSettingsPresets.ropeStick);
                    bRope.SoftBodySettings = softBodySettings;

                    //now build it
                    bRope._BuildCollisionObject();

                }
            }

        }


        void Update()
        {



        }



    }
}
