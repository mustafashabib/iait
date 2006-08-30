; Script generated with the Venis Install Wizard

; Define your application name
!define APPNAME "iTunes Album Art Tagger"
!define APPNAMEANDVERSION "iTunes Album Art Tagger 1.0"

; Main Install settings
Name "${APPNAMEANDVERSION}"
InstallDir "$PROGRAMFILES\iTunesAlbumArtTagger"
InstallDirRegKey HKLM "Software\${APPNAME}" ""
OutFile "D:\Projects\itunes_tag_art\AlbumArtTagger\AlbumArtTagger\AlbumArtTagger.exe"

; Modern interface settings
!include "MUI.nsh"

!define MUI_ABORTWARNING
!define MUI_FINISHPAGE_RUN "$INSTDIR\AlbumArtTagger.exe"

!insertmacro MUI_PAGE_WELCOME
!insertmacro MUI_PAGE_DIRECTORY
!insertmacro MUI_PAGE_INSTFILES
!insertmacro MUI_PAGE_FINISH

!insertmacro MUI_UNPAGE_CONFIRM
!insertmacro MUI_UNPAGE_INSTFILES

; Set languages (first is default language)
!insertmacro MUI_LANGUAGE "English"
!insertmacro MUI_RESERVEFILE_LANGDLL

Section "AlbumArtTagger" Section1

	; Set Section properties
	SetOverwrite on

	; Set Section Files and Shortcuts
	SetOutPath "$INSTDIR\"
	File "..\bin\Release\Interop.iTunesLib.dll"
	File "..\bin\Release\AlbumArtTagger.XmlSerializers.dll"
	File "..\bin\Release\AlbumArtTagger.exe.config"
	File "..\bin\Release\AlbumArtTagger.vshost.exe"
	File "..\bin\Release\AlbumArtTagger.exe"
	File "..\bin\Release\AlbumArtTagger.vshost.exe.config"
	CreateShortCut "$DESKTOP\iTunes Album Art Tagger.lnk" "$INSTDIR\AlbumArtTagger.exe"
	CreateDirectory "$SMPROGRAMS\iTunes Album Art Tagger"
	CreateShortCut "$SMPROGRAMS\iTunes Album Art Tagger\iTunes Album Art Tagger.lnk" "$INSTDIR\AlbumArtTagger.exe"
	CreateShortCut "$SMPROGRAMS\iTunes Album Art Tagger\Uninstall.lnk" "$INSTDIR\uninstall.exe"

SectionEnd

Section -FinishSection

	WriteRegStr HKLM "Software\${APPNAME}" "" "$INSTDIR"
	WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APPNAME}" "DisplayName" "${APPNAME}"
	WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APPNAME}" "UninstallString" "$INSTDIR\uninstall.exe"
	WriteUninstaller "$INSTDIR\uninstall.exe"

SectionEnd

; Modern install component descriptions
!insertmacro MUI_FUNCTION_DESCRIPTION_BEGIN
	!insertmacro MUI_DESCRIPTION_TEXT ${Section1} ""
!insertmacro MUI_FUNCTION_DESCRIPTION_END

;Uninstall section
Section Uninstall

	;Remove from registry...
	DeleteRegKey HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APPNAME}"
	DeleteRegKey HKLM "SOFTWARE\${APPNAME}"

	; Delete self
	Delete "$INSTDIR\uninstall.exe"

	; Delete Shortcuts
	Delete "$DESKTOP\iTunes Album Art Tagger.lnk"
	Delete "$SMPROGRAMS\iTunes Album Art Tagger\iTunes Album Art Tagger.lnk"
	Delete "$SMPROGRAMS\iTunes Album Art Tagger\Uninstall.lnk"

	; Clean up ImageTagger
	Delete "$INSTDIR\Interop.iTunesLib.dll"
	Delete "$INSTDIR\AlbumArtTagger.XmlSerializers.dll"
	Delete "$INSTDIR\AlbumArtTagger.exe.config"
	Delete "$INSTDIR\AlbumArtTagger.vshost.exe"
	Delete "$INSTDIR\AlbumArtTagger.exe"
	Delete "$INSTDIR\AlbumArtTagger.vshost.exe.config"

	; Remove remaining directories
	RMDir "$SMPROGRAMS\iTunes Album Art Tagger"
	RMDir "$INSTDIR\"

SectionEnd

; eof
