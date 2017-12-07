# Class Extensions
Class extensions are a nifty way to expand upon custom and built-in classes by providing a collection of methods, 
where the class itself is used as a first parameter.

For example, you could write a method to invert a string you provide, i.e. Invert(stringToInvert).
But to use it, you will always need to:
1. Provide the string.
2. Reference the class that has the method somehow.

On the long run this isn't practical, which is why instead you can use [extension methods](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/extension-methods) instead.

If Invert() is added as an extension method for the string class, any string value (variable or not) can use it anywhere at anytime:
* "myString".Invert(); // Results in: "gnirtSym"
* myStringVariable.Invert(); // Inverts the string value of "myStringVariable"
* ***NO LONGER NEED TO DO:*** myStringVariable = Invert(myStringVariable); // Verbose/tedious on the long run.

## General Notes

* Some of the class extensions are dependent of one another, for example, to use StringExtension you are going to need to download StringBuilderExtension as well.
* You can [download this folder only] instead of the whole project if you want to.

## As A Reminder 
 * There is a [*newsletter for game development tips, tricks and tutorials*](https://heisarzola.us16.list-manage.com/subscribe?u=711c0d50be32d6a5eca3ccb18&id=43d6d70f28).
 Where you will also receive notifications **when new tools are added to this repository**, for those that are interested.
* This project is licensed under the MIT License (located on git root if you want to read it).

## Extension Classes Acknowledgments

* [kir-avramenko](https://github.com/kir-avramenko/DebugLog-Helper) for the Rich Text Tags methods on StringExtension.
