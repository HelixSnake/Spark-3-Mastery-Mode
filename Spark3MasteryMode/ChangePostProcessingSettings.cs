using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.PostProcessing;
using HarmonyLib;
using Rewired;

namespace MoreAggressivePostProcessing
{
    [HarmonyPatch(typeof(LevelProgressControl))]
    [HarmonyPatch("Start")]
    class GetPostProcessingSettings
    {
        public static BloomModel.Settings defaultBloomSettings;
        public static ColorGradingModel.Settings defaultColorGradingSettings;
        public static bool useNewSettings = false;
        public static BloomModel.Settings newBloomSettings;
        public static ColorGradingModel.Settings newColorGradingSettings;
        private static void Postfix(LevelProgressControl __instance)
        {
            if (__instance.Graphics.Profile.bloom.settings.bloom.threshold == 0.5f) return; // don't copy changed settings
            defaultBloomSettings = __instance.Graphics.Profile.bloom.settings;
            newBloomSettings = defaultBloomSettings;
            newBloomSettings.bloom.intensity = 0.1f;
            newBloomSettings.bloom.radius = 4;
            newBloomSettings.bloom.threshold = 0.5f;
            newBloomSettings.bloom.softKnee = 0.5f;

            defaultColorGradingSettings = __instance.Graphics.Profile.colorGrading.settings;
            newColorGradingSettings = defaultColorGradingSettings;
            newColorGradingSettings.tonemapping.tonemapper = UnityEngine.PostProcessing.ColorGradingModel.Tonemapper.Neutral;
            newColorGradingSettings.tonemapping.neutralBlackIn = 0.05f;
            newColorGradingSettings.tonemapping.neutralBlackOut = 0f;
            newColorGradingSettings.tonemapping.neutralWhiteIn = 10f;
            newColorGradingSettings.tonemapping.neutralWhiteOut = 10f;
            newColorGradingSettings.tonemapping.neutralWhiteLevel = 5f;
            newColorGradingSettings.tonemapping.neutralWhiteClip = 6f;

            useNewSettings = true;
            __instance.Graphics.Profile.bloom.settings = newBloomSettings;
            __instance.Graphics.Profile.colorGrading.settings = newColorGradingSettings;
            __instance.Graphics.Profile.colorGrading.OnValidate();

        }
    }
    [HarmonyPatch(typeof(LevelProgressControl))]
    [HarmonyPatch("Update")]
    class TogglePostProcessingSettings
    {
        private static void Postfix(LevelProgressControl __instance)
        {
            var inp = ReInput.players.GetPlayer(0);
            if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.F11))
            {
                GetPostProcessingSettings.useNewSettings = !GetPostProcessingSettings.useNewSettings;
                if (GetPostProcessingSettings.useNewSettings)
                {
                    __instance.Graphics.Profile.bloom.settings = GetPostProcessingSettings.newBloomSettings;
                    __instance.Graphics.Profile.colorGrading.settings = GetPostProcessingSettings.newColorGradingSettings;
                    __instance.Graphics.Profile.colorGrading.OnValidate();
                }
                else
                {
                    __instance.Graphics.Profile.bloom.settings = GetPostProcessingSettings.defaultBloomSettings;
                    __instance.Graphics.Profile.colorGrading.settings = GetPostProcessingSettings.defaultColorGradingSettings;
                    __instance.Graphics.Profile.colorGrading.OnValidate();
                }
            }
        }
    }
}