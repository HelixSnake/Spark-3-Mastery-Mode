using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
namespace Spark3MasteryMode
{

    [HarmonyPatch(typeof(LevelData))]
    [HarmonyPatch("Awake")]
    class SpeedMedalsEveryLevel
    {
        private static void Prefix(LevelData __instance)
        {
            if (MasteryMod.DifficultyIsMastery())
            {
                if (__instance.ID >= 0)
                {
                    __instance.HasSpeedMedals = true;
                }
            }
        }
    }
    [HarmonyPatch(typeof(GameProgressVariables))]
    [HarmonyPatch("Start")]
    class SpeedMedalsPauseMenu
    {
        private static void Prefix(GameProgressVariables __instance)
        {
            if (MasteryMod.DifficultyIsMastery())
            {
                __instance.NoTimeMedal = false;
            }
        }
    }

    [HarmonyPatch(typeof(WorldMapCursor))]
    [HarmonyPatch("SwitchToMiniMenu")]
    class SpeedMedalsCollectathonMenuEnable
    {
        private static void Prefix(ref bool __state, ref bool ___MapMode)
        {
            __state = ___MapMode;
        }
        private static void Postfix(WorldMapCursor __instance, ref bool __state, ref LevelData ___StageData)
        {
            if (__state && ___StageData != null && ___StageData.LevelType == StageType.Collectathon && MasteryMod.DifficultyIsMastery())
            {
                __instance.MedalsHolder.SetActive(true);
                __instance.CollectathonMenu.SetActive(false);
            }
        }
    }
    [HarmonyPatch(typeof(Save))]
    [HarmonyPatch("Awake")]
    class SetNewMedalThresholds
    {
        private static void Prefix()
        {
        }
        private static void Postfix()
        {
            if (MasteryMod.DifficultyIsMastery())
            {
                Save.SpeedGoldTargets[0] = 95f;
                Save.SpeedDiaTargets[0] = 90f;
                Save.SpeedGoldTargets[1] = 80f;
                Save.SpeedDiaTargets[1] = 75f;
                Save.SpeedGoldTargets[2] = 40f;
                Save.SpeedDiaTargets[2] = 32f;
                Save.SpeedGoldTargets[3] = 36f;
                Save.SpeedDiaTargets[3] = 31f;
                Save.SpeedGoldTargets[5] = 190f;
                Save.SpeedDiaTargets[5] = 180f;
                Save.ScoreGoldTargets[7] = 600000f;
                Save.ScoreDiaTargets[7] = 1000000f;
            }
        }
    }
}
