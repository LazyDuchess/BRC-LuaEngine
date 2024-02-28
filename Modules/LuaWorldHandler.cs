using LuaEngine.Components;
using MoonSharp.Interpreter;
using Reptile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LuaEngine.Modules.LuaStageManager;

namespace LuaEngine.Modules
{
    public class LuaWorldHandler : ILuaModule
    {
        private LuaBindings _bindings = null;
        public void OnRegister(Script script)
        {
            _bindings = new(script);
            script.Globals["WorldHandler"] = _bindings;
        }

        [MoonSharpUserData]
        public class LuaBindings
        {
            public LuaGameObject CurrentCamera
            {
                get
                {
                    var worldHandler = WorldHandler.instance;
                    if (worldHandler == null) return null;
                    return LuaHooks.GetOrMake(worldHandler.CurrentCamera.gameObject, _script).LuaGameObject;
                }
            }
            public LuaPlayer CurrentPlayer
            {
                get
                {
                    var worldHandler = WorldHandler.instance;
                    if (worldHandler == null) return null;
                    var player = worldHandler.GetCurrentPlayer();
                    if (player == null) return null;
                    return new LuaPlayer(player, _script);
                }
            }
            private Script _script = null;

            [MoonSharpHidden]
            public LuaBindings(Script script)
            {
                _script = script;
            }
        }
    }
}
