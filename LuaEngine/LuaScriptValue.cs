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
                return _handle.Name;
            }
            set
            {
                _handle.Name = value;
            }
        }
        private ScriptValue _handle = null;
        [MoonSharpHidden]
        public LuaScriptValue(ScriptValue value, Script script) : base(value, script)
        {
            _handle = value;
        }
    }
}
