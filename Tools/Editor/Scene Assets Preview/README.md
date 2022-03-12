# Scene Assets Preview
Editor class intended to show an image preview of a given scene asset when seen in the inspector. (When you click on an asset on vanilla Unity, it shows nothing but the name.)

You can also add your own custom thumbnails if you wish.

#### Provided Tool:
![](https://github.com/heisarzola/Unity-Development-Tools/blob/master/Tools/Editor/Scene%20Assets%20Preview/Scene%20Assets%20Preview.gif)

## General Notes

* If a scene doesn't have a thumbnail, playing it on runtime will take a screenshot of the first frame, and make it the thumbnail.
* If you wish to include your own thumbnails, simply replace or add one .png file with the scene name in the "Gizmos/Scene Preview Thumbnails/Editor/Resources" directory. (If requested, a preferences editor can be added to make this custom.)
* Thumbnails are stored within an "Editor" folder to avoid being included in builds.
* Thumbnails are also stored within a "Resources" folder to be able to display them.
* Please note that this tool ***REQUIRES*** to be within an "Editor" named folder to work. (For more information about the reason, look at [Unity's official documentation](https://docs.unity3d.com/560/Documentation/Manual/SpecialFolders.html).)
* You can [download this folder only](https://minhaskamal.github.io/DownGit/#/home?url=https://github.com/heisarzola/Unity-Development-Tools/tree/master/Tools/Editor/Scene%20Assets%20Preview) instead of the whole project if you want to.

## As A Reminder 
* If you make any changes/improvements to this tool, please do consider sharing them to update the repository via a pull request.
* This project's license is located in the repository root under the name "LICENSE.md".

## Scene Assets Preview Acknowledgments

* [diegogiacomelli](https://diegogiacomelli.com.br/unity3d-scenepreview-inspector/) - For providing the base class, it was further improved by allowing the tool to work without the need of manually creating folders, now creates thumnails in a folder that will be ignored in builds, beautified instructions when selecting a thumbnail-less scene, and by avoid making a new thumbnail if one already exists, so you can make your own ones.