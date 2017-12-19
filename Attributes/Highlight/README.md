# Highlight Attribute
The ***Highlight Parameter*** allows a certain area in the inspector to be highlighted when a given condition is fulfilled. It is useful when one value should stand out on a given event or value threshold, in editor or runtime.

#### End Result:

![](https://github.com/heisarzola/Unity-Development-Tools/blob/master/Attributes/Highlight/Highlight.gif)

If any doubt on how to use the attribute arises, please see the provided examples for reference.

## General Notes
* Currently the highlights are only called via a method. Property / Field support will be added if requested.
* You can [download this folder only](https://minhaskamal.github.io/DownGit/#/home?url=https://github.com/heisarzola/Unity-Development-Tools/tree/master/Attributes/Highlight) instead of the whole project if you want to.

## As A Reminder 
* If you make any changes/improvements to this tool, please do consider sharing them to update the repository. The easiest way to do so is via an e-mail to: contact@heisarzola.com
* There is a [*newsletter for game development tips, tricks and tutorials*](https://heisarzola.us16.list-manage.com/subscribe?u=711c0d50be32d6a5eca3ccb18&id=43d6d70f28). Where you will also receive notifications **when new tools are added to this repository**, for those that are interested.
* This project's license is located in the repository root under the name "LICENSE.md".

## Highlight Acknowledgments

* [Lotte Makes Stuff](https://gist.github.com/LotteMakesStuff/2d3c6dc7a913ed118601db95735574de) - For providing the base class and general idea. It was improved by caching the expensive reflection references it uses, ensuring the highlight colors always remained visible, by adding several new coloring options, adding a multiple parameter support for the method, and by allowing white tags to be used on dark highlights.