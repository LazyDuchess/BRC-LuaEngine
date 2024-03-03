using MoonSharp.Interpreter;
using Reptile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace LuaEngine
{
    [MoonSharpUserData]
    public class LuaBuiltInBehaviour : LuaBuiltInComponent
    {
        public bool IsActiveAndEnabled => Handle.isActiveAndEnabled;
        public bool Enabled
        {
            get
            {
                return Handle.enabled;
            }
            set
            {
                Handle.enabled = true;
            }
        }
        public new Behaviour Handle = null;
        [MoonSharpHidden]
        public LuaBuiltInBehaviour(Behaviour behaviour, Script script) : base(behaviour, script)
        {
            Handle = behaviour;
        }
    }
}
