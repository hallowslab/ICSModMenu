using BepInEx;
using BepInEx.Logging;
using UnityEngine;
using HarmonyLib;

using ICSModMenu.Utils;
using ICSModMenu.Menus;
using ICSModMenu.Menus.SubMenus;
using ICSModMenu.Models;


namespace ICSModMenu
{
    [BepInPlugin("com.hallowslab.ICSModMenu", "Internet Cafe Simulator Mod Menu", "0.7.4")]
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
        private icstore icstore;
        private Exchange exchange;

        // Public read-only exposure
        public PlayerStats PlayerStats => playerStats;
        public TrashSystem TrashSystem => trashSystem;
        public CivilManager CivilManager => civilManager;
        public WorkersPanel WorkersPanel => workersPanel;
        public icstore ICStore => icstore;
        public Exchange Exchange => exchange;

        private Harmony HarmonyInstance;
        // For tracking state
        public bool thiefPatchEnabled = false;
        public bool beggarPatchEnabled = false;
        public bool playerStatsPatchEnabled = false;
        // custom states
        public RoomManager RoomManager { get; private set; }

        // Mod menu and submenus
        // Track which menu is currently active
        public enum MenuPage
        {
            Main,
            Cheats,
            Patches,
            Workers,
            PlayerStatsMenu,
            CurrenciesMenu,
            CryptoHoldingsMenu,
            StoreMenu,
            TeleportMenu
        }

        // Public so menus can read/write it
        public MenuPage ActivePage { get; set; } = MenuPage.Main;
        public MainMenu mainMenu;
        public CheatsMenu cheatsMenu;
        public PatchesMenu patchesMenu;
        public WorkersMenu workersMenu;
        public PlayerStatsMenu playerStatsMenu;
        public CurrenciesMenu currenciesMenu;
        public CryptoHoldingsMenu cryptoHoldingsMenu;
        public StoreMenu storeMenu;
        public TeleportMenu teleportMenu;

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

        #if CI
        private new void Awake()
        #else
        private void Awake()
        #endif
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
            playerStatsMenu = new PlayerStatsMenu(this);
            currenciesMenu = new CurrenciesMenu(this);
            cryptoHoldingsMenu = new CryptoHoldingsMenu(this);
            storeMenu = new StoreMenu(this);
            teleportMenu = new TeleportMenu(this);
        }

        void OnDestroy()
        {
            PatchToggle.UnpatchAll(HarmonyInstance);
        }

        private static void Ensure<T>(ref T field) where T : UnityEngine.Object
        {
            if (field == null)
            {
                field = Object.FindObjectOfType<T>();
            }
        }

        #if CI
        private new void Update()
        #else
        private void Update()
        #endif
        {
            // TODO: This is not working
            if (LoadingHelper.IsLoading())
            {
                DebugOverlay.Log("Functionality disabled while loading");
                return; // disable hotkeys while loading
            }
            Ensure(ref playerStats);
            Ensure(ref civilManager);
            Ensure(ref trashSystem);
            Ensure(ref workersPanel);
            Ensure(ref icstore);
            Ensure(ref exchange);
            if (icstore != null && RoomManager == null)
            {
                RoomManager = new RoomManager(icstore);
                #if DEBUG
                DebugOverlay.Log("RoomManager initialized");
                Features.ICStoreFeatures.DumpRoomReferences(RoomManager, "startup");
                #endif
            }
            if (Input.GetKeyDown(KeyCode.F11))
            {
                menuVisible = !menuVisible;
                // Toggle cursor
                Cursor.visible = menuVisible; // show cursor when menu is open
                Cursor.lockState = menuVisible ? CursorLockMode.None : CursorLockMode.Locked;
                #if DEBUG
                DebugOverlay.Log("Menu toggled");
                #endif
            }
        }

        #if CI
        private new void OnGUI()
        #else
        private void OnGUI()
        #endif
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
                case MenuPage.PlayerStatsMenu:
                    playerStatsMenu.Draw();
                    break;
                case MenuPage.CurrenciesMenu:
                    currenciesMenu.Draw();
                    break;
                case MenuPage.CryptoHoldingsMenu:
                    cryptoHoldingsMenu.Draw();
                    break;
                case MenuPage.StoreMenu:
                    storeMenu.Draw();
                    break;
                case MenuPage.TeleportMenu:
                    teleportMenu.Draw();
                    break;
            }
        }
    }
}
