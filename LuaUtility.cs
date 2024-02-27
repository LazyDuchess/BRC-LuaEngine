using LuaEngine.Components;
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
        public static T GetComponent<T>(GameObject gameObject) where T : Component
        {
            var comp = gameObject.GetComponent<T>();
            if (comp == null)
                return gameObject.GetComponentInParent<T>();
            return comp;
        }

        public static void RemoveAllLuaHooks(GameObject gameObject)
        {
            var luaHooks = gameObject.GetComponentsInChildren<LuaHooks>(true);
            foreach(var luaHook in luaHooks)
            {
                GameObject.DestroyImmediate(luaHook);
            }
        }
    }
}
