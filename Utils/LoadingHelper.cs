using UnityEngine;
using UnityEngine.SceneManagement;

namespace ICSModMenu.Utils
{
    public static class LoadingHelper
    {
        private static SaveManager saveManager;
        private static bool saveLoadInProgress = false;

        public static bool IsLoading()
        {
            // Check if SaveManager exists
            if (saveManager == null)
            {
                // Optimization: Don't spam FindObjectOfType every frame if it's missing.
                // Only try to find it if we are in a scene that might have it, or throttle it.
                // For now, simple throttling handled effectively by the fact that if it's null, 
                // we probably aren't loading a save game (unless we just started).
                // But to be safe against lag spikes:
                if (Time.frameCount % 60 == 0) // check once every ~60 frames
                {
                    saveManager = Object.FindObjectOfType<SaveManager>();
                }
            }

            // If no save manager, likely in main menu: not loading
            if (saveManager == null) return false;

            // If saveLoadInProgress flag is set, we are loading a save
            if (saveLoadInProgress) return true;

            // Optionally, check for any active scene load operations
            if (SceneManager.GetActiveScene().isLoaded == false) return true;

            return false;
        }

        // Call this via Harmony patch or manually at start/end of SaveManager.Load()
        public static void StartSaveLoad() => saveLoadInProgress = true;
        public static void EndSaveLoad() => saveLoadInProgress = false;
    }
}
