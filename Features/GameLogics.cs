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
            var moneyTaker = GetMoneyTaker();
            moneyTaker?.GenerateMoneyTaker(amount);
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
        public static void SetCryptoCoinPrice(CryptoType coin, float price)
        {
            var cryptoManager = GetCryptoManager();
            if (cryptoManager == null)
                return;

            // Map CryptoType to the corresponding Coin object and its Myprefs key
            Coin coinObj = null;
            string prefsKey = null;

            switch (coin)
            {
                case CryptoType.BTC:
                    coinObj = cryptoManager.bitcoin;
                    prefsKey = cryptoManager.bitcoin?.Myprefs;
                    break;
                case CryptoType.LTC:
                    coinObj = cryptoManager.litecoin;
                    prefsKey = cryptoManager.litecoin?.Myprefs;
                    break;
                case CryptoType.DOGE:
                    coinObj = cryptoManager.dogeCoin;
                    prefsKey = cryptoManager.dogeCoin?.Myprefs;
                    break;
                case CryptoType.ETH:
                    coinObj = cryptoManager.etherium;
                    prefsKey = cryptoManager.etherium?.Myprefs;
                    break;
                case CryptoType.XRP:
                    coinObj = cryptoManager.xrp;
                    prefsKey = cryptoManager.xrp?.Myprefs;
                    break;
                case CryptoType.IOTA:
                    coinObj = cryptoManager.iota;
                    prefsKey = cryptoManager.iota?.Myprefs;
                    break;
                case CryptoType.EOS:
                    coinObj = cryptoManager.eos;
                    prefsKey = cryptoManager.eos?.Myprefs;
                    break;
                case CryptoType.CARDANO:
                    coinObj = cryptoManager.cardano;
                    prefsKey = cryptoManager.cardano?.Myprefs;
                    break;
                case CryptoType.MONERO:
                    coinObj = cryptoManager.monero;
                    prefsKey = cryptoManager.monero?.Myprefs;
                    break;
                case CryptoType.TETHER:
                    coinObj = cryptoManager.tether;
                    prefsKey = cryptoManager.tether?.Myprefs;
                    break;
                case CryptoType.DASH:
                    coinObj = cryptoManager.dash;
                    prefsKey = cryptoManager.dash?.Myprefs;
                    break;
                case CryptoType.ZCASH:
                    coinObj = cryptoManager.zCash;
                    prefsKey = cryptoManager.zCash?.Myprefs;
                    break;
            }

            if (coinObj == null || prefsKey == null)
                return;

            // Update the in-game coin price
            coinObj.coinPrice = price;
            
            // Persist to PlayerPrefs
            PlayerPrefs.SetFloat(prefsKey, price);
            PlayerPrefs.Save();
        }

        public static CryptoManager GetCryptoManager()
        {
            return Object.FindObjectOfType<CryptoManager>();
        }

        public static MoneyTaker GetMoneyTaker()
        {
            return Object.FindObjectOfType<MoneyTaker>();
        }
    }
}
