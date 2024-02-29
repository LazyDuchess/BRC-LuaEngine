using LuaEngine.Mono;
using Reptile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace LuaEngine
{
    public static class LuaCastFactory
    {
        private static Dictionary<Type, LuaCast> CastFromCSharpType = [];
        private static Dictionary<string, LuaCast> CastFromLuaTypeName = [];

        internal static void Initialize()
        {
            RegisterCasts(
                LuaCast.Create<LuaPlayer, Player>(LuaPlayer.CastMethod),
                LuaCast.Create<LuaScriptBehavior, ScriptBehavior>(LuaScriptBehavior.CastMethod)
                );
        }

        public static Type GetCSharpTypeFromLuaTypeName(string luaTypeName)
        {
            if (CastFromLuaTypeName.TryGetValue(luaTypeName, out var cast))
                return cast.CSharpType;
            return null;
        }

        public static T CastCSharpTypeToLuaType<T>(object CSharpObject) where T : class
        {
            if (CastFromCSharpType.TryGetValue(CSharpObject.GetType(), out var cast))
                return cast.CastMethod(CSharpObject) as T;
            return null;
        }

        public static void RegisterCasts(params LuaCast[] casts)
        {
            foreach (var cast in casts)
            {
                CastFromCSharpType[cast.CSharpType] = cast;
                CastFromLuaTypeName[cast.LuaType.Name] = cast;
            }
        }
    }
}
