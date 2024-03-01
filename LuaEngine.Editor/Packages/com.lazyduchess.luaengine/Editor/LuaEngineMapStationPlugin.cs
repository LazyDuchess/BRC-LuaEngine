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

        public override void ProcessThunderstoreZip(ZipArchive archive, string mapName)
        {
            var luaAssetPath = Path.Combine(AssetNames.GetAssetDirectoryForMap(mapName),"lua.luamod");
            archive.CreateEntryFromFile(luaAssetPath, "lua.luamod");
        }
    }
}
