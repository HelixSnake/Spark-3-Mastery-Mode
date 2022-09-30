using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;

[HarmonyPatch(typeof(WorldMapPauseMenu))]
[HarmonyPatch("Update")]
class SetDifficultyMinimumPauseMenu
{
    private static void Prefix(out bool __state)
    {
        __state = MasteryMod.DifficultyIsMastery();
    }
    private static void Postfix(bool __state, WorldMapPauseMenu __instance)
    {
        if (__state)
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