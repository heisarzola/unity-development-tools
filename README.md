# Unity Development Tools
This repository is a collection of several tools and code snippets to improve development speed, and promote best practices when using the [Unity game engine](https://unity3d.com/).

## Getting Started

The easiest way to make use of the provided tools, is to download the zip of the project and copy-paste the parts you want into your Unity solution.

(If you don't know how to open .zip files, you can simply install [7-Zip](http://www.7-zip.org/) to open it, and extract the folders where you want them.)

Likewise, every subfolder in this repository has its own README.md file with a direct link to download that specific folder, alongside specific subfolder descriptions, notes and tips.

Finally, every script was documented for easy usage when making use of [Visual Studio](https://www.visualstudio.com/). When referencing the different classes and elements, descriptions of what each method does and parameter you should provide will appear, so please abstain from removing the "summary" notes on top of field, methods, classes, enums and properties (esentially anywhere you find them). They are there for your own good.

## Additional Notes
* The Unity version used while packing everything was **Unity 2017.2.0f3**. This is important as some tools *REQUIRE* features from this version, such as the tools that automatically start before/during/after runtime to give an example.
* .meta files are included in the repository so that if you end up updating the tools in your local Unity copy, Unity knows what files and folders replace what. So when importing a folder/script/resource, make sure to paste it with its .meta file. ***Don't let Unity make a new one***, else every new version or update it will treat them as a new script, which can be troublesome to manually adjust.
* Some of the class extensions are dependent of one another, for example, to use StringExtension you are going to need to download StringBuilderExtension as well. (Every script has a "dependencies" segment written on the top notes, after the short script description, where you can see what is needed.) However it is attempted that for the most part each folder works standalone, so you may use what you need.
* Not all scripts are fully documented, as some are tools intended to be used rather than inspected on a source code level. If you would like for a specific tool's code to be documented, feel free to request so.
* The author does not claim to have made ALL of the provided tools and snippets, some are just improvements and adaptations over other creators' work. (Check "Acknowledgments" on each subfolder.)
* Currently the intention is to keep these tools in C# only, no plans of porting to other languages.
* If you wish to provide tools to include them in this repo (and comply with the [MIT license](LICENSE.md) it has), do please *send an e-mail* to ***contact@heisarzola.com***.
* If you want to report a problem or bug, please do so in the ***issues section*** of the GitHub repository.
* Finally, considering you are using GitHub, you might be interested in a [*newsletter for game development tips, tricks and tutorials*](https://heisarzola.us16.list-manage.com/subscribe?u=711c0d50be32d6a5eca3ccb18&id=43d6d70f28). Where you will also receive notifications **when new tools are added to this repository**, so make sure to check it out.

## About The Author

Arzola is an indie game developer that works under the motto: 
**"Messing With Your Brain, Via Thought-Provoking Games."** You can read more in [his website](http://heisarzola.com) if you so desire.

Likewise, on his social media (starting December 2017) it is attempted to share tools like those included in the collection. In case anyone wants to follow:

[Twitter](https://twitter.com/heisarzola/)
 | [Facebook](https://www.facebook.com/heisarzola/)
 | [Instagram (Developer Tips)](https://www.instagram.com/heisarzola/)
 | [Developer Blog](http://heisarzola.com/devblog/)

## License

This project's license is available in the provided "[LICENSE.md](LICENSE.md)" file.

## Acknowledgments

Not all of the tools were made from scratch, but rather they might've been inspired after, or improved from pre-existing code.
So here are some people (in alphabetical order) you can personally thank for their own contributions if you want to:

* [amlovey](http://www.amlovey.com/) - For providing the idea of quickly opening special folders for the Quick Opener tool.
* [anchan828](https://github.com/anchan828/) - For providing (the almost untouched) AnimatorParameter and EnumLabel attributes.
* [charblar](https://github.com/charblar/) - For providing (the also almost untouched) Sticky Notes Tool.
* [diegogiacomelli](http://diegogiacomelli.com.br/) - For providing the base for the Scene Preview tool.
* [DuzzOnDrums](http://tools.powerhoof.com) - For providing the base for the Selection History tool.
* [Hyper Games Studio](https://github.com/HyperGamesStudio/) - For providing the base code and idea for the RangedInt/Float classes.
* [kir-avramenko](https://github.com/kir-avramenko/) - For providing the Rich Text Tags idea for the string class extension.
* [liortal53](https://github.com/liortal53/) - For providing the base for the Scriptable Object Factory, as well as the base for the Auto Saver tool.
* [Lotte Makes Stuff](https://gist.github.com/LotteMakesStuff/) - For providing the idea of having options to edit the limits on the RangedInt/Float classes, the base for the Highlight attribute, and the base for the Bar attribute.
* [plyoung](https://github.com/plyoung/) - For providing the untouched Favorite Assets tool.
* [reptoidgames](http://www.reptoidgames.com/goodies/) - For providing the group utility.