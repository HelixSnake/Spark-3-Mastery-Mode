using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;

namespace Spark3MasteryMode
{
    [HarmonyPatch(typeof(Action_12_Block))]
    [HarmonyPatch("BlockCheck")]
    class ReaperParryExtraInvuln
    {
        private static void Postfix(Action_12_Block __instance, ref bool ___Perfect)
        {
            if (MasteryMod.DifficultyIsMastery())
            {
                if (___Perfect && CharacterAnimatorChange.Character == 1)
                    __instance.Obj.InvTimer = 1.5f;
            }
        }
    }
    [HarmonyPatch(typeof(Action06_Dash))]
    [HarmonyPatch("OnEnable")]
    class DashExtraInvuln
    {
        private static void Postfix(Action06_Dash __instance, ref ActionManager ___Actions)
        {
            if (MasteryMod.DifficultyIsMastery())
            {
                ___Actions.Obj.InvTimer = Math.Max(___Actions.Obj.InvTimer, 0.12f);
            }
        }
    }
}