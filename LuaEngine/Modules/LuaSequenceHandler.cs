using MoonSharp.Interpreter;
using Reptile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace LuaEngine.Modules
{
    public class LuaSequenceHandler : ILuaModule
    {
        private LuaBindings _bindings;

        public void OnRegister(Script script)
        {
            _bindings = new(this);
            script.Globals["SequenceHandler"] = _bindings;
        }

        [MoonSharpUserData]
        public class LuaBindings
        {
            private LuaSequenceHandler _luaSequenceHandler = null;

            [MoonSharpHidden]
            public LuaBindings(LuaSequenceHandler luaSequenceHandler)
            {
                _luaSequenceHandler = luaSequenceHandler;
            }

            public void StartSequence(LuaPlayableDirector director, DynValue OnSequenceEnd = null, bool hidePlayer = true, bool interruptPlayer = true, bool instantly = false, bool pausePlayer = false, bool allowPhoneOnAfterSequence = true, bool skippable = true, bool lowerVolume = true)
            {
                var gameplayEventGO = new GameObject("Lua Sequence Event");
                var gameplayEvent = gameplayEventGO.AddComponent<SequenceEvent>();
                if (OnSequenceEnd != null)
                    gameplayEvent.OnSequenceEnd.Add(OnSequenceEnd);
                SequenceHandler.instance.StartEnteringSequence(director.PlayableDirector, gameplayEvent, hidePlayer, interruptPlayer, instantly, pausePlayer, allowPhoneOnAfterSequence, null, skippable, lowerVolume);
            }
        }
    }
}
