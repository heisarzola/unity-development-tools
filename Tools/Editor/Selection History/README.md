# Selection History
A window dedicated to storing the last selected assets in both the active scene and project files.

This is useful when having to switch back and forth between assets to edit them or copy some components.

#### Provided Tool:
![](https://github.com/heisarzola/Unity-Development-Tools/blob/master/Tools/Editor/Selection%20History/Selection%20History.gif)

## General Notes

* Once installed, the tool will be located on the top menu under the "Tools/Utilities/Selection History/Open Window" directory.
* The selection history isn't stored between sessions, it will be lost the next time you open Unity. It is inteded to be used for work sessions. If you want to save frequent selections, you might be interested in the [Favorite Assets Tool](https://github.com/heisarzola/Unity-Development-Tools/blob/master/Tools/Favorites%20Panel/) instead.
* You can [download this folder only](https://minhaskamal.github.io/DownGit/#/home?url=https://github.com/heisarzola/Unity-Development-Tools/tree/master/Tools/Editor/Selection%20History) instead of the whole project if you want to.

## As A Reminder 
* If you make any changes/improvements to this tool, please do consider sharing them to update the repository via a pull request.
* This project's license is located in the repository root under the name "LICENSE.md".

## Selection History Acknowledgments

* [DuzzOnDrums](https://pastebin.com/V9kkemiu) - For providing the base class, it was further improved by allowing to show/hide elements from project/scene, adding an option to ignore new selections if needed, by adding a clear distinction between scene and project elements on the "Any" view (scene elements have the scene name), and improved by allowing settings to be saved locally on EditorPrefs.