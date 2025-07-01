using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using HarmonyLib;
using Photon.Pun;
using System.ComponentModel;

namespace SuperSlipperyJellyfish
{
    internal class JellyfishPatch
    {
        private static Rigidbody GetBodypartRig(Character character, BodypartType bp)
        {
            return character.refs.ragdoll.partDict[bp].Rig;
        }

        [HarmonyPatch(typeof(SlipperyJellyfish), nameof(SlipperyJellyfish.Trigger))]
        [HarmonyPrefix]
        private static bool TriggerPatch(int targetID, SlipperyJellyfish __instance)
        {
            Character component = PhotonView.Find(targetID).GetComponent<Character>();
            if (!(component == null))
            {
                Log.Write.LogInfo($"Running original code with {Base.multiplier.Value}x force multiplier");
                Rigidbody bodypartRig = GetBodypartRig(component, BodypartType.Foot_R);
                Rigidbody bodypartRig2 = GetBodypartRig(component, BodypartType.Foot_L);
                Rigidbody bodypartRig3 = GetBodypartRig(component, BodypartType.Hip);
                Rigidbody bodypartRig4 = GetBodypartRig(component, BodypartType.Head);
                component.RPCA_Fall(2f);
                bodypartRig.AddForce((component.data.lookDirection_Flat + Vector3.up) * (200f), ForceMode.Impulse);
                bodypartRig2.AddForce((component.data.lookDirection_Flat + Vector3.up) * (200f), ForceMode.Impulse);
                bodypartRig3.AddForce(Vector3.up * (1500f * Base.multiplier.Value), ForceMode.Impulse);
                bodypartRig4.AddForce(component.data.lookDirection_Flat * -300f, ForceMode.Impulse);
                component.refs.afflictions.AddStatus(CharacterAfflictions.STATUSTYPE.Poison, 0.05f, true);
                for (int i = 0; i < __instance.slipSFX.Length; i++)
                {
                    if (Base.volume.Value)
                        __instance.slipSFX[i].settings.volume = Base.multiplier.Value;
                    __instance.slipSFX[i].Play(__instance.transform.position);
                }
            }
            return false;
        }
    }
}
