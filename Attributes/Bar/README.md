# Bar Attribute
The ***Bar Parameter*** allows to draw a bar filled in a specified color, based on the provided current and max values.

#### End Result:

![](https://github.com/heisarzola/Unity-Development-Tools/blob/master/Attributes/Bar/Bar.gif)

If any doubt on how to use the attribute arises, please see the provided examples for reference.

## General Notes
* This attribute can only be used from a field with a ***float or integer*** type.	
* Currently, 0 is always used as the reference point for the minumum value of the bar. No exception will be thrown when working with negative numbers, but the bar itself won't be filled up properly.
* You can [download this folder only](https://minhaskamal.github.io/DownGit/#/home?url=https://github.com/heisarzola/Unity-Development-Tools/tree/master/Attributes/Animator%20Parameter) instead of the whole project if you want to.

## As A Reminder 
* If you make any changes/improvements to this tool, please do consider sharing them to update the repository. The easiest way to do so is via an e-mail to: contact@heisarzola.com
* There is a [*newsletter for game development tips, tricks and tutorials*](https://heisarzola.us16.list-manage.com/subscribe?u=711c0d50be32d6a5eca3ccb18&id=43d6d70f28). Where you will also receive notifications **when new tools are added to this repository**, for those that are interested.
* This project's license is located in the repository root under the name "LICENSE.md".

## Bar Acknowledgments

* [Lotte Makes Stuff](https://gist.github.com/LotteMakesStuff/2d3c6dc7a913ed118601db95735574de) - For providing the base class and general idea. It was improved by caching the expensive reflection references it uses, ensuring the bar colors always remained visible, by adding several new coloring options, allowing any float value (original had to be less than one), and by providing the ability of using a constant, field, property or method to get the max value to track (used to be only field).