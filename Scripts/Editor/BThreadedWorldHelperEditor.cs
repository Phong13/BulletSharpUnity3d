using UnityEditor;
using UnityEngine;

namespace BulletUnity
{
    [CustomEditor(typeof(BThreadedWorldHelper))]
    public class BThreadedWorldHelperEditor : Editor
    {
        BThreadedWorldHelper script;

        float limit = 0;

        void OnEnable()
        {
            script = (BThreadedWorldHelper)target;
        }

        public override void OnInspectorGUI()
        {
            base.DrawDefaultInspector();
            if (!script.ShowGraphs)
                return;
            if (script.PhysicsWorld != null)
            {
                limit = script.FixedTimeStep;
            }

            GraphDisplay.DisplayGraph("Step Time", script._simulationTime, limit, Color.green, Color.red);

            GraphDisplay.DisplayGraph("Mean Step Time", script._meanStepTime, limit, Color.green, Color.red);

            GraphDisplay.DisplayGraph("Delta Time", script._deltaTime, limit, Color.green, Color.red);
        }


    }
}