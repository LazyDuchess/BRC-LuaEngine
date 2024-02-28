using LuaEngine.Mono;
using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace LuaEngine
{
    [MoonSharpUserData]
    public class LuaScriptBehavior : LuaBuiltInComponent
    {
        public string ScriptName => _handle.LuaScriptName;
        private ScriptBehavior _handle = null;

        [MoonSharpHidden]
        public LuaScriptBehavior(ScriptBehavior scriptBehavior, Script script) : base(scriptBehavior, script)
        {
            _handle = scriptBehavior;
        }
    }
}
