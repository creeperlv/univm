@echo off
mkdir bin
cd bin
cl.exe ..\src\core\*.c ..\src\univm\*.c /Feunivm.exe
cl.exe ..\src\core\*.c ..\src\functest\*.c /Fefunctest.exe
del *.obj
cd ..
