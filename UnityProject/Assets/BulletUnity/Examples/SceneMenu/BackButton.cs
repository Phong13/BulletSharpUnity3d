using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour {

	public void OnGUI () {
        GUI.BeginGroup(new Rect(Screen.width - 150, Screen.height - 40, 300, 400));
        GUILayout.BeginVertical("box", GUILayout.Width(150));

        if (GUILayout.Button("Back"))
        {
            SceneManager.LoadScene("ExampleMenu");
        }

        GUILayout.EndVertical();
        GUI.EndGroup();
    }
}
