using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using HarmonyLib;

namespace HelixBugFix
{


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
    [HarmonyPatch(typeof(WorldMapCursor))]
    [HarmonyPatch("Start")]
    class SetUpMedalGoals
    {
        private static void MoveOutOfUnused(GameObject gObject)
        {
            if (gObject.transform.parent.gameObject.name == "Unused")
            {
                gObject.transform.parent = gObject.transform.parent.parent;
            }
        }

        private static void Postfix(WorldMapCursor __instance)
        {
            if (HelixFixMod.DifficultyIsNotMastery())
            {
                GameObject scoreGold = __instance.ScoreGoldGoal.transform.parent.gameObject;
                GameObject scoreDia = __instance.ScoreDiaGoal.transform.parent.gameObject;
                GameObject speedGold = __instance.SpeedGoldGoal.transform.parent.gameObject;
                GameObject speedDia = __instance.SpeedDiaGoal.transform.parent.gameObject;

                MoveOutOfUnused(scoreGold);
                MoveOutOfUnused(scoreDia);
                MoveOutOfUnused(speedGold);
                MoveOutOfUnused(speedDia);

                RectTransform rectScoreGold = scoreGold.GetComponent<RectTransform>();
                RectTransform rectScoreDia = scoreDia.GetComponent<RectTransform>();
                RectTransform rectSpeedGold = speedGold.GetComponent<RectTransform>();
                RectTransform rectSpeedDia = speedDia.GetComponent<RectTransform>();
                rectScoreGold.anchoredPosition += new Vector2(25, 10);
                rectScoreDia.anchoredPosition += new Vector2(25, 10);
                rectSpeedGold.anchoredPosition += new Vector2(-35, 10);
                rectSpeedDia.anchoredPosition += new Vector2(-35, 10);
                rectScoreGold.sizeDelta = new Vector2(70, 15);
                rectScoreDia.sizeDelta = new Vector2(70, 15);
                rectSpeedGold.sizeDelta = new Vector2(65, 15);
                rectSpeedDia.sizeDelta = new Vector2(65, 15);
            }
        }
    }

    [HarmonyPatch(typeof(WorldMapCursor))]
    [HarmonyPatch("SwitchToMiniMenu")]
    class EnableMedalsInStageMenu
    {
        private static void SetColorBackground(Text text)
        {
            Image image = text.transform.parent.GetComponent<Image>();
            image.color = image.transform.parent.parent.GetComponent<Image>().color;
        }

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

                    __instance.SpeedGoldGoal.text = WorldMapCursor.FormattedTime(Save.SpeedGoldTargets[___StageData.ID]);
                    __instance.SpeedDiaGoal.text = WorldMapCursor.FormattedTime(Save.SpeedDiaTargets[___StageData.ID]);
                    __instance.ScoreGoldGoal.text = string.Format("{0:000,000,000}", Save.ScoreGoldTargets[___StageData.ID]);
                    __instance.ScoreDiaGoal.text = string.Format("{0:000,000,000}", Save.ScoreDiaTargets[___StageData.ID]);

                    SetColorBackground(__instance.SpeedGoldGoal);
                    SetColorBackground(__instance.SpeedDiaGoal);
                    SetColorBackground(__instance.ScoreGoldGoal);
                    SetColorBackground(__instance.ScoreDiaGoal);

                    if (___StageData.HasScoreMedals)
                    {
                        __instance.ScoreGoldGoal.transform.parent.gameObject.SetActive(true);
                        __instance.ScoreDiaGoal.transform.parent.gameObject.SetActive(true);
                    }
                    else
                    {
                        __instance.ScoreGoldGoal.transform.parent.gameObject.SetActive(false);
                        __instance.ScoreDiaGoal.transform.parent.gameObject.SetActive(false);
                    }
                    if (___StageData.HasSpeedMedals)
                    {
                        __instance.SpeedGoldGoal.transform.parent.gameObject.SetActive(true);
                        __instance.SpeedDiaGoal.transform.parent.gameObject.SetActive(true);
                    }
                    else
                    {
                        __instance.SpeedGoldGoal.transform.parent.gameObject.SetActive(false);
                        __instance.SpeedDiaGoal.transform.parent.gameObject.SetActive(false);
                    }
                }
            }
        }
    }
}
