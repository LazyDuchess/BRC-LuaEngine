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
        public bool IsActiveAndEnabled => _handle.isActiveAndEnabled;
        public bool Enabled
        {
            get
            {
                return _handle.enabled;
            }
            set
            {
                _handle.enabled = true;
            }
        }
        private Behaviour _handle = null;
        [MoonSharpHidden]
        public LuaBuiltInBehaviour(Behaviour behaviour, Script script) : base(behaviour, script)
        {
            _handle = behaviour;
        }
    }
}
