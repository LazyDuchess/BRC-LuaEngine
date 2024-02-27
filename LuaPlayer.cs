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
    public class LuaPlayer : LuaBuiltInComponent
    {
        public bool IsTooBusyForSuddenCutscenes => _handle.IsTooBusyForSuddenCutscenes();
        public bool IsBusyWithSequence => _handle.IsBusyWithSequence();
        public bool IsDead => _handle.IsDead();
        public bool IsAI
        {
            get { return _handle.isAI; }
            set { _handle.isAI = value; }
        }
        public float BoostCharge
        {
            get { return _handle.boostCharge; }
            set { _handle.boostCharge = value; }
        }
        public float MaxBoostCharge
        {
            get { return _handle.maxBoostCharge; }
            set { _handle.maxBoostCharge = value; }
        }
        private Player _handle = null;

        [MoonSharpHidden]
        public LuaPlayer(Player player, Script script) : base(player, script)
        {
            _handle = player;
        }

        public void AddBoostCharge(float amount)
        {
            _handle.AddBoostCharge(amount);
        }
    }
}
