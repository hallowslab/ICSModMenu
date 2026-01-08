namespace ICSModMenu.Models
{
    public class TeleportLocation
    {
        public string Name { get; set; }
        public UnityEngine.Vector3 Position { get; set; }

        public TeleportLocation(string name, UnityEngine.Vector3 position)
        {
            Name = name;
            Position = position;
        }

        public override string ToString()
        {
            return $"{Name} ({Position.x:F1}, {Position.y:F1}, {Position.z:F1})";
        }
    }
}
