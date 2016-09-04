using UnityEngine;
using System.Collections;
using BulletUnity;
using BulletSharp;

public class BCamera {

    private static CollisionObject closestRayResultReturnObj;

    public static CollisionObject ScreenPointToRay (Camera cam, Vector3 inputScreenCoords, CollisionFilterGroups rayFilterGroup, CollisionFilterGroups rayFilterMask) {
        closestRayResultReturnObj = null;

        CollisionWorld collisionWorld = BPhysicsWorld.Get().world;
        BulletSharp.Math.Vector3 rayFrom = cam.transform.position.ToBullet();
        BulletSharp.Math.Vector3 rayTo = cam.ScreenToWorldPoint(new Vector3(inputScreenCoords.x, inputScreenCoords.y, cam.farClipPlane)).ToBullet();

        BulletSharp.ClosestRayResultCallback rayCallBack = new BulletSharp.ClosestRayResultCallback(ref rayFrom, ref rayTo);
        rayCallBack.CollisionFilterGroup = (short)rayFilterGroup;
        rayCallBack.CollisionFilterMask = (short)rayFilterMask;
        //BulletSharp.AllHitsRayResultCallback rayCallBack = new BulletSharp.AllHitsRayResultCallback(rayFrom, rayTo);

        //Debug.Log("Casting ray from: " + rayFrom + " to: " + rayTo);
        collisionWorld.RayTest(rayFrom, rayTo, rayCallBack);
        if (rayCallBack.HasHit)
        {
            //Debug.Log("rayCallBack " + rayCallBack.GetType() + " had a hit: " + rayCallBack.CollisionObject.UserObject + " / of type: " + rayCallBack.CollisionObject.UserObject.GetType());
            closestRayResultReturnObj = rayCallBack.CollisionObject;
            rayCallBack.Dispose();
            return closestRayResultReturnObj;
        }
        else
        {
            //Debug.Log("rayCallBack " + rayCallBack.GetType() + " had no hits.");
            rayCallBack.Dispose();
            return closestRayResultReturnObj;
        }
    }
}
