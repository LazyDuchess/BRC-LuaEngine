using LuaEngine.Mono;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace LuaEngine
{
    public static class LuaUtility
    {

        public static void OnInstantiate(GameObject gameObject)
        {
            var luaScripts = gameObject.GetComponentsInChildren<ScriptBehavior>(true);
            foreach(var luaScript in luaScripts)
            {
                luaScript.Restart();
            }
        }
    }
}
