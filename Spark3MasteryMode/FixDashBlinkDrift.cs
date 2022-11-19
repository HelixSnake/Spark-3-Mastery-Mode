using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;

namespace Spark3MasteryMode
{
    [HarmonyPatch(typeof(Action06_Dash))]
    [HarmonyPatch("FixedUpdate")]
    class FixDashBlinkDrift
    {
        private static Vector3 storedInput;
        private static void Prefix(Action06_Dash __instance, ref PlayerBinput ___Inp)
        {
            if (MasteryMod.DifficultyIsMastery())
            {
                storedInput = ___Inp.MoveInput;
                ___Inp.MoveInput = Vector3.Normalize(__instance.MoveDir);
            }
        }
        private static void Postfix(Action06_Dash __instance, ref PlayerBinput ___Inp)
        {
            if (MasteryMod.DifficultyIsMastery())
            {
                ___Inp.MoveInput = storedInput;
            }
        }
    }
}
