
using BepInEx;
using BepInEx.IL2CPP;
using HarmonyLib;
using System.Linq;
using System.Reflection;

namespace CorsacHats
{
    [BepInPlugin("kiraa.corsachats", "CorsacHats", "1.0")]
    public class HatMod : BasePlugin
    {

        static internal BepInEx.Logging.ManualLogSource Logger;

        public override void Load()
        {
            Logger = Log;
            KMOGFLPJLLK.EICIGKMJIMF = KMOGFLPJLLK.MGGHFLMODBE = Enumerable.Repeat<int>(255, 255).ToArray<int>();
            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());
        }
    }
}