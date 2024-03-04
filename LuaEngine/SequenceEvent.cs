using Reptile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuaEngine
{
    public class SequenceEvent : GameplayEvent
    {
        public LuaEventHandler OnSequenceEnd = null;
        public override void Awake() {
            base.Awake();
            OnSequenceEnd = new(LuaManager.Instance.GlobalScript);
        }

        public override void LetPlayerGo()
        {
            base.LetPlayerGo();
            OnSequenceEnd?.Invoke();
        }
    }
}
