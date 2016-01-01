using UnityEngine;
using System.Collections;
using BulletSharp.SoftBody;
using System;
using BulletSharp;
using System.Collections.Generic;
using BulletUnity.Primitives;

namespace BulletUnity
{
    class BunnyRainTest : MonoBehaviour
    {
        public PrimitiveMeshOptions meshType = PrimitiveMeshOptions.Bunny;

        [Tooltip("Use a softBody preset values?")]
        public SBSettingsPresets SBPresetSelect = SBSettingsPresets.ShapeMatching;

        public Rect startAreaOfRain = new Rect(new Vector2(0f, 0f), new Vector2(10f, 10f));
        [Range(0.5f, 50f)]
        public float fromHeight = 30f;
        [Range(0.1f, 20f)]
        public float softBodiesPerSecond = 2.0f;
        [Range(1f, 60f)]
        public float lifetime = 10f;

        float lastBunnyTime = 0f;
        public bool enableRain = true;

        void Start()
        {


        }


        void Update()
        {
            if (!enableRain)
                return;
            if ((Time.time - lastBunnyTime) > (1 / softBodiesPerSecond))
            {

                Vector3 pos = new Vector3(0, 0, 0);

                pos.x = UnityEngine.Random.Range(-startAreaOfRain.width / 2, startAreaOfRain.width / 2);
                pos.z = UnityEngine.Random.Range(-startAreaOfRain.height / 2, startAreaOfRain.height / 2);
                pos.y = fromHeight;

                GameObject go = BAnySoftObject.CreateNew(pos, UnityEngine.Random.rotation, meshType, SBPresetSelect);
                //randomize color for effect
                go.GetComponent<MeshRenderer>().material.color =
                  new Color(UnityEngine.Random.Range(0.0f, 1.0f), UnityEngine.Random.Range(0.0f, 1.0f), UnityEngine.Random.Range(0.0f, 1.0f));

                Destroy(go, lifetime);
                
                lastBunnyTime = Time.time;
            }
        }




    }
}
