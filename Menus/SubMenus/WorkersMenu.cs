using UnityEngine;

using ICSModMenu.Features;
using ICSModMenu.Utils;

namespace ICSModMenu.Menus.SubMenus
{
    public class WorkersMenu
    {
        private ModMenuPlugin plugin;





        public WorkersMenu(ModMenuPlugin plugin)
        {
            this.plugin = plugin;
        }

        public void Draw()
        {
            if (plugin.WorkersPanel == null)
            {
                GUILayout.Label("Open Workers page in-game first");
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("Back"))
                    plugin.ActivePage = ModMenuPlugin.MenuPage.Cheats;
                return;
            }

            bool hasBodyguard = WorkersPanelFeatures.HasBodyguard(plugin.WorkersPanel);
            string bodyguardLabel = hasBodyguard ? "Remove Bodyguard" : "Add Bodyguard";

            if (GUILayout.Button(bodyguardLabel))
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

            GUILayout.Space(10);

            bool hasChef = WorkersPanelFeatures.HasChef(plugin.WorkersPanel);
            string chefLabel = hasChef ? "Remove Chef" : "Add Chef";

            if (GUILayout.Button(chefLabel))
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

            GUILayout.FlexibleSpace();

            if (GUILayout.Button("Back"))
                plugin.ActivePage = ModMenuPlugin.MenuPage.Cheats;
        }
    }
}
