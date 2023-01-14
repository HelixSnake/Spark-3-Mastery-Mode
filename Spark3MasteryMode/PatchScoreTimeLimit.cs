using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using MelonLoader;

namespace Spark3ScoreLockMod
{

    [HarmonyPatch(typeof(ScoreManager))]
    [HarmonyPatch("Start")]
    class PatchScoreTimeLimit
    {
        private static void Prefix()
        {

        }
        private static void Postfix(ref float ___TimeLimitStatic)
        {
            if (!ScoreLockMod.DifficultyIsMastery())
            {
                if (Save.CurrentStageIndex != 7 && Save.CurrentStageIndex != 50)
                    ___TimeLimitStatic = 420f;
            }
        }
    }
}