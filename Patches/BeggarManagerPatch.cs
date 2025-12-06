namespace ICSModMenu.Patches
{
    public static class BeggarManagerPatch
    {
        // Prefix runs before SendMyBeggar()
        public static bool Prefix(BeggarManager __instance)
        {
            return false;
        }
    }
}
