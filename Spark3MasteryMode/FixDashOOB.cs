using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;

namespace Spark3MasteryMode
{
    [HarmonyPatch(typeof(Action06_Dash))]
    [HarmonyPatch("Awake")]
    class FixDashOOB
    {
        static private void Prefix(Action06_Dash __instance)
        {
            if (MasteryMod.DifficultyIsMastery())
            {
                __instance.FarkDashRayMask = 41947137;
                __instance.ReaperDashRayMask = 41947137;
            }
        }
    }
}
