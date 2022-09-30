using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using MelonLoader;

[HarmonyPatch(typeof(PauseMenuMedalTargets))]
[HarmonyPatch("Start")]
class ShowStageIndex
{
    private static void Prefix()
    {
    }
    private static void Postfix(PauseMenuMedalTargets __instance)
    {
        if (MasteryMod.DifficultyIsMastery())
        {
            // for debugging
            //__instance.SpeedObject.SetActive(true);

            //__instance.SpeedGoldGoal.text = "GOLD: " + WorldMapCursor.FormattedTime(Save.SpeedGoldTargets[Save.CurrentStageIndex]) + " ID: " + Save.CurrentStageIndex;

        }
    }
}