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
    [BepInPlugin("com.hallowslab.ICSModMenu", "Internet Cafe Simulator Mod Menu", "0.9.0")]
    public class ModMenuPlugin : BaseUnityPlugin
    {
        // https://docs.bepinex.dev/articles/dev_guide/plugin_tutorial/3_logging.html
        // This can be done with with a global plugin logger pattern. To apply the pattern, do the following:
        // - Create an internal static ManualLogSource field inside the plugin class
        // - In plugin's startup code, assign plugin's logger to the field
        // - In your other classes, use the static logger field from your plugin class
        internal static ManualLogSource Log;
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
            TeleportMenu,
            Spawner
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
        public SpawnerMenu spawnerMenu;

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

        
        private void Awake()
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
            spawnerMenu = new SpawnerMenu(this);
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

        // Performance optimization
        private float nextCheckTime = 0f;
        private const float CheckInterval = 1.0f;

        private void Update()
        {
            // TODO: This is not working
            if (LoadingHelper.IsLoading())
            {
                DebugOverlay.Log("Functionality disabled while loading");
                return; // disable hotkeys while loading
            }
            // Throttle the heavy FindObjectOfType calls to once per second
            if (Time.time >= nextCheckTime)
            {
                nextCheckTime = Time.time + CheckInterval;
                
                Ensure(ref playerStats);
                Ensure(ref civilManager);
                Ensure(ref trashSystem);
                Ensure(ref workersPanel);
                Ensure(ref icstore);
                Ensure(ref exchange);
            }
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

        // Window rect for the draggable window
        private Rect windowRect = new Rect(20, 20, 300, 400);
        
        // Styling
        private GUIStyle windowStyle;
        private GUIStyle boxStyle;
        private GUIStyle buttonStyle;
        private GUIStyle labelStyle;
        private GUIStyle textFieldStyle;
        private Texture2D darkBackground;
        private bool stylesInitialized = false;

        private void InitStyles()
        {
            if (stylesInitialized) return;

            // Create a 1x1 dark texture
            darkBackground = new Texture2D(1, 1);
            darkBackground.SetPixel(0, 0, new Color(0.1f, 0.1f, 0.1f, 1f)); // Dark Gray Opaque
            darkBackground.Apply();

            // Window Style
            windowStyle = new GUIStyle(GUI.skin.window);
            windowStyle.normal.background = darkBackground;
            windowStyle.onNormal.background = darkBackground;
            windowStyle.normal.textColor = Color.white;
            windowStyle.onNormal.textColor = Color.white;
            windowStyle.active.background = darkBackground;
            windowStyle.focused.background = darkBackground;
            windowStyle.hover.background = darkBackground;
            
            // Make title bar bigger
            windowStyle.border.top = 40; 
            windowStyle.padding.top = 45; // Push content down
            windowStyle.fontSize = 20; // Bigger title text
            windowStyle.alignment = UnityEngine.TextAnchor.UpperCenter;
            windowStyle.fontStyle = UnityEngine.FontStyle.Bold;

            // Box Style
            boxStyle = new GUIStyle(GUI.skin.box);
            boxStyle.normal.textColor = Color.white;
            
            // Button Style
            buttonStyle = new GUIStyle(GUI.skin.button);
            buttonStyle.normal.textColor = Color.white;
            
            // Label Style
            labelStyle = new GUIStyle(GUI.skin.label);
            labelStyle.normal.textColor = Color.white;

            // TextField Style
            textFieldStyle = new GUIStyle(GUI.skin.textField);
            textFieldStyle.normal.textColor = Color.white;

            stylesInitialized = true;
        }

        private void OnGUI()
        {
            if (!menuVisible) return;

            if (!stylesInitialized) InitStyles();

            GUI.skin.window = windowStyle;
            GUI.skin.box = boxStyle;
            GUI.skin.button = buttonStyle;
            GUI.skin.label = labelStyle;
            GUI.skin.textField = textFieldStyle;
            
            // Drag indicator in title
            // We use a different title variable or just format it here
            string titleBuffer = "  ::  Mod Menu v0.8.0  ::  "; // ASCII grip hint

            windowRect = GUILayout.Window(1001, windowRect, DoWindow, titleBuffer);
        }

        private void DoWindow(int windowID)
        {
            GUILayout.BeginVertical();
            
            // Add space for the large title bar
            GUILayout.Space(10); 

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
                case MenuPage.Spawner:
                    spawnerMenu.Draw();
                    break;
            }

            GUILayout.EndVertical();

            // Draw Resize Handle
            ResizeWindow();

            // Make the window draggable
            // Drag area covers the enlarged title bar
            GUI.DragWindow(new Rect(0, 0, 10000, 40)); 
        }

        private void ResizeWindow()
        {
            Vector2 mouse = Event.current.mousePosition;
            Rect resizeRect = new Rect(windowRect.width - 20, windowRect.height - 20, 20, 20);
             
            // Draw a visual indicator
            GUI.Box(resizeRect, "↘");

            if (Event.current.type == EventType.MouseDown && resizeRect.Contains(mouse))
            {
                isResizing = true;
                resizeStart = mouse;
                startSize = new Vector2(windowRect.width, windowRect.height);
                Event.current.Use(); // Consume event
            }
            if (Event.current.type == EventType.MouseUp)
            {
                isResizing = false;
            }
            if (Event.current.type == EventType.MouseDrag && isResizing)
            {
                Vector2 delta = mouse - resizeStart;
                windowRect.width = Mathf.Max(200, startSize.x + delta.x);
                windowRect.height = Mathf.Max(200, startSize.y + delta.y);
                Event.current.Use(); // Consume event to prevent drag window
            }
        }

        private bool isResizing = false;
        private Vector2 resizeStart;
        private Vector2 startSize;
    }
}
