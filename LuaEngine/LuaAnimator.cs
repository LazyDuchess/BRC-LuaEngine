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
    public class LuaAnimator : LuaBuiltInBehaviour
    {
        private Animator _handle = null;

        [MoonSharpHidden]
        public LuaAnimator(Animator animator, Script script) : base(animator, script)
        {
            _handle = animator;
        }

        internal static LuaAnimator CastMethod(Animator animator)
        {
            return new LuaAnimator(animator, LuaManager.Instance.GlobalScript);
        }

        public void SetTrigger(string name)
        {
            _handle.SetTrigger(name);
        }

        public void SetBool(string name, bool value)
        {
            _handle.SetBool(name, value);
        }

        public bool GetBool(string name)
        {
            return _handle.GetBool(name);
        }

        public void SetInteger(string name, int value)
        {
            _handle.SetInteger(name, value);
        }

        public int GetInteger(string name)
        {
            return _handle.GetInteger(name);
        }

        public void SetFloat(string name, float value)
        {
            _handle.SetFloat(name, value);
        }

        public float GetFloat(string name)
        {
            return _handle.GetFloat(name);
        }
    }
}
