# Microsoft Developer Studio Project File - Name="UniVM" - Package Owner=<4>
# Microsoft Developer Studio Generated Build File, Format Version 6.00
# ** DO NOT EDIT **

# TARGTYPE "Win32 (x86) Console Application" 0x0103

CFG=UniVM - Win32 Debug
!MESSAGE This is not a valid makefile. To build this project using NMAKE,
!MESSAGE use the Export Makefile command and run
!MESSAGE 
!MESSAGE NMAKE /f "UniVM.mak".
!MESSAGE 
!MESSAGE You can specify a configuration when running NMAKE
!MESSAGE by defining the macro CFG on the command line. For example:
!MESSAGE 
!MESSAGE NMAKE /f "UniVM.mak" CFG="UniVM - Win32 Debug"
!MESSAGE 
!MESSAGE Possible choices for configuration are:
!MESSAGE 
!MESSAGE "UniVM - Win32 Release" (based on "Win32 (x86) Console Application")
!MESSAGE "UniVM - Win32 Debug" (based on "Win32 (x86) Console Application")
!MESSAGE 

# Begin Project
# PROP AllowPerConfigDependencies 0
# PROP Scc_ProjName ""
# PROP Scc_LocalPath ""
CPP=cl.exe
RSC=rc.exe

!IF  "$(CFG)" == "UniVM - Win32 Release"

# PROP BASE Use_MFC 0
# PROP BASE Use_Debug_Libraries 0
# PROP BASE Output_Dir "Release"
# PROP BASE Intermediate_Dir "Release"
# PROP BASE Target_Dir ""
# PROP Use_MFC 0
# PROP Use_Debug_Libraries 0
# PROP Output_Dir "Release"
# PROP Intermediate_Dir "Release"
# PROP Ignore_Export_Lib 0
# PROP Target_Dir ""
# ADD BASE CPP /nologo /W3 /GX /O2 /D "WIN32" /D "NDEBUG" /D "_CONSOLE" /D "_MBCS" /YX /FD /c
# ADD CPP /nologo /MT /W3 /GX /O2 /D "WIN32" /D "NDEBUG" /D "_CONSOLE" /D "_MBCS" /YX /FD /c
# ADD BASE RSC /l 0x409 /d "NDEBUG"
# ADD RSC /l 0x409 /d "NDEBUG"
BSC32=bscmake.exe
# ADD BASE BSC32 /nologo
# ADD BSC32 /nologo
LINK32=link.exe
# ADD BASE LINK32 kernel32.lib user32.lib gdi32.lib winspool.lib comdlg32.lib advapi32.lib shell32.lib ole32.lib oleaut32.lib uuid.lib odbc32.lib odbccp32.lib kernel32.lib user32.lib gdi32.lib winspool.lib comdlg32.lib advapi32.lib shell32.lib ole32.lib oleaut32.lib uuid.lib odbc32.lib odbccp32.lib /nologo /subsystem:console /machine:I386
# ADD LINK32 kernel32.lib user32.lib gdi32.lib winspool.lib comdlg32.lib advapi32.lib shell32.lib ole32.lib oleaut32.lib uuid.lib odbc32.lib odbccp32.lib kernel32.lib user32.lib gdi32.lib winspool.lib comdlg32.lib advapi32.lib shell32.lib ole32.lib oleaut32.lib uuid.lib odbc32.lib odbccp32.lib /nologo /subsystem:console /machine:I386 /out:"../../../bin/Release/UniVM.exe"

!ELSEIF  "$(CFG)" == "UniVM - Win32 Debug"

# PROP BASE Use_MFC 0
# PROP BASE Use_Debug_Libraries 1
# PROP BASE Output_Dir "Debug"
# PROP BASE Intermediate_Dir "Debug"
# PROP BASE Target_Dir ""
# PROP Use_MFC 0
# PROP Use_Debug_Libraries 1
# PROP Output_Dir "Debug"
# PROP Intermediate_Dir "Debug"
# PROP Ignore_Export_Lib 0
# PROP Target_Dir ""
# ADD BASE CPP /nologo /W3 /Gm /GX /ZI /Od /D "WIN32" /D "_DEBUG" /D "_CONSOLE" /D "_MBCS" /YX /FD /GZ /c
# ADD CPP /nologo /MT /W3 /Gm /GX /ZI /Od /D "WIN32" /D "_DEBUG" /D "_CONSOLE" /D "_MBCS" /YX /FD /GZ /c
# ADD BASE RSC /l 0x409 /d "_DEBUG"
# ADD RSC /l 0x409 /d "_DEBUG"
BSC32=bscmake.exe
# ADD BASE BSC32 /nologo
# ADD BSC32 /nologo
LINK32=link.exe
# ADD BASE LINK32 kernel32.lib user32.lib gdi32.lib winspool.lib comdlg32.lib advapi32.lib shell32.lib ole32.lib oleaut32.lib uuid.lib odbc32.lib odbccp32.lib kernel32.lib user32.lib gdi32.lib winspool.lib comdlg32.lib advapi32.lib shell32.lib ole32.lib oleaut32.lib uuid.lib odbc32.lib odbccp32.lib /nologo /subsystem:console /debug /machine:I386 /pdbtype:sept
# ADD LINK32 kernel32.lib user32.lib gdi32.lib winspool.lib comdlg32.lib advapi32.lib shell32.lib ole32.lib oleaut32.lib uuid.lib odbc32.lib odbccp32.lib kernel32.lib user32.lib gdi32.lib winspool.lib comdlg32.lib advapi32.lib shell32.lib ole32.lib oleaut32.lib uuid.lib odbc32.lib odbccp32.lib /nologo /subsystem:console /debug /machine:I386 /out:"../../../bin/Debug/UniVM.exe" /pdbtype:sept

