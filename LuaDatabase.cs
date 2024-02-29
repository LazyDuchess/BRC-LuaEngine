using BepInEx.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuaEngine
{
    public static class LuaDatabase
    {
        public static List<LuaScript> AutoRunScripts = null;
        public static Dictionary<string, LuaScript> BehaviorScripts = null;
        private static ManualLogSource LogSource = null;

        internal static void Initialize(string pluginPath)
        {
            LogSource = BepInEx.Logging.Logger.CreateLogSource("Lua Database");
            AutoRunScripts = new();
            BehaviorScripts = new();
            var pluginPaths = Directory.GetDirectories(pluginPath);
            foreach (var plugin in pluginPaths)
            {
                var luaPath = Path.Combine(plugin, "lua");
                if (Directory.Exists(luaPath))
                {
                    LogSource.LogInfo($"Loading Lua scripts for Plugin {Path.GetFileName(plugin)}");
                    LoadPlugin(luaPath);
                }
            }
            AutoRunScripts = AutoRunScripts.OrderBy(script => script.Priority).ToList();
        }

        public static void LoadPlugin(string path)
        {
            var autorunPath = Path.Combine(path, "autorun");

            if (Directory.Exists(autorunPath))
            {
                var autorunFiles = Directory.GetFiles(autorunPath, "*.lua", SearchOption.AllDirectories);
                foreach (var file in autorunFiles)
                {
                    var script = LuaScript.FromFile(file, true);
                    if (script != null)
                        AutoRunScripts.Add(script);
                }
            }
            var behaviorPath = Path.Combine(path, "behavior");

            if (Directory.Exists(behaviorPath))
            {
                var behaviorFiles = Directory.GetFiles(behaviorPath, "*.lua", SearchOption.AllDirectories);
                foreach (var file in behaviorFiles)
                {
                    var script = LuaScript.FromFile(file);
                    if (script != null)
                        BehaviorScripts[script.Name] = script;
                }
            }
        }
    }
}
