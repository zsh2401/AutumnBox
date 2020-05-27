$NetCoreSDK = [System.IO.Path]::Combine($PSScriptRoot,"../../src/AutumnBox.STDCore");
$NetFxSDK = [System.IO.Path]::Combine($PSScriptRoot,"../../src/AutumnBox.Core");
$TmpDir =  [System.IO.Path]::Combine($PSScriptRoot,"tmp");
$NugetExe =  [System.IO.Path]::Combine($PSScriptRoot,"nuget.exe")
$Nuspec = [System.IO.Path]::Combine($PSScriptRoot,"AutumnBox.SDK.nuspec")
$NugetPackOutputDir = [System.IO.Path]::Combine($PSScriptRoot,"packages")
$API_KEY_FILE = [System.IO.Path]::Combine($PSScriptRoot,"API_KEY")

Write-Output $CacheDirectory
function Build-SDK(){
    dotnet build $NetCoreSDK -o "$TmpDir/netcore31" -c SDK
    dotnet build $NetFxSDK -o "$TmpDir/net45" -c SDK
    &$NugetExe pack $Nuspec -OutputDirectory $CacheDirectory
}
function Push-SDK($version){
    $CacheDirPath = $([System.IO.Path]::Combine($PSScriptRoot,[System.Guid]::NewGuid().ToString()))
    $CacheDirectory = [System.IO.DirectoryInfo]::new($CacheDirPath);
    $API_KEY = [System.IO.File]::ReadAllText($API_KEY_FILE);
    try{
        &$NugetExe pack $Nuspec -OutputDirectory $CacheDirectory.FullName
        &$NugetExe push "$($CacheDirectory.FullName)\*" $API_KEY -Source https://api.nuget.org/v3/index.json
        $CacheDirectory.Delete($true);
    }catch{

    }
}