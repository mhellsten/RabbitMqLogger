$scriptPath = Split-Path -parent $PSCommandPath

Add-Type -Path "$scriptPath\RabbitMQ.Client.dll"
Add-Type -Path "$scriptPath\EasyNetQ.dll"
Add-Type -Path "$scriptPath\RabbitMqLogger.dll"

# Create convenience access to the logger and enum values in a global object
$global:rmq = @{}
$global:rmq.Info = [RabbitMqLogger.MessageType]::Info
$global:rmq.JobStart = [RabbitMqLogger.MessageType]::JobStart
$global:rmq.JobEnd = [RabbitMqLogger.MessageType]::JobEnd
$global:rmq.StepStart = [RabbitMqLogger.MessageType]::StepStart
$global:rmq.StepEnd = [RabbitMqLogger.MessageType]::StepEnd

function New-RmqLogger($host, $topic = "GlobalLogging") {
	$global:rmq.Logger = New-Object RabbitMqLogger.Logger -ArgumentList $host, $topic
}

function Assert-Logger() {
	if (-Not $global:rmq.Logger) {
		Write-Error "RMQ logger has not been initialized. Use New-RmqLogger."
	}
}

function Write-DebugRmqLog(
		[Parameter(Mandatory=$true)][string]$message, 
		[RabbitMqLogger.MessageType]$type = $global:rmq.Info, 
		[string]$category = "Uncategorized") {
	Assert-Logger
	$global:rmq.Logger.Debug($message, $type, $category)
}

function Write-InfoRmqLog(
		[Parameter(Mandatory=$true)][string]$message, 
		[RabbitMqLogger.MessageType]$type = $global:rmq.Info, 
		[string]$category = "Uncategorized") {
	Assert-Logger
	$global:rmq.Logger.Info($message, $type, $category)
}

function Write-WarnRmqLog(
		[Parameter(Mandatory=$true)][string]$message, 
		[RabbitMqLogger.MessageType]$type = $global:rmq.Info, 
		[string]$category = "Uncategorized") {
	Assert-Logger
	$global:rmq.Logger.Warn([string]$message, $type, $category)
}

function Write-ErrorRmqLog(
		[Parameter(Mandatory=$true)][string]$message, 
		[RabbitMqLogger.MessageType]$type = $global:rmq.Info, 
		[string]$category = "Uncategorized", 
		$error = $null) {
	Assert-Logger
	$global:rmq.Logger.Error($message, $type, $category, $error)
}

function Write-FatalRmqLog(
		[string]$message, 
		[RabbitMqLogger.MessageType]$type = $global:rmq.Info, 
		[string]$category = "Uncategorized", 
		$error = $null) {
	Assert-Logger
	$global:rmq.Logger.Fatal($message, $type, $category, $error)
}

Export-ModuleMember -Function New-RmqLogger
Export-ModuleMember -Function Write-DebugRmqLog
Export-ModuleMember -Function Write-InfoRmqLog
Export-ModuleMember -Function Write-WarnRmqLog
Export-ModuleMember -Function Write-ErrorRmqLog
Export-ModuleMember -Function Write-FatalRmqLog