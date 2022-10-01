﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
namespace Spark3MasteryMode
{

    [HarmonyPatch(typeof(Save))]
    [HarmonyPatch("Awake")]
    class SetNewMedalThresholds
    {
        private static void Prefix()
        {
        }
        private static void Postfix()
        {
            if (MasteryMod.DifficultyIsMastery())
            {
                Save.ScoreGoldTargets[7] = 600000f;
                Save.ScoreDiaTargets[7] = 1000000f;
            }
        }
    }
}
