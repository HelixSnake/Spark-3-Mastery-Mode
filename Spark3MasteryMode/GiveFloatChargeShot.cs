using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;

namespace Spark3MasteryMode
{
	[HarmonyPatch(typeof(Action10Control_Blast))]
	[HarmonyPatch("FixedUpdate")]
	class GiveFloatChargeShot
	{
		private static float FloatCooldownCounter = 0;
		private const float FloatCooldownTime = 1.5f;
		private static void Prefix(Action10Control_Blast __instance, ref bool ___Buffer, ref bool ___Initial, ref float ___Counter, ref bool __state)
		{
			if (MasteryMod.DifficultyIsMastery())
			{
				__state = false;
				FloatCooldownCounter += Time.fixedDeltaTime;
				if ((__instance.Inp.Rewinp.GetButtonDown("LockOn")) && (__instance.Actions.Action == 0 || __instance.Actions.Action == 1 || __instance.Actions.Action == 6 || __instance.Actions.Action == 7))
				{
					if (CharacterAnimatorChange.Character == 2 && FloatCooldownTime < FloatCooldownCounter)
					{
						FloatCooldownCounter = 0f;
						___Counter = 0f;
						___Buffer = false;
						___Initial = false;
						__instance.Actions.ChangeAction(10);
						__state = true;
					}
				}
				if (__instance.Inp.Rewinp.GetButtonUp("LockOn") && __instance.Charge > __instance.ChargedThreshold && __instance.Actions.Action != 4 && __instance.Actions.Action != 2 && __instance.Actions.Action != 10)
				{
					if (CharacterAnimatorChange.Character == 2)
					{
						__instance.Actions.ChangeAction(10);
					}
				}
			}
		}

