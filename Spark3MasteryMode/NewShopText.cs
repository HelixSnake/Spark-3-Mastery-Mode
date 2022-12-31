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
    class ConstructInformationMenu
    {
        public static List<(string, string)> TipsList = new List<(string, string)>()
        {
            {("Changes:\nEnemies", "Enemies have not only been given <color=red>more health and armor</color>, but made <color=red>more aggressive</color>. Health and armor levels are " +
                "about <color=red>3</color> times what they are in Hardcore.") },
            
            {("Changes:\nDifficulty", "While you have as much health as on <color=red>Hardcore difficulty</color>, your <color=red>parry timing</color> is that of <color=red>Challenge Jester.</color> " +
                "Make sure you learn to time it well, and be careful of the <color=red>cooldown</color> after releasing block!") },
            
            {("Changes:\nStatic Mode", "<color=cyan>Static mode</color> now <color=red>gains less energy</color> while it's active, to prevent it from lasting too long.")},
            
            {("Changes:\nFloat's Charge\nShot", "<color=Violet>Float</color> now has a <color=Green>charge shot.</color> Experiment with it!")},
            
            {("Changes:\nBlink", "Your teleport special power, <color=magenta>Blink</color>, now takes <color=green>15 energy instead of 25.</color> In some fights you might want to make good " +
                "use of it to <color=magenta>keep up your juggle,</color> since you might not be able to keep the aggression up as well when the enemy is attacking!") },
            
            {("Changes:\nMagnet Dash", "A <color=cyan>Magnet Dash</color> is a dash right after a <color=cyan>homing attack. " +
                "Magnet Dash</color> has been made <color=red>less overpowered,</color> but <color=green>more consistent. If you have speed,</color> it will generally " +
                "give you the <color=green>same amount of extra speed,</color> regardless of vertical angle. You still should try to magent dash in the enemy's direction for the most speed.") },
            
            {("Changes:\nNew Combo UI", "The <color=orange>Combo Meter</color> has been given a new UI for the <color=orange>multiplier.</color> The way the combo multiplier works <color=orange>hasn't changed</color> - but the new number on the multiplier is <color=green>now correct,</color> compared to the <color=red>incorrect</color> old UI.") },

            
            {("Combat Hint:\nCombo Meter", "Your <color=orange>combo meter</color> goes from <color=green>1x to 4x.</color> Always keep it <color=green>as full as possible,</color> " +
                "otherwise you will do <color=red>pitiful damage,</color> especially in Mastery Mode. If you have to make space from the enemy, <color=cyan>keep the meter up with " +
                "shots.</color>")},
            
            {("Combat Hint:\nJuggling", "After you break a <color=red>boss's armor,</color> the amount of time they stay open is proportionate to the amount of time they " +
                "spend <color=red>on the ground.</color> Make sure to <color=green>keep your juggle going as long as possible</color> while they're open. Remember, Spark can land during juggles, " +
                "and get his <color=cyan>jump / dashes</color> back.")},
            
            {("Combat Hint:\nHeavy Attacks", "<color=yellow>Heavy Attack Combos</color> do <color=green>more DPS</color> than Light Attack Combos. If you're having trouble hitting the diamond speed medals for combat stages, try getting in the habit of <color=green>using Heavy Attack combos more often.</color> " +
                "A notable light attack combo that is still very useful is <color=orange>Fark's grounded light attack combo</color> (see the <color=cyan>'Storing Attacks'</color> tip)")},
            
            {("Combat Hint:\nHyper Surge", "<color=yellow>Hyper Surge</color> is going to be your biggest time saver on stages with combat. Make sure to make good use of this.")},
            
            {("Combat Hint:\nCharged Kick", "<color=yellow>Charged Kick</color> does a suprising amount of <color=green>damage</color> with a full <color=orange>combo meter.</color> "+
                "Getting into position and <color=yellow>charging it</color> is a good thing to do in between enemy waves or Throwback's boss transitions.")},
            
            {("Combat Hint:\nScary Face", "<color=purple>Float</color> may be limited in combat, but her <color=red>Scary Face</color> passive does a <color=green>lot of free damage</color> even when not " +
                "attacking with her. Keeping her in your party even for combat stages is a good idea.")},

            {("Combat Hint:\nCharge Shot", "You can <color=cyan>charge your shot</color> with any character, and the charge will <color=cyan>maintain between character switches.</color> " +
                "Keep your shots charged and unleash them with <color=yellow>Spark</color> or <color=cyan>Sfarx.</color> Sfarx's charge shot gives you a lot of <color=cyan>energy</color> and <color=orange>combo</color> and does a lot " +
                "of <color=green>damage,</color> so use it when you can.")},
            
            {("Combat Hint:\nStoring Attacks", "You can store the <color=yellow>hitbox</color> of any attack by <color=yellow>switching characters when it is active.</color> This works best with " +
                "lingering hitboxes like <color=orange>Fark's light attack combo ender</color> and <color=yellow>Spark's air heavy attack combo ender.</color> When switching back to the character, " +
                "the hitbox will <color=yellow>reactivate,</color> and you can attack while it is active, increasing your DPS.")},
            
            {("Combat Hint:\nDash I-Frames", "<color=orange>Fark's dash</color> has <color=orange>considerably more i-frames</color> than it looks. Even for big, <color=red>long lasting AOE attacks,</color> " +
                "Fark can use <color=orange>multiple dashes</color> to <color=green>avoid damage</color> completely without having to back off.")},
            
            {("Combat Hint:\nHigher\nDouble Jump", "If you <color=cyan>air dash and then immediately jump,</color> you will get the <color=green>amount of height from your grounded jump</color> instead of your double jump. " +
                "Use this to more effectively <color=green>continue the combo</color> against enemies you've air launched with <color=orange>Fark</color> or <color=cyan>Sfarx.</color>")},
            
            {("Combat Hint:\nDash Cancel", "Almost <color=green>any action</color> can be canceled into a <color=cyan>dash or air dash.</color> Use it to cancel the <color=red>end lag</color> from various actions from parries to attacks.")},
            
            {("Combat Hint:\nCancelling\nEnemy Attacks", "In exchange for some <color=cyan>energy</color> you can <color=green>cancel enemy attack animations</color> - simply <color=purple>blink</color> to them and immediately <color=cyan>shoot</color> them once.")},
            
            {("Combat Hint:\nSkipping Combat", "While getting good at combat is useful, you also want to <color=green>skip combat arenas</color> as much as possible. " +
                "A lot of arenas can have their activation box <color=green>maneuvered around,</color> and some can be <color=green>escaped after activating.</color> " +
                "Remember that <color=yellow>jester swipes</color>  and <color=cyan>homing attacks</color> can <color=green>take you through walls!</color> ")},

            {("Speed Hint:\nExperimentation", "When you are testing your run and experimenting with the various mechanics, remember the <color=magenta>Radar</color> has a " +
                "<color=green>speed indicator</color> on it - use it to make sure you're doing the <color=green>right things</color> to gain and maintain speed!")},

            {("Speed Hint:\nCharged Jester\nDash Cap", "Charged Jester Dash reaches <color=yellow>max charge</color> earlier than you think - around <color=yellow>halfway through the audio cue.</color> Practice doing it with the <color=magenta>Radar</color> to get a feel for when to release it!")},

            {("Speed Hint:\nSfarx", "While other characters have top speeds which they can not get past without downward slopes or other speed gaining mechanics, " +
                "<color=cyan>Sfarx has no top speed.</color> He is generally <color=green>faster</color> in every case that doesn't involve a specific ability of another character, and as such " +
                "should be your <color=green>main choice</color> when you want to go <color=green>as fast as possible.</color>")},
            
            {("Speed Hint:\nSpeed Boosters", "Often it is a good idea to <color=green>skip speed boosters</color> (the wheel ones) - if you're already going really fast, they can actually <color=red>slow you down!</color> " +
                "Keep track of which speed boosters, conveyer ramps, springs, etc. make you <color=red>lose speed</color> - they might be worth skipping.") },
            
            {("Speed Hint:\nJester Swipe", "When <color=yellow>swiping</color> certain oddly shaped <color=yellow>clusters of bits</color>, you will get different speeds and directions based on the distance, " +
                "angle and height you press the swipe button. In addition, jumping after swiping will often give you <color=green>more speed.</color> <color=yellow>Swiping</color> can also <color=green>teleport you to the bits, " +
                "even through walls, without losing speed.</color>")},
            
            {("Speed Hint:\nRail Boosting", "<color=cyan>Boosting</color> is going to be one of your most powerful tools in levels with rails. Remember you can <color=cyan>boost while crouching</color> - utilize both to <color=green>maximize speed.</color>")},
            
            {("Speed Hint:\nRail Braking", "The amount of <color=cyan>energy</color> you get while <color=cyan>braking</color> is proportionate to your speed - if you're going really fast, you can get a <color=cyan>lot of energy</color> with one <color=green>quick tap</color> " +
                "without losing much speed at all.")},
            
            {("Speed Hint:\nRail Braking 2", "Once you reach a <color=green>certain speed threshold,</color> you can <color=cyan>get more energy through tapping brake than you use when boosting</color> to compensate for the " +
                "lost speed. At this point, you can keep <color=green>gaining speed indefinitely</color> and end with a <color=cyan>full energy meter.</color> Your top priority should be getting to this level of speed on a rail " +
                "as often as possible.")},
            
            {("Speed Hint:\nRail Braking 3", "Because of the cumulative effect of <color=cyan>having energy</color> meaning you can <color=green>go fast enough to accelerate while maintaining that energy,</color> " +
                "<color=cyan>getting energy on rail levels</color> can be <color=green>very important.</color> It can be worth it to <color=green>go somewhat out of your way</color> to make sure you get some, and to not skip parts of rail sections " +
                "if those parts can be used to <color=cyan>gain more speed and energy.</color>")},
            
            {("Speed Hint:\nRail Swiping", "A very <color=cyan>quick jump</color> followed, with a small delay, by a <color=yellow>jester swipe</color> can be used to <color=green>gain speed on rails</color> when there is a <color=yellow>line of bits</color> - the swipe " +
                "will often put you back on the rail with a <color=green>speed boost.</color> You should definitely make use of this when possible, since speed on rails is so important. <color=red>If this is inconsistent,</color> " +
                "and you find yourself coming off the rail, <color=red>delay the jester swipe more.</color>")},
            
            {("Speed Hint:\nCharged Jester\nSwipe", "If you encounter a <color=yellow>line of bits</color> while at low speed, it can be a good idea to do a <color=yellow>charged jester dash</color> before swiping them. Otherwise you're wasting the extra speed from those bits. " +
                "Lines of bits that are right before a <color=red>moving hazard</color> are good to charge before, instead of relying on inconsistent timing.") },
            
            {("Speed Hint:\nFloat's Homing\nAttack", "In addition to <color=purple>Float's</color> platforming abilities, she also has a faster <color=cyan>homing attack</color> - <color=red>groups of enemies</color> that are " +
                "close enough together can allow <color=purple>Float</color> to get through them <color=green>very quickly</color> by <color=green>mashing the homing attack button.</color> Since histun pauses the timer, dashing " +
                "through enemies this way is <color=green>borderline instant.</color>") },

            {("Traversal Hint:\nRunning Up Walls", "<color=green>Running up walls</color> will be an <color=green>extremely important</color> tool for you to learn. Almost anywhere an <color=green>upward slope</color> leads " +
                "into a <color=green>wall</color> can be used to do it, and <color=cyan>repeated dashes</color> will <color=cyan>keep you on the wall.</color> When wallrunning, to <color=green>transition around angled edges and avoid falling off,</color> run at an angle " +
                "<color=green>almost parallel</color> to the edge.")},
            
            {("Traversal Hint:\nSlope Jumping", "Using a <color=yellow>charged jester dash</color> on a slope and then <color=cyan>immediately jumping</color> can get you a <color=green>lot of height</color> with no runway - if you're having trouble " +
                "angling your <color=yellow>charged jester dash</color> up the slope, <color=green>angle it to the side first to 'ease' your character into aiming up the slope.</color>")},
            
            {("Traversal Hint:\nRamp Jumping", "Using a <color=yellow>charged jester dash</color> before a <color=grey>conveyer ramp</color> can be a REALLY good tool for <color=green>gaining a lot of height and distance.</color> Make sure to time " +
                "the jump <color=grey>near the end, but not after the end of the ramp</color> for the most height. The <color=green>faster</color> you're going before hand, the <color=green>more height you'll get.</color>")},
            
            {("Traversal Hint:\nStatic Mode", "<color=orange>Fark</color> actually gets a <color=cyan>boost to jump force</color> from <color=cyan>static mode,</color> as well as <color=green>quicker acceleration</color> - if you have a short runway for a <color=cyan>slope jump</color> " +
                "and can get enough <color=cyan>energy,</color> you might want to use <color=cyan>static mode</color> to get that <color=green>extra height or distance.</color>")},
            
            {("Traversal Hint:\nAir Movement", "A general rule of thumb for <color=green>climbing and making large gaps</color> - you will often want to start as <color=purple>Float</color> to make most use of her <color=purple>hover ability,</color> " +
                "and then switch to <color=orange>Fark</color> to use <color=orange>both his air dashes.</color> Remember if you use the <color=orange>first airdash</color> with <color=orange>Fark,</color> you can do a <color=orange>second one.</color> <color=purple>Float</color> can <color=purple>repeatedly hover into wall jumps</color> " +
                "for that <color=green>extra height</color> before using <color=orange>Fark.</color>")},
            
            {("Traversal Hint:\nBig Jumps", "If you have a way to get a <color=green>tremendous amount of speed,</color> you might need all that speed to get a <color=cyan>slope jump</color> high enough to reach some " +
                "locations. Keep an eye out for opportunities to go <color=green>really fast right before a slope, or an upwards slant in a rail</color> that can be jumped to get a <color=green>huge amount of height.</color>")},
            
            {("Traversal Hint:\nHigher\nDouble Jump", "If you <color=cyan>air dash and then immediately jump,</color> you will get the <color=green>amount of height from your grounded jump</color> instead of your double jump. " +
                "While you'll use up the air dash you could have used for a wall jump, this can be situationally useful for traversal. <color=cyan>Fark's static mode</color> has a very <color=green>high double jump,</color> and can get a <color=green>LOT of extra height</color> this way.")},
            
            {("Traversal Hint:\nBetter\nWall Jump", "When <color=green>wall jumping</color> across large distances, <color=green>hold in the direction you're jumping</color> to gain more speed and keep more height. " +
                "Hold parallel to the wall near the end of the jump to sacrifice some height for extra speed. Also, <color=cyan>Sfarx</color> has a higher wall jump.")},
            
            {("Traversal Hint:\nTrees", "using <color=green>trees</color> is an important skill in the protest prison district! Learn how to <color=green>dash up them</color> from parts you can stand on, how to get around the polygon edges by <color=cyan>running near-parallel to them,</color> " +
                "and how to <color=yellow>charged jester dash jump</color> from sloped parts for <color=cyan>higher jumps.</color>")},


            {("Score Hint:\nGeneral Advice", "While you're routing for <color=magenta>score,</color> pay attention to <color=magenta>which objects actually add the most score, or the most multiplier,</color> " +
                "and plan your route based on that. The <color=red>biped robots</color> give a ton of <color=magenta>score,</color> as do <color=green>green capsules.</color> Green capsules don't give you that much multiplier though, keep that in mind.")},
            
            {("Score Hint:\nCheckpoints", "<color=yellow>Checkpoints</color> give you a TON of <color=magenta>score</color> and <color=magenta>much more multiplier</color> than green capsules. " +
                "It's worth <color=green>going out of your way</color> to get them!")},
            
            {("Score Hint:\nWeak Enemies", "If there's a route with a lot of <color=cyan>weak enemies</color> and <color=yellow>bit bubbles</color> you can <color=cyan>homing attack</color> through really quickly, it can be <color=green>worth it to do so,</color> " +
                "especially early on in the run. Doing so will <color=magenta>get your multiplier up significantly.</color>")},
            
            {("Score Hint:\nStrong Enemies", "A fast way to take out the <color=red>biped robots</color> is to use <color=cyan>energy</color> built elsewhere, and then get a <color=orange>lot of combo really quickly</color> with multihits like <color=yellow>Spark,</color> <color=orange>Fark</color> and <color=cyan>Sfarx's</color> <color=yellow>heavy air combo,</color> " +
                "along with <color=cyan>Sfark's</color> <color=green>charge shot.</color> Once your combo is about 3/4ths full, use <color=yellow>Hyper Surge</color> to kill them quickly <color=red>along with any other nearby enemies.</color>")},
            
            {("Score Hint:\nWeigh Time\nVs Opportunity", "When you're deciding what to go for during <color=magenta>score runs,</color> always ask yourself <color=red>how much time</color> something takes versus <color=green>how much you have to gain.</color> " +
                "Do I have to <color=red>slow down</color> too much to get this, or go <color=red>too far out of my way?</color> Can I <color=green>break a lot of things here very quickly?</color> <color=red>Don't waste too much time</color> breaking <color=red>every little thing</color> if they're <color=red>not worth it.</color>")},
            
            {("Score Hint:\nMeasuring Score", "When you are prioritizing what to break, kill or collect, make sure to pay attention to the thing's actual <color=magenta>worth in comparison to each other.</color>  You may find some things are <color=green>more or less " +
                "worth breaking than you thought.</color> Remember that things scale with your multiplier, so <color=green>break different things while the multiplier is about the same to see how they compare.</color>")},
            
            {("Score Hint:\nMath", "If you feel like actually doing <color=red>math,</color> the actual <color=magenta>score worth</color> of something is the <color=magenta>amount of points gained divided by (multiplier + 1).</color>")},
            
            {("Score Hint:\nBreaking Stuff\nFaster", "You can break <color=cyan>cargo containers</color> by <color=yellow>attacking on top of them</color> with <color=yellow>Spark,</color> <color=orange>Fark,</color> and <color=cyan>Sfarx.</color> " +
                "Use <color=yellow>heavy attack combos</color> to break them. <color=grey>Concrete boxes</color> give <color=magenta>much more score</color> than wooden ones and <color=yellow>can be broken in a single heavy attack.</color> " +
                "Make good use of <color=yellow>Hyper Surge</color> as a way to <color=cyan>destroy lots of tough breakables quickly</color>; it's a good use of all the <color=cyan>energy</color> you build in score runs.")},

            {("Exploit Fixes:\nDash Phasing", "<color=orange>Fark</color> and <color=cyan>Reaper Jester</color> can <color=red>no longer phase through some solid walls using their dash.</color> You'll have to work a bit harder " +
                "if you want out of bounds.")},
        };

        public static List<(string, string)> HintsList = new List<(string, string)>()
        {
            {("Alpine Carrera:\nSpeed", "Maximize the utility of your <color=yellow>boost meter</color> by " +
                "<color=green>repeatedly quickly tapping the boost button</color> instead of holding it. Try to <color=green>space your taps</color> so you <color=green>just barely use all of it up,</color> " +
                "and save it when there's a boost pad.") },

            {("High Rise Tracks:\nSpeed", "On the first set of <color=grey>conveyer ramps,</color> take the <color=green>rightmost ramp</color> and <color=yellow>jester swipe</color> the <color=yellow>rightmost line of bits,</color> you will ride the side of the ramp and <color=green>keep from losing speed.</color> " +
                "On the other ramps, <color=cyan>jump</color> before you hit them to more reliably <color=green>maintain your speed</color> and keep from losing it.") },
            
            {("Lost Riviera:\nSpeed", "The way you start this level is <color=red>EXTREMELY IMPORTANT</color> as it determines how much <color=cyan>momentum</color> you'll have for the segments after, and whether you'll reach the <color=cyan>energy gain threshold</color> (see the three <color=cyan>'rail braking'</color> tips). " +
                "Start by jumping and getting the <color=cyan>blue bit bubble</color> on the left, then find a route that lets you <color=green>rail jump to near the end of the</color> <color=red>leftmost</color> <color=green>off rails segment</color> with around <color=cyan>half your energy bar full.</color>") },

            {("Lost Ravine:\nSpeed", "It's actually possible to <color=green>skip the fights</color> on this level. Figuring out how to is <color=red>critical</color> for beating the speed medal targets. " +
                "For a hint - <color=yellow>closed doors are not always impassable.</color>") },

            {("District 5:\nSpeed", "After you <color=cyan>land in the water,</color> take the middle route to the <color=grey>boost ramp</color> and <color=cyan>jump high</color> to the left; when you get to the <color=yellow>2 lines of bits, charge a jester dash</color> and then, timed so you won't hit the <color=red>fire</color>, " +
                "<color=yellow>jester swipe the right line of bits</color> to the <color=grey>boost ramp.</color> This route can save you an incredible amount of time.") },

            {("District 6:\nScore", "<color=yellow>Checkpoints</color> give the most <color=purple>score,</color> but the <color=red>least multiplier.</color> You should <color=green>work your way to the end</color> getting as many points as you can while <color=red>avoiding checkpoints,</color> and then <color=yellow>run back and grab them all.</color>") },

            {("District 9:\nExplore Medals", "The <color=orange>off-pulley part</color> of the <color=orange>final rocket pulley section</color> can be traversed <color=green>forwards or backwards</color> to get the <color=blue>explore medals</color> - getting onto it from <color=cyan>the end</color> is the easier of the two. Here's a hint - <color=red>don't fall down to the area where the goal is!</color>") },

            {("District 79:\nSpeed", "Every fight in this level can be <color=green>skipped!</color> The first one is the trickiest - <color=yellow>jester swiping through the wall</color> will leave you <color=red>stuck under an invisible ceiling.</color> Try to get to the <color=cyan>reservior underneath</color> and <color=cyan>dash jump</color> to the next platform. If you do it right you'll <color=green>miss the invisible ceiling</color> and can <color=cyan>wall jump up.</color>") },

            {("Throwback:\nCombat", "Double's <color=yellow>delayed vertical flash</color> move can actually be <color=yellow>blocked</color> and <color=cyan>parried.</color> This knowledge is critical if you want to keep up the aggression.") },
        };
        public static GameObject GetTipsMenu(ShopaloShop shopBase)
        {
            return shopBase.transform.GetChild(1).Find("TipsMenu").gameObject;
        }
        public static GameObject GetHintsMenu(ShopaloShop shopBase)
        {
            return shopBase.transform.GetChild(1).Find("HintsMenu").gameObject;
        }
        public static Transform GetMenuIconParent(GameObject menu)
        {
            return menu.transform.GetChild(0).GetChild(0);
        }
        public static ShopItenDetails[] GetMenuItems(Transform IconsParent)
        {
            List<ShopItenDetails> itemList = new List<ShopItenDetails>();
            for (int i = 0; i < IconsParent.childCount; i++)
            {
                itemList.Add(IconsParent.GetChild(i).GetComponent<ShopItenDetails>());
            }
            return itemList.ToArray();
        }
        private static void Prefix(ShopaloShop __instance)
        {
            if (MasteryMod.DifficultyIsMastery())
            {
                __instance.WriteFrequency = 2000;
                __instance.MenuMoveSpeed = 2000;
                RectTransform controlsTip = __instance.transform.GetChild(2).GetComponent<RectTransform>();
                Vector2 controlsPos = controlsTip.anchoredPosition;
                controlsPos.y = -165;
                controlsTip.anchoredPosition = controlsPos;

                RectTransform textBox = __instance.transform.GetChild(3).GetComponent<RectTransform>();
                Vector2 tbSize = textBox.sizeDelta;
                tbSize.y = 130;
                textBox.sizeDelta = tbSize;

                RectTransform textBoxText = textBox.transform.GetChild(0).GetComponent<RectTransform>();
                Vector2 tbtPos = textBoxText.anchoredPosition;
                tbtPos.y = 10;
                textBoxText.anchoredPosition = tbtPos;

                ShopItenDetails tipsMenuButton = __instance.transform.GetChild(1).GetChild(0).GetChild(5).GetComponent<ShopItenDetails>();
                __instance.MainPageItens = __instance.MainPageItens.Append(__instance.MainPageItens[4]).ToArray();
                __instance.MainPageItens = __instance.MainPageItens.Append(__instance.MainPageItens[4]).ToArray();
                __instance.MainPageItens[4] = tipsMenuButton;
                tipsMenuButton.name = "Tips";
                tipsMenuButton.gameObject.SetActive(true);

                ShopItenDetails hintsMenuButton = GameObject.Instantiate(tipsMenuButton.gameObject, tipsMenuButton.transform.parent)
                    .GetComponent<ShopItenDetails>();
                hintsMenuButton.transform.SetSiblingIndex(7);
                hintsMenuButton.gameObject.name = "Hints";
                __instance.MainPageItens[5] = hintsMenuButton;

                Vector3 movedBackPos = hintsMenuButton.GetComponent<RectTransform>().anchoredPosition;
                movedBackPos.y = -97;
                hintsMenuButton.GetComponent<RectTransform>().anchoredPosition = movedBackPos;

                movedBackPos = __instance.MainPageItens[6].GetComponent<RectTransform>().anchoredPosition;
                movedBackPos.y = -134;
                __instance.MainPageItens[6].GetComponent<RectTransform>().anchoredPosition = movedBackPos;

                tipsMenuButton.Description = "A nice collection of general tips and tricks for advanced play.";
                tipsMenuButton.ToDoOnClick = ShopItenDetails.OnClick.GoToMenu;
                tipsMenuButton.PageToGo = 5;
                tipsMenuButton.transform.GetChild(0).GetComponent<Text>().text = "TIPS AND TRICKS";

                hintsMenuButton.Description = "Some tips for speed, score and exploration medals on specific stages.";
                hintsMenuButton.ToDoOnClick = ShopItenDetails.OnClick.GoToMenu;
                hintsMenuButton.PageToGo = 6;
                hintsMenuButton.transform.GetChild(0).GetComponent<Text>().text = "STAGE HINTS";

                GameObject TipsMenu = GameObject.Instantiate(__instance.UpgradePage, __instance.UpgradePage.transform.parent);
                TipsMenu.name = "TipsMenu";
                TipsMenu.transform.GetChild(0).gameObject.name = "TipsBG";
                Transform TipsMenuIconsParent = GetMenuIconParent(TipsMenu);
                // delete all children except for the first one
                GameObject.Destroy(TipsMenuIconsParent.GetChild(3).gameObject);
                GameObject.Destroy(TipsMenuIconsParent.GetChild(2).gameObject);
                GameObject.Destroy(TipsMenuIconsParent.GetChild(1).gameObject);
                GameObject TipsLevelDataSource = TipsMenuIconsParent.GetChild(0).gameObject;
                List<GameObject> TipsMenuEntries = new List<GameObject>();
                TipsMenuEntries.Add(TipsLevelDataSource);

                GameObject HintsMenu = GameObject.Instantiate(__instance.UpgradePage, __instance.UpgradePage.transform.parent);
                HintsMenu.name = "HintsMenu";
                HintsMenu.transform.GetChild(0).gameObject.name = "HintsBG";
                Transform HintsMenuIconsParent = GetMenuIconParent(HintsMenu);
                // delete all children except for the first one
                GameObject.Destroy(HintsMenuIconsParent.GetChild(3).gameObject);
                GameObject.Destroy(HintsMenuIconsParent.GetChild(2).gameObject);
                GameObject.Destroy(HintsMenuIconsParent.GetChild(1).gameObject);
                GameObject HintsLevelDataSource = HintsMenuIconsParent.GetChild(0).gameObject;
                List<GameObject> HintsMenuEntries = new List<GameObject>();
                HintsMenuEntries.Add(HintsLevelDataSource);

                for (int i = 0; i < TipsList.Count - 1; i++)
                {
                    TipsMenuEntries.Add(GameObject.Instantiate(TipsLevelDataSource, TipsLevelDataSource.transform.parent));
                }
                for (int i = 0; i < TipsMenuEntries.Count; i++)
                {
                    TipsMenuEntries[i].name = "TIP-" + i;
                    TipsMenuEntries[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 121 - 65 * i);
                    TipsMenuEntries[i].transform.GetChild(2).gameObject.SetActive(false);
                    TipsMenuEntries[i].transform.GetChild(1).GetComponent<Image>().sprite = tipsMenuButton.transform.GetChild(1).GetComponent<Image>().sprite;
                    var item = TipsMenuEntries[i].GetComponent<ShopItenDetails>();
                    item.ToDoOnClick = ShopItenDetails.OnClick.PurchaseIten;
                    item.BitsCost = 1;
                    item.Special = SpecialType.None;
                    item.Move = MovesType.None;
                    item.Upgrade = UpgradeType.None;
                    item.CostText.gameObject.SetActive(false);
                    item.CostText = null;
                    item.MiniMenu = null;
                    item.Video = null;
                    item.transform.Find("txt").GetComponent<Text>().text = TipsList[i].Item1;
                    item.Description = TipsList[i].Item2;
                }

                for (int i = 0; i < HintsList.Count - 1; i++)
                {
                    HintsMenuEntries.Add(GameObject.Instantiate(HintsLevelDataSource, HintsLevelDataSource.transform.parent));
                }
                for (int i = 0; i < HintsMenuEntries.Count; i++)
                {
                    HintsMenuEntries[i].name = "HINT-" + i;
                    HintsMenuEntries[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 121 - 65 * i);
                    HintsMenuEntries[i].transform.GetChild(2).gameObject.SetActive(false);
                    HintsMenuEntries[i].transform.GetChild(1).GetComponent<Image>().sprite = tipsMenuButton.transform.GetChild(1).GetComponent<Image>().sprite;
                    var item = HintsMenuEntries[i].GetComponent<ShopItenDetails>();
                    item.ToDoOnClick = ShopItenDetails.OnClick.PurchaseIten;
                    item.BitsCost = 1;
                    item.Special = SpecialType.None;
                    item.Move = MovesType.None;
                    item.Upgrade = UpgradeType.None;
                    item.CostText.gameObject.SetActive(false);
                    item.CostText = null;
                    item.MiniMenu = null;
                    item.Video = null;
                    item.transform.Find("txt").GetComponent<Text>().text = HintsList[i].Item1;
                    item.Description = HintsList[i].Item2;
                }
                __instance.SpecialItens[2].Description = "It was a hard decision, but I can't have you just skipping stages through energy magnet dashes, and this power is just generally way too good.";

                __instance.SpecialItens[0].Description = "This power was not that good to begin with - I disabled it just so you know you don't have to use it.";

                __instance.SpecialItens[3].Description = "This sort of goes against the spirit of this mod. If you want to do big damage, use Hyper Surge.";

            }
        }
    }

    [HarmonyPatch(typeof(ShopaloShop))]
    [HarmonyPatch("Update")]
    class InformationMenuUpdate
    {
        public static void Scroll(ShopaloShop __instance, Transform page)
        {
            if (__instance.Cursor.transform.localPosition.y > __instance.ScrollTop.localPosition.y)
            {
                page.localPosition -= new Vector3(0f, Time.deltaTime * __instance.MenuMoveSpeed, 0f);
                return;
            }
            if (__instance.Cursor.transform.localPosition.y < __instance.ScrollBot.localPosition.y)
            {
                page.localPosition += new Vector3(0f, Time.deltaTime * __instance.MenuMoveSpeed, 0f);
            }
        }
        private static void Postfix(ShopaloShop __instance, ref ShopItenDetails[] ___CurrentItens)
        {
            if (MasteryMod.DifficultyIsMastery())
            {
                GameObject tipsMenu = ConstructInformationMenu.GetTipsMenu(__instance);
                GameObject hintsMenu = ConstructInformationMenu.GetHintsMenu(__instance);
                if (tipsMenu.activeSelf)
                {
                    if (__instance.Rewinp.GetButtonDown("Jump"))
                    {
                        ___CurrentItens[__instance.Index].Click(__instance);
                    }
                    if (__instance.Rewinp.GetButtonDown("JesterDash"))
                    {
                        __instance.SwitchPage(0);
                        __instance.Back.Play();
                        __instance.VideoImage.enabled = false;
                        __instance.Video.Stop();
                    }
                    Scroll(__instance, ConstructInformationMenu.GetMenuIconParent(tipsMenu));
                }
                else if (hintsMenu.activeSelf)
                {
                    if (__instance.Rewinp.GetButtonDown("Jump"))
                    {
                        ___CurrentItens[__instance.Index].Click(__instance);
                    }
                    if (__instance.Rewinp.GetButtonDown("JesterDash"))
                    {
                        __instance.SwitchPage(0);
                        __instance.Back.Play();
                        __instance.VideoImage.enabled = false;
                        __instance.Video.Stop();
                    }
                    Scroll(__instance, ConstructInformationMenu.GetMenuIconParent(hintsMenu));
                }
            }
        }
    }

    [HarmonyPatch(typeof(ShopaloShop))]
    [HarmonyPatch("SwitchPage")]
    class InformationMenuSwitchPage
    {
        public static void ActivatePage(ShopaloShop __instance, GameObject Page)
        {
            ShopItenDetails component = Page.GetComponent<ShopItenDetails>();
            if (component != null)
            {
                __instance.StartTextBox(component.Description);
            }
            Page.SetActive(true);
        }
        public static void SetPivots(ref ShopItenDetails[] ___CurrentItens, ref Transform[] ___CurrentPivots, ShopItenDetails[] itens)
        {
            ___CurrentItens = itens;
            ___CurrentPivots = new Transform[itens.Length];
            for (int i = 0; i < itens.Length; i++)
            {
                ___CurrentPivots[i] = itens[i].transform.Find("pivot");
            }
        }
        private static void Prefix(int page, ShopaloShop __instance)
        {
            if (MasteryMod.DifficultyIsMastery())
            {
                GameObject tipsPage = ConstructInformationMenu.GetTipsMenu(__instance);
                Transform tipsParent = ConstructInformationMenu.GetMenuIconParent(tipsPage);
                tipsPage.SetActive(false);
                tipsParent.localPosition = Vector3.zero;
                GameObject hintsPage = ConstructInformationMenu.GetHintsMenu(__instance);
                Transform hintsParent = ConstructInformationMenu.GetMenuIconParent(hintsPage);
                hintsPage.SetActive(false);
                hintsParent.localPosition = Vector3.zero;
            }
        }
        private static void Postfix(int page, ShopaloShop __instance, ref ShopItenDetails[] ___CurrentItens, ref Transform[] ___CurrentPivots)
        {
            if (MasteryMod.DifficultyIsMastery())
            {
                if (page == 5)
                {
                    GameObject tipsPage = ConstructInformationMenu.GetTipsMenu(__instance);
                    var itemsList = ConstructInformationMenu.GetMenuItems(ConstructInformationMenu.GetMenuIconParent(tipsPage));
                    ActivatePage(__instance, tipsPage);
                    SetPivots(ref ___CurrentItens, ref ___CurrentPivots, itemsList);
                }
                if (page == 6)
                {
                    GameObject hintsPage = ConstructInformationMenu.GetHintsMenu(__instance);
                    var itemsList = ConstructInformationMenu.GetMenuItems(ConstructInformationMenu.GetMenuIconParent(hintsPage));
                    ActivatePage(__instance, hintsPage);
                    SetPivots(ref ___CurrentItens, ref ___CurrentPivots, itemsList);
                }
            }
        }

        [HarmonyPatch(typeof(ShopaloShop))]
        [HarmonyPatch("WriteShopaloBox")]
        class MakeRichTextWork
        {
            private static bool Prefix(ShopaloShop __instance, ref bool ___DoneWritting, ref float ___dCount, ref float ___FreqCounter,
                ref int ___charindex, ref string ___ToWrite, ref string ___t)
            {
                if (MasteryMod.DifficultyIsMastery())
                {
                    if (!___DoneWritting)
                    {
                        ___dCount += Time.fixedDeltaTime;
                        if (___dCount > __instance.DialogSoundInterval)
                        {
                            __instance.DialogSound.Play();
                            __instance.DialogSound.pitch = UnityEngine.Random.Range(__instance.DialogSoundPitchRange.x, __instance.DialogSoundPitchRange.y);
                            ___dCount = 0f;
                        }
                        ___FreqCounter += Time.fixedDeltaTime * __instance.WriteFrequency;
                        if (___FreqCounter >= 1f)
                        {
                            ___FreqCounter = 0f;
                            int num = 0;
                            int insideFormattedState = 0;
                            while ((num < __instance.CharactersPerFrame || insideFormattedState != 0) && ___charindex < ___ToWrite.Length)
                            {
                                if (___ToWrite.Substring(___charindex) != null)
                                {
                                    string substr = ___ToWrite.Substring(___charindex, 1);
                                    ___t += substr;
                                    if (insideFormattedState == 0 && substr == "<") insideFormattedState = 1;
                                    else if (insideFormattedState == 1 && substr == ">") insideFormattedState = 2;
                                    else if (insideFormattedState == 2 && substr == "<") insideFormattedState = 3;
                                    else if (insideFormattedState == 3 && substr == "/") insideFormattedState = 4;
                                    else if (insideFormattedState == 4 && substr == ">") insideFormattedState = 0;
                                    ___charindex++;
                                    __instance.TextBox.text = ___t;
                                }
                                else
                                {
                                    ___DoneWritting = true;
                                }
                                num++;
                            }
                            if (___charindex >= ___ToWrite.Length)
                            {
                                ___DoneWritting = true;
                            }
                        }
                    }
                    return false;
                }
                return true;
            }
        }
    }
}