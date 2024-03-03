using Reptile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace LuaEngine.Alarms
{
    public class AlarmHandler : MonoBehaviour
    {
        public Dictionary<string, Alarm> AlarmByGUID = null;
        private void Awake()
        {
            AlarmByGUID = new();
        }

        private void Update()
        {
            var newAlarms = new Dictionary<string, Alarm>();
            foreach(var alarm in AlarmByGUID)
            {
                if (Core.Instance.IsCorePaused && !alarm.Value.RunPaused)
                {
                    newAlarms[alarm.Key] = alarm.Value;
                    return;
                }
                alarm.Value.TimeLeft -= Core.dt;
                if (alarm.Value.TimeLeft <= 0f)
                {
                    alarm.Value.Callback.Invoke();
                    return;
                }
                newAlarms[alarm.Key] = alarm.Value;
            }
            AlarmByGUID = newAlarms;
        }

        public string Add(Alarm alarm)
        {
            var guid = Guid.NewGuid().ToString();
            AlarmByGUID[guid] = alarm;
            return guid;
        }

        public void Remove(string guid)
        {
            if (AlarmByGUID.TryGetValue(guid, out var alarm))
                AlarmByGUID.Remove(guid);
        }
    }
}
