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
Both pfim and ModControl use MIT license.
