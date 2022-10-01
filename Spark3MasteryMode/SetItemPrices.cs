using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
namespace Spark3MasteryMode
{

    [HarmonyPatch(typeof(ShopaloShop))]
    [HarmonyPatch("Start")]
    class SetItemPrices
    {
        private static void Prefix(ShopaloShop __instance)
        {
            if (MasteryMod.DifficultyIsMastery())
            {
                foreach (var item in __instance.MainPageItens)
                {
                    item.BitsCost = 1;
                }
                foreach (var item in __instance.JesterItens)
                {
                    item.BitsCost = 1;
                }
                for (int i = 0; i < __instance.SpecialItens.Length; i++)
                {
                    if (i == 0 || i == 2 || i == 3)
                        __instance.SpecialItens[i].BitsCost = int.MaxValue;
                    else
                        __instance.SpecialItens[i].BitsCost = 1;

                }
                foreach (var item in __instance.MoveItens)
                {
                    item.BitsCost = 1;
                }
                foreach (var item in __instance.UpgradeItens)
                {
                    item.BitsCost = 1;
                }
            }
        }
    }

    [HarmonyPatch(typeof(ShopItenDetails))]
    [HarmonyPatch("CheckUnlocks")]
    class SetItemsUnlocked
    {
        private static bool Prefix()
        {
            if (MasteryMod.DifficultyIsMastery())
            {
                return false;
            }
            return true;
        }
    }
}