using UnityEngine;
using System.Collections;
using BulletSharp.SoftBody;
using System;
using BulletSharp;
using System.Collections.Generic;
using BulletUnity.Primitives;

namespace BulletUnity
{
    public class BunnyRainTest : MonoBehaviour
    {
         
        public Rect startAreaOfRain = new Rect(new Vector2(0f, 0f), new Vector2(10f, 10f));
        [Range(0.5f, 50f)]
        public float fromHeight = 30f;
        [Range(0.1f, 20f)]
        public float softBodiesPerSecond = 2.0f;
        [Range(1f, 60f)]
        public float lifetime = 10f;
      
        public bool enableRain = true;

        public BAnyMeshSettings anyMeshSettings = new BAnyMeshSettings();

        [Tooltip("Use a softBody preset values?")]
        public SBSettingsPresets SBPresetSelect = SBSettingsPresets.ShapeMatching;

        SBSettingsPresets lastSBPresetSelect;

//        const string collisionTooltip = "Collisions flags\n" +
//"SDF_RS Rigid versus soft mask.\n" +
//"CL_RS: SDF based rigid vs soft.\n" +
//"SVSmask: Cluster vs convex rigid vs soft.\n" +
//"VF_SS: Rigid versus soft mask.\n" +
//"CL_SS:Vertex vs face soft vs soft handling.\n" +
//"CL_SELF: Cluster vs cluster soft vs soft handling.\n" +
//"Default: Cluster soft body self collision.";
//        //[HideInInspector]
//        [Tooltip(collisionTooltip)]
//        public Collisions collisionMask = Collisions.Default;

        public SBSettings SoftBodySettings = new SBSettings();

        float lastBunnyTime = 0f;

        void Start()
        {


        }


        void Update()
        {
            //Update presets if changed
            if (SBPresetSelect != lastSBPresetSelect)
                SoftBodySettings.ResetToSoftBodyPresets(SBPresetSelect);
            lastSBPresetSelect = SBPresetSelect;
            
            if (!enableRain)
                return;
            if ((Time.time - lastBunnyTime) > (1 / softBodiesPerSecond))
            {
                Vector3 pos = new Vector3(0, 0, 0);

                pos.x = startAreaOfRain.center.x + UnityEngine.Random.Range(-startAreaOfRain.width / 2, startAreaOfRain.width / 2);
                pos.z = startAreaOfRain.center.y + UnityEngine.Random.Range(-startAreaOfRain.height / 2, startAreaOfRain.height / 2);
                pos.y = fromHeight;

                GameObject go = BSoftBodyWMesh.CreateNew(pos, UnityEngine.Random.rotation, anyMeshSettings.Build(), false, SBPresetSelect);
                BSoftBodyWMesh bSoft = go.GetComponent<BSoftBodyWMesh>();

                bSoft.meshSettings. autoWeldVertices = anyMeshSettings.autoWeldVertices;
                bSoft.meshSettings.autoWeldThreshold = anyMeshSettings.autoWeldThreshold;
                bSoft.meshSettings.recalculateNormals = anyMeshSettings.recalculateNormals;
                bSoft.meshSettings.addBackFaceTriangles = anyMeshSettings.addBackFaceTriangles;
                bSoft.meshSettings.recalculateBounds = anyMeshSettings.recalculateBounds;
                bSoft.meshSettings.optimize = anyMeshSettings.optimize;

                bSoft.SoftBodySettings = SoftBodySettings;  //play with settings

                //bSoft.SoftBodySettings.config.Collisions = collisionMask;

                bSoft._BuildCollisionObject();

                //randomize color for effect
                go.GetComponent<MeshRenderer>().material.color =
                  new Color(UnityEngine.Random.Range(0.0f, 1.0f), UnityEngine.Random.Range(0.0f, 1.0f), UnityEngine.Random.Range(0.0f, 1.0f));

                Destroy(go, lifetime);

                lastBunnyTime = Time.time;
            }
        }




    }
}
