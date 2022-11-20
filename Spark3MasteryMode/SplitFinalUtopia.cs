using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using HarmonyLib;

namespace HelixBugFix
{
    [HarmonyPatch(typeof(LevelData))]
    [HarmonyPatch("Awake")]
    class SplitFinalUtopia
    {
        static private void Prefix(LevelData __instance)
        {
            if (HelixFixMod.DifficultyIsNotMastery())
            {
                if (__instance.ID == 90) // instance is newly created Flint boss fight
                {
                    __instance.LevelType = StageType.BossFight;
                    __instance.StageName = "BOSS - FLINT";
                    __instance.AreaName = "AREA 08 - UTOPIA SHELTER";
                    __instance.AreaColor = new Color(0.7412f, 0.4078f, 0.9529f, 1);
                    __instance.StageScene = "[STAGE 08 - FLINT FINAL FIGHT]";
                }
                if (__instance.ID == 91) // instance is Final Utopia level
                {
                    __instance.LevelType = StageType.Stage;
                    __instance.StageName = "ROAD TO SHELTER";
                    __instance.AreaName = "AREA 08 - UTOPIA SHELTER";
                    __instance.StageScene = "[STAGE 08 - UTOPIA]";
                }
                if (__instance.ID == 92) // instance is Linework Spark boss fight
                {
                    __instance.LevelType = StageType.BossFight;
                    __instance.StageName = "BOSS - LINEWORK SPARK";
                    __instance.AreaName = "AREA 08 - UTOPIA SHELTER";
                    __instance.AreaColor = new Color(0.7412f, 0.4078f, 0.9529f, 1);
                    __instance.StageScene = "[STAGE 09 - LINEWORK SPARK FIGHT]";
                }
                if (__instance.ID == 93) // instance is Clarity Centralis boss fight
                {
                    __instance.LevelType = StageType.BossFight;
                    __instance.StageName = "BOSS - CENTRALIS";
                    __instance.AreaName = "AREA 08 - UTOPIA SHELTER";
                    __instance.AreaColor = new Color(0.7412f, 0.4078f, 0.9529f, 1);
                    __instance.StageScene = "[STAGE 09 - CLARITAS CENTRALLIS FIGHT]";
                }
            }
        }
    }
    [HarmonyPatch(typeof(WorldMapCursor))]
    [HarmonyPatch("Start")]
    class SplitFinalUtopiaCursor
    {
        static private void Prefix(WorldMapCursor __instance)
        {
            if (HelixFixMod.DifficultyIsNotMastery())
            {
                LevelData finalUtopia = __instance.AllLevelData[44];

                finalUtopia.ID = 90; // prepare ID for flint boss fight on copy
                var flintBossLevelObj = GameObject.Instantiate(finalUtopia.gameObject);
                flintBossLevelObj.transform.parent = finalUtopia.transform.parent;
                flintBossLevelObj.name = "US - Flint Final Fight";
                flintBossLevelObj.transform.localPosition = new Vector3(10.5f, -5.725f, 0.01f);
                flintBossLevelObj.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
                var flintBossLevelLine = flintBossLevelObj.transform.GetChild(1).GetComponent<LineRenderer>();
                Vector3 pos1 = finalUtopia.transform.position;
                Vector3 pos2 = flintBossLevelObj.transform.position;
                pos1.z = -0.015f;
                pos2.z = -0.015f;
                flintBossLevelLine.SetPosition(0, pos1);
                flintBossLevelLine.SetPosition(1, pos2);

                finalUtopia.ID = 91; // prepare ID for road to utopia on copy
                var utopiaLevelObj = GameObject.Instantiate(finalUtopia.gameObject);
                utopiaLevelObj.transform.parent = finalUtopia.transform.parent;
                utopiaLevelObj.name = "US - Road To Utopia Shelter";
                utopiaLevelObj.transform.localPosition = new Vector3(8.775f, -6.725f, 0.01f);
                utopiaLevelObj.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
                var utopiaLevelLine = utopiaLevelObj.transform.GetChild(1).GetComponent<LineRenderer>();
                Vector3 upos1 = finalUtopia.transform.position;
                Vector3 upos2 = utopiaLevelObj.transform.position;
                upos1.z = -0.015f;
                upos2.z = -0.015f;
                utopiaLevelLine.SetPosition(0, upos1);
                utopiaLevelLine.SetPosition(1, upos2);

                finalUtopia.ID = 92; // prepare ID for linework spark boss on copy
                var lineworkObj = GameObject.Instantiate(finalUtopia.gameObject);
                lineworkObj.transform.parent = finalUtopia.transform.parent;
                lineworkObj.name = "US - Linework Spark Fight";
                lineworkObj.transform.localPosition = new Vector3(10.7f, -6.725f, 0.01f);
                lineworkObj.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
                var lineworkLine = lineworkObj.transform.GetChild(1).GetComponent<LineRenderer>();
                Vector3 lpos1 = finalUtopia.transform.position;
                Vector3 lpos2 = lineworkObj.transform.position;
                lpos1.z = -0.015f;
                lpos2.z = -0.015f;
                lineworkLine.SetPosition(0, lpos1);
                lineworkLine.SetPosition(1, lpos2);


                finalUtopia.ID = 93; // prepare ID for linework spark boss on copy
                var centralisObj = GameObject.Instantiate(finalUtopia.gameObject);
                centralisObj.transform.parent = finalUtopia.transform.parent;
                centralisObj.name = "US - Centralis Fight";
                centralisObj.transform.localPosition = new Vector3(10.5f, -7.725f, 0.01f);
                centralisObj.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
                var centralisLine = centralisObj.transform.GetChild(1).GetComponent<LineRenderer>();
                Vector3 cpos1 = finalUtopia.transform.position;
                Vector3 cpos2 = centralisObj.transform.position;
                cpos1.z = -0.015f;
                cpos2.z = -0.015f;
                centralisLine.SetPosition(0, cpos1);
                centralisLine.SetPosition(1, cpos2);

                LevelData[] addedLevelData = new LevelData[4];
                addedLevelData[0] = flintBossLevelObj.GetComponent<LevelData>();
                addedLevelData[1] = utopiaLevelObj.GetComponent<LevelData>();
                addedLevelData[2] = lineworkObj.GetComponent<LevelData>();
                addedLevelData[3] = centralisObj.GetComponent<LevelData>();

                __instance.AllLevelData = __instance.AllLevelData.AddRangeToArray(addedLevelData);

                // return to original level ID
                finalUtopia.ID = 50;
            }
        }
    }

