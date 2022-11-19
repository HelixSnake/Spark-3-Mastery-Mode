using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;

namespace HelixBugFix
{
    [HarmonyPatch(typeof(SceneController))]
    [HarmonyPatch("ResetValues")]
    class FixComboMeterReset
    {
        private static void Postfix()
        {
            if (HelixFixMod.DifficultyIsNotMastery())
            {
                PlayerHealthAndStats.Combo = 0;
            }
        }
    }

    [HarmonyPatch(typeof(PauseCotrol))]
    [HarmonyPatch("RestartCheckpoint")]
    class FixComboMeterResetCheckpoint
    {
        private static void Postfix()
        {
            if (HelixFixMod.DifficultyIsNotMastery())
            {
                PlayerHealthAndStats.Combo = 0;
            }
        }
    }
    [HarmonyPatch(typeof(HurtControl))]
    [HarmonyPatch("Death")]
    class FixComboMeterResetDeath
    {
        private static void Postfix()
        {
            if (HelixFixMod.DifficultyIsNotMastery())
            {
                PlayerHealthAndStats.Combo = 0;
            }
        }
    }
}
