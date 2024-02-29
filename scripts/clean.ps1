Set-Location $PSScriptRoot/..
$ErrorActionPreference = 'Stop'

# `dotnet clean` doesn't delete everything.

Write-Output `
    .\LuaEngine\bin `
    .\LuaEngine\obj `
| ForEach-Object {
    if(test-path $_) { remove-item -recurse $_ }
}