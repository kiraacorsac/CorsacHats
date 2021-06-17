using HarmonyLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnhollowerBaseLib;
using UnityEngine;

namespace CorsacHats {
    public class MyHats {
        [HarmonyPatch(typeof(HatsTab), nameof(HatsTab.OnEnable))]
        public static class HatTabPatch {
            public static void Postfix(HatsTab __instance) {
                for(int i=0;i< __instance.ColorChips.Count;i++) {
                    var hat = __instance.ColorChips[i];
                    hat.transform.localScale *= 0.7f;
                }
            }
        }
        static bool modded = false;
        [HarmonyPatch(typeof(HatManager), nameof(HatManager.GetHatById))]
        public static class HatManagerHatsPatch {
            internal delegate bool d_LoadImage(IntPtr tex, IntPtr data, bool markNonReadable);
            internal static d_LoadImage iCall_LoadImage;
            public static bool LoadImage(Texture2D tex, byte[] data, bool markNonReadable) {
                if (iCall_LoadImage == null)
                    iCall_LoadImage = IL2CPP.ResolveICall<d_LoadImage>("UnityEngine.ImageConversion::LoadImage");

                var il2cppArray = (Il2CppStructArray<byte>)data;

                return iCall_LoadImage.Invoke(tex.Pointer, il2cppArray.Pointer, markNonReadable);
            }

            private static HatBehaviour CreateHat(Stream texture, string id, int order) {
                HatMod.Logger.LogMessage($"Creating Hat: {id}");
                HatBehaviour newHat = new HatBehaviour();
                Texture2D tex = new Texture2D(2, 2, TextureFormat.ARGB32, false);

                byte[] hatTexture = new byte[texture.Length];
                texture.Read(hatTexture, 0, (int)texture.Length);
                LoadImage(tex, hatTexture, false);
                newHat.MainImage = Sprite.Create(
                    tex,
                    new Rect(0, 0, tex.width, tex.height),
                    new Vector2(0.53f, 0.575f),
                    tex.width * 0.375f
                );
                newHat.Order = order + 99;
                newHat.ProductId = id;
                newHat.InFront = true;
                newHat.NoBounce = true;

                return newHat;
            }

            private static HatBehaviour CreateFilesystemHat(string filePath) {
                HatMod.Logger.LogMessage($"Reaching Filesystem Hat: {filePath}");
                int id = 0;
                try {
                    using (var hatFileStream = new StreamReader(filePath)) {
                        return CreateHat(hatFileStream.BaseStream, Path.GetFileNameWithoutExtension(filePath), id++);
                    }
                }
                catch (IOException e) {
                    HatMod.Logger.LogError($"The file {filePath} could not be read:");
                    HatMod.Logger.LogError(e.Message);
                    throw e;
                }

            }

            private static IEnumerable<HatBehaviour> CreateFilesystemHats() {
                var hatPath = Path.Combine(Directory.GetCurrentDirectory(), "CorsacHats");
                HatMod.Logger.LogInfo($"Looking for *.png hats in path: {hatPath}");
                Directory.CreateDirectory(hatPath);
                var hatFileNames = Directory.GetFiles(hatPath, "*.png"); // use .png instead of .hat.png, for easier importing
                if(hatFileNames.Count() == 0) {
                    HatMod.Logger.LogWarning("No Filesystem Hats found, dumping the full list of file names in searched path");
                    foreach (var name in Directory.GetFiles(hatPath)) {
                        HatMod.Logger.LogInfo(name);
                    }
                }
                return hatFileNames.Select(CreateFilesystemHat);
            }

            public static bool Prefix(HatManager __instance) {
                try {
                    ModManager.Instance.ShowModStamp();
                    if (!modded) {
                        HatMod.Logger.LogMessage("Adding filesystem hats");
                        modded = true;
                        var hatsFromFilesystem = CreateFilesystemHats();
                        foreach (var hat in hatsFromFilesystem) {
                            __instance.AllHats.Add(hat);
                        }

                        // 
                        __instance.AllHats.Sort((Il2CppSystem.Comparison<HatBehaviour>)((h1, h2) =>  h2.ProductId.CompareTo(h1.ProductId)));

                    }
                    return true;
                }
                catch (Exception e) {
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
