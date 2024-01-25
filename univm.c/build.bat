@echo off
mkdir bin
cd bin
cl.exe ..\src\core\*.c ..\src\core\dispatch*.c ..\src\univm\*.c /Feunivm.exe /MT /nologo > nil
cl.exe ..\src\core\*.c ..\src\core\dispatch*.c ..\src\functest\*.c /Fefunctest.exe /MT /nologo > nil
del nil
del *.obj
cd ..
