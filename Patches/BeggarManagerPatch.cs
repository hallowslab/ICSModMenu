namespace ICSModMenu.Patches
{
    public static class BeggarManagerPatch
    {
        // Postfix runs after SendMyBeggar()
        public static bool Prefix(BeggarManager __instance)
        {
            return false;
        }
    }
}
