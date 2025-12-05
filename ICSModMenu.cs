using BepInEx;
using UnityEngine;
using HarmonyLib;

using ICSModMenu.Utils;
using ICSModMenu.Menus;


namespace ICSModMenu
{
    [BepInPlugin("com.hallowslab.ICSModMenu", "Internet Cafe Simulator Mod Menu", "0.0.3")]
    public class ModMenuPlugin : BaseUnityPlugin
    {
        private bool menuVisible = false;

        // In game class references
        private TrashSystem trashSystem;
        private PlayerStats playerStats;
        private CivilManager civilManager;

        // Public read-only exposure
        public PlayerStats PlayerStats => playerStats;
        public TrashSystem TrashSystem => trashSystem;
        public CivilManager CivilManager => civilManager;

        // Helpers for cheats
        public GameActions Actions;

        private Harmony HarmonyInstance;
        public bool thiefPatchEnabled = false;
        public bool beggarPatchEnabled = false;

        // Mod menu and submenus
        // Track which menu is currently active
        public enum MenuPage
        {
            Main,
            Cheats,
            Patches
        }

        // Public so menus can read/write it
        public MenuPage ActivePage { get; set; } = MenuPage.Main;
        public MainMenu mainMenu;
        public CheatsMenu cheatsMenu;
        public PatchesMenu patchesMenu;

        //  Forward calls for patches
        public void ToggleThiefPatch()
        {
            PatchActions.ToggleThiefPatch(
                harmony: HarmonyInstance,
                enabledFlag: ref thiefPatchEnabled,
                logger: this.Logger
            );
        }

        public void ToggleBeggarPatch()
        {
            PatchActions.ToggleBeggarPatch(
                harmony: HarmonyInstance,
                enabledFlag: ref beggarPatchEnabled,
                logger: this.Logger
            );
        }

        void Awake()
        {
            // Required to allow opening the overlay before the menu
            DebugOverlay.Log("");
            // Global harmony instance, to allow patching and unpatching
            HarmonyInstance = new Harmony("com.hallowslab.ICSModMenu");

            // instantiate the menus
            mainMenu = new MainMenu(this);
            cheatsMenu = new CheatsMenu(this);
            patchesMenu = new PatchesMenu(this);
            // instantiate actions
            Actions = new GameActions(this);
        }

        void OnDestroy()
        {
            PatchToggle.UnpatchAll(HarmonyInstance);
        }

        void Update()
        {
            // TODO: This is not working
            if (LoadingHelper.IsLoading())
            {
                DebugOverlay.Log("Functionality disabled while loading");
                return; // disable hotkeys while loading
            }
            if (playerStats == null)
            {
                playerStats = FindObjectOfType<PlayerStats>();
            }
            if (civilManager == null)
                civilManager = FindObjectOfType<CivilManager>();
            if (trashSystem == null)
                trashSystem = FindObjectOfType<TrashSystem>();
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

            switch (ActivePage)
            {
                case MenuPage.Main:
                    mainMenu.Draw();
                    break;
                case MenuPage.Cheats:
                    cheatsMenu.Draw();
                    break;
                case MenuPage.Patches:
                    patchesMenu.Draw();
                    break;
            }
        }
    }
}