		private static void Postfix(ref bool ___Buffer, ref bool __state)
		{
			if (MasteryMod.DifficultyIsMastery())
			{
				if (__state)
				{
					___Buffer = false;
				}
			}
		}
	}
	[HarmonyPatch(typeof(Action10Control_Blast))]
	[HarmonyPatch("InitialActions")]
	class GiveFloatChargeShot2
	{
		public static bool Charged = false;
		private static void Prefix(Action10Control_Blast __instance, ref bool ___Buffer, ref bool ___Initial)
		{
			if (MasteryMod.DifficultyIsMastery())
			{
				if (__instance.Charge > __instance.ChargedThreshold)
				{
					if (CharacterAnimatorChange.Character == 2)
					{
						__instance.Anim.SetTrigger("Start_Attack_Finisher");
						__instance.Anim.SetTrigger("Next_Finisher");
						__instance.Anim.SetTrigger("Next_Attack");
						Charged = true;
					}
				}
				else if (CharacterAnimatorChange.Character == 2)
				{
					__instance.Anim.SetTrigger("Start_Attack");
					__instance.Anim.SetTrigger("Next_Attack");
					Charged = false;
					GiveFloatChargeShot3.movedFwd = false;
				}
			}
		}
	}
	[HarmonyPatch(typeof(Action10Control_Blast))]
	[HarmonyPatch("BlastAction")]
	class GiveFloatChargeShot3
	{
		public static void MakeFloatMultiSpikeSmall(bool small)
		{
			CharacterAnimatorChange.StaticReference.Skins[2].transform.Find("FloatIHB").Find("FloatMultiSpike").Find("Hitbox").GetComponent<HitBoxInfo>().SmallAttack = small;
		}
		public static void KeepFloatMultiSpikeGoing()
		{
			var IHB = CharacterAnimatorChange.StaticReference.Skins[2].transform.Find("FloatIHB").Find("FloatMultiSpike").Find("Hitbox").GetComponent<IntegradedHitBoxControl>();
			typeof(IntegradedHitBoxControl).GetField("counter", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(IHB, 0.0f);
		}
		public static bool movedFwd = false;
		private static void Prefix(Action10Control_Blast __instance, ref float ___Counter)
		{
			if (MasteryMod.DifficultyIsMastery())
			{
				if (CharacterAnimatorChange.Character == 2 && GiveFloatChargeShot2.Charged)
				{
					if (___Counter > __instance.ActionDuration * 3)
					{
						__instance.Actions.Action00.ManageSkinRotation();
						__instance.Actions.ChangeAction(0);
					}
					else
					{
						if (___Counter > __instance.ActionDuration * 1.4f)
						{
							__instance.Anim.SetTrigger("Next_Finisher");
						}
						if (___Counter > __instance.ActionDuration * 2 && !__instance.Player.Grounded)
						{
							if (__instance.Inp.Rewinp.GetButtonDown("Jump"))
							{
								__instance.Actions.ChangeAction(1);
								__instance.Actions.Action01.DoDoubleJump();
							}
						}
						if (HomingAttackControl.TargetObject != null)
						{
							if (___Counter < __instance.ActionDuration)
							{
								__instance.BlastLookAtEnemy();
							}
							if (Action07_Attack.NearestEnemy != null && Vector3.Distance(__instance.transform.position, Action07_Attack.NearestEnemy.position) < 50f)
							{
								__instance.Player.rigid.velocity = Vector3.zero;
								if (___Counter < __instance.ActionDuration)
								{
									__instance.BlastLookAtEnemy();
								}
							}
						}
					}
				}
				else if (CharacterAnimatorChange.Character == 2)
				{
					bool circleActive = GameObject.Find("FloatDamageCircle").GetComponent<FloatDamageTarget>().Hit.activeSelf;
					if (!circleActive && ___Counter > __instance.ActionDuration * 2.5)
					{
						__instance.Actions.Action00.ManageSkinRotation();
						__instance.Actions.ChangeAction(0);
						movedFwd = false;
					}
					else
					{
						if (___Counter > Time.fixedDeltaTime * 3.1f)
						{
							if (!movedFwd) {
								__instance.Anim.SetTrigger("Next_Attack");
								movedFwd = true;
								MakeFloatMultiSpikeSmall(true);
							}
							else
							{
								__instance.Anim.ResetTrigger("Next_Attack");
							}
						}
						if (___Counter < __instance.ActionDuration)
						{
							__instance.BlastLookAtEnemy();
						}
						if (Action07_Attack.NearestEnemy != null && Vector3.Distance(__instance.transform.position, Action07_Attack.NearestEnemy.position) < 50f)
						{
							RadsamuEnemy enemy = HomingAttackControl.TargetObject.GetComponentInParent<RadsamuEnemy>();
							if (enemy != null && !__instance.Player.Grounded)
							{
								__instance.Player.rigid.velocity = new Vector3(0, (enemy.Rigid.velocity + (enemy.Rigid.transform.position - __instance.Player.rigid.transform.position)).y, 0);
							}
						}
					}
				}
			}
			else
				movedFwd = false;
		}
	}
	[HarmonyPatch(typeof(ActionManager))]
	[HarmonyPatch("ChangeAction")]
	class ResetFloatMultiHit
	{
		private static void Prefix(int ActionToChange)
		{
			if (MasteryMod.DifficultyIsMastery())
			{
				if (ActionToChange == 7)
					GiveFloatChargeShot3.MakeFloatMultiSpikeSmall(false);
			}

		}
	}

	[HarmonyPatch(typeof(Action10Control_Blast))]
	[HarmonyPatch("Start")]
	class GiveFloatChargeShotGlow
	{
		private static void Prefix(Action10Control_Blast __instance, ref float ___Counter)
		{
			if (MasteryMod.DifficultyIsMastery())
			{
				Transform accessories = CharacterAnimatorChange.StaticReference.Skins[2].transform.Find("Float_Acessories");
				if (accessories != null)
				{
					var accessoriesRenderer = accessories.GetComponent<SkinnedMeshRenderer>();
					SkinnedMeshRenderer[] newArray = new SkinnedMeshRenderer[__instance.PlayerModel.Length + 1];
					__instance.PlayerModel.CopyTo(newArray, 0);
					newArray[__instance.PlayerModel.Length] = accessoriesRenderer;
					__instance.PlayerModel = newArray;
				}
			}
		}
	}
}