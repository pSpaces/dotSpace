del *.nupkg
nuget pack dotSpace\dotSpace.csproj

FOR /f %%f in ('dir /b *.nupkg') DO nuget.exe push -Source https://api.nuget.org/v3/index.json %%f
