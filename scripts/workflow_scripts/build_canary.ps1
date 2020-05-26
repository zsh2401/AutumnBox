# should working on root directory
# You should run this script as :  ./scripts/workflow_scripts/build_canary.ps1

# Functions
function Write-Green($message) {
    [System.Console]::ForegroundColor = [System.ConsoleColor]::Green;
    Write-Output $message
    [System.Console]::ResetColor()
}

#Configures
$CanaryPath = "AutumnBox-Canary/";
$CanaryExtensionsPath = [System.IO.Path]::Combine($CanaryPath, "extensions");
$Runtime = "win-x86";
$CompileConfigure = "Release";

#Build
Write-Green "Restoring dependencies"
dotnet restore src/

Write-Green "Compiling AutumnBox.DNCGUI"
dotnet publish src/AutumnBox.DNCGUI -c $CompileConfigure -r $Runtime --no-dependencies --self-contained true --no-restore --output $CanaryPath

Write-Green "Compiling Extensions."
dotnet publish src/AutumnBox.Extensions.DNCEssentials -c $CompileConfigure -r $Runtime --output $CanaryExtensionsPath

#Finishing
Write-Green "Clear Useless files."
Remove-Item -Recurse -Force $([System.IO.Path]::Combine($CanaryExtensionsPath, "*")) -Exclude AutumnBox.Extensions.*.dll;

#Finished
Write-Green "===Finished==="