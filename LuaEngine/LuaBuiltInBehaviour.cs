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
        public bool IsActiveAndEnabled => Behaviour.isActiveAndEnabled;
        public bool Enabled
        {
            get
            {
                return Behaviour.enabled;
            }
            set
            {
                Behaviour.enabled = true;
            }
        }
        public Behaviour Behaviour = null;
        [MoonSharpHidden]
        public LuaBuiltInBehaviour(Behaviour behaviour, Script script) : base(behaviour, script)
        {
            Behaviour = behaviour;
        }
    }
}
