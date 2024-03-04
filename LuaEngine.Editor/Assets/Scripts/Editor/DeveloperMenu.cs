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
        var outputPath = "../Build";
        var mapsPath = "Assets/Maps";
        if (!Directory.Exists(outputPath))
            Directory.CreateDirectory(outputPath);
        AssetDatabase.ExportPackage(new string[] { mapsPath }, Path.Combine(outputPath, "Example Maps.unitypackage"), ExportPackageOptions.Recurse);
    }
}
