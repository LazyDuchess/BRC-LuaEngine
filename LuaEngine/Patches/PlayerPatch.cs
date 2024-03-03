using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using LuaEngine.Mono;
using Reptile;
using UnityEngine;

namespace LuaEngine.Patches
{
    [HarmonyPatch(typeof(Player))]
    internal static class PlayerPatch
    {
        [HarmonyPostfix]
        [HarmonyPatch(nameof(Player.Init))]
        private static void Init_Postfix(Player __instance)
        {
            if (__instance.gameObject.GetComponent<PlayerLuaEngineComponent>() != null)
                return;
            var luaEngineComponent = __instance.gameObject.AddComponent<PlayerLuaEngineComponent>();
            luaEngineComponent.Player = __instance;
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(Player.LandCombo))]
        private static void LandCombo_Prefix(Player __instance)
        {
            if (!__instance.IsComboing())
                return;
            var luaEnginePlayer = PlayerLuaEngineComponent.Get(__instance);
            if (luaEnginePlayer == null)
                return;
            luaEnginePlayer.OnLandCombo?.Invoke();
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(Player.DropCombo))]
        private static void DropCombo_Prefix(Player __instance)
        {
            if (!__instance.IsComboing())
                return;
            var luaEnginePlayer = PlayerLuaEngineComponent.Get(__instance);
            if (luaEnginePlayer == null)
                return;
            luaEnginePlayer.OnDropCombo?.Invoke();
        }
    }
}
