using LuaEngine.Mono;
using MoonSharp.Interpreter;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LuaEngine.Modules
{
    public class LuaUnityEngine : ILuaModule
    {
        private LuaBindings _bindings = null;
        public void OnRegister(Script script)
        {
            _bindings = new(script);
            script.Globals["Engine"] = _bindings;
            script.Globals["LuaGameObject"] = typeof(LuaGameObject);
        }

        [MoonSharpUserData]
        public class LuaBindings
        {
            private Script _script = null;
            [MoonSharpHidden]
            public LuaBindings(Script script)
            {
                _script = script;
            }

            public List<ScriptBehavior> FindScriptBehaviors(string scriptName, bool includeInactive)
            {
                var scripts = GameObject.FindObjectsOfType<ScriptBehavior>(includeInactive).Where(script => script.LuaScriptName == scriptName);
                return scripts.ToList();
            }

            public List<LuaGameObject> GetGameObjects(bool includeInactive)
            {
                var objectArray = GameObject.FindObjectsOfType<GameObject>(includeInactive);
                var objectList = new List<LuaGameObject>();
                foreach(var obj in objectArray)
                {
                    objectList.Add(new LuaGameObject(obj));
                }
                return objectList;
            }

            public LuaGameObject FindGameObjectByName(string name)
            {
                var gameObject = GameObject.Find(name);
                if (gameObject == null)
                    return null;
                return new LuaGameObject(gameObject);
            }
        }
    }
}
