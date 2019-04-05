using BulletSharp;
using BulletUnity;
using UnityEngine;

public class CollisionNormalDisplayCallback : BCollisionCallbacksDefault
{



    /// <summary>
    ///Beware of creating, destroying, adding or removing bullet objects inside CollisionEnter, CollisionStay and CollisionExit. Doing so can alter the list of collisions and ContactManifolds 
    ///that are being iteratated over
    ///(comodification). This can result in infinite loops, null pointer exceptions, out of sequence Enter,Stay,Exit, etc... A good way to handle this sitution is 
    ///to collect the information in these callbacks then override "OnFinishedVisitingManifolds" like:
    ///
    /// public override void OnFinishedVisitingManifolds(){
    ///     base.OnFinishedVistingManifolds(); //don't omit this it does the callbacks
    ///     do my Instantiation and deletion here.
    /// }
    /// </summary>

    public override void BOnCollisionEnter(CollisionObject other, PersistentManifoldList manifoldList)
    {
        foreach (PersistentManifold manifold in manifoldList.manifolds)
        {
            for (int i = 0; i < manifold.NumContacts; i++)
            {
                ManifoldPoint manifoldPoint = manifold.GetContactPoint(i);
                Vector3 position = manifoldPoint.PositionWorldOnB.ToUnity();
                Vector3 normal = manifoldPoint.NormalWorldOnB.ToUnity();
                UnityThreadExecute.InvokeNextUpdate(() => Debug.DrawLine(position, position + normal, Color.red, 0.5f));

            }
        }
    }

    public override void OnFinishedVisitingManifolds()
    {
        base.OnFinishedVisitingManifolds();
        //it is safe to Instantiate, Destroy, Enable and Disable here
    }
}

