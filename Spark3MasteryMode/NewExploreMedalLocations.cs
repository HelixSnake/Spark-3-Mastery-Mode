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
        private static List<int> enabledMedalStages = new List<int> { 1, 2, 5, 6, 7 };
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

        // lost riviera:
        //5.0912 241.9356 -6387.575
        //431.6225 -67.4099 -5379.03
        //-819.8027 -143.2319 -1692.298
        //-457.368 421.1428 -1115.819
        //208.888 475.0196 2396.036
        //-487.1549 346.4791 1259.954
        //-1867.247 636.2703 1435.407
        //-3021.808 770.9678 1027.95
        //-4678.568 585.8435 8517.062
        //-3678.162 1049.313 9835.422

        //lost ravine
        //296.8832 362.6606 -1733.448
        //-230.2561 479.9605 -208.294
        //2445.923 584.4319 -3618.033
        //2493.374 737.8845 -728.7992
        //2578.111 633.363 -122.5204

        //1 2 4 6 7

        public static Dictionary<(int, int), Vector3> NewMedalLocations = new Dictionary<(int, int), UnityEngine.Vector3> {
            {(1, 0), new Vector3(183.2969f, 448.6375f, -2602.669f) },
            {(1, 2), new Vector3(300.5526f, 478.2237f, -2195.653f) },
            {(1, 8), new Vector3(-359.947f, 772.5374f, 2340.729f) },

            {(5, 0), new Vector3(5.0912f, 244, -6387.575f) },
            {(5, 1), new Vector3(431.6225f, -67.4099f, -5379.03f) },
            {(5, 2), new Vector3(-815, -133, -1690) },
            {(5, 3), new Vector3(-457.368f, 425, -1115.819f) },
            {(5, 4), new Vector3(208.888f, 475.0196f, 2396.036f) },
            {(5, 5), new Vector3(-487.1549f, 350f, 1259.954f) },
            {(5, 6), new Vector3(-1867.247f, 640f, 1435.407f) },
            {(5, 7), new Vector3(-3021.808f, 774, 1027.95f) },
            {(5, 8), new Vector3(-4678.568f, 590, 8517.062f) },
            {(5, 9), new Vector3(-3665.879f, 1038.413f, 9716.465f) },

            {(6, 1), new Vector3(296.8832f, 367.6606f, -1733.448f) },
            {(6, 2), new Vector3(-230.2561f, 486.9605f, -208.294f) },
            {(6, 4), new Vector3(2445.923f, 589.4319f, -3618.033f) },
            {(6, 6), new Vector3(2493.374f, 742.8845f, -728.7992f) },
            {(6, 7), new Vector3(2578.111f, 638.363f, -122.5204f) },
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