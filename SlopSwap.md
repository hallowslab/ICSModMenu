# Assembly Hot-Swap Trick (BepInEx / Unity Modding)

This document summarizes a practical method for "hot-swapping" your
plugin logic during development **without restarting the game**.\
This is not an official BepInEx feature --- it's a workflow trick.

------------------------------------------------------------------------

## ⚡ Goal

Reload your updated C# code while the game is running by: - Compiling
your plugin into a **separate DLL** (logic assembly) - Having the
BepInEx plugin **load and unload that DLL at runtime** - Allowing you to
overwrite and recompile quickly

------------------------------------------------------------------------

## 🧩 Architecture

    ICSModMenu/
    │
    ├── ICSModMenu.Plugin.dll        ← Main BepInEx plugin (stable, never reloaded)
    └── ICSModMenu.Core.dll          ← Your hot‑swappable logic assembly

### Plugin Responsibilities

- Watches the `ICSModMenu.Core.dll` file
- Loads the assembly via `Assembly.Load`
- Stores a reference to the loaded types
- Calls into methods inside the hot‑swapped assembly

### Hot‑Swap Assembly Responsibilities

- Contains your real mod logic
- Recompile → overwrite → plugin loads new version

------------------------------------------------------------------------

## 🛠 Minimal Working Example

### **1. Main plugin (non-hot-swapped)**

    ```csharp
    using BepInEx;
    using System.IO;
    using System.Reflection;
    using UnityEngine;

    namespace ICSModMenu
    {
        [BepInPlugin("com.your.mod", "ModMenu", "1.0.0")]
        public class ModMenuPlugin : BaseUnityPlugin
        {
            private Assembly hotAssembly;
            private FileSystemWatcher watcher;
            private object hotLogicInstance;

            void Start()
            {
                LoadHotAssembly();
                SetupWatcher();
            }

            void LoadHotAssembly()
            {
                string path = Path.Combine(Paths.PluginPath, "ICSModMenu.Core.dll");
                if (!File.Exists(path))
                {
                    Logger.LogError("Hot assembly missing!");
                    return;
                }

                byte[] data = File.ReadAllBytes(path);
                hotAssembly = Assembly.Load(data);

                var logicType = hotAssembly.GetType("ICSModMenu.Core.HotLogic");
                hotLogicInstance = System.Activator.CreateInstance(logicType);

                Logger.LogInfo("Hot assembly loaded.");
            }

            void SetupWatcher()
            {
                string dir = Paths.PluginPath;
                watcher = new FileSystemWatcher(dir, "ICSModMenu.Core.dll");
                watcher.NotifyFilter = NotifyFilters.LastWrite;
                watcher.Changed += (s, e) =>
                {
                    Logger.LogInfo("Detected change, reloading hot assembly...");
                    LoadHotAssembly();
                };
                watcher.EnableRaisingEvents = true;
            }

            void Update()
            {
                hotAssembly?.GetType("ICSModMenu.Core.HotLogic")
                    ?.GetMethod("Update")
                    ?.Invoke(hotLogicInstance, null);
            }
        }
    }
    ```

------------------------------------------------------------------------

### **2. Hot‑swappable assembly**

    ```csharp
    namespace ICSModMenu.Core
    {
        public class HotLogic
        {
            public void Update()
            {
                // Your logic here — this updates live!
            }

            public void AddMoney(float amount)
            {
                // any logic you want
            }
        }
    }
    ```

Recompile → overwrite `ICSModMenu.Core.dll` → plugin reloads
automatically.

------------------------------------------------------------------------

## ✔ Advantages

- Very fast iteration
- Game stays running
- Keeps UI separate from logic
- You can reload 50+ times without restarting the game

## ✖ Limitations / Warnings

- Unity *cannot unload assemblies*, so memory will grow slowly\
    (OK for development, not for production).
- If you break the plugin loader, restart the game.
- Static fields in hot module persist until restart.

------------------------------------------------------------------------

## 🧠 Recommended Workflow

1. Keep your BepInEx plugin tiny and stable.\
2. Put 99% of the code in `ICSModMenu.Core.dll`.\
3. Use a file watcher to auto-reload logic.\
4. Recompile and overwrite the DLL anytime.

------------------------------------------------------------------------

## 📂 Suggested Folder Structure in Git

    /ModMenuPlugin
        /Plugin     ← stable BepInEx plugin
        /Core       ← your hot-reloadable logic
        HotSwap.md  ← this file

------------------------------------------------------------------------

## 👍 Good for

- UI iteration\
- Gameplay logic\
- Debug menus\
- Rapid experimentation

------------------------------------------------------------------------

This is a dev-mode trick but incredibly useful for Unity / BepInEx
modding.
