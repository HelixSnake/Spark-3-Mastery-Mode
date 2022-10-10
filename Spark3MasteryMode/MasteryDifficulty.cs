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
        private static void Prefix(SaveFileMenu __instance)
        {
            int numOriginalDifficulties = __instance.CombatDifiNames.Length;
            string[] newNames = new string[numOriginalDifficulties + 1];
            string[] newDesc = new string[numOriginalDifficulties + 1];
            Color[] newCol = new Color[numOriginalDifficulties + 1];
            int i;
            for (i = 0; i < numOriginalDifficulties; i++)
            {
                newNames[i] = __instance.CombatDifiNames[i];
                newDesc[i] = __instance.CombatDifiDescrp[i];
                newCol[i] = __instance.CombatDifiColor[i];
            }
            newNames[i] = "MASTERY MODE";
            newDesc[i] = "Enemies are tougher and meaner, along with other changes like new medal targets. Can't change difficulty after starting. NOTE: All targets are set WITHOUT magnet dashing (dashing during homing attack for speed boost). It's recommended that you DON'T use magnet dashes.";
            newCol[i] = Color.black;

            __instance.CombatDifiNames = newNames;
            __instance.CombatDifiDescrp = newDesc;
            __instance.CombatDifiColor = newCol;
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
        private static void Prefix(Save __instance)
        {
            int numOriginalDifficulties = __instance.CombatDificultiesName.Length;
            string[] newNames = new string[numOriginalDifficulties + 1];
            int i;
            for (i = 0; i < numOriginalDifficulties; i++)
            {
                newNames[i] = __instance.CombatDificultiesName[i];
            }
            newNames[i] = "Mastery Mode";

            __instance.CombatDificultiesName = newNames;
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