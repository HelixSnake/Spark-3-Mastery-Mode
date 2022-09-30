using HarmonyLib;
using MelonLoader;

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
class PatchEnemyParryDamage
{
    private static void Postfix(RadsamuEnemy __instance)
    {
        if (MasteryMod.DifficultyIsMastery())
        {
            __instance.ArmourDamageOnParry *= 0.35f;
            __instance.LeaderAttackInterval *= 0.5f;
            __instance.LeaderAttackRecoveryTime *= 0.5f;
            __instance.BackgroundAttackInterval *= 0.5f;
            __instance.BackgroundAttackRecoveryTime *= 0.5f;
            __instance.DontAttackIfAnotherAttackHasStarted = false;
        }
    }
}