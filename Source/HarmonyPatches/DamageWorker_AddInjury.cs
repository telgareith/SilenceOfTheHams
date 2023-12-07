using HarmonyLib;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace SilenceOfTheHams.HarmonyPatches;

/// <summary>
/// Both <see cref="Pawn.DoKillSideEffects"/> and <see cref="DamageWorker_AddInjury.PlayWoundedVoiceSound"/> Play sounds when a Pawn is dealt <see cref="DamageDefOf.ExecutionCut"/> damage.
/// </summary>
[HarmonyPatch(typeof(DamageWorker_AddInjury))]
[HarmonyPatch("PlayWoundedVoiceSound")]
// ReSharper disable once UnusedType.Global
static class DamageWorkerAddInjuryPlayWoundedVoiceSound
{
     /// <summary>
     /// Prevents <see cref="DamageWorker_AddInjury.PlayWoundedVoiceSound">PlayWoundedVoiceSound</see> from producing sound if its an <see cref="DamageDefOf.ExecutionCut">Execution</see>
     /// </summary>
     /// <param name="dinfo">DamageInfo</param>
     /// <param name="pawn">Pawn</param>
     /// <returns>Whether execution should continue.</returns>
    static bool Prefix(DamageInfo dinfo, Pawn pawn)
    {
#if DEBUG
        Log.Message($"DamageWorker_AddInjury:PlayWoundedVoiceSound- dinfo.Def: {dinfo.Def} pawn.RaceProps.Humanlike={pawn.RaceProps.Humanlike}");
#endif
        return !dinfo.Def.Equals(DamageDefOf.ExecutionCut) && !pawn.RaceProps.Humanlike;
    }
}
