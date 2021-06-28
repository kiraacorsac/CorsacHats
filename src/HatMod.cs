using BepInEx;
using BepInEx.IL2CPP;
using HarmonyLib;
using System;
using System.Linq;
using System.Reflection;

namespace CorsacHats
{
    [BepInPlugin("kiraa.corsachats", "CorsacHats", "1.0.5")]
    public class HatMod : BasePlugin {
        static internal BepInEx.Logging.ManualLogSource Logger;
        static Harmony _harmony;
        public override void Load() {
            try {
                Logger = Log;
                GameOptionsData.RecommendedImpostors = GameOptionsData.MaxImpostors = Enumerable.Repeat<int>(255, 255).ToArray<int>();
                Logger.LogMessage("Patching harmony...");
                Logger.LogInfo("Succesfully loaded CorsacHats");

                Attribute[] attrs = Attribute.GetCustomAttributes(typeof(MyHats.HatManagerHatsPatch)); 

                foreach (Attribute attr in attrs) {
                    Logger.LogMessage($"Looking {attr.GetType()}");
                }
                if(attrs.Length == 0) {
                    Logger.LogError("HarmonyPatchers not found.");
                }
                _harmony = Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());
                Logger.LogMessage($"Is patched? {Harmony.HasAnyPatches(_harmony.Id)}");
                
            }
            catch (Exception e) {
                Log.LogError(e);
                throw e;
            }
        }
    }


}