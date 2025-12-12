# ICS Mod Menu

*A mod menu for **Internet Cafe Simulator**, powered by BepInEx.*

![Mod Status](https://img.shields.io/badge/status-active-brightgreen)
![BepInEx](https://img.shields.io/badge/BepInEx-5.x-blue)
![Platform](https://img.shields.io/badge/platform-Windows%20x64-lightgrey)

---

## Features

- **In‑game Mod Menu (F11)** – Toggle the mod UI at any time.
- **Modular Structure** – Clean separation of features, patches, menus and utilities.
- **Future‑proof Architecture** – Designed to be easily expanded

### Cheats

- **Set Money** – Enter any amount and instantly add it to your wallet.
- **Set Hunger** – Set the current hunger value.
- **Set Crypto** – Enter any amount select the coin and it gets added to portfolio.
- **Clear Trash** - Clears all trash in the cafe.
- **Send New Customer** - Sends a customer to the cafe
- **Add/Remove Chef** - Adds or removes the Chef
- **Add/Remove Bodyguard** - Adds or removes the Bodyguard
- **Unlock all rooms** - Unlocks all room expansions for the cafe

### Patches

- **Enable/Disable Thiefs** - Prevents Thiefs from being sent to the cafe (does not remove them from the game).
- **Enable/Disable Beggars** - Prevents Beggars from being sent to the cafe (does not remove them from the game).
- **Enable/Disable Hunger** - Prevents Hunger from decreasing.

---

## Requirements

- **[BepInEx 5 (x64)](https://github.com/BepInEx/BepInEx)**
  Required for loading and running the mod.

---

## Installation

### 1. Install BepInEx

1. Download **BepInEx x64** from the [official releases](https://github.com/BepInEx/BepInEx/releases):
   > If you run into issues, try the older version used during development:
   > [**5.4.23.4**](https://github.com/BepInEx/BepInEx/releases/tag/v5.4.23.4)

2. Extract the contents of the ZIP.

3. Place all extracted files into the **game’s root folder**
   (the folder containing the game executable).
   Example:

   ```cmd
   C:\SteamLibrary\steamapps\common\Internet Cafe Simulator\windows_content
   ```

---

### 2. Install the Mod

Place `ICSModMenu.dll` into:

```txt
<Internet Cafe Simulator root>\BepInEx\plugins\ICSModMenu.dll
```

Launch the game and press **F11** to open the menu.

---

## Development

### Building

1. Copy/rename:

   ```txt
   GamePath.props.template → GamePath.props
   ```

2. Edit the file and ensure the game path is correct.

3. Build the mod:

   ```cmd
   dotnet publish -c Release
   ```

   3.1. If you want to test the stubs locally you need to run

   ```cmd
      dotnet build -c Debug /p:CI=true
   ```

The DLL will be generated inside the `publish` folder.

### Creating a release

Since we can't include game's dlls releases must be compiled locally.

1. Compile the assets, make sure CI and UseStubs is not set or set to false
2. Create the structure like bepinex expects

   ```cmd
   # for release
   dotnet build ICSModMenu.csproj -c Release -o build/release
   New-Item -ItemType Directory -Force -Path .\BepInEx\plugins
   Copy-Item .\build\release\*.dll .\BepInEx\plugins\
   Compress-Archive -Path BepInEx ICSModMenu-Release.zip
   Remove-Item -Path BepInEx -Recurse -Force
   # for debug
   dotnet build ICSModMenu.csproj -c Debug -o build/debug
   New-Item -ItemType Directory -Force -Path .\BepInEx\plugins
   Copy-Item .\build\debug\*.dll .\BepInEx\plugins\
   Copy-Item .\build\debug\*.pdb .\BepInEx\plugins\
   Compress-Archive -Path BepInEx ICSModMenu-Debug.zip
   Remove-Item -Path BepInEx -Recurse -Force
   ```

3. Manually create a releases at a tag and upload artifacts

### TODOS/Issues

- UnlockAllRooms functionality can be broken if the mod is on that menu page while loading the game.
   Seems like icstore gets instanced during loading, then removed and instanced again on opening the game's browser page
- Unsure if SendNewCustomer is working as intended, needs further debugging

---

## Project Structure

```table
ICSModMenu/
├── Features/           # Game logic and mod features
├── Menus/              # Menus and submenus
├── Patches/            # Harmony patches
├── Utils/              # Helper classes
├── GamePath.props      # Local path to the game installation
└── ICSModMenu.cs    # Main BepInEx entrypoint
```

---

## Notes

- This mod is does not modify game files.
- For debugging, a Debug build will also generate PDBs for dnSpy inspection.

---

## Support

If you want improvements, more cheats, or UI enhancements, feel free to request them!
