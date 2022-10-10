using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;

namespace Spark3MasteryMode
{
    [HarmonyPatch(typeof(WorldMedal))]
    [HarmonyPatch("SetExploreMedal")]
    class MakeExploreMedalsUncollectable
    {
        private static List<int> enabledMedalStages = new List<int> { 1, 2 };
        private static bool Prefix()
        {
            if (MasteryMod.DifficultyIsMastery())
            {
                if (!enabledMedalStages.Contains(Save.CurrentStageIndex)) return false;
            }
            return true;
        }
    }
    [HarmonyPatch(typeof(ActivateOnDistance))]
    [HarmonyPatch("Start")]
    class ActivateExploreMedals
    {
        // needed to make sure the score medals move; their start methods won't run if this deactivates them first
        private static bool Prefix(ActivateOnDistance __instance)
        {
            if (MasteryMod.DifficultyIsMastery())
            {
                WorldMedal temp;
                if (__instance.TryGetComponent<WorldMedal>(out temp))
                {
                    __instance.gameObject.SetActive(true);
                    return false;
                }
            }
            return true;
        }
    }
    [HarmonyPatch(typeof(WorldMedal))]
    [HarmonyPatch("Start")]
    class MoveExploreMedals
    {
        // double moon villa:
        //183.2969 443.6375 -2602.669
        //300.5526 473.2237 -2195.653
        //-359.947 772.5374 2340.729

            //0 2 8

        public static Dictionary<(int, int), Vector3> NewMedalLocations = new Dictionary<(int, int), UnityEngine.Vector3> {
            {(1, 0), new Vector3(183.2969f, 448.6375f, -2602.669f) },
            {(1, 2), new Vector3(300.5526f, 478.2237f, -2195.653f) },
            {(1, 8), new Vector3(-359.947f, 772.5374f, 2340.729f) },
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