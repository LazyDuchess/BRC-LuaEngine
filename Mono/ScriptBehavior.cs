using LuaEngine.Components;
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
        public string LuaScriptName = null;
        private bool _initialized = false;
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
            luaScript.RunInContext(globalScript, LuaHooks.GetOrMake(gameObject, globalScript).LuaGameObject);
        }
    }
}
