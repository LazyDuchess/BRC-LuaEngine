﻿using BepInEx.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
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
            var pluginZips = Directory.GetFiles(pluginPath, "*.luamod", SearchOption.AllDirectories);
            foreach(var pluginZip in pluginZips)
            {
                LogSource.LogInfo($"Loading Lua scripts in {pluginZip}");
                LoadPluginZip(pluginZip);
            }
            AutoRunScripts = AutoRunScripts.OrderBy(script => script.Priority).ToList();
        }

        public static void LoadPluginZip(string zipPath)
        {
            var zip = ZipFile.OpenRead(zipPath);
            foreach(var entry in zip.Entries)
            {
                if (entry.Name.EndsWith(".lua"))
                {
                    var body = ReadStringZipEntry(entry);
                    if (entry.FullName.StartsWith("autorun/"))
                    {
                        var script = LuaScript.FromString(entry.Name, body, true);
                        if (script != null)
                            AutoRunScripts.Add(script);
                    }
                    else if (entry.FullName.StartsWith("behavior/"))
                    {
                        var script = LuaScript.FromString(entry.Name, body, true);
                        if (script != null)
                            BehaviorScripts[script.Name] = script;
                    }
                }
            }
        }

        private static string ReadStringZipEntry(ZipArchiveEntry entry)
        {
            using(var stream = entry.Open())
            {
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}