$ErrorActionPreference = 'Stop'
$PSNativeCommandUseErrorActionPreference = $true

Set-Location $PSScriptRoot/..
[Environment]::CurrentDirectory = (Get-Location -PSProvider FileSystem).ProviderPath

$easyDecalPath = [System.IO.Path]::Combine($env:BRCPath, "Bomb Rush Cyberfunk_Data/Managed/EasyDecal.Runtime.dll")
$easyDecalTargetPath = "LuaEngine.Editor/Assets"
Copy-Item $easyDecalPath -Destination $easyDecalTargetPath