using UnityEngine;
using UnityEditor;
using BulletSharp;
using BulletSharp.Math;
using BulletUnity;
using DemoFramework.FileLoaders;

public class LoadMJCFWindow : EditorWindow
{
    // Add menu named "My Window" to the Window menu
    [MenuItem("BulletForUnity/File Loaders/Load MJCF (Alpha)")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        LoadMJCFWindow window = (LoadMJCFWindow) EditorWindow.GetWindow(typeof(LoadMJCFWindow));
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.Label("Load MJCF Model", EditorStyles.boldLabel);
        string path;
        EditorGUILayout.HelpBox("This is in an Alpha state. It is included because it does correctly load some models and create a MultiBody using BulletUnity components", MessageType.Warning);
        if (GUILayout.Button("Browse For Model XML File"))
        {
            path = EditorUtility.OpenFilePanelWithFilters("Browse For File", Application.dataPath, new string[0]);
            LoadHumanoid2(path);
        }
    }

    void LoadHumanoid2(string gfilename)
    {
        URDFImporterInterface loader;
        BulletMJCFImporter importer = new BulletMJCFImporter(null);
        string pathPrefix = System.IO.Path.GetDirectoryName(gfilename);
        Debug.Log("Loading robot " + gfilename);
        BulletMJCFImporter.MJCFErrorLogger logger = new BulletMJCFImporter.MJCFErrorLogger();
        importer.loadMJCF(gfilename, logger, false);
        loader = importer;

        ConvertURDF convertUrdf = new ConvertURDF();
        URDF2BulletCachedData cache = new URDF2BulletCachedData();

        Matrix parentTransform = Matrix.Identity;

        for (int m = 0; m < importer.getNumModels(); m++)
        {
            loader.activateModel(m);

            // normally used with PhysicsServerCommandProcessor that allocates unique ids to multibodies,
            // emulate this behavior here:
            loader.setBodyUniqueId(m);
        }
        Debug.LogWarning("Done loading document ================== Creating BulletUnity Model");
        GameObject b = new GameObject(loader.getBodyName());
        convertUrdf.InitURDF2BulletCache(importer, cache);
        int urdfLinkIndex = importer.getRootLinkIndex();
        convertUrdf.ConvertURDF2BulletInternal(importer, null, cache, urdfLinkIndex, parentTransform, b, null, true, "", true, ConvertURDFFlags.NONE);
    }
}