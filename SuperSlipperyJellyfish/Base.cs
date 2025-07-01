using System;
using BepInEx;
using HarmonyLib;
using UnityEngine;
using UnityEditor;
using BepInEx.Logging;
using BepInEx.Configuration;

namespace SuperSlipperyJellyfish
{
    [BepInPlugin(ModInfo.guid, ModInfo.name, ModInfo.ver)]
    public class Base : BaseUnityPlugin
    {
        private readonly Harmony harmony = new Harmony(ModInfo.guid);
        public static ConfigEntry<int> multiplier;
        public static ConfigEntry<bool> volume;
        void Awake()
        {
            Log.Init(ModInfo.initMessage);
            multiplier = Config.Bind("Super Slippery Jellyfish", "Multiplier", 2, "The multiplier the jellyfish slipperiness is multiplied by. Is 2 by default.");
            volume = Config.Bind("Super Slippery Jellyfish", "Multiply volume?", true, "Is the SFX's volume also multiplied by the multiplier? Is true by default.");
            harmony.PatchAll(typeof(JellyfishPatch));
        }
    }
}
