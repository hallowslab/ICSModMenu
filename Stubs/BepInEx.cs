#if CI
using System;
using UnityEngine;

namespace BepInEx.Logging
{
    public class ManualLogSource
    {
        public void LogInfo(object data) { }
        public void LogWarning(object data) { }
        public void LogError(object data) { }
        public void LogDebug(object data) { }
        public void LogFatal(object data) { }
        public void LogMessage(object data) { }
        public void Log(object data) { }
    }
}

namespace BepInEx
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class BepInPlugin : Attribute
    {
        public readonly string GUID;
        public readonly string Name;
        public readonly string Version;

        public BepInPlugin(string guid, string name, string version)
        {
            GUID = guid;
            Name = name;
            Version = version;
        }
    }

    public class BaseUnityPlugin : MonoBehaviour
    {
        public Logging.ManualLogSource Logger { get; } = new Logging.ManualLogSource();

        // Match the actual BepInEx virtual API
        public virtual void Awake() { }
        public virtual void Start() { }
        public virtual void Update() { }
        public virtual void OnGUI() { }
    }
}
#endif
