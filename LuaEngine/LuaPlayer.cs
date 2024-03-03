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
        public LuaEventHandler OnLandCombo => PlayerLuaEngineComponent.Get(Player)?.OnLandCombo;
        public LuaEventHandler OnDropCombo => PlayerLuaEngineComponent.Get(Player)?.OnDropCombo;
        public bool IsComboing
        {
            get
            {
                return Player.IsComboing();
            }
        }
        public float ScoreMultiplier
        {
            get
            {
                return Player.scoreMultiplier;
            }
            set
            {
                Player.scoreMultiplier = value;
            }
        }
        public float BaseScore
        {
            get
            {
                return Player.baseScore;
            }
            set
            {
                Player.baseScore = value;
            }
        }
        public bool Grounded => Player.IsGrounded();
        public int MoveStyleEquipped => (int)Player.moveStyleEquipped;
        public int MoveStyle => (int)Player.moveStyle;
        public float NormalizedHP => Player.GetNormalizedHP();
        public float MaxHP
        {
            get
            {
                return Player.maxHP;
            }
            set
            {
                Player.maxHP = value;
            }
        }
        public float HP
        {
            get
            {
                return Player.HP;
            }
            set
            {
                Player.HP = value;
            }
        }
        public string AbilityName
        {
            get
            {
                if (Player.ability == null)
                    return null;
                return Player.ability.GetType().Name;
            }
        }
        public bool IsTooBusyForSuddenCutscenes => Player.IsTooBusyForSuddenCutscenes();
        public bool IsBusyWithSequence => Player.IsBusyWithSequence();
        public bool IsDead => Player.IsDead();
        public bool IsAI
        {
            get { return Player.isAI; }
            set { Player.isAI = value; }
        }
        public float BoostCharge
        {
            get { return Player.boostCharge; }
            set { Player.boostCharge = value; }
        }
        public float MaxBoostCharge
        {
            get { return Player.maxBoostCharge; }
            set { Player.maxBoostCharge = value; }
        }
        public Player Player = null;

        [MoonSharpHidden]
        public LuaPlayer(Player player, Script script) : base(player, script)
        {
            Player = player;
        }

        internal static LuaPlayer CastMethod(Player player)
        {
            return new LuaPlayer(player, LuaManager.Instance.GlobalScript);
        }

        public void ForceUnground(bool resetVisual)
        {
            Player.ForceUnground(resetVisual);
        }

        public void AddBoostCharge(float amount)
        {
            Player.AddBoostCharge(amount);
        }

        public void SetRotation(Table forward)
        {
            Player.SetRotation(LuaMathUtils.TableToVector3(forward));
        }

        public void SetRotationHard(Table forward)
        {
            Player.SetRotHard(LuaMathUtils.TableToVector3(forward));
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
            Player.GetHit(damage, LuaMathUtils.TableToVector3(damageDirection), parsedKnockbackType);
        }

        public void ChangeHP(int damage)
        {
            Player.ChangeHP(damage);
        }

        public void SetCurrentMoveStyleEquipped(int moveStyle)
        {
            Player.SetCurrentMoveStyleEquipped((MoveStyle)moveStyle);
        }

        public void SwitchToEquippedMovestyle(bool set, bool doAirTrick, bool showEffect)
        {
            Player.SwitchToEquippedMovestyle(set, doAirTrick, true, showEffect);
        }

        public void LandCombo()
        {
            Player.LandCombo();
        }

        public void DropCombo()
        {
            Player.DropCombo();
        }

        public void AddScoreMultiplier()
        {
            Player.AddScoreMultiplier();
        }

        public void DoTrick(string trickName, int score)
        {
            Player.currentTrickPoints = score;
            Player.currentTrickType = Player.TrickType.HANDPLANT;
            Player.currentTrickName = trickName;
            Player.currentTrickOnFoot = !Player.usingEquippedMovestyle;
            Player.baseScore += (float)((int)((float)Player.currentTrickPoints * Player.scoreFactor));
        }

        public void PlayVoice(int audioClipID, int voicePriority, bool fromPlayer = true)
        {
            Player.PlayVoice((AudioClipID)audioClipID, (VoicePriority)voicePriority, fromPlayer);
        }
    }
}
