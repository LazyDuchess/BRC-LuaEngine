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

            public void StartScreenShake(string screenShakeType, float duration, bool continuous)
            {
                var parsedScreenShakeType = ScreenShakeType.LIGHT;
                switch (screenShakeType)
                {
                    case "Light":
                        parsedScreenShakeType = ScreenShakeType.LIGHT;
                        break;

                    case "Medium":
                        parsedScreenShakeType = ScreenShakeType.MEDIUM;
                        break;

                    case "Heavy":
                        parsedScreenShakeType = ScreenShakeType.HEAVY;
                        break;

                    case "ExtraLight":
                        parsedScreenShakeType = ScreenShakeType.EXTRALIGHT;
                        break;

                    case "JustABitLighter":
                        parsedScreenShakeType = ScreenShakeType.JUST_A_BIT_LIGHTER;
                        break;

                    case "UltraLight":
                        parsedScreenShakeType = ScreenShakeType.ULTRALIGHT;
                        break;
                }
                GameplayCamera.StartScreenShake(parsedScreenShakeType, duration, continuous);
            }

            public void FadeInAndOut(float durationIn, float durationStay, float durationOut)
            {
                Core.Instance.BaseModule.uiManager.effects.FadeInAndOutBlack(durationIn, durationStay, durationOut);
            }

            public void ShowNotification(string text)
            {
                Core.Instance.BaseModule.uiManager.ShowNotification(text);
            }
        }
    }
}
