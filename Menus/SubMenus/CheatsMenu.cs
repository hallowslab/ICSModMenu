using UnityEngine;

namespace ICSModMenu.Menus.SubMenus
{
    public class CheatsMenu
    {
        private ModMenuPlugin plugin;




        public CheatsMenu(ModMenuPlugin plugin)
        {
            this.plugin = plugin;
        }

        public void Draw()
        {
            // PLAYERSTATS SUBMENU SECTION
            if (GUILayout.Button("Player Stats Menu"))
            {
                plugin.ActivePage = ModMenuPlugin.MenuPage.PlayerStatsMenu;
            }

            // CURRENCIES SUBMENU SECTION
            GUILayout.Space(10);
            if (GUILayout.Button("Currencies Menu"))
            {
                plugin.ActivePage = ModMenuPlugin.MenuPage.CurrenciesMenu;
            }

            // STORE SUBMENU SECTION
            GUILayout.Space(10);
            if (GUILayout.Button("Store Menu"))
            {
                plugin.ActivePage = ModMenuPlugin.MenuPage.StoreMenu;
            }

            // WORKERS SUBMENU SECTION
            GUILayout.Space(10);
            if (GUILayout.Button("Workers Menu"))
            {
                plugin.ActivePage = ModMenuPlugin.MenuPage.Workers;
            }

            // TELEPORT SUBMENU SECTION
            GUILayout.Space(10);
            if (GUILayout.Button("Teleport"))
            {
                plugin.ActivePage = ModMenuPlugin.MenuPage.TeleportMenu;
            }

            GUILayout.FlexibleSpace();

            // Back Button
            if (GUILayout.Button("Back"))
            {
                plugin.ActivePage = ModMenuPlugin.MenuPage.Main;
            }
        }
    }
}
