Set-Location $PSScriptRoot/..
$ErrorActionPreference = 'Stop'

# `dotnet clean` doesn't delete everything.

Write-Output `
    .\src\bin `
    .\src\obj `
| ForEach-Object {
    if(test-path $_) { remove-item -recurse $_ }
}