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
                var rigidBody = Handle.gameObject.GetComponent<Rigidbody>();
                if (rigidBody == null)
                    return LuaMathUtils.Vector3ToTable(Vector3.zero);
                return LuaMathUtils.Vector3ToTable(rigidBody.velocity);
            }

            set
            {
                var rigidBody = Handle.gameObject.GetComponent<Rigidbody>();
                if (rigidBody == null)
                    return;
                rigidBody.velocity = LuaMathUtils.TableToVector3(value);
            }
        }
        public bool ActiveInHierarchy => Handle.gameObject.activeInHierarchy;
        public bool Active
        {
            get
            {
                return Handle.gameObject.activeSelf;
            }
            set
            {
                Handle.gameObject.SetActive(value);
            }
        }
        public int InstanceID => Handle.gameObject.GetInstanceID();

        public string Name
        {
            get { return Handle.gameObject.name; }
            set { Handle.gameObject.name = value; }
        }
        [MoonSharpHidden]
        public GameObject Handle = null;

        [MoonSharpHidden]
        public LuaGameObject(GameObject handle)
        {
            Handle = handle;
        }

        public LuaGameObject FindRecursive(string name)
        {
            var child = Handle.gameObject.transform.FindRecursive(name);
            if (child == null)
                return null;
            return new LuaGameObject(child.gameObject);
        }

        public Table GetPosition()
        {
            return LuaMathUtils.Vector3ToTable(Handle.transform.position);
        }

        public void SetPosition(Table position)
        {
            Handle.transform.position = LuaMathUtils.TableToVector3(position);
        }

        public void AddPosition(Table offset)
        {
            Handle.transform.position += LuaMathUtils.TableToVector3(offset);
        }

        public Table GetEulerAngles()
        {
            return LuaMathUtils.Vector3ToTable(Handle.transform.eulerAngles);
        }

        public void SetEulerAngles(Table eulerAngles)
        {
            Handle.transform.rotation = Quaternion.Euler(LuaMathUtils.TableToVector3(eulerAngles));
        }

        public void Rotate(Table axis, float angle)
        {
            Handle.transform.Rotate(LuaMathUtils.TableToVector3(axis), angle);
        }

        public Table GetForward()
        {
            return LuaMathUtils.Vector3ToTable(Handle.transform.forward);
        }

        public LuaGameObject Instantiate()
        {
            var instance = GameObject.Instantiate(Handle.gameObject);
            LuaUtility.OnInstantiate(instance);
            return new LuaGameObject(instance);
        }

        public void Destroy()
        {
            GameObject.Destroy(Handle.gameObject);
        }

        public LuaScriptBehavior AddScriptBehavior(string scriptFilename)
        {
            var scriptBehavior = Handle.gameObject.AddComponent<ScriptBehavior>();
            scriptBehavior.LuaScriptName = scriptFilename;
            scriptBehavior.Initialize();
            return new LuaScriptBehavior(scriptBehavior, LuaManager.Instance.GlobalScript);
        }

        public LuaBuiltInComponent GetLuaComponent(string luaComponentName)
        {
            var cSharpType = LuaCastFactory.GetCSharpTypeFromLuaTypeName(luaComponentName);
            if (cSharpType == null)
                return null;
            var component = Handle.gameObject.GetComponent(cSharpType);
            if (component == null)
                return null;
            return LuaCastFactory.CastCSharpTypeToLuaType<LuaBuiltInComponent>(component);
        }
    }
}
