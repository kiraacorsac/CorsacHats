# CorsacHats


## Description

Client-side BepInEx mod that adds the ability to wear custom hats represented as simple .png files. 

Any custom hat will be seen by *you* and *anyone who has the same mod and exact same hats installed*. 
People who don't have the mod will see you with a pseudo-random hat. You might also see other people wearing hats you've added into the game - on their side, it looks like they are wearing a their own hat.

![screenshot](https://i.imgur.com/ap0txjo.png)
![screenshot](https://i.imgur.com/OXxk1a7.png)

Short video: https://i.imgur.com/um3i9Yx.mp4

## Installation

For PC:

1) Install latest BepInEx x86 for UnityIL2CPP_x86 (Yes, it needs to be x86!)
	1) Download from [here](https://builds.bepis.io/projects/bepinex_be) (lastest tested Artifact: #320, version UnityIL2CPP_x86)
	2) Extract into AmongUs folder (something like `C:\Program Files (x86)\Steam\steamapps\common\Among Us`) 
2) Download CorsacHats.dll from latest release [here](https://github.com/kiraacorsac/CorsacHats/releases/)
3) Place CorsacHats.dll in `>AmongUs folder<\BepInEx\plugins` (create the folder if it does not exist)
4) Add your custom hats in `.hat.png` format to `>AmongUs folder<\CorsacHats` (create the folder if it does not exist)

Currently not supported on other platforms.

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
- [Oggy](https://twitter.com/OggyOsbourne) for the embedded Kiraa hat.
