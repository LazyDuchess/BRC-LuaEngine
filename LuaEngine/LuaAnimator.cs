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
        public new Animator Handle = null;

        [MoonSharpHidden]
        public LuaAnimator(Animator animator, Script script) : base(animator, script)
        {
            Handle = animator;
        }

        internal static LuaAnimator CastMethod(Animator animator)
        {
            return new LuaAnimator(animator, LuaManager.Instance.GlobalScript);
        }

        public void SetTrigger(string name)
        {
            Handle.SetTrigger(name);
        }

        public void SetBool(string name, bool value)
        {
            Handle.SetBool(name, value);
        }

        public bool GetBool(string name)
        {
            return Handle.GetBool(name);
        }

        public void SetInteger(string name, int value)
        {
            Handle.SetInteger(name, value);
        }

        public int GetInteger(string name)
        {
            return Handle.GetInteger(name);
        }

        public void SetFloat(string name, float value)
        {
            Handle.SetFloat(name, value);
        }

        public float GetFloat(string name)
        {
            return Handle.GetFloat(name);
        }
    }
}
