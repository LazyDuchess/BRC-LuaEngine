using LuaEngine.Mono;
using MoonSharp.Interpreter;
using Reptile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace LuaEngine
{
    [MoonSharpUserData]
    public class LuaScriptBehavior : LuaBuiltInBehaviour
    {
        public string ScriptName => _handle.LuaScriptName;
        public LuaEventHandler OnDestroy => _handle.OnDestroyed;

        public LuaEventHandler OnTriggerEnter => _handle.OnAnyTriggerEnter;
        public LuaEventHandler OnTriggerStay => _handle.OnAnyTriggerStay;
        public LuaEventHandler OnTriggerExit => _handle.OnAnyTriggerExit;

        public LuaEventHandler OnPlayerTriggerEnter => _handle.OnPlayerTriggerEnter;
        public LuaEventHandler OnPlayerTriggerStay => _handle.OnPlayerTriggerStay;
        public LuaEventHandler OnPlayerTriggerExit => _handle.OnPlayerTriggerExit;

        private ScriptBehavior _handle = null;

        [MoonSharpHidden]
        public LuaScriptBehavior(ScriptBehavior scriptBehavior, Script script) : base(scriptBehavior, script)
        {
            _handle = scriptBehavior;
        }

        internal static LuaScriptBehavior CastMethod(ScriptBehavior scriptBehavior)
        {
            return new LuaScriptBehavior(scriptBehavior, LuaManager.Instance.GlobalScript);
        }
    }
}
