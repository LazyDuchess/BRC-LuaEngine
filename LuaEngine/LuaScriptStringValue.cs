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
    public class LuaScriptStringValue : LuaScriptValue
    {
        public string Value
        {
            get
            {
                return Handle.Value;
            }
            set
            {
                Handle.Value = value;
            }
        }
        internal ScriptStringValue Handle = null;
        [MoonSharpHidden]
        public LuaScriptStringValue(ScriptStringValue value, Script script) : base(value, script)
        {
            Handle = value;
        }

        internal static LuaScriptStringValue CastMethod(ScriptStringValue stringValue)
        {
            return new LuaScriptStringValue(stringValue, LuaManager.Instance.GlobalScript);
        }
    }
}
