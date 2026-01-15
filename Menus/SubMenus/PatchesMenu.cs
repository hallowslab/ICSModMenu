using UnityEngine;

namespace ICSModMenu.Menus.SubMenus
{
    public class PatchesMenu
    {
        private ModMenuPlugin plugin;




        public PatchesMenu(ModMenuPlugin plugin)
        {
            this.plugin = plugin;
        }

        public void Draw()
        {
            //
            // Thief Patch Toggle
            //
            if (GUILayout.Button(plugin.thiefPatchEnabled ? "Enable Thiefs" : "Disable Thiefs"))
            {
                plugin.ToggleThiefPatch();
            }

            GUILayout.Space(10);
            //
            // Beggar Patch Toggle
            //
            if (GUILayout.Button(plugin.beggarPatchEnabled ? "Enable Beggars" : "Disable Beggars"))
            {
                plugin.ToggleBeggarPatch();
            }

            GUILayout.Space(10);
            //
            // Hunger Patch Toggle
            //
            if (GUILayout.Button(plugin.playerStatsPatchEnabled ? "Enable Hunger" : "Disable Hunger"))
            {
                plugin.TogglePlayerStatsPatch();
            }

            GUILayout.FlexibleSpace();

            //
            // Back Button
            //
            if (GUILayout.Button("Back"))
            {
                plugin.ActivePage = ModMenuPlugin.MenuPage.Main;
            }
        }
    }
}
