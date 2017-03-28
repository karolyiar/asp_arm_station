REM automate nuget process

mkdir lib\uap10.0
copy bin\release\GpioWrapper.dll lib\uap10.0 /y
del *.nupkg
d:\download\nuget pack GpioWrapper.nuspec
del lib\uap10.0\*.* /q
rmdir lib /s /q
