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
        private AudioSource _handle = null;

        [MoonSharpHidden]
        public LuaAudioSource(AudioSource audio, Script script) : base(audio, script)
        {
            _handle = audio;
        }

        public void Play()
        {
            _handle.Play();
        }

        public void Stop()
        {
            _handle.Stop();
        }

        public void Pause()
        {
            _handle.Pause();
        }

        public void SetMixerGroup(int mixerGroup)
        {
            _handle.outputAudioMixerGroup = Core.Instance.AudioManager.mixerGroups[mixerGroup];
        }
    }
}
