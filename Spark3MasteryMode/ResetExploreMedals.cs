using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using MelonLoader;

namespace Spark3ResetExploreMedalsMod
{

    [HarmonyPatch(typeof(PauseCotrol))]
    [HarmonyPatch("RestartStage")]
    class ResetExploreMedals
    {
        private static void Prefix()
        {
            Save.GetCurrentSave().ExploreMedal0[Save.CurrentStageIndex] = false;
            Save.GetCurrentSave().ExploreMedal1[Save.CurrentStageIndex] = false;
            Save.GetCurrentSave().ExploreMedal2[Save.CurrentStageIndex] = false;
            Save.GetCurrentSave().ExploreMedal3[Save.CurrentStageIndex] = false;
            Save.GetCurrentSave().ExploreMedal4[Save.CurrentStageIndex] = false;
            Save.GetCurrentSave().ExploreMedal5[Save.CurrentStageIndex] = false;
            Save.GetCurrentSave().ExploreMedal6[Save.CurrentStageIndex] = false;
            Save.GetCurrentSave().ExploreMedal7[Save.CurrentStageIndex] = false;
            Save.GetCurrentSave().ExploreMedal8[Save.CurrentStageIndex] = false;
            Save.GetCurrentSave().ExploreMedal9[Save.CurrentStageIndex] = false;
        }
        private static void Postfix()
        {
        }
    }
}