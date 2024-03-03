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
        public int MoveStyleEquipped => (int)Handle.moveStyleEquipped;
        public int MoveStyle => (int)Handle.moveStyle;
        public float NormalizedHP => Handle.GetNormalizedHP();
        public float MaxHP
        {
            get
            {
                return Handle.maxHP;
            }
            set
            {
                Handle.maxHP = value;
            }
        }
        public float HP
        {
            get
            {
                return Handle.HP;
            }
            set
            {
                Handle.HP = value;
            }
        }
        public string AbilityName
        {
            get
            {
                if (Handle.ability == null)
                    return null;
                return Handle.ability.GetType().Name;
            }
        }
        public bool IsTooBusyForSuddenCutscenes => Handle.IsTooBusyForSuddenCutscenes();
        public bool IsBusyWithSequence => Handle.IsBusyWithSequence();
        public bool IsDead => Handle.IsDead();
        public bool IsAI
        {
            get { return Handle.isAI; }
            set { Handle.isAI = value; }
        }
        public float BoostCharge
        {
            get { return Handle.boostCharge; }
            set { Handle.boostCharge = value; }
        }
        public float MaxBoostCharge
        {
            get { return Handle.maxBoostCharge; }
            set { Handle.maxBoostCharge = value; }
        }
        public new Player Handle = null;

        [MoonSharpHidden]
        public LuaPlayer(Player player, Script script) : base(player, script)
        {
            Handle = player;
        }

        internal static LuaPlayer CastMethod(Player player)
        {
            return new LuaPlayer(player, LuaManager.Instance.GlobalScript);
        }

        public void AddBoostCharge(float amount)
        {
            Handle.AddBoostCharge(amount);
        }

        public void SetRotation(Table forward)
        {
            Handle.SetRotation(LuaMathUtils.TableToVector3(forward));
        }

        public void SetRotationHard(Table forward)
        {
            Handle.SetRotHard(LuaMathUtils.TableToVector3(forward));
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
            Handle.GetHit(damage, LuaMathUtils.TableToVector3(damageDirection), parsedKnockbackType);
        }

        public void ChangeHP(int damage)
        {
            Handle.ChangeHP(damage);
        }

        public void SetCurrentMoveStyleEquipped(int moveStyle)
        {
            Handle.SetCurrentMoveStyleEquipped((MoveStyle)moveStyle);
        }

        public void SwitchToEquippedMovestyle(bool set, bool doAirTrick, bool showEffect)
        {
            Handle.SwitchToEquippedMovestyle(set, doAirTrick, true, showEffect);
        }
    }
}
