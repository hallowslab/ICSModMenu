using UnityEngine;

namespace ICSModMenu.Menus.SubMenus
{
    public class CheatsMenu
    {
        private ModMenuPlugin plugin;

        private static readonly float menuWidth = 300f;
        private static readonly float menuHeight = 300f;
        private static readonly float menuX = 10f;
        private static readonly float menuY = 10f;
        private static readonly float buttonWidth = 220f;
        private static readonly float buttonHeight = 34f;
        // center horizontally
        private float buttonX = menuX + (menuWidth - buttonWidth) / 2f;


        public CheatsMenu(ModMenuPlugin plugin)
        {
            this.plugin = plugin;
        }

        public void Draw()
        {
            GUI.Box(new Rect(menuX, menuY, menuWidth, menuHeight), "Cheats Menu");

            float y = 40f;

            // PLAYERSTATS SUBMENU SECTION
            if (GUI.Button(new Rect(buttonX, y, buttonWidth, buttonHeight), "Player Stats Menu"))
            {
                plugin.ActivePage = ModMenuPlugin.MenuPage.PlayerStatsMenu;
            }

            // CURRENCIES SUBMENU SECTION
            y += buttonHeight + 10f;
            if (GUI.Button(new Rect(buttonX, y, buttonWidth, buttonHeight), "Currencies Menu"))
            {
                plugin.ActivePage = ModMenuPlugin.MenuPage.CurrenciesMenu;
            }

            // STORE SUBMENU SECTION
            y += buttonHeight + 10f;
            if (GUI.Button(new Rect(buttonX, y, buttonWidth, buttonHeight), "Store Menu"))
            {
                plugin.ActivePage = ModMenuPlugin.MenuPage.StoreMenu;
            }

            // WORKERS SUBMENU SECTION
            y += buttonHeight + 10f;
            if (GUI.Button(new Rect(buttonX, y, buttonWidth, buttonHeight), "Workers Menu"))
            {
                plugin.ActivePage = ModMenuPlugin.MenuPage.Workers;
            }

            // TELEPORT SUBMENU SECTION
            y += buttonHeight + 10f;
            if (GUI.Button(new Rect(buttonX, y, buttonWidth, buttonHeight), "Teleport"))
            {
                plugin.ActivePage = ModMenuPlugin.MenuPage.TeleportMenu;
            }

            // Back Button
            y += buttonHeight + 10f;
            if (GUI.Button(new Rect(buttonX, y, buttonWidth, buttonHeight), "Back"))
            {
                plugin.ActivePage = ModMenuPlugin.MenuPage.Main;
            }
        }
    }
}
