#if CI
using System;
using UnityEngine;

namespace BepInEx.Logging
{
    public class ManualLogSource
    {
        public void LogInfo(string message) { }
        public void LogWarning(string message) { }
        public void LogError(string message) { }
        public void LogDebug(string message) { }
        public void LogFatal(string message) { }
        public void LogMessage(object data) { }
        public void Log(object data) { }
    }
}

namespace BepInEx
{
    public class BaseUnityPlugin : MonoBehaviour
    {
        public Logging.ManualLogSource Logger { get; } = new Logging.ManualLogSource();
    }

    public class BepInPluginAttribute : Attribute
    {
        public BepInPluginAttribute(string guid, string name, string version) { }
    }
}
#endif
