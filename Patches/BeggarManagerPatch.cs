using ICSModMenu.Utils;

namespace ICSModMenu.Patches
{
    public static class BeggarManagerPatch
    {
        // Prefix runs before SendMyBeggar()
        public static bool Prefix(BeggarManager __instance)
        {
            DebugOverlay.Log("A beggar was sent but the method is patched");
            return false;
        }
    }
}
