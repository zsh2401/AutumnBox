#Define constants
$OutDir = $($args[0])
$ConfigurationName = $($args[1])

#Copy adb binaries
$ADBBinariesDirectoryPath = [System.IO.Path]::Combine($PSScriptRoot, "../../adb_binary")
Copy-Item -Force -Recurse $ADBBinariesDirectoryPath $OutDir -Exclude .git

echo "Adb Binaries has been placed in  $ADBBinariesDirectoryPath"