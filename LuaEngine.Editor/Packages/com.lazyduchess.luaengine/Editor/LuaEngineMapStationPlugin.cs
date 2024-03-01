using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MapStation.Tools;

namespace LuaEngine.Editor
{
    public class LuaEngineMapStationPlugin : AMapStationPlugin
    {
        public override string[] GetDependencies()
        {
            return new string[] { "LazyDuchess-LuaEngine-0.1.0" };
        }
    }
}
