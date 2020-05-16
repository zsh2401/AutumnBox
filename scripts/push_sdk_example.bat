@echo off
..\\nuget.exe push %~nx1 API_KEY -Source https://api.nuget.org/v3/index.json
PAUSE