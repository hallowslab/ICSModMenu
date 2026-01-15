using UnityEngine;

using ICSModMenu.Utils;

namespace ICSModMenu.Menus.SubMenus
{
    public class PlayerStatsMenu
    {
        private ModMenuPlugin plugin;

        private float hungerValue = 100f;




        public PlayerStatsMenu(ModMenuPlugin plugin)
        {
            this.plugin = plugin;
            // Initialize hunger slider from player stats if available
            if (plugin.PlayerStats != null)
                hungerValue = plugin.PlayerStats.hungry;
        }

        public void Draw()
        {
            if (plugin.PlayerStats == null)
            {
                GUILayout.Label("PlayerStats not available yet");
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("Back"))
                    plugin.ActivePage = ModMenuPlugin.MenuPage.Cheats;
                return;
            }

            //
            // SET HUNGER SECTION
            //
            GUILayout.BeginHorizontal();
            GUILayout.Label("Hunger:", GUILayout.Width(60));
            
            hungerValue = GUILayout.HorizontalSlider(
                hungerValue,
                0f,
                100f
            );
            
            GUILayout.Label($"{hungerValue:F0}", GUILayout.Width(40));
            GUILayout.EndHorizontal();

            if (GUILayout.Button("Set Hunger"))
            {
                plugin.PlayerStats.hungry = hungerValue;
                DebugOverlay.Log($"Hunger set to {hungerValue}");
            }

            GUILayout.FlexibleSpace();

            //
            // Back Button
            //
            if (GUILayout.Button("Back"))
            {
                plugin.ActivePage = ModMenuPlugin.MenuPage.Cheats;
            }
        }
    }
}
