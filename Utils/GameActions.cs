using System.Runtime.Serialization;
using ICSModMenu.Features;

namespace ICSModMenu.Utils
{
    public class GameActions
    {
        private readonly ModMenuPlugin plugin;

        public GameActions(ModMenuPlugin plugin)
        {
            this.plugin = plugin;
        }

        public void SetMoney(float amount)
        {
            GameLogic.SetMoney(amount);
            DebugOverlay.Log($"Money set to: {amount}");
        }

        public void SetHunger(float value)
        {
            if (plugin.PlayerStats == null) return;

            plugin.PlayerStats.hungry = value;
            DebugOverlay.Log($"Hunger set to {value}");
        }

        public void ClearTrash()
        {
            if (plugin.TrashSystem == null) return;

            TrashFeatures.ClearAllTrash(plugin.TrashSystem);
            DebugOverlay.Log("Cleared trash!");
        }

        public void SendCustomer()
        {
            if (plugin.CivilManager == null) return;

            CivilManagerFeatures.SendNewCustomer(plugin.CivilManager);
            DebugOverlay.Log("Sent customer");
        }

        public void AddBodyguard()
        {
            if (plugin.WorkersPanel == null) return;

            WorkersPanelFeatures.AddBodyguard(plugin.WorkersPanel);
            DebugOverlay.Log("Added bodyguard");
        }

        public void RemoveBodyguard()
        {
            if (plugin.WorkersPanel == null) return;

            WorkersPanelFeatures.RemoveBodyguard(plugin.WorkersPanel);
            DebugOverlay.Log("Removed bodyguard");
        }

        public void AddChef()
        {
            if (plugin.WorkersPanel == null) return;

            WorkersPanelFeatures.AddChef(plugin.WorkersPanel);
            DebugOverlay.Log("Added Chef");
        }

        public void RemoveChef()
        {
            if (plugin.WorkersPanel == null) return;

            WorkersPanelFeatures.RemoveChef(plugin.WorkersPanel);
            DebugOverlay.Log("Removed Chef");
        }
    }
}
