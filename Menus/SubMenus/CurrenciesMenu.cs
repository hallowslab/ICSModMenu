using System;
using UnityEngine;

using ICSModMenu.Features;
using ICSModMenu.Utils;
using ICSModMenu.Models;

namespace ICSModMenu.Menus.SubMenus
{
    public class CurrenciesMenu
    {
        private ModMenuPlugin plugin;

        private string moneyText = "1000";
        private float moneyValue = 1000f;
        private string cryptoText = "100";
        private float cryptoValue = 100f;
        private CryptoType selectedCoin = CryptoType.BTC;
        private readonly string[] cryptoNames = Enum.GetNames(typeof(CryptoType));



        private static readonly float menuWidth = 240f;
        private static readonly float menuHeight = 220f;
        private static readonly float menuX = 10f;
        private static readonly float menuY = 10f;
        private static readonly float buttonWidth = 200f;
        private static readonly float buttonHeight = 30f;
        private readonly float buttonX = menuX + (menuWidth - buttonWidth) / 2f;


        public CurrenciesMenu(ModMenuPlugin plugin)
        {
            this.plugin = plugin;
        }

        public void Draw()
        {
            GUI.Box(new Rect(menuX, menuY, menuWidth, menuHeight), "Currencies Menu");
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
            // SET CRYPTO SECTION
            //
            if (GUI.Button(new Rect(buttonX, 110, buttonWidth, buttonHeight), "Set Crypto"))
            {
                plugin.ActivePage = ModMenuPlugin.MenuPage.CryptoHoldingsMenu;
            }

            //
            // Back Button
            //
            if (GUI.Button(new Rect(buttonX, 190, buttonWidth, buttonHeight), "Back"))
            {
                plugin.ActivePage = ModMenuPlugin.MenuPage.Cheats;
            }
        }
    }
}
