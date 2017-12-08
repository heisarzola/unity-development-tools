# Ranged Float
The ***RangedFloat*** attribute, drawers and class collection is a set of tools intended to easily draw in inspector a range between two ***float*** values.

#### End Result:

![](https://github.com/heisarzola/Unity-Development-Tools/blob/master/Attributes/Ranged%20Float/Ranged%20Float.gif)

If (see the provided examples for reference).

## General Notes
* The ***RangedFloatAttribute*** (what is between brackets) will only work on the provided ***RangedFloat*** class.
* The ***RangedFloat*** comes with a built in random within range method. If you use a ***RangedFloat*** instance as a float, it will automatically fetch a random value between min and max.
	* i.e. myFloat = myRangedFloat; // myFloat now has a value between myRangedFloat's min and max.
	* Likewise, you can call it explicitly if you want to: i.e. myFloat = myRangedFloat.GetRandomValue();// Same result as above.	
* You can [download this folder only](https://minhaskamal.github.io/DownGit/#/home?url=https://github.com/heisarzola/Unity-Development-Tools/tree/master/Attributes/Ranged%20Float) instead of the whole project if you want to.

## As A Reminder 
 * There is a [*newsletter for game development tips, tricks and tutorials*](https://heisarzola.us16.list-manage.com/subscribe?u=711c0d50be32d6a5eca3ccb18&id=43d6d70f28).
 Where you will also receive notifications **when new tools are added to this repository**, for those that are interested.
* This project's license is located in the repository root under the name "LICENSE.md".