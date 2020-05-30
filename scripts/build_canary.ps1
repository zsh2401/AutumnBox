# should working on root directory
# You should run this script as :  ./scripts/canary_build.ps1


#Configures
$DNCGUI = [System.IO.Path]::Combine($PSScriptRoot, "../src/AutumnBox.DNCGUI")
$StdExt = [System.IO.Path]::Combine($PSScriptRoot, "../src/AutumnBox.Extensions.DNCStandard")
$EssExt = [System.IO.Path]::Combine($PSScriptRoot, "../src/AutumnBox.Extensions.DNCEssentials")
$CanaryPath = [System.IO.Path]::Combine($PSScriptRoot,"../AutumnBox-Canary/");
$ExtensionsOutputDir = [System.IO.Path]::Combine($CanaryPath, "extensions");
$AdbOutputDir = [System.IO.Path]::Combine($CanaryPath, "adb_binary");
$CompileConfigure = "Release";
$Runtime ="win-x86";
$ADBBinariesDirectoryPath = [System.IO.Path]::Combine($PSScriptRoot, "../adb_binary")
# Functions
function Write-Green($message) {
    [System.Console]::ForegroundColor = [System.ConsoleColor]::Green;
    Write-Output $message
    [System.Console]::ResetColor()
}
function Initialize-Canary(){
    if($CanaryDir.Exists){
        Write-Green $CanaryDir.FullName;
        $CanaryDir.Delete($true)
    }
    [System.IO.Directory]::CreateDirectory($CanaryPath)
}

Initialize-Canary

#Build
Write-Green "Restoring dependencies"
dotnet restore src/

Write-Green "Compiling AutumnBox.DNCGUI"
dotnet publish $DNCGUI -c $CompileConfigure -r $Runtime --no-dependencies --self-contained true -o $CanaryPath

#Copying Adb binaries
Write-Green "Copying adb files"
Copy-Item -Force -Recurse $ADBBinariesDirectoryPath $AdbOutputDir -Exclude **/.git

#Build extensions
Write-Green "Compiling Extensions"
dotnet publish $StdExt -c $CompileConfigure -r $Runtime -o $ExtensionsOutputDir
dotnet publish $EssExt -c $CompileConfigure -r $Runtime -o $ExtensionsOutputDir
Remove-Item -Force -Recurse $([System.IO.Path]::Combine($ExtensionsOutputDir,"*")) -Exclude "AutumnBox.Extensions.*.dll"

#Finished
Write-Green "===Finished==="