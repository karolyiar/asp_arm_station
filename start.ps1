$stdErrLog = "C:\tmp\stderr.log"
$stdOutLog = "C:\tmp\stdout.log"
#$proc = Start-Process -NoNewWindow -WorkingDirectory c:\tmp -RedirectStandardOutput $stdOutLog -RedirectStandardError $stdErrLog c:\dotnet\dotnet.exe c:\tmp\asp_arm.dll -Wait
#$proc | kill
Set-Location C:\tmp
powershell.exe -Command "& c:\dotnet\dotnet.exe c:\tmp\asp_arm.dll 2>&1 >> $stdOutLog"