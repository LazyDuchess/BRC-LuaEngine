#Requires -Version 7.4
$ErrorActionPreference = 'Stop'
$PSNativeCommandUseErrorActionPreference = $true

Set-Location $PSScriptRoot/..
[Environment]::CurrentDirectory = (Get-Location -PSProvider FileSystem).ProviderPath

if($Null -ne $(git status --untracked-files=no --porcelain=v1)) {
        Write-Error "Git status shows modified files. This script cannot commit a new version while there are uncommitted, modified files."
        return
}

$luaEnginePackagePath = "LuaEngine.Editor/Packages/com.lazyduchess.luaengine/package.json"
$luaEnginePackage = Get-Content $luaEnginePackagePath -raw | ConvertFrom-Json -AsHashtable
$oldPackageVersion = $luaEnginePackage["dependencies"]["com.brcmapstation.tools"]

$packagesLockPath = "LuaEngine.Editor/Packages/packages-lock.json"
$packagesLock = Get-Content $packagesLockPath -raw | ConvertFrom-Json -AsHashtable
$packageVersion = $packagesLock["dependencies"]["com.brcmapstation.tools"]["version"]

if($oldPackageVersion == $packageVersion){
    Write-Host "MapStation dependency is already up-to-date."
    return
}

$luaEnginePackage["dependencies"]["com.brcmapstation.tools"] = $packageVersion
$luaEnginePackage["dependencies"]["com.brcmapstation.common"] = $packageVersion
$luaEnginePackage | ConvertTo-Json -depth 32| set-content $luaEnginePackagePath

git add $luaEnginePackagePath

git commit -m "Synchronize MapStation dependency version"

Write-Host "Synchronized MapStation version from $oldPackageVersion to $packageVersion"