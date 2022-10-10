using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;

namespace Spark3MasteryMode
{
    [HarmonyPatch(typeof(SaveFileMenu))]
    [HarmonyPatch("Start")]
    class MasteryDifficultySaveFileMenu
    {
        public static void EnsureSaveFileMenuHasDifficulty(SaveFileMenu menu)
        {
            if (menu.CombatDifiNames.Length < 6)
            {
                string[] newNames = new string[6];
                string[] newDesc = new string[6];
                Color[] newCol = new Color[6];
                for (int i = 0; i < 5; i++)
                {
                    newNames[i] = menu.CombatDifiNames[i];
                    newDesc[i] = menu.CombatDifiDescrp[i];
                    newCol[i] = menu.CombatDifiColor[i];
                }
                newNames[5] = "MASTERY MODE";
                newDesc[5] = "Enemies are tougher and meaner, along with other changes like new medal targets. Can't change difficulty after starting. NOTE: All targets are set WITHOUT magnet dashing (dashing during homing attack for speed boost). It's recommended that you DON'T use magnet dashes.";
                newCol[5] = Color.black;

                menu.CombatDifiNames = newNames;
                menu.CombatDifiDescrp = newDesc;
                menu.CombatDifiColor = newCol;
            }
        }
        private static void Prefix(SaveFileMenu __instance)
        {
            EnsureSaveFileMenuHasDifficulty(__instance);
        }
    }

    [HarmonyPatch(typeof(SaveSlotReference))]
    [HarmonyPatch("Start")]
    class MasteryDifficultySaveSlot
    {
        private static void Prefix(SaveSlotReference __instance, ref Save.SaveFile ___s)
        {
            MasteryDifficultySaveFileMenu.EnsureSaveFileMenuHasDifficulty(__instance.Menu);
        }
    }

    [HarmonyPatch(typeof(Dificulty))]
    [HarmonyPatch("Start")]
    class MasteryDifficultySetDifficultyLevels
    {
        private static void Postfix(Dificulty __instance)
        {
            if (MasteryMod.DifficultyIsMastery())
            {
                Dificulty.Level = DificultyLevel.Hard;
                Dificulty.PlatformingLevel = DificultyLevel.Normal;
                __instance.Set();
            }
        }
    }
    [HarmonyPatch(typeof(Save))]
    [HarmonyPatch("Awake")]
    class AddDifficultyToSave
    {
        public static void EnsureSaveClassHasDifficulty(Save save)
        {
            if (save.CombatDificultiesName.Length < 6 || save.CombatDificultiesName[5] != "Mastery Mode")
            {
                string[] newNames = new string[6];
                int i;
                for (i = 0; i < 5; i++)
                {
                    newNames[i] = save.CombatDificultiesName[i];
                }
                newNames[5] = "MASTERY MODE";

                save.CombatDificultiesName = newNames;
            }
        }
        private static void Prefix(Save __instance)
        {
            EnsureSaveClassHasDifficulty(__instance);
        }
    }


    [HarmonyPatch(typeof(WorldMapPauseMenu))]
    [HarmonyPatch("Start")]
    class AddDifficultyToWorldMapPauseMenu
    {
        private static void Prefix()
        {
            AddDifficultyToSave.EnsureSaveClassHasDifficulty(Save.SaveFileScript);
        }
    }

    [HarmonyPatch(typeof(NewGamePlusMenu))]
    [HarmonyPatch("Start")]
    class AddDifficultyToNewGamePlusMenu
    {
        private static void Prefix()
        {
            AddDifficultyToSave.EnsureSaveClassHasDifficulty(Save.SaveFileScript);
        }
    }

    [HarmonyPatch(typeof(PlayerHealthAndStats))]
    [HarmonyPatch("Awake")]
    class SetMasteryModeHealth
    {
        private static void Postfix(PlayerHealthAndStats __instance)
        {
            if (MasteryMod.DifficultyIsMastery())
            {
                __instance.HpOver.sprite = __instance.HardBar;
                __instance.HpCover.sprite = __instance.HardBar;
                PlayerHealthAndStats.PlayerMaxHp = 3;
            }
        }
    }

}