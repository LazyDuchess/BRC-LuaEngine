﻿using BepInEx.Logging;
using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using LuaEngine.Modules;

namespace LuaEngine
{
    public class LuaManager : MonoBehaviour
    {
        public static LuaManager Instance { get; private set; }
        public Script GlobalScript = null;
        private ManualLogSource _logSource = null;
        private ManualLogSource _scriptLogSource = null;

        internal static void Create()
        {
            var gameObject = new GameObject("Lua Manager");
            var luaManager = gameObject.AddComponent<LuaManager>();
            DontDestroyOnLoad(gameObject);
            Instance = luaManager;
        }

        private void Awake()
        {
            _logSource = BepInEx.Logging.Logger.CreateLogSource("Lua Manager");
            _scriptLogSource = BepInEx.Logging.Logger.CreateLogSource("Global Lua");
            GlobalScript = new();
            GlobalScript.Options.DebugPrint = message => { _scriptLogSource.LogInfo(message); };
            RegisterModules();
            RunScripts();
        }

        private void RunScripts()
        {
            LuaDatabase.AutoRunScripts = LuaDatabase.AutoRunScripts.OrderBy(script => script.Priority).ToList();
            foreach (var script in LuaDatabase.AutoRunScripts)
            {
                _logSource.LogInfo($"Running Autorun Script {script.Name} with priority {script.Priority}");
                script.Run(GlobalScript);
            }
        }

        public DynValue Call(DynValue function, params object[] args)
        {
            try
            {
                return GlobalScript.Call(function, args);
            }
            catch(InterpreterException e)
            {
                Debug.LogError($"Error executing lua function!{Environment.NewLine}{e.DecoratedMessage}");
            }
            return null;
		}

        public void Reload()
        {
            LuaReloadableManager.OnReload();
            LuaDatabase.Reload();
            RunScripts();

        }

        private void RegisterModules()
        {
            /*
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var element in assemblies)
            {
                UserData.RegisterAssembly(element);
            }*/

            UserData.RegisterType<LuaAnimator>();
            UserData.RegisterType<LuaAudioSource>();
            UserData.RegisterType<LuaBuiltInBehaviour>();
            UserData.RegisterType<LuaBuiltInComponent>();
            UserData.RegisterType<LuaEventHandler>();
            UserData.RegisterType<LuaGameObject>();
            UserData.RegisterType<LuaPlayableDirector>();
            UserData.RegisterType<LuaPlayer>();
            UserData.RegisterType<LuaScriptBehavior>();
            UserData.RegisterType<LuaScriptComponentValue>();
            UserData.RegisterType<LuaScriptGameObjectValue>();
            UserData.RegisterType<LuaScriptNumberValue>();
            UserData.RegisterType<LuaScriptStringValue>();
            UserData.RegisterType<LuaScriptValue>();
            UserData.RegisterType<LuaAlarmManager>();
            UserData.RegisterType<LuaCore>();
            UserData.RegisterType<LuaSequenceHandler>();
            UserData.RegisterType<LuaStageManager>();
            UserData.RegisterType<LuaUI>();
            UserData.RegisterType<LuaUnityEngine>();
            UserData.RegisterType<LuaWorldHandler>();

            _logSource.LogInfo($"Registering Lua modules");

            var stageManager = new LuaStageManager();
            RegisterModule(stageManager);

            var core = new LuaCore();
            RegisterModule(core);

            var engine = new LuaUnityEngine();
            RegisterModule(engine);

            var worldHandler = new LuaWorldHandler();
            RegisterModule(worldHandler);

            var ui = new LuaUI();
            RegisterModule(ui);

            var alarmManager = new LuaAlarmManager();
            RegisterModule(alarmManager);

            var sequenceHandler = new LuaSequenceHandler();
            RegisterModule(sequenceHandler);
        }

        private void RegisterModule(ILuaModule module)
        {
            module.OnRegister(GlobalScript);
        }
    }
}
