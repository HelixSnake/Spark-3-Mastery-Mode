using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;

namespace Spark3UnFixChargeTap
{
    [HarmonyPatch(typeof(Action08_SuperMoves))]
    [HarmonyPatch("SuperManagement")]
    class UnSetJesterPower
    {
        private static void Prefix(Action08_SuperMoves __instance, ref float __state)
        {
            __state = __instance.JesterPower;
        }
        private static void Postfix(Action08_SuperMoves __instance, ref float __state, ref PlayerBinput ___Inp)
        {
            if (___Inp.Rewinp.GetAxis("Special") > 0.8f)
            {
                __instance.JesterPower = __state;
            }
        }
    }
}
