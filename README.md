## Requirements

- BepInEx

### Installing

1. Install [BepInEx](https://github.com/BepInEx/BepInEx/releases)
    - Download the latest release for windows x64, or if experiencing issues the version at the time of the creation of this mod [5.4.23.4](https://github.com/BepInEx/BepInEx/releases/tag/v5.4.23.4)
    - Extract the contents of the zip
    - Place the contents in the game root (the place where the executable is) Example: C:\SteamLibrary\steamapps\common\Internet Cafe Simulator\windows_content
2. Put ICSModMenu.dll in `<Internet Cafe Simulator root>\BepInEx\plugins\ICSModMenu.dll` where `<Internet Cafe Simulator root>`

### DEV

#### Building

- Copy/rename the GamePath.props.template into GamePath.props and make sure the game path is correct
- Run `dotnet publish -c Release`
