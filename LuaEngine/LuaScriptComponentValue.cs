using LuaEngine.Mono;
using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuaEngine
{
    [MoonSharpUserData]
    public class LuaScriptComponentValue : LuaScriptValue
    {
        public LuaBuiltInComponent Value
        {
            get
            {
                return LuaCastFactory.CastCSharpTypeToLuaType<LuaBuiltInComponent>(ScriptComponentValue.Value);
            }
            set
            {
                ScriptComponentValue.Value = value.Component;
            }
        }
        public ScriptComponentValue ScriptComponentValue = null;
        [MoonSharpHidden]
        public LuaScriptComponentValue(ScriptComponentValue value, Script script) : base(value, script)
        {
            ScriptComponentValue = value;
        }
    }
}
