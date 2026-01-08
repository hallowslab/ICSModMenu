using UnityEngine;

using ICSModMenu.Utils;

namespace ICSModMenu.Features
{
    public static class TrashFeatures
    {
        public static void ClearAllTrash(TrashSystem trashSystem)
        {
            if (trashSystem == null) return;

            // deactivate all trash
            void DeactivateArray(GameObject[] arr)
            {
                if (arr == null) return;
                foreach (var obj in arr)
                    obj?.SetActive(false);
            }

            DeactivateArray(trashSystem.room1Trash);
            DeactivateArray(trashSystem.room2Trash);
            DeactivateArray(trashSystem.room3Trash);
            DeactivateArray(trashSystem.room4Trash);
            DeactivateArray(trashSystem.room5Trash);
            DeactivateArray(trashSystem.room6Trash);
            DeactivateArray(trashSystem.room7Trash);
            DeactivateArray(trashSystem.room8Trash);
            DeactivateArray(trashSystem.room9Trash);

            // reset private fields using reflection
            var flags = System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance;
            for (int i = 1; i <= 9; i++)
            {
                var field = typeof(TrashSystem).GetField($"_room{i}Rand", flags);
                field?.SetValue(trashSystem, 0);
            }

            DebugOverlay.Log("All trash cleared!");
        }
    }
}
