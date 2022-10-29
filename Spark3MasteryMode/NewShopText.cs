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
            {("Changes:\nEnemies", "Enemies have not only been given more health and armor, but made more aggressive. Health and armor levels are " +
                "about 3 times what they are in Hardcore.") },
            {("Changes:\nDifficulty", "While you have as much health as on Hardcore difficulty, your parry timing is that of Challenge Jester. " +
                "Make sure you learn to time it well, and be careful of the cooldown after releasing block!") },
            {("Changes:\nStatic Mode", "Static mode now drains half of your energy on use, to prevent using it to heal and then switching to not lose " +
                "energy. You also can't gain energy while it's active, but it drains less slowly. It is still situationally useful and a good way of " +
                "getting back health regardless.") },
            {("Changes:\nBlink", "Your teleport special power, Blink, now takes 15 energy instead of 25. In some fights you might want to make good " +
                "use of it, since you might not be able to keep the aggression up as well when the enemy is attacking!") },
            {("Changes:\nMagnet Dash", "A Magnet Dash is a dash right after a homing attack. " +
                "Magnet Dash has been made less overpowered, but more consistent. If you have speed, it will generally " +
                "give you the same amount of extra speed, regardless of vertical angle. You still should try to magent dash in the enemy's direction for the most speed.") },
            
            {("Combat Hint:\nCombo Meter", "The UI is actually inaccurate - your combo meter goes from 1x to 4x. Always keep it as full as possible, " +
                "otherwise you will do pitiful damage, especially in Mastery Mode. If you have to make space from the enemy, keep the meter up with " +
                "shots.")},
            {("Combat Hint:\nJuggling", "After you break a boss's armor, the amount of time they stay open is proportionate to the amount of time they " +
                "spend on the ground. Make sure to keep your juggle going as long as possible while they're open. Remember, Spark can land during juggles, " +
                "and get his jump / dashes back.")},
            {("Combat Hint:\nHyper Surge", "Hyper Surge is going to be your biggest time saver on stages with combat. Make sure to make good use of this. " +
                "For score medals, you can make use of energy gained elsewhere, quickly build a combo to around 60-70%, and then use this to take " +
                "tough enemies out quickly for lots of points.")},
            {("Combat Hint:\nCharged Kick", "Charged Kick does a suprising amount of damage with a full combo. "+
                "Getting into position and charging it is a good thing to do in between enemy waves or Throwback's boss transitions.")},
            {("Combat Hint:\nScary Face", "Float may be limited in combat, but her Scary Face passive does a lot of free damage even when not " +
                "attacking with her. Keeping her in your party even for combat stages is a good idea.")},
            {("Combat Hint:\nCharge Shot", "You can charge your shot with any character, and the charge will maintain between character switches. " +
                "Keep your shots charged and unleash them with Spark or Sfarx. Sfarx's charge shot gives you a lot of energy and combo and does a lot " +
                "of damage, so use it when you can.")},
            {("Combat Hint:\nStoring Attacks", "You can store the hitbox of any attack by switching characters when it is active. This works best with " +
                "lingering hitboxes like Fark's light attack combo ender and Spark's air heavy attack combo ender. When switching back to the character, " +
                "the hitbox will reactivate, and you can attack while it is active, increasing your DPS.")},
            {("Combat Hint:\nDash I-Frames", "Fark's dash has considerably more i-frames than it looks. Even for big, long lasting AOE attacks, " +
                "Fark can use multiple dashes to avoid damage completely without having to back off.")},
            {("Combat Hint:\nSkipping Combat", "While getting good at combat is useful, you also want to skip combat arenas as much as possible. " +
                "A lot of arenas can have their activation box maneuvered around, and some can be escaped after activating. " +
                "Remember that jester swipes and homing attacks can take you through walls!")},

            {("Speed Hint:\nExperimentation", "When you are testing your run and experimenting with the various mechanics, remember the Radar has a " +
                "speed indicator on it - use it to make sure you're doing the right things to gain and maintain speed!")},
            {("Speed Hint:\nSfarx", "While other characters have top speeds which they can not get past without downward slopes or other speed gaining mechanics, " +
                "Sfarx has no top speed. He is generally faster in every case that doesn't involve a specific ability of another character, and as such " +
                "should be your main choice when you want to go as fast as possible.")},
            {("Speed Hint:\nSpeed Boosters", "Often it is a good idea to skip speed boosters (the wheel ones) - if you're already going really fast, they can actually slow you down! " +
                "Keep track of which speed boosters, conveyer ramps, springs, etc. make you lose speed - they might be worth skipping.") },
            {("Speed Hint:\nJester Swipe", "When swiping certain oddly shaped clusters of bits, you will get different speeds and directions based on the distance, " +
                "angle and height you press the swipe button. In addition, jumping after swiping will often give you more speed. Swiping can also teleport you to the bits, " +
                "even through walls, without losing speed.")},
            {("Speed Hint:\nRail Boosting", "Boosting is going to be one of your most powerful tools in levels with rails. Remember you can boost while crouching - utilize both to maximize speed.")},
            {("Speed Hint:\nRail Braking", "The amount of energy you get while braking is proportionate to your speed - if you're going really fast, you can get a lot of energy with one quick tap " +
                "without losing much speed at all. Other good moments to brake are right after a rail jester swipe, or hitting a boost pad.")},
            {("Speed Hint:\nRail Braking 2", "Once you reach a certain speed threshold, you can get more energy through tapping brake than you use when boosting to compensate for the " +
                "lost speed. At this point, you can keep gaining speed indefinitely and end with a full energy meter. Your top priority should be getting to this level of speed on a rail " +
                "as often as possible.")},
            {("Speed Hint:\nRail Braking 3", "Because of the cumulative effect of having energy meaning you can go fast enough to accelerate while maintaining that energy, " +
                "getting energy on rail levels can be very important. It can be worth it to go somewhat out of your way to make sure you get some, and to not skip parts of rail sections " +
                "if those parts can be used to gain more speed and energy.")},
            {("Speed Hint:\nRail Swiping", "A very quick jump followed immediately by a jester swipe can be used to gain speed on rails when there is a line of bits - the swipe " +
                "will often put you back on the rail with a speed boost. You should definitely make use of this when possible, since speed on rails is so important.")},
            {("Speed Hint:\nFloat's Homing\nAttack", "In addition to Float's platforming abilities, she also has a faster homing attack - groups of enemies that are " +
                "close enough together can allow float to get through them very quickly by mashing the homing attack button. Since histun pauses the timer, dashing " +
                "through enemies this way is borderline instant.") },

            {("Traversal Hint:\nRunning Up Walls", "Running up walls will be an extremely important tool for you to learn. Almost anywhere an upward slope leads " +
                "into a wall can be used to do it, and repeated dashes will keep you on the wall. When wallrunning, to transition around angled edges, run at an angle " +
                "almost parallel to the edge.")},
            {("Traversal Hint:\nSlope Jumping", "Using a charged jester dash on a slope and then immediately jumping can get you a lot of height with no runway - if you're having trouble " +
                "angling your charged jester dash up the slope, angle it to the side first to 'ease' your character into aiming up the slope.")},
            {("Traversal Hint:\nRamp Jumping", "Using a charged jester dash before a conveyer ramp can be a REALLY good tool for gaining a lot of height and distance. Make sure to time " +
                "the jump while you're on the ramp. The faster you're going before hand, the more height you'll get.")},
            {("Traversal Hint:\nStatic Mode", "Fark actually gets a boost to jump force from static mode, as well as quicker acceleration - if you have a short runway for a slope jump " +
                "and can get enough energy, you might want to use static mode to get that extra height or distance.")},
            {("Traversal Hint:\nAir Movement", "A general rule of thumb for climbing and making large gaps - you will often want to start as Float to make most use of her hover ability, " +
                "and then switch to Fark to use both his air dashes. Remember if you use the first airdash with Fark, you can do a second one. Float can repeatedly hover into wall jumps " +
                "for that extra height before using Fark.")},
            
            {("Score Hint:\nGeneral Advice", "While you're routing for score, pay attention to which objects actually add the most score, or the most multiplier, " +
                "and plan your route based on that. The biped robots give a ton of score, as do green capsules.")},
        };

        public static List<(string, string)> HintsList = new List<(string, string)>()
        {
            {("Alpine Carrera:\nSpeed", "It is always faster to do car segments on foot when you can. On Alpine Carrera, you should boost by " +
                "repeatedly quickly tapping the boost button instead of holding it. Try to space your taps so you just barely use all of it up, " +
                "and save it when there's a boost pad.") },
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
        public static void SetPivots(ref ShopItenDetails[] ___CurrentItens, ref Transform[] ___CurrentPivots,  ShopItenDetails[] itens)
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
    }
}