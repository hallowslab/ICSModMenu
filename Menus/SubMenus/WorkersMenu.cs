using UnityEngine;

namespace ICSModMenu.Menus.SubMenus
{
    public class WorkersMenu
    {
        private ModMenuPlugin plugin;

        private const float menuWidth = 240f;
        private const float menuX = 10f;
        private const float menuY = 10f;
        private const float menuHeight = 260f;
        private const float buttonWidth = 180f;
        private const float buttonHeight = 30f;
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
                if (GUI.Button(new Rect(buttonX, 200, buttonWidth, buttonHeight), "Back"))
                    plugin.ActivePage = ModMenuPlugin.MenuPage.Cheats;
                return;
            }

            if (GUI.Button(new Rect(buttonX, 40, buttonWidth, buttonHeight), "Add Bodyguard"))
                plugin.Actions.AddBodyguard();

            if (GUI.Button(new Rect(buttonX, 80, buttonWidth, buttonHeight), "Remove Bodyguard"))
                plugin.Actions.RemoveBodyguard();

            if (GUI.Button(new Rect(buttonX, 120, buttonWidth, buttonHeight), "Add Chef"))
                plugin.Actions.AddChef();

            if (GUI.Button(new Rect(buttonX, 160, buttonWidth, buttonHeight), "Remove Chef"))
                plugin.Actions.RemoveChef();

            if (GUI.Button(new Rect(buttonX, 200, buttonWidth, buttonHeight), "Back"))
                plugin.ActivePage = ModMenuPlugin.MenuPage.Cheats;
        }
    }
}
