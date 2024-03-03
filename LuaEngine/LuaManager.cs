using BepInEx.Logging;
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
        }

        private void Start()
        {
            LuaDatabase.AutoRunScripts = LuaDatabase.AutoRunScripts.OrderBy(script => script.Priority).ToList();
            foreach (var script in LuaDatabase.AutoRunScripts)
            {
                _logSource.LogInfo($"Running Autorun Script {script.Name} with priority {script.Priority}");
                script.Run(GlobalScript);
            }
        }

        private void RegisterModules()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var element in assemblies)
            {
                UserData.RegisterAssembly(element);
            }

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
        }

        private void RegisterModule(ILuaModule module)
        {
            module.OnRegister(GlobalScript);
        }
    }
}
