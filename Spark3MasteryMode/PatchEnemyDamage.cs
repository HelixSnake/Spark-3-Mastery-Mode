using HarmonyLib;
using MelonLoader;

[HarmonyPatch(typeof(RadsamuEnemy))]
[HarmonyPatch("Damage")]
class PatchEnemyDamage
{
    private static void Prefix(HitBoxInfo h)
    {
        h.Damage *= 0.25f;
    }
    private static void Postfix(HitBoxInfo h)
    {
        h.Damage /= 0.25f;
    }
}