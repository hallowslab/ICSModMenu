using UnityEngine;

using ICSModMenu.Features;
using ICSModMenu.Utils;

namespace ICSModMenu.Menus.SubMenus
{
    public class StoreMenu
    {
        private ModMenuPlugin plugin;


        private static readonly float menuWidth = 240f;
        private static readonly float menuHeight = 200f;
        private static readonly float menuX = 10f;
        private static readonly float menuY = 10f;
        private static readonly float buttonWidth = 200f;
        private static readonly float buttonHeight = 30f;
        private readonly float buttonX = menuX + (menuWidth - buttonWidth) / 2f;


        public StoreMenu(ModMenuPlugin plugin)
        {
            this.plugin = plugin;
        }

        public void Draw()
        {
            GUI.Box(new Rect(menuX, menuY, menuWidth, menuHeight), "Store Menu");

            //
            // CLEAR TRASH SECTION
            //
            if (plugin.TrashSystem != null)
            {
                if (GUI.Button(new Rect(buttonX, 40, buttonWidth, buttonHeight), "Clear All Trash"))
                {
                    TrashFeatures.ClearAllTrash(plugin.TrashSystem);
                    DebugOverlay.Log("Cleared trash!");
                }
            }
            else
            {
                GUI.Label(new Rect(20, 40, 200, 20), "TrashSystem not available yet");
            }

            // 
            // SEND CLIENT SECTION
            // 
            if (plugin.CivilManager != null)
            {
                if (GUI.Button(new Rect(buttonX, 80, buttonWidth, buttonHeight), "Send Client"))
                {
                    CivilManagerFeatures.SendNewCustomer(plugin.CivilManager);
                    DebugOverlay.Log("Sent customer");
                }
            }
            else
            {
                GUI.Label(new Rect(20, 80, 200, 20), "CivilManager not available yet");
            }

            if (plugin.RoomManager != null)
            {
                if (GUI.Button(new Rect(buttonX, 120, buttonWidth, buttonHeight), "Unlock All Rooms"))
                {
                    ICStoreFeatures.UnlockAllRooms(plugin.RoomManager);
                }
            }
            else
            {
                GUI.Label(new Rect(20, 120, 200, 20), "RoomManager not available yet");
            }

            //
            // Back Button
            //
            if (GUI.Button(new Rect(buttonX, 160, buttonWidth, buttonHeight), "Back"))
            {
                plugin.ActivePage = ModMenuPlugin.MenuPage.Cheats;
            }
        }
    }
}
