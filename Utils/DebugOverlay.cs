using UnityEngine;
using System.Collections.Generic;

namespace ICSModMenu.Utils
{
    public class DebugOverlay : MonoBehaviour
    {
        private static DebugOverlay _instance;
        private List<string> _lines = new List<string>();
        private const int maxLines = 15;

        public static void Log(string message)
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("DebugOverlay");
                _instance = go.AddComponent<DebugOverlay>();
                DontDestroyOnLoad(go);
            }

            if (_instance._lines.Count >= maxLines)
                _instance._lines.RemoveAt(0);

            _instance._lines.Add(message);
        }

        void OnGUI()
        {
            GUI.color = Color.black;
            GUI.Box(new Rect(10, 10, 400, 300), "");

            GUI.color = Color.green;
            float y = 30;

            for (int i = 0; i < _lines.Count; i++)
            {
                GUI.Label(new Rect(20, y, 380, 20), _lines[i]);
                y += 20;
            }
        }
    }
}
