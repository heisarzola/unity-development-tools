# Ranged Int
The RangedInt attribute, drawers and class collection is a set of tools intended to easily draw in inspector a range between two integer values.

#### End Result:

![](https://github.com/heisarzola/Unity-Development-Tools/blob/master/Tools/Scriptable%20Object%20Factory/Scriptable%20Object%20Factory.gif)

If (see the provided examples for reference).

## General Notes
* The RangedIntAttribute (what is between brackets) will only work on the provided RangedInt class.
* The RangedInt comes with a built in random within range method. If you use a RangedInt instance as an int, it will automatically fetch a random value between min and max.
	* i.e. myInt = myRangedInt; // myInt now has a value between myRangedInt's min and max.
	* Likewise, you can call it explicitly if you want to: i.e. myInt = myRangedInt.GetRandomValue();// Same result as above.	
* You can [download this folder only](https://minhaskamal.github.io/DownGit/#/home?url=https://github.com/heisarzola/Unity-Development-Tools/tree/master/Attributes/Ranged%20Int) instead of the whole project if you want to.

## As A Reminder 
 * There is a [*newsletter for game development tips, tricks and tutorials*](https://heisarzola.us16.list-manage.com/subscribe?u=711c0d50be32d6a5eca3ccb18&id=43d6d70f28).
 Where you will also receive notifications **when new tools are added to this repository**, for those that are interested.
* This project's license is located in the repository root under the name "LICENSE.md".