# Ranged Float Attribute
The ***RangedFloat*** attribute, drawers and class collection is a set of tools intended to easily draw in inspector a range between two ***float*** values.

#### End Result:

![](https://github.com/heisarzola/Unity-Development-Tools/blob/master/Attributes/Ranged%20Float/Ranged%20Float.gif)

If any doubt on how to use the attribute arises, please see the provided examples for reference.

## General Notes
* The ***RangedFloatAttribute*** (what is between brackets) will only work on the provided ***RangedFloat*** class.
* The ***RangedFloat*** comes with a built in random within range method. If you use a ***RangedFloat*** instance as a float, it will automatically fetch a random value between min and max.
	* i.e. myFloat = myRangedFloat; // myFloat now has a value between myRangedFloat's min and max.
	* Likewise, you can call it explicitly if you want to: i.e. myFloat = myRangedFloat.GetRandomValue();// Same result as above.	
* You can [download this folder only](https://minhaskamal.github.io/DownGit/#/home?url=https://github.com/heisarzola/Unity-Development-Tools/tree/master/Attributes/Ranged%20Float) instead of the whole project if you want to.

## As A Reminder 
* If you make any changes/improvements to this tool, please do consider sharing them to update the repository. The easiest way to do so is via an e-mail to: contact@heisarzola.com
* There is a [*newsletter for game development tips, tricks and tutorials*](https://heisarzola.us16.list-manage.com/subscribe?u=711c0d50be32d6a5eca3ccb18&id=43d6d70f28). Where you will also receive notifications **when new tools are added to this repository**, for those that are interested.
* This project's license is located in the repository root under the name "LICENSE.md".

## Ranged Float Acknowledgments

* [Hyper Games Studio](https://github.com/HyperGamesStudio/unity-minmax-slider/blob/master/Editor/MinMaxSliderDrawer.cs) - For providing the base code and idea for the slider. It was improved by creating a custom class (***RangedFloat***) to avoid making use of the Vector2 and its unintuitive conventions (instead of ***myRangedFloat.min***, you would need to use ***myRangedFloat.x*** because it used to be a Vector2).
* [Lotte Makes Stuff](https://gist.github.com/LotteMakesStuff/0de9be35044bab97cbe79b9ced695585) - For providing the idea of including values in the attribute to accommodate the scenarios of having editable/locked/hidden bottom and upper limits. It was improved by providing the "hidden" scenario, as well as by substituting several boolean flags into one enum.