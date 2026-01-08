using UnityEngine;
using System;

namespace ICSModMenu.Models
{
    [Serializable]
    public struct SerializableVector3
    {
        public float x, y, z;

        public SerializableVector3(Vector3 v)
        {
            x = v.x;
            y = v.y;
            z = v.z;
        }

        public Vector3 ToVector3() => new Vector3(x, y, z);
    }

    [Serializable]
    public class TeleportLocation
    {
        public string Name;
        public SerializableVector3 Position;

        public TeleportLocation() { }

        public TeleportLocation(string name, Vector3 position)
        {
            Name = name;
            Position = new SerializableVector3(position);
        }
    }
}
