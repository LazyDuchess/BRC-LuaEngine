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
        public LuaGameObject Parent
        {
            get
            {
                return new LuaGameObject(Handle.transform.parent.gameObject);
            }
            set
            {
                Handle.transform.SetParent(value.Handle.transform, true);
            }
        }
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

        public LuaGameObject Find(string name)
        {
            var child = Handle.gameObject.transform.Find(name);
            if (child == null)
                return null;
            return new LuaGameObject(child.gameObject);
        }

        public LuaGameObject FindRecursive(string name)
        {
            var child = Handle.gameObject.transform.FindRecursive(name);
            if (child == null)
                return null;
            return new LuaGameObject(child.gameObject);
        }

        public Table GetLocalPosition()
        {
            return LuaMathUtils.Vector3ToTable(Handle.transform.localPosition);
        }

        public void SetLocalPosition(Table position)
        {
            Handle.transform.localPosition = LuaMathUtils.TableToVector3(position);
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

        public Table GetLocalEulerAngles()
        {
            return LuaMathUtils.Vector3ToTable(Handle.transform.localEulerAngles);
        }

        public void SetLocalEulerAngles(Table eulerAngles)
        {
            Handle.transform.localRotation = Quaternion.Euler(LuaMathUtils.TableToVector3(eulerAngles));
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
        
        public Table GetRight()
        {
            return LuaMathUtils.Vector3ToTable(Handle.transform.right);
        }

        public Table GetUp()
        {
            return LuaMathUtils.Vector3ToTable(Handle.transform.up);
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

        public LuaScriptBehavior GetScriptBehavior(string scriptFilename)
        {
            var behaviors = Handle.GetComponents<ScriptBehavior>();
            foreach(var behavior in behaviors)
            {
                if (behavior.LuaScriptName == scriptFilename)
                    return new LuaScriptBehavior(behavior, LuaManager.Instance.GlobalScript);
            }
            return null;
        }

        public static LuaGameObject New(string name)
        {
            var gameObject = new GameObject(name);
            return new LuaGameObject(gameObject);
        }

        public LuaScriptStringValue GetStringValue(string name)
        {
            var stringValue = Handle.GetComponents<ScriptStringValue>().Where(comp => comp.Name == name).FirstOrDefault();
            return new LuaScriptStringValue(stringValue, LuaManager.Instance.GlobalScript);
        }

        public LuaScriptNumberValue GetNumberValue(string name)
        {
            var numberValue = Handle.GetComponents<ScriptNumberValue>().Where(comp => comp.Name == name).FirstOrDefault();
            return new LuaScriptNumberValue(numberValue, LuaManager.Instance.GlobalScript);
        }

        public LuaScriptGameObjectValue GetGameObjectValue(string name)
        {
            var goValue = Handle.GetComponents<ScriptGameObjectValue>().Where(comp => comp.Name == name).FirstOrDefault();
            return new LuaScriptGameObjectValue(goValue, LuaManager.Instance.GlobalScript);
        }

        public LuaScriptComponentValue GetComponentValue(string name)
        {
            var compValue = Handle.GetComponents<ScriptComponentValue>().Where(comp => comp.Name == name).FirstOrDefault();
            return new LuaScriptComponentValue(compValue, LuaManager.Instance.GlobalScript);
        }

        public void SetStringValue(string name, string value)
        {
            var stringValue = GetStringValue(name).ScriptStringValue;
            if (stringValue == null)
                stringValue = Handle.AddComponent<ScriptStringValue>();
            stringValue.Value = value;
        }

        public void SetNumberValue(string name, double value)
        {
            var numberValue = GetNumberValue(name).ScriptNumberValue;
            if (numberValue == null)
                numberValue = Handle.AddComponent<ScriptNumberValue>();
            numberValue.Value = value;
        }

        public void SetGameObjectValue(string name, LuaGameObject value)
        {
            var goValue = GetGameObjectValue(name).ScriptGameObjectValue;
            if (goValue == null)
                goValue = Handle.AddComponent<ScriptGameObjectValue>();
            goValue.Value = value.Handle;
        }

        public void SetComponentValue(string name, LuaBuiltInComponent value)
        {
            var compValue = GetComponentValue(name).ScriptComponentValue;
            if (compValue == null)
                compValue = Handle.AddComponent<ScriptComponentValue>();
            compValue.Value = value.Component;
        }
    }
}
