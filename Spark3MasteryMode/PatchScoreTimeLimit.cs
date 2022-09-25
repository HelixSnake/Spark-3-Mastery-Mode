using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using MelonLoader;

[HarmonyPatch(typeof(ScoreManager))]
[HarmonyPatch("Start")]
class PatchScoreTimeLimit
{
    private static void Prefix()
    {
            
    }
    private static void Postfix(ref float ___TimeLimitStatic)
    {
        ScoreManager.IgnoreLimit = false;
        ___TimeLimitStatic = 300f;
    }
}
