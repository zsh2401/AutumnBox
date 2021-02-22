#Define constants
$OutDir = $($args[0])
$ConfigurationName = $($args[1])

#Build extensions
$ExtensionsOutputDir = [System.IO.Path]::Combine($OutDir, "extensions");
$StdExt = [System.IO.Path]::Combine($PSScriptRoot, "../AutumnBox.Extensions.Standard")
$EssExt = [System.IO.Path]::Combine($PSScriptRoot, "../AutumnBox.Extensions.Essentials")

if (![System.IO.Directory]::Exists($ExtensionsOutputDir)) {
    [System.IO.Directory]::CreateDirectory($ExtensionsOutputDir);
}
dotnet build -c $ConfigurationName $StdExt -o $ExtensionsOutputDir
dotnet build -c $ConfigurationName $EssExt -o $ExtensionsOutputDir

Remove-Item $([System.IO.Path]::Combine($ExtensionsOutputDir, "AutumnBox.Core.*")) 

echo "Extensions has been built and installed successfully"