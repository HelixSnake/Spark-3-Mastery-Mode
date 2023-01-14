using HarmonyLib;
using MelonLoader;
using UnityEngine;

namespace Spark3MasteryMode
{

    [HarmonyPatch(typeof(RadsamuEnemy))]
    [HarmonyPatch("Damage")]
    class PatchEnemyDamage
    {
        private static void Prefix(HitBoxInfo h)
        {
            if (MasteryMod.DifficultyIsMastery())
            {
                if (Save.CurrentStageIndex == 38)
                    h.Damage *= 0.5f;
                else
                    h.Damage *= 0.35f;
            }
        }
        private static void Postfix(HitBoxInfo h)
        {
            if (MasteryMod.DifficultyIsMastery())
            {
                if (Save.CurrentStageIndex == 38)
                    h.Damage /= 0.5f;
                else
                    h.Damage /= 0.35f;
            }
        }
    }

    [HarmonyPatch(typeof(RadsamuEnemy))]
    [HarmonyPatch("Start")]
    class PatchEnemyParryDamageAndNerfMotorheads
    {
        private static void Prefix(RadsamuEnemy __instance)
        {
            if (MasteryMod.DifficultyIsMastery())
            {
                __instance.SmallAttackJuggleLimit = Mathf.Min(__instance.SmallAttackJuggleLimit, 15);
                if (__instance.MaxHp == 0.05f) // nerf motorhead health
                {
                    __instance.MaxHp = 0.01f;
                }
            }
        }
            private static void Postfix(RadsamuEnemy __instance)
        {
            if (MasteryMod.DifficultyIsMastery())
            {
                __instance.ArmourDamageOnParry *= 0.35f;
                __instance.LeaderAttackInterval *= 0.5f;
                __instance.BackgroundAttackInterval *= 0.5f;
                __instance.BackgroundAttackRecoveryTime = Mathf.Max(__instance.LeaderAttackRecoveryTime, __instance.BackgroundAttackRecoveryTime * 0.5f);
                __instance.DontAttackIfAnotherAttackHasStarted = false;
            }
        }
    }
}