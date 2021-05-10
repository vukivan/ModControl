## What is ModControl?

ModControl is a simple mod manager for Farming Simulator 19.

It's desinged to avoid mod duplication and having multiple folders with same sets of mods depending on your save game.
Instead, you can have all your mods, active and inactive, in the same directory.
Additionaly, you can create mod packages, which are essentially mod lists, that can be "bulk" activated at any moment.

## Isn't having all mods in one directory bad?

Normally, yes. It is bad to have all mods in one directory. However...
Mods deactivated by ModControl are ignored by the game entirely, and will not get loaded or even touched by the game.
To put it differently, only active mods are in mod directory, at least as far as game is concerned.

## How does it work?

When deactivating mods, ModControl will append .deactivated extension to zip files. This will prevent file from being loaded by the game.
To activate the mod, additional extension will be removed, and you will again have your normal zip file.

**NOTE**: mods in directories are IGNORED and not managed by ModControl.

## How do I start?

See [wiki](https://github.com/vukivan/ModControl/wiki)

## Anything else?

Yes, licences and third party SW. Mod control uses [pfim](https://github.com/nickbabcock/Pfim) to generate mod previews.
Pfim is included in the release files, though dll is not present in the repository.
Both pfim and ModControl use MIT license which is included in the repo.

## Disclaimer

This software is offered as is. The author takes no responsibility for harm done in using this software, or parts of this software. Author makes no guarantee of support or update of this software. Author does not take any responsibility for results of modified versions of this software, or modified parts of this software. This software can only be distributed from ModControl GitHub repository.

## Donate

You can send me a monetary equivalent of a beer/coffee. This entitles you to nothing, but I will be grateful.

[![Donate](https://img.shields.io/badge/Donate-PayPal-green.svg)](https://www.paypal.me/vukicamods)
