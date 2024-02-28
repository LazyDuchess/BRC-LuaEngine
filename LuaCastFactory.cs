using LuaEngine.Mono;
using Reptile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace LuaEngine
{
    public static class LuaCastFactory
    {
        public static bool TryCastComponent(Component component, string luaComponentName, out LuaBuiltInComponent result)
        {
            var script = LuaManager.Instance.GlobalScript;
            result = null;
            switch (luaComponentName)
            {
                case "LuaPlayer":
                    var player = component as Player;
                    if (player != null)
                    {
                        result = new LuaPlayer(player, script);
                        return true;
                    }
                    return false;

                case "LuaScriptBehavior":
                    var scriptBehavior = component as ScriptBehavior;
                    if (scriptBehavior != null)
                    {
                        result = new LuaScriptBehavior(scriptBehavior, script);
                        return true;
                    }
                    return false;
            }
            return false;
        }
    }
}
