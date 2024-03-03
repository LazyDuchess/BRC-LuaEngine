using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace LuaEngine
{
    [MoonSharpUserData]
    public class LuaBuiltInComponent
    {
        public LuaGameObject GameObject = null;
        public Component Component = null;
        [MoonSharpHidden]
        public LuaBuiltInComponent(Component component, Script script)
        {
            Component = component;
            GameObject = new LuaGameObject(component.gameObject);
        }
    }
}
