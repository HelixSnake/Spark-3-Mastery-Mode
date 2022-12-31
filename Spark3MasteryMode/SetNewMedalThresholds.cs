using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;

namespace Spark3MasteryMode
{

    [HarmonyPatch(typeof(LevelData))]
    [HarmonyPatch("Awake")]
    class AddMedalsToLevels
    {
        public static List<int> LevelsWithScoreMedalsAdded = new List<int> { 6, 15, 30, 26 };
        private static void Prefix(LevelData __instance)
        {
            if (MasteryMod.DifficultyIsMastery())
            {
                if (__instance.ID >= 0)
                {
                    if (Save.SpeedDiaTargets[__instance.ID] > 0)
                    {
                        __instance.HasSpeedMedals = true;
                    }
                    else
                    {
                        __instance.HasSpeedMedals = false;
                    }
                    if (Save.ScoreDiaTargets[__instance.ID] < 1000000000000000000f)
                    {
                        __instance.HasScoreMedals = true;
                    }
                    else
                    {
                        __instance.HasScoreMedals = false;
                    }
                }
            }
        }
    }
    [HarmonyPatch(typeof(GameProgressVariables))]
    [HarmonyPatch("Start")]
    class MedalsPauseMenu
    {
        private static void Prefix(GameProgressVariables __instance)
        {
            if (MasteryMod.DifficultyIsMastery())
            {
                if (Save.SpeedDiaTargets[__instance.StageIndex] > 0)
                {
                    __instance.NoTimeMedal = false;
                }
                else
                {
                    __instance.NoTimeMedal = true;
                }
                if (Save.ScoreDiaTargets[__instance.StageIndex] < 1000000000000000000f)
                {
                    __instance.NoScoreMedal = false;
                }
                else
                {
                    __instance.NoScoreMedal = true;
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
            if (MasteryMod.DifficultyIsMastery())
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
            if (MasteryMod.DifficultyIsMastery())
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

    [HarmonyPatch(typeof(StageConpleteControl))]
    [HarmonyPatch("ModeObjects")]
    class EnableBestSpeedAndScoreEndLevel
    {
        private static void Postfix(StageConpleteControl __instance)
        {
            if (MasteryMod.DifficultyIsMastery())
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
                // Make all targets impossible until they're set
                for (int i = 0; i < Save.SpeedGoldTargets.Length; i++)
                {
                    Save.SpeedGoldTargets[i] = 0;
                }
                for (int i = 0; i < Save.SpeedDiaTargets.Length; i++)
                {
                    Save.SpeedDiaTargets[i] = 0;
                }
                for (int i = 0; i < Save.ScoreGoldTargets.Length; i++)
                {
                    Save.ScoreGoldTargets[i] = 1000000000000000000f;
                }
                for (int i = 0; i < Save.ScoreDiaTargets.Length; i++)
                {
                    Save.ScoreDiaTargets[i] = 1000000000000000000f;
                }
                
                // alpine carrera
                Save.SpeedGoldTargets[0] = 95f;
                Save.SpeedDiaTargets[0] = 90f;
                // doublemoon villa
                Save.SpeedGoldTargets[1] = 90f;
                Save.SpeedDiaTargets[1] = 75f;
                Save.ScoreGoldTargets[1] = 350000f;
                Save.ScoreDiaTargets[1] = 550000f;
                // high rise tracks
                Save.SpeedGoldTargets[2] = 45f;
                Save.SpeedDiaTargets[2] = 32f;
                Save.ScoreGoldTargets[2] = 250000f;
                Save.ScoreDiaTargets[2] = 300000f;
                // cold-dry desert
                Save.SpeedGoldTargets[3] = 38f;
                Save.SpeedDiaTargets[3] = 32f;
                // A.M. Village
                Save.SpeedGoldTargets[4] = 50f;
                Save.SpeedDiaTargets[4] = 41f;
                // Saw Man
                Save.SpeedGoldTargets[9] = 100f;
                Save.SpeedDiaTargets[9] = 80f;
                // slope jumping
                Save.SpeedGoldTargets[100] = 32f;
                Save.SpeedDiaTargets[100] = 30f;
                // jester dash
                Save.SpeedGoldTargets[101] = 47f;
                Save.SpeedDiaTargets[101] = 42f;
                // charged jester dash
                Save.SpeedGoldTargets[102] = 48f;
                Save.SpeedDiaTargets[102] = 45f;

                // lost riviera
                Save.SpeedGoldTargets[5] = 195f;
                Save.SpeedDiaTargets[5] = 165f;
                Save.ScoreGoldTargets[5] = 275000f;
                Save.ScoreDiaTargets[5] = 425000f;

                // lost ravine
                Save.SpeedGoldTargets[6] = 180f;
                Save.SpeedDiaTargets[6] = 107f;
                Save.ScoreGoldTargets[6] = 200000f;
                Save.ScoreDiaTargets[6] = 325000f;

                // canyon zero
                Save.ScoreGoldTargets[7] = 700000;
                Save.ScoreDiaTargets[7] = 1000000;

                // beatdown tower
                // 13
                Save.SpeedGoldTargets[13] = 120f;
                Save.SpeedDiaTargets[13] = 97f;

                // arid hole
                Save.SpeedGoldTargets[11] = 65f;
                Save.SpeedDiaTargets[11] = 52f;

                // splash grotto
                Save.SpeedGoldTargets[12] = 50f;
                Save.SpeedDiaTargets[12] = 41f;

                // high speeds
                Save.SpeedGoldTargets[103] = 54f;
                Save.SpeedDiaTargets[103] = 52f;
                // wall jump
                Save.SpeedGoldTargets[104] = 20f;
                Save.SpeedDiaTargets[104] = 17f;
                // wall walking
                Save.SpeedGoldTargets[105] = 36f;
                Save.SpeedDiaTargets[105] = 29f;

                // district 5

                //Save.SpeedGoldTargets[8] = 220f;
                //Save.SpeedDiaTargets[8] = 190f;
                //Save.ScoreGoldTargets[8] = 200000f;
                //Save.ScoreDiaTargets[8] = 350000f;

                // district 6 

                //Save.SpeedGoldTargets[14] = 60f;
                //Save.SpeedDiaTargets[14] = 35f;
                //Save.ScoreGoldTargets[14] = 80000f;
                //Save.ScoreDiaTargets[14] = 150000f;

                // district 9

                //Save.SpeedGoldTargets[15] = 160f;
                //Save.SpeedDiaTargets[15] = 140f;
                //Save.ScoreGoldTargets[15] = 200000f;
                //Save.ScoreDiaTargets[15] = 400000f;

                // district 4

                //Save.SpeedGoldTargets[16] = f;
                //Save.SpeedDiaTargets[16] = f;

                // district 79

                //Save.SpeedGoldTargets[17] = 100f;
                //Save.SpeedDiaTargets[17] = 70f;
                //Save.ScoreGoldTargets[17] = 200000f;
                //Save.ScoreDiaTargets[17] = 360000f;
            }
        }
    }
}
