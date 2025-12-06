using System.Reflection;
using BepInEx.Logging;
using HarmonyLib;

namespace ICSModMenu.Utils
{
    // 
    // Reference: http://harmony.pardeike.net/articles/basics.html
    // 
    public enum PatchType { Prefix, Postfix, Transpiler, All }

    /// <summary>
    /// Allows toggling individual Harmony patches on/off safely.
    /// </summary>
    public static class PatchToggle
    {
        /// <summary>
        /// Toggle a specific method patch.
        /// </summary>
        /// <param name="harmony">Main Harmony instance.</param>
        /// <param name="originalMethod">The method to patch (the game's method).</param>
        /// <param name="patchMethod">Your patch method (static).</param>
        /// <param name="patchType">Prefix / Postfix / Transpiler / All</param>
        /// <param name="isEnabled">Reference to a boolean storing the toggle state.</param>
        public static void Toggle(Harmony harmony, MethodInfo originalMethod, MethodInfo patchMethod, PatchType patchType, ref bool isEnabled)
        {
            if (harmony == null)
            {
                ModMenuPlugin.Log.LogError($"Missing main harmony instance: '{harmony}'");
                return;
            }

            if (originalMethod == null)
            {
                ModMenuPlugin.Log.LogError($"Original game method not found: '{originalMethod.Name}'");
                return;
            }

            if (patchMethod == null)
            {
                ModMenuPlugin.Log.LogError($"Patch method not found: '{patchMethod.Name}'");
                return; 
            }

            if (!patchMethod.IsStatic)
            {
                ModMenuPlugin.Log.LogError($"Patch method '{patchMethod.Name}' must be static!");
                return;
            }

            if (isEnabled)
            {
                // Remove *only this* patch method
                harmony.Unpatch(originalMethod, patchMethod);
                isEnabled = false;
                DebugOverlay.Log($"Patch method '{patchMethod.Name}' disabled for '{originalMethod.Name}'");
                ModMenuPlugin.Log.LogInfo($"Patch method '{patchMethod.Name}' disabled for '{originalMethod.Name}'");
                return;
            }

            // Apply the requested patch type
            var hm = new HarmonyMethod(patchMethod);

            switch (patchType)
            {
                case PatchType.Prefix:
                    harmony.Patch(originalMethod, prefix: hm);
                    break;

                case PatchType.Postfix:
                    harmony.Patch(originalMethod, postfix: hm);
                    break;

                case PatchType.Transpiler:
                    harmony.Patch(originalMethod, transpiler: hm);
                    break;

                case PatchType.All:
                    harmony.Patch(originalMethod, prefix: hm, postfix: hm, transpiler: hm);
                    break;
            }

            isEnabled = true;
            DebugOverlay.Log($"Patch method '{patchMethod.Name}' enabled for '{originalMethod.Name}'");
            ModMenuPlugin.Log.LogInfo($"Patch method '{patchMethod.Name}' enabled for '{originalMethod.Name}'");
        }

        /// <summary>
        /// Unpatch all patches created by this Harmony instance.
        /// Useful inside OnDestroy.
        /// </summary>
        public static void UnpatchAll(Harmony harmony)
        {
            if (harmony == null) return;
            harmony.UnpatchAll(harmony.Id);
        }
    }
}
