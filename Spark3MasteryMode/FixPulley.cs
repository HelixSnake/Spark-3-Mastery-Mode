using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using MelonLoader;
using UnityEngine;

namespace Spark3MasteryMode
{
    [HarmonyPatch(typeof(Pulley))]
    [HarmonyPatch("OnTriggerEnter")]
    class FixPully
    {
        private static bool Prefix(Collider col, Pulley __instance, ref bool ___Pull, ref ActionManager ___Action, ref float ___TruePullSpeed,
			ref PlayerBhysics ___Player, ref bool ___KeepMoving, ref float ___PulleyTime)
        {
            if (MasteryMod.DifficultyIsMastery())
            {
				if (col.tag == "Player")
				{
					if ((float)PlayerHealthAndStats.PlayerHP <= -1f)
					{
						PlayerHealthAndStats.PlayerHP = 0;
					}
					if (!___Pull && PlayerHealthAndStats.PlayerHP >= 0 && ___Action.Action != 4 && ___Action.Action != 5 && ___Action.Action != 5)
					{
						Action14_Gimmicks.DifferentPulley = false;
						__instance.Ended = false;
						___TruePullSpeed = Vector3.Scale(___Player.rigid.velocity, __instance.transform.right * -__instance.PulleyDir).magnitude;
						__instance.ReleaseAudio.Play();
						Debug.Log("PULL SPEED MAG: " + Vector3.Scale(___Player.rigid.velocity, __instance.transform.right * -__instance.PulleyDir).magnitude.ToString());
						if (___TruePullSpeed < __instance.PulleyMinSpeed)
						{
							___TruePullSpeed = __instance.PulleyMinSpeed;
						}
						else if (___TruePullSpeed > __instance.PulleyMaxSpeed)
						{
							___TruePullSpeed = __instance.PulleyMaxSpeed;
						}
						___Player.GetComponent<ActionManager>().ChangeAction(14);
						Pulley.PullEnded = false;
						Pulley.IsPulling = true;
						Action14_Gimmicks.Gimmick = 0;
						___Pull = true;
						___KeepMoving = false;
						___PulleyTime = 0f;
					}
				}
				return false;
			}
			return true;
        }
    }
}