using BepInEx;
using UnityEngine;
using HarmonyLib;
using System.Reflection;

using ICSModMenu.Features;
using ICSModMenu.Utils;
using ICSModMenu.Patches;


namespace ICSModMenu
{
    [BepInPlugin("com.hallowslab.ICSModMenu", "Internet Cafe Simulator Mod Menu", "0.0.2")]
    public class ModMenuPlugin : BaseUnityPlugin
    {
        private bool menuVisible = false;
        private string moneyToAddText = "1000"; // store input as string
        private float moneyToSet = 1000f;
        // For hunger slider
        private float hungerValue = 100f;

        // In game class references
        private TrashSystem trashSystem;
        private PlayerStats playerStats;
        private CivilManager civilManager;
        private Harmony HarmonyInstance;
        public bool thiefPatchEnabled = false;
        public bool beggarPatchEnabled = false;

        void Awake()
        {
            // Required to allow opening the overlay before the menu
            DebugOverlay.Log("");
            // Global harmony instance, to allow patching and unpatching
            HarmonyInstance = new Harmony("com.hallowslab.ICSModMenu");
        }

        void OnDestroy()
        {
            PatchToggle.UnpatchAll(HarmonyInstance);
        }

        void Update()
        {
            if (playerStats == null)
            {
                playerStats = FindObjectOfType<PlayerStats>();
                if (playerStats != null)
                    hungerValue = playerStats.hungry;
            }
            if (civilManager == null)
                civilManager = FindObjectOfType<CivilManager>();
            if (trashSystem == null)
                trashSystem = FindObjectOfType<TrashSystem>();
            // TODO: This is not working
            if (LoadingHelper.IsLoading())
            {
                DebugOverlay.Log("Hotkeys disabled while loading");
                return; // disable hotkeys while loading
            }
            if (Input.GetKeyDown(KeyCode.F11))
            {
                menuVisible = !menuVisible;
                DebugOverlay.Log("Menu toggled");
                // Toggle cursor
                Cursor.visible = menuVisible; // show cursor when menu is open
                Cursor.lockState = menuVisible ? CursorLockMode.None : CursorLockMode.Locked;
            }
        }

        void OnGUI()
        {
            if (!menuVisible) return;

            // Mod Menu box
            GUI.Box(new Rect(10, 10, 260, 360), "Mod Menu");

            // Money label
            GUI.Label(new Rect(20, 40, 80, 20), "Amount:");
            // Creat input with default value
            moneyToAddText = GUI.TextField(new Rect(100, 40, 100, 20), moneyToAddText);


            // Logger.LogInfo($"playerStats={playerStats}, civilManager={civilManager}, trashSystem={trashSystem}");

            // Money feature
            // Convert string to float safely
            if (!float.TryParse(moneyToAddText, out moneyToSet))
            {
                moneyToSet = 0f;
            }
            // Create GUI button: https://docs.unity3d.com/ScriptReference/GUI.Button.html
            if (GUI.Button(new Rect(20, 70, 180, 30), "Set money"))
            {
                GameLogic.SetMoney(moneyToSet);
                DebugOverlay.Log($"Money set to: {moneyToSet:N0}$");
            }
            // PlayerStats do not exist yet
            if (playerStats == null)
            {
                GUI.Label(new Rect(20, 100, 200, 20), "PlayerStats not available yet");
            }
            else
            // Modify hunger feature
            {
                // Hunger label and slider
                GUI.Label(new Rect(20, 110, 100, 20), "Hunger:");
                hungerValue = GUI.HorizontalSlider(new Rect(100, 115, 120, 20), hungerValue, 0f, 100f);
                GUI.Label(new Rect(225, 110, 50, 20), $"{hungerValue:F0}");

                // Button to apply hunger value
                if (GUI.Button(new Rect(20, 140, 200, 30), "Set Hunger"))
                {
                    playerStats.hungry = hungerValue;
                    DebugOverlay.Log($"Hunger set to: {hungerValue:F0}");
                }
            }
            //  Send costumer feature
            if (civilManager == null)
            {
                GUI.Label(new Rect(20, 120, 200, 20), "CivilManager not available yet");
            }
            // We never seem to reach this statement?
            else if (civilManager.readyToCustomerCivil.Count <= 0)
            {
                GUI.Label(new Rect(20, 120, 200, 20), "No civilians available");
            }
            else
            {
                if (GUI.Button(new Rect(20, 180, 200, 30), "Send New Customer"))
                {
                    CivilManagerUtils.SendNewCustomer(civilManager);
                    DebugOverlay.Log("Sent a new customer!");
                }
            }
            // Clear trash feature
            if (trashSystem == null)
            {
                GUI.Label(new Rect(20, 140, 220, 20), "TrashSystem not available yet");
            }
            else
            {
                if (GUI.Button(new Rect(20, 220, 200, 30), "Clear All Trash"))
                {
                    TrashFeatures.ClearAllTrash(trashSystem);
                    DebugOverlay.Log("Cleared trash!");
                }
            }
            if (GUI.Button(new Rect(20, 260, 200, 30), thiefPatchEnabled ? "Enable Thiefs" : "Disable Thief"))
            {
                PatchToggle.Toggle(
                    harmony: HarmonyInstance,
                    originalMethod: typeof(ThiefManager).GetMethod("SendMyThief", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic),
                    patchMethod: typeof(ThiefManagerPatch).GetMethod("Prefix", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic),
                    patchType: PatchType.Prefix,
                    isEnabled: ref thiefPatchEnabled,
                    this.Logger
                );
            }
            if (GUI.Button(new Rect(20, 300, 200, 30), beggarPatchEnabled ? "Enable Beggars" : "Disable Beggars"))
            {
                PatchToggle.Toggle(
                    harmony: HarmonyInstance,
                    originalMethod: typeof(BeggarManager).GetMethod("SendMyBeggar", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic),
                    patchMethod: typeof(BeggarManagerPatch).GetMethod("Prefix", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic),
                    patchType: PatchType.Prefix,
                    isEnabled: ref beggarPatchEnabled,
                    logger: this.Logger
                );
            }
        }
    }
}
