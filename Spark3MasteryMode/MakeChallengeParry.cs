using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;

namespace Spark3MasteryMode
{
    [HarmonyPatch(typeof(Action00_Regular))]
    [HarmonyPatch("ManageParry")]
    class MakeChallengeParry
    {
        private static void Prefix()
        {
            if (MasteryMod.DifficultyIsMastery())
            {
                Dificulty.Level = DificultyLevel.Challange;
            }
        }
        private static void Postfix()
        {
            if (MasteryMod.DifficultyIsMastery())
            {
                Dificulty.Level = DificultyLevel.Hard;
            }
        }
    }

    [HarmonyPatch(typeof(Action_12_Block))]
    [HarmonyPatch("FixedUpdate")]
    class MakeChallengeBlock1
    {
        private static void Prefix()
        {
            if (MasteryMod.DifficultyIsMastery())
            {
                Dificulty.Level = DificultyLevel.Challange;
            }
        }
        private static void Postfix()
        {
            if (MasteryMod.DifficultyIsMastery())
            {
                Dificulty.Level = DificultyLevel.Hard;
            }
        }
    }

    [HarmonyPatch(typeof(Action_12_Block))]
    [HarmonyPatch("OnEnable")]
    class MakeChallengeBlock2
    {
        private static void Prefix()
        {
            if (MasteryMod.DifficultyIsMastery())
            {
                Dificulty.Level = DificultyLevel.Challange;
            }
        }
        private static void Postfix()
        {
            if (MasteryMod.DifficultyIsMastery())
            {
                Dificulty.Level = DificultyLevel.Hard;
            }
        }
    }

    [HarmonyPatch(typeof(Action00_Regular))]
    [HarmonyPatch("ManageDash")]
    class ReduceDashBlockCounterDelay
    {
        private static void Postfix(Action00_Regular __instance)
        {
            if (MasteryMod.DifficultyIsMastery())
            {
                if (__instance.Actions.Action06.enabled)
                    __instance.Actions.Action12.BlockCounter = Math.Max(__instance.Actions.Action12.BlockCounter, 0.1f);
            }
        }
    }
}