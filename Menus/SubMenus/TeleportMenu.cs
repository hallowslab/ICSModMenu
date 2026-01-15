using UnityEngine;

using ICSModMenu.Features;
using ICSModMenu.Utils;

namespace ICSModMenu.Menus.SubMenus
{
    public class TeleportMenu
    {
        private ModMenuPlugin plugin;

        private Vector2 scrollPos = Vector2.zero;
        private string newLocationName = "";
        private string newX = "";
        private string newY = "";
        private string newZ = "";



        public TeleportMenu(ModMenuPlugin plugin)
        {
            this.plugin = plugin;
        }

        public void Draw()
        {
            // GUILayout.Box("Teleport Menu"); // Header usually sufficient

            // Current player position
            var playerPos = TeleportFeatures.GetCurrentPosition();
            GUILayout.Label($"Current Position: {playerPos}");

            GUILayout.Space(10);

            // Input fields for new manual teleport
            GUILayout.BeginHorizontal();
            GUILayout.Label("X:", GUILayout.Width(20));
            newX = GUILayout.TextField(newX, GUILayout.Width(60));
            GUILayout.Label("Y:", GUILayout.Width(20));
            newY = GUILayout.TextField(newY, GUILayout.Width(60));
            GUILayout.Label("Z:", GUILayout.Width(20));
            newZ = GUILayout.TextField(newZ, GUILayout.Width(60));
            GUILayout.EndHorizontal();

            if (GUILayout.Button("Teleport to Coordinates"))
            {
                if (float.TryParse(newX, out float x) &&
                    float.TryParse(newY, out float y) &&
                    float.TryParse(newZ, out float z))
                {
                    TeleportFeatures.Teleport(new Vector3(x, y, z));
                }
                else
                {
                    DebugOverlay.Log("Invalid coordinates!");
                }
            }

            GUILayout.Space(10);

            // Input to save current position
            GUILayout.BeginHorizontal();
            GUILayout.Label("Save Name:", GUILayout.Width(80));
            newLocationName = GUILayout.TextField(newLocationName);
            GUILayout.EndHorizontal();

            if (GUILayout.Button("Save Current Location"))
            {
                if (!string.IsNullOrEmpty(newLocationName))
                {
                    TeleportLocationManager.AddLocation(newLocationName, playerPos);
                    newLocationName = "";
                }
            }

            GUILayout.Space(10);

            // Scrollable list of saved locations
            GUILayout.Label("Saved Locations:");
            
            // Begin ScrollView
            scrollPos = GUILayout.BeginScrollView(scrollPos, GUILayout.Height(200));

            for (int i = 0; i < TeleportLocationManager.Locations.Count; i++)
            {
                var loc = TeleportLocationManager.Locations[i];
                GUILayout.BeginHorizontal();
                
                if (GUILayout.Button(loc.Name))
                {
                    // Convert SerializableVector3 → Vector3
                    TeleportFeatures.Teleport(loc.Position.ToVector3());
                }
                
                if (GUILayout.Button("X", GUILayout.Width(30)))
                {
                    TeleportLocationManager.RemoveLocation(i);
                }
                
                GUILayout.EndHorizontal();
            }

            GUILayout.EndScrollView();

            GUILayout.FlexibleSpace();

            // Back button
            if (GUILayout.Button("Back"))
                plugin.ActivePage = ModMenuPlugin.MenuPage.Cheats; // Or Main, depending on flow
        }
    }
}
