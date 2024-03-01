using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LuaEngine.Mono
{
    public class ScriptBehavior : MonoBehaviour
    {
        [Header("Filename of a Lua Behavior Script (e.g. \"doctorpolo.triggerhurt.lua\")")]
        public string LuaScriptName = null;
    }
}
