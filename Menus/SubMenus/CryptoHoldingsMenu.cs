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
        private string cryptoPriceText = "50000";
        private float cryptoPriceValue = 50000f;
        private CryptoType selectedCoin = CryptoType.BTC;
        private readonly string[] cryptoNames = Enum.GetNames(typeof(CryptoType));



        public CryptoHoldingsMenu(ModMenuPlugin plugin)
        {
            this.plugin = plugin;
        }

        public void Draw()
        {
            if (plugin.Exchange == null)
            {
                GUILayout.Label("Open Exchange page in-game first");
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("Back"))
                {
                    plugin.ActivePage = ModMenuPlugin.MenuPage.CurrenciesMenu;
                }
                return;
            }

            // SET HOLDINGS SECTION
            GUILayout.BeginHorizontal();
            GUILayout.Label("Amount:", GUILayout.Width(60));
            cryptoText = GUILayout.TextField(cryptoText);
            GUILayout.EndHorizontal();

            if (!float.TryParse(cryptoText, out cryptoValue))
                cryptoValue = 0f;

            // Coin selector
            GUILayout.Label("Select Coin:");
            selectedCoin = (CryptoType)GUILayout.SelectionGrid(
                (int)selectedCoin,
                cryptoNames,
                3
            );

            // Apply holdings button
            if (GUILayout.Button("Set Holdings"))
            {
                GameLogic.SetCrypto(selectedCoin, cryptoValue);
                DebugOverlay.Log($"{selectedCoin} holdings set to: {cryptoValue}");
            }

            GUILayout.Space(10);

            // SET PRICE SECTION
            GUILayout.BeginHorizontal();
            GUILayout.Label("Coin Price:", GUILayout.Width(80));
            cryptoPriceText = GUILayout.TextField(cryptoPriceText);
            GUILayout.EndHorizontal();

            if (!float.TryParse(cryptoPriceText, out cryptoPriceValue))
                cryptoPriceValue = 0f;

            if (GUILayout.Button("Set Price"))
            {
                GameLogic.SetCryptoCoinPrice(selectedCoin, cryptoPriceValue);
                DebugOverlay.Log($"{selectedCoin} price set to: {cryptoPriceValue}");
            }

            GUILayout.FlexibleSpace();

            if (GUILayout.Button("Back"))
                plugin.ActivePage = ModMenuPlugin.MenuPage.CurrenciesMenu;
        }
    }
}
