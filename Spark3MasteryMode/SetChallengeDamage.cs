using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;

[HarmonyPatch(typeof(Dificulty))]
[HarmonyPatch("Start")]
class SetChallengeDamage
{
    private static void Prefix(Dificulty __instance)
    {
        __instance.Challange_AttackMulti = 1;
        __instance.Challange_ScoreMultiplier = 1;
    }
}

[HarmonyPatch(typeof(Dificulty))]
[HarmonyPatch("Set")]
class SetDifficultyMinimum
{
    private static void Prefix()
    {
        if (Dificulty.Level == DificultyLevel.Casual || Dificulty.Level == DificultyLevel.Easy || Dificulty.Level == DificultyLevel.Normal)
        {
            Dificulty.Level = DificultyLevel.Hard;
        }
    }
}

[HarmonyPatch(typeof(Dificulty))]
[HarmonyPatch("Update")]
class SetDifficultyMinimumUpdate
{
    private static void Prefix()
    {
        if (Dificulty.Level == DificultyLevel.Casual || Dificulty.Level == DificultyLevel.Easy || Dificulty.Level == DificultyLevel.Normal)
        {
            Dificulty.Level = DificultyLevel.Hard;
        }
    }
}

[HarmonyPatch(typeof(InGameDificultyButton))]
[HarmonyPatch("Update")]
class SetDifficultyMinimumMenu
{
    private static void Postfix(InGameDificultyButton __instance, ref int ___type)
    {
        if (___type < 4)
        {
            ___type = 4;
            Dificulty.Level = DificultyLevel.Hard;
            __instance.DifiText.text = Dificulty.GetDificultyName(___type);
        }
    }
}

[HarmonyPatch(typeof(WorldMapPauseMenu))]
[HarmonyPatch("SetDificultyText")]
class SetDifficultyMinimumPauseMenu
{
    private static void Prefix()
    {
        Save.GetCurrentSave().CombatDificulty = Mathf.Clamp(Save.GetCurrentSave().CombatDificulty, 3, 4);
    }
}