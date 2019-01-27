using UnityEngine;
using System.Collections;
using System;

namespace BulletUnity.Debugging {
	public static class BDebug
    {
        [Flags]
        public enum DebugType
        {
			Error = 1,
            Warning = 2,
            Debug = 8,
            Info = 4,
            Trace = 16,
        }
        
		public static void Log(DebugType debugType, object message)
        {
			if(EnumExtensions.IsFlagSet(DebugType.Info, debugType)) {
            	Debug.Log(message);
			}
        }

		public static void LogWarning(DebugType debugType, object message)
        {
			if(EnumExtensions.IsFlagSet(DebugType.Warning, debugType)) {
            	Debug.LogWarning(message);
			}
        }

        /*
		public static void LogError(DebugType debugType, object message)
        {
			if(EnumExtensions.IsFlagSet(DebugType.Error, debugType)) {
            	Debug.LogError(message);
			}
        }
        */

		public static void Log(DebugType debugType, object message, params object[] arguments) {
			if(EnumExtensions.IsFlagSet(DebugType.Info, debugType)) {
				Debug.Log(string.Format(message.ToString(), arguments));
			}
		}

		public static void LogWarning(DebugType debugType, object message, params object[] arguments) {
			if(EnumExtensions.IsFlagSet(DebugType.Warning, debugType)) {
				Debug.LogWarning(string.Format(message.ToString(), arguments));
			}
		}

        /*
		public static void LogError(DebugType debugType, object message, params object[] arguments) {
			if(EnumExtensions.IsFlagSet(DebugType.Error, debugType)) {
				Debug.LogError(string.Format(message.ToString(), arguments));
			}
		}
        */
    }
}
