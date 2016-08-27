param(
    $installLocal = $false
)

$msbuild = "C:\Program Files (x86)\MSBuild\14.0\Bin\msbuild.exe"
$documents = [Environment]::GetFolderPath("MyDocuments")
$programFiles = $env:programfiles
$targetRoot = $programFiles
if ($installLocal) {
    $targetRoot = $documents
}
$targetPath = "$targetRoot\WindowsPowerShell\Modules\RabbitMqLogger"

& $msbuild RabbitMqLogger.sln /p:Configuration=Debug

New-Item $targetPath -type directory

Write-Output "Copying files to $targetPath"
cp RabbitMqLogger.psm1 $targetPath
cp bin\Debug\*.dll $targetPath