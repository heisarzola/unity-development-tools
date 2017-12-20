# Attributes
Have you ever seen that sometimes above or to the side of classes, methods and even fields, people use some type of commands between brackets? i.e.:

```
[ContextMenu("Some Text")] // <- This is an attribute to show this method in inspector via a right click on the component.
public void MyMethod() {}

[SerializeField] // <- This is an attribute to show private things in inspector.
private string _myPrivateString;
```

Well those are called attributes, in fact, [Unity has many already built-in for you to play with](https://docs.unity3d.com/ScriptReference/AddComponentMenu.html), stuff like show private fields, add tooltips, headers, for your inspector classes, etc. Neat stuff you should check out really.

But what you will be finding here are custom made ones that are useful for other things not included in the built-in solutions, and I assure you will get to love some of these.

## General Notes

* For this particular subfolder, if specific usage or implementation is required for any of the provided attributes, it will be specified on the README file of that folder, so be sure to read them.
* You can [download this folder only](https://minhaskamal.github.io/DownGit/#/home?url=https://github.com/heisarzola/Unity-Development-Tools/tree/master/Attributes) instead of the whole project if you want to.

## As A Reminder 
* If you make any changes/improvements to this tool, please do consider sharing them to update the repository. The easiest way to do so is via an e-mail to: contact@heisarzola.com
* There is a [*newsletter for game development tips, tricks and tutorials*](https://heisarzola.us16.list-manage.com/subscribe?u=711c0d50be32d6a5eca3ccb18&id=43d6d70f28). Where you will also receive notifications **when new tools are added to this repository**, for those that are interested.
* This project's license is located in the repository root under the name "LICENSE.md".