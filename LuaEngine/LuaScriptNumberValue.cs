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
    public class LuaScriptNumberValue : LuaScriptValue
    {
        public double Value
        {
            get
            {
                return ScriptNumberValue.Value;
            }
            set
            {
                ScriptNumberValue.Value = value;
            }
        }
        public ScriptNumberValue ScriptNumberValue = null;
        [MoonSharpHidden]
        public LuaScriptNumberValue(ScriptNumberValue value, Script script) : base(value, script)
        {
            ScriptNumberValue = value;
        }

        internal static LuaScriptNumberValue CastMethod(ScriptNumberValue numberValue)
        {
            return new LuaScriptNumberValue(numberValue, LuaManager.Instance.GlobalScript);
        }
    }
}
