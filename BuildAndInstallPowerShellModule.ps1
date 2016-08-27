$msbuild = "C:\Program Files (x86)\MSBuild\14.0\Bin\msbuild.exe"
$documents = [Environment]::GetFolderPath("MyDocuments")
$targetPath = "$documents\WindowsPowerShell\Modules\RabbitMqLogger"

& $msbuild RabbitMqLogger.sln /p:Configuration=Debug

New-Item $targetPath -type directory

Write-Output "Copying files to $targetPath"
cp RabbitMqLogger.psm1 $targetPath
cp bin\Debug\*.dll $targetPath