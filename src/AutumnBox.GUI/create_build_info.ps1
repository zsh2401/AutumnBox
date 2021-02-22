#Define constants
$OutDir = $($args[0])
$ConfigurationName = $($args[1])

$FilePath = [System.IO.Path]::Combine($OutDir,"build.ini");

#Content
$Now = [System.DateTime]::Now.ToString("yyyy/MM/dd HH:mm:ss")
$Commit =  git rev-parse HEAD
$LatestTag = git describe --abbrev=0 --tags 
$Content="[Build]
datetime=$Now
commit=$Commit
tag=$LatestTag
"
#Write to file
$fs = [System.IO.FileStream]::new($FilePath,[System.IO.FileMode]::OpenOrCreate);
$sw = [System.IO.StreamWriter]::new($fs);
$sw.Write($Content);
$sw.Flush();
$sw.Dispose();
$fs.Dispose();
