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
        public string ScriptName => Handle.LuaScriptName;

        public LuaEventHandler OnStart => Handle.OnStart;

        public LuaEventHandler OnDestroy => Handle.OnDestroyed;

        public LuaEventHandler OnTriggerEnter => Handle.OnAnyTriggerEnter;
        public LuaEventHandler OnTriggerStay => Handle.OnAnyTriggerStay;
        public LuaEventHandler OnTriggerExit => Handle.OnAnyTriggerExit;

        public LuaEventHandler OnPlayerTriggerEnter => Handle.OnPlayerTriggerEnter;
        public LuaEventHandler OnPlayerTriggerStay => Handle.OnPlayerTriggerStay;
        public LuaEventHandler OnPlayerTriggerExit => Handle.OnPlayerTriggerExit;

        public new ScriptBehavior Handle = null;

        [MoonSharpHidden]
        public LuaScriptBehavior(ScriptBehavior scriptBehavior, Script script) : base(scriptBehavior, script)
        {
            Handle = scriptBehavior;
        }

        internal static LuaScriptBehavior CastMethod(ScriptBehavior scriptBehavior)
        {
            return new LuaScriptBehavior(scriptBehavior, LuaManager.Instance.GlobalScript);
        }
    }
}
