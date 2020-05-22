@ECHO OFF
rmdir /s/q extensions || echo ""

rmdir /s/q storage || echo ""
rmdir /s/q cache || echo ""

rmdir /s/q %TEMP%\AutumnBox || echo ""
rmdir /s/q %APPDATA%\AutumnBox || echo ""

start msiexec.exe /x {AF7DF473-1080-4D8B-AF84-3920F125AD0F}
