using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace LuaEngine.Components
{
    public class LuaHooks : MonoBehaviour
    {
        public LuaGameObject LuaGameObject = null;
        public LuaEventHandler OnDestroyed = null;
        public LuaEventHandler OnPlayerTriggerEnter = null;
        public LuaEventHandler OnPlayerTriggerStay = null;
        public LuaEventHandler OnPlayerTriggerExit = null;

        public LuaEventHandler OnAnyTriggerEnter = null;
        public LuaEventHandler OnAnyTriggerStay = null;
        public LuaEventHandler OnAnyTriggerExit = null;
        [MoonSharpHidden]
        public Script Script = null;

        private void OnTriggerEnter(Collider other)
        {
            OnAnyTriggerEnter?.Invoke(LuaHooks.GetOrMake(other.gameObject, Script).LuaGameObject);
        }

        private void OnTriggerStay(Collider other)
        {
            OnAnyTriggerStay?.Invoke(LuaHooks.GetOrMake(other.gameObject, Script).LuaGameObject);
        }

        private void OnTriggerExit(Collider other)
        {
            OnAnyTriggerExit?.Invoke(LuaHooks.GetOrMake(other.gameObject, Script).LuaGameObject);
        }

        public static LuaHooks GetOrMake(GameObject gameObject, Script script)
        {
            var comp = gameObject.GetComponent<LuaHooks>();
            if (comp != null)
                return comp;
            return Make(gameObject, script);
        }

        public static LuaHooks Make(GameObject gameObject, Script script)
        {
            var comp = gameObject.GetComponent<LuaHooks>();
            if (comp != null)
            {
                comp.OnDestroyed = null;
                Destroy(comp);
            }
            comp = gameObject.AddComponent<LuaHooks>();
            comp.Initialize(script);
            return comp;
        }

        public void Initialize(Script script)
        {
            Script = script;
            LuaGameObject = new(this);
            OnDestroyed = new(script);
            OnPlayerTriggerEnter = new(script);
            OnPlayerTriggerStay = new(script);
            OnPlayerTriggerExit = new(script);
            OnAnyTriggerEnter = new(script);
            OnAnyTriggerStay = new(script);
            OnAnyTriggerExit = new(script);
        }

        private void OnDestroy()
        {
            OnDestroyed?.Invoke();
        }
    }
}
