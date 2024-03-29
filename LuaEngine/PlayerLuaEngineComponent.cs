﻿using LuaEngine.Mono;
using Reptile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace LuaEngine
{
    public class PlayerLuaEngineComponent : MonoBehaviour
    {
        public bool AllowClimb = true;
        public LuaEventHandler OnLandCombo = null;
        public LuaEventHandler OnDropCombo = null;
        public Player Player = null;

        private void Awake()
        {
            OnLandCombo = new(LuaManager.Instance.GlobalScript);
            OnDropCombo = new(LuaManager.Instance.GlobalScript);
        }

        public static PlayerLuaEngineComponent Get(Player player)
        {
            var comp = player.gameObject.GetComponent<PlayerLuaEngineComponent>();
            return comp;
        }

        private void OnCollisionEnter(Collision collision)
        {
            var scripts = collision.gameObject.GetComponentsInParent<ScriptBehavior>();
            foreach (var script in scripts)
            {
                script.OnPlayerCollisionEnter?.Invoke(new LuaPlayer(Player, LuaManager.Instance.GlobalScript));
            }
        }

        private void OnCollisionStay(Collision collision)
        {
            var scripts = collision.gameObject.GetComponentsInParent<ScriptBehavior>();
            foreach (var script in scripts)
            {
                script.OnPlayerCollisionStay?.Invoke(new LuaPlayer(Player, LuaManager.Instance.GlobalScript));
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            var scripts = collision.gameObject.GetComponentsInParent<ScriptBehavior>();
            foreach (var script in scripts)
            {
                script.OnPlayerCollisionExit?.Invoke(new LuaPlayer(Player, LuaManager.Instance.GlobalScript));
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            var scripts = other.GetComponentsInParent<ScriptBehavior>();
            foreach (var script in scripts)
            {
                script.OnPlayerTriggerEnter?.Invoke(new LuaPlayer(Player, LuaManager.Instance.GlobalScript));
            }
        }

        private void OnTriggerStay(Collider other)
        {
            var scripts = other.GetComponentsInParent<ScriptBehavior>();
            foreach (var script in scripts)
            {
                script.OnPlayerTriggerStay?.Invoke(new LuaPlayer(Player, LuaManager.Instance.GlobalScript));
            }
        }

        private void OnTriggerExit(Collider other)
        {
            var scripts = other.GetComponentsInParent<ScriptBehavior>();
            foreach (var script in scripts)
            {
                script.OnPlayerTriggerExit?.Invoke(new LuaPlayer(Player, LuaManager.Instance.GlobalScript));
            }
        }
    }
}
