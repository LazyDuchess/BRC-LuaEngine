using Reptile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Playables;

namespace LuaEngine
{
    public class SequenceEvent : GameplayEvent
    {
        public PlayableDirector Director = null;
        public LuaEventHandler OnSequenceEnd = null;
        public override void Awake() {
            base.Awake();
            OnSequenceEnd = new(LuaManager.Instance.GlobalScript);
        }

        public override void LetPlayerGo()
        {
            base.LetPlayerGo();
            Director.time = Director.duration;
            Director.Evaluate();
            if (WorldHandler.instance.CurrentCamera != WorldHandler.instance.GetCurrentPlayer().cam.cam)
                    WorldHandler.instance.CurrentCamera.gameObject.SetActive(false);
            OnSequenceEnd?.Invoke();
            Destroy(this.gameObject);
        }
    }
}
