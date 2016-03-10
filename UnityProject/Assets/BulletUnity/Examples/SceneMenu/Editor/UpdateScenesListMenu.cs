using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class UpdateScenesListMenu : MonoBehaviour {

	[MenuItem("Build/Update Scenes List")]
	public static void UpdateScenesList () {
		// Create list.
		ScenesList asset = ScriptableObject.CreateInstance<ScenesList>();
		asset.Scenes = new List<Scene>();

		// Fill list.
		for (int i = 0; i < EditorBuildSettings.scenes.Count(); i++) {
			EditorBuildSettingsScene scene = EditorBuildSettings.scenes[i];
			if (!scene.enabled) continue;
			
			asset.Scenes.Add(new Scene(Path.GetFileNameWithoutExtension(scene.path), scene.path, i));
		}

		// Write asset to disk.
		AssetDatabase.CreateAsset(asset, "Assets/BulletUnity/Examples/SceneMenu/Resources/ScenesList.asset");
		AssetDatabase.SaveAssets();
	}
}
