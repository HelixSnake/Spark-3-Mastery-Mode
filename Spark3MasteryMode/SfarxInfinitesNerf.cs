using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;

namespace Spark3MasteryMode
{
    [HarmonyPatch(typeof(Action10Control_Blast))]
    [HarmonyPatch("BlastAction")]
    class NerfSfarxBlastInfinite
    {
        private static void Prefix(Action10Control_Blast __instance, ref Vector3 __state)
        {
            __state = __instance.Player.rigid.velocity;
        }
        private static void Postfix(Action10Control_Blast __instance, ref Vector3 __state, ref float ___Counter)
        {
            if (MasteryMod.DifficultyIsMastery())
            {
                if (CharacterAnimatorChange.Character == 4)
                {
                    if (__instance.enabled)
                    {
                        if (!__instance.Player.Grounded && HomingAttackControl.TargetObject != null)
                        {
                            RadsamuEnemy enemy = HomingAttackControl.TargetObject.GetComponentInParent<RadsamuEnemy>();
                            if(enemy != null)
                            {
                                float juggleLimit = (float)typeof(RadsamuEnemy).GetField("SmallJuggleLimit", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(enemy);
                                if (juggleLimit <= 0)
                                {
                                    // don't allow sfarx to stall
                                    __instance.Player.rigid.velocity = __state;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
