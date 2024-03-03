using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuaEngine.Alarms
{
    public class Alarm
    {
        public float TimeLeft = 0f;
        public bool RunPaused = false;
        public LuaEventHandler Callback = null;

        public Alarm()
        {
            Callback = new(LuaManager.Instance.GlobalScript);
        }
    }
}
