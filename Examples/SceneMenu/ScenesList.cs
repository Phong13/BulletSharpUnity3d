using System;
using System.Collections.Generic;
using UnityEngine;

public class ScenesList : ScriptableObject
{
    public List<BulletScene> Scenes;
}

[Serializable]
public class BulletScene
{
    public string Name;
    public string Path;
    public bool isLocked;
    public int Index;

    public BulletScene(string name, string path, int index)
    {
        Name = name;
        Path = path;
        isLocked = false;
        Index = index;
    }
}
