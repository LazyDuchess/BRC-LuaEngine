using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Playables;

namespace LuaEngine
{
    [MoonSharpUserData]
    public class LuaPlayableDirector : LuaBuiltInBehaviour
    {
        public PlayableDirector PlayableDirector = null;

        [MoonSharpHidden]
        public LuaPlayableDirector(PlayableDirector playableDirector, Script script) : base(playableDirector, script)
        {
            PlayableDirector = playableDirector;
        }

        internal static LuaPlayableDirector CastMethod(PlayableDirector playableDirector)
        {
            return new LuaPlayableDirector(playableDirector, LuaManager.Instance.GlobalScript);
        }
    }
}
