using HarmonyLib;

namespace ICSModMenu.Patches
{
    [HarmonyPatch(typeof(Civil))]
    public static class CivilPatch
    {
        [HarmonyPatch("Update")]
        [HarmonyPrefix]
        public static void Prefix(Civil __instance)
        {
            UnityEngine.Debug.Log("patch triggered");
            UnityEngine.Debug.Log($"Civil noPayEscape: {__instance.noPayEscape}");
        }
    }
}
