using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.PostProcessing;
using HarmonyLib;
using UnityEngine;
using Rewired;

namespace MoreAggressivePostProcessing
{


    [HarmonyPatch(typeof(GraphicsMenu))]
    [HarmonyPatch("Start")]
    class GetPostProcessingSettings
    {
        public static PostProcessingProfile profile = null;
        public static BloomModel.Settings defaultBloomSettings;
        public static ColorGradingModel.Settings defaultColorGradingSettings;
        public static bool useNewSettings = true;
        public static BloomModel.Settings newBloomSettings;
        public static ColorGradingModel.Settings newColorGradingSettings;
        public static Dictionary<int, float> NewThresholds = new Dictionary<int, float>()
        {
            {100, 2f},
            {101, 2f},
            {102, 2f},
            {103, 2f},
            {104, 2f},
            {105, 2f},

            {0, 1},
            {1, 0.8f},
            {2, 0.8f},
            {4, 0.8f},
            {9, 0.8f},

            {5, 0.8f},
            {11, 0.8f},
            {13, 1f},

            {14, 0.8f},
            {15, 0.8f},

            {23, 0.8f},

            {37, 0.8f},

            {34, 1f},

            {38, 1f},

            {26, 1f},
            {50, 1f},
            {52, 1.5f},

            {141, 0.8f},
            {143, 0.8f},
            {144, 0.8f},
            {145, 1.5f},
            {150, 0.8f},

            // split Final Utopia
            
            {91, 1f},
            {93, 1.5f},
        };
        public static void SetSettings()
        {
            if (profile == null) return;
            if (useNewSettings)
            {
                float threshold = 0.5f;
                if (NewThresholds.ContainsKey(Save.CurrentStageIndex))
                {
                    threshold = NewThresholds[Save.CurrentStageIndex];
                }
                var newBloomSettingsModified = newBloomSettings;
                newBloomSettingsModified.bloom.threshold = threshold;
                profile.bloom.settings = newBloomSettingsModified;
                if (Save.CurrentStageIndex == 145 || (Save.CurrentStageIndex >= 100 && Save.CurrentStageIndex <= 105)) // levels too bright for tonemapping
                    profile.colorGrading.settings = defaultColorGradingSettings;
                else
                    profile.colorGrading.settings = newColorGradingSettings;
                profile.colorGrading.OnValidate();
            }
            else
            {
                profile.bloom.settings = defaultBloomSettings;
                profile.colorGrading.settings = defaultColorGradingSettings;
                profile.colorGrading.OnValidate();
            }
        }
        private static void Postfix(GraphicsMenu __instance)
        {
            profile = __instance.Profile;
            if (__instance.Profile.bloom.settings.bloom.intensity == 0.1f)
            {
                SetSettings();
                return; // don't copy changed settings
            }
            defaultBloomSettings = __instance.Profile.bloom.settings;
            newBloomSettings = defaultBloomSettings;
            newBloomSettings.bloom.intensity = 0.1f;
            newBloomSettings.bloom.radius = 4;
            newBloomSettings.bloom.threshold = 0.5f;
            newBloomSettings.bloom.softKnee = 0.5f;

            defaultColorGradingSettings = __instance.Profile.colorGrading.settings;
            newColorGradingSettings = defaultColorGradingSettings;
            newColorGradingSettings.tonemapping.tonemapper = UnityEngine.PostProcessing.ColorGradingModel.Tonemapper.Neutral;
            newColorGradingSettings.tonemapping.neutralBlackIn = 0.05f;
            newColorGradingSettings.tonemapping.neutralBlackOut = 0f;
            newColorGradingSettings.tonemapping.neutralWhiteIn = 10f;
            newColorGradingSettings.tonemapping.neutralWhiteOut = 10f;
            newColorGradingSettings.tonemapping.neutralWhiteLevel = 5f;
            newColorGradingSettings.tonemapping.neutralWhiteClip = 6f;
            var newLumVsSat = newColorGradingSettings.curves.lumVSsat = new ColorGradingCurve(new AnimationCurve(), 0.5f, false, new Vector2(0, 1));
            newLumVsSat.curve.AddKey(new Keyframe(0, 0.5f));
            newLumVsSat.curve.AddKey(new Keyframe(0.4f, 0.5f));
            newLumVsSat.curve.AddKey(new Keyframe(1, 0f));

            SetSettings();
        }
    }
    [HarmonyPatch(typeof(PostProcessingBehaviour))]
    [HarmonyPatch("OnRenderImage")]
    class TogglePostProcessingSettings
    {
        private static void Postfix(PostProcessingBehaviour __instance)
        {
            if (__instance.profile == null) return;
            if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.F11))
            {
                GetPostProcessingSettings.useNewSettings = !GetPostProcessingSettings.useNewSettings;
                GetPostProcessingSettings.SetSettings();
            }
        }
    }
    [HarmonyPatch(typeof(GameProgressVariables))]
    [HarmonyPatch("Start")]
    class UpdateBloomThreshold
    {
        private static void Postfix()
        {
            GetPostProcessingSettings.SetSettings();
        }
    }
}