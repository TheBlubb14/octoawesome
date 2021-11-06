function grantRights($file) {
	takeown /f $file
	icacls $file /grant "${env:ComputerName}\${env:UserName}:F"
}

function InstallMesaOpenGL ($arch, $basedir) {
	$source_path = "./download/build/$arch/opengl32.dll"
	$filepath = $basedir + "opengl32.dll"
	if (Test-Path $filepath) {
		grantRights $filepath
		Rename-Item -Path $filepath -NewName opengl32.dll_old
	}
	Copy-Item $source_path $filepath
}

Invoke-WebRequest https://github.com/jvbsl/MesaBinary/releases/download/19/opengl_win_dll.zip -OutFile download.zip
Expand-Archive -Force ./download.zip

InstallMesaOpenGL "x86" "$env:WINDIR\SysWOW64\"
InstallMesaOpenGL "x86_64" "$env:WINDIR\system32\"