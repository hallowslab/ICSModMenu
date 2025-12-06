using UnityEngine;

namespace ICSModMenu.Menus.SubMenus
{
    public class CheatsMenu
    {
        private ModMenuPlugin plugin;

        private string moneyText = "1000";
        private float moneyValue = 1000f;
        private float hungerValue = 100f;

        private static readonly float menuWidth = 240f;
        private static readonly float menuHeight = 340f;
        private static readonly float menuX = 10f;
        private static readonly float menuY = 10f;
        private static readonly float buttonWidth = 180f;
        private static readonly float buttonHeight = 30f;
        // We probably won't be aligning buttons on this menu
        private float buttonX = menuX + (menuWidth - buttonWidth) / 2f;


        public CheatsMenu(ModMenuPlugin plugin)
        {
            this.plugin = plugin;

            // Initialize hunger slider from player stats if available
            if (plugin.PlayerStats != null)
                hungerValue = plugin.PlayerStats.hungry;
        }

        public void Draw()
        {
            GUI.Box(new Rect(menuX, menuY, menuWidth, menuHeight), "Cheats Menu");

            //
            // SET MONEY SECTION
            //
            GUI.Label(new Rect(30, 40, 80, 20), "Amount:");
            moneyText = GUI.TextField(new Rect(80, 40, 100, 20), moneyText);

            if (!float.TryParse(moneyText, out moneyValue))
                moneyValue = 0f;

            if (GUI.Button(new Rect(buttonX, 70, buttonWidth, 30), "Set Money"))
            {
                plugin.Actions.SetMoney(moneyValue);
            }

            //
            // SET HUNGER SECTION
            //
            if (plugin.PlayerStats != null)
            {
                GUI.Label(new Rect(30, 110, 100, 20), "Hunger:");

                hungerValue = GUI.HorizontalSlider(
                    new Rect(80, 115, 120, 20),
                    hungerValue,
                    0f,
                    100f
                );

                GUI.Label(new Rect(205, 110, 50, 20), $"{hungerValue:F0}");

                if (GUI.Button(new Rect(buttonX, 140, buttonWidth, buttonHeight), "Set Hunger"))
                {
                    plugin.Actions.SetHunger(hungerValue);
                }
            }
            else
            {
                GUI.Label(new Rect(20, 110, 200, 20), "PlayerStats not available yet");
            }

            //
            // CLEAR TRASH SECTION
            //
            if (plugin.TrashSystem != null)
            {
                if (GUI.Button(new Rect(buttonX, 180, buttonWidth, buttonHeight), "Clear All Trash"))
                {
                    plugin.Actions.ClearTrash();
                }
            }
            else
            {
                GUI.Label(new Rect(20, 140, 200, 20), "TrashSystem not available yet");
            }

            // 
            // SEND CLIENT SECTION
            // 
            if (plugin.CivilManager != null)
            {
                if (GUI.Button(new Rect(buttonX, 220, buttonWidth, buttonHeight), "Send Client"))
                {
                    plugin.Actions.SendCustomer();
                }
            }
            else
            {
                GUI.Label(new Rect(20, 180, 200, 20), "CivilManager not available yet");
            }

            // 
            // WORKERS SUBMENU SECTION
            // 
            if (GUI.Button(new Rect(buttonX, 260, buttonWidth, buttonHeight), "Workers Menu"))
            {
                plugin.ActivePage = ModMenuPlugin.MenuPage.Workers;
            }


            //
            // Back Button
            //
            if (GUI.Button(new Rect(buttonX, 300, buttonWidth, buttonHeight), "Back"))
            {
                plugin.ActivePage = ModMenuPlugin.MenuPage.Main;
            }
        }
    }
}
