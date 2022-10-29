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
        public static List<(string, string)> HintsList = new List<(string, string)>()
        {
            {("Changes:\nEnemies", "Enemies have not only been given more health and armor, but made more aggressive. Health and armor levels are " +
                "about 3 times what they are in Hardcore.") },
            {("Changes:\nDifficulty", "While you have as much health as on Hardcore difficulty, your parry timing is that of Challenge Jester. " +
                "Make sure you learn to time it well, and be careful of the cooldown after releasing block!") },
            {("Changes:\nMagnet Dash", "Magnet Dash has been made less overpowered, but more consistent. If you have speed, it will generally " +
                "give you the same amount of speed, regardless of vertical angle. You still should try to aim for the enemy horizontally though.") },
            {("Combat Hint:\nCharged Kick", "This move does a suprising amount of damage with a full combo. "+
                "Getting into position and charging it is a good thing to do in between enemy waves or Throwback's boss transitions.")},
            {("Combat Hint:\nScary Face", "Float may be limited in combat, but her Scary Face passive does a lot of free damage even when not " +
                "attacking with her. Keeping her in your party even for combat stages is a good idea.")},
            {("Combat Hint:\nCharge Shot", "You can charge your shot with any character, and the charge will maintain between character switches. " +
                "Keep your shots charged and unleash them with Spark or Sfarx. Sfarx's charge shot gives you a lot of energy and combo and does a lot " +
                "of damage, so use it when you can.")}, 
            {("Combat Hint:\nSkipping Combat", "While getting good at combat is useful, you also want to skip combat arenas as much as possible. " +
                "A lot of arenas can have their activation box maneuvered around, and some can be escaped after activating. " +
                "Remember that jester swipes and homing attacks can take you through walls!")},
        };
        public static GameObject GetHintsMenu(ShopaloShop shopBase)
        {
            return shopBase.transform.GetChild(1).Find("HintsMenu").gameObject;
        }
        public static Transform GetHintsMenuIconParent(GameObject hintsMenu)
        {
            return hintsMenu.transform.GetChild(0).GetChild(0);
        }
        public static ShopItenDetails[] GetHintsMenuItems(Transform IconsParent)
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
                ShopItenDetails hintsMenuButton = __instance.transform.GetChild(1).GetChild(0).GetChild(5).GetComponent<ShopItenDetails>();
                __instance.MainPageItens = __instance.MainPageItens.Append(__instance.MainPageItens[4]).ToArray();
                __instance.MainPageItens[4] = hintsMenuButton;
                hintsMenuButton.gameObject.SetActive(true);
                Vector3 movedBackPos = __instance.MainPageItens[5].GetComponent<RectTransform>().anchoredPosition;
                movedBackPos.y = -97;
                __instance.MainPageItens[5].GetComponent<RectTransform>().anchoredPosition = movedBackPos;
                hintsMenuButton.Description = "A nice collection of tips and tricks for advanced play, as well as level specific hints for speed, score and exploration medals";
                hintsMenuButton.ToDoOnClick = ShopItenDetails.OnClick.GoToMenu;
                hintsMenuButton.PageToGo = 5;
                hintsMenuButton.transform.GetChild(0).GetComponent<Text>().text = "TIPS AND HINTS";

                GameObject HintsMenu = GameObject.Instantiate(__instance.UpgradePage, __instance.UpgradePage.transform.parent);
                HintsMenu.name = "HintsMenu";
                HintsMenu.transform.GetChild(0).gameObject.name = "HintsBG";
                Transform HintsMenuIconsParent = GetHintsMenuIconParent(HintsMenu);
                // delete all children except for the first one
                GameObject.Destroy(HintsMenuIconsParent.GetChild(3).gameObject);
                GameObject.Destroy(HintsMenuIconsParent.GetChild(2).gameObject);
                GameObject.Destroy(HintsMenuIconsParent.GetChild(1).gameObject);
                GameObject LevelDataSource = HintsMenuIconsParent.GetChild(0).gameObject;
                List<GameObject> HintsMenuTips = new List<GameObject>();
                HintsMenuTips.Add(LevelDataSource);
                for (int i = 0; i < HintsList.Count - 1; i++)
                {
                    HintsMenuTips.Add(GameObject.Instantiate(LevelDataSource, LevelDataSource.transform.parent));
                }
                for (int i = 0; i < HintsMenuTips.Count; i++)
                {
                    HintsMenuTips[i].name = "HINT-" + i;
                    HintsMenuTips[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 121 - 65 * i);
                    HintsMenuTips[i].transform.GetChild(2).gameObject.SetActive(false);
                    HintsMenuTips[i].transform.GetChild(1).GetComponent<Image>().sprite = hintsMenuButton.transform.GetChild(1).GetComponent<Image>().sprite;
                    var item = HintsMenuTips[i].GetComponent<ShopItenDetails>();
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

                /*__instance.InitialText = "CHECK ITEM DESCRIPTIONS FOR HINTS. All medal targets are found and set WITHOUT magnet dashing (dashing during a homing attack for a big speed boost) - while it is not explicitly disabled, if you want the best challenge you should avoid using it, and focus on other mechanics to build and maintain speed.";
                
                __instance.MainPageItens[0].Description = "Some of these may actually reduce your DPS or combo gain by adding more low damage attacks or repeated hitboxes to your strings. Remember that you can disable or enable them whenever you want.";
                __instance.MainPageItens[1].Description = "I had to selectively ban some of these items due to being overpowered or not positively affecting the engagement with the level design or enemies. Apologies.";
                __instance.MainPageItens[2].Description = "Often it is a good idea to skip boost launchers - if you're already going really fast, they can actually slow you down! Also, to jester swipe groups of bits on rails, do a quick jump and then immediately jester swipe. This is often a good time to tap regen brake for a bit of free energy as well.";
                __instance.MainPageItens[3].Description = "These are all unlocked by default. Get your characters now!";
                
                __instance.MoveItens[0].Description = "This move does a suprising amount of damage with a full combo. Getting into position and charging it is a good thing to do in between enemy waves or Throwback's boss transitions.";
                __instance.MoveItens[1].Description = "Keep an eye out for ways to increase your DPS in fights. While Float may be weak in combat, her Scary Face passive ability is super important for extra DPS and energy gain. Sfarx's charge shot is another move that is great for increasing DPS and energy gain.";
                __instance.MoveItens[2].Description = "You can charge Spark and Sfarx's gun by holding the charge shot button while attacking, and even with other characters - the charge will be maintained when switching characters. Make good use of this!";
                __instance.MoveItens[3].Description = "You can store the hitbox of any attack by switching characters when it is active. This works best with lingering hitboxes like Fark's light attack combo ender and Spark's air heavy attack combo ender. When switching back to the character, the hitbox will reactivate, and you can attack while it is active, increasing your DPS.";
                __instance.MoveItens[4].Description = "While getting good at combat is useful, you also want to skip combat arenas as much as possible. A lot of arenas can have their activation box maneuvered around, and some can be escaped after activating with things like dashes and jester swipes. Remember that jester swipes and homing attacks can take you through walls!";

                __instance.UpgradeItens[0].Description = "Bosses can only regenerate their armor when they're on the ground - it is in your best interest to keep them in the air as long as possible when you have an opening. If you have to stop attacking a boss to avoid an unparryable move, keep the combo going with shots.";
                __instance.UpgradeItens[1].Description = "This is going to be one of your best reasons to get energy in levels that have rails. Make sure to kill enemies when you can using Spark's gun, as he can use it without slowing down. Also, remember that you can crouch even when boosting.";
                __instance.UpgradeItens[2].Description = "Best used with quick taps when you're going REALLY REALLY fast to get bursts of energy. Can be used in conjunction with boosting to keep from decelerating on rails while either maintaining or even gaining energy. The faster you're going, the more energy you get and the less you slow down.";
                __instance.UpgradeItens[3].Description = "When swiping certain oddly shaped clusters of bits, you will get different speeds and directions based on the distance, angle and height you press the swipe button. In addition, jumping after swiping will often give you more speed. Swiping can also teleport you to the bits, even through walls, without losing speed.";
                
                __instance.SpecialItens[0].Description = "It is always faster to do car segments on foot when you can. On Alpine Carrera, you should boost by repeatedly quickly tapping the boost button instead of holding it. Try to space your taps so you just barely use all of it up, and save it when there's a boost pad.";
                __instance.SpecialItens[1].Description = "This is going to be your biggest time saver on stages with combat. Make sure to make good use of this. For score medals, you can make use of energy gained elsewhere, quickly build a combo to around 60-70%, and then use this to take tough enemies out quickly for lots of points.";
                __instance.SpecialItens[2].Description = "It was a hard decision, but I can't have you just skipping stages through super magnet dashes, and this power is just generally way too good.";
                __instance.SpecialItens[3].Description = "The UI for your combo multiplier is misleading - a full combo bar is actually 4x damage. Make sure to keep it full as much as possible.";
                __instance.SpecialItens[4].Description = "I've modified this to use less energy. Use it to keep your juggles going! Also, a tip on slope jumping - while charging, you can point a charged jester dash up a steep slope by angling it to the side first.";
                __instance.SpecialItens[5].Description = "While you're routing for score, pay attention to which objects actually add the most score, or the most multiplier, and plan your route based on that.";
                __instance.SpecialItens[6].Description = "Running up walls will be an extremely important tool for you to learn. Almost anywhere an upward slope leads into a wall can be used to do it, and repeated dashes will keep you on the wall. When wallrunning, to transition around angled edges, run at an angle almost parallel to the edge.";

                __instance.JesterItens[0].Description = "Reaper Jester's parry has a second hit that counts as a second parry - when parrying single red attacks, you can use this to break an enemy or boss's armor faster. However, Reaper's low DPS might not justify using him outside of fighting groups.";
                __instance.JesterItens[1].Description = "In addition to Float's platforming abilities, she also has a faster homing attack - groups of enemies that are close enough together can allow float to get through them very quickly by mashing the homing attack button. Since histun pauses the timer, dashing through enemies this way is borderline instant.";
                __instance.JesterItens[2].Description = "Fark's static mode's ability to get quick speed, his double air dash with invulnerability, great DPS and ability to heal will make him a valuable tool. His air dash has more iframes than it looks and is great for dodging boss AOE attacks, even Clarity Centralis's long lasting one.";
                __instance.JesterItens[3].Description = "Sfarx's acceleration and ability to maintain speed are second to none. You should generally be using him to go fast unless you need the ability of another character, or Spark's ability to shoot while moving. His DPS is also great without Fark's poor range and his charge shot is a fantastic way to get extra damage.";
            */
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
                if (ConstructInformationMenu.GetHintsMenu(__instance).activeSelf)
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
                    Scroll(__instance, __instance.MovesIconParent.transform);
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
                GameObject hintsPage = ConstructInformationMenu.GetHintsMenu(__instance);
                Transform hintsParent = ConstructInformationMenu.GetHintsMenuIconParent(hintsPage);
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
                    GameObject hintsPage = ConstructInformationMenu.GetHintsMenu(__instance);
                    var itemsList = ConstructInformationMenu.GetHintsMenuItems(ConstructInformationMenu.GetHintsMenuIconParent(hintsPage));
                    ActivatePage(__instance, hintsPage);
                    SetPivots(ref ___CurrentItens, ref ___CurrentPivots, itemsList);
                }
            }
        }
    }
}