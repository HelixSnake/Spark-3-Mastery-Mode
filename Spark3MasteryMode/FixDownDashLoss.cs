using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;

namespace HelixBugFix
{
    [HarmonyPatch(typeof(Action08_SuperMoves))]
    [HarmonyPatch("SuperManagement")]
    class FixDownDashLoss
    {
        private static void Prefix(Action08_SuperMoves __instance, ref float ___count, ref bool ___DownDashAvailable)
        {
            if (HelixFixMod.DifficultyIsNotMastery())
            {

                if (__instance.AttackType >= 0)
                {
                    ___count += PlayerBhysics.TimeStep;
                    if (___count > 3)
                    {
                        ___DownDashAvailable = true;
                    }
                }
            }
        }
    }
}
