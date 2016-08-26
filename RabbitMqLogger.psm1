Add-Type -Path "RabbitMQ.Client.dll"
Add-Type -Path "EasyNetQ.dll"
Add-Type -Path "RabbitMqLogger.dll"

$global:rmqLogger = $null

function Create-RmqLogger($host, $topic = "GlobalLogging") {
	$global:rmqLogger = New-Object RabbitMqLogger.Logger -ArgumentList $host, $topic
}

function Write-DebugRmqLog($message, $type = [RabbitMqLogger.MessageType]::Info, $category = "Uncategorized") {
	$logger.Debug($message, $type, $category)
}

function Write-InfoRmqLog($message, $type = [RabbitMqLogger.MessageType]::Info, $category = "Uncategorized") {
	$logger.Info($message, $type, $category)
}

function Write-WarnRmqLog($message, $type = [RabbitMqLogger.MessageType]::Info, $category = "Uncategorized") {
	$logger.Warn($message, $type, $category)
}

function Write-ErrorRmqLog($message, $type = [RabbitMqLogger.MessageType]::Info, $category = "Uncategorized", $error = $null) {
	$logger.Error($message, $type, $category, $error)
}

function Write-FatalRmqLog($message, $type = [RabbitMqLogger.MessageType]::Info, $category = "Uncategorized", $error = $null) {
	$logger.Fatal($message, $type, $category, $error)
}

Export-ModuleMember -Function Create-RmqLogger
Export-ModuleMember -Function Write-DebugRmqLog
Export-ModuleMember -Function Write-InfoRmqLog
Export-ModuleMember -Function Write-WarnRmqLog
Export-ModuleMember -Function Write-ErrorRmqLog
Export-ModuleMember -Function Write-FatalRmqLog