!ENDIF 

# Begin Target

# Name "UniVM - Win32 Release"
# Name "UniVM - Win32 Debug"
# Begin Group "Source Files"

# PROP Default_Filter "cpp;c;cxx;rc;def;r;odl;idl;hpj;bat"
# Begin Group "univm"

# PROP Default_Filter ".c"
# Begin Source File

SOURCE=..\..\..\src\univm\basesysc.c
# End Source File
# Begin Source File

SOURCE=..\..\..\src\univm\main.c
# End Source File
# End Group
# Begin Group "core"

# PROP Default_Filter ".c"
# Begin Group "dispatch"

# PROP Default_Filter ".c"
# Begin Source File

SOURCE=..\..\..\src\core\dispatch\dispatch.c
# End Source File
# Begin Source File

SOURCE=..\..\..\src\core\dispatch\thread.c
# End Source File
# End Group
# Begin Source File

SOURCE=..\..\..\src\core\advstr.c
# End Source File
# Begin Source File

SOURCE=..\..\..\src\core\base.c
# End Source File
# Begin Source File

SOURCE=..\..\..\src\core\basedata.c
# End Source File
# Begin Source File

SOURCE=..\..\..\src\core\core.c
# End Source File
# Begin Source File

SOURCE=..\..\..\src\core\coredata.c
# End Source File
# Begin Source File

SOURCE=..\..\..\src\core\panic.c
# End Source File
# Begin Source File

SOURCE=..\..\..\src\core\vm.c
# End Source File
# Begin Source File

SOURCE=..\..\..\src\core\vm_func.c
# End Source File
# End Group
# End Group
# Begin Group "Header Files"

# PROP Default_Filter "h;hpp;hxx;hm;inl"
# Begin Group "univm_h"

# PROP Default_Filter ".h"
# Begin Source File

SOURCE=..\..\..\src\univm\basesysc.h
# End Source File
# End Group
# Begin Group "core_h"

# PROP Default_Filter ".h"
# Begin Group "dispatch_h"

# PROP Default_Filter ".h"
# Begin Source File

SOURCE=..\..\..\src\core\dispatch\dispatch.h
# End Source File
# Begin Source File

SOURCE=..\..\..\src\core\dispatch\thread.h
# End Source File
# End Group
# Begin Source File

SOURCE=..\..\..\src\core\advstr.h
# End Source File
# Begin Source File

SOURCE=..\..\..\src\core\base.h
# End Source File
# Begin Source File

SOURCE=..\..\..\src\core\basedata.h
# End Source File
# Begin Source File

SOURCE=..\..\..\src\core\basesysc.h
# End Source File
# Begin Source File

SOURCE=..\..\..\src\core\core.h
# End Source File
# Begin Source File

SOURCE=..\..\..\src\core\data.h
# End Source File
# Begin Source File

SOURCE=..\..\..\src\core\inst.h
# End Source File
# Begin Source File

SOURCE=..\..\..\src\core\instname.h
# End Source File
# Begin Source File

SOURCE=..\..\..\src\core\panic.h
# End Source File
# Begin Source File

SOURCE=..\..\..\src\core\types.h
# End Source File
# Begin Source File

SOURCE=..\..\..\src\core\vm.h
# End Source File
# Begin Source File

SOURCE=..\..\..\src\core\vm_func.h
# End Source File
# End Group
# End Group
# Begin Group "Resource Files"

# PROP Default_Filter "ico;cur;bmp;dlg;rc2;rct;bin;rgs;gif;jpg;jpeg;jpe"
# End Group
# End Target
# End Project
