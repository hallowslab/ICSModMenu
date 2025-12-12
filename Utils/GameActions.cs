using System.Runtime.Serialization;
using ICSModMenu.Features;
using ICSModMenu.Models;

namespace ICSModMenu.Utils
{
    public class GameActions
    {
        private readonly ModMenuPlugin plugin;

        public GameActions(ModMenuPlugin plugin)
        {
            this.plugin = plugin;
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
