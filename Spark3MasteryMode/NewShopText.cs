using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;

namespace Spark3MasteryMode
{
    [HarmonyPatch(typeof(ShopaloBubbleTrigger))]
    [HarmonyPatch("Start")]
    class NewShopMapText
    {
        private static void Postfix(ShopaloBubbleTrigger __instance)
        {
            if (MasteryMod.DifficultyIsMastery())
            {
                __instance.transform.GetChild(0).GetChild(0).GetComponent<UnityEngine.TextMesh>().text = "IT'S CHRISTMAS IN JULY";
            }
        }
    }

    [HarmonyPatch(typeof(ShopaloShop))]
    [HarmonyPatch("Start")]
    class NewShopMenuText
    {
        private static void Prefix(ShopaloShop __instance)
        {
            if (MasteryMod.DifficultyIsMastery())
            {
                __instance.InitialText = "CHECK ITEM DESCRIPTIONS FOR HINTS. All medal targets are found and set WITHOUT magnet dashing - while it is not explicitly disabled, if you want the best challenge you should avoid using it.";
                __instance.MainPageItens[0].Description = "Some of these may actually reduce your DPS or combo gain by adding more low damage attacks or repeated hitboxes to your strings. Remember that you can disable or enable them whenever you want.";
                __instance.MainPageItens[1].Description = "I had to selectively ban some of these items due to being overpowered or not positively affecting the engagement with the level design or enemies. Apologies.";
                __instance.MainPageItens[2].Description = "These are your most important tools. They're all of critical importance for meeting the diamond targets. Get them all.";
                __instance.MainPageItens[3].Description = "These are all unlocked by default. Get your characters now!";
                __instance.MoveItens[0].Description = "This move does a suprising amount of damage with a full combo. Getting into position and charging it is a good thing to do in between enemy waves or Throwback's boss transitions.";
                __instance.UpgradeItens[0].Description = "Bosses can only regenerate their armor when they're on the ground - it is in your best interest to keep them in the air as long as possible when you have an opening. If you have to stop attacking a boss to avoid an unparryable move, keep the combo going with shots.";
                __instance.UpgradeItens[1].Description = "This is going to be one of your best reasons to get energy in levels that have rails. Make sure to kill enemies when you can using Spark's gun, as he can use it without slowing down. Also, remember that you can crouch even when boosting.";
                __instance.UpgradeItens[2].Description = "Best used with quick taps when you're going REALLY REALLY fast to get bursts of energy. Can be used in conjunction with boosting to keep from decelerating on rails when low on energy. Don't use while going downhill since you can't brake while crouching.";
                __instance.UpgradeItens[3].Description = "When swiping certain oddly shaped clusters of bits, you will get different speeds and directions based on the distance, angle and height you press the swipe button. In addition, jumping after swiping will often give you more speed. Swiping can also teleport you up or sideways to the bits without losing speed.";
                __instance.SpecialItens[0].Description = "It is always faster to do car segments on foot when you can. On Alpine Carrera, you should boost by repeatedly tapping the boost button instead of holding it. Try to space your taps so you just barely use all of it up, and save it when there's a boost pad.";
                __instance.SpecialItens[1].Description = "This is going to be your biggest time saver on stages with combat. Make sure to make good use of this.";
                __instance.SpecialItens[2].Description = "It was a hard decision, but I can't have you just skipping stages through super magnet dashes, and this power is just generally way too good.";
                __instance.SpecialItens[3].Description = "The UI for your combo multiplier is misleading - a full combo bar is actually 4x damage. Make sure to keep it full as much as possible.";
                __instance.JesterItens[0].Description = "Reaper Jester's parry has a second hit that counts as a second parry - when parrying single red attacks, you can use this to break an enemy or boss's armor faster.";
                __instance.JesterItens[1].Description = "In addition to Float's platforming abilities, she also has a faster homing attack. Float's floating ability is not just useful for making long or precise jumps, but also for climbing with walljumps.";
                __instance.JesterItens[2].Description = "Fark's static mode may be nerfed but his double air dash with invulnerability, great DPS and ability to heal will make him a valuable tool. His air dash has more iframes than it looks and is great for dodging boss AOE attacks, even Clarity Centralis's long lasting one.";
                __instance.JesterItens[3].Description = "Sfarx's acceleration and ability to maintain speed are second to none. You should generally be using him to go fast unless you need the ability of another character, or Spark's ability to shoot while moving. His DPS is also great without Fark's poor range.";
            }
        }
    }

}
