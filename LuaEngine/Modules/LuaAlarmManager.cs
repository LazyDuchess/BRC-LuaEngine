using LuaEngine.Alarms;
using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuaEngine.Modules
{
    public class LuaAlarmManager : ILuaModule
    {
        private LuaBindings _bindings = null;
        public void OnRegister(Script script)
        {
            _bindings = new();
            script.Globals["AlarmManager"] = _bindings;
        }

        [MoonSharpUserData]
        public class LuaBindings
        {
            public string CreateAlarm(LuaGameObject gameObject, float seconds, bool runPaused, DynValue callback)
            {
                var alarm = new Alarm();
                alarm.TimeLeft = seconds;
                alarm.RunPaused = runPaused;
                alarm.Callback.Add(callback);
                var alarmHandler = gameObject.Handle.GetComponent<AlarmHandler>();
                if (alarmHandler == null)
                    alarmHandler = gameObject.Handle.AddComponent<AlarmHandler>();
                return alarmHandler.Add(alarm);
            }

            public void RemoveAlarm(LuaGameObject gameObject, string guid)
            {
                var alarmHandler = gameObject.Handle.GetComponent<AlarmHandler>();
                if (alarmHandler == null)
                    return;
                alarmHandler.Remove(guid);
            }
        }
    }
}
