using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;

[HarmonyPatch(typeof(Action00_Regular))]
[HarmonyPatch("ManageParry")]
class MakeChallengeParry
{
    private static void Prefix()
    {
        if (MasteryMod.DifficultyIsMastery())
        {
            Dificulty.Level = DificultyLevel.Challange;
        }
    }
    private static void Postfix()
    {
        if (MasteryMod.DifficultyIsMastery())
        {
            Dificulty.Level = DificultyLevel.Hard;
        }
    }
}

[HarmonyPatch(typeof(Action_12_Block))]
[HarmonyPatch("FixedUpdate")]
class MakeChallengeBlock1
{
    private static void Prefix()
    {
        if (MasteryMod.DifficultyIsMastery())
        {
            Dificulty.Level = DificultyLevel.Challange;
        }
    }
    private static void Postfix()
    {
        if (MasteryMod.DifficultyIsMastery())
        {
            Dificulty.Level = DificultyLevel.Hard;
        }
    }
}

[HarmonyPatch(typeof(Action_12_Block))]
[HarmonyPatch("OnEnable")]
class MakeChallengeBlock2
{
    private static void Prefix()
    {
        if (MasteryMod.DifficultyIsMastery())
        {
            Dificulty.Level = DificultyLevel.Challange;
        }
    }
    private static void Postfix()
    {
        if (MasteryMod.DifficultyIsMastery())
        {
            Dificulty.Level = DificultyLevel.Hard;
        }
    }
}
