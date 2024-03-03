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
    public class LuaScriptValue : LuaBuiltInBehaviour
    {
        public string Name
        {
            get
            {
                return Handle.Name;
            }
            set
            {
                Handle.Name = value;
            }
        }
        public new ScriptValue Handle = null;
        [MoonSharpHidden]
        public LuaScriptValue(ScriptValue value, Script script) : base(value, script)
        {
            Handle = value;
        }
    }
}
