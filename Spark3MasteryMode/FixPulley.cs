using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using MelonLoader;
using UnityEngine;

namespace HelixBugFix
{
	[HarmonyPatch(typeof(Pulley))]
	[HarmonyPatch("SetPulleyPos")]
	class FixPullyCollider
	{
		private static void SetPulleyColliderEnabled(Transform pulley, bool enabled)
		{
			var colliders = pulley.parent.GetComponents<BoxCollider>();
			foreach (var collider in colliders)
			{
				collider.enabled = enabled;
			}
		}
		private static void Postfix(Pulley __instance, ref float ___PulleyPosition)
		{
			if (HelixFixMod.DifficultyIsNotMastery())
			{
				if (___PulleyPosition < 0.01f)
				{
					SetPulleyColliderEnabled(__instance.Pivot_1, false);
				}
				else
				{
					SetPulleyColliderEnabled(__instance.Pivot_1, true);
				}
				if (___PulleyPosition > 0.99f)
				{
					SetPulleyColliderEnabled(__instance.Pivot_2, false);
				}
				else
				{
					SetPulleyColliderEnabled(__instance.Pivot_2, true);
				}
			}
		}
	}
}