using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MapStation.Tools;
using MapStation.Common;
using System.IO.Compression;
using System.IO;

namespace LuaEngine.Editor
{
    public class LuaEngineMapStationPlugin : AMapStationPlugin
    {
        public override string[] GetDependencies()
        {
            return new string[] { "LazyDuchess-LuaEngine-0.1.0" };
        }

        private string MakeLuaZip(string assetPath, string luaPath)
        {
            var luaOutputPath = Path.Combine(assetPath, "lua.luamod");
            ZipFile.CreateFromDirectory(luaPath, luaOutputPath);
            return luaOutputPath;
        }

        public override void ProcessMapZip(ZipArchive archive, string mapName, System.IO.Compression.CompressionLevel compressionLevel)
        {
            var assetPath = AssetNames.GetAssetDirectoryForMap(mapName);
            var luaZipPath = Path.Combine(assetPath, "lua.luamod");
            if (File.Exists(luaZipPath))
            {
                archive.CreateEntryFromFile(luaZipPath, "lua.luamod", compressionLevel);
                return;
            }

            var luaPath = Path.Combine(assetPath, "lua");
            if (Directory.Exists(luaPath)) {
                luaZipPath = MakeLuaZip(assetPath, luaPath);
                archive.CreateEntryFromFile(luaZipPath, "lua.luamod", compressionLevel);
                File.Delete(luaZipPath);
                return;
            }
        }
    }
}
