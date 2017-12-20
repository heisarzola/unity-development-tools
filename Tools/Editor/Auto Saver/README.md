# Auto Saver
Ever lost progress because you didn't save often? Don't let that happen anymore! 

This little tool auto-saves the open scene every X minutes and before you enter runtime. 

This tool also supports saving scenes as a backup (to avoid modifying the original).

#### Auto Saver Preferences:

![Auto Saver Preferences](https://github.com/heisarzola/Unity-Development-Tools/blob/master/Tools/Editor/Auto%20Saver/Auto%20Saver.png "These are the Auto Saver Preferences.")

## General Notes

* Once installed, the tool will automatically run by itself, no special window needed. However, you can always configure it via the "Edit/Preferences/Auto Saver" menu.
* Please note that this tool ***REQUIRES*** to be within an "Editor" named folder to work. (For more information about the reason, look at [Unity's official documentation](https://docs.unity3d.com/560/Documentation/Manual/SpecialFolders.html).)
* You can [download this folder only](https://minhaskamal.github.io/DownGit/#/home?url=https://github.com/heisarzola/Unity-Development-Tools/tree/master/Tools/Editor/Auto%20Saver) instead of the whole project if you want to.

## As A Reminder 
* If you make any changes/improvements to this tool, please do consider sharing them to update the repository. The easiest way to do so is via an e-mail to: contact@heisarzola.com
* There is a [*newsletter for game development tips, tricks and tutorials*](https://heisarzola.us16.list-manage.com/subscribe?u=711c0d50be32d6a5eca3ccb18&id=43d6d70f28). Where you will also receive notifications **when new tools are added to this repository**, for those that are interested.
* This project's license is located in the repository root under the name "LICENSE.md".

## Auto Saver Acknowledgments

* [liortal53](https://github.com/liortal53/AutoSaveScene/blob/master/Assets/Editor/AutoSaveScene.cs) - For providing the base class, it was improved by adding several tweakable options on a preferences panel, as well as adapting it to Unity 2017.2. Also improved by adding the option to save only a max amount of backups on a per-scene basis, as well as an easily accesible button to delete all backups if needed.