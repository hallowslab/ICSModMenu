#if CI
namespace UnityEngine
{
    public class MonoBehaviour { }
    public class GameObject
    {
        public bool activeSelf;
        public void SetActive(bool active) { }
    }
    public class Debug
    {
        public static void Log(object message) { }
    }
}
#endif