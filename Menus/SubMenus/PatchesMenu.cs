using UnityEngine;

namespace ICSModMenu.Menus.SubMenus
{
    public class PatchesMenu
    {
        private ModMenuPlugin plugin;

        private static readonly float menuWidth = 240f;
        private static readonly float menuHeight = 200f;
        private static readonly float menuX = 10f;
        private static readonly float menuY = 10f;
        private static readonly float buttonWidth = 200f;
        private static readonly float buttonHeight = 30f;
        private readonly float buttonX = menuX + (menuWidth - buttonWidth) / 2f;


        public PatchesMenu(ModMenuPlugin plugin)
        {
            this.plugin = plugin;
        }

        public void Draw()
        {
            GUI.Box(new Rect(menuX, menuY, menuWidth, menuHeight), "Patch Menu");

            //
            // Thief Patch Toggle
            //
            if (GUI.Button(new Rect(buttonX, 40, buttonWidth, buttonHeight),
                plugin.thiefPatchEnabled ? "Enable Thiefs" : "Disable Thiefs"))
            {
                plugin.ToggleThiefPatch();
            }

            //
            // Beggar Patch Toggle
            //
            if (GUI.Button(new Rect(buttonX, 80, buttonWidth, buttonHeight),
                plugin.beggarPatchEnabled ? "Enable Beggars" : "Disable Beggars"))
            {
                plugin.ToggleBeggarPatch();
            }

            //
            // Hunger Patch Toggle
            //
            if (GUI.Button(new Rect(buttonX, 120, buttonWidth, buttonHeight),
                plugin.playerStatsPatchEnabled ? "Enable Hunger" : "Disable Hunger"))
            {
                plugin.TogglePlayerStatsPatch();
            }

            //
            // Back Button
            //
            if (GUI.Button(new Rect(buttonX, 160, buttonWidth, buttonHeight), "Back"))
            {
                plugin.ActivePage = ModMenuPlugin.MenuPage.Main;
            }
        }
    }
}
