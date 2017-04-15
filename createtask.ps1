schtasks /Delete /TN asparm /f
schtasks /Create /SC ONLOGON /TN asparm /TR "powershell -file C:\tmp\start.ps1" /ru "MINWINPC\DefaultAccount"