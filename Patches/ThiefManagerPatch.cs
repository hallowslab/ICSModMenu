namespace ICSModMenu.Patches
{
    public static class ThiefManagerPatch
    {
        // Prefix runs before SendMyThief
        public static bool Prefix(ThiefManager __instance)
        {
            // Returning false skips the original method
            return false;
        }
    }
}
