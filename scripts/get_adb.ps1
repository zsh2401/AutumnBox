$ADBGitStore = "https://github.com/zsh2401/AutumnBox-AdbBinaries-Store"
$BranchName = "1.0.40"
$ADBBinariesDirectoryPath = [System.IO.Path]::Combine($PSScriptRoot, "../adb_binary")
$ADBBinariesDirectory = [System.IO.DirectoryInfo]::new($ADBBinariesDirectoryPath);
if ($ADBBinariesDirectory.Exists) {
    Remove-Item $ADBBinariesDirectory.FullName -Force -Recurse;
    $ADBBinariesDirectory.Create();
}
git clone -b $BranchName $ADBGitStore $ADBBinariesDirectory.FullName