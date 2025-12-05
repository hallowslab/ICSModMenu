using HarmonyLib;
using ICSModMenu.Patches;
using BepInEx.Logging;
using System.Reflection;

namespace ICSModMenu.Utils
{
    public static class PatchActions
    {
        public static void ToggleThiefPatch(
            Harmony harmony,
            ref bool enabledFlag,
            ManualLogSource logger)
        {
            PatchToggle.Toggle(
                harmony: harmony,
                originalMethod: typeof(ThiefManager).GetMethod("SendMyThief",
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic),
                patchMethod: typeof(ThiefManagerPatch).GetMethod("Prefix",
                    BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic),
                patchType: PatchType.Prefix,
                isEnabled: ref enabledFlag,
                logger: logger
            );
        }

        public static void ToggleBeggarPatch(
            Harmony harmony,
            ref bool enabledFlag,
            ManualLogSource logger)
        {
            PatchToggle.Toggle(
                harmony: harmony,
                originalMethod: typeof(BeggarManager).GetMethod("SendMyBeggar",
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic),
                patchMethod: typeof(BeggarManagerPatch).GetMethod("Prefix",
                    BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic),
                patchType: PatchType.Prefix,
                isEnabled: ref enabledFlag,
                logger: logger
            );
        }
    }
}
