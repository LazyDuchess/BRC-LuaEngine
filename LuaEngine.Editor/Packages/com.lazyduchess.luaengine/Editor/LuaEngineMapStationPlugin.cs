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
            return new string[] { $"LazyDuchess-LuaEngine-{LuaEngineVersion.Version}" };
        }

        private string MakeLuaZip(string assetPath, string luaPath)
        {
            var luaOutputPath = Path.Combine(assetPath, "lua.luamod");
            var archive = ZipFile.Open(luaOutputPath, ZipArchiveMode.Create);
            var luaFiles = Directory.GetFiles(luaPath, "*.lua", SearchOption.AllDirectories);
            foreach(var luaFile in luaFiles)
            {
                var pathInZip = luaFile.Substring(luaPath.Length);
                if (pathInZip[0] == '/' || pathInZip[0] == '\\')
                    pathInZip = pathInZip.Substring(1);
                archive.CreateEntryFromFile(luaFile, pathInZip);
            }
            archive.Dispose();
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
