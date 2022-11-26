using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;

namespace Spark3MasteryMode
{
	[HarmonyPatch(typeof(HystoriaBossFight))]
	[HarmonyPatch("FixedUpdate")]
	class FixHystoriaBossSoftLock
	{
		private static void Postfix(HystoriaBossFight __instance, ref float ___c, ref float ___c2, ref int ___boss)
		{
			if (MasteryMod.DifficultyIsMastery())
			{
				if (___c > __instance.FirstPhaseBossFightDuration && ___c2 > __instance.TransitionTime)
				{
					if (__instance.Ryno.HP <= 0f && __instance.Double.HP <= 0f && ___boss == 1) // FixedUpdate is 1 run into the softlock loop
					{
						___boss = 0; // break out of softlock; set boss to Double so it will transition to EJ correctly
					}
				}
			}
		}
	}
}
