using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public static class DeveloperEditor
{
    [MenuItem("LuaEngine Developer/Export Example Maps")]
    private static void ExportExampleMaps()
    {
        var outputPath = "Build";
        var mapsPath = "Assets/Maps";
        var mapFolders = Directory.GetDirectories(mapsPath);
        if (!Directory.Exists(outputPath))
        {
            Directory.CreateDirectory(outputPath);
        }
        foreach(var mapFolder in mapFolders)
        {
            var mapName = Path.GetFileName(mapFolder);
            AssetDatabase.ExportPackage(new string[] { mapFolder }, Path.Combine(outputPath, mapName) + ".unitypackage", ExportPackageOptions.Recurse);
        }
    }
}
