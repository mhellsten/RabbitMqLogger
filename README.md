# RabbitMqLogger

This is a RabbitMQ-based pub/sub logging library for C#.NET, with a PowerShell module wrapper. It also includes a console-based real-time log watcher.

Usage example:

    Import-Module .\RabbitMqLogger.psm1
    Create-RmqLogger "rabbit.local"
    Write-InfoRmqLog "Test logging"
