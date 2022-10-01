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
        public static float normalEnergyOnHit = 0;
        static private void Postfix(FarkStaticMode __instance)
        {
            if (MasteryMod.DifficultyIsMastery())
            {
                PlayerHealthAndStats.Energy -= 50f;
                if (PlayerHealthAndStats.EnergyOnHit > 0)
                {
                    normalEnergyOnHit = PlayerHealthAndStats.EnergyOnHit;
                    PlayerHealthAndStats.EnergyOnHit = 0;
                }
                __instance.EnergyConsumption = 10;
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
                if (NerfStaticMode.normalEnergyOnHit != 0)
                {
                    PlayerHealthAndStats.EnergyOnHit = NerfStaticMode.normalEnergyOnHit;
                }
            } 
        }
    }
}