    [HarmonyPatch(typeof(LineworkSparkBossFight))]
    [HarmonyPatch("FixedUpdate")]
    class SplitFinalUtopiaBossGoToEndScreen
    {
        static List<int> ModifiedBossLevels = new List<int> { 90, 92, 93 };
        static private bool Prefix(LineworkSparkBossFight __instance, ref float ___endingcounter)
        {
            if (HelixFixMod.DifficultyIsNotMastery())
            {
                if (ModifiedBossLevels.Contains(Save.LastStageStarted))
                {
                    if (__instance.ending)
                    {
                        PauseCotrol.PauseCounter = 0f;
                        __instance.Hurt.GetComponent<LevelProgressControl>().readyForNextStage = true;
                        LevelProgressControl.LevelOver = true;
                        Time.timeScale = 1f;
                        return false;
                    }
                }
            }
            return true;
        }
    }
    [HarmonyPatch(typeof(DisableButtons))]
    [HarmonyPatch("Start")]
    class SplitFinalUtopiaDontDisableButtons
    {
        static List<int> ModifiedLevels = new List<int> { 90, 91, 92, 93 };
        static private bool Prefix(DisableButtons __instance)
        {
            if (HelixFixMod.DifficultyIsNotMastery())
            {
                if (ModifiedLevels.Contains(Save.LastStageStarted))
                {
                    return false;
                }
            }
            return true;
        }
    }

    [HarmonyPatch(typeof(GameProgressVariables))]
    [HarmonyPatch("Start")]
    class SplitFinalUtopiaDisableLivesAndEnableEndScreen
    {
        static List<int> ModifiedLevels = new List<int> { 90, 91, 92, 93 };
        static private void Prefix(GameProgressVariables __instance)
        {
            if (HelixFixMod.DifficultyIsNotMastery())
            {
                if (ModifiedLevels.Contains(Save.LastStageStarted))
                {
                    __instance.StageToGoAfter = "0";
                    __instance.StageIndex = Save.LastStageStarted;
                    __instance.EnableLives = false;
                    __instance.SkipEndStageScreen = false;
                    __instance.IsStage = true;
                }
            }
        }
    }
    [HarmonyPatch(typeof(UnlockStuffOnStart))]
    [HarmonyPatch("Start")]
    class SplitFinalUtopiaUnlockLevels
    {
        static private void Prefix(UnlockStuffOnStart __instance)
        {
            if (HelixFixMod.DifficultyIsNotMastery())
            {
                if (__instance.StagesToUnlock.Length == 3 && __instance.StagesToUnlock[0] == 54 && __instance.StagesToUnlock[1] == 55 && __instance.StagesToUnlock[2] == 141) // final cutscene
                {
                    __instance.StagesToUnlock = new int[] { 54, 55, 141, 90, 91, 92, 93 };
                }
            }
        }
    }

    [HarmonyPatch(typeof(WorldMapCursor))]
    [HarmonyPatch("SwitchToMiniMenu")]
    class SplitFinalUtopiaSetStageID
    {
        static private void Prefix(WorldMapCursor __instance, ref bool ___MapMode, ref LevelData ___StageData)
        {
            if (HelixFixMod.DifficultyIsNotMastery())
            {
                if (___MapMode && ___StageData != null)
                {
                    if (___StageData.LevelType == StageType.ShopOrMisc)
                    {
                        Save.LastStageStarted = ___StageData.ID; // nothing actually uses Save.LastStageStarted but we need this for certain levels to work correctly if visited after our new levels
                    }
                }
            }
        }
    }
}