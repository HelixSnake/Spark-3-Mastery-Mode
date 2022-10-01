using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;

namespace Spark3MasteryMode
{
    [HarmonyPatch(typeof(CustomStageSettings))]
    [HarmonyPatch("SetCharacterInfo")]
    class DisableForcedCharacter
    {
        private static bool Prefix()
        {
            if (MasteryMod.DifficultyIsMastery())
            {
                return false;
            }
            return true;
        }
    }
}
