using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;

namespace Spark3MasteryMode
{
    [HarmonyPatch(typeof(PlayerHealthAndStats))]
    [HarmonyPatch("ComboManager")]
    class MoreAccurateComboMeter
    {
        private static float ang = 0;
        private static void Postfix(PlayerHealthAndStats __instance)
        {
            if (MasteryMod.DifficultyIsMastery())
            {
                if (PlayerHealthAndStats.Combo > 0.1f)
                {
                    __instance.ComboMultiplierInfo.gameObject.SetActive(true);
                }
                __instance.ComboMultiplierImage.gameObject.SetActive(false);
                GameObject comboMultiplierText = __instance.ComboMultiplierInfo.transform.Find("ComboMultiplierText")?.gameObject;
                if (comboMultiplierText == null)
                {
                    comboMultiplierText = GameObject.Instantiate(__instance.ComboMultiplierImage.gameObject, __instance.ComboMultiplierInfo.transform);
                    comboMultiplierText.SetActive(true);
                    comboMultiplierText.name = "ComboMultiplierText";
                    GameObject.Destroy(comboMultiplierText.GetComponent<Image>());
                }
                var comboText = comboMultiplierText.GetComponent<Text>();
                if (comboText == null)
                {
                    var text = comboMultiplierText.AddComponent<Text>();
                    if (text == null) return; // text will fail to be added at least once
                    text.supportRichText = true;
                    text.color = new Color(1f, 0.7f, 0);
                    text.font = Resources.FindObjectsOfTypeAll<Font>().First<Font>(x => x.name == "VIPNAGORGIALLA");
                    text.horizontalOverflow = HorizontalWrapMode.Overflow;
                    text.verticalOverflow = VerticalWrapMode.Overflow;
                    text.alignment = TextAnchor.MiddleCenter;
                    text.resizeTextForBestFit = true;
                    text.lineSpacing = 0.7f;
                    text.fontSize = 13;
                }
                else
                {
                    GameObject comboMultiplierTextShadow = __instance.ComboMultiplierInfo.transform.Find("ComboMultiplierTextShadow")?.gameObject;
                    if (comboMultiplierTextShadow == null)
                    {
                        comboMultiplierTextShadow = GameObject.Instantiate(comboMultiplierText, __instance.ComboMultiplierInfo.transform);
                        comboMultiplierTextShadow.SetActive(true);
                        comboMultiplierTextShadow.name = "ComboMultiplierTextShadow";
                        var shadowText = comboMultiplierTextShadow.GetComponent<Text>();
                        shadowText.color = new Color(0.5f, 0f, 0);
                        shadowText.fontStyle = FontStyle.Bold;
                        comboMultiplierTextShadow.transform.SetAsFirstSibling();
                    }
                    else
                    {
                        int actualComboMultXTen = Mathf.RoundToInt((PlayerHealthAndStats.Combo + 1) * PlayerHealthAndStats.ComboDamageMultiplier * 10);
                        decimal actualComboMult = actualComboMultXTen / 10.0m;
                        comboText.text = string.Format("x{0:0.0}\nDMG", actualComboMult);
                        comboMultiplierTextShadow.GetComponent<Text>().text = comboText.text;
                        ang += 2;
                        comboMultiplierTextShadow.GetComponent<RectTransform>().anchoredPosition = comboMultiplierText.GetComponent<RectTransform>().anchoredPosition = 
                            new Vector2(Mathf.Cos(ang), Mathf.Sin(ang)) * actualComboMultXTen * 0.03f;
                    }
                }
            }
        }
    }
}
