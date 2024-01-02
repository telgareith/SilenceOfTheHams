using HarmonyLib;

namespace SilenceOfTheHams.HarmonyPatches;

[StaticConstructorOnStartup]
public static class Init
{
    static Init()
    {
        Harmony harmony = new Harmony("telgareith.SilenceOfTheHams");
        #if DEBUG
		    Harmony.DEBUG = true;
        #endif
        harmony.PatchAll();
    }
}
