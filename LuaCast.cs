using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace LuaEngine
{
    public class LuaCast
    {
        public Type CSharpType { get; private set; }
        public Type LuaType { get; private set; }
        public Func<object, object> CastMethod { get; private set; }

        public static LuaCast Create<TLua, TCSharp>(Func<TCSharp, TLua> castMethod) where TLua : class where TCSharp : class
        {
            var luaCast = new LuaCast();
            luaCast.CastMethod = castMethod as Func<object, object>;
            luaCast.LuaType = castMethod.Method.ReturnType;
            luaCast.CSharpType = castMethod.Method.GetParameters()[0].ParameterType;
            return luaCast;
        }
    }
}
