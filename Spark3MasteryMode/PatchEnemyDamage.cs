using HarmonyLib;
using MelonLoader;

namespace Spark3MasteryMode
{
    [HarmonyPatch(typeof(RadsamuEnemy))]
    [HarmonyPatch("Damage")]
    class PatchEnemyDamage
    {
        private static void Prefix(HitBoxInfo h)
        {
            h.Damage *= 35f;
        }
        private static void Postfix(HitBoxInfo h)
        {
            h.Damage /= 35f;
        }
    }
}