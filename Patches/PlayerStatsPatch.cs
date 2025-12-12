namespace ICSModMenu.Patches
{
    public static class PlayerStatsPatch
    {
        public static void Postfix(PlayerStats __instance)
        {
            // Keep hunger full
            __instance.hungry = 100f;
        }
    }
}