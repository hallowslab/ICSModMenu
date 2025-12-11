using UnityEngine;

using ICSModMenu.Features;
using ICSModMenu.Utils;

namespace ICSModMenu.Menus.SubMenus
{
    public class PlayerStatsMenu
    {
        private ModMenuPlugin plugin;

        private string moneyText = "1000";
        private float moneyValue = 1000f;
        private float hungerValue = 100f;

        private static readonly float menuWidth = 240f;
        private static readonly float menuHeight = 200f;
        private static readonly float menuX = 10f;
        private static readonly float menuY = 10f;
        private static readonly float buttonWidth = 200f;
        private static readonly float buttonHeight = 30f;
        private readonly float buttonX = menuX + (menuWidth - buttonWidth) / 2f;


        public PlayerStatsMenu(ModMenuPlugin plugin)
        {
            this.plugin = plugin;
            // Initialize hunger slider from player stats if available
            if (plugin.PlayerStats != null)
                hungerValue = plugin.PlayerStats.hungry;
        }

        public void Draw()
        {
            GUI.Box(new Rect(menuX, menuY, menuWidth, menuHeight), "Player stats Menu");
            if (plugin.PlayerStats == null)
            {
                GUI.Label(new Rect(buttonX, 40, buttonWidth, 60), "PlayerStats not available yet");
                if (GUI.Button(new Rect(buttonX, 160, buttonWidth, buttonHeight), "Back"))
                    plugin.ActivePage = ModMenuPlugin.MenuPage.Cheats;
                return;
            }

            //
            // SET MONEY SECTION
            //
            GUI.Label(new Rect(30, 40, 80, 20), "Amount:");
            moneyText = GUI.TextField(new Rect(80, 40, 100, 20), moneyText);

            if (!float.TryParse(moneyText, out moneyValue))
                moneyValue = 0f;

            if (GUI.Button(new Rect(buttonX, 70, buttonWidth, 30), "Set Money"))
            {
                GameLogic.SetMoney(moneyValue);
                DebugOverlay.Log($"Money set to: {moneyValue}");
            }

            //
            // SET HUNGER SECTION
            //
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
                plugin.PlayerStats.hungry = hungerValue;
                DebugOverlay.Log($"Hunger set to {hungerValue}");
            }

            //
            // Back Button
            //
            if (GUI.Button(new Rect(buttonX, 170, buttonWidth, buttonHeight), "Back"))
            {
                plugin.ActivePage = ModMenuPlugin.MenuPage.Cheats;
            }
        }
    }
}
