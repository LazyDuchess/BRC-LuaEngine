using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuaEngine
{
    public interface ILuaModule
    {
        public void OnRegister(Script script);
    }
}
