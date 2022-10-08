using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;

namespace Spark3MasteryMode
{
    [HarmonyPatch(typeof(WorldMedal))]
    [HarmonyPatch("SetExploreMedal")]
    class MakeExploreMedalsUncollectable
    {
        private static List<int> enabledMedalStages = new List<int> { };
        private static bool Prefix()
        {
            if (MasteryMod.DifficultyIsMastery())
            {
                if (!enabledMedalStages.Contains(Save.CurrentStageIndex)) return false;
            }
            return true;
        }
    }
    [HarmonyPatch(typeof(WorldMedal))]
    [HarmonyPatch("Start")]
    class MoveExploreMedals
    {
        public static Dictionary<(int, int), UnityEngine.Vector3> NewMedalLocations = new Dictionary<(int, int), UnityEngine.Vector3> {

        };
        private static void Prefix(WorldMedal __instance)
        {
            if (MasteryMod.DifficultyIsMastery())
            {
                if (NewMedalLocations.ContainsKey((Save.CurrentStageIndex, __instance.MedalId)))
                {
                    __instance.transform.position = NewMedalLocations[(Save.CurrentStageIndex, __instance.MedalId)];
                }
            }
        }
    }
}