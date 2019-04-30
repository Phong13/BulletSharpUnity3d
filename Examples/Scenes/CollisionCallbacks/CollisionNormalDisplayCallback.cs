using BulletSharp;
using BulletUnity;
using UnityEngine;

public class CollisionNormalDisplayCallback : BCollisionCallbacksDefault
{

    public Color CollisionStartColor = Color.red;
    public Color CollisionStayColor = Color.red;

    private Vector3 collisionPoint;
    private Vector3 collistionNormal;
    private bool MustDisplay = false;
    private bool CollisionEnter = false;

    /// <summary>
    /// Lock object between threads
    /// </summary>
    System.Object lck = new object();

    void Update()
    {
        CollisionToDisplay();
    }

    /// <summary>
    /// This is called in Unity's thread
    /// </summary>
    private void CollisionToDisplay()
    {
        lock (lck)
        {
            if (MustDisplay)
                Debug.DrawLine(collisionPoint, collisionPoint + collistionNormal, CollisionEnter ? CollisionStartColor : CollisionStayColor, 0.5f);
            MustDisplay = false;
            CollisionEnter = false;
        }
    }

    /// <summary>
    /// This is called in bullet thread
    /// </summary>
    /// <param name="other"></param>
    /// <param name="manifoldList"></param>
    public override void BOnCollisionEnter(CollisionObject other, PersistentManifoldList manifoldList)
    {
        foreach (PersistentManifold manifold in manifoldList.manifolds)
        {
            for (int i = 0; i < manifold.NumContacts; i++)
            {
                ManifoldPoint manifoldPoint = manifold.GetContactPoint(i);
                lock (lck)
                {
                    collisionPoint = manifoldPoint.PositionWorldOnB.ToUnity();
                    collistionNormal = manifoldPoint.NormalWorldOnB.ToUnity();
                    MustDisplay = true;
                    CollisionEnter = true;
                }

            }
        }
    }

    /// <summary>
    /// This is called in bullet thread
    /// </summary>
    /// <param name="other"></param>
    /// <param name="manifoldList"></param>
    public override void BOnCollisionStay(CollisionObject other, PersistentManifoldList manifoldList)
    {
        if (!CollisionEnter)
        {
            foreach (PersistentManifold manifold in manifoldList.manifolds)
            {
                for (int i = 0; i < manifold.NumContacts; i++)
                {
                    ManifoldPoint manifoldPoint = manifold.GetContactPoint(i);
                    lock (lck)
                    {
                        collisionPoint = manifoldPoint.PositionWorldOnB.ToUnity();
                        collistionNormal = manifoldPoint.NormalWorldOnB.ToUnity() / 10f;
                        MustDisplay = true;
                    }

                }
            }
        }
    }

    public override void OnFinishedVisitingManifolds()
    {
        base.OnFinishedVisitingManifolds();
        //it is safe to Instantiate, Destroy, Enable and Disable here
    }
}

