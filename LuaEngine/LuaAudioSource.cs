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
    public class LuaAudioSource : LuaBuiltInBehaviour
    {
        public new AudioSource Handle = null;

        [MoonSharpHidden]
        public LuaAudioSource(AudioSource audio, Script script) : base(audio, script)
        {
            Handle = audio;
        }

        internal static LuaAudioSource CastMethod(AudioSource audioSource)
        {
            return new LuaAudioSource(audioSource, LuaManager.Instance.GlobalScript);
        }

        public void Play()
        {
            Handle.Play();
        }

        public void Stop()
        {
            Handle.Stop();
        }

        public void Pause()
        {
            Handle.Pause();
        }

        public void SetMixerGroup(int mixerGroup)
        {
            Handle.outputAudioMixerGroup = Core.Instance.AudioManager.mixerGroups[mixerGroup];
        }
    }
}
