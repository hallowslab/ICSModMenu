using BepInEx;
using BepInEx.Logging;
using UnityEngine;
using HarmonyLib;

using ICSModMenu.Utils;
using ICSModMenu.Menus;
using ICSModMenu.Menus.SubMenus;


namespace ICSModMenu
{
    [BepInPlugin("com.hallowslab.ICSModMenu", "Internet Cafe Simulator Mod Menu", "0.0.4")]
    public class ModMenuPlugin : BaseUnityPlugin
    {
        // https://docs.bepinex.dev/articles/dev_guide/plugin_tutorial/3_logging.html
        // This can be done with with a global plugin logger pattern. To apply the pattern, do the following:
        // - Create an internal static ManualLogSource field inside the plugin class
        // - In plugin's startup code, assign plugin's logger to the field
        // - In your other classes, use the static logger field from your plugin class
        internal static new ManualLogSource Log;
        // Tracks if the menu is open or closed
        private bool menuVisible = false;
        // In game class references
        private TrashSystem trashSystem;
        private PlayerStats playerStats;
        private CivilManager civilManager;
        private WorkersPanel workersPanel;

        // Public read-only exposure
        public PlayerStats PlayerStats => playerStats;
        public TrashSystem TrashSystem => trashSystem;
        public CivilManager CivilManager => civilManager;
        public WorkersPanel WorkersPanel => workersPanel;

        // Helpers for cheats
        public GameActions Actions;

        private Harmony HarmonyInstance;
        public bool thiefPatchEnabled = false;
        public bool beggarPatchEnabled = false;
        public bool playerStatsPatchEnabled = false;

        // Mod menu and submenus
        // Track which menu is currently active
        public enum MenuPage
        {
            Main,
            Cheats,
            Patches,
            Workers
        }

        // Public so menus can read/write it
        public MenuPage ActivePage { get; set; } = MenuPage.Main;
        public MainMenu mainMenu;
        public CheatsMenu cheatsMenu;
        public PatchesMenu patchesMenu;
        public WorkersMenu workersMenu;

        //  Forward calls for patches
        public void ToggleThiefPatch()
        {
            PatchActions.ToggleThiefPatch(
                harmony: HarmonyInstance,
                enabledFlag: ref thiefPatchEnabled
            );
        }
        public void ToggleBeggarPatch()
        {
            PatchActions.ToggleBeggarPatch(
                harmony: HarmonyInstance,
                enabledFlag: ref beggarPatchEnabled
            );
        }
        public void TogglePlayerStatsPatch()
        {
            PatchActions.TogglePlayerStatsPatch(
                harmony: HarmonyInstance,
                enabledFlag: ref playerStatsPatchEnabled
            );
        }

        void Awake()
        {
            // Required to allow opening the overlay before the menu
            DebugOverlay.Log("");
            DebugOverlay.Clear();
            // Assign logger
            Log = base.Logger;
            // Global harmony instance, to allow patching and unpatching
            HarmonyInstance = new Harmony("com.hallowslab.ICSModMenu");

            // instantiate the menus
            mainMenu = new MainMenu(this);
            cheatsMenu = new CheatsMenu(this);
            patchesMenu = new PatchesMenu(this);
            workersMenu = new WorkersMenu(this);
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
            if (workersPanel == null)
                workersPanel = FindObjectOfType<WorkersPanel>();
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
                case MenuPage.Workers:
                    workersMenu.Draw();
                    break;
            }
        }
    }
}
