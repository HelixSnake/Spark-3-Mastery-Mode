using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;

namespace Spark3MasteryMode
{
    [HarmonyPatch(typeof(Action_13_NewSuperMoves))]
    [HarmonyPatch("Awake")]
    class ReduceBlinkCost
    {
        private static void Prefix(Action_13_NewSuperMoves __instance)
        {
            if (MasteryMod.DifficultyIsMastery())
            {
                __instance.EnergyThreshold[5] = 15;
            }
        }
    }
}
