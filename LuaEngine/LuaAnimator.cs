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
        public Animator Animator = null;

        [MoonSharpHidden]
        public LuaAnimator(Animator animator, Script script) : base(animator, script)
        {
            Animator = animator;
        }

        internal static LuaAnimator CastMethod(Animator animator)
        {
            return new LuaAnimator(animator, LuaManager.Instance.GlobalScript);
        }

        public void SetTrigger(string name)
        {
            Animator.SetTrigger(name);
        }

        public void SetBool(string name, bool value)
        {
            Animator.SetBool(name, value);
        }

        public bool GetBool(string name)
        {
            return Animator.GetBool(name);
        }

        public void SetInteger(string name, int value)
        {
            Animator.SetInteger(name, value);
        }

        public int GetInteger(string name)
        {
            return Animator.GetInteger(name);
        }

        public void SetFloat(string name, float value)
        {
            Animator.SetFloat(name, value);
        }

        public float GetFloat(string name)
        {
            return Animator.GetFloat(name);
        }
    }
}
