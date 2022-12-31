using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;

namespace Spark3MasteryMode
{

    [HarmonyPatch(typeof(Action08_SuperMoves))]
    [HarmonyPatch("JesterDashManagement")]
    class FixChargeTapping
    {
        private static void Prefix(Action08_SuperMoves __instance, ref float ___count, ref PlayerBinput ___Inp, ref PlayerBhysics ___Player, ref float ___JesterPower)
        {
            if (MasteryMod.DifficultyIsMastery())
            {
                if (___count < 3)
                {
                    if (___Inp.Rewinp.GetAxis("Special") < 0.4f && InputDevice.CurrentControllerType != InputDevice.Device.Keyboard)
                    {
                        ___Player.rigid.velocity = __instance.transform.TransformDirection(___Inp.MoveInput) * 0.1f;
                    }
                    if (___Inp.Rewinp.GetButtonUp("JesterDash"))
                    {
                        ___Player.rigid.velocity = __instance.transform.TransformDirection(___Inp.MoveInput) * 0.1f;
                    }
                    if (___Inp.Rewinp.GetButtonUp("ChargedJesterDash"))
                    {
                        ___Player.rigid.velocity = __instance.transform.TransformDirection(___Inp.MoveInput) * 0.1f;
                    }
                }
            }
        }
    }
}
