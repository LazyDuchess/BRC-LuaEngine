param(
    [switch]$major = $False,
    [switch]$minor = $False,
    [switch]$patch = $False,
    [switch]$git = $True
)

$ErrorActionPreference = 'Stop'
$PSNativeCommandUseErrorActionPreference = $true

Set-Location $PSScriptRoot/..
[Environment]::CurrentDirectory = (Get-Location -PSProvider FileSystem).ProviderPath

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

$version = $projxml.Project.PropertyGroup.Version

$versionArray = $version[0].Split(".")

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

Write-Output "Bumping from $version to $newVersion"

$projxml.Project.PropertyGroup.Version[0] = $newVersion
$projxml.Save($csprojPath)