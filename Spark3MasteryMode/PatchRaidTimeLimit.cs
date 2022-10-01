using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HarmonyLib;
using MelonLoader;
namespace Spark3MasteryMode
{

    [HarmonyPatch(typeof(Raid))]
    [HarmonyPatch("Start")]
    class PatchRaidTimeLimit
    {
        private static void Prefix(Raid __instance)
        {
            if (MasteryMod.DifficultyIsMastery())
            {
                __instance.LevelEndTime = 300f;
                __instance.MinScoreGoal = 100000f;
                __instance.GoldScoreGoal = 700000f;
                __instance.DiaScoreGoal = 1000000f;
            }
        }
        private static void Postfix()
        {
        }
    }

}