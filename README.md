# Carpet Fishing
Expandable Carpet fishing game according to Dilbert's rules

## Background
If you are a reader of the [Dilbert](http://dilbert.com/) comic by Scott Adams, you might remember this game:
[![alt text](http://assets.amuniversal.com/6c6cc1306cc801301d50001dd8b71c47 "Dilbert by Scott Adams")](http://dilbert.com/strip/2007-09-30)

This program is an implementation of this game with extension capabilities. It not only includes the standard carpet fishing but also a data collection cover.
If you don't want your PHB (Pointy Haired Boss) to know that you are playing a game, you can use this cover. It prints out different messages but the gaming principle is the same.

## Usage

### Standard Arguments
There are a few arguments you can set:
Argument |Description
---------|-----------
 -h      |  Prints the help message. Combined with a game mode, it might look different.
 -n      |  Enables notifications. By default it uses `Console.Beep()` but it can be overridden by each `GameData` implementations.
 -t[m]   |  Sets the time to wait between results in minutes. Example: "-t5". This value can also be set in-game.
 -g[x,y] |   Sets the grid size. Example: "-g5,5". This value can also be set in-game.

### Game Mode Arguments
By default there are only two game modes. You can add more by implementing your own `GameData` class. See "Expanding" for more information.
Argument|Description
--------|-----------
 -f     | Standard Carpet Fishing game mode. This is also the default if no parameter is given.
 -d     | Data Collection Tool game mode. It looks like a tool that is collection some ominous data. Use it to hide that you are playing a game.

## Expanding

You can also easily add your own texts to create a different version.
Simply create your own class that inherits from the `GameData` class and add it to the `AvailableGameTypes` array at the start of the `Main` method.

This is how the class properties related to the printed text:

Started without any arguments:
```
########################
## Welcome to {Title} ##
########################

{GetGrid} (x,y):
{awaiting user input here}

{GetTime} (in minutes):
{awaiting user input here}

{Start}
{Time}: 5m  -  {Grid}: X:5 Y:5

{Progress}

{GetResultString(x, y)}
```
The `GetGrid` and `GetTime` will be overwritten by `Start` once they are typed in. And `Progress` will be overwritten by the `GetResultString(x, y)`.
After a result is printed out, it starts again with `Progress`.

If you set the "-h" argument alone it will print out the standard help with a list of available game modes, where each game mode is presented like this:
```
-{Tag}  {Description}
```

If you set the "-h" argument together with a game mode argument, the help can change a bit.
* The title will be `Help for {Title}` instead of `Help for Carpet Fishing`.
* The available game types will not be listed.
* If not `null`, the `Help` property will be shown.
* If `OmitHowTo` is set to true, the How To messages will not be shown.

Additionally you can override these two methods:
Method        |Description
--------------|-----------
 Init         | Will be called before the game begins. It gets all provides arguments passed so you can define your own game configurations.
 Notification | Will be called after each result if the "-n" argument is given. By default it calls `Console.Beep()`.

## What's the Point
Well, to be honest there is none really. This was written just for fun and nothing else.
However, you could use it as a reminder for taking a break once in a while with the added fun of potentially catching a fish.

## Your Input
If you have found a bug or a problem, please report them here.
If you have written an extension, consider a pull request and it might get included here.
Just make sure it is in the GameType folder and it's not inappropriate.