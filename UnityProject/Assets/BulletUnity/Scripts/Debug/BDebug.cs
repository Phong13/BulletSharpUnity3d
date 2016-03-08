using UnityEngine;
using System.Collections;
using System;

namespace BulletUnity.Debugging {
    public class BDebug : MonoBehaviour
    {
        [Flags]
        public enum DebugType
        {
        	Log, Warning, Error
        }

        public static void Log(object message)
        {
            UnityEngine.Debug.Log(message);
        }

        public static void LogWarning(object message)
        {
            UnityEngine.Debug.LogWarning(message);
        }

        public static void LogError(object message)
        {
            UnityEngine.Debug.LogError(message);
        }
    }
}
