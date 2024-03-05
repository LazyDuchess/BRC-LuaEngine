using MoonSharp.Interpreter;
using Reptile;
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
        public double Time => PlayableDirector.time;
        public double Duration => PlayableDirector.duration;
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

        public void SkipTo(double time, bool keepGameplayCamera = true)
        {
            PlayableDirector.time = time;
            PlayableDirector.Evaluate();
            if (keepGameplayCamera)
            {
                if (WorldHandler.instance.CurrentCamera != WorldHandler.instance.GetCurrentPlayer().cam.cam)
                    WorldHandler.instance.CurrentCamera.gameObject.SetActive(false);
            }
        }

        public void Play()
        {
            PlayableDirector.Play();
        }

        public void Pause()
        {
            PlayableDirector.Pause();
        }

        public void Stop()
        {
            PlayableDirector.Stop();
        }
    }
}
