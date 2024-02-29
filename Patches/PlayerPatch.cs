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
        [HarmonyPatch(nameof(Player.OnTriggerEnter))]
        private static void OnTriggerEnter_Postfix(Player __instance, Collider other)
        {
            var scripts = other.GetComponentsInParent<ScriptBehavior>();
            foreach(var script in scripts)
            {
                script.OnPlayerTriggerEnter?.Invoke(new LuaPlayer(__instance, LuaManager.Instance.GlobalScript));
            }
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(Player.OnTriggerStay))]
        private static void OnTriggerStay_Postfix(Player __instance, Collider other)
        {
            var scripts = other.GetComponentsInParent<ScriptBehavior>();
            foreach (var script in scripts)
            {
                script.OnPlayerTriggerStay?.Invoke(new LuaPlayer(__instance, LuaManager.Instance.GlobalScript));
            }
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(Player.OnTriggerExit))]
        private static void OnTriggerExit_Postfix(Player __instance, Collider other)
        {
            var scripts = other.GetComponentsInParent<ScriptBehavior>();
            foreach (var script in scripts)
            {
                script.OnPlayerTriggerExit?.Invoke(new LuaPlayer(__instance, LuaManager.Instance.GlobalScript));
            }
        }
    }
}
