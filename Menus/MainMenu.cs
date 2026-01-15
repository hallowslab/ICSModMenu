using UnityEngine;

namespace ICSModMenu.Menus
{
    public class MainMenu
    {
        private ModMenuPlugin plugin;



        public MainMenu(ModMenuPlugin plugin)
        {
            this.plugin = plugin;
        }

        public void Draw()
        {
            // Title is now in the Window header
            // GUILayout.Label("Main Menu");

            if (GUILayout.Button("Cheats", GUILayout.Height(30)))
            {
                plugin.ActivePage = ModMenuPlugin.MenuPage.Cheats;
            }

            if (GUILayout.Button("Patches", GUILayout.Height(30)))
            {
                plugin.ActivePage = ModMenuPlugin.MenuPage.Patches;
            }
            

            
            GUILayout.FlexibleSpace(); // Push content up if window is large
        }
    }
}
