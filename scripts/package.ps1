param(
    [switch]$Release = $False
)

#Requires -Version 7.4
$ErrorActionPreference = 'Stop'
$PSNativeCommandUseErrorActionPreference = $true

Set-Location $PSScriptRoot/..
[Environment]::CurrentDirectory = (Get-Location -PSProvider FileSystem).ProviderPath

$csprojPath = "LuaEngine/LuaEngine.csproj"
$projxml = [xml](Get-Content -Path $csprojPath)
$version = $projxml.Project.PropertyGroup[0].Version

if($Release) {
    $Configuration='Release'
} else {
    $Configuration='Debug'
}

function EnsureDir($path) {
    if(!(Test-Path $path)) { New-Item -Type Directory $path > $null }
}

function Clean() {
    if(Test-Path Build) {
        Remove-Item -Recurse Build
    }
}

function CreateZip($zipPath) {
    if(Test-Path $zipPath) { Remove-Item $zipPath }
    $zip = [System.IO.Compression.ZipFile]::Open($zipPath, 'Create')
    return $zip
}

function ExtractZip($zipPath){
    $targetPath = [System.IO.Path]::Combine([System.IO.Path]::GetDirectoryName($zipPath),[System.IO.Path]::GetFileNameWithoutExtension($zipPath))
    [System.IO.Compression.ZipFile]::ExtractToDirectory($zipPath, $targetPath)
}

function AddToZip($zip, $path, $pathInZip=$path) {
    [System.IO.Compression.ZipFileExtensions]::CreateEntryFromFile($zip, $path, $pathInZip) > $Null
}

$luaZipPath = "Build/luaengine.luamod"

function CreateLuaZip(){
    $zip = CreateZip $luaZipPath

    Push-Location "lua"
    Get-ChildItem -Recurse './*.lua' | ForEach-Object {
        $path = ($_ | Resolve-Path -Relative).Replace('.\', '')
        AddToZip $zip $_.FullName $path
    }
    Pop-Location
    $zip.Dispose()
}

function CreatePluginZip(){
    $zipPath = "Build/LuaEngine.$Configuration-$version.zip"
    $readmePath = "README.md"
    $zip = CreateZip $zipPath

    Push-Location "LuaEngine/bin/$Configuration/net471"
    Get-ChildItem -Recurse './' -Exclude *.pdb,CommonAPI.dll,SlopCrewClient.dll,MapStation.API.dll,MapStation.Tools.dll | ForEach-Object {
        $path = ($_ | Resolve-Path -Relative).Replace('.\', '')
        AddToZip $zip $_.FullName $path
    }
    Pop-Location

    Push-Location "Thunderstore"
    Get-ChildItem -Recurse './' | ForEach-Object {
        $path = ($_ | Resolve-Path -Relative).Replace('.\', '')
        AddToZip $zip $_.FullName $path
    }
    Pop-Location


    if(Test-Path $readmePath){
        AddToZip $zip $readmePath $readmePath
    }

    CreateLuaZip

    AddToZip $zip $luaZipPath "luaengine.luamod"

    $zip.Dispose()

    Remove-Item $luaZipPath

    ExtractZip $zipPath
}

function CreatePackageZip(){
	$zipPath = "Build/com.lazyduchess.luaengine-$version.zip"
	$zip = CreateZip $zipPath
	
	Push-Location "LuaEngine.Editor/Packages/com.lazyduchess.luaengine"
    Get-ChildItem -Recurse './' | ForEach-Object {
        $path = ($_ | Resolve-Path -Relative).Replace('.\', '')
		if(Test-Path -Path $_.FullName -PathType leaf){
			AddToZip $zip $_.FullName $path
		}
    }
    Pop-Location
	$zip.Dispose()
	
	ExtractZip $zipPath
}

Clean
dotnet build -c $Configuration
EnsureDir "Build"
CreatePluginZip
CreatePackageZip

