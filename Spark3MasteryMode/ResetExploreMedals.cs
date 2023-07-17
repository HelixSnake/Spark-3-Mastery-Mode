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
            Save.GetCurrentSave().ExploreMedal0[SceneController.LastLoadedLevel] = false;
            Save.GetCurrentSave().ExploreMedal1[SceneController.LastLoadedLevel] = false;
            Save.GetCurrentSave().ExploreMedal2[SceneController.LastLoadedLevel] = false;
            Save.GetCurrentSave().ExploreMedal3[SceneController.LastLoadedLevel] = false;
            Save.GetCurrentSave().ExploreMedal4[SceneController.LastLoadedLevel] = false;
            Save.GetCurrentSave().ExploreMedal5[SceneController.LastLoadedLevel] = false;
            Save.GetCurrentSave().ExploreMedal6[SceneController.LastLoadedLevel] = false;
            Save.GetCurrentSave().ExploreMedal7[SceneController.LastLoadedLevel] = false;
            Save.GetCurrentSave().ExploreMedal8[SceneController.LastLoadedLevel] = false;
            Save.GetCurrentSave().ExploreMedal9[SceneController.LastLoadedLevel] = false;
        }
        private static void Postfix()
        {
        }
    }
}