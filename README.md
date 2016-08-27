# RabbitMqLogger

## What is it?
This is a RabbitMQ-based pub/sub logging library for C#.NET, with a PowerShell module wrapper. It also includes a console-based real-time log watcher.

##What's the point?
I have various scripts and services that perform automated tasks or need to be monitored. These are distributed across different machines and therefore it is tedious and impractical to access log files for a quick status check. Using messaging queues, we can easily monitor as much or as little of the published log messages as we want. Since multiple queues can be set up on an exchange, we can have multiple listeners, for example: a real-time viewer, a database writer, and a file writer.

##Pre-Requisites
* A RabbitMQ service

##Build and Installation:

    .\BuildAndInstallPowerShellModule.ps1

##Usage Example 
(RabbitMQ running on rabbit.local)

    Import-Module RabbitMqLogger
    New-RmqLogger rabbit.local
    Write-InfoRmqLog "Test logging"
    Write-ErrorRmqLog "Bad issue occurred" ([RabbitMqLogger.MessageType]::JobStart) "Bootstrapping" "FileNotFoundException: C:\Goofy.gif"

![LogWatcher output screenshot](/LogWatcher/screenshot.png?raw=true "LogWatcher Output Screenshot")

##Exported Functions:
* New-RmqLogger
* Write-DebugRmqLog
* Write-InfoRmqLog
* Write-WarnRmqLog
* Write-ErrorRmqLog
* Write-FatalRmqLog
