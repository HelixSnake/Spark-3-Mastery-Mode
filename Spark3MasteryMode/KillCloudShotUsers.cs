using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;

namespace Spark3MasteryMode
{
    [HarmonyPatch(typeof(FlutterObject))]
    [HarmonyPatch("Init")]
    class KillCloudShotUsers
    {
        private static bool Prefix(FlutterObject __instance)
        {
            if (MasteryMod.DifficultyIsMastery())
            {
                PlayerHealthAndStats.PlayerHP = -1;
                __instance.Player.GetComponent<HurtControl>().CheckForDeathAndKill();
                return false;
            }
            return true;
        }
    }
}
