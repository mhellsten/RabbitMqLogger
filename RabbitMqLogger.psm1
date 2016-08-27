$scriptPath = Split-Path -parent $PSCommandPath

Add-Type -Path "$scriptPath\RabbitMQ.Client.dll"
Add-Type -Path "$scriptPath\EasyNetQ.dll"
Add-Type -Path "$scriptPath\RabbitMqLogger.dll"

$global:rmqLogger = $null

function New-RmqLogger($host, $topic = "GlobalLogging") {
	$global:rmqLogger = New-Object RabbitMqLogger.Logger -ArgumentList $host, $topic
}

function Write-DebugRmqLog($message, $type = [RabbitMqLogger.MessageType]::Info, $category = "Uncategorized") {
	$global:rmqLogger.Debug($message, $type, $category)
}

function Write-InfoRmqLog($message, $type = [RabbitMqLogger.MessageType]::Info, $category = "Uncategorized") {
	$global:rmqLogger.Info($message, $type, $category)
}

function Write-WarnRmqLog($message, $type = [RabbitMqLogger.MessageType]::Info, $category = "Uncategorized") {
	$global:rmqLogger.Warn($message, $type, $category)
}

function Write-ErrorRmqLog($message, $type = [RabbitMqLogger.MessageType]::Info, $category = "Uncategorized", $error = $null) {
	$global:rmqLogger.Error($message, $type, $category, $error)
}

function Write-FatalRmqLog($message, $type = [RabbitMqLogger.MessageType]::Info, $category = "Uncategorized", $error = $null) {
	$global:rmqLogger.Fatal($message, $type, $category, $error)
}

Export-ModuleMember -Function New-RmqLogger
Export-ModuleMember -Function Write-DebugRmqLog
Export-ModuleMember -Function Write-InfoRmqLog
Export-ModuleMember -Function Write-WarnRmqLog
Export-ModuleMember -Function Write-ErrorRmqLog
Export-ModuleMember -Function Write-FatalRmqLog