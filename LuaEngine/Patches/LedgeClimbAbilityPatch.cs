using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using Reptile;

namespace LuaEngine.Patches
{
    [HarmonyPatch(typeof(LedgeClimbAbility))]
    internal static class LedgeClimbAbilityPatch
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(LedgeClimbAbility.CheckActivation))]
        private static bool CheckActivation_Prefix(ref bool __result, LedgeClimbAbility __instance)
        {
            var luaPlayer = PlayerLuaEngineComponent.Get(__instance.p);
            if (luaPlayer == null) return true;
            if (luaPlayer.AllowClimb) return true;
            __result = false;
            return false;
        }
    }
}
