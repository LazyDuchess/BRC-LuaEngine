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
        public string ScriptName => ScriptBehavior.LuaScriptName;

        public LuaEventHandler OnStart => ScriptBehavior.OnStart;

        public LuaEventHandler OnDestroy => ScriptBehavior.OnDestroyed;

        public LuaEventHandler OnCollisionEnter => ScriptBehavior.OnAnyCollisionEnter;
        public LuaEventHandler OnCollisionStay => ScriptBehavior.OnAnyCollisionStay;
        public LuaEventHandler OnCollisionExit => ScriptBehavior.OnAnyCollisionExit;

        public LuaEventHandler OnPlayerCollisionEnter => ScriptBehavior.OnPlayerCollisionEnter;
        public LuaEventHandler OnPlayerCollisionStay => ScriptBehavior.OnPlayerCollisionStay;
        public LuaEventHandler OnPlayerCollisionExit => ScriptBehavior.OnPlayerCollisionExit;

        public LuaEventHandler OnTriggerEnter => ScriptBehavior.OnAnyTriggerEnter;
        public LuaEventHandler OnTriggerStay => ScriptBehavior.OnAnyTriggerStay;
        public LuaEventHandler OnTriggerExit => ScriptBehavior.OnAnyTriggerExit;

        public LuaEventHandler OnPlayerTriggerEnter => ScriptBehavior.OnPlayerTriggerEnter;
        public LuaEventHandler OnPlayerTriggerStay => ScriptBehavior.OnPlayerTriggerStay;
        public LuaEventHandler OnPlayerTriggerExit => ScriptBehavior.OnPlayerTriggerExit;

        public ScriptBehavior ScriptBehavior = null;

        [MoonSharpHidden]
        public LuaScriptBehavior(ScriptBehavior scriptBehavior, Script script) : base(scriptBehavior, script)
        {
            ScriptBehavior = scriptBehavior;
        }

        internal static LuaScriptBehavior CastMethod(ScriptBehavior scriptBehavior)
        {
            return new LuaScriptBehavior(scriptBehavior, LuaManager.Instance.GlobalScript);
        }
    }
}
