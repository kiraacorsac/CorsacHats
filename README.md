# CorsacHats


## Description

Client-side BepInEx mod that adds the ability to wear custom hats represented as simple .png files. 

Any custom hat will be seen by *you* and *anyone who has the same mod and exact same hats installed*. 
People who don't have the mod will see you with a pseudo-random hat. You might also see other people wearing hats you've added into the game - on their side, it looks like they are wearing a their own hat.

![screenshot](https://i.imgur.com/ap0txjo.png)
![screenshot](https://i.imgur.com/OXxk1a7.png)

Short video: https://i.imgur.com/um3i9Yx.mp4

## Installation

For PC (Among Us 2021.3.31.3s)
1)Download the Latest Release [1.0.3](https://github.com/MrFawkes1337/CorsacHats/Releases/)
2)Extract the .zip Archive to Your Among Us Base Folder (You can get to this by right clicking Among Us on Steam, Clicking Properties, then Browse local Files)
3)Upon running the game, you will see the BepInEx console loaded in the background, and any Custom Hats placed into the CorsacHats folder will be loaded upon entering a Lobby, and appear halfway down the hat selections.

## Creating custom hats
1) Download tempate for Gimp (.xfc) or Photoshop (.psd) [here](https://github.com/kiraacorsac/CorsacHats/releases/)
2) Draw your hat in the template
3) Extract in PNG file format. Preserve the resolution.
4) Rename to `>hatname<.hat.png`, where `>hatname<` is any unique name
5) Place into AmongUs installation directory, in folder CorsacHats.

## Known Issues
- People who don't have the mod and your hats installed will see you with a pseudo-random hat. 
- You will also see other, random people wearing hats you've added into the game - on their side, it looks like they are wearing a their own hat.

## Issue reporting
Create an issue with steps to replicate the problem. In case of the mod not working at all, make sure to include `LogOutput.log` from the `BepInEx` folder.

### Attributions
- [CrowdedMod](https://github.com/CrowdedMods/CrowdedMod) for getting me started with modding AmongUs.
- [Oggy](https://twitter.com/OggyOsbourne) for the exempary hats and the embedded Kiraa hat.
