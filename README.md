# RabbitMqLogger

## What is it?
This is a RabbitMQ-based pub/sub logging library for C#.NET, with a PowerShell module wrapper. It includes:
* Logging class library for C#
* PowerShell module wrapper, for making logging from PS scripts easy
* Real-time watcher console application
* Windows service that writes all log messages to a SQL database. Uses Entity Framework. 

## What's the point?
I have various scripts and services that perform automated tasks or need to be monitored. These are distributed across different machines and therefore it is tedious and impractical to access log files for a quick status check. Using messaging queues, we can easily monitor as much or as little of the published log messages as we want. Since multiple queues can be set up on an exchange, we can have multiple listeners, for example: a real-time viewer, a database writer, and a file writer.

## Pre-Requisites
* A RabbitMQ service
* SQL Server, if you want to store the messages in a database

## Build and Installation:
### Installing the PowerShell module:
Note: There is an optional parameter to install it in the current user's directory instead of in Program Files (shared location). Just add $true as a parameter. 

    .\BuildAndInstallPowerShellModule.ps1

### Installing the DatabaseWriter Service:
1. Build the solution 
2. Create an empty database and a user with dbo membership
3. Copy App.config.example to RabbitMqLogger.DatabaseWriter\bin\Debug and rename it to RabbitMqLogger.DatabaseWriter.exe.config
4. Add your connection string and RabbitMQ host name to the config file   
5. Open *Developer Command Prompt for VS2015* as an Administrator
6. cd to RabbitMqLogger.DatabaseWriter\bin\Debug
7. Run: *installutil RabbitMqLogger.DatabaseWriter.exe*
8. Start the service in the Services console

## Usage Example 
(RabbitMQ running on rabbit.local)

    Import-Module RabbitMqLogger
    New-RmqLogger rabbit.local
    Write-InfoRmqLog "Test logging"
    Write-ErrorRmqLog "Bad issue occurred" $rmq.JobStart "Bootstrapping" "FileNotFoundException: C:\Goofy.gif"

![LogWatcher output screenshot](/RabbitMqLogger.LogWatcher/screenshot.png?raw=true "LogWatcher Output Screenshot")

## Exported Functions:
* New-RmqLogger
* Write-DebugRmqLog
* Write-InfoRmqLog
* Write-WarnRmqLog
* Write-ErrorRmqLog
* Write-FatalRmqLog
