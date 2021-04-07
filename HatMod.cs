
using BepInEx;
using BepInEx.IL2CPP;
using HarmonyLib;
using System;
using System.Linq;
using System.Reflection;
using static CorsacHats.MyHats;

namespace CorsacHats
{
    [BepInPlugin("kiraa.corsachats", "CorsacHats", "1.1")]
    public class HatMod : BasePlugin
    {

        static internal BepInEx.Logging.ManualLogSource Logger;
        static Harmony _harmony;


        public override void Load()
        {
            try
            {
                Logger = Log;

                IGDMNKLDEPI.OHDJGJFDDDP = IGDMNKLDEPI.AHFOOEHOFNC = Enumerable.Repeat<int>(255, 255).ToArray<int>();
                Logger.LogMessage("Patching harmony...");

                Attribute[] attrs = Attribute.GetCustomAttributes(typeof(HatManagerHatsPatch)); 

                foreach (Attribute attr in attrs)
                {
                    Logger.LogMessage($"Looking {attr.GetType()}");
                }
                if(attrs.Length == 0)
                {
                    Logger.LogError("HarmonyPatchers not found.");
                }
                _harmony = Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());
                Logger.LogMessage($"Is patched? {Harmony.HasAnyPatches(_harmony.Id)}");
            }
            catch (Exception e)
            {
                Log.LogError(e);
                throw e;
            }
        }
    }
}