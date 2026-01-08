using UnityEngine;

using ICSModMenu.Models;

namespace ICSModMenu.Features
{
    public static class GameLogic
    {
        public static void SetMoney(float amount)
        {
            // From UnityEngine
            // PlayerPrefs seems to always be instantiated?
            PlayerPrefs.SetFloat("money", amount);
            PlayerPrefs.Save();

            // Since player prefs is always instantiated we can skip/check this when on main menu
            MoneyTaker.Instance?.GenerateMoneyTaker(amount);
        }
        public static void SetCrypto(CryptoType coin, float amount)
        {
            string key = null;

            if (coin == CryptoType.BTC) key = "btc";
            else if (coin == CryptoType.LTC) key = "ltc";
            else if (coin == CryptoType.DOGE) key = "doge";
            else if (coin == CryptoType.ETH) key = "eth";
            else if (coin == CryptoType.XRP) key = "xrp";
            else if (coin == CryptoType.IOTA) key = "iota";
            else if (coin == CryptoType.EOS) key = "eos";
            else if (coin == CryptoType.CARDANO) key = "cardano";
            else if (coin == CryptoType.MONERO) key = "monero";
            else if (coin == CryptoType.TETHER) key = "tether";
            else if (coin == CryptoType.DASH) key = "dash";
            else if (coin == CryptoType.ZCASH) key = "zcash";

            if (key == null)
                return;

            PlayerPrefs.SetFloat(key, amount);
            PlayerPrefs.Save();
        }
    }
}
