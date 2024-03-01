using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MapStation.Common;

namespace LuaEngine.MapStation
{
    public class LuaEngineGameMapStationPlugin : AGameMapStationPlugin
    {
        public override void OnAddMapToDatabase(ZipArchive archive, string path, string mapName)
        {
            var luaModEntry = archive.GetEntry("lua.luamod");
            if (luaModEntry == null)
                return;
            var entryStream = luaModEntry.Open();
            var entryZip = new ZipArchive(entryStream);
            LuaDatabase.LoadPluginZip(entryZip);
            entryZip.Dispose();
            entryStream.Dispose();
        }
    }
}
