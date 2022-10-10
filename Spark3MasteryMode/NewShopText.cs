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
                __instance.InitialText = "CHECK ITEM DESCRIPTIONS FOR HINTS. All medal targets are found and set WITHOUT magnet dashing (dashing during a homing attack for a big speed boost) - while it is not explicitly disabled, if you want the best challenge you should avoid using it, and focus on other mechanics to build and maintain speed.";
                
                __instance.MainPageItens[0].Description = "Some of these may actually reduce your DPS or combo gain by adding more low damage attacks or repeated hitboxes to your strings. Remember that you can disable or enable them whenever you want.";
                __instance.MainPageItens[1].Description = "I had to selectively ban some of these items due to being overpowered or not positively affecting the engagement with the level design or enemies. Apologies.";
                __instance.MainPageItens[2].Description = "Often it is a good idea to skip boost pads - if you're already going really fast, they can actually slow you down!";
                __instance.MainPageItens[3].Description = "These are all unlocked by default. Get your characters now!";
                
                __instance.MoveItens[0].Description = "This move does a suprising amount of damage with a full combo. Getting into position and charging it is a good thing to do in between enemy waves or Throwback's boss transitions.";
                __instance.MoveItens[1].Description = "Keep an eye out for ways to increase your DPS in fights. While Float may be weak in combat, her Scary Face passive ability is super important for extra DPS and energy gain. Sfarx's charge shot is another move that is great for increasing DPS and energy gain.";
                __instance.MoveItens[2].Description = "You can charge Spark and Sfarx's gun by holding the charge shot button while attacking, and even with other characters - the charge will be maintained when switching characters. Make good use of this!";
                __instance.MoveItens[3].Description = "You can store the hitbox of any attack by switching characters when it is active. This works best with lingering hitboxes like Fark's light attack combo ender and Spark's air heavy attack combo ender. When switching back to the character, the hitbox will reactivate, and you can attack while it is active, increasing your DPS.";

                __instance.UpgradeItens[0].Description = "Bosses can only regenerate their armor when they're on the ground - it is in your best interest to keep them in the air as long as possible when you have an opening. If you have to stop attacking a boss to avoid an unparryable move, keep the combo going with shots.";
                __instance.UpgradeItens[1].Description = "This is going to be one of your best reasons to get energy in levels that have rails. Make sure to kill enemies when you can using Spark's gun, as he can use it without slowing down. Also, remember that you can crouch even when boosting.";
                __instance.UpgradeItens[2].Description = "Best used with quick taps when you're going REALLY REALLY fast to get bursts of energy. Can be used in conjunction with boosting to keep from decelerating on rails while either maintaining or even gaining energy. The faster you're going, the more energy you get and the less you slow down.";
                __instance.UpgradeItens[3].Description = "When swiping certain oddly shaped clusters of bits, you will get different speeds and directions based on the distance, angle and height you press the swipe button. In addition, jumping after swiping will often give you more speed. Swiping can also teleport you up or sideways to the bits without losing speed.";
                
                __instance.SpecialItens[0].Description = "It is always faster to do car segments on foot when you can. On Alpine Carrera, you should boost by repeatedly quickly tapping the boost button instead of holding it. Try to space your taps so you just barely use all of it up, and save it when there's a boost pad.";
                __instance.SpecialItens[1].Description = "This is going to be your biggest time saver on stages with combat. Make sure to make good use of this. For score medals, you can make use of energy gained elsewhere, quickly build a combo to around 60-70%, and then use this to take tough enemies out quickly for lots of points.";
                __instance.SpecialItens[2].Description = "It was a hard decision, but I can't have you just skipping stages through super magnet dashes, and this power is just generally way too good.";
                __instance.SpecialItens[3].Description = "The UI for your combo multiplier is misleading - a full combo bar is actually 4x damage. Make sure to keep it full as much as possible.";
                __instance.SpecialItens[4].Description = "I've modified this to use less energy. Use it to keep your juggles going!";
                __instance.SpecialItens[5].Description = "Remember, the things that give you a larger amount of score give you more multiplier as well. Try to focus just on the things that give you the most score when going for score medals. Score capsules, tough enemies, and large groups of breakables are good candidates.";
                
                __instance.JesterItens[0].Description = "Reaper Jester's parry has a second hit that counts as a second parry - when parrying single red attacks, you can use this to break an enemy or boss's armor faster. However, Reaper's low DPS might not justify using him outside of fighting groups.";
                __instance.JesterItens[1].Description = "In addition to Float's platforming abilities, she also has a faster homing attack - groups of enemies that are close enough together can allow float to get through them very quickly by mashing the homing attack button. Since histun pauses the timer, dashing through enemies this way is borderline instant.";
                __instance.JesterItens[2].Description = "Fark's static mode's ability to get quick speed, his double air dash with invulnerability, great DPS and ability to heal will make him a valuable tool. His air dash has more iframes than it looks and is great for dodging boss AOE attacks, even Clarity Centralis's long lasting one.";
                __instance.JesterItens[3].Description = "Sfarx's acceleration and ability to maintain speed are second to none. You should generally be using him to go fast unless you need the ability of another character, or Spark's ability to shoot while moving. His DPS is also great without Fark's poor range and his charge shot is a fantastic way to get extra damage.";
            }
        }
    }

}
