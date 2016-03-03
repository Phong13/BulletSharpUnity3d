using UnityEngine;
using System.Collections;
using System;

namespace BulletSharp.Debug {
    public class BDebug : MonoBehaviour
    {
        [Flags]
        enum DebugType
        {
            None, Log, Warning, Error
        }

        static DebugType debugType;

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
