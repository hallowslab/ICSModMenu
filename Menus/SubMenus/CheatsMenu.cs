using UnityEngine;

namespace ICSModMenu.Menus.SubMenus
{
    public class CheatsMenu
    {
        private ModMenuPlugin plugin;

        private static readonly float menuWidth = 240f;
        private static readonly float menuHeight = 220f;
        private static readonly float menuX = 10f;
        private static readonly float menuY = 10f;
        private static readonly float buttonWidth = 180f;
        private static readonly float buttonHeight = 30f;
        // We probably won't be aligning buttons on this menu
        private float buttonX = menuX + (menuWidth - buttonWidth) / 2f;


        public CheatsMenu(ModMenuPlugin plugin)
        {
            this.plugin = plugin;
        }

        public void Draw()
        {
            GUI.Box(new Rect(menuX, menuY, menuWidth, menuHeight), "Cheats Menu");

            // 
            // PLAYERSTATS SUBMENU SECTION
            // 
            if (GUI.Button(new Rect(buttonX, 40, buttonWidth, buttonHeight), "Player Stats Menu"))
            {
                plugin.ActivePage = ModMenuPlugin.MenuPage.PlayerStatsMenu;
            }

            // 
            // STORE SUBMENU SECTION
            // 
            if (GUI.Button(new Rect(buttonX, 80, buttonWidth, buttonHeight), "Store Menu"))
            {
                plugin.ActivePage = ModMenuPlugin.MenuPage.StoreMenu;
            }

            // 
            // WORKERS SUBMENU SECTION
            // 
            if (GUI.Button(new Rect(buttonX, 120, buttonWidth, buttonHeight), "Workers Menu"))
            {
                plugin.ActivePage = ModMenuPlugin.MenuPage.Workers;
            }

            //
            // Back Button
            //
            if (GUI.Button(new Rect(buttonX, 200, buttonWidth, buttonHeight), "Back"))
            {
                plugin.ActivePage = ModMenuPlugin.MenuPage.Main;
            }
        }
    }
}
