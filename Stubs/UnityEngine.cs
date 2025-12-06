#if CI
using System;
using System.Collections.Generic;

namespace UnityEngine
{
    // Base class for all Unity objects
    public class Object
    {
        // Mimic Unity's FindObjectOfType
        public static T FindObjectOfType<T>() where T : Object, new()
        {
            return new T();
        }

        public static void DontDestroyOnLoad(Object obj) { }
    }

    public class GameObject : Object
    {
        public string name;
        public GameObject(string name = "") { this.name = name; }

        // Stub for adding components
        public T AddComponent<T>() where T : Component, new() => new T();

        // Stub for activating/deactivating objects
        public void SetActive(bool value) { }

        // Stub for checking state
        public bool activeSelf;
    }

    public class Component : Object { }

    public class MonoBehaviour : Component { }

    public struct Vector3
    {
        public float x, y, z;
        public Vector3(float x, float y, float z = 0f) { this.x = x; this.y = y; this.z = z; }
    }

    // INPUT
    public enum KeyCode
    {
        F10 = 291,
        F11 = 292,
    }

    // Cursor & LockMode
    public enum CursorLockMode
    {
        None,
        Locked,
        Confined
    }
    public static class Cursor
    {
        public static bool visible { get; set; } = true;
        public static CursorLockMode lockState { get; set; } = CursorLockMode.None;
    }

    // Input stub
    public static class Input
    {
        // Example: stub for GetKey, GetKeyDown, GetKeyUp
        public static bool GetKey(KeyCode key) => false;
        public static bool GetKeyDown(KeyCode key) => false;
        public static bool GetKeyUp(KeyCode key) => false;

        // Mouse input stubs
        public static float mouseScrollDelta => 0f;
        public static bool mousePresent => false;
        public static Vector3 mousePosition => new Vector3(0, 0, 0);
    }

    // PlayerPrefs
    public class PlayerPrefs
    {
        public static void SetInt(string key, int value) { }
        public static void SetFloat(string key, float value) { }
        public static void Save() {}
    }

    // GUI
    
    // Rect stub
    public struct Rect
    {
        public float x, y, width, height;
        public Rect(float x, float y, float width, float height)
        {
            this.x = x; this.y = y; this.width = width; this.height = height;
        }
    }

    // Color
    public struct Color
    {
        public float r, g, b, a;
        public Color(float r, float g, float b, float a = 1f) { this.r = r; this.g = g; this.b = b; this.a = a; }
        public static Color green => new Color(0f, 1f, 0f, 1f);
    }

    // GUI stub
    public static class GUI
    {
        public static bool Button(Rect rect, string text) => false;
        public static Color color { get; set; } = new Color(1f, 1f, 1f);
        public static void Label(Rect rect, string text) { }
        public static float HorizontalSlider(Rect rect, float value, float leftValue, float rightValue) => value;
        public static void Box(Rect rect, string text) { }
        public static string TextField(Rect rect, string text) => text;
        // Add other GUI methods you need
    }

    public static class GUIStyle { }

    // SCREEN
    public static class Screen
    {
        public static int width => 1920;
        public static int height => 1080;
    }

    // DEBUG
    public static class Debug
    {
        public static void Log(object message) { }
        public static void LogWarning(object message) { }
        public static void LogError(object message) { }
    }

}

namespace UnityEngine.SceneManagement
{
    public static class SceneManager
    {
        public static Scene GetActiveScene() => new Scene();
        public struct Scene
        {
            // Dummy property so CI compiles
            public bool isLoaded => true;
        }
    }
    
}
namespace UnityEngine.UI
{
    public class Button
    {
        public bool interactable { get; set; } = true;
    }
}
#endif
