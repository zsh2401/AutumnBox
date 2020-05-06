@ECHO OFF
nuget pack ../src/AutumnBox.OpenFramework/AutumnBox.OpenFramework.csproj -Prop Configuration=SDK -OutputDirectory out
PAUSE