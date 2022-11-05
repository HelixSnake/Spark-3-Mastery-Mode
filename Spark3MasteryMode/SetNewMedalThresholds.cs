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
    class AddMedalsToLevels
    {
        public static List<int> LevelsWithScoreMedalsAdded = new List<int> { 6, 15, 30, 26 };
        private static void Prefix(LevelData __instance)
        {
            if (MasteryMod.DifficultyIsMastery())
            {
                if (__instance.ID >= 0)
                {
                    __instance.HasSpeedMedals = true;
                    if (LevelsWithScoreMedalsAdded.Contains(__instance.ID))
                    {
                        __instance.HasScoreMedals = true;
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
                __instance.NoTimeMedal = false;
                if (AddMedalsToLevels.LevelsWithScoreMedalsAdded.Contains(Save.CurrentStageIndex))
                {
                    __instance.NoScoreMedal = false;
                }
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
            if (MasteryMod.DifficultyIsMastery())
            {
                if (__state && ___StageData != null && ___StageData.LevelType == StageType.Collectathon)
                {
                    __instance.MedalsHolder.SetActive(true);
                    __instance.CollectathonMenu.SetActive(false);
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
                    Save.ScoreGoldTargets[i] = 1000000000000000000;
                }
                for (int i = 0; i < Save.ScoreDiaTargets.Length; i++)
                {
                    Save.ScoreDiaTargets[i] = 1000000000000000000;
                }
                
                // alpine carrera
                Save.SpeedGoldTargets[0] = 95f;
                Save.SpeedDiaTargets[0] = 90f;
                // doublemoon villa
                Save.SpeedGoldTargets[1] = 85f;
                Save.SpeedDiaTargets[1] = 75f;
                Save.ScoreGoldTargets[1] = 400000f;
                Save.ScoreDiaTargets[1] = 550000f;
                // high rise tracks
                Save.SpeedGoldTargets[2] = 40f;
                Save.SpeedDiaTargets[2] = 32f;
                Save.ScoreGoldTargets[2] = 280000f;
                Save.ScoreDiaTargets[2] = 300000f;
                // cold-dry desert
                Save.SpeedGoldTargets[3] = 36f;
                Save.SpeedDiaTargets[3] = 32f;
                // A.M. Village
                Save.SpeedGoldTargets[4] = 45f;
                Save.SpeedDiaTargets[4] = 41f;
                // Saw Man
                Save.SpeedGoldTargets[9] = 90f;
                Save.SpeedDiaTargets[9] = 80f;
                // slope jumping
                Save.SpeedGoldTargets[100] = 32f;
                Save.SpeedDiaTargets[100] = 30f;
                // jester dash
                Save.SpeedGoldTargets[101] = 45f;
                Save.SpeedDiaTargets[101] = 42f;
                // charged jester dash
                Save.SpeedGoldTargets[102] = 48f;
                Save.SpeedDiaTargets[102] = 45f;

                // lost riviera
                Save.SpeedGoldTargets[5] = 180f;
                Save.SpeedDiaTargets[5] = 160f;
                Save.ScoreGoldTargets[5] = 350000f;
                Save.ScoreDiaTargets[5] = 425000f;

                // lost ravine
                Save.SpeedGoldTargets[6] = 130f;
                Save.SpeedDiaTargets[6] = 107f;
                Save.ScoreGoldTargets[6] = 250000f;
                Save.ScoreDiaTargets[6] = 325000f;

                // canyon zero
                Save.ScoreGoldTargets[7] = 700000;
                Save.ScoreDiaTargets[7] = 1000000;

                // beatdown tower
                // 13
                Save.SpeedGoldTargets[13] = 110f;
                Save.SpeedDiaTargets[13] = 97f;

                // arid hole
                Save.SpeedGoldTargets[11] = 60f;
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
                // wall running
                Save.SpeedGoldTargets[105] = 33f;
                Save.SpeedDiaTargets[105] = 29f;
            }
        }
    }
}
