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
        private static List<int> enabledMedalStages = new List<int> { 1, 2, 5, 6, 7, 8, 14, 15, 17 };
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
        //208.888 475.0196 2396.036
        //-487.1549 346.4791 1259.954
        //-1181.892 567.2075 1332.291
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
            {(5, 3), new Vector3(208.888f, 475.0196f, 2396.036f) },
            {(5, 4), new Vector3(-487.1549f, 350f, 1259.954f) },
            {(5, 5), new Vector3(-1181.89f, 572.2075f, 1332.291f) },
            {(5, 6), new Vector3(-1867.247f, 640f, 1435.407f) },
            {(5, 7), new Vector3(-3021.808f, 774, 1027.95f) },
            {(5, 8), new Vector3(-4678.568f, 590, 8517.062f) },
            {(5, 9), new Vector3(-3665.879f, 1038.413f, 9716.465f) },

            {(6, 1), new Vector3(296.8832f, 367.6606f, -1733.448f) },
            {(6, 2), new Vector3(-230.2561f, 486.9605f, -208.294f) },
            {(6, 4), new Vector3(2445.923f, 589.4319f, -3618.033f) },
            {(6, 6), new Vector3(2493.374f, 742.8845f, -728.7992f) },
            {(6, 7), new Vector3(2578.111f, 638.363f, -122.5204f) },

            {(8, 0), new Vector3(425.8514f, 630.6449f, -3737.659f) },
            {(8, 1), new Vector3(-72.3306f, 397.9791f, 3668.934f) },
            {(8, 2), new Vector3(-250.2478f, 630.441f, 4566.729f) },
            {(8, 3), new Vector3(301.5574f, 286.723f, 5973.084f) },
            {(8, 4), new Vector3(-303.5384f, 486.7621f, 5972f) },
            {(8, 5), new Vector3(-295.6239f, 773.0584f, 6582.098f) },
            {(8, 6), new Vector3(1131.825f, 722.7222f, 2548.14f) },
            {(8, 7), new Vector3(1502.683f, 708.3549f, 2604.034f) },
            {(8, 8), new Vector3(1370.909f, 763.8295f, 796.1392f) },
            {(8, 9), new Vector3(1606.545f, 739.7922f, -5502.138f) },

            {(14, 3), new Vector3(61.5591f, 25.4092f, 851.7642f) },
            {(14, 5), new Vector3(151.2248f, 301.9377f, 960.3292f) },
            {(14, 9), new Vector3(163.3537f, 407.7137f, 1013.404f) },

            {(15, 0), new Vector3(-200.393f, 220.1582f, 2753.306f) },
            {(15, 1), new Vector3(-190.2955f, 203.3545f, 3677.852f) },
            {(15, 2), new Vector3(-2671.333f, 119.5486f, 4874.401f) },
            {(15, 3), new Vector3(-2538.541f, 408.8451f, 5630.845f) },
            {(15, 4), new Vector3(-2341.339f, 408.4889f, 5313.162f) },
            {(15, 5), new Vector3(91.401f, 310.3247f, 6291.083f) },
            {(15, 6), new Vector3(-534.1182f, 333.0716f, 7155.086f) },
            {(15, 7), new Vector3(-4242.333f, 796.6566f, 5337.876f) },
            {(15, 8), new Vector3(-6312.762f, 932.7791f, 2681.7f) },
            {(15, 9), new Vector3(-6314.963f, 794.8566f, -1175.596f) },

            {(17, 0), new Vector3(17.1695f, 176.1187f, 436.3261f) },
            {(17, 1), new Vector3(77.0203f, -13.6541f, 726.903f) },
            {(17, 2), new Vector3(306.8456f, 218.0575f, 909.246f) },
            {(17, 3), new Vector3(1145.292f, 154.4202f, 663.5221f) },
            {(17, 4), new Vector3(1481.67f, 185.966f, 1001.992f) },
            {(17, 5), new Vector3(1493.069f, 178.8751f, 1517.701f) },
            {(17, 6), new Vector3(336.2091f, 204.4428f, 1959.539f) },
            {(17, 7), new Vector3(-616.312f, 39.7034f, 1962.721f) },
            {(17, 8), new Vector3(-1284.084f, 219.035f, 1884.625f) },
            {(17, 9), new Vector3(-1359.787f, 56.3276f, 1838.8f) },
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