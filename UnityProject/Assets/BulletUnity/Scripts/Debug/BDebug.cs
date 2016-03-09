using UnityEngine;
using System.Collections;
using System;

namespace BulletUnity.Debugging {
	public static class BDebug
    {
        [Flags]
        public enum DebugType
        {
        	Info = 1,
			Warning = 2,
			Error = 4
        }
        
        public static void Log(object message, DebugType debugType)
        {
			if(EnumExtensions.IsFlagSet(DebugType.Info, debugType)) {
            	Debug.Log(message);
			}
        }

        public static void LogWarning(object message, DebugType debugType)
        {
			if(EnumExtensions.IsFlagSet(DebugType.Warning, debugType)) {
            	Debug.LogWarning(message);
			}
        }

        public static void LogError(object message, DebugType debugType)
        {
			if(EnumExtensions.IsFlagSet(DebugType.Error, debugType)) {
            	Debug.LogError(message);
			}
        }
    }
}
