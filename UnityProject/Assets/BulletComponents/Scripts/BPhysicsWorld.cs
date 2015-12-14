using System;
using UnityEngine;
using System.Collections;
using BulletSharp;

public class BPhysicsWorld : MonoBehaviour, IDisposable
{
    protected static BPhysicsWorld singleton;
    protected static bool _isDisposed = false;

    public static BPhysicsWorld Get() {
        if (singleton == null && !_isDisposed) {
            BPhysicsWorld[] ws = FindObjectsOfType<BPhysicsWorld>();
            if (ws.Length == 1) {
                singleton = ws[0];
            } else if (ws.Length == 0) {
                Debug.LogError("Need to add a dynamics world to the scene");
            } else {
                Debug.LogError("Found more than one dynamics world.");
                singleton = ws[0];
                for (int i = 1; i < ws.Length; i++) {
                    GameObject.Destroy(ws[i].gameObject);
                }
            }
        }
        if (singleton.World == null && !singleton.isDisposed) singleton._InitializePhysicsWorld();
        return singleton;
    }

    public DynamicsWorld World;
    
    //It is critical that Awake be called before any other scripts call BPhysicsWorld.Get()
    //Set this script and any derived classes very early in script execution order.
    protected virtual void Awake() {
        _isDisposed = false;
        singleton = BPhysicsWorld.Get();
    }

    protected virtual void FixedUpdate() {
        World.StepSimulation(UnityEngine.Time.fixedTime);
    }

    protected virtual void OnDestroy() {
        Debug.Log("Destroying Physics World");
        Dispose(false);
    }

    public bool isDisposed {
        get { return _isDisposed; }
    }

    protected virtual void _InitializePhysicsWorld() {
        _isDisposed = false;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing) {
        _isDisposed = true;
    }

    public bool AddRigidBody(BulletSharp.RigidBody rb) {
        if (!_isDisposed) {
            Debug.LogFormat("Adding {0} to world",rb);
            World.AddRigidBody(rb);
            return true;
        }
        return false;
    }

    public void RemoveRigidBody(BulletSharp.RigidBody rb) {
        if (!_isDisposed) {
            Debug.LogFormat("Removing {0} from world", rb);
            World.RemoveRigidBody(rb);
        }
    }
}
