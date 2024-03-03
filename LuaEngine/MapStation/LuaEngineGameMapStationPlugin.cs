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
        public override void OnReload()
        {
            LuaManager.Instance.Reload();
        }
    }
}
