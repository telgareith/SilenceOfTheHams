using HarmonyLib;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace SilenceOfTheHams.HarmonyPatches;

/// <summary>
/// Both <see cref="Pawn.DoKillSideEffects"/> and <see cref="DamageWorker_AddInjury.PlayWoundedVoiceSound"/> Play sounds when a Pawn is dealt <see cref="DamageDefOf.ExecutionCut"/> damage.
/// </summary>
[HarmonyPatch(typeof(Pawn))]
[HarmonyPatch("DoKillSideEffects")]
// ReSharper disable once UnusedType.Global
static class PawnDoKillSideEffects
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="instructions"></param>
    /// <returns></returns>
    // TODO: Refactor Pawn.cs to use Pattern shown at bottom of Pawn.cs
    public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> ci = instructions.ToList();
        bool patched = false;
        
        MethodInfo playNearestLifestageSound = AccessTools.Method(typeof(LifeStageUtility), nameof(LifeStageUtility.PlayNearestLifestageSound));

        for (int i = 0; i < ci.Count; i++)
        {
            if (!patched && ci[i].Calls(playNearestLifestageSound))
            {
                List<Label> soundLabelsToSkip = ci[i + 1].labels;
                for (int j = i; j > 0; j--)
                {
                    if (ci[j].opcode == OpCodes.Ldarga_S && ci[j].OperandIs(1) && ci[j + 1].opcode == OpCodes.Call) //TODO: How do I buffer ldarga_s from the method argument order changing? 
                    {
#if DEBUG
                        Log.Message("PDKSE- --------------------PATCHING PawnDoKillSideEffects------------------");
#endif
                        //TODO: Validate that "dinfo" isn't null
                        ci.Insert(j++, new CodeInstruction(OpCodes.Ldarga, 1));
                        ci.Insert(j++, new CodeInstruction(OpCodes.Call, AccessTools.PropertyGetter(typeof(DamageInfo), nameof(DamageInfo.Def))));
                        ci.Insert(j++, new CodeInstruction(OpCodes.Ldsfld, AccessTools.Field(typeof(DamageDefOf), nameof(DamageDefOf.ExecutionCut))));
                        ci.Insert(j++, new CodeInstruction(OpCodes.Beq, soundLabelsToSkip.FirstOrDefault()));

                        patched = true;

                        break;
                    }
                }
            }
        }
        
        return ci;
    }
}
// [HarmonyPatch(typeof(SiteMaker), nameof(SiteMaker.MakeSite), new Type[] { typeof(IEnumerable<SitePartDefWithParams>), typeof(int), typeof(Faction), typeof(bool) })]
// public static class Patch_SiteMaker
// {
//     /// <summary>
//     /// Instead of calling the constructor for SitePart, detour to utility method, which returns a SitePart or subtype instance
//     /// </summary>
//     [HarmonyTranspiler]
//     public static IEnumerable<CodeInstruction> MakeCustomSitePart(IEnumerable<CodeInstruction> instructions)
//     {
//         List<CodeInstruction> codeInstructions = instructions.ToList();
//
//         int targetIndex = codeInstructions.FindIndex(ci => ci.opcode == OpCodes.Newobj);
//         codeInstructions.RemoveAt(targetIndex);
//         codeInstructions.InsertRange(targetIndex, InjectedInstructions());
//
//         return codeInstructions.AsEnumerable();
//     }
//
//     private static IEnumerable<CodeInstruction> InjectedInstructions()
//     {
//         yield return new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(Patch_SiteMaker), nameof(SiteMakerHelper)));
//     }
//
//     private static SitePart SiteMakerHelper(Site site, SitePartDef def, SitePartParams sitePartParams)
//     {
//         if(def is Arch_SitePartDef archDef)
//         {
//             return (SitePart)Activator.CreateInstance(archDef.sitePartClass, new object[] { site, def, sitePartParams });
//         }
//         return new SitePart(site, def, sitePartParams);
//     }
// }
