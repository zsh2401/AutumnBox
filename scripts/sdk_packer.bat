@ECHO OFF
nuget pack ../src/AutumnBox.Core/AutumnBox.Core.csproj -Prop Configuration=SDK -OutputDirectory out
PAUSE