using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;

namespace HelixBugFix
{
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
            if (HelixFixMod.DifficultyIsNotMastery())
            {
                if (__state && ___StageData != null && ___StageData.LevelType == StageType.Collectathon)
                {
                    __instance.MedalsHolder.SetActive(true);
                    __instance.CollectathonMenu.SetActive(false);
                }
                if (__state && ___StageData != null)
                {
                    __instance.BestScoreObject.SetActive(true);
                    __instance.BestTimeObject.SetActive(true);
                }
            }
        }
    }

    [HarmonyPatch(typeof(StageConpleteControl))]
    [HarmonyPatch("ModeObjects")]
    class EnableBestSpeedAndScoreEndLevel
    {
        private static void Postfix(StageConpleteControl __instance)
        {
            if (HelixFixMod.DifficultyIsNotMastery())
            {
                for (int j = 0; j < __instance.SpeedObjects.Length; j++)
                {
                    __instance.SpeedObjects[j].SetActive(true);
                }
                for (int k = 0; k < __instance.ScoreObjects.Length; k++)
                {
                    __instance.ScoreObjects[k].SetActive(true);
                }
            }
        }
    }
}
