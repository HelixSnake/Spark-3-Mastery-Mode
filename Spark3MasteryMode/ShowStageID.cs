using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using MelonLoader;

[HarmonyPatch(typeof(WorldMapCursor))]
[HarmonyPatch("MakeStageDetailsList")]
class ShowStageID
{
    private static void Prefix()
    {
    }
    private static void Postfix(WorldMapCursor __instance)
    {
        foreach (StageData data in WorldMapCursor.StageDetailsList)
        {
            data.StageName += " [" + data.ID + "]";
        }
    }
}