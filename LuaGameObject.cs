using LuaEngine.Components;
using LuaEngine.Mono;
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
    public class LuaGameObject
    {
        public Table Velocity
        {
            get
            {
                var rigidBody = _handle.gameObject.GetComponent<Rigidbody>();
                if (rigidBody == null)
                    return LuaMathUtils.Vector3ToTable(Vector3.zero);
                return LuaMathUtils.Vector3ToTable(rigidBody.velocity);
            }

            set
            {
                var rigidBody = _handle.gameObject.GetComponent<Rigidbody>();
                if (rigidBody == null)
                    return;
                rigidBody.velocity = LuaMathUtils.TableToVector3(value);
            }
        }
        public bool ActiveInHierarchy => _handle.gameObject.activeInHierarchy;
        public bool Active
        {
            get
            {
                return _handle.gameObject.activeSelf;
            }
            set
            {
                _handle.gameObject.SetActive(value);
            }
        }
        public int InstanceID => _handle.gameObject.GetInstanceID();
        public LuaEventHandler OnPlayerTriggerEnter => _handle.OnPlayerTriggerEnter;
        public LuaEventHandler OnPlayerTriggerStay => _handle.OnPlayerTriggerStay;
        public LuaEventHandler OnPlayerTriggerExit => _handle.OnPlayerTriggerExit;
        public LuaEventHandler OnDestroyed => _handle.OnDestroyed;
        public string Name
        {
            get { return _handle.gameObject.name; }
            set { _handle.gameObject.name = value; }
        }
        private LuaHooks _handle = null;

        [MoonSharpHidden]
        public LuaGameObject(LuaHooks handle)
        {
            _handle = handle;
        }

        public LuaGameObject FindRecursive(string name)
        {
            var child = _handle.gameObject.transform.FindRecursive(name);
            if (child == null)
                return null;
            return LuaHooks.GetOrMake(child.gameObject, LuaManager.Instance.GlobalScript).LuaGameObject;
        }

        public Table GetPosition()
        {
            return LuaMathUtils.Vector3ToTable(_handle.transform.position);
        }

        public void SetPosition(Table position)
        {
            _handle.transform.position = LuaMathUtils.TableToVector3(position);
        }

        public void AddPosition(Table offset)
        {
            _handle.transform.position += LuaMathUtils.TableToVector3(offset);
        }

        public Table GetEulerAngles()
        {
            return LuaMathUtils.Vector3ToTable(_handle.transform.eulerAngles);
        }

        public void SetEulerAngles(Table eulerAngles)
        {
            _handle.transform.rotation = Quaternion.Euler(LuaMathUtils.TableToVector3(eulerAngles));
        }

        public void Rotate(Table axis, float angle)
        {
            _handle.transform.Rotate(LuaMathUtils.TableToVector3(axis), angle);
        }

        public Table GetForward()
        {
            return LuaMathUtils.Vector3ToTable(_handle.transform.forward);
        }

        public LuaGameObject Instantiate()
        {
            var instance = GameObject.Instantiate(_handle.gameObject);
            LuaUtility.RemoveAllLuaHooks(instance);
            var luaHooks = LuaHooks.GetOrMake(_handle.gameObject, _handle.Script);
            return luaHooks.LuaGameObject;
        }

        public void Destroy()
        {
            GameObject.Destroy(_handle.gameObject);
        }

        public LuaScriptBehavior AddScriptBehavior(string scriptFilename)
        {
            var scriptBehavior = _handle.gameObject.AddComponent<ScriptBehavior>();
            scriptBehavior.LuaScriptName = scriptFilename;
            scriptBehavior.Initialize();
            return new LuaScriptBehavior(scriptBehavior, LuaManager.Instance.GlobalScript);
        }
    }
}
