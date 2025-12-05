using UnityEngine;
using System.Collections.Generic;

namespace ICSModMenu.Utils
{
    public class DebugOverlay : MonoBehaviour
    {
        private static DebugOverlay _instance;
        private List<string> _lines = new List<string>();
        private const int maxLines = 15;
        private const float lineHeight = 18f;
        private const float padding = 5f;

        private bool visible = false;
        private KeyCode toggleKey = KeyCode.F10;

        // Ensure instance exists
        public static DebugOverlay Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject go = new GameObject("DebugOverlay");
                    _instance = go.AddComponent<DebugOverlay>();
                    DontDestroyOnLoad(go);
                }
                return _instance;
            }
        }

        // Clear all lines
        public static void Clear()
        {
            var inst = Instance;
            inst._lines.Clear();
        }

        // Public logging
        public static void Log(string message)
        {
            var inst = Instance;
            if (inst._lines.Count >= maxLines)
                inst._lines.RemoveAt(0);

            inst._lines.Add(message);
        }

        void Update()
        {
            if (Input.GetKeyDown(toggleKey))
            {
                visible = !visible;
            }
        }

        void OnGUI()
        {
            if (!visible || _lines.Count == 0) return;

            float boxWidth = Screen.width - 2 * padding;
            float boxHeight = maxLines * lineHeight + 2 * padding;
            float boxX = padding;
            float boxY = Screen.height - boxHeight - padding;

            GUI.color = new Color(0f, 0f, 0f, 0.7f);
            GUI.Box(new Rect(boxX, boxY, boxWidth, boxHeight), "");

            GUI.color = Color.green;
            float y = boxY + padding;
            foreach (string line in _lines)
            {
                GUI.Label(new Rect(boxX + padding, y, boxWidth - 2 * padding, lineHeight), line);
                y += lineHeight;
            }
        }
    }
}
