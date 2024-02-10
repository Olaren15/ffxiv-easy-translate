# Easy Translate

Easy Translate is a [Dalamud](https://github.com/goatcorp/Dalamud) plugin that allows you to search the translations for almost anything in Final Fantasy XIV!
It's really useful when you are playing with people who have the game in another language.

## How To Use

> ⚠️ This plugin is currently under development and therefore is not yet available on the official Dalamud plugin source. You will have to add it manually. ⚠️

1. Open the dalamud settings via the in-game UI or by entering `/xlsettings` in the chat window.
2. Head over to the "Experimental" tab
3. Scroll all the way down to the "Custom Plugin Repositories" section.
4. Copy and paste the following link into the first free text input field: `https://raw.githubusercontent.com/Olaren15/ffxiv-easy-translate/master/repo.json`
5. Click on the "+" button on the right hand side and make sure the repo is enabled with a checkmark.
6. **Click on the save icon in the bottom right corner**

## For developers

### Prerequisites

EasyTranslate assumes all the following prerequisites are met:

* XIVLauncher, FINAL FANTASY XIV, and Dalamud have all been installed and the game has been run with Dalamud at least once.
* XIVLauncher is installed to its default directories and configurations.
  * If a custom path is required for Dalamud's dev directory, it must be set with the `DALAMUD_HOME` environment variable.
* A .NET Core 7 SDK has been installed and configured, or is otherwise available. (In most cases, the IDE will take care of this.)

### Building

1. Open up `EasyTranslate.sln` in your C# editor of choice (likely [Visual Studio 2022](https://visualstudio.microsoft.com) or [JetBrains Rider](https://www.jetbrains.com/rider/)).
2. Build the solution. By default, this will build a `Debug` build, but you can switch to `Release` in your IDE.
3. The resulting plugin can be found at `EasyTranslate/bin/x64/Debug/SamplePlugin.dll` (or `Release` if appropriate.)

### Activating in-game

1. Launch the game and use `/xlsettings` in chat or `xlsettings` in the Dalamud Console to open up the Dalamud settings.
    * In here, go to `Experimental`, and add the full path to the `SamplePlugin.dll` to the list of Dev Plugin Locations.
2. Next, use `/xlplugins` (chat) or `xlplugins` (console) to open up the Plugin Installer.
    * In here, go to `Dev Tools > Installed Dev Plugins`, and the `SamplePlugin` should be visible. Enable it.
3. You should now be able to use `/trans` (chat) or `trans` (console)!

Note that you only need to add it to the Dev Plugin Locations once (Step 1); it is preserved afterwards. You can disable, enable, or load your plugin on startup through the Plugin Installer.
