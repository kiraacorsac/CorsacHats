using HarmonyLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using UnhollowerBaseLib;
using UnityEngine;
using BepInEx;
using BepInEx.IL2CPP;
using HatManager = OFCPCFDHIEF;
using Palette = LOCPGOACAJF;


namespace CorsacHats
{
    public class MyHats
    {
        static bool modded = false;
        [HarmonyPatch(typeof(HatManager), nameof(HatManager.GetUnlockedHats))]
        public static class HatManagerHatsPatch
        {
            internal delegate bool d_LoadImage(IntPtr tex, IntPtr data, bool markNonReadable);
            internal static d_LoadImage iCall_LoadImage;

            public static bool LoadImage(Texture2D tex, byte[] data, bool markNonReadable)
            {
                if (iCall_LoadImage == null)
                    iCall_LoadImage = IL2CPP.ResolveICall<d_LoadImage>("UnityEngine.ImageConversion::LoadImage");

                var il2cppArray = (Il2CppStructArray<byte>)data;

                return iCall_LoadImage.Invoke(tex.Pointer, il2cppArray.Pointer, markNonReadable);
            }

            private static HatBehaviour CreateHat(Stream texture, string id)
            {
                HatMod.Logger.LogMessage($"Creating Hat: {id}");
                HatBehaviour newHat = new HatBehaviour();
                Texture2D tex = new Texture2D(2, 2, TextureFormat.ARGB32, false);
                float pixelsPerUnit = 225f;

                byte[] hatTexture = new byte[texture.Length];
                texture.Read(hatTexture, 0, (int)texture.Length);
                LoadImage(tex, hatTexture, false);
                newHat.MainImage = Sprite.Create(
                    tex,
                    new Rect(0, 0, tex.width, tex.height),
                    new Vector2(0.5f, 0.8f),
                    pixelsPerUnit
                );
                newHat.ProductId = $"+{id}";
                newHat.InFront = true;
                newHat.NoBounce = true;

                return newHat;
            }

            private static HatBehaviour CreateResourceHat(Assembly assembly, string resourceName)
            {
                HatMod.Logger.LogMessage($"Reaching Resource Hat: {resourceName}");
                var texture = assembly.GetManifestResourceStream(resourceName);

                var nameParts = resourceName.Split('.');
                return CreateHat(texture, nameParts[nameParts.Length - 2]);
            }

            private static HatBehaviour CreateFilesystemHat(string filePath)
            {
                HatMod.Logger.LogMessage($"Reaching Filesystem Hat: {filePath}");
                try
                {
                    using (var hatFileStream = new StreamReader(filePath))
                    {
                        return CreateHat(hatFileStream.BaseStream, Path.GetFileNameWithoutExtension(filePath));
                    }
                }
                catch (IOException e)
                {
                    HatMod.Logger.LogError($"The file {filePath} could not be read:");
                    HatMod.Logger.LogError(e.Message);
                    throw e;
                }

            }

            private static IEnumerable<HatBehaviour> CreateResourceHats()
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                var resourceNames = assembly.GetManifestResourceNames().Where(n => n.StartsWith("CorsacHats.HatSprites"));
                if (resourceNames.Count() == 0)
                {
                    HatMod.Logger.LogWarning("No Resource Hats found, dumping the full list of resource names");
                    foreach (var name in assembly.GetManifestResourceNames())
                    {
                        HatMod.Logger.LogInfo(name);
                    }
                }
                return resourceNames.Select(name => CreateResourceHat(assembly, name));


            }

            private static IEnumerable<HatBehaviour> CreateFilesystemHats()
            {
                var hatPath = Path.Combine(Directory.GetCurrentDirectory(), "CorsacHats");

                HatMod.Logger.LogInfo($"Looking for *.hat.png hats in path: {hatPath}");
                Directory.CreateDirectory(hatPath);
                var hatFileNames = Directory.GetFiles(hatPath, "*.hat.png");
                if(hatFileNames.Count() == 0)
                {
                    HatMod.Logger.LogWarning("No Filesystem Hats found, dumping the full list of file names in searched path");
                    foreach (var name in Directory.GetFiles(hatPath))
                    {
                        HatMod.Logger.LogInfo(name);
                    }
                }
                return hatFileNames.Select(CreateFilesystemHat);

            }


            public static bool Prefix(HatManager __instance)
            {
                try
                {
                    if (!modded)
                    {

                        HatMod.Logger.LogMessage("Adding resource hats begin");
                        modded = true;
                        var hatsFromResoruces = CreateResourceHats();
                        foreach (var hat in hatsFromResoruces)
                        {
                            __instance.AllHats.Add(hat);
                        }

                        HatMod.Logger.LogMessage("Adding filesystem hats");
                        var hatsFromFilesystem = CreateFilesystemHats();
                        foreach (var hat in hatsFromFilesystem)
                        {
                            __instance.AllHats.Add(hat);
                        }

                        // 
                        __instance.AllHats.Sort((Il2CppSystem.Comparison<HatBehaviour>)((h1, h2) =>  h2.ProductId.CompareTo(h1.ProductId)));

                    }
                    else
                    {
                        HatMod.Logger.LogMessage("Already loaded, skipped");
                    }
                    return true;
                }
                catch (Exception e)
                {
                    HatMod.Logger.LogError("During Prefix, an exception occured");
                    HatMod.Logger.LogError("------------------------------------------------");
                    HatMod.Logger.LogError(e);
                    HatMod.Logger.LogError("------------------------------------------------");
                    throw e;
                }
            }
        }
    }
}
