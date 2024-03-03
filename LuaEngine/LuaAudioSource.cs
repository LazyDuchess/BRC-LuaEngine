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
        public AudioSource AudioSource = null;

        [MoonSharpHidden]
        public LuaAudioSource(AudioSource audio, Script script) : base(audio, script)
        {
            AudioSource = audio;
        }

        internal static LuaAudioSource CastMethod(AudioSource audioSource)
        {
            return new LuaAudioSource(audioSource, LuaManager.Instance.GlobalScript);
        }

        public void Play()
        {
            AudioSource.Play();
        }

        public void Stop()
        {
            AudioSource.Stop();
        }

        public void Pause()
        {
            AudioSource.Pause();
        }

        public void SetMixerGroup(int mixerGroup)
        {
            AudioSource.outputAudioMixerGroup = Core.Instance.AudioManager.mixerGroups[mixerGroup];
        }
    }
}
