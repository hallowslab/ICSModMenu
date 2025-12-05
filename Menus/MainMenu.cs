using UnityEngine;

namespace ICSModMenu.Menus
{
    public class MainMenu
    {
        private ModMenuPlugin plugin;

        private static float menuWidth = 240f;
        private static float buttonWidth = 180f;
        private static float buttonHeight = 30f;
        private float buttonX = (menuWidth - buttonWidth) / 2;  // center horizontally

        public MainMenu(ModMenuPlugin plugin)
        {
            this.plugin = plugin;
        }

        public void Draw()
        {
            GUI.Box(new Rect(10, 10, menuWidth, 150), "Mod Menu");

            if (GUI.Button(new Rect(buttonX, 50, buttonWidth, buttonHeight), "Cheats"))
            {
                plugin.ActivePage = ModMenuPlugin.MenuPage.Cheats;
            }

            if (GUI.Button(new Rect(buttonX, 90, buttonWidth, buttonHeight), "Patches"))
            {
                plugin.ActivePage = ModMenuPlugin.MenuPage.Patches;
            }
        }
    }
}
