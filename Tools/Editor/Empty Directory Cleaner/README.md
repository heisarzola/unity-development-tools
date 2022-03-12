# Empty Directory Cleaner
A straightforward tool in charge of deleting all empty folders located within a project.

This is specially useful when working with git, because git ***DOESN'T*** track any empty folders, but UNITY still makes a .meta file for it which ***IS*** tracked. This is really annoying in teams, as esentially every commit will have "phantom/undead" folders, that will keep going back and forth.

#### Provided Tool:
![](https://github.com/heisarzola/Unity-Development-Tools/blob/master/Tools/Editor/Empty%20Directory%20Cleaner/Empty%20Directory%20Cleaner.gif)

## General Notes

* Once installed, the tool will be located on the top menu under the "Tools/Project/Delete Empty Folders" directory.
* Please note that this tool ***REQUIRES*** to be within an "Editor" named folder to work. (For more information about the reason, look at [Unity's official documentation](https://docs.unity3d.com/560/Documentation/Manual/SpecialFolders.html).)
* For the auto-cleaning-on-save to work, keep in mind that ***the window needs to be open***. But it is so small you can just tuck it away in a corner, so it shouldn't be a problem.
* You can [download this folder only](https://minhaskamal.github.io/DownGit/#/home?url=https://github.com/heisarzola/Unity-Development-Tools/tree/master/Tools/Editor/Empty%20Directory%20Cleaner) instead of the whole project if you want to.

## As A Reminder 
* If you make any changes/improvements to this tool, please do consider sharing them to update the repository via a pull request.
* This project's license is located in the repository root under the name "LICENSE.md".

## Empty Directory Cleaner Acknowledgments

* [amlovey](http://www.amlovey.com/) - For providing the idea of quickly opening special folders.