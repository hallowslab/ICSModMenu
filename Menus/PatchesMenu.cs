using UnityEngine;

namespace ICSModMenu.Menus
{
    public class PatchesMenu
    {
        private ModMenuPlugin plugin;

        private static float menuWidth = 240f;
        private static float buttonWidth = 200f;
        private static float buttonHeight = 30f;
        private float buttonX = (menuWidth - buttonWidth) / 2;

        public PatchesMenu(ModMenuPlugin plugin)
        {
            this.plugin = plugin;
        }

        public void Draw()
        {
            GUI.Box(new Rect(10, 10, menuWidth, 200), "Patch Menu");

            //
            // Thief Patch Toggle
            //
            if (GUI.Button(new Rect(buttonX, 40, buttonWidth, buttonHeight),
                plugin.thiefPatchEnabled ? "Disable Thief Alert" : "Enable Thief Alert"))
            {
                plugin.ToggleThiefPatch();
            }

            //
            // Beggar Patch Toggle
            //
            if (GUI.Button(new Rect(buttonX, 80, buttonWidth, buttonHeight),
                plugin.beggarPatchEnabled ? "Disable Beggar Alert" : "Enable Beggar Alert"))
            {
                plugin.ToggleBeggarPatch();
            }

            //
            // Back Button
            //
            if (GUI.Button(new Rect(buttonX, 120, buttonWidth, buttonHeight), "Back"))
            {
                plugin.ActivePage = ModMenuPlugin.MenuPage.Main;
            }
        }
    }
}
