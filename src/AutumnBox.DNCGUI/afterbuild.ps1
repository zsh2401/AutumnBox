#Define constants
$OutDir = $($args[0])
$ConfigurationName = $($args[1])

#Copy adb binaries
$ADBBinariesDirectoryPath = [System.IO.Path]::Combine($PSScriptRoot, "../../adb_binary")
Copy-Item -Force -Recurse $ADBBinariesDirectoryPath $OutDir -Exclude .git

#Build extensions
$ExtensionsOutputDir = [System.IO.Path]::Combine($OutDir, "extensions");
$StdExt = [System.IO.Path]::Combine($PSScriptRoot, "../AutumnBox.Extensions.DNCStandard")
$EssExt = [System.IO.Path]::Combine($PSScriptRoot, "../AutumnBox.Extensions.DNCEssentials")

if (![System.IO.Directory]::Exists($ExtensionsOutputDir)) {
    [System.IO.Directory]::CreateDirectory($ExtensionsOutputDir);
}
dotnet build -c $ConfigurationName $StdExt -o $ExtensionsOutputDir
dotnet build -c $ConfigurationName $EssExt -o $ExtensionsOutputDir

Remove-Item $([System.IO.Path]::Combine($ExtensionsOutputDir, "*")) -Exclude "AutumnBox.Extensions.*.dll"