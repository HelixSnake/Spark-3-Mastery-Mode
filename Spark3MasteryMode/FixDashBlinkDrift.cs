using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;

namespace HelixBugFix
{
    [HarmonyPatch(typeof(Action06_Dash))]
    [HarmonyPatch("FixedUpdate")]
    class FixDashBlinkDrift
    {
        private static Vector3 storedInput;
        private static void Prefix(Action06_Dash __instance, ref PlayerBinput ___Inp)
        {
            if (HelixFixMod.DifficultyIsNotMastery())
            {
                storedInput = ___Inp.MoveInput;
                ___Inp.MoveInput = Vector3.Normalize(__instance.MoveDir);
            }
        }
        private static void Postfix(Action06_Dash __instance, ref PlayerBinput ___Inp)
        {
            if (HelixFixMod.DifficultyIsNotMastery())
            {
                ___Inp.MoveInput = storedInput;
            }
        }
    }
}
