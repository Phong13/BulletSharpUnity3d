using UnityEngine;
using System.Collections;
using System;

namespace BulletUnity.Debugging {
    public class BDebug
    {
        [Flags]
        public enum DebugType
        {
        	Log, Warning, Error
        }

		public DebugType debugType;

        public void Log(object message)
        {
            Debug.Log(message);
        }

        public void LogWarning(object message)
        {
            Debug.LogWarning(message);
        }

        public void LogError(object message)
        {
            Debug.LogError(message);
        }
    }
}
