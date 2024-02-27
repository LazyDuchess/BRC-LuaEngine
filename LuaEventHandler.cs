using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuaEngine
{
    [MoonSharpUserData]
    public class LuaEventHandler
    {
        private List<DynValue> _callbacks = [];
        private Dictionary<string, int> _indexByGUID = [];
        private Script _script = null;

        [MoonSharpHidden]
        public LuaEventHandler(Script script)
        {
            _script = script;
        }

        public string Add(DynValue callback)
        {
            var guid = Guid.NewGuid().ToString();
            _indexByGUID[guid] = _callbacks.Count;
            _callbacks.Add(callback);
            return guid;
        }

        public void Remove(string callbackGUID)
        {
            if (_indexByGUID.TryGetValue(callbackGUID, out var index))
            {
                _callbacks.RemoveAt(index);
                _indexByGUID.Remove(callbackGUID);
            }
        }

        public void Invoke(params object[] args)
        {
            foreach(var callback in _callbacks)
            {
                _script.Call(callback, args);
            }
        }
    }
}
