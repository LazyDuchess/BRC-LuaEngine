﻿using LuaEngine.Mono;
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
                return new LuaGameObject(ScriptGameObjectValue.Value);
            }
            set
            {
                ScriptGameObjectValue.Value = value.Handle;
            }
        }
        public ScriptGameObjectValue ScriptGameObjectValue = null;
        [MoonSharpHidden]
        public LuaScriptGameObjectValue(ScriptGameObjectValue value, Script script) : base(value, script)
        {
            ScriptGameObjectValue = value;
        }

        internal static LuaScriptGameObjectValue CastMethod(ScriptGameObjectValue gameObjectValue)
        {
            return new LuaScriptGameObjectValue(gameObjectValue, LuaManager.Instance.GlobalScript);
        }
    }
}
