namespace ICSModMenu.Features
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
