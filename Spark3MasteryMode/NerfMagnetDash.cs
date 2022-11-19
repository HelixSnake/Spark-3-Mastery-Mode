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
    [HarmonyPatch(typeof(Action02_Homing))]
    [HarmonyPatch("Awake")]
    class NerfMagnetDash
    {
        static private void Prefix(Action02_Homing __instance)
        {
            if (MasteryMod.DifficultyIsMastery())
            {
                __instance.MagnetDashMultiplier = 0.4f;
            }
        }
    }

    [HarmonyPatch(typeof(Action02_Homing))]
    [HarmonyPatch("FixedUpdate")]
    class MakeMagnetDashMoreConsistent
    {
        private const float ADDED_SPEED = 50f;
        private const float MULTIPLIED_SPEED = 1.2f;
        static private bool Prefix(Action02_Homing __instance, ref Vector3 ___direction, ref float ___Speed, ref PlayerBhysics ___Player)
        {
            if (MasteryMod.DifficultyIsMastery())
            {
                bool dashAvailable = __instance.Actions.Action00.DashAvailable;
                if (__instance.Inp.Rewinp.GetButtonDown("Dash") && CharacterAnimatorChange.Character != 2)
                {
                    if (__instance.Target)
                    {
                        ___direction = __instance.Target.position - __instance.transform.position;
                    }
                    ___direction = ___direction.normalized;
                    if (dashAvailable)
                    {
                        Vector3 newDir = new Vector3(___direction.x, 0.001f, ___direction.z);
                        ___Speed *= newDir.magnitude;
                        ___direction = newDir.normalized;
                    }
                    ___Speed = Mathf.Max(___Speed, __instance.InitialHorizontalVelocity * MULTIPLIED_SPEED + ADDED_SPEED);
                    ___Player.rigid.velocity = ___direction * ___Speed;
                    __instance.Actions.ChangeAction(1);
                    return false;
                }
            }
            return true;
        }
    }
}
