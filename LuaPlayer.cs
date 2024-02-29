using MonoMod.Cil;
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
    public class LuaPlayer : LuaBuiltInBehaviour
    {
        public float NormalizedHP => _handle.GetNormalizedHP();
        public float MaxHP
        {
            get
            {
                return _handle.maxHP;
            }
            set
            {
                _handle.maxHP = value;
            }
        }
        public float HP
        {
            get
            {
                return _handle.HP;
            }
            set
            {
                _handle.HP = value;
            }
        }
        public string AbilityName
        {
            get
            {
                if (_handle.ability == null)
                    return null;
                return _handle.ability.GetType().Name;
            }
        }
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

        internal static LuaPlayer CastMethod(Player player)
        {
            return new LuaPlayer(player, LuaManager.Instance.GlobalScript);
        }

        public void AddBoostCharge(float amount)
        {
            _handle.AddBoostCharge(amount);
        }

        public void SetRotation(Table forward)
        {
            _handle.SetRotation(LuaMathUtils.TableToVector3(forward));
        }

        public void SetRotationHard(Table forward)
        {
            _handle.SetRotHard(LuaMathUtils.TableToVector3(forward));
        }

        public void GetHit(int damage, Table damageDirection, string knockbackType)
        {
            var parsedKnockbackType = KnockbackAbility.KnockbackType.NO_KNOCKBACK;
            switch (knockbackType)
            {
                case "Stationary":
                    parsedKnockbackType = KnockbackAbility.KnockbackType.STATIONARY;
                    break;
                case "Far":
                    parsedKnockbackType = KnockbackAbility.KnockbackType.FAR;
                    break;
                case "Big":
                    parsedKnockbackType = KnockbackAbility.KnockbackType.BIG;
                    break;
                case "ShieldBash":
                    parsedKnockbackType = KnockbackAbility.KnockbackType.SHIELDBASH;
                    break;
                case "Fall":
                    parsedKnockbackType = KnockbackAbility.KnockbackType.FALL;
                    break;
                case "None":
                    parsedKnockbackType = KnockbackAbility.KnockbackType.NO_KNOCKBACK;
                    break;
            }
            _handle.GetHit(damage, LuaMathUtils.TableToVector3(damageDirection), parsedKnockbackType);
        }

        public void ChangeHP(int damage)
        {
            _handle.ChangeHP(damage);
        }
    }
}
