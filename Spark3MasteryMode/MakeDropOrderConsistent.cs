using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;

namespace Spark3MasteryMode
{
    [HarmonyPatch(typeof(ItenDropBuffsControl))]
    [HarmonyPatch("Start")]
    class MakeDropOrderConsistent
    {
        static private void Prefix()
        {
            if (MasteryMod.DifficultyIsMastery())
            {
                ItenDropBuffsControl.ItenDropIndex = 0;
            }
        }
    }
}
