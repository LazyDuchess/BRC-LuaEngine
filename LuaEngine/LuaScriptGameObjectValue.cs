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
    public class LuaScriptGameObjectValue : LuaScriptValue
    {
        public LuaGameObject Value
        {
            get
            {
                return new LuaGameObject(Handle.Value);
            }
            set
            {
                Handle.Value = value.Handle;
            }
        }
        public new ScriptGameObjectValue Handle = null;
        [MoonSharpHidden]
        public LuaScriptGameObjectValue(ScriptGameObjectValue value, Script script) : base(value, script)
        {
            Handle = value;
        }
    }
}
