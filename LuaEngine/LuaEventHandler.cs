using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuaEngine
{
    [MoonSharpUserData]
    public class LuaEventHandler : ILuaReloadable
    {
        private Dictionary<string, DynValue> _callbackByGUID = [];
        private Script _script = null;

        [MoonSharpHidden]
        public LuaEventHandler(Script script)
        {
            _script = script;
            LuaReloadableManager.Register(this);
        }

        [MoonSharpHidden]
        public void OnReload()
        {
            Clear();
        }

        public void Clear()
        {
            _callbackByGUID = [];
        }

        public string Add(DynValue callback)
        {
            var guid = Guid.NewGuid().ToString();
            _callbackByGUID[guid] = callback;
            return guid;
        }

        public void Remove(string callbackGUID)
        {
            if (callbackGUID == null) return;

            if (_callbackByGUID.TryGetValue(callbackGUID, out var _))
            {
                _callbackByGUID.Remove(callbackGUID);
            }
        }

        [MoonSharpHidden]
        public void Invoke(params object[] args)
        {
            foreach(var callback in _callbackByGUID)
            {
                _script.Call(callback.Value, args);
            }
        }
    }
}
