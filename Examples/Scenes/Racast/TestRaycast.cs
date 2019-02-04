using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BulletSharp;
using BulletUnity;

public class TestRaycast : MonoBehaviour {

    public void Update()
    {
        if (Time.frameCount == 10)
        {
            BulletSharp.Math.Vector3 fromP = transform.position.ToBullet();
            BulletSharp.Math.Vector3 toP = (transform.position + Vector3.down * 10f).ToBullet();
            ClosestRayResultCallback callback = new ClosestRayResultCallback(ref fromP, ref toP);
            BPhysicsWorld world = BPhysicsWorld.Get();
            world.world.RayTest(fromP, toP, callback);
            if (callback.HasHit)
            {
                Debug.LogFormat("Hit p={0} n={1} obj{2}",callback.HitPointWorld,callback.HitNormalWorld,callback.CollisionObject.UserObject);
            }
        }
    }
}
