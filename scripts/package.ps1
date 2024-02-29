param(
    [switch]$Release = $False
)

#Requires -Version 7.4
$ErrorActionPreference = 'Stop'
$PSNativeCommandUseErrorActionPreference = $true

Set-Location $PSScriptRoot/..
[Environment]::CurrentDirectory = (Get-Location -PSProvider FileSystem).ProviderPath

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
    $zipPath = "Build/LuaEngine.$Configuration.zip"
    $readmePath = "README.md"
    $zip = CreateZip $zipPath

    Push-Location "src/bin/$Configuration/net46"
    Get-ChildItem -Recurse './' -Exclude '*.pdb' | ForEach-Object {
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

Clean
dotnet build -c $Configuration
EnsureDir "Build"
CreatePluginZip

