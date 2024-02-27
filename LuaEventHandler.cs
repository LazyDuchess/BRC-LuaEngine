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
    public class LuaEventHandler
    {
        private List<DynValue> _callbacks = [];
        private Dictionary<string, DynValue> _callbackByGUID = [];
        private Script _script = null;

        [MoonSharpHidden]
        public LuaEventHandler(Script script)
        {
            _script = script;
        }

        public string Add(DynValue callback)
        {
            var guid = Guid.NewGuid().ToString();
            _callbackByGUID[guid] = callback;
            _callbacks.Add(callback);
            return guid;
        }

        public void Remove(string callbackGUID)
        {
            if (_callbackByGUID.TryGetValue(callbackGUID, out var callback))
            {
                _callbacks.Remove(callback);
                _callbackByGUID.Remove(callbackGUID);
            }
        }

        [MoonSharpHidden]
        public void Invoke(params object[] args)
        {
            foreach(var callback in _callbacks)
            {
                _script.Call(callback, args);
            }
        }
    }
}
