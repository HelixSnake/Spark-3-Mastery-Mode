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
	[HarmonyPatch("CreateBlast")]
	class FixShotPosition
	{

		private static bool Prefix(bool big, Action10Control_Blast __instance, ref Vector3 ___EnemyDir)
		{
			if (MasteryMod.DifficultyIsMastery())
			{
				Vector3 enemyDir = ___EnemyDir;
				enemyDir.y = 0f;
				Quaternion rotation = Quaternion.LookRotation(enemyDir.normalized, __instance.transform.up);
				__instance.Anim.transform.rotation = rotation;
				Quaternion rotation2 = Quaternion.LookRotation(___EnemyDir, __instance.transform.up);
				Vector3 offset = Vector3.Normalize(___EnemyDir) * 2.9f;
				if (CharacterAnimatorChange.Character != 0)
				{
					if (CharacterAnimatorChange.Character == 4)
					{
						if (big)
						{
							Debug.Log("BigFireSfarx");
							__instance.Anim.SetTrigger("SfarxLazer");
							return false;
						}
						UnityEngine.Object.Instantiate<GameObject>(__instance.OtherBlasts[0], __instance.BlastStartPosition.position + offset, rotation2).transform.parent = null;
					}
					return false;
				}
				if (big)
				{
					HedgeCamera.Shakeforce = 2.5f;
					UnityEngine.Object.Instantiate<GameObject>(__instance.BigBlast, __instance.BlastStartPosition.position + offset, rotation2).transform.parent = null;
					return false;
				}
				UnityEngine.Object.Instantiate<GameObject>(__instance.SmallBlast, __instance.BlastStartPosition.position + offset, rotation2).transform.parent = null;
				return false;
			}
			return true;
		}
	}
}
