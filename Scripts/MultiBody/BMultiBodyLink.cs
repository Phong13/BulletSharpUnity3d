using System.Collections.Generic;
using BulletSharp;
using BulletUnity;
using UnityEngine;

public abstract class BMultiBodyLink : BCollisionObject
{
    internal bool isLinked = false;
    internal MultiBodyLinkCollider linkCollider;

    public List<BMultiBodyConstraint> Constraints;
    public List<BMultiBodyLink> Links;

    public int NbLinks
    {
        get
        {
            int nbLinks = Links.Count;
            foreach (BMultiBodyLink link in Links)
                nbLinks += link.NbLinks;
            return nbLinks;
        }
    }

    internal BMultiBody multiBody;
    public BMultiBody MultiBody
    {
        get
        {
            return multiBody;
        }
    }

    internal int linkId = -2;
    public int LinkId
    {
        get
        {
            return linkId;
        }
    }

    internal int parentLinkId = -2;
    public int ParentLinkId
    {
        get
        {
            return parentLinkId;
        }
    }

    internal Transform parentTransform;

    [SerializeField]
    float _mass = 1f;
    public float Mass
    {
        get { return _mass; }
        set
        {
            if (linkCollider != null)
            {
                linkCollider.MultiBody.GetLink(linkCollider.Link).Mass = value;
            }
            _mass = value;
        }
    }

    [SerializeField]
    float _friction = .5f;
    public float Friction
    {
        get { return _friction; }
        set
        {
            if (linkCollider != null)
            {
                linkCollider.Friction = value;
            }
            _friction = value;
        }
    }


    [SerializeField]
    float _rollingFriction = 0f;
    public float RollingFriction
    {
        get { return _rollingFriction; }
        set
        {
            if (linkCollider != null)
            {
                linkCollider.RollingFriction = value;
            }
            _rollingFriction = value;
        }
    }

    [SerializeField]
    float _restitution = 0f;
    public float Restitution
    {
        get { return _restitution; }
        set
        {
            if (linkCollider != null)
            {
                linkCollider.Restitution = value;
            }
            _restitution = value;
        }
    }

    internal override void Start()
    {

    }

    protected override void OnEnable()
    {

    }

    protected override void OnDisable()
    {
    }

    protected override void OnDestroy()
    {
    }

    protected override void Dispose(bool isdisposing)
    {
    }

    public override CollisionObject GetCollisionObject()
    {

        return m_collisionObject;
    }

    public abstract void SetupLink(BulletSharp.Math.Vector3 linkInertia);

    public int AddLinkToMultiBody(BMultiBody mb, int currentLinkIndex, int parentIndex, Transform parent)
    {
        if (isLinked)
        {
            Debug.LogErrorFormat("Cannot add link {0} to multibody {1} bacause it is already linked", name, mb.name);
            return 0;
        }

        BCollisionShape collisionShape = GetComponent<BCollisionShape>();
        if (collisionShape == null)
        {
            throw new MissingComponentException("Could not find " + typeof(BCollisionShape).Name + " component on BodyLink " + name);
        }

        multiBody = mb;
        linkId = currentLinkIndex;
        parentLinkId = parentIndex;
        parentTransform = parent;

        CollisionShape shape = collisionShape.GetCollisionShape();
        if (shape == null)
        {
            throw new MissingComponentException("Could not get collision shape from " + collisionShape.GetType().Name + " shape component on BodyLink " + name);
        }
        BulletSharp.Math.Vector3 linkInertia;
        shape.CalculateLocalInertia(Mass, out linkInertia);

        if (BPhysicsWorld.Get().debugType >= BulletUnity.Debugging.BDebug.DebugType.Debug)
            Debug.LogFormat(this, "Adding link {0} : {1} to parent {2} of multibody {3}", currentLinkIndex, name, parentIndex, mb.name);


        SetupLink(linkInertia);

        linkCollider = new MultiBodyLinkCollider(mb.MultiBody, currentLinkIndex);
        linkCollider.CollisionShape = shape;
        linkCollider.WorldTransform = transform.localToWorldMatrix.ToBullet();
        linkCollider.CollisionFlags = collisionFlags;
        linkCollider.Friction = Friction;
        linkCollider.RollingFriction = RollingFriction;
        linkCollider.Restitution = Restitution;
        linkCollider.UserObject = this;
        BPhysicsWorld.Get().world.AddCollisionObject(linkCollider, groupsIBelongTo, collisionMask);
        m_collisionObject = linkCollider;

        BulletMultiBodyLinkColliderProxy proxy = gameObject.GetComponent<BulletMultiBodyLinkColliderProxy>();
        if (proxy == null)
            proxy = gameObject.AddComponent<BulletMultiBodyLinkColliderProxy>();

        mb.MultiBody.GetLink(currentLinkIndex).Collider = linkCollider;
        proxy.target = linkCollider;

        isLinked = true;

        foreach (BMultiBodyConstraint mbc in Constraints)
        {
            mbc.AddConstraintToMultiBody(MultiBody, LinkId);
        }

        int addedLinks = 1;
        for (int i = 0; i < Links.Count; ++i)
        {

            addedLinks += Links[i].AddLinkToMultiBody(mb, i + currentLinkIndex + 1, currentLinkIndex, transform);
        }
        return addedLinks;
    }

}
