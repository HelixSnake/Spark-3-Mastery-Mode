using System;
using System.Collections.Generic;
using System.Linq;
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
		private const float FloatCooldownTime = 0.5f;
		private static void Prefix(Action10Control_Blast __instance, ref bool ___Buffer, ref bool ___Initial)
		{
			if (MasteryMod.DifficultyIsMastery())
			{
				if (CharacterAnimatorChange.Character == 2)
				{
					FloatCooldownCounter += Time.fixedDeltaTime;
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
	}
	[HarmonyPatch(typeof(Action10Control_Blast))]
	[HarmonyPatch("InitialActions")]
	class GiveFloatChargeShot2
	{
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
					}
				}
			}
		}
	}
	[HarmonyPatch(typeof(Action10Control_Blast))]
	[HarmonyPatch("BlastAction")]
	class GiveFloatChargeShot3
	{
		private static void Prefix(Action10Control_Blast __instance, ref float ___Counter)
		{
			if (MasteryMod.DifficultyIsMastery())
			{
				if (CharacterAnimatorChange.Character == 2)
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