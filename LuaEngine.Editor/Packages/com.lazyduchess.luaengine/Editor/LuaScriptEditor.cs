using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEditor.VersionControl;
using static UnityEditor.FilePathAttribute;

namespace LuaEngine.Editor
{
    public static class LuaScriptEditor
    {
        private const string EmptyScriptContents = "";
        private const string BehaviorScriptContents = @"-- Any code outside of functions is executed immediately.
local updateCallback

-- Called before the first frame update.
local function Start()
end

-- Called every frame the game is unpaused.
local function Update()
end

-- Called when the script or object holding the script is destroyed.
local function OnDestroy()
    Core.OnUpdate.Remove(updateCallback)
end

script.OnStart.Add(Start)
updateCallback = Core.OnUpdate.Add(Update)
script.OnDestroy.Add(OnDestroy)";
        private const string AutorunScriptContents = @"--Priority comment can be skipped. Scripts with lower priority load first.
--PRIORITY: 0

-- Global variables can be read and written from any script.
MyMathLibrary = {}
function MyMathLibrary.Add(x, y)
    return x + y
end";
        [MenuItem("Assets/Create/Lua/Lua Folder", false, -100)]
        public static void CreateLuaFolder()
        {
            var path = GetCurrentFolder();
            var luaFolderPath = Path.Combine(GetCurrentFolder(), "lua");
            var autorunFolderPath = Path.Combine(luaFolderPath, "autorun");
            var behaviorFolderPath = Path.Combine(luaFolderPath, "behavior");
            Directory.CreateDirectory(luaFolderPath);
            Directory.CreateDirectory(autorunFolderPath);
            Directory.CreateDirectory(behaviorFolderPath);
            AssetDatabase.Refresh();
            Selection.activeObject = AssetDatabase.LoadAssetAtPath<DefaultAsset>(luaFolderPath);
        }

        [MenuItem("Assets/Create/Lua/Empty Script", false)]
        public static void CreateEmptyLuaScript()
        {
            CreateScript("New Empty Script", EmptyScriptContents);
        }

        [MenuItem("Assets/Create/Lua/Behavior Script", false)]
        public static void CreateBehaviorScript()
        {
            CreateScript("New Behavior Script", BehaviorScriptContents);
        }

        [MenuItem("Assets/Create/Lua/Autorun Script", false)]
        public static void CreateAutorunScript()
        {
            CreateScript("New Autorun Script", AutorunScriptContents);
        }

        private static string GetCurrentFolder()
        {
            var location = "Assets";
            var activeObject = Selection.activeObject;
            if (activeObject != null)
            {
                location = AssetDatabase.GetAssetPath(activeObject);
            }
            if (File.Exists(location))
                location = Path.GetDirectoryName(location);
            return location;
        }

        private static void CreateScript(string name, string contents)
        {
            var location = GetCurrentFolder();
            location = GetUniqueFileName(Path.Combine(location, name), ".lua");
            File.WriteAllText(location, contents);
            AssetDatabase.Refresh();
            Selection.activeObject = AssetDatabase.LoadAssetAtPath<DefaultAsset>(location);
        }

        private static string GetUniqueFileName(string filename, string extension)
        {
            var duplicates = 0;
            var newFilename = filename + extension;
            while (File.Exists(newFilename))
            {
                duplicates++;
                newFilename = $"{filename} {duplicates}{extension}";
            }
            return newFilename;
        }
    }
}