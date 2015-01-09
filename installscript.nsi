; example2.nsi
;
; This script is based on example1.nsi, but it remember the directory, 
; has uninstall support and (optionally) installs start menu shortcuts.
;
; It will install example2.nsi into a directory that the user selects,

;--------------------------------

; The name of the installer
Name "FolderDecorator"

; The file to write
OutFile "Setup.exe"

; The default installation directory
InstallDir $PROGRAMFILES\FolderDecorator

; Registry key to check for directory (so if you install again, it will 
; overwrite the old one automatically)
InstallDirRegKey HKLM "Software\FolderDecorator" "Install_Dir"

; Request application privileges for Windows Vista
RequestExecutionLevel admin

;--------------------------------

; Pages

Page components
Page directory
Page instfiles

UninstPage uninstConfirm
UninstPage instfiles

;--------------------------------

; The stuff to install
Section "FolderDecorator (required)"

  SectionIn RO
  
  ; Set output path to the installation directory.
  SetOutPath $INSTDIR
  
  ; Put file there
 File Autofac.dll
 File Autofac.xml
 File FolderDesigner.exe
 File FolderDesigner.exe.config
 File FolderDesigner.pdb
 File Magick.NET-AnyCPU.dll
 File Magick.NET-AnyCPU.xml
 File Newtonsoft.Json.dll
 File Newtonsoft.Json.xml
  
  ; Write the installation path into the registry
  WriteRegStr HKLM SOFTWARE\FolderDecorator "Install_Dir" "$INSTDIR"
  
  ; Write the uninstall keys for Windows
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\FolderDecorator" "DisplayName" "FolderDecorator"
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\FolderDecorator" "UninstallString" '"$INSTDIR\uninstall.exe"'
  WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\FolderDecorator" "NoModify" 1
  WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\FolderDecorator" "NoRepair" 1
  WriteUninstaller "uninstall.exe"
  
SectionEnd

; Optional section (can be disabled by the user)
Section "Start Menu Shortcuts"

  CreateDirectory "$SMPROGRAMS\FolderDecorator"
  CreateShortcut "$SMPROGRAMS\FolderDecorator\Uninstall.lnk" "$INSTDIR\uninstall.exe" "" "$INSTDIR\uninstall.exe" 0
  CreateShortcut "$SMPROGRAMS\FolderDecorator\FolderDecorator.lnk" "$INSTDIR\FolderDesigner.exe" "" "$INSTDIR\FolderDesigner.exe" 0
  
SectionEnd

;--------------------------------

; Uninstaller

Section "Uninstall"
  
  ; Remove registry keys
  DeleteRegKey HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\FolderDecorator"
  DeleteRegKey HKLM SOFTWARE\FolderDecorator

  ; Remove files and uninstaller
  
  Delete $INSTDIR\uninstall.exe
  
 Delete $INSTDIR\Autofac.dll
 Delete $INSTDIR\Autofac.xml
 Delete $INSTDIR\folderdecorator.exe
 Delete $INSTDIR\FolderDesigner.exe
 Delete $INSTDIR\FolderDesigner.exe.config
 Delete $INSTDIR\FolderDesigner.pdb
 Delete $INSTDIR\Magick.NET-AnyCPU.dll
 Delete $INSTDIR\Magick.NET-AnyCPU.xml
 Delete $INSTDIR\Newtonsoft.Json.dll
 Delete $INSTDIR\Newtonsoft.Json.xml

  ; Remove shortcuts, if any
  Delete "$SMPROGRAMS\FolderDecorator\*.*"

  ; Remove directories used
  RMDir "$SMPROGRAMS\FolderDecorator"
  RMDir "$INSTDIR"

SectionEnd
