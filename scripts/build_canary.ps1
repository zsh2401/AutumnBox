# should working on root directory
# You should run this script as :  ./scripts/canary_build.ps1
$MainProj = [System.IO.Path]::Combine($PSScriptRoot, "../src/AutumnBox.GUI")
$StdExt = [System.IO.Path]::Combine($PSScriptRoot, "../src/AutumnBox.Extensions.Standard")
$EssExt = [System.IO.Path]::Combine($PSScriptRoot, "../src/AutumnBox.Extensions.Essentials")
$CanaryPath = [System.IO.Path]::Combine($PSScriptRoot,"../AutumnBox-Canary/");
$ExtensionsOutputDir = [System.IO.Path]::Combine($CanaryPath, "extensions");
$AdbSetupPath = [System.IO.Path]::Combine($CanaryPath, "adb_binary");
$CompileConfigure = "Canary";
$Runtime ="win-x86";
$ADBBinariesDirectoryPath = [System.IO.Path]::Combine($PSScriptRoot, "../adb_binary")


function Step($message,$command){
    Write-Green $message
    &$command
}
function Write-Green($message) {
    [System.Console]::ForegroundColor = [System.ConsoleColor]::Green;
    Write-Output $message
    [System.Console]::ResetColor()
}
function Initialize-ADBFiles(){
    $ADBBinariesDirectory = [System.IO.DirectoryInfo]::new($ADBBinariesDirectoryPath);
    if($ADBBinariesDirectory.Exists){
        Remove-Item -Force -Recurse $ADBBinariesDirectoryPath
    }

    $ADBGitStore = "https://github.com/zsh2401/AutumnBox-AdbBinaries-Store"
    $BranchName = "1.0.41"
    $ADBBinariesDirectoryPath = [System.IO.Path]::Combine($PSScriptRoot, "../adb_binary")
    $ADBBinariesDirectory = [System.IO.DirectoryInfo]::new($ADBBinariesDirectoryPath);

    if ($ADBBinariesDirectory.Exists) {
        Remove-Item $ADBBinariesDirectory.FullName -Force -Recurse;
        $ADBBinariesDirectory.Create();
    }
    git clone -b $BranchName $ADBGitStore $ADBBinariesDirectory.FullName
}
function Initialize-OutputDir(){
    if($CanaryDir.Exists){
        Write-Green $CanaryDir.FullName;
        Remove-Item -Force -Recurse $CanaryPath
    }
    [System.IO.Directory]::CreateDirectory($CanaryPath)
}
function Initialize-Env {
    dotnet restore src/
}
function Compile-MainProgram{
    dotnet publish $MainProj -c $CompileConfigure -r $Runtime --no-dependencies --self-contained true -o $CanaryPath
}
function Compile-Extensions{
    dotnet publish $StdExt -c $CompileConfigure -r $Runtime -o $ExtensionsOutputDir
    dotnet publish $EssExt -c $CompileConfigure -r $Runtime -o $ExtensionsOutputDir
    Remove-Item -Force -Recurse $([System.IO.Path]::Combine($ExtensionsOutputDir,"*")) -Exclude "AutumnBox.Extensions.*.dll"
}
function Setup-ADB{
    Copy-Item -Force -Recurse $ADBBinariesDirectoryPath $AdbSetupPath -Exclude .git
}

Initialize-OutputDir
Initialize-ADBFiles
Compile-MainProgram
Compile-Extensions
Setup-ADB

#Finished
Write-Green "===Finished==="