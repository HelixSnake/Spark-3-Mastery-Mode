using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;

namespace Spark3MasteryMode
{
    [HarmonyPatch(typeof(WorldMapPauseMenu))]
    [HarmonyPatch("Update")]
    class SetDifficultyMinimumPauseMenu
    {
        private static void Prefix()
        {
        }
        private static void Postfix(WorldMapPauseMenu __instance)
        {
            if (Save.GetCurrentSave().OriginalCombatDificulty == 5)
            {
                if (Save.GetCurrentSave().CombatDificulty < 5 || Save.GetCurrentSave().PlatformingDificulty < 2)
                {
                    Save.GetCurrentSave().CombatDificulty = 5;
                    Save.GetCurrentSave().PlatformingDificulty = 2;
                    typeof(WorldMapPauseMenu).GetMethod("SetDificultyText", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(__instance, new object[0]);
                }
            }
        }
    }
}