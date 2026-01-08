using UnityEngine;

using ICSModMenu.Models;

namespace ICSModMenu.Features
{
    public static class ICStoreFeatures
    {
        public static void UnlockAllRooms(RoomManager rm)
        {
            if (rm == null || rm.Rooms == null)
            {
                Debug.LogWarning("[ICStoreFeatures] UnlockAllRooms: RoomManager or Rooms is null");
                return;
            }

            for (int i = 0; i < rm.Rooms.Count; i++)
            {
                var room = rm.Rooms[i];
                if (room == null) continue;

                // Enable the current button
                if (room.curButton != null) room.curButton.interactable = false;

                // Enable next button (optional, usually for progression, can skip)
                if (room.nextButton != null) room.nextButton.interactable = true;

                // Open the lock
                if (room.dangerLock != null)
                {
                    room.dangerLock.lockOpen = true;
                    room.dangerLock.dangerCollider?.SetActive(false);
                }

                // Show the tick
                room.Tick?.SetActive(true);

                // Save state
                if (!string.IsNullOrEmpty(room.PrefsKey))
                {
                    PlayerPrefs.SetInt(room.PrefsKey, 50);
                }

                // Hide nextLock if assigned
                room.nextLock?.SetActive(false);
            }

            PlayerPrefs.Save();
            Debug.Log("[ICStoreFeatures] All rooms unlocked.");
        }

        #if DEBUG
        public static void DumpRoomReferences(RoomManager rm, string context)
        {
            if (rm == null)
            {
                string msg = $"[ICStoreFeatures:{context}] DumpRoomReferences: RoomManager is null";
                Debug.Log(msg);
                return;
            }

            for (int i = 0; i < rm.Rooms.Count; i++)
            {
                var room = rm.Rooms[i];
                string dangerName = room.dangerLock != null ? (room.dangerLock.gameObject != null ? room.dangerLock.gameObject.name : "<no gameObject>") : "<null>";
                string dangerActive = room.dangerLock != null && room.dangerLock.gameObject != null ? room.dangerLock.gameObject.activeSelf.ToString() : "<null>";
                string nextLockName = room.nextLock != null ? room.nextLock.name : "<null>";
                string curButtonName = room.curButton != null ? room.curButton.name : "<null>";
                string nextButtonName = room.nextButton != null ? room.nextButton.name : "<null>";

                string msg = $"[ICStoreFeatures] Room {i+1}: " +
                            $"danger='{dangerName}', " +
                            $"dangerGO active={dangerActive}, " +
                            $"nextLock='{nextLockName}', " +
                            $"curButton='{curButtonName}', " +
                            $"nextButton='{nextButtonName}'";
                Debug.Log(msg);
            }
        }
        #endif

    }

}