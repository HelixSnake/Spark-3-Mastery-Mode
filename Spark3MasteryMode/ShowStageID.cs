using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using MelonLoader;

namespace Spark3MasteryMode
{
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
                /*
                float val1 = PlayerHealthAndStats.AttackMultiplierCombo.Evaluate(0.36f) * 1.36f;
                float val2 = PlayerHealthAndStats.AttackMultiplierCombo.Evaluate(0.55f) * 1.55f;
                float val3 = PlayerHealthAndStats.AttackMultiplierCombo.Evaluate(0.72f) * 1.72f;
                float val4 = PlayerHealthAndStats.AttackMultiplierCombo.Evaluate(0.85f) * 1.85f;
                float val5 = PlayerHealthAndStats.AttackMultiplierCombo.Evaluate(1f) * 2f;
                __instance.SpeedGoldGoal.text = val1.ToString() + ", " + val2.ToString() + ", " + val3.ToString() + ", " + val4.ToString() + ", " + val5.ToString();
            */
            }
        }
    }
}