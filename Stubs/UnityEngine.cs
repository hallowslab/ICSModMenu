namespace UnityEngine
{
    public class MonoBehaviour { }
    public class GameObject
    {
        public bool activeSelf;
        public void SetActive(bool active) { }
    }
    public struct Vector3 { }
    public class Transform
    {
        public Vector3 position;
        public Vector3 localScale;
    }
    public class Time
    {
        public static float deltaTime;
    }
    public static class Random
    {
        public static float value => 0.5f;
        public static int Range(int min, int max) => min;
    }
    public class Debug
    {
        public static void Log(object message) { }
    }
}
