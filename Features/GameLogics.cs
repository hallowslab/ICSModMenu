using UnityEngine;

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
    }
}
