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
                return Handle.Value;
            }
            set
            {
                Handle.Value = value;
            }
        }
        internal ScriptNumberValue Handle = null;
        [MoonSharpHidden]
        public LuaScriptNumberValue(ScriptNumberValue value, Script script) : base(value, script)
        {
            Handle = value;
        }

        internal static LuaScriptNumberValue CastMethod(ScriptNumberValue numberValue)
        {
            return new LuaScriptNumberValue(numberValue, LuaManager.Instance.GlobalScript);
        }
    }
}
