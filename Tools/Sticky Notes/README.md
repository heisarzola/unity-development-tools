# Sticky Notes
This tool allows to create in any scene an EditorOnly GameObject (Meaning they aren't included in builds) that allows to effortlessly make notes and "paste them", wherever needed in scene.

This is very handy for when certain areas of a game need to be improved, but corrections can't be done right away. The drawn gizmo also provides a visual reminder that something needs to be corrected.

#### Provided Tool:
![](https://github.com/heisarzola/Unity-Development-Tools/blob/master/Tools/Sticky%20Notes/Sticky%20Notes.gif)

## General Notes

* Once installed, the tool will be located on the top menu under the "Tools/Scene/Sticky Notes" directory to either create a new note or open the search window.
* To make new notes you can alternatively add them via the "+" button on the search window, or add the "StickyNote" component to an empty GameObject. ***FAIR WARNING: DO NOT ADD THIS DIRECTLY TO IMPORTANT GAME OBJECTS***, as it will change their tag to "EditorOnly" and will be excluded from the build. Instead, you should have a section in your hierarchy composed of Sticky Notes only.
* This tool requires the [Sticky Notes Icons](https://github.com/heisarzola/Unity-Development-Tools/tree/master/Gizmos/Sticky%20Notes) to work, they are ***MANDATORY TO HAVE*** in order for that tool to work. Also, the names and path of said icons are hardcoded so please keep all of them inside the "Assets/Gizmos/Sticky Notes" directory with the provided names intact. (Unless you wish to manually change them).
* You can [download this folder only](https://minhaskamal.github.io/DownGit/#/home?url=https://github.com/heisarzola/Unity-Development-Tools/tree/master/Tools/Sticky%20Notes) instead of the whole project if you want to.

## As A Reminder 
* If you make any changes/improvements to this tool, please do consider sharing them to update the repository. The easiest way to do so is via an e-mail to: contact@heisarzola.com
* There is a [*newsletter for game development tips, tricks and tutorials*](https://heisarzola.us16.list-manage.com/subscribe?u=711c0d50be32d6a5eca3ccb18&id=43d6d70f28). Where you will also receive notifications **when new tools are added to this repository**, for those that are interested.
* This project's license is located in the repository root under the name "LICENSE.md".

## Sticky Notes Acknowledgments

* [charblar](https://github.com/charblar/stickies) - For providing the almost untouched code and idea for the tool. It was improved from the original by forcing the GameObjects with the component to have an "EditorOnly" tag, by improving the overall visual presentation of the search tool and also provide it with a one button way of adding new notes in the scene.