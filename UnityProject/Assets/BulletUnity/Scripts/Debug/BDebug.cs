using UnityEngine;
using System.Collections;
using System;

namespace BulletUnity.Debugging {
	public class BDebug : UnityEngine.Object
    {
        [Flags]
        public enum DebugType
        {
        	Info = 1,
			Warning = 2,
			Error = 4
        }

		public DebugType debugType;

        public void Log(object message)
        {
			if(IsSet(DebugType.Info, debugType)) {
            	Debug.Log(message);
			}
        }

        public void LogWarning(object message)
        {
			if(IsSet(DebugType.Warning, debugType)) {
            	Debug.LogWarning(message);
			}
        }

        public void LogError(object message)
        {
			if(IsSet(DebugType.Error, debugType)) {
            	Debug.LogError(message);
			}
        }

		bool IsSet(DebugType value, DebugType flag)
		{
			return (value & flag) == flag;
		}
    }
}
