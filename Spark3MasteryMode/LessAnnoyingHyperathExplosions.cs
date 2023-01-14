using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;

namespace Spark3MasteryMode
{
    [HarmonyPatch(typeof(HitBoxInfo))]
    [HarmonyPatch("Start")]
    class DisableHitboxesOnHit
    {
        static Dictionary<string, float> HitboxDisableTimes = new Dictionary<string, float>()
        {
            { "BlasterShotExplosionBigCannonRed(Clone)", 0.3f },
            { "BlasterShotExplosionBigCannon(Clone)", 0.3f },
        };

        private static void Postfix(HitBoxInfo __instance)
        {
            if(MasteryMod.DifficultyIsMastery())
            {
                if (HitboxDisableTimes.ContainsKey(__instance.gameObject.name))
                {
                    var deactivateColliders = __instance.gameObject.AddComponent<DeactivateCollidersForAwhile>();
                    deactivateColliders.enabled = false;
                    float time = HitboxDisableTimes[__instance.gameObject.name];
                    deactivateColliders.ExtraColliders = new Collider[1];
                    deactivateColliders.ExtraColliders[0] = deactivateColliders.GetComponent<Collider>();
                    deactivateColliders.TimeDoDisable = time;
                }
            }
        }
    }
    [HarmonyPatch(typeof(DeactivateCollidersForAwhile))]
    [HarmonyPatch("DisableColliders")]
    class KillBigCannonRedExplosion
    {
        private static void Postfix(DeactivateCollidersForAwhile __instance)
        {
            if (MasteryMod.DifficultyIsMastery())
            {
                if (__instance.gameObject.name == "BlasterShotExplosionBigCannonRed(Clone)")
                {
                    // animator will keep trying to re-enable collider - kill it
                    GameObject.Destroy(__instance.ExtraColliders[0]);
                    GameObject.Destroy(__instance);
                }
            }
        }
    }
}
