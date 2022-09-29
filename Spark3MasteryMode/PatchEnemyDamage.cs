using HarmonyLib;
using MelonLoader;

[HarmonyPatch(typeof(RadsamuEnemy))]
[HarmonyPatch("Damage")]
class PatchEnemyDamage
{
    private static void Prefix(HitBoxInfo h)
    {
        h.Damage *= 0.35f;
    }
    private static void Postfix(HitBoxInfo h)
    {
        h.Damage /= 0.35f;
    }
}

[HarmonyPatch(typeof(RadsamuEnemy))]
[HarmonyPatch("Start")]
class PatchEnemyParryDamage
{
    private static void Postfix(RadsamuEnemy __instance)
    {
        __instance.ArmourDamageOnParry *= 0.35f;
        __instance.LeaderAttackInterval *= 0.4f;
        __instance.LeaderAttackRecoveryTime *= 0.4f;
        __instance.BackgroundAttackInterval *= 0.4f;
        __instance.BackgroundAttackRecoveryTime *= 0.4f;
        __instance.DontAttackIfAnotherAttackHasStarted = false;
    }
}