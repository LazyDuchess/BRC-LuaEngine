param(
    [switch]$major = $False,
    [switch]$minor = $False,
    [switch]$patch = $False,
    [switch]$nogit = $False
)

$ErrorActionPreference = 'Stop'
$PSNativeCommandUseErrorActionPreference = $true

Set-Location $PSScriptRoot/..
[Environment]::CurrentDirectory = (Get-Location -PSProvider FileSystem).ProviderPath

if($Null -ne $(git status --untracked-files=no --porcelain=v1)) {
        Write-Error "Git status shows modified files. This script cannot commit a new version while there are uncommitted, modified files."
}

if($major){
    $minor = $False
    $patch = $False
}
elseif($minor){
    $major = $False
    $patch = $False
}
elseif($patch){
    $major = $False
    $minor = $False
}
else{
    Write-Error "Invalid version bump."
    return
}
$csprojPath = "LuaEngine/LuaEngine.csproj"

$projxml = [xml](Get-Content -Path $csprojPath)

$version = $projxml.Project.PropertyGroup[0].Version

$versionArray = $version.Split(".")

$majorVersion = [int]$versionArray[0]
$minorVersion = [int]$versionArray[1]
$patchVersion = [int]$versionArray[2]

if($major){
    $majorVersion++
    $minorVersion = 0
    $patchVersion = 0
}
elseif($minor){
    $minorVersion++
    $patchVersion = 0
}
else{
    $patchVersion++
}

$newVersion = "$majorVersion.$minorVersion.$patchVersion"

Write-Host "Bumping from $version to $newVersion"

$projxml.Project.PropertyGroup[0].Version = $newVersion
$projxml.Save($csprojPath)

$editorVersionCSPath = "LuaEngine.Editor/Packages/com.lazyduchess.luaengine/Editor/LuaEngineVersion.cs"

$versionString = 'namespace LuaEngine.Editor
{
    public static class LuaEngineVersion
    {
        public const string Version = "'+$newVersion+'";
    }
}
'

Out-File -FilePath $editorVersionCSPath -InputObject $versionString

$packagePath = "LuaEngine.Editor/Packages/com.lazyduchess.luaengine/package.json"

$package = Get-Content $packagePath -raw | ConvertFrom-Json
$package.version = $newVersion
$package | ConvertTo-Json -depth 32| set-content $packagePath

$manifestPath = "Thunderstore/manifest.json"

$manifest = Get-Content $manifestPath -raw | ConvertFrom-Json
$manifest.version_number = $newVersion
$manifest | ConvertTo-Json -depth 32| set-content $manifestPath

Write-Host "Bumped all versions!"

if($noit){
    return;
}

Write-Host "Making Git Tag"

git add $packagePath
git add $editorVersionCSPath
git add $csprojPath
git add $manifestPath

git commit -m "v$newVersion"
git tag $newVersion

Write-Host -ForegroundColor Green "Don't forget to 'git push --tags'"