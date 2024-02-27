using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace LuaEngine.Components
{
    public class LuaHooks : MonoBehaviour
    {
        public LuaGameObject LuaGameObject = null;
        public LuaEventHandler OnDestroyed = null;

        public static LuaHooks GetOrMake(GameObject gameObject, Script script)
        {
            var comp = gameObject.GetComponent<LuaHooks>();
            if (comp != null)
                return comp;
            comp = gameObject.AddComponent<LuaHooks>();
            comp.Initialize(script);
            return comp;
        }

        public void Initialize(Script script)
        {
            LuaGameObject = new(this);
            OnDestroyed = new(script);
        }

        private void OnDestroy()
        {
            OnDestroyed.Invoke(LuaGameObject);
        }
    }
}
