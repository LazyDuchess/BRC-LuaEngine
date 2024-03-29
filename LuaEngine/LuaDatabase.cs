﻿using BepInEx.Bootstrap;
using BepInEx.Logging;
using MapStation.Plugin;
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
        private static bool MapStationInstalled
        {
            get
            {
                if (CheckedMapStationInstalled)
                    return CachedMapStationInstalled;
                CachedMapStationInstalled = CheckMapStationInstalled();
                CheckedMapStationInstalled = true;
                return CachedMapStationInstalled;
            }
        }
        private static bool CheckedMapStationInstalled = false;
        private static bool CachedMapStationInstalled;
        private static string PluginPath;

        private static bool CheckMapStationInstalled()
        {
            return (Chainloader.PluginInfos.ContainsKey("MapStation.Plugin"));
        }

        internal static void Initialize(string pluginPath)
        {
            PluginPath = pluginPath;
            LogSource = BepInEx.Logging.Logger.CreateLogSource("Lua Database");
            InitializeScripts();
        }

        public static void Reload()
        {
            InitializeScripts();
        }

        private static void InitializeScripts()
        {
            AutoRunScripts = new();
            BehaviorScripts = new();
            LoadPluginScripts();
            if (MapStationInstalled)
                LoadMapScripts();
        }

        private static void LoadPluginScripts()
        {
            var pluginZips = Directory.GetFiles(PluginPath, "*.luamod", SearchOption.AllDirectories);
            foreach (var pluginZip in pluginZips)
            {
                LogSource.LogInfo($"Loading Lua scripts in {pluginZip}");
                LoadPluginZip(pluginZip);
            }
        }

        private static void LoadMapScripts()
        {
            var mapDatabase = MapDatabase.Instance;
            foreach(var map in mapDatabase.maps)
            {
                var zip = map.Value.zipPath;
                var archive = ZipFile.OpenRead(zip);
                var luaEntry = archive.GetEntry("lua.luamod");
                if (luaEntry != null)
                {
                    var entryStream = luaEntry.Open();
                    var entryZip = new ZipArchive(entryStream);
                    LoadPluginZip(entryZip);
                    entryZip.Dispose();
                    entryStream.Dispose();
                }
            }
        }

        public static void LoadPluginZip(ZipArchive archive)
        {
            foreach (var entry in archive.Entries)
            {
                var fullName = entry.FullName.Replace(@"\", "/");
                var name = entry.Name.Replace(@"\", "/");
                if (name.EndsWith(".lua"))
                {
                    var body = ReadStringZipEntry(entry);
                    if (fullName.StartsWith("autorun/"))
                    {
                        var script = LuaScript.FromString(name, body, true);
                        if (script != null)
                            AutoRunScripts.Add(script);
                    }
                    else if (fullName.StartsWith("behavior/"))
                    {
                        var script = LuaScript.FromString(name, body, true);
                        if (script != null)
                            BehaviorScripts[script.Name] = script;
                    }
                }
            }
        }

        public static void LoadPluginZip(string zipPath)
        {
            var zip = ZipFile.OpenRead(zipPath);
            LoadPluginZip(zip);
            zip.Dispose();
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
