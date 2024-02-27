using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using LuaEngine.Components;
using Reptile;
using UnityEngine;

namespace LuaEngine.Patches
{
    [HarmonyPatch(typeof(Player))]
    internal static class PlayerPatch
    {
        [HarmonyPostfix]
        [HarmonyPatch(nameof(Player.OnTriggerEnter))]
        private static void OnTriggerEnter_Postfix(Player __instance, Collider other)
        {
            var luaHooks = LuaUtility.GetComponent<LuaHooks>(other.gameObject);
            if (luaHooks == null) return;
            luaHooks.OnPlayerTriggerEnter.Invoke(new LuaPlayer(__instance, luaHooks.Script));
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(Player.OnTriggerStay))]
        private static void OnTriggerStay_Postfix(Player __instance, Collider other)
        {
            var luaHooks = LuaUtility.GetComponent<LuaHooks>(other.gameObject);
            if (luaHooks == null) return;
            luaHooks.OnPlayerTriggerEnter.Invoke(new LuaPlayer(__instance, luaHooks.Script));
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(Player.OnTriggerExit))]
        private static void OnTriggerExit_Postfix(Player __instance, Collider other)
        {
            var luaHooks = LuaUtility.GetComponent<LuaHooks>(other.gameObject);
            if (luaHooks == null) return;
            luaHooks.OnPlayerTriggerEnter.Invoke(new LuaPlayer(__instance, luaHooks.Script));
        }
    }
}
