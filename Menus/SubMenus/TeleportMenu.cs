using ICSModMenu.Features;
using ICSModMenu.Models;
using ICSModMenu.Utils;
using UnityEngine;

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

        private static readonly float menuWidth = 320f;
        private static readonly float menuHeight = 440f;
        private static readonly float menuX = 10f;
        private static readonly float menuY = 10f;

        public TeleportMenu(ModMenuPlugin plugin)
        {
            this.plugin = plugin;
        }

        public void Draw()
        {
            GUI.Box(new Rect(menuX, menuY, menuWidth, menuHeight), "Teleport Menu");

            // Current player position
            var playerPos = TeleportFeatures.GetCurrentPosition();
            GUI.Label(new Rect(menuX + 10, menuY + 25, menuWidth - 20, 20), $"Current Position: {playerPos}");

            float yOffset = 50f;

            // Input fields for new manual teleport
            GUI.Label(new Rect(menuX + 10, menuY + yOffset, 100, 20), "X:");
            newX = GUI.TextField(new Rect(menuX + 40, menuY + yOffset, 60, 20), newX);

            GUI.Label(new Rect(menuX + 110, menuY + yOffset, 100, 20), "Y:");
            newY = GUI.TextField(new Rect(menuX + 140, menuY + yOffset, 60, 20), newY);

            GUI.Label(new Rect(menuX + 210, menuY + yOffset, 100, 20), "Z:");
            newZ = GUI.TextField(new Rect(menuX + 240, menuY + yOffset, 60, 20), newZ);

            if (GUI.Button(new Rect(menuX + 10, menuY + yOffset + 25, 290, 25), "Teleport to Coordinates"))
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

            yOffset += 60;

            // Input to save current position
            GUI.Label(new Rect(menuX + 10, menuY + yOffset, 100, 20), "Save Current Pos as:");
            newLocationName = GUI.TextField(new Rect(menuX + 120, menuY + yOffset, 180, 20), newLocationName);

            if (GUI.Button(new Rect(menuX + 10, menuY + yOffset + 25, 290, 25), "Save Location"))
            {
                if (!string.IsNullOrEmpty(newLocationName))
                {
                    TeleportLocationManager.AddLocation(newLocationName, playerPos);
                    newLocationName = "";
                }
            }

            yOffset += 60;

            // Scrollable list of saved locations
            GUI.Label(new Rect(menuX + 10, menuY + yOffset, 200, 20), "Saved Locations:");
            Rect scrollViewRect = new Rect(menuX + 10, menuY + yOffset + 20, 300, 200);
            Rect contentRect = new Rect(0, 0, 280, TeleportLocationManager.Locations.Count * 30);

            scrollPos = GUI.BeginScrollView(scrollViewRect, scrollPos, contentRect);

            for (int i = 0; i < TeleportLocationManager.Locations.Count; i++)
            {
                var loc = TeleportLocationManager.Locations[i];
                if (GUI.Button(new Rect(0, i * 30, 200, 25), loc.ToString()))
                {
                    TeleportFeatures.Teleport(loc.Position);
                }
                if (GUI.Button(new Rect(210, i * 30, 60, 25), "Delete"))
                {
                    TeleportLocationManager.RemoveLocation(i);
                }
            }

            GUI.EndScrollView();

            // Back button
            if (GUI.Button(new Rect(menuX + 10, menuY + menuHeight - 30, 290, 25), "Back"))
                plugin.ActivePage = ModMenuPlugin.MenuPage.Main;
        }
    }
}
