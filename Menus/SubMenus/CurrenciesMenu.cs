using UnityEngine;

using ICSModMenu.Features;
using ICSModMenu.Utils;

namespace ICSModMenu.Menus.SubMenus
{
    public class CurrenciesMenu
    {
        private ModMenuPlugin plugin;

        private string moneyText = "1000";
        private float moneyValue = 1000f;




        public CurrenciesMenu(ModMenuPlugin plugin)
        {
            this.plugin = plugin;
        }

        public void Draw()
        {
            if (plugin.PlayerStats == null)
            {
                GUILayout.Label("PlayerStats not available yet");
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("Back"))
                    plugin.ActivePage = ModMenuPlugin.MenuPage.Cheats;
                return;
            }

            //
            // SET MONEY SECTION
            //
            GUILayout.BeginHorizontal();
            GUILayout.Label("Amount:", GUILayout.Width(60));
            moneyText = GUILayout.TextField(moneyText);
            GUILayout.EndHorizontal();

            if (!float.TryParse(moneyText, out moneyValue))
                moneyValue = 0f;

            if (GUILayout.Button("Set Money"))
            {
                GameLogic.SetMoney(moneyValue);
                DebugOverlay.Log($"Money set to: {moneyValue}");
            }

            GUILayout.Space(10);

            //
            // SET CRYPTO SECTION
            //
            if (GUILayout.Button("Set Crypto"))
            {
                plugin.ActivePage = ModMenuPlugin.MenuPage.CryptoHoldingsMenu;
            }

            GUILayout.FlexibleSpace();

            //
            // Back Button
            //
            if (GUILayout.Button("Back"))
            {
                plugin.ActivePage = ModMenuPlugin.MenuPage.Cheats;
            }
        }
    }
}
