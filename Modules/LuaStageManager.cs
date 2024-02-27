﻿using BepInEx.Logging;
using MoonSharp.Interpreter;
using Reptile;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuaEngine.Modules
{
    public class LuaStageManager : ILuaModule
    {
        private LuaBindings _bindings = null;

        public void OnRegister(Script script)
        {
            _bindings = new(script);
            StageManager.OnStageInitialized += StageManager_OnStageInitialized;
            StageManager.OnStagePostInitialization += StageManager_OnStagePostInitialization;
            script.Globals["StageManager"] = _bindings;
        }

        private void StageManager_OnStageInitialized()
        {
            _bindings.OnStageInitialized.Invoke();
        }

        private void StageManager_OnStagePostInitialization()
        {
            _bindings.OnStagePostInitialization.Invoke();
        }

        [MoonSharpUserData]
        public class LuaBindings
        {
            public LuaStage CurrentStage => new LuaStage(Utility.GetCurrentStage());
            public LuaEventHandler OnStageInitialized = null;
            public LuaEventHandler OnStagePostInitialization = null;

            [MoonSharpHidden]
            public LuaBindings(Script script)
            {
                OnStageInitialized = new(script);
                OnStagePostInitialization = new(script);
            }

            public void ExitCurrentStage(int targetStageID, int previousStageID)
            {
                Core.Instance.BaseModule.StageManager.ExitCurrentStage((Stage)targetStageID, (Stage)previousStageID);
            }
        }

        [MoonSharpUserData]
        public class LuaStage
        {
            public string DisplayName => Core.Instance.Localizer.GetStageName(_stage);
            public int ID => (int)_stage;
            public string InternalName => _stage.ToString();
            private Stage _stage = Stage.NONE;

            [MoonSharpHidden]
            public LuaStage(Stage stage)
            {
                _stage = stage;
            }
        }
    }
}
