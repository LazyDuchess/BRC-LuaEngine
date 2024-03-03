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
    }
}
