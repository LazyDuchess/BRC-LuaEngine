using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reptile;

namespace LuaEngine.Modules
{
    public class LuaCore : ILuaModule
    {
        private LuaBindings _bindings = null;
        public void OnRegister(Script script)
        {
            _bindings = new LuaBindings(script);
            Core.OnAlwaysUpdate += Core_OnAlwaysUpdate;
            Core.OnFixedUpdate += Core_OnFixedUpdate;
            Core.OnUpdate += Core_OnUpdate;
            Core.OnLateUpdate += Core_OnLateUpdate;
            script.Globals["Core"] = _bindings;
        }

        private void Core_OnAlwaysUpdate()
        {
            _bindings.OnAlwaysUpdate.Invoke();
        }

        private void Core_OnFixedUpdate()
        {
            _bindings.OnFixedUpdate.Invoke();
        }

        private void Core_OnUpdate()
        {
            _bindings.OnUpdate.Invoke();
        }

        private void Core_OnLateUpdate()
        {
            _bindings.OnLateUpdate.Invoke();
        }

        [MoonSharpUserData]
        public class LuaBindings
        {
            public float DeltaTime => Core.dt;
            public LuaEventHandler OnAlwaysUpdate = null;
            public LuaEventHandler OnFixedUpdate = null;
            public LuaEventHandler OnUpdate = null;
            public LuaEventHandler OnLateUpdate = null;

            [MoonSharpHidden]
            public LuaBindings(Script script)
            {
                OnAlwaysUpdate = new(script);
                OnFixedUpdate = new(script);
                OnUpdate = new(script);
                OnLateUpdate = new(script);
            }
        }
    }
}
