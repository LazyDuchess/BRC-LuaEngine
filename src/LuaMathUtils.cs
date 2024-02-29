using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace LuaEngine
{
    public static class LuaMathUtils
    {
        public static Vector3 TableToVector3(Table table)
        {
            return new Vector3((float)table.Get("x").CastToNumber(), (float)table.Get("y").CastToNumber(), (float)table.Get("z").CastToNumber());
        }
        public static Table Vector3ToTable(Vector3 vector)
        {
            var script = LuaManager.Instance.GlobalScript;
            var luaClass = script.Globals["Vector3"] as Table;
            var result = script.Call(luaClass.Get("New"), vector.x, vector.y, vector.z);
            return result.Table;
        }
    }
}
