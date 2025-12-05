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

            CivilManagerUtils.SendNewCustomer(plugin.CivilManager);
            DebugOverlay.Log("Sent customer");
        }
    }
}
