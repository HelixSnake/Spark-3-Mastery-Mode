using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
namespace Spark3MasteryMode
{
    [HarmonyPatch(typeof(HystoriaBossFight))]
    [HarmonyPatch("SetAllobjects")]
    class DisableEJElectricity
    {
        private static void Postfix(HystoriaBossFight __instance, ref int ___boss)
        {
            if (MasteryMod.DifficultyIsMastery())
            {
                if (___boss != 0)
                {
                    var enemyAttack = __instance.EJ.GetComponent<EnemyAttacks>();
                    enemyAttack.IntegratedHitBox[8].transform.GetChild(1).GetComponent<DeactivateAfterAWhile>().DeactivateEvent();
                }
            }
        }
    }
}
