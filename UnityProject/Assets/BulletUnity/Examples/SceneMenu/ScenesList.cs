using System;
using System.Collections.Generic;
using UnityEngine;

public class ScenesList : ScriptableObject {
	public List<Scene> Scenes;
}

[Serializable]
public class Scene {
	public string Name;
	public string Path;
	public bool isLocked;
	public int Index;

	public Scene(string name, string path, int index) {
		Name = name;
		Path = path;
		isLocked = false;
		Index = index;
	}
}
