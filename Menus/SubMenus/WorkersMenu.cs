using ICSModMenu.Features;
using ICSModMenu.Utils;
using UnityEngine;

namespace ICSModMenu.Menus.SubMenus
{
    public class WorkersMenu
    {
        private ModMenuPlugin plugin;

        private static readonly float menuWidth = 240f;
        private static readonly float menuHeight = 150f;
        private static readonly float menuX = 10f;
        private static readonly float menuY = 10f;
        private static readonly float backButtonY = 120f;
        private static readonly float buttonWidth = 180f;
        private static readonly float buttonHeight = 30f;
        private readonly float buttonX = menuX + (menuWidth - buttonWidth) / 2f;



        public WorkersMenu(ModMenuPlugin plugin)
        {
            this.plugin = plugin;
        }

        public void Draw()
        {
            GUI.Box(new Rect(menuX, menuY, menuWidth, menuHeight), "Workers Menu");

            if (plugin.WorkersPanel == null)
            {
                GUI.Label(new Rect(20, 40, 200, 20), "Open Workers page in-game first");
                if (GUI.Button(new Rect(buttonX, backButtonY, buttonWidth, buttonHeight), "Back"))
                    plugin.ActivePage = ModMenuPlugin.MenuPage.Cheats;
                return;
            }

            bool hasBodyguard = WorkersPanelFeatures.HasBodyguard(plugin.WorkersPanel);
            string bodyguardLabel = hasBodyguard ? "Remove Bodyguard" : "Add Bodyguard";

            if (GUI.Button(new Rect(buttonX, 40, buttonWidth, buttonHeight), bodyguardLabel))
            {
                if (hasBodyguard)
                {
                    WorkersPanelFeatures.RemoveBodyguard(plugin.WorkersPanel);
                    DebugOverlay.Log("Removed bodyguard");
                }
                else
                {
                    WorkersPanelFeatures.AddBodyguard(plugin.WorkersPanel);
                    DebugOverlay.Log("Added bodyguard");
                }
            }

            bool hasChef = WorkersPanelFeatures.HasChef(plugin.WorkersPanel);
            string chefLabel = hasChef ? "Remove Chef" : "Add Chef";

            if (GUI.Button(new Rect(buttonX, 80, buttonWidth, buttonHeight), chefLabel))
            {
                if (hasChef)
                {
                    WorkersPanelFeatures.RemoveChef(plugin.WorkersPanel);
                    DebugOverlay.Log("Removed Chef");
                }
                else
                {
                    WorkersPanelFeatures.AddChef(plugin.WorkersPanel);
                    DebugOverlay.Log("Added Chef");
                }
            }

            if (GUI.Button(new Rect(buttonX, backButtonY, buttonWidth, buttonHeight), "Back"))
                plugin.ActivePage = ModMenuPlugin.MenuPage.Cheats;
        }
    }
}
