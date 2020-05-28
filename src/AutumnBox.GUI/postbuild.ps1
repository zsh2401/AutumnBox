#Constants
$OutDir = $($args[0])

#Copy adb binaries
$ADBBinariesDirectoryPath = [System.IO.Path]::Combine($PSScriptRoot, "../../adb_binary")
Copy-Item -Force -Recurse $ADBBinariesDirectoryPath $OutDir -Exclude .git