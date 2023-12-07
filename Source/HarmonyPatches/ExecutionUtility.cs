//TODO: Future Use, This disables the Knife Whoosh Foley 
// using HarmonyLib;
// using System.Linq;
// using System.Reflection;
// using System.Reflection.Emit;
//
// namespace SilenceOfTheHams.HarmonyPatches;
//
// [HarmonyPatch(typeof(ExecutionUtility))]
// [HarmonyPatch("ExecutionInt")]
// // ReSharper disable once UnusedType.Global
// static class ExecutionUtilityExecutionInt
// {
//     public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
//     {
//         foreach (CodeInstruction ci in instructions)
//         {
//             //SoundDefOf.Execute_Cut.PlayOneShot(victim);
//             //TODO: ??? MethodInfo playNearestLifestageSound = AccessTools.Method(type: typeof(LifeStageUtility), name: nameof(LifeStageUtility.PlayNearestLifestageSound));
//             if (ci.opcode == OpCodes.Ldsfld && ci.operand is FieldInfo { Name: "Execute_Cut" })
//             {
//                 CodeInstruction c = new CodeInstruction(OpCodes.Ret) { labels = ci.labels.ToList() };
//                 yield return c;
//                 break;
//             }
//
//             yield return ci;
//         }
//     }
// }
