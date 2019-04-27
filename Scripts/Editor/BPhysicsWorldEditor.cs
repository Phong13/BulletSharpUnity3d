using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;
using BulletUnity;

//[CustomEditor(typeof(BPhysicsWorld),true)]
public class BDynamicsWorldEditor : Editor
{
    [MenuItem("BulletForUnity/BulletPhysicsWorld")]
    [MenuItem("GameObject/Create Other/BulletForUnity/BulletPhysicsWorld")]  //right click menu
    public static GameObject CreateNew()
    {
        if (GameObject.FindObjectOfType<BPhysicsWorld>() != null){
            Debug.LogError("The Scene already includes a BulletPhysicsWorld. Not creating.");
            return null;
        }
        GameObject go = new GameObject();
        go.AddComponent<BPhysicsWorld>();
        go.name = "BulletPhysicsWorld";
        return go;
    }
}
