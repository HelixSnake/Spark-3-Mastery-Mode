using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;

namespace MoreAggressivePostProcessing
{
    [HarmonyPatch(typeof(GraphicsMenu))]
    [HarmonyPatch("Start")]
    class ChangePostProcessingSettings
    {
        private static void Prefix(GraphicsMenu __instance)
        {
            var bloomSettings = __instance.Profile.bloom.settings;
            bloomSettings.bloom.intensity = 0.3f;
            bloomSettings.bloom.radius = 4;
            bloomSettings.bloom.threshold = 1;
            bloomSettings.bloom.softKnee = 1;
            __instance.Profile.bloom.settings = bloomSettings;

            var colorGradingSettings = __instance.Profile.colorGrading.settings;
            colorGradingSettings.tonemapping.tonemapper = UnityEngine.PostProcessing.ColorGradingModel.Tonemapper.Neutral;
            colorGradingSettings.tonemapping.neutralBlackIn = 0.02f;
            colorGradingSettings.tonemapping.neutralBlackOut = 0f;
            colorGradingSettings.tonemapping.neutralWhiteIn = 8f;
            colorGradingSettings.tonemapping.neutralWhiteOut = 8f;
            colorGradingSettings.tonemapping.neutralWhiteLevel = 8f;
            colorGradingSettings.tonemapping.neutralWhiteClip = 8f;
        }
    }
}