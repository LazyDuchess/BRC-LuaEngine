using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace LuaEngine.Mono
{
    public class ScriptBehavior : MonoBehaviour
    {
        public LuaEventHandler OnStart = null;
        public LuaEventHandler OnDestroyed = null;

        public LuaEventHandler OnAnyCollisionEnter = null;
        public LuaEventHandler OnAnyCollisionStay = null;
        public LuaEventHandler OnAnyCollisionExit = null;

        public LuaEventHandler OnPlayerCollisionEnter = null;
        public LuaEventHandler OnPlayerCollisionStay = null;
        public LuaEventHandler OnPlayerCollisionExit = null;

        public LuaEventHandler OnAnyTriggerEnter = null;
        public LuaEventHandler OnAnyTriggerStay = null;
        public LuaEventHandler OnAnyTriggerExit = null;

        public LuaEventHandler OnPlayerTriggerEnter = null;
        public LuaEventHandler OnPlayerTriggerStay = null;
        public LuaEventHandler OnPlayerTriggerExit = null;

        public string LuaScriptName = null;
        private bool _initialized = false;

        private void OnCollisionEnter(Collision collision)
        {
            OnAnyCollisionEnter?.Invoke(new LuaGameObject(collision.gameObject));
        }

        private void OnCollisionStay(Collision collision)
        {
            OnAnyCollisionStay?.Invoke(new LuaGameObject(collision.gameObject));
        }

        private void OnCollisionExit(Collision collision)
        {
            OnAnyCollisionExit?.Invoke(new LuaGameObject(collision.gameObject));
        }

        private void OnTriggerEnter(Collider other)
        {
            OnAnyTriggerEnter?.Invoke(new LuaGameObject(other.gameObject));
        }

        private void OnTriggerStay(Collider other)
        {
            OnAnyTriggerStay?.Invoke(new LuaGameObject(other.gameObject));
        }

        private void OnTriggerExit(Collider other)
        {
            OnAnyTriggerExit?.Invoke(new LuaGameObject(other.gameObject));
        }

        private void Awake()
        {
            Initialize();
        }
        private void OnEnable()
        {
            Initialize();
        }
        public void Initialize()
        {
            if (LuaScriptName == null) return;
            if (!gameObject.activeInHierarchy) return;
            if (_initialized) return;
            _initialized = true;

            var globalScript = LuaManager.Instance.GlobalScript;
            var luaScript = LuaDatabase.BehaviorScripts[LuaScriptName];
            OnStart = new(globalScript);
            OnDestroyed = new(globalScript);

            OnAnyCollisionEnter = new(globalScript);
            OnAnyCollisionStay = new(globalScript);
            OnAnyCollisionExit = new(globalScript);

            OnPlayerCollisionEnter = new(globalScript);
            OnPlayerCollisionStay = new(globalScript);
            OnPlayerCollisionExit = new(globalScript);

            OnAnyTriggerEnter = new(globalScript);
            OnAnyTriggerStay = new(globalScript);
            OnAnyTriggerExit = new(globalScript);

            OnPlayerTriggerEnter = new(globalScript);
            OnPlayerTriggerStay = new(globalScript);
            OnPlayerTriggerExit = new(globalScript);

            luaScript.RunForScriptBehavior(globalScript, new LuaScriptBehavior(this, globalScript));
        }
        public void Restart()
        {
            _initialized = false;
            Initialize();
        }
        private void OnDestroy()
        {
            OnDestroyed?.Invoke();
        }
        
        private void Start()
        {
            OnStart?.Invoke();
        }
    }
}
