using MoonSharp.Interpreter;
using Reptile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using static System.Net.Mime.MediaTypeNames;

namespace LuaEngine.Modules
{
    public class LuaUI : ILuaModule
    {
        private LuaBindings _bindings = null;
        public void OnRegister(Script script)
        {
            _bindings = new LuaBindings(script);
            script.Globals["UI"] = _bindings;
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

            public void SetTextMeshProText(LuaGameObject gameObject, string text)
            {
                var tmpPro = gameObject.Handle.gameObject.GetComponent<TMP_Text>();
                tmpPro.text = text;
            }

            public string GetTextMeshProText(LuaGameObject gameObject)
            {
                var tmpPro = gameObject.Handle.gameObject.GetComponent<TMP_Text>();
                return tmpPro.text;
            }
        }
    }
}
