using UnityEditor;

public static class PackageExporter
{
    private const string PackageAssetsPath = "Assets/LuaEngine";
    private const string PackageOutputPath = "../Build/LuaEngine.unitypackage";

    [MenuItem(EditorConstants.MenuLabel + "/Export Editor Package")]
    private static void ExportPackage()
    {
        AssetDatabase.ExportPackage(new [] { PackageAssetsPath }, PackageOutputPath, ExportPackageOptions.Recurse);
    }
}