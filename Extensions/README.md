# Class Extensions
Class extensions are a nifty way to expand upon custom and built-in classes by providing a collection of methods, 
where the class itself is used as a first parameter.

For example, you could write a method to invert a string you provide, i.e. ```Invert(stringToInvert)```.
But to use it, you will always need to:
1. Provide the string.
2. Reference the class that has the method somehow.

On the long run this isn't practical, which is why instead you can use [extension methods](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/extension-methods) instead.

If Invert() is added as an extension method for the string class, any string value (variable or not) can use it anywhere at anytime:
* ```"myString".Invert(); // Results in: "gnirtSym"```
* ```myStringVariable.Invert(); // Returns the inverted string value stored in myStringVariable```
* You can also **concatenate the extension method calls if you wish**. Although something like ```"someString".Invert().Invert()``` doesn't make much sense in this example, depending on the extension methods you use and create it might be a powerful combination:
	* Assuming you create all of the following extension methods, something like ```"myString".Invert().Capitalize().Append("otherString").Invert();``` would replace the verbose and confusing ```Invert(Append(Capitalize(Invert("myString")),"otherString"))```
	
But see? No need to reference a method within a class, a lot more practical and cleaner.

## General Notes

* Some of the class extensions are dependent of one another, for example, to use ***StringExtension*** you are going to need to download ***StringBuilderExtension*** as well. (Every script has a "dependencies" segment written on the top notes, after the short script description, where you can see what is needed.)
* You can [download this folder only](https://minhaskamal.github.io/DownGit/#/home?url=https://github.com/heisarzola/Unity-Development-Tools/tree/master/Extensions) instead of the whole project if you want to.

## As A Reminder 
* If you make any changes/improvements to this tool, please do consider sharing them to update the repository. The easiest way to do so is via an e-mail to: contact@heisarzola.com
* There is a [*newsletter for game development tips, tricks and tutorials*](https://heisarzola.us16.list-manage.com/subscribe?u=711c0d50be32d6a5eca3ccb18&id=43d6d70f28). Where you will also receive notifications **when new tools are added to this repository**, for those that are interested.
* This project's license is located in the repository root under the name "LICENSE.md".

## Extension Classes Acknowledgments

* [kir-avramenko](https://github.com/kir-avramenko/DebugLog-Helper) for the Rich Text Tags methods on StringExtension. The original intention of these was to beautify the console outputs, but they can also be used for components that support rich text, so they were renamed and re-purposed.
