using System;
using System.Collections.Generic;
using ICSModMenu.Utils;
using UnityEngine;

namespace ICSModMenu.Menus.SubMenus
{
    public class SpawnerMenu
    {
        private ModMenuPlugin plugin;
        private Vector2 scrollPosition;

        // Valid IDs extracted from ItemMove.cs
        private readonly string[] spawnableItems = new string[]
        {
            "case1", "case2", "case3", "case4", "case5", "case6",
            "keyboard1", "keyboard2", "keyboard3", "keyboard4",
            "mouse1", "mouse2", "mouse3", "mouse4",
            "notebook1", "notebook2", "notebook3", "notebook4",
            "screen1", "screen2", "screen3", "screen4", "screen5", "screen6",
            "ac1", "ac2",
            "cfan1", "cfan2", "cfan3",
            "fan1", "fan2",
            "chair1", "chair2", "chair3", "chair4", "chair5", "chair6", "chair7", "chair8",
            "arcade1", "arcade2", "arcade3", "arcade4", "arcade5", "arcade6", "arcade7", "arcade8",
            "table1", "table2", "table3", "table4", "table5", "table6", "table7", "table8", "table9",
            "gameconsole1", "gameconsole2", "gameconsole3", "gameconsole4",
            "controller1", "controller2",
            "headset1", "headset2", "headset3", "headset4", "headset5",
            "tv1", "tv2", "tv3", "tv4", "tv5",
            "waterbootle", "cola", "hotdog", "hamburger", "energy", "chocolate",
            "tvtable", "poster", "miner"
        };

        public SpawnerMenu(ModMenuPlugin plugin)
        {
            this.plugin = plugin;
        }

        public void Draw()
        {
            if (GUILayout.Button("Back"))
            {
                plugin.ActivePage = ModMenuPlugin.MenuPage.Cheats;
            }

            GUILayout.BeginHorizontal();
            GUILayout.Label("Spawn Items (Click to spawn in front of you)", GUILayout.Height(25));
            GUILayout.EndHorizontal();

            GUILayout.Space(10);

            scrollPosition = GUILayout.BeginScrollView(scrollPosition);

            GUILayout.BeginVertical();
            
            // Group helper
            int columns = 3;
            for (int i = 0; i < spawnableItems.Length; i += columns)
            {
                GUILayout.BeginHorizontal();
                for (int j = 0; j < columns; j++)
                {
                    if (i + j < spawnableItems.Length)
                    {
                        string id = spawnableItems[i + j];
                        if (GUILayout.Button(id))
                        {
                            SpawnItem(id);
                        }
                    }
                    else
                    {
                        GUILayout.FlexibleSpace();
                    }
                }
                GUILayout.EndHorizontal();
            }

            GUILayout.EndVertical();
            GUILayout.EndScrollView();
        }

        private void SpawnItem(string itemId)
        {
            if (ItemMove.Instance == null)
            {
                ModMenuPlugin.Log.LogWarning("ItemMove.Instance is null. Cannot spawn.");
                DebugOverlay.Log("ItemMove.Instance is null. Cannot spawn.");
                return;
            }

            // Find valid position
            Vector3 spawnPos = Vector3.zero;
            Vector3 spawnRot = Vector3.zero;

            // Try to find player
            // We can access plugin.playerStats, usually that's on the player object
            if (plugin.PlayerStats != null)
            {
                Transform playerT = plugin.PlayerStats.transform;
                spawnPos = playerT.position + (playerT.forward * 2f) + (Vector3.up * 1f);
                spawnRot = playerT.eulerAngles;
            }
            else
            {
                // Fallback to Camera main
                Camera cam = Camera.main;
                if (cam != null)
                {
                    spawnPos = cam.transform.position + (cam.transform.forward * 2f);
                    spawnRot = new Vector3(0, cam.transform.eulerAngles.y, 0); // Keep it upright
                }
            }
            
            DebugOverlay.Log($"Spawning {itemId} at {spawnPos}");
            
            // Call the game's spawn method
            // public Transform SpawnSavedItem(string _itemID, float _posX, float _posY, float _posZ, float _rotX, float _rotY, float _rotZ, int _posterCount = -1)
            ItemMove.Instance.SpawnSavedItem(
                itemId, 
                spawnPos.x, spawnPos.y, spawnPos.z,
                spawnRot.x, spawnRot.y, spawnRot.z
            );
        }
    }
}
