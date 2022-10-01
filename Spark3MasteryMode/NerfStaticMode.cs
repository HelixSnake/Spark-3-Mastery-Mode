using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;

namespace Spark3MasteryMode
{
    [HarmonyPatch(typeof(FarkStaticMode))]
    [HarmonyPatch("StartStaticMode")]
    class NerfStaticMode
    {
        public static float normalEnergyMultiplier = 0;
        static private void Postfix(FarkStaticMode __instance)
        {
            if (MasteryMod.DifficultyIsMastery())
            {
                PlayerHealthAndStats.Energy -= 50f;
                Dificulty.EnergyGainMultiplier = 0;
            }
        }
    }
    [HarmonyPatch(typeof(FarkStaticMode))]
    [HarmonyPatch("DisableStaticMode")]
    class NerfStaticModeDisable
    {
        static private void Postfix()
        {
            if (MasteryMod.DifficultyIsMastery())
            {
                Dificulty.EnergyGainMultiplier = 1;
            } 
        }
    }
}