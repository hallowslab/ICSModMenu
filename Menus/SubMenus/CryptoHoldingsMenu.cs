using System;
using ICSModMenu.Features;
using ICSModMenu.Models;
using ICSModMenu.Utils;
using UnityEngine;

namespace ICSModMenu.Menus.SubMenus
{
    public class CryptoHoldingsMenu
    {
        private ModMenuPlugin plugin;
        private string cryptoText = "100";
        private float cryptoValue = 100f;
        private CryptoType selectedCoin = CryptoType.BTC;
        private readonly string[] cryptoNames = Enum.GetNames(typeof(CryptoType));

        private static readonly float menuWidth = 240f;
        private static readonly float menuHeight = 260f;
        private static readonly float menuX = 10f;
        private static readonly float menuY = 10f;
        private static readonly float buttonWidth = 200f;
        private static readonly float buttonHeight = 30f;
        private readonly float buttonX = menuX + (menuWidth - buttonWidth) / 2f;

        public CryptoHoldingsMenu(ModMenuPlugin plugin)
        {
            this.plugin = plugin;
        }

        public void Draw()
        {
            GUI.Box(new Rect(menuX, menuY, menuWidth, menuHeight), "Crypto Holdings");

            if (plugin.Exchange == null)
            {
                GUI.Label(new Rect(20, 40, 200, 20), "Open Exchange page in-game first");
                if (GUI.Button(new Rect(buttonX, 200, buttonWidth, buttonHeight), "Back"))
                    plugin.ActivePage = ModMenuPlugin.MenuPage.Cheats;
                return;
            }

            // Amount input
            GUI.Label(new Rect(30, 40, 120, 20), "Amount:");
            cryptoText = GUI.TextField(new Rect(150, 40, 80, 20), cryptoText);

            if (!float.TryParse(cryptoText, out cryptoValue))
                cryptoValue = 0f;

            // Coin selector
            GUI.Label(new Rect(30, 70, 120, 20), "Select Coin:");
            selectedCoin = (CryptoType)GUI.SelectionGrid(
                new Rect(30, 100, 200, 80),
                (int)selectedCoin,
                cryptoNames,
                3
            );

            // Apply button
            if (GUI.Button(new Rect(buttonX, 190, buttonWidth, buttonHeight), "Set Balance"))
            {
                GameLogic.SetCrypto(selectedCoin, cryptoValue);
                DebugOverlay.Log($"{selectedCoin} set to: {cryptoValue}");
            }

            if (GUI.Button(new Rect(buttonX, 230, buttonWidth, buttonHeight), "Back"))
                plugin.ActivePage = ModMenuPlugin.MenuPage.CurrenciesMenu;
        }
    }
}
