using LuaEngine.Components;
using MoonSharp.Interpreter;
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
        public int InstanceID => _handle.gameObject.GetInstanceID();
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

        public float[] GetPosition()
        {
            return [_handle.transform.position.x, _handle.transform.position.y, _handle.transform.position.z];
        }

        public void SetPosition(float x, float y, float z)
        {
            _handle.transform.position = new Vector3(x, y, z);
        }

        public void AddPosition(float x, float y, float z)
        {
            _handle.transform.position += new Vector3(x, y, z);
        }

        public float[] GetEulerAngles()
        {
            var eulerAngles = _handle.transform.eulerAngles;
            return [eulerAngles.x, eulerAngles.y, eulerAngles.z];
        }

        public void SetEulerAngles(float x, float y, float z)
        {
            _handle.transform.rotation = Quaternion.Euler(x, y, z);
        }

        public void Rotate(float xAxis, float yAxis, float zAxis, float angle)
        {
            var axis = new Vector3(xAxis, yAxis, zAxis);
            _handle.transform.Rotate(axis, angle);
        }

        public float[] GetForward()
        {
            var forward = _handle.transform.forward;
            return [forward.x, forward.y, forward.z];
        }
    }
}
