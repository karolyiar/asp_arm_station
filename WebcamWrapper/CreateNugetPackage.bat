REM automate nuget process

mkdir lib\uap10.0
copy bin\release\WebcamWrapper.dll lib\uap10.0 /y
del *.nupkg
d:\download\nuget pack WebcamWrapper.nuspec
del lib\uap10.0\*.* /q
rmdir lib /s /q
