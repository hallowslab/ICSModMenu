using ICSModMenu.Utils;

namespace ICSModMenu.Patches
{
    public static class ThiefManagerPatch
    {
        // Prefix runs before SendMyThief
        public static bool Prefix(ThiefManager __instance)
        {
            // Returning false skips the original method
            DebugOverlay.Log("A thief was sent but the method is patched");
            return false;
        }
    }
}
