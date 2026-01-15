using UnityEngine;

using ICSModMenu.Features;
using ICSModMenu.Utils;

namespace ICSModMenu.Menus.SubMenus
{
    public class StoreMenu
    {
        private ModMenuPlugin plugin;





        public StoreMenu(ModMenuPlugin plugin)
        {
            this.plugin = plugin;
        }

        public void Draw()
        {
            //
            // CLEAR TRASH SECTION
            //
            if (plugin.TrashSystem != null)
            {
                if (GUILayout.Button("Clear All Trash"))
                {
                    TrashFeatures.ClearAllTrash(plugin.TrashSystem);
                    DebugOverlay.Log("Cleared trash!");
                }
            }
            else
            {
                GUILayout.Label("TrashSystem not available yet");
            }

            GUILayout.Space(10);

            // 
            // SEND CLIENT SECTION
            // 
            if (plugin.CivilManager != null)
            {
                if (GUILayout.Button("Send Client"))
                {
                    CivilManagerFeatures.SendNewCustomer(plugin.CivilManager);
                    DebugOverlay.Log("Sent customer");
                }
            }
            else
            {
                GUILayout.Label("CivilManager not available yet");
            }

            GUILayout.Space(10);

            if (plugin.RoomManager != null)
            {
                if (GUILayout.Button("Unlock All Rooms"))
                {
                    ICStoreFeatures.UnlockAllRooms(plugin.RoomManager);
                }
            }
            else
            {
                GUILayout.Label("Open icstore page in-game first");
            }

            GUILayout.FlexibleSpace();

            //
            // Back Button
            //
            if (GUILayout.Button("Back"))
            {
                plugin.ActivePage = ModMenuPlugin.MenuPage.Cheats;
            }
        }
    }
}
