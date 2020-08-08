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
$RepoRootPath = [System.IO.Path]::Combine($PSScriptRoot,"../");
$ADBBinariesDirectoryPath = [System.IO.Path]::Combine($RepoRootPath, "adb_binary")


function Step($message,$command){
    Write-Green $message
    &$command
}
function Write-Green($message) {
    [System.Console]::ForegroundColor = [System.ConsoleColor]::Green;
    Write-Output $message
    [System.Console]::ResetColor()
}
function Initialize-OutputDir(){
    if($([System.IO.Directory]::Exists($CanaryPath))){
        Write-Green Cleaning
        Remove-Item -Force -Recurse $CanaryPath
    }
    [System.IO.Directory]::CreateDirectory($CanaryPath)
}
function Initialize-Env {
    dotnet restore src/
}
function Compile-MainProgram{
    dotnet publish $MainProj -c $CompileConfigure -r $Runtime -p:PublishSingleFile=true --no-dependencies --self-contained true -o $CanaryPath
    # Remove-Item -Force -Recurse $([System.IO.Path]::Combine($CanaryPath,"*")) -Exclude "AutumnBox.GUI.exe"
}
function Compile-Extensions{
    dotnet publish $StdExt -c $CompileConfigure -r $Runtime -o $ExtensionsOutputDir
    dotnet publish $EssExt -c $CompileConfigure -r $Runtime -o $ExtensionsOutputDir
    Remove-Item -Force -Recurse $([System.IO.Path]::Combine($ExtensionsOutputDir,"*")) -Exclude "AutumnBox.Extensions.*.dll"
}
function Setup-ADB{
    $ADBGitStore = "https://github.com/zsh2401/AutumnBox-AdbBinaries-Store"
    $BranchName = "1.0.41"
    $Target = [System.IO.Path]::Combine($CanaryPath,"adb_binary")
    git clone --depth 1 -b $BranchName $ADBGitStore $Target
    Remove-Item -Force -Recurse $([System.IO.Path]::Combine($Target,".git"))
}
function Make-Archive{
    $compress = @{
        Path = $CanaryPath
        CompressionLevel = "Optimal"
        DestinationPath = [System.IO.Path]::Combine($RepoRootPath,"archive.zip")
    }
    Compress-Archive -Force @compress
}

Initialize-OutputDir
Compile-MainProgram
Compile-Extensions
Setup-ADB
Make-Archive

#Finished
Write-Green "===Finished